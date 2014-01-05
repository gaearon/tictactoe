using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    class BoardAnalyzer : IBoardAnalyzer
    {
        public Player? FindWinner (IBoard board)
        {
            return board
                .SelectAllLines ()
                .Select (FindLineWinner)
                .FirstOrDefault (winner => winner.HasValue);
        }

        static Player? FindLineWinner (IEnumerable<Player?> line)
        {
            try {
                return line
                    .Distinct ()
                    .Single ();
            } catch (InvalidOperationException) {
                return null;
            }
        }
    }
}

