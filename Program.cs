using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    class MainClass
    {
        public static void Main (string[] args)
        {
            var game = new Game (new Board (3), new BoardAnalyzer (), new ConsoleGameIO ());
            game.Run ();
        }
    }
}