namespace ConsoleAppCesarUtf;

public class HelperInput
{
    public static string InputText()
    {
        bool isValidInput = false;
        string userInput="";
        while(!isValidInput)
        {
            
            Console.Write("Type Plain text to encrypt or 'exit' to finish: ");
            userInput += Console.ReadLine();

            
            if (!string.IsNullOrWhiteSpace(userInput)) // is null or empty or white space
            {
                isValidInput = true;

            }


        }

        if (userInput == "exit")
        {
            Console.WriteLine("Finishing program");
            throw new ArgumentException("Program has finished.");
        }
        

        return userInput;
    }
    public static string InputText2()
    {
        bool isValidInput = false;
        string userInput="";
        while(!isValidInput)
        {
            
            Console.Write("Type passhrase text or 'exit' to finish: ");
            userInput += Console.ReadLine();

            
            if (!string.IsNullOrWhiteSpace(userInput)) // is null or empty or white space
            {
                isValidInput = true;

            }


        }

        if (userInput == "exit")
        {
            Console.WriteLine("Finishing program");
            throw new ArgumentException("Program has finished.");
        }
        

        return userInput;
    }

}