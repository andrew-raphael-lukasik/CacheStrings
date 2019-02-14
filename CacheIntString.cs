using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CacheIntString
{
	#region FIELDS

	[SerializeField] protected Dictionary<int,string> _values;
	System.Func<int,string> _intToString;

	#endregion
	#region CONSTRUCTORS

	public CacheIntString ( System.Func<int,string> intToString  ) : this( intToString , 100 ) {}
	public CacheIntString ( System.Func<int,string> intToString , int initialDictCapacity )
	{
		UnityEngine.Assertions.Assert.IsNotNull( intToString );
		_values = new Dictionary<int,string>( initialDictCapacity );
		_intToString = intToString;
	}
	public CacheIntString ( System.Func<int,string> intToString , int initMin , int initMax )
	{
		UnityEngine.Assertions.Assert.IsNotNull( intToString );
		_values = new Dictionary<int,string>( initMax-initMin );
		for( int i=initMin ; i<=initMax ; i++ )
		{
			string str = intToString( i );
			UnityEngine.Assertions.Assert.IsNotNull( str );
			_values.Add( i , str );
		}
		_intToString = intToString;
	}
	
	#endregion
	#region OPERATORS

	public string this [ int key ]
	{
		get
		{
			string result;
			if( _values.TryGetValue( key , out result )==false )//fail safe
			{
				result = _intToString( key );
				UnityEngine.Assertions.Assert.IsNotNull( result );
				_values.Add( key , result );
			}
			return result;
		}
	}

	#endregion
}
