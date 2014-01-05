using System;

namespace TicTacToe
{
    class Game
    {
        IBoard board;
        IBoardAnalyzer analyzer;
        IGameIO io;

        public Game (IBoard board, IBoardAnalyzer analyzer, IGameIO io)
        {
            this.board = board;
            this.analyzer = analyzer;
            this.io = io;
        }

        public void Run ()
        {
            Player player = Player.X;
            Player? winner = null;

            do {
                Tuple<int, int> move;
                try {
                    move = io.AskNextMove (player, board);
                } catch {
                    io.DisplayError (GameError.CouldNotParseMove);
                    continue;
                }

                if (!ValidateMove (move)) {
                    io.DisplayError (GameError.CouldNotParseMove);
                    continue;
                }

                try {
                    board[move.Item1, move.Item2] = player;
                } catch (InvalidOperationException) {
                    io.DisplayError (GameError.CellAlreadyOccupied);
                    continue;
                }

                player = GetNextPlayer (player);
                winner = analyzer.DetermineWinner (board);

            } while (!winner.HasValue);

            io.DisplayWinner (winner.Value);
        }

        bool ValidateMove (Tuple<int, int> move)
        {
            return (move.Item1 >= 0 && move.Item1 < board.Size)
                && (move.Item2 >= 0 && move.Item2 < board.Size);
        }

        static Player GetNextPlayer (Player player)
        {
            switch (player) {
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
            var game = new Game (new Board (3), new BoardAnalyzer (), new ConsoleGameIO ());
            game.Run ();
        }
    }
}

