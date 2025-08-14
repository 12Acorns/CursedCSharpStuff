namespace CSPP.lib.std.stream;

internal class iostream
{
	public static readonly string endl = Environment.NewLine;
	public static readonly char ends = '\0';

	public static readonly ConsoleStream<object> cout = default;
	public static readonly ConsoleStream<object> cin = default;
}
