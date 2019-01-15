# CacheIntString
Specialized <int,string> storage to ease your fight with all those UI-related memory leaks. You provide string-formatting method + numerical range and the rest will be taken care of.

Use cases:
- Timers (!)
- Any kind of number UI fields where strings repeat (eventually) and pool size is limited
