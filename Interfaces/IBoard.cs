using System;

namespace TicTacToe
{
    interface IBoard
    {
        int Size { get; }
        Player? this [int row, int column] { get; set; }
    }
}

