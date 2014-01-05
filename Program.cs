using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    class MainClass
    {
        static Player GetNextPlayer (Player p)
        {
            switch (p) {
            case Player.X:
                return Player.O;
            case Player.O:
                return Player.X;
            default:
                throw new NotImplementedException ();
            }
        }

        public static void Main (string[] args)
        {
            var game = new TicTacToe ();
            var player = Player.X;

            while (!game.HasWinner) {
                Console.WriteLine ("{0}, what will be your move?\n", FormatPlayer (player));
                Console.WriteLine (FormatGame (game));
                Console.Write ("\nType A1 to C3: ", FormatPlayer (player));

                try {
                    var move = ParseMove (Console.ReadLine ().Trim ());
                    game.MakeMove (player, move.Item1, move.Item2);
                    player = GetNextPlayer (player);
                } catch (Exception ex) {
                    Console.WriteLine ("I can't do that. {0}", ex.Message);
                } finally {
                    Console.WriteLine ("\n\n");
                }
            }

            foreach (var msg in FormatWinnerMessage (game.Winner.Value))
                Console.Write (msg);
        }

        static string FormatPlayer (Player player)
        {
            return FormatCell (player);
        }

        static string FormatCell (Player? cell)
        {
            return cell.HasValue ? cell.Value.ToString () : "_";
        }

        static string FormatRow (IEnumerable<Player?> row)
        {
            return string.Join ("|", row.Select (FormatCell));
        }

        static string FormatGame (TicTacToe game)
        {
            return string.Join ("\n", game.GetRows ().Select (FormatRow));
        }

        static Tuple<int, int> ParseMove (string input)
        {
            try {
                var move = Tuple.Create (
                    input [0] - 'A',
                    input [1] - '1'
                );

                if (!ValidateMove (move))
                    throw new Exception ();

                return move;

            } catch (Exception ex) {
                throw new Exception ("Stick to simple things like A1 or C2.", ex);
            }
        }

        static bool ValidateMove (Tuple<int, int> move)
        {
            return move.Item1 >= 0 && move.Item1 <= 2
                && move.Item2 >= 0 && move.Item2 <= 2;
        }

        static IEnumerable<string> FormatWinnerMessage (Player player)
        {
            while (true) {
                for (var i = 0; i < 30; i++)
                    yield return FormatPlayer (player);

                yield return "   CONGRATULATIONS   ";
            }
        }
    }
}