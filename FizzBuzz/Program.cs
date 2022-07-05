using System.Text;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxFizzBuzz = EnterNo();
            int[] selectedRules = EnterRules();
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
        
        private static int[] EnterRules()
        {
            return new[] {0};
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
                        fizzBuzzCurrent.Insert(BIndex(fizzBuzzCurrent, 'B'), newWord);
                        break;
                    case 17:
                        //reverse order of fizzes, buzzes etc.
                        fizzBuzzCurrent = CapitalWordReverser(fizzBuzzCurrent);
                        break;
                }
            }
            return fizzBuzzCurrent;
        }

        private static int BIndex(StringBuilder fizzBuzzCurrent, char inputLetter)
        //This finds the index of the first specified capital letter in the input StringBuilder
        {
            if (Array.IndexOf(fizzBuzzCurrent.ToString().ToCharArray(),inputLetter) != -1)
            {
                return Array.IndexOf(fizzBuzzCurrent.ToString().ToCharArray(), inputLetter);
            }
            return fizzBuzzCurrent.Length;
        }
        
        private static int CapIndex(StringBuilder fizzBuzzCurrent)
        //finds the index of the last capital letter
        {
            int index = 0;
            char[] splitFizzBuzz = fizzBuzzCurrent.ToString().ToCharArray();
            Array.Reverse(splitFizzBuzz);
            foreach (char character in splitFizzBuzz)
            {
                if (char.ToUpper(character) == character)
                {
                    index = Array.IndexOf(splitFizzBuzz, character);
                    break;
                }
            }
            return fizzBuzzCurrent.Length-index-1;
        }

        private static StringBuilder CapitalWordReverser(StringBuilder fizzBuzzCurrent)
        {
            StringBuilder reversedString = new StringBuilder();
            int lastCapIndex;
            while (fizzBuzzCurrent.Length > 0)
            {
                lastCapIndex = CapIndex(fizzBuzzCurrent);
                reversedString.Append(fizzBuzzCurrent.ToString(lastCapIndex, fizzBuzzCurrent.Length - lastCapIndex));
                fizzBuzzCurrent.Remove(lastCapIndex, fizzBuzzCurrent.Length - lastCapIndex);
            }
            return reversedString;

        }
        
    }
}