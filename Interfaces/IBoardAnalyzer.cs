using System;

namespace TicTacToe
{
    interface IBoardAnalyzer
    {
        Player? DetermineWinner (IBoard board);
    }
}

