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
    CacheIntString cacheSeconds = new CacheIntString( (i)=>i%60 , (hash)=>hash.ToString("00") , 0 , 59 , 1 );
    CacheIntString cacheMinutes = new CacheIntString( (i)=>i/60%60 , (hash)=>hash.ToString("00") , 0 , 60 , 60 );
    CacheIntString cacheHours = new CacheIntString( (i)=>i/(60*60)%24 , (hash)=>hash.ToString("00") , 0 , 24 , 60*60 );
    CacheIntString cacheDays = new CacheIntString( (i)=>i/(60*60*24) , (hash)=>hash.ToString() , 0 , 31 , 60*60*24 );
    void Update ()
    {
        _time += Time.deltaTime;
        int seconds = Mathf.FloorToInt( (float)_time );
        _displaySeconds.text = cacheSeconds[ seconds ];
        _displayMinutes.text = cacheMinutes[ seconds ];
        _displayHours.text = cacheHours[ seconds ];
        _displayDays.text = cacheDays[ seconds ];
    }
}
```
#
