using System;

namespace Assignment04_FirstProject
{
    // 1. Point3D with chained constructors, 5. IComparable, 6. ICloneable
    public class Point3D : IComparable, ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        // Constructor chaining: every constructor calls the full one via this(...)
        public Point3D() : this(0, 0, 0) { }
        public Point3D(int x) : this(x, 0, 0) { }
        public Point3D(int x, int y) : this(x, y, 0) { }
        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        // 2. ToString
        public override string ToString() => $"Point Coordinates: ({X}, {Y}, {Z})";

        // 4. Make == work on values (by default == compares references for classes)
        public override bool Equals(object obj)
        {
            Point3D other = obj as Point3D;
            if (ReferenceEquals(other, null)) return false;
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + X;
                hash = hash * 31 + Y;
                hash = hash * 31 + Z;
                return hash;
            }
        }

        public static bool operator ==(Point3D a, Point3D b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        public static bool operator !=(Point3D a, Point3D b) => !(a == b);

        // 5. Sort by X then Y
        public int CompareTo(object obj)
        {
            Point3D other = obj as Point3D;
            if (ReferenceEquals(other, null)) return 1;

            int result = X.CompareTo(other.X);
            if (result != 0) return result;
            return Y.CompareTo(other.Y);
        }

        // 6. Clone
        public object Clone() => MemberwiseClone();
    }

    internal class Program
    {
        // 3. Reading input - three different ways of validating

        // Way 1: int.TryParse (no exception, returns bool)
        static int ReadWithTryParse(string name)
        {
            int value;
            Console.Write($"Enter {name}: ");
            while (!int.TryParse(Console.ReadLine(), out value))
                Console.Write($"Invalid number. Enter {name} again: ");
            return value;
        }

        // Way 2: int.Parse (throws exception on bad input)
        static int ReadWithParse(string name)
        {
            while (true)
            {
                Console.Write($"Enter {name}: ");
                try
                {
                    return int.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Invalid input ({ex.GetType().Name}). Try again.");
                }
            }
        }

        // Way 3: Convert.ToInt32 (throws exception on bad input)
        static int ReadWithConvert(string name)
        {
            while (true)
            {
                Console.Write($"Enter {name}: ");
                try
                {
                    return Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Invalid input ({ex.GetType().Name}). Try again.");
                }
            }
        }

        static void Main(string[] args)
        {
            // 2. ToString
            Console.WriteLine("=== 2. ToString ===");
            Point3D P = new Point3D(10, 10, 10);
            Console.WriteLine(P.ToString());

            // 3. Read two points from the user
            Console.WriteLine("\n=== 3. Read P1 (TryParse) and P2 (Parse / Convert / TryParse) ===");
            Point3D P1 = new Point3D(ReadWithTryParse("P1.X"), ReadWithTryParse("P1.Y"), ReadWithTryParse("P1.Z"));
            Point3D P2 = new Point3D(ReadWithParse("P2.X"), ReadWithConvert("P2.Y"), ReadWithTryParse("P2.Z"));
            Console.WriteLine(P1);
            Console.WriteLine(P2);

            // 4. Does == work properly?
            Console.WriteLine("\n=== 4. Equality ===");
            // Without overloading, == on classes compares REFERENCES, so two different objects
            // with identical coordinates give false. Casting to object forces that default behaviour:
            Console.WriteLine($"Default (reference) comparison: {(object)P1 == (object)P2}");
            // After overloading == (and Equals/GetHashCode), it compares the coordinates:
            Console.WriteLine($"P1 == P2 (overloaded):          {P1 == P2}");

            // 5. Sort an array of points by X then Y
            Console.WriteLine("\n=== 5. Sorting ===");
            Point3D[] points =
            {
                new Point3D(5, 2, 1),
                new Point3D(1, 9, 4),
                new Point3D(5, 1, 7),
                new Point3D(3, 3, 3),
                new Point3D(1, 2, 8)
            };
            Array.Sort(points);
            foreach (Point3D p in points)
                Console.WriteLine(p);

            // 6. Clone
            Console.WriteLine("\n=== 6. Clone ===");
            Point3D copy = (Point3D)P1.Clone();
            Console.WriteLine($"Original: {P1}");
            Console.WriteLine($"Clone:    {copy}");
            copy.X = 999;
            Console.WriteLine("After changing the clone's X to 999:");
            Console.WriteLine($"Original: {P1}");
            Console.WriteLine($"Clone:    {copy}");

            Console.ReadKey();
        }
    }
}
