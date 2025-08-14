using System.Runtime.CompilerServices;

namespace CSPP;

internal readonly struct ConsoleStream<T>
{
	public ConsoleStream(T[] val) => _value = new ReadOnlyMemory<T>(val);
	public ConsoleStream(T val) => _value = new ReadOnlyMemory<T>([val]);

	private readonly ReadOnlyMemory<T> _value;

	public static ConsoleStream<T> operator >>(ConsoleStream<T> stream, in int data)
	{
		ref var s = ref Unsafe.AsRef(in data);
		if(!int.TryParse(Console.ReadLine(), out var val))
		{
			s = -1;
		}
		else
		{
			s = val;
		}
		return stream;
	}
	public static ConsoleStream<T> operator >>(ConsoleStream<T> stream, in object data)
	{
		ref var s = ref Unsafe.AsRef(in data);
		s = Console.ReadLine();
		return stream;
	}
	public static ConsoleStream<T> operator >>(ConsoleStream<T> stream, in string? data)
	{
		ref var s = ref Unsafe.AsRef(in data);
		s = Console.ReadLine();
		return stream;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> stream, ConsoleStream<T> data)
	{
		Console.Out.Write(data._value);
		return data;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> stream, string data) =>
		stream << data.AsSpan();
	public static ConsoleStream<T> operator <<(ConsoleStream<T> stream, ReadOnlySpan<char> data)
	{
		Console.Out.Write(data);
		return stream;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> stream, float data)
	{
		Console.Out.Write(data);
		return stream;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> stream, double data)
	{
		Console.Out.Write(data);
		return stream;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> stream, ulong data)
	{
		Console.Out.Write(data);
		return stream;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> stream, long data)
	{
		Console.Out.Write(data);
		return stream;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> stream, uint data)
	{
		Console.Out.Write(data);
		return stream;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> stream, int data)
	{
		Console.Out.Write(data);
		return stream;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> stream, ushort data)
	{
		Console.Out.Write(data);
		return stream;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> stream, short data)
	{
		Console.Out.Write(data);
		return stream;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> stream, sbyte data)
	{
		Console.Out.Write(data);
		return stream;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> stream, byte data)
	{
		Console.Out.Write(data);
		return stream;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> stream, object data)
	{
		Console.Out.Write(data);
		return stream;
	}
	public static implicit operator ConsoleStream<T>(T _c) => new(_c);
}
