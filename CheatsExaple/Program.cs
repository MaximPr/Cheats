using CheatsLib;
using System;

namespace CheatsExaple
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
            var cheats = InitCheats();

            Player p = new Player { Name = "Alex" };

            Console.WriteLine("Enter cheat!");

            while (!quit)
            {
                Console.Write(">>");
                string message = Console.ReadLine();
                bool result = cheats.TryRunCheat(message, p);
                if (!result)
                    Console.WriteLine("for help write '/help'");

            }
        }

        static Cheats<Player> InitCheats()
        {
            Cheats<Player> cheats = new Cheats<Player>();
            cheats.RegisterCheat("help", (p) => Console.WriteLine(cheats.GetCheatDescription()), "Print all cheat descriptions");
            cheats.RegisterCheat("info", (p) => Console.WriteLine("PersonInfo: " + p), "Print my info");
            cheats.RegisterCheat("dosomething", (p) => p.DoSomething(), "run Player.DoSomething()");
            cheats.RegisterCheat<string>("setname", (p, name) => p.Name = name, "Set name");
            cheats.RegisterCheat<int>("setprogress", (p, progress) => p.Progress = progress, "Set progress");
            cheats.RegisterCheat("quit", (p) => quit = true, "Quit");

            return cheats;
        }
    }
}
