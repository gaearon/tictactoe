using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    static class BoardExtensions
    {
        public enum DiagonalKind {
            Primary,
            Secondary
        }

        public static IEnumerable<Player?> GetRow (this IBoard board, int row)
        {
            return from column in board.EnumerateIndices ()
                   select board.GetCell (row, column);
        }

        public static IEnumerable<Player?> GetColumn (this IBoard board, int column)
        {
            return from row in board.EnumerateIndices ()
                   select board.GetCell (row, column);
        }

        public static IEnumerable<Player?> GetDiagonal (this IBoard board, DiagonalKind kind)
        {
            return from index in board.EnumerateIndices ()
                   let row = index
                   let column = (kind == DiagonalKind.Primary)
                       ? index
                       : board.Size - 1 - index
                   select board.GetCell (row, column);
        }

        public static IEnumerable<int> EnumerateIndices (this IBoard board)
        {
            return Enumerable.Range (0, board.Size);
        }

        public static IEnumerable<IEnumerable<Player?>> EnumerateRows (this IBoard board)
        {
            return from row in board.EnumerateIndices ()
                   select board.GetRow (row);
        }

        public static IEnumerable<IEnumerable<Player?>> EnumerateColumns (this IBoard board)
        {
            return from column in board.EnumerateIndices ()
                   select board.GetColumn (column);
        }

        public static IEnumerable<IEnumerable<Player?>> EnumerateDiagonals (this IBoard board)
        {
            return from kind in new [] { DiagonalKind.Primary, DiagonalKind.Secondary }
                   select board.GetDiagonal (kind);
        }

        public static IEnumerable<IEnumerable<Player?>> EnumerateLines (this IBoard board)
        {
            return board.EnumerateDiagonals ()
                .Concat (board.EnumerateRows ())
                .Concat (board.EnumerateColumns ());
        }
    }
}

