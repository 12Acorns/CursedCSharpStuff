using CSPP.lib.std.allocator.arena;
using CSPP.lib.std.allocator;
using CSPP.lib.std.text;

unsafe
{
	main();

	int main()
	{
		var arena = ArenaAllocator.CreateUnmanaged<char>(30, AllocatorDisposalMode.ClearUsed);

		var c_str = new c_string<ArenaAllocator>(arena.Pointer, "Hello, World!");

		Console.WriteLine(c_str);
		Console.Out.WriteLine(c_str.GetContent(stackalloc char[c_str.Length()]));

		arena.Pointer->Dispose();

		Console.WriteLine(c_str);
		Console.Out.WriteLine(c_str.GetContent(stackalloc char[c_str.Length()]));

		return 1;
	}
}