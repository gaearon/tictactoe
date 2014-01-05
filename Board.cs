using System;
using System.Linq;

namespace TicTacToe
{
    class Board : IBoard
    {
        Player? [,] _cells;

        public int Size {
            get { return _cells.GetLength (0); }
        }

        public Board (int size)
        {
            _cells = new Player? [size, size];
        }

        public Player? this [int row, int column] {
            get {
                return _cells [row, column];
            } set {
                if (_cells [row, column].HasValue)
                    throw new InvalidOperationException ("The cell is already claimed.");

                _cells [row, column] = value;
            }
        }
    }
}

