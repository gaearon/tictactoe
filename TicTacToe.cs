using System;
using System.Linq;
using System.Collections.Generic;

namespace TicTacToe
{
    class TicTacToe
    {
        const int Length = 3;

        private bool?[][] _data;
        private bool? _winner;

        public TicTacToe ()
        {
            _data = Enumerable
                .Range (0, Length)
                .Select (_ => new bool? [Length])
                .ToArray ();
        }

        public bool? GetCell (int row, int column)
        {
            return _data [row] [column];
        }

        public IEnumerable<bool?> GetRow (int index)
        {
            return _data [index];
        }

        IEnumerable<int> GetIndices ()
        {
            return Enumerable.Range (0, Length);
        }

        public IEnumerable<bool?> GetColumn (int index)
        {
            return GetIndices ()
                .Select (GetRow)
                .Select (row => row.ElementAt (index));
        }

        public IEnumerable<bool?> GetDiagonal (bool ltr)
        {
            return GetIndices ()
                .Select (i => Tuple.Create (i, ltr ? i : Length - 1 - i))
                .Select (pos => GetCell (pos.Item1, pos.Item2));
        }

        public IEnumerable<IEnumerable<bool?>> GetRows ()
        {
            return GetIndices ()
                .Select (GetRow);
        }

        public IEnumerable<IEnumerable<bool?>> GetColumns ()
        {
            return GetIndices ()
                .Select (GetColumn);
        }

        public IEnumerable<IEnumerable<bool?>> GetDiagonals ()
        {
            return new [] { true, false }
                .Select (GetDiagonal);
        }

        public IEnumerable<IEnumerable<bool?>> GetVectors ()
        {
            return GetDiagonals ()
                .Concat (GetRows ())
                .Concat (GetColumns ());
        }

        static bool? FindWinner (IEnumerable<bool?> vector)
        {
            try {
                return vector
                    .Distinct ()
                    .Single ();
            } catch (InvalidOperationException) {
                return null;
            }
        }

        static bool? FindWinner (IEnumerable<IEnumerable<bool?>> vectors)
        {
            return vectors
                .Select (FindWinner)
                .FirstOrDefault (v => v != null);
        }

        public bool? FindWinner ()
        {
            return FindWinner (GetVectors ());
        }

        public bool MakeMove (int row, int column, bool move)
        {
            if (_winner.HasValue)
                throw new InvalidOperationException ("The game is already won.");

            if (_data [row] [column].HasValue)
                throw new InvalidOperationException ("This cell is already taken.");

            _data [row] [column] = move;
            _winner = FindWinner ();

            return move == _winner;
        }

        public bool? Winner {
            get { return _winner; }
        }
    }
}