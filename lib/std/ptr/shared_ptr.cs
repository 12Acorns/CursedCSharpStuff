namespace CSPP.lib.std.ptr;

internal unsafe sealed class shared_ptr<TPtr> : IDisposable
	where TPtr : unmanaged
{
	public static shared_ptr<TPtr> Null => new();

	private readonly meta_pointer<TPtr> _refernece;
	private bool _disposedValue;
	private int _refCount;

	public shared_ptr(meta_pointer<TPtr> referencePointer)
	{
		if((nint)referencePointer.Pointer == nint.Zero)
		{
			throw new ArgumentException("Cannot create a unique_ptr with a null reference.", nameof(referencePointer));
		}
		_refernece = referencePointer;
	}
	public shared_ptr(TPtr* reference)
	{
		if(reference == NULL)
		{
			throw new ArgumentException("Cannot create a unique_ptr with a null reference.", nameof(reference));
		}
		_refernece = new meta_pointer<TPtr>(reference, false);
	}
	private shared_ptr()
	{
		_refernece = new meta_pointer<TPtr>(null, false);
	}

	public unsafe shared_ptr<TPtr> copy()
	{
		_refCount++;
		return this;
	}

	public meta_pointer<TPtr> get() => _refernece;
	public TPtr* get_pointer() => _refernece.Pointer;
	public int use_count() => _refCount;
	public void reduce_count()
	{
		if(_refCount > 0)
		{
			_refCount--;
		}
		if(_refCount == 0)
		{
			Dispose();
		}
	}

	public static implicit operator TPtr*(shared_ptr<TPtr> reference) => 
		reference._refernece.Pointer;
	public static implicit operator meta_pointer<TPtr>(shared_ptr<TPtr> reference) => 
		reference._refernece;
	public static implicit operator shared_ptr<TPtr>(meta_pointer<TPtr> reference) => 
		new shared_ptr<TPtr>(reference);
	public static implicit operator shared_ptr<TPtr>(TPtr reference) => 
		new shared_ptr<TPtr>(&reference);
	public static implicit operator shared_ptr<TPtr>(TPtr* reference) => 
		new shared_ptr<TPtr>(reference);

	~shared_ptr()
	{
		Dispose(disposing: false);
	}

	public void Dispose()
	{
		if(_refCount > 0)
		{
			return;
		}

		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
	private void Dispose(bool disposing)
	{
		if(_disposedValue)
		{
			return;
		}
		if(*_refernece.Pointer is IDisposable dispose)
		{
			dispose.Dispose();
		}
		if(_refernece.IsNative)
		{
			std_usage.free((nint)_refernece.Pointer);
		}
		_disposedValue = true;
	}
}
