using System;

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
        static void Main(string[] args)
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

            Console.ReadKey();
        }
    }
}
