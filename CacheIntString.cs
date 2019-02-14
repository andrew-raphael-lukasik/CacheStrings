using System;
using System.Collections.Generic;
using UnityEngine;
public sealed class CacheIntString
{
	#region FIELDS

	Dictionary<int,string> _values;
	Func<int,string> _intToString;
	Func<int,int> _hashFunction;

	#endregion
	#region CONSTRUCTORS

	public CacheIntString ( Func<int,int> hashFunction , Func<int,string> intToString  ) : this( hashFunction , intToString , 100 ) {}
	public CacheIntString ( Func<int,int> hashFunction , Func<int,string> intToString , int initialDictCapacity )
	{
		UnityEngine.Assertions.Assert.IsNotNull( hashFunction );
		UnityEngine.Assertions.Assert.IsNotNull( intToString );
		_values = new Dictionary<int,string>( initialDictCapacity );
		_hashFunction = hashFunction;
		_intToString = intToString;
	}
	public CacheIntString ( Func<int,int> hashFunction , Func<int,string> intToString , int initMin , int initMax , int initStep )
	{
		UnityEngine.Assertions.Assert.IsNotNull( hashFunction );
		UnityEngine.Assertions.Assert.IsNotNull( intToString );
		_hashFunction = hashFunction;
		_intToString = intToString;
		_values = new Dictionary<int,string>( initMax-initMin );
		for( int key=initMin ; key<=initMax ; key+=initStep )
		{
			int hash = hashFunction( key );
			string str = intToString( hash );
			UnityEngine.Assertions.Assert.IsNotNull( str );
			try {
				_values.Add( hash , str );
			}
			catch( Exception ex ) { Debug.LogWarning($"Redundant key: { key }, where hash: { hash }"); Debug.LogException( ex ); }
		}
	}
	
	#endregion
	#region OPERATORS

	public string this [ int key ]
	{
		get
		{
			string result;
			int hash = _hashFunction( key );
			if( _values.TryGetValue( hash , out result )==false )//fail safe
			{
				result = _intToString( hash );
				UnityEngine.Assertions.Assert.IsNotNull( result );
				_values.Add( hash , result );
			}
			return result;
		}
	}

	#endregion
}
