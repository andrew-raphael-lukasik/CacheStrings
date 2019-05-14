using System;
using System.Collections.Generic;
using UnityEngine;

public class CacheStrings <KEY,HASH>
{
	protected Dictionary<HASH,string> _table;
	protected Func<HASH,string> _hashToString;
	protected Func<KEY,HASH> _hashFunction;

	public CacheStrings ( Func<KEY,HASH> hashFunction , Func<HASH,string> hashToString  ) : this( hashFunction , hashToString , 100 ) {}
	public CacheStrings ( Func<KEY,HASH> hashFunction , Func<HASH,string> hashToString , int initialDictCapacity )
	{
		UnityEngine.Assertions.Assert.IsNotNull( hashFunction );
		UnityEngine.Assertions.Assert.IsNotNull( hashToString );
		_table = new Dictionary<HASH,string>( initialDictCapacity );
		_hashFunction = hashFunction;
		_hashToString = hashToString;
	}
	
	public string this [ KEY key ]
	{
		get
		{
			string result;
			HASH hash = _hashFunction( key );
			if( _table.TryGetValue( hash , out result )==false )//fail safe
			{
				result = _hashToString( hash );
				UnityEngine.Assertions.Assert.IsNotNull( result );
				_table.Add( hash , result );
			}
			return result;
		}
	}

}

public sealed class CacheIntString : CacheStrings<int,int>
{
	public CacheIntString
	(
		Func<int,int> hashFunction ,
		Func<int,string> hashToString ,
		int initMin ,
		int initMax ,
		int initStep
	)
		: base( hashFunction , hashToString , (initMax-initMin)/initStep )
	{
		for( int key=initMin ; key<=initMax ; key+=initStep )
		{
			int hash = hashFunction( key );
			string str = hashToString( hash );
			UnityEngine.Assertions.Assert.IsNotNull( str );
			if( _table.ContainsKey( hash )==false ) _table.Add( hash , str );
			else Debug.LogWarning( $"Redundant key: { key }, where hash: { hash }" );
		}
	}
}

public sealed class CacheUIntString : CacheStrings<uint,uint>
{
	public CacheUIntString
	(
		Func<uint,uint> hashFunction ,
		Func<uint,string> hashToString ,
		uint initMin ,
		uint initMax ,
		uint initStep
	)
		: base( hashFunction , hashToString , Convert.ToInt32((initMax-initMin)/initStep) )
	{
		for( uint key=initMin ; key<=initMax ; key+=initStep )
		{
			uint hash = hashFunction( key );
			string str = hashToString( hash );
			UnityEngine.Assertions.Assert.IsNotNull( str );
			if( _table.ContainsKey( hash )==false ) _table.Add( hash , str );
			else Debug.LogWarning( $"Redundant key: { key }, where hash: { hash }" );
		}
	}
}

public sealed class CacheULongString : CacheStrings<ulong,ulong>
{
	public CacheULongString
	(
		Func<ulong,ulong> hashFunction ,
		Func<ulong,string> hashToString ,
		ulong initMin ,
		ulong initMax ,
		ulong initStep
	)
		: base( hashFunction , hashToString , Convert.ToInt32((initMax-initMin)/initStep) )
	{
		for( ulong key=initMin ; key<=initMax ; key+=initStep )
		{
			ulong hash = hashFunction( key );
			string str = hashToString( hash );
			UnityEngine.Assertions.Assert.IsNotNull( str );
			if( _table.ContainsKey( hash )==false ) _table.Add( hash , str );
			else Debug.LogWarning( $"Redundant key: { key }, where hash: { hash }" );
		}
	}
}

public sealed class CacheDoubleString : CacheStrings<double,double>
{
	public CacheDoubleString
	(
		Func<double,double> hashFunction ,
		Func<double,string> hashToString ,
		double initMin ,
		double initMax ,
		double initStep
	)
		: base( hashFunction , hashToString , Convert.ToInt32((initMax-initMin)/initStep) )
	{
		for( double key=initMin ; key<=initMax ; key+=initStep )
		{
			double hash = hashFunction( key );
			string str = hashToString( hash );
			UnityEngine.Assertions.Assert.IsNotNull( str );
			if( _table.ContainsKey( hash )==false ) _table.Add( hash , str );
			else Debug.LogWarning( $"Redundant key: { key }, where hash: { hash }" );
		}
	}
}

public sealed class CacheDoubleIntString : CacheStrings<double,int>
{
	public CacheDoubleIntString
	(
		Func<double,int> hashFunction ,
		Func<int,string> hashToString ,
		double initMin ,
		double initMax ,
		double initStep
	)
		: base( hashFunction , hashToString , Convert.ToInt32((initMax-initMin)/initStep) )
	{
		for( double key=initMin ; key<=initMax ; key+=initStep )
		{
			int hash = hashFunction( key );
			string str = hashToString( hash );
			UnityEngine.Assertions.Assert.IsNotNull( str );
			if( _table.ContainsKey( hash )==false ) _table.Add( hash , str );
			else Debug.LogWarning( $"Redundant key: { key }, where hash: { hash }" );
		}
	}
}
