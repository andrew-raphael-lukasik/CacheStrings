/// <summary> Most basic/approachable way of string caching </summary>
// create example:
// public static LazyStringCache<int> MyIntStrings = new LazyStringCache<int>( (t)=>t.ToString() );
// usage example:
// string myString = MyIntStrings[ someIntVariable ];
public class LazyStringCache <T>
{
	System.Collections.Generic.Dictionary<T,string> _lookup;
	System.Func<T,string> _toString;
	public LazyStringCache ( System.Func<T,string> toString ) { this._toString = toString; }
	public string this [ T key ]
	{
		get
		{
			if( _lookup.TryGetValue( key , out string result )==false )
			{
				result = _toString( key );
				_lookup.Add( key , result );
			}
			return result;
		}
	}
	public void Clear () { _lookup.Clear(); }
}
