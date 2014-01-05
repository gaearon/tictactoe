using System;
using System.Linq;

namespace TicTacToe
{
    class Board : IBoard
    {
        Player? [][] _cells;

        public int Size {
            get { return _cells.Length; }
        }

        public Board (int size)
        {
            _cells = CreateEmptyCells (size);
        }

        public Player? GetCell (int row, int column)
        {
            CheckBounds (row, column);

            return _cells [row] [column];
        }

        public void SetCell (int row, int column, Player player)
        {
            CheckBounds (row, column);

            if (GetCell (row, column).HasValue)
                throw new InvalidOperationException ("The cell is already claimed.");

            _cells [row] [column] = player;
        }

        static Player? [][] CreateEmptyCells (int size)
        {
            return Enumerable
                .Range (0, size)
                .Select (_ => new Player? [size])
                .ToArray ();
        }

        void CheckBounds (int row, int column)
        {
            if (row < 0 || row >= Size)
                throw new ArgumentOutOfRangeException ("row");

            if (column < 0 || column >= Size)
                throw new ArgumentOutOfRangeException ("column");
        }
    }
}

