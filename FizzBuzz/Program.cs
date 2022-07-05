using System.Text;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxFizzBuzz = EnterNo();
            int[] fizzBuzzCheckers = { 3, 5, 7, 11, 13, 17 };
            string[] fizzBuzzWords = { "Fizz", "Buzz", "Bang", "Bong", "Fezz", "placeholder"};
            for (int currentNo = 1; currentNo <= maxFizzBuzz; currentNo++)
            {
                StringBuilder fizzBuzzOutput = new StringBuilder();
                for(int iterator = 0; iterator<fizzBuzzCheckers.Length; iterator++ )
                {
                    fizzBuzzOutput = FizzBuzzAppend(fizzBuzzOutput, fizzBuzzWords[iterator], currentNo,
                        fizzBuzzCheckers[iterator]);
                }

                if (fizzBuzzOutput.Equals(""))
                {
                    Console.WriteLine(currentNo);
                }
                else
                {
                    Console.WriteLine(fizzBuzzOutput);
                }
                //break;
            }
        }

        private static bool IsMultiple(int inputNo, int multiplier)
        {
            return (inputNo % multiplier == 0);
        }

        private static int EnterNo()
        {
            Console.WriteLine("Please enter the number you would like to fizz buzz up to");
            string numInput = Console.ReadLine();
            int numOutput;
            while (true)
            {
                if (int.TryParse(numInput, out numOutput))
                {
                    return numOutput;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter an integer");
                    numInput = Console.ReadLine();
                }
            }
        }
        
        private static StringBuilder FizzBuzzAppend(StringBuilder fizzBuzzCurrent, string newWord, int currentNo ,int multiplier)
        {
            if (IsMultiple(currentNo, multiplier))
            {
                switch (multiplier)
                {
                    default:
                        fizzBuzzCurrent.Append(newWord);
                        break;
                    case 11:
                        fizzBuzzCurrent = new StringBuilder();
                        fizzBuzzCurrent.Append(newWord);
                        break; 
                    case 13:
                        fizzBuzzCurrent.Insert(BIndex(fizzBuzzCurrent), newWord);
                        break;
                    case 17:
                        //reverse order of fizzes, buzzes etc.
                        fizzBuzzCurrent = CapitalWordReverser(fizzBuzzCurrent);
                        break;
                }
            }
            return fizzBuzzCurrent;
        }

        private static int BIndex(StringBuilder fizzBuzzCurrent)
        //This finds the index of the first capital B in the input StringBuilder
        {
            if (Array.IndexOf(fizzBuzzCurrent.ToString().ToCharArray(),'B') != -1)
            {
                return Array.IndexOf(fizzBuzzCurrent.ToString().ToCharArray(), 'B');
            }
            return fizzBuzzCurrent.Length;
        }

        private static StringBuilder CapitalWordReverser(StringBuilder fizzBuzzCurrent)
        {
            StringBuilder reversedString = new StringBuilder();
            return reversedString;
        }
    }
}