namespace ConsoleAppDH;

public class InputHelper
{
    public static int InputUser()
    {
        bool isValidInput = false;
        int result = 0;
        int modResult = 0;
        string input="";
        while(!isValidInput)
        {
            
            Console.Write("Write number or exit to finished: ");
            input += Console.ReadLine()?.Trim();


            if (input == "exit" )
            {
                Console.WriteLine("Finishing program");
                break;
            }
            else if (string.IsNullOrWhiteSpace(input)) // is null or empty or white space
            {
                Console.WriteLine("incorrect input");

            }
            else
            {
                
                ParseHelper.CheckParse(input, ref result);
                isValidInput = true;
            }
            
        }

        if (input == "exit")
        {
            throw new ArgumentException("Program has finished.");
        }
        
        

        return result;
    }
    
    
    public static int InputUser2(int primeNumber)
    {
        bool isValidInput = false;
        int result = 0;
        int modResult = 0;
        string input="";
        while(!isValidInput)
        {
            
            Console.Write("Write number or exit to finished: ");
            input += Console.ReadLine()?.Trim();


            if (input == "exit" )
            {
                Console.WriteLine("Finishing program");
                break;
            }
            else if (string.IsNullOrWhiteSpace(input)) // is null or empty or white space
            {
                Console.WriteLine("incorrect input");

            }
            else
            {
                
                ParseHelper.CheckParse2(input, ref result, primeNumber);
                isValidInput = true;
            }
            
        }

        if (input == "exit")
        {
            throw new ArgumentException("Program has finished.");
        }
        
        

        return result;
    }
}