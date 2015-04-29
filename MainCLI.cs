using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lea.Game;

namespace lea.CLI
{
    public class MainCLI
    {
        public enum CLI
        {
            playgame
        }

        public static void Listen()
        {
            string command;
            while (true)
            {
                command = Console.ReadLine();
                while (!IsValidCommand(command))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid command.");
                    Console.ForegroundColor = ConsoleColor.White;
                    command = Console.ReadLine();
                }
                if (command.Contains("playgame"))
                {
                    string game;
                    game = command.Substring(9);
                    lea.Game.Engine.StartGame(game);
                }
            }

            
        }

        private static bool IsValidCommand(string command)
        {
            return true;
        }
    }
}
