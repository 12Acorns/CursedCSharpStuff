using System.Runtime.InteropServices;

namespace CSPP.lib.std;

internal static unsafe class std_usage
{
	public static nint realloc(void* ptr, int size) =>
		Marshal.ReAllocHGlobal((nint)ptr, size);
	public static nint realloc(nint ptr, int size) =>
		Marshal.ReAllocHGlobal(ptr, size);
	public static nint malloc(nint sizeInBytes) =>
		Marshal.AllocHGlobal(sizeInBytes);
	public static nint malloc(int sizeInBytes) =>
		Marshal.AllocHGlobal(sizeInBytes);
	public static void free(nint ptr) =>
		Marshal.FreeHGlobal(ptr);
	public static void free(void* ptr) =>
		Marshal.FreeHGlobal((nint)ptr);
}
