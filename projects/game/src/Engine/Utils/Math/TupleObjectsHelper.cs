using System;

namespace Utils
{
    public static class TupleObjectsHelper
    {
        public readonly struct IntPair
        {
            public int Item1 { get; }
            public int Item2 { get; }

            public IntPair(int item1, int item2)
            {
                Item1 = item1;
                Item2 = item2;
            }

            public override bool Equals(object obj) =>
                obj is IntPair other &&
                Item1 == other.Item1 &&
                Item2 == other.Item2;

            public override int GetHashCode() => HashCode.Combine(Item1, Item2);

            public static bool operator ==(IntPair left, IntPair right) => left.Equals(right);
            public static bool operator !=(IntPair left, IntPair right) => !left.Equals(right);

            public static IntPair MinusOne;

            static IntPair()
            {
                MinusOne = new IntPair(-1, -1);
            }

            public override string ToString() => $"({Item1}, {Item2})";
        }
    }
}