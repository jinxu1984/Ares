using PalindromeCheck.DI;
using PalindromeCheck.Interfaces;
using PalindromeCheck.Models;
using PalindromeCheck.Utilities;
using System;

namespace PalindromeCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a sentence");
            string input = Console.ReadLine();

            Action actionToCheckPalindrome = () =>
            {
                Container container = new Container();

                string palindromeStrategyValue = ConfigUtils.GetValueByKey("PalindromeStrategyType");
                PalindromeStrategyType palindromeStrategyType = StringUtils.ToEnum(palindromeStrategyValue, PalindromeStrategyType.Recursive);

                IPalindromeFactory palindromeProcessor = container.GetInstance<IPalindromeFactory>();
                IPalindromeStrategy palindromeStrategy = palindromeProcessor.GetPalindromeStrategy(palindromeStrategyType);
                bool isInputPalindrome = palindromeStrategy.CheckIfInputIsPalindrome(input);

                Console.WriteLine(string.Format("Your input is {0} Palindrome", (isInputPalindrome ? "a" : "not a")));
            };

            Action<Exception> actionToLogException = excpetion => Console.WriteLine(excpetion.Message);

            ExceptionHandler.TryCatch(actionToCheckPalindrome, actionToLogException);
            
            Console.ReadLine();
        }
    }
}
