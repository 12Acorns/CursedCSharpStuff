namespace CSPP.Memory;

internal readonly unsafe struct smart_pointer<T> where T : unmanaged
{
	public smart_pointer(T* pointer, bool isNative = false)
	{
		if(pointer == null)
		{
			throw new ArgumentNullException(nameof(pointer), "Pointer cannot be null.");
		}
		Pointer = pointer;
		IsNative = isNative;
	}

	public readonly T* Pointer;
	public readonly bool IsNative;
}
