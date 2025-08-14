using System.Runtime.CompilerServices;
using CSPP.lib.std.allocator;
using CSPP.lib.std.ptr;

namespace CSPP.lib.std.text;

internal readonly unsafe struct c_string<Allocator>
	where Allocator : unmanaged, IAllocator
{
	private readonly meta_pointer<char> _start;

	public c_string(Allocator* allocator, ReadOnlySpan<char> content)
	{
		_start = allocator->Allocate<char>(content.Length + 1).Pointer.As<char>();
		_start.Pointer[content.Length] = '\0';
		if(content.Length > 0)
		{
			content.CopyTo(new Span<char>(_start.Pointer, content.Length));
		}
	}
	public c_string(Allocator* allocator, int length)
	{
		if(length < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(length), "Length cannot be negative.");
		}

		_start = allocator->Allocate<char>(length + 1).Pointer.As<char>();
		_start.Pointer[length] = '\0';
	}
	public c_string(int length)
	{
		if(length < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(length), "Length cannot be negative.");
		}

		_start = new meta_pointer<char>((char*)malloc((length + 1) * sizeof(char)), true);
		_start.Pointer[length] = '\0';
	}
	public c_string()
	{
		_start = new meta_pointer<char>((char*)NULLPTR, true);
	}

	public readonly char* Start => _start.Pointer;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int Length()
	{
		int length = 0;
		while (Start[length] != '\0')
		{
			length++;
		}
		return length;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public char GetNoBoundsCheck(int indx) => Start[indx];
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void SetNoBoundsCheck(int indx, char value) => Start[indx] = value;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Span<char> GetContent(Span<char> destination, bool withNullTerminator = false)
	{
		var len = Length();

		if(destination.Length < len)
		{
			throw new ArgumentException($"Destination span is too small. Required length: {len}");
		}
		if(withNullTerminator && destination.Length < len + 1)
		{
			throw new ArgumentException($"Destination span is too small for null terminator. Required length: {len + 1}");
		}
		if(len == 0)
		{
			if(withNullTerminator)
			{
				destination[0] = '\0';
			}
			return destination;
		}
		fixed(char* destPtr = destination)
		{
			Buffer.MemoryCopy(Start, destPtr, destination.Length * sizeof(char), (len + (withNullTerminator ? 1 : 0)) * sizeof(char));
		}
		return destination;
	}

	public char this[int idx]
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			var len = Length();
			if(idx < 0 || idx >= len)
			{
				throw new IndexOutOfRangeException($"Index {idx} is out of range for c_string of length {len}.");
			}
			return Start[idx];
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		set
		{
			var len = Length();
			if(idx < 0 || idx >= len)
			{
				throw new IndexOutOfRangeException($"Index {idx} is out of range for c_string of length {len}.");
			}
			Start[idx] = value;
		}
	}

	public string ToStringLowAlloc() => GetContent(stackalloc char[Length()]).ToString();
	public override string ToString() => GetContent(new Span<char>(Start, Length())).ToString();
}