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

        public static void Run()
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

        }
    }
}

namespace Assignment04_SecondProject
{
    // ===== Version 1: instance methods (need an object to call them) =====
    public class MathsInstance
    {
        public double Add(double a, double b) => a + b;
        public double Subtract(double a, double b) => a - b;
        public double Multiply(double a, double b) => a * b;
        public double Divide(double a, double b)
        {
            if (b == 0) throw new DivideByZeroException("Cannot divide by zero.");
            return a / b;
        }
    }

    // ===== Version 2 (the modification): static class =====
    // Static members belong to the class itself, so no instance is needed:
    // Maths.Add(5, 3). A static class can't be instantiated at all.
    public static class Maths
    {
        public static double Add(double a, double b) => a + b;
        public static double Subtract(double a, double b) => a - b;
        public static double Multiply(double a, double b) => a * b;
        public static double Divide(double a, double b)
        {
            if (b == 0) throw new DivideByZeroException("Cannot divide by zero.");
            return a / b;
        }
    }

    internal class Program
    {
        public static void Run()
        {
            // Version 1: must create an instance
            Console.WriteLine("=== Instance version ===");
            MathsInstance m = new MathsInstance();
            Console.WriteLine($"Add:      {m.Add(10, 5)}");
            Console.WriteLine($"Subtract: {m.Subtract(10, 5)}");
            Console.WriteLine($"Multiply: {m.Multiply(10, 5)}");
            Console.WriteLine($"Divide:   {m.Divide(10, 5)}");

            // Version 2: call directly through the class name, no instance
            Console.WriteLine("\n=== Static version ===");
            Console.WriteLine($"Add:      {Maths.Add(10, 5)}");
            Console.WriteLine($"Subtract: {Maths.Subtract(10, 5)}");
            Console.WriteLine($"Multiply: {Maths.Multiply(10, 5)}");
            Console.WriteLine($"Divide:   {Maths.Divide(10, 5)}");

            try
            {
                Console.WriteLine(Maths.Divide(10, 0));
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
    }
}

namespace Assignment04_ThirdProject
{
    public class Duration
    {
        public int Hours { get; private set; }
        public int Minutes { get; private set; }
        public int Seconds { get; private set; }

        public int TotalSeconds => Hours * 3600 + Minutes * 60 + Seconds;

        // ===== Constructors (chained) =====
        public Duration() : this(0) { }

        public Duration(int hours, int minutes, int seconds)
            : this(hours * 3600 + minutes * 60 + seconds) { }

        public Duration(int totalSeconds)
        {
            if (totalSeconds < 0)
                throw new ArgumentOutOfRangeException(nameof(totalSeconds), "Duration can't be negative.");

            Hours = totalSeconds / 3600;
            Minutes = (totalSeconds % 3600) / 60;
            Seconds = totalSeconds % 60;
        }

        // ===== System.Object overrides =====
        public override string ToString()
        {
            if (Hours > 0)
                return $"Hours: {Hours}, Minutes :{Minutes}, Seconds :{Seconds}";

            return $"Minutes :{Minutes}, Seconds :{Seconds}";
        }

        public override bool Equals(object obj)
        {
            Duration other = obj as Duration;
            return other != null && TotalSeconds == other.TotalSeconds;
        }

        public override int GetHashCode() => TotalSeconds.GetHashCode();

        // ===== Operators =====
        // D3 = D1 + D2
        public static Duration operator +(Duration a, Duration b)
            => new Duration(a.TotalSeconds + b.TotalSeconds);

        // D3 = D1 + 7800
        public static Duration operator +(Duration a, int seconds)
            => new Duration(Math.Max(0, a.TotalSeconds + seconds));

        // D3 = 666 + D3
        public static Duration operator +(int seconds, Duration a) => a + seconds;

        // D1 = D1 - D2  (result is clamped at zero, a duration can't be negative)
        public static Duration operator -(Duration a, Duration b)
            => new Duration(Math.Max(0, a.TotalSeconds - b.TotalSeconds));

