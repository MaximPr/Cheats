using CheatsLib;
using System;

namespace CheatsExample
{
    class Program
    {
        public class Player
        {
            public string Name;
            public int Progress;
            public void DoSomething()
            {
                Console.WriteLine("something done...");
            }

            public override string ToString()
            {
                return $"(Name: {Name}; Progress: {Progress})";
            }
        }

        static bool quit = false;
        static void Main(string[] args)
        {
            Cheats<Player> cheats = new Cheats<Player>();
            RegisterCheats(cheats);

            Player p = new Player { Name = "Alex" };

            Console.WriteLine("Enter cheat! For help write '/help'");

            while (!quit)
            {
                Console.Write(">>");
                string message = Console.ReadLine();
                bool result = cheats.TryRunCheat(message, p);
                if (!result)
                    Console.WriteLine("format /[cheat_name] or /[cheat_name] [args]");
            }
        }

        static void RegisterCheats(Cheats<Player> cheats)
        {
            cheats.RegisterCheat("help", (p) => Console.WriteLine(cheats.GetCheatDescription()), "Print all cheat descriptions");
            cheats.RegisterCheat("info", (p) => Console.WriteLine("PersonInfo: " + p.ToString()), "Print my info");
            cheats.RegisterCheat("dosomething", (p) => p.DoSomething(), "run Player.DoSomething()");
            cheats.RegisterCheat<string>("setname", (p, name) => p.Name = name, "Set name");
            cheats.RegisterCheat<int>("setprogress", (p, progress) => p.Progress = progress, "Set progress");
            cheats.RegisterCheat("quit", (p) => quit = true, "Quit");
        }
    }
}
