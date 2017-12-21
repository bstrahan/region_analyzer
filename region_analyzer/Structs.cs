using System;
using System.Collections;
using System.Collections.Generic;

namespace region_analyzer
{

    public class InterestingRegion : ICollection<Coordinate>, IEnumerable<Coordinate>
    {
        public List<Coordinate> ComponentPoints { get; set; }
        public Coordinate CenterOfMass { get; set; }

        public float[,] Region { get; set; }

        public int Count => ((ICollection<Coordinate>) ComponentPoints).Count;

        public bool IsReadOnly => ((ICollection<Coordinate>) ComponentPoints).IsReadOnly;

        public ICoordinateCalculator Calculator { get; private set; }

        public InterestingRegion() : this ( new float[0,0], new CenterOfMassCoordinateCalculator()){}

        public InterestingRegion(float[,] region, ICoordinateCalculator calculator){
            Region = region;
            Calculator = calculator;
            ComponentPoints = new List<Coordinate>();
        }

        public InterestingRegion(InterestingRegion other)
        {
            Region = other.Region;
            Calculator = other.Calculator;
            this.ComponentPoints = new List<Coordinate>();
        }

        public void Add(Coordinate item)
        {
            ((ICollection<Coordinate>) ComponentPoints).Add(item);
        }

        public void Clear()
        {
            ((ICollection<Coordinate>) ComponentPoints).Clear();
        }

        public bool Contains(Coordinate item)
        {
            return ((ICollection<Coordinate>) ComponentPoints).Contains(item);
        }

        public void CopyTo(Coordinate[] array, int arrayIndex)
        {
            ((ICollection<Coordinate>) ComponentPoints).CopyTo(array, arrayIndex);
        }

        public bool Remove(Coordinate item)
        {
            return ((ICollection<Coordinate>) ComponentPoints).Remove(item);
        }

        public IEnumerator<Coordinate> GetEnumerator()
        {
            return ((ICollection<Coordinate>) ComponentPoints).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((ICollection<Coordinate>) ComponentPoints).GetEnumerator();
        }
    }

    public class Coordinate : Tuple<int, int, float> , IEqualityComparer
    {
        public int X => Item1;
        public int Y => Item2;
        public float Z => Item3; //this is the 'magnitude' but really it's just another dimension

        public Coordinate(int item1, int item2, float item3) : base(item1, item2, item3)
        {
        }

        public bool IsAdjacentTo(Coordinate other)
        {
            var x_d = Math.Abs(other.X - X); // 'horizonal'
            var y_d = Math.Abs(other.Y - Y); // 'vertical'

            if (x_d == 0 && y_d == 0)
                return false;

            return (x_d <= 1 && y_d <= 1);
        }

        public new bool Equals(object x, object y)
        {
            if ( x == null || y == null) {
                return false;
            }

            var xAsCoord = x as Coordinate;
            var yAsCoord = y as Coordinate;
            if (xAsCoord == null || yAsCoord == null) {
                return false;
            }

            return (xAsCoord.X == yAsCoord.X)
                && (xAsCoord.Y == yAsCoord.Y)
                && (xAsCoord.Z == yAsCoord.Z);
        }

        public int GetHashCode(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
