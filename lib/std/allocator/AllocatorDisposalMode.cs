namespace CSPP.lib.std.allocator;

internal enum AllocatorDisposalMode : byte
{
	NoClear,
	ClearWhole,
	ClearUsed
}
