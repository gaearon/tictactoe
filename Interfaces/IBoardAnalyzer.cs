using System;

namespace TicTacToe
{
    interface IBoardAnalyzer
    {
        Player? FindWinner (IBoard board);
    }
}

