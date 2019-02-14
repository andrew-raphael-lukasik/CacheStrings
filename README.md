# CacheIntString
Specialized <int,string> storage to ease your fight with all those UI-related memory leaks. You provide string-formatting method + numerical range and the rest will be taken care of.
#
Use cases:
- Timers (!)
- Any kind of number UI fields where strings repeat (eventually) and pool size can be imagined as limited
#
```C#
using UnityEngine;
using UnityEngine.UI;
public class TestCacheIntString : MonoBehaviour
{
    public Text _displaySeconds;
    public Text _displayMinutes;
    public Text _displayHours;
    public Text _displayDays;

    public double _time = 0;
    CacheIntString cacheSeconds = new CacheIntString( (i) => ( i%60 ).ToString("00") , 0 , 59 );
    CacheIntString cacheMinutes = new CacheIntString( (i) => ( i/60 ).ToString("00") , 0 , 59 );
    CacheIntString cacheHours = new CacheIntString( (i) => ( i/(60*60) ).ToString("00") , 0 , 24 );
    CacheIntString cacheDays = new CacheIntString( (i) => ( i/(60*60*24) ).ToString() , 0 , 31 );

    void Update ()
    {
        _time += Time.deltaTime;
        int seconds = Mathf.FloorToInt( (float)_time );
        _displaySeconds.text = cacheSeconds[ seconds%60 ];
        _displayMinutes.text = cacheSeconds[ seconds/60 ];
        _displayHours.text = cacheSeconds[ seconds/(60*60)%24 ];
        _displayDays.text = cacheSeconds[ seconds/(60*60*24) ];
    }
```
#
