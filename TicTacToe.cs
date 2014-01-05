using System;
using System.Linq;
using System.Collections.Generic;

namespace TicTacToe
{
    enum Player {
        X,
        O
    }

    class TicTacToe
    {
        const int Length = 3;

        private Player?[][] _data;
        private Player? _winner;

        public TicTacToe ()
        {
            _data = Enumerable
                .Range (0, Length)
                .Select (_ => new Player? [Length])
                .ToArray ();
        }

        public IEnumerable<IEnumerable<Player?>> GetRows ()
        {
            return GetIndices ()
                .Select (GetRow);
        }

        public IEnumerable<IEnumerable<Player?>> GetColumns ()
        {
            return GetIndices ()
                .Select (GetColumn);
        }

        public IEnumerable<IEnumerable<Player?>> GetDiagonals ()
        {
            return new [] { true, false }
                .Select (GetDiagonal);
        }

        Player? GetCell (int row, int column)
        {
            return _data [row] [column];
        }

        IEnumerable<Player?> GetRow (int index)
        {
            return _data [index];
        }

        IEnumerable<int> GetIndices ()
        {
            return Enumerable.Range (0, Length);
        }

        IEnumerable<Player?> GetColumn (int index)
        {
            return GetIndices ()
                .Select (GetRow)
                .Select (row => row.ElementAt (index));
        }

        IEnumerable<Player?> GetDiagonal (bool primary)
        {
            return GetIndices ()
                .Select (i => Tuple.Create (i, primary ? i : Length - 1 - i))
                .Select (pos => GetCell (pos.Item1, pos.Item2));
        }

        IEnumerable<IEnumerable<Player?>> GetVectors ()
        {
            return GetDiagonals ()
                .Concat (GetRows ())
                .Concat (GetColumns ());
        }

        static Player? FindWinner (IEnumerable<Player?> vector)
        {
            try {
                return vector
                    .Distinct ()
                    .Single ();
            } catch (InvalidOperationException) {
                return null;
            }
        }

        static Player? FindWinner (IEnumerable<IEnumerable<Player?>> vectors)
        {
            return vectors
                .Select (FindWinner)
                .FirstOrDefault (winner => winner.HasValue);
        }

        public Player? FindWinner ()
        {
            return FindWinner (GetVectors ());
        }

        public bool MakeMove (Player player, int row, int column)
        {
            if (_winner.HasValue)
                throw new InvalidOperationException ("The game is already won.");

            if (_data [row] [column].HasValue)
                throw new InvalidOperationException ("This cell is already taken.");

            _data [row] [column] = player;
            _winner = FindWinner ();

            return _winner.HasValue;
        }

        public bool HasWinner {
            get { return _winner.HasValue; }
        }

        public Player? Winner {
            get { return _winner; }
        }
    }
}