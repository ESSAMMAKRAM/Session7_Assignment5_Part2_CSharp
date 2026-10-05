using System;

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
        static void Main(string[] args)
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

            Console.ReadKey();
        }
    }
}
