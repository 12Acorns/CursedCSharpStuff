using CSPP.Memory;
using System.Runtime.InteropServices;
using static CSPP.Memory.StdLib;
using std = CSPP.Memory;

unsafe
{
	main();

	int main()
	{
		_ =	cout << "Hello, World!" << endl;
		_ =	std::StdLib.cout << "Hello, World!" << std::StdLib.endl;
		printStuff();

		// cause GC pressure, causes finalizer to be called
		// causes unique_pointer to be freed inturn
		// in real world case just wait until gc natually runs
		while(true)
		{
			var s = new string('a', 100);
		}
		return 1;
	}
	void printStuff()
	{
		std::unique_ptr<std::Arena> arena = std::Arena.CreateUnmanaged<int>(100);


		int iter = 0;
		while(arena.getPointer()->TryNewBlock(20 * sizeof(int), out var ptr2))
		{
			for(int i = 0; i < ptr2.LengthFromOffsetInBytes / sizeof(int); i++)
			{
				int* offset = (int*)ptr2.OffsetFromStart;
				offset[i] = iter;

				_ = cout << offset[i] << endl;
				iter++;
			}
			_ = cout << endl;
		}
	}
}