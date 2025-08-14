using CSPP.lib.std.ptr;

namespace CSPP.lib.std.allocator;

internal interface IAllocator
{
	public IAllocReturn Allocate<T>(int length) where T : unmanaged;
}
internal interface IAllocReturn
{
	public meta_pointer Pointer { get; }
}
