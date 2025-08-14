﻿using System.Diagnostics.CodeAnalysis;

namespace CSPP.lib.std.ptr;

internal unsafe struct meta_pointer
{
	public meta_pointer(void* pointer, bool isNative = false)
	{
		Pointer = (nint)pointer;
		IsNative = isNative;
	}
	public meta_pointer(nint pointer, bool isNative = false)
	{
		Pointer = pointer;
		IsNative = isNative;
	}

	public bool IsNative;
	public nint Pointer;

	public static bool operator ==(meta_pointer left, meta_pointer right) => left.Pointer == right.Pointer;
	public static bool operator !=(meta_pointer left, meta_pointer right) => left.Pointer != right.Pointer;

	public static bool operator ==(meta_pointer left, nint right) => left.Pointer == right;
	public static bool operator !=(meta_pointer left, nint right) => left.Pointer != right;

	public readonly meta_pointer<T> As<T>() where T : unmanaged
	{
		return new meta_pointer<T>((T*)Pointer, IsNative);
	}
	public readonly override bool Equals([NotNullWhen(true)] object? obj)
	{
		if(obj is null || obj is not meta_pointer other)
		{
			return false;
		}
		return this == other;
	}
	public readonly override int GetHashCode() => (int)Pointer;
}
internal unsafe struct meta_pointer<TPtr> where TPtr : unmanaged
{
	public meta_pointer(TPtr* pointer, bool isNative = false)
	{
		Pointer = pointer;
		IsNative = isNative;
	}

	public bool IsNative;
	public TPtr* Pointer;

	public static bool operator ==(meta_pointer<TPtr> left, meta_pointer<TPtr> right) => left.Pointer == right.Pointer;
	public static bool operator !=(meta_pointer<TPtr> left, meta_pointer<TPtr> right) => left.Pointer != right.Pointer;

	public static bool operator ==(meta_pointer<TPtr> left, TPtr* right) => left.Pointer == right;
	public static bool operator !=(meta_pointer<TPtr> left, TPtr* right) => left.Pointer != right;

	public static bool operator ==(meta_pointer<TPtr> left, nint right) => left.Pointer == (TPtr*)right;
	public static bool operator !=(meta_pointer<TPtr> left, nint right) => left.Pointer != (TPtr*)right;

	public static bool operator ==(meta_pointer<TPtr> left, void* right) => left.Pointer == (TPtr*)right;
	public static bool operator !=(meta_pointer<TPtr> left, void* right) => left.Pointer != (TPtr*)right;

	public static explicit operator meta_pointer(meta_pointer<TPtr> pointer) => new(pointer.Pointer, pointer.IsNative);
	public static implicit operator meta_pointer<TPtr>(TPtr* pointer) => new(pointer);
	public static implicit operator TPtr*(meta_pointer<TPtr> pointer) => pointer.Pointer;


	public readonly override bool Equals([NotNullWhen(true)] object? obj)
	{
		if(obj is null || obj is not meta_pointer<TPtr> other)
		{
			return false;
		}
		return this == other;
	}
	public readonly override int GetHashCode() => (int)Pointer;
}
