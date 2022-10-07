namespace ConsoleApp01;

public static class Helper
{
    public static string GetUserAlphabet(string defaultAlphabet)
    {
        var isValidInput = true;
        string? userAlphabet;
        string? lower;
        do
        {
            isValidInput = true;
            Console.Write($"Input your alphabet [{defaultAlphabet}]:");
            userAlphabet = Console.ReadLine();
            
            

            if (string.IsNullOrWhiteSpace(userAlphabet) || userAlphabet.Length == 1)// is null or empty or white space
            {
                userAlphabet = defaultAlphabet;
            }

            var charSet = new HashSet<char>();

            foreach (var chr in userAlphabet.ToUpper())//using lowercase letters only
            {
                if (charSet.Add(chr) == false)
                {
                    isValidInput = false;
                    Console.WriteLine($"Your alphabet is not unique - duplicate '{chr}' found!");
                    break;
                }
            }
        } while (isValidInput == false);

        return userAlphabet.ToUpper();
    }
    
    public static int GetUserShift(int lenghtAlpha)
    {
        bool isValidInput = false;
        int result = 0;
        int modResult = 0;
        string userInput="";
        while(!isValidInput)
        {
            result = 0;
            Console.Write("Cesar shift amount or type 'exit' to finish: ");
            userInput += Console.ReadLine();


            if (userInput == "exit" )
            {
                Console.WriteLine("Finishing program");
                break;
            }
            else if (string.IsNullOrWhiteSpace(userInput)) // is null or empty or white space
            {
                Console.WriteLine("incorrect input");

            }
            else if (int.TryParse(userInput, out result))
            {
                modResult = result % lenghtAlpha;
                if (modResult == 0)
                {
                    Console.WriteLine($"number different than 0  needed!!");
                    continue;
                }
                
                if (modResult < 0)
                {
                    modResult += lenghtAlpha;
                }
                

                isValidInput = true;

            }


        }

        if (userInput == "exit")
        {
            throw new ArgumentException("Program has finished.");
        }
        
        if (result == 0)
            throw new ArgumentException("value must be different than 0");

        return modResult;
    }
    
    public static string GetPlainText(string alpha)
    {
        bool isValidInput = false;
        
        string? userInput =null;
        
        while(!isValidInput)
        {
            bool validLetters = true;
            Console.Write("Plain text or type 'exit' to finish: ");
            userInput = Console.ReadLine();


            if (string.Equals(userInput, "exit", StringComparison.OrdinalIgnoreCase) )
            {
                Console.WriteLine("Finishing program");
                break;
            }
            else if (string.IsNullOrEmpty(userInput)) // is null or empty
            {
                Console.WriteLine("Empty input. Please try again: ");
                

            }
            // compare the plain text letters with the ones of the alphabet to see if they exist
            else
            {
                foreach (var letter in userInput.ToUpper())
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

        return userInput != "exit" ? userInput!.ToUpper() : throw new ArgumentException("program finished");
    }


    public static string GetEncryption(string alphabet, int shift, string text)
    {
        string aux ="";
        

        foreach (var letter in text)
        {
            var position = alphabet.IndexOf(letter);
            var newPosition = position + shift;
            var modPosition = newPosition % alphabet.Length;
            if (modPosition < 0)
            {
                modPosition += alphabet.Length;
            }
            aux += alphabet[modPosition];

        }

        return aux;

    }
    
}