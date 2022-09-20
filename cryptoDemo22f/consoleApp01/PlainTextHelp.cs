namespace ConsoleApp01;

public class PlainTextHelp
{
    public static string GetPlainText(string alpha)
    {
        bool isValidInput = false;
        bool validLetters = true;
        string? userInput =null;
        
        while(!isValidInput)
        {
            Console.Write("Plain text: ");
            userInput = Console.ReadLine();


            if (userInput == "exit" )
            {
                Console.WriteLine("Finishing program");
                break;
            }
            else if (userInput == null) // is null
            {
                Console.WriteLine("incorrect input. Please try again: ");
                userInput = Console.ReadLine();

            }
            // compare the plain text letters with the ones of the alphabet to see if they exist
            else
            {
                foreach (var letter in userInput)
                {
                    if (alpha.IndexOf(letter) == -1)
                    {
                        Console.WriteLine("input contains letters not defined in alphabet");
                        validLetters = false;
                        break; // input not valid
                    }
                }

                if (validLetters)
                {
                    isValidInput = true;
                }

            }

        }

        if (userInput != null) return userInput;
        return "what";
    }

}