# Init
```c#
Cheats<Player> cheats = new Cheats<Player>();
```
  
# Register
```c#
cheats.RegisterCheat("dosomething", (p) => p.DoSomething(), "run Player.DoSomething()");
```

# Run
```c#
cheats.TryRunCheat("/dosomething", p)
```
