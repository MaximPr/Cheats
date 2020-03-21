Init
```c#
Cheats<Player> cheats = new Cheats<Player>();
```
  
Register
```c#
cheats.RegisterCheat("dosomething", (p) => p.DoSomething(), "run Player.DoSomething()");
cheats.RegisterCheat<string>("setname", (p, name) => p.Name = name, "Set name");
```

Run
```c#
cheats.TryRunCheat("/dosomething", p)
cheats.TryRunCheat("/setname Alex", p)
```
