using System;

namespace TicTacToe
{
    interface IGameIO
    {
        Tuple<int, int> AskNextMove (Player player, IBoard board);
        void DisplayError (GameError error);
        void DisplayWinner (Player player);
    }
}

