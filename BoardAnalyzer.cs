using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    class BoardAnalyzer : IBoardAnalyzer
    {
        public Player? FindWinner (IBoard board)
        {
            return (
                from line in board.SelectAllLines ()
                let winner = FindLineWinner (line)
                where winner.HasValue
                select winner
            ).FirstOrDefault ();
        }

        static Player? FindLineWinner (IEnumerable<Player?> line)
        {
            try {
                return line.Distinct ().Single ();
            } catch (InvalidOperationException) {
                return null;
            }
        }
    }
}

