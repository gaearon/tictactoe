using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    class ConsoleGameIO : IGameIO
    {
        public Tuple<int, int> AskNextMove (Player player, IBoard board)
        {
            Console.WriteLine ("\n\n");
            Console.WriteLine ("{0}, what is your move?\n", FormatPlayer (player));
            Console.WriteLine (FormatBoard (board));
            Console.Write ("\nType A1 to C3: ", FormatPlayer (player));

            return ParseMove (Console.ReadLine ().Trim ().ToUpperInvariant ());
        }

        public void DisplayError (GameError error)
        {
            switch (error) {
            case GameError.CellAlreadyOccupied:
                Console.WriteLine ("Bad move: cell is already occupied.");
                break;
            case GameError.CouldNotParseMove:
                Console.WriteLine ("Bad move. Valid moves are A1 to C3.");
                break;
            default:
                Console.WriteLine ("Something went wrong.");
                break;
            }
        }

        public void DisplayWinner (Player player)
        {
            Console.WriteLine ("Congatulations, {0}! You won.", FormatPlayer (player));
        }

        static string FormatBoard (IBoard board)
        {
            return string.Join ("\n", from row in board.SelectAllRows () select FormatRow (row));
        }

        static string FormatRow (IEnumerable<Player?> row)
        {
            return string.Join ("|", from cell in row select FormatCell (cell));
        }

        static string FormatPlayer (Player player)
        {
            return FormatCell (player);
        }

        static string FormatCell (Player? cell)
        {
            return cell.HasValue ? cell.Value.ToString () : "_";
        }

        static Tuple<int, int> ParseMove (string input)
        {
            return Tuple.Create (
                input [0] - 'A',
                input [1] - '1'
            );
        }
    }
}

