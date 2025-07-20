using System.Runtime.InteropServices;

namespace CSPP.Memory;

internal static unsafe class StdLib
{
	public static readonly string endl = Environment.NewLine;
	public static readonly ConsoleStream<object> cout = default;
	public static readonly ConsoleStream<object> cin = default;

	public static nint realloc(void* ptr, int size) =>
		Marshal.ReAllocHGlobal((nint)ptr, size);
	public static nint realloc(nint ptr, int size) =>
		Marshal.ReAllocHGlobal(ptr, size);
	public static nint malloc(int sizeInBytes) =>
		Marshal.AllocHGlobal(sizeInBytes);
	public static void free(nint ptr) =>
		Marshal.FreeHGlobal(ptr);
	public static void free(void* ptr) =>
		Marshal.FreeHGlobal((nint)ptr);

}
