using CheatsLib;
using System;

namespace CheatsExaple
{
    class Program
    {
        public class Player
        {
            public string Name;
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
                cheats.TryRunCheat(message, p);
            }
        }

        static Cheats<Player> InitCheats()
        {
            Cheats<Player> cheats = new Cheats<Player>();
            cheats.RegisterCheat("help", (p) => Console.WriteLine(cheats.GetCheatDescription()), "Print all cheat descriptions");
            cheats.RegisterCheat("getname", (p) => Console.WriteLine("Name: " + p.Name), "Print my name");
            cheats.RegisterCheat<string>("setname", (p, name) => p.Name = name, "Set my name");
            cheats.RegisterCheat("quit", (p) => quit = true, "Quit");
            cheats.RegisterCheat("q", (p) => quit = true, "Quit");

            return cheats;
        }
    }
}
