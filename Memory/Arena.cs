using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static CSPP.Memory.StdLib;

namespace CSPP.Memory;

[StructLayout(LayoutKind.Explicit)]
internal unsafe record struct Arena : IDisposable
{
	private static readonly int _size = sizeof(Arena);

	[FieldOffset(0)]
	private readonly nint _start;
	[FieldOffset(8)]
	private nint _currentOffset;
	[FieldOffset(16)]
	private readonly int _lengthInBytes;

	public bool TryNewBlock(int lengthInBytes, out ArenaReference reference)
	{
		reference = default;
		int currentLength = (int)(_currentOffset - _start);
		if(currentLength + lengthInBytes > _lengthInBytes || lengthInBytes <= 0)
		{
			return false;
		}
		reference = new ArenaReference(_start, _currentOffset, lengthInBytes);
		_currentOffset += lengthInBytes;
		return true;
	}

	public readonly void Dispose() => free(_start);

	public static unsafe smart_pointer<Arena> CreateUnmanaged<T>(int length) where T : unmanaged => CreateUnTrackedByGC(length, sizeof(T));
	public static unsafe smart_pointer<Arena> Create<T>(int length) => CreateUnTrackedByGC(length, Unsafe.SizeOf<T>());

	private static smart_pointer<Arena> CreateUnTrackedByGC(int length, int size)
	{
		if(length <= 0 || size <= 0)
		{
			throw new ArgumentOutOfRangeException("Length and size must be greater than zero.");
		}
		var arenaPtr = malloc(_size);
		if(arenaPtr == nint.Zero)
		{
			throw new OutOfMemoryException("Failed to allocate memory for Arena.");
		}
		*(long*)arenaPtr = malloc(length * size);
		*(long*)(arenaPtr + 8) = *(long*)arenaPtr;
		*(int*)(arenaPtr + 16) = length * size;
		return new smart_pointer<Arena>((Arena*)arenaPtr, true);
	}
}
