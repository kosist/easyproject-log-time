using System;

namespace Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputText = Console.ReadLine();
            var inputNumber = 0;
            // try to parse - if success, then parsingFlag = True
            // and parsed number is stored to inputNumber
            var parsingFlag = Int32.TryParse(inputText, out inputNumber);
            if ((inputNumber >= 1000) && (inputNumber <= 9999))
            {
                // do your algorithm
            }
            else
                Console.WriteLine("Invalid entry");

        }
    }
}
