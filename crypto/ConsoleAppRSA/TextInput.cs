namespace ConsoleAppRSA;

public class TextInput
{
    public static byte[] InputText(long n)
    {
        bool isValidInput = false;
        string? userInput = null;
        while(!isValidInput)
        {
            userInput = new string("");
            Console.Write("Type Plain text to encrypt or 'exit' to finish: ");
            userInput += Console.ReadLine();

            
            if (!string.IsNullOrWhiteSpace(userInput)) // is null or empty or white space
            {
                var codedBytes = System.Text.Encoding.UTF8.GetBytes(userInput);
                var numberString = Calculations.BytesToNumber(codedBytes);
                if (numberString > n)
                {
                    Console.WriteLine("The text to encrypt is too long for key, please insert something shorter.");
                    continue;
                    
                }
                isValidInput = true;
                
            }


        }

        if (userInput == "exit")
        {
            Console.WriteLine("Finishing program");
            throw new ArgumentException("Program has finished.");
        }
        

        return System.Text.Encoding.UTF8.GetBytes(userInput);
    }
}