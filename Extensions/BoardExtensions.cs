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

        public static IEnumerable<IEnumerable<Player?>> SelectAllLines (this IBoard board)
        {
            return board.SelectAllDiagonals ()
                .Concat (board.SelectAllRows ())
                .Concat (board.SelectAllColumns ());
        }

        public static IEnumerable<IEnumerable<Player?>> SelectAllRows (this IBoard board)
        {
            return from row in board.SelectIndices ()
                   select board.SelectRow (row);
        }

        public static IEnumerable<IEnumerable<Player?>> SelectAllColumns (this IBoard board)
        {
            return from column in board.SelectIndices ()
                   select board.SelectColumn (column);
        }

        public static IEnumerable<IEnumerable<Player?>> SelectAllDiagonals (this IBoard board)
        {
            return from kind in new [] { DiagonalKind.Primary, DiagonalKind.Secondary }
                   select board.SelectDiagonal (kind);
        }

        public static IEnumerable<Player?> SelectRow (this IBoard board, int row)
        {
            return from column in board.SelectIndices ()
                   select board [row, column];
        }

        public static IEnumerable<Player?> SelectColumn (this IBoard board, int column)
        {
            return from row in board.SelectIndices ()
                   select board [row, column];
        }

        public static IEnumerable<Player?> SelectDiagonal (this IBoard board, DiagonalKind kind)
        {
            return from index in board.SelectIndices ()
                   let row = index
                   let column = (kind == DiagonalKind.Primary)
                       ? index
                       : board.Size - 1 - index
                   select board [row, column];
        }

        public static IEnumerable<int> SelectIndices (this IBoard board)
        {
            return Enumerable.Range (0, board.Size);
        }
    }
}

