using System.Numerics;
using System.Runtime.CompilerServices;

namespace CSPP;

internal readonly struct ConsoleStream<T>
{
	public ConsoleStream(T[] _value) => value = new ReadOnlyMemory<T>(_value);
	public ConsoleStream(T _value) => value = new ReadOnlyMemory<T>([_value]);

	private readonly ReadOnlyMemory<T> value;

	public static ConsoleStream<T> operator >>(ConsoleStream<T> _this, in int _out)
	{
		ref var _s = ref Unsafe.AsRef(in _out);
		if(!int.TryParse(Console.ReadLine(), out var _val))
		{
			_s = -1;
		}
		else
		{
			_s = _val;
		}
		return _this;
	}
	public static ConsoleStream<T> operator >>(ConsoleStream<T> _this, in object _out)
	{
		ref var _s = ref Unsafe.AsRef(in _out);
		_s = Console.ReadLine();
		return _this;
	}
	public static ConsoleStream<T> operator >>(ConsoleStream<T> _this, in string? _out)
	{
		ref var _s = ref Unsafe.AsRef(in _out);
		_s = Console.ReadLine();
		return _this;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> _this, ConsoleStream<T> _other)
	{
		Console.Out.Write(_other.value);
		return _other;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> _this, string _other) =>
		_this << _other.AsSpan();
	public static ConsoleStream<T> operator <<(ConsoleStream<T> _this, ReadOnlySpan<char> _other)
	{
		Console.Out.Write(_other);
		return _this;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> _this, float _other)
	{
		Console.Out.Write(_other);
		return _this;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> _this, double _other)
	{
		Console.Out.Write(_other);
		return _this;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> _this, ulong _other)
	{
		Console.Out.Write(_other);
		return _this;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> _this, long _other)
	{
		Console.Out.Write(_other);
		return _this;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> _this, uint _other)
	{
		Console.Out.Write(_other);
		return _this;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> _this, int _other)
	{
		Console.Out.Write(_other);
		return _this;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> _this, ushort _other)
	{
		Console.Out.Write(_other);
		return _this;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> _this, short _other)
	{
		Console.Out.Write(_other);
		return _this;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> _this, sbyte _other)
	{
		Console.Out.Write(_other);
		return _this;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> _this, byte _other)
	{
		Console.Out.Write(_other);
		return _this;
	}
	public static ConsoleStream<T> operator <<(ConsoleStream<T> _this, object _other)
	{
		Console.Out.Write(_other);
		return _this;
	}
	public static implicit operator ConsoleStream<T>(T _c) => new(_c);
}
