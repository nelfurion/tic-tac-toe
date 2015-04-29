using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lea.Game
{
    class Engine
    {
        public static void StartGame(string game)
        {
            switch (game)
            {
                case "tictactoe":
                        TicTacToe();
                    break;
                default:
                    break;
            }
        }

        private static void TicTacToe()
        {
            lea.Game.TicTacToe.StartGame();
        }
    }
}
