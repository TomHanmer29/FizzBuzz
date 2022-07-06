using System.Collections;
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
                foreach(int iterator in selectedRules)
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
            }
        }

        private static bool IsMultiple(int inputNo, int multiplier)
        {
            return (inputNo % multiplier == 0);
        }

        private static int CheckIntErr(string numInput)
        {
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
        
        private static int CheckInt(string numInput)
        {
            int numOutput;

            if (int.TryParse(numInput, out numOutput))
            {
                return numOutput;
            }
            return -1;
        }
        private static int EnterNo()
        {
            Console.WriteLine("Please enter the number you would like to fizz buzz up to");
            string numInput = Console.ReadLine();
            return CheckIntErr(numInput);
        }

        private static int[] EnterRules()
        {
            //FIX LATER, currently writes fizz an extra time for every non valid integer input in the list
            Console.WriteLine("Please enter which rules you'd like to use, separated by commas!\n " +
                              "1)Fizz if the number is a multiple of 3\n " +
                              "2)Buzz if the number is a multiple of 5\n " +
                              "3)Bang if the number is a multiple of 7\n " +
                              "4)Bong if the number is a multiple of 11, and all other text is removed\n " +
                              "5)Fezz if the number is a multiple of 13, this is appended in front of the first B or at the end if there are no B's. Occurs after \"Bongs\" have been processed\n " +
                              "6)Reverses the order of all other words if the number is a multiple of 17");
            string numInput = Console.ReadLine();
            int checkedInt;
            while(true)
            {
                if (numInput.Contains(","))
                {
                    string[] splitNumInputsArr = numInput.Split(",");
                    ArrayList splitNumInputs = new ArrayList();
                    foreach (string entry in splitNumInputsArr)
                    {
                        checkedInt = CheckInt(entry);
                        if (checkedInt > 0 && checkedInt < 7)
                        {
                            splitNumInputs.Add(int.Parse(entry)-1);
                        }
                    }

                    return (int[])splitNumInputs.ToArray(typeof(int));
                }
                else
                {
                    checkedInt = CheckInt(numInput);
                    if (checkedInt.GetType() == typeof(int) && checkedInt > 0 && checkedInt < 7)
                    {
                        return new[] { checkedInt - 1 };
                    }
                }

                Console.WriteLine("Invalid input. Please enter a list of integers separated by commas");
                numInput = Console.ReadLine();
            }
            
            return new[] { 0, 1, 2, 3, 4, 5 };
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