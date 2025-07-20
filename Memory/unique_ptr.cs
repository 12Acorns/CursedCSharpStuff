namespace CSPP.Memory;

internal unsafe sealed class unique_ptr<TPtr> : IDisposable
	where TPtr : unmanaged
{
	private readonly smart_pointer<TPtr> _refernece;
	private bool _disposedValue;

	public unique_ptr(smart_pointer<TPtr> referencePointer)
	{
		if((nint)referencePointer.Pointer == nint.Zero)
		{
			throw new ArgumentException("Cannot create a unique_ptr with a null reference.", nameof(referencePointer));
		}
		_refernece = referencePointer;
	}
	public unique_ptr(TPtr* reference, bool native)
	{
		if((nint)reference == nint.Zero)
		{
			throw new ArgumentException("Cannot create a unique_ptr with a null reference.", nameof(reference));
		}
		_refernece = new smart_pointer<TPtr>(reference, native);
	}
	public unique_ptr(TPtr* reference)
	{
		if((nint)reference == nint.Zero)
		{
			throw new ArgumentException("Cannot create a unique_ptr with a null reference.", nameof(reference));
		}
		_refernece = new smart_pointer<TPtr>(reference, false);
	}

	public smart_pointer<TPtr> get() => _refernece;
	public TPtr* getPointer() => _refernece.Pointer;

	public static implicit operator TPtr*(unique_ptr<TPtr> reference) => 
		reference._refernece.Pointer;
	public static implicit operator smart_pointer<TPtr>(unique_ptr<TPtr> reference) => 
		reference._refernece;
	public static implicit operator unique_ptr<TPtr>(smart_pointer<TPtr> reference) => 
		new unique_ptr<TPtr>(reference);
	public static implicit operator unique_ptr<TPtr>(TPtr reference) => 
		new unique_ptr<TPtr>(&reference);
	public static implicit operator unique_ptr<TPtr>(TPtr* reference) => 
		new unique_ptr<TPtr>(reference);

	~unique_ptr()
	{
		Dispose(disposing: false);
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
	private void Dispose(bool disposing)
	{
		if(!_disposedValue)
		{
			if(*_refernece.Pointer is IDisposable dispose)
			{
				dispose.Dispose();
			}
			if(_refernece.IsNative)
			{
				StdLib.free((nint)_refernece.Pointer);
			}
			_disposedValue = true;
		}
	}
}
