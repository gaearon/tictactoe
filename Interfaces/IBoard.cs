using System;

namespace TicTacToe
{
    interface IBoard
    {
        int Size { get; }
        Player? GetCell (int row, int column);
        void SetCell (int row, int column, Player player);
    }
}