        // ++D1 : add one minute
        public static Duration operator ++(Duration d)
            => new Duration(d.TotalSeconds + 60);

        // --D2 : subtract one minute (not below zero)
        public static Duration operator --(Duration d)
            => new Duration(Math.Max(0, d.TotalSeconds - 60));

        // Comparison operators (must be defined in pairs)
        public static bool operator >(Duration a, Duration b) => a.TotalSeconds > b.TotalSeconds;
        public static bool operator <(Duration a, Duration b) => a.TotalSeconds < b.TotalSeconds;
        public static bool operator >=(Duration a, Duration b) => a.TotalSeconds >= b.TotalSeconds;
        public static bool operator <=(Duration a, Duration b) => a.TotalSeconds <= b.TotalSeconds;

        // if (D1) : true when the duration is not zero (true/false must be defined together)
        public static bool operator true(Duration d) => d.TotalSeconds > 0;
        public static bool operator false(Duration d) => d.TotalSeconds == 0;

        // DateTime obj = (DateTime)D1  -> today at 00:00 plus the duration
        public static explicit operator DateTime(Duration d)
            => DateTime.Today.AddSeconds(d.TotalSeconds);
    }

    internal class Program
    {
        public static void Run()
        {
            // ===== Constructors / ToString =====
            Console.WriteLine("=== Constructors ===");
            Duration D1 = new Duration(1, 10, 15);
            Console.WriteLine(D1.ToString());   // Hours: 1, Minutes :10, Seconds :15

            D1 = new Duration(3600);
            Console.WriteLine(D1.ToString());   // Hours: 1, Minutes :0, Seconds :0

            Duration D2 = new Duration(7800);
            Console.WriteLine(D2.ToString());   // Hours: 2, Minutes :10, Seconds :0

            Duration D3 = new Duration(666);
            Console.WriteLine(D3.ToString());   // Minutes :11, Seconds :6

            // ===== Operators =====
            Console.WriteLine("\n=== Operators ===");
            D3 = D1 + D2;
            Console.WriteLine($"D3 = D1 + D2   -> {D3}");

            D3 = D1 + 7800;
            Console.WriteLine($"D3 = D1 + 7800 -> {D3}");

            D3 = 666 + D3;
            Console.WriteLine($"D3 = 666 + D3  -> {D3}");

            D3 = ++D1;
            Console.WriteLine($"D3 = ++D1      -> {D3}   (D1 is now {D1})");

            D3 = --D2;
            Console.WriteLine($"D3 = --D2      -> {D3}   (D2 is now {D2})");

            // new values so the subtraction and comparisons are meaningful
            D1 = new Duration(2, 30, 0);
            D2 = new Duration(1, 10, 0);

            D1 = D1 - D2;
            Console.WriteLine($"D1 = D1 - D2   -> {D1}");

            if (D1 > D2)
                Console.WriteLine("D1 > D2 is true");

            if (D1 <= D2)
                Console.WriteLine("D1 <= D2 is true");
            else
                Console.WriteLine("D1 <= D2 is false");

            if (D1)
                Console.WriteLine("if (D1): D1 is not zero");

            DateTime obj = (DateTime)D1;
            Console.WriteLine($"(DateTime)D1   -> {obj}");

        }
    }
}

// ================= Launcher: the only entry point =================
namespace Assignment04
{
    internal class Launcher
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n=== Assignment 04 ===");
                Console.WriteLine("1. First Project  (Point3D)");
                Console.WriteLine("2. Second Project (Maths)");
                Console.WriteLine("3. Third Project  (Duration)");
                Console.WriteLine("0. Exit");
                Console.Write("Choose: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": Assignment04_FirstProject.Program.Run(); break;
                    case "2": Assignment04_SecondProject.Program.Run(); break;
                    case "3": Assignment04_ThirdProject.Program.Run(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }
    }
}
