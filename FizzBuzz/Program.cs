using System.Text;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int currentNo = 1; currentNo <= 100; currentNo++)
            {
                StringBuilder fizzBuzzOutput = new StringBuilder();
                if (IsMultiple(currentNo, 3))
                {
                    fizzBuzzOutput.Append("Fizz");
                }

                if (IsMultiple(currentNo, 5))
                {
                    fizzBuzzOutput.Append("Buzz");
                }

                if (fizzBuzzOutput.Equals(""))
                {
                    Console.WriteLine(currentNo);
                }
                else
                {
                    Console.WriteLine(fizzBuzzOutput);
                }
            }
        }

        private static bool IsMultiple(int inputNo, int multiplier)
        {
            return (inputNo % multiplier == 0);
        }
    }
}