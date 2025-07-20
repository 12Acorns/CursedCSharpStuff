namespace CSPP.Memory;

internal readonly record struct ArenaReference
{
	public ArenaReference(nint start, nint offset, int length) =>
		(Start, OffsetFromStart, LengthFromOffsetInBytes) = (start, offset, length);

	public nint Start { get; internal init; }
	public nint OffsetFromStart { get; internal init; }
	public int LengthFromOffsetInBytes { get; internal init; }
}
