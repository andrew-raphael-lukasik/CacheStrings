// creation:
// public static LazyCacheStrings<int> MyIntStrings = new LazyCacheStrings<int>( (t)=>t.ToString() );
// usage:
// string myString = MyIntStrings[ someIntVariable ];

/// <summary> Most basic/approachable/triage way of string caching. </summary>
public class LazyCacheStrings <T>
{

	System.Collections.Generic.Dictionary<T,string> _lookup;
	readonly System.Func<T,string> _toString;
	
	public LazyCacheStrings ( System.Func<T,string> toString , int initialCapacity = 1000 )
	{
		this._toString = toString;
		this._lookup = new System.Collections.Generic.Dictionary<T,string>( initialCapacity );
	}
	
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
