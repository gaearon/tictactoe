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

        public Player? this [int row, int column] {
            get {
                CheckBounds (row, column);

                return _cells [row] [column];
            } set {
                CheckBounds (row, column);

                if (this [row, column].HasValue)
                    throw new InvalidOperationException ("The cell is already claimed.");

                _cells [row] [column] = value;
            }
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

