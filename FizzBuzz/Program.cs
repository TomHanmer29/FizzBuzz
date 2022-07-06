using System.Collections;
using System.Text;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            FizzBuzzPrint(EnterNo(),EnterRules());
        }

        private static void FizzBuzzPrint(int maxFizzBuzz, int[] selectedRules)
        {
            UserRule rule;
            ArrayList customUserRules = new ArrayList();
            foreach (int entry in selectedRules)
            {
                if (entry == 6)
                {
                    customUserRules.Add(CustomRulePrompt());
                }
            }
            for (int currentNo = 1; currentNo <= maxFizzBuzz; currentNo++)
            {
                StringBuilder fizzBuzzOutput = new StringBuilder();
                foreach(int iterator in selectedRules)
                {
                    if (iterator == 6)
                    {
                        //for the i'th 6 pass in the ith entry of customUserRules
                        //this is just here ATM so the code doesnt break :)
                        rule = new UserRule(iterator);
                    }
                    else
                    {
                        rule = new UserRule(iterator);
                    }
                    fizzBuzzOutput = FizzBuzzAppend(fizzBuzzOutput, currentNo, rule);
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
            Console.WriteLine("Please enter which rules you'd like to use, separated by commas!\n " +
                              "1)Fizz if the number is a multiple of 3\n " +
                              "2)Buzz if the number is a multiple of 5\n " +
                              "3)Bang if the number is a multiple of 7\n " +
                              "4)Bong if the number is a multiple of 11, and all other text is removed\n " +
                              "5)Fezz if the number is a multiple of 13, this is appended in front of the first B or at the end if there are no B's. Occurs after \"Bongs\" have been processed\n " +
                              "6)Reverses the order of all other words if the number is a multiple of 17\n " +
                              "7)Custom user rule");
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
                        if (checkedInt > 0 && checkedInt < 8)
                        {
                            splitNumInputs.Add(int.Parse(entry)-1);
                        }
                    }

                    return (int[])splitNumInputs.ToArray(typeof(int));
                }
                else
                {
                    checkedInt = CheckInt(numInput);
                    if (checkedInt.GetType() == typeof(int) && checkedInt > 0 && checkedInt < 8)
                    {
                        return new[] { checkedInt - 1 };
                    }
                }

                Console.WriteLine("Invalid input. Please enter a list of integers separated by commas");
                numInput = Console.ReadLine();
            }
            
            return new[] { 0, 1, 2, 3, 4, 5 };
        }
        
        private static StringBuilder FizzBuzzAppend(StringBuilder fizzBuzzCurrent, int currentNo ,UserRule rule)
        {
            if (IsMultiple(currentNo, rule.Replacer))
            {
                switch (rule.Replacer)
                {
                    default:
                        fizzBuzzCurrent.Append(rule.Text);
                        break;
                    case 11:
                        fizzBuzzCurrent = new StringBuilder();
                        fizzBuzzCurrent.Append(rule.Text);
                        break; 
                    case 13:
                        fizzBuzzCurrent.Insert(BIndex(fizzBuzzCurrent, 'B'), rule.Text);
                        break;
                    case 17:
                        //reverse order of fizzes, buzzes etc.
                        fizzBuzzCurrent = CapitalWordReverser(fizzBuzzCurrent);
                        break;
                    case 0:
                        //custom user rule:
                        fizzBuzzCurrent = CustomRule(fizzBuzzCurrent, rule);
                        break;
                }
            }
            return fizzBuzzCurrent;
        }

        private static UserRule CustomRulePrompt()
        {
            //number to replace? OR other function?
            Console.WriteLine("Which number's multiples would you like to replace?");
            int replacer = CheckIntErr(Console.ReadLine());
            //word to replace it with?
            Console.WriteLine("Which word would you like to replace them with?");
            string text = Console.ReadLine();
            //where to insert (after/before/between current words)?
            Console.WriteLine("Would you like to insert the new words\n1)after\n2)before\n3)between\ncurrent text?");
            int position = -1;
            while (true)
            {
                position = CheckIntErr(Console.ReadLine());
                if (position == 3)
                {
                    //if between, type the first letter of the current word you would like to insert between.
                    //MAKE ONLY ACCEPT SINGLE CHARACTERS
                    Console.WriteLine("Type the first letter of the current word (e.g. \"F\" for Fizz, etc.) you would like to insert the new word before");
                    char userChar = char.Parse(Console.ReadLine());
                    return new UserRule(replacer, text, position, userChar);
                }
                else if (position == 1 || position ==2)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a number between 1 and 3");
                }
            }
            
            
            return new UserRule(replacer, text, position);
        }

        private static StringBuilder CustomRule(StringBuilder fizzBuzzCurrent,  UserRule rule)
        {
            return new StringBuilder();
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
    class UserRule
    {
        int[] fizzBuzzCheckers = { 3, 5, 7, 11, 13, 17, 0};
        string[] fizzBuzzWords = { "Fizz", "Buzz", "Bang", "Bong", "Fezz", "reverse", "custom"};
        public int Replacer { get; set; }
        public string Text { get; set; }
        public int Position { get; set; }
        public char Letter { get; set; }

        public UserRule(int currentRule)
        {
            Text = fizzBuzzWords[currentRule];
            Replacer = fizzBuzzCheckers[currentRule];
            
        }

        public UserRule(int replacer, string text, int position)
        {
            Replacer = replacer;
            Text = text;
            Position = position;
        }
        public UserRule(int replacer, string text, int position, char letter)
        {
            Replacer = replacer;
            Text = text;
            Position = position;
            Letter = letter;
        }
    }
}