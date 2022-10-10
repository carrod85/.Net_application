namespace ConsoleAppRSA;

public class InputHelper
{
    public static int Inputp()
    {
        bool isValidInput = false;
        int result = 0;
        int modResult = 0;
        string input="";
        while(!isValidInput)
        {
            
            Console.Write("Write number for p variable or 'exit' to finish: ");
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
    
    
    public static int Inputq()
    {
        bool isValidInput = false;
        int result = 0;
        int modResult = 0;
        string input="";
        while(!isValidInput)
        {
            
            Console.Write("Write number for q variable or 'exit' to finish: ");
            input += Console.ReadLine()?.Trim();


            if (input == "exit" )
            {
                Console.WriteLine("Finishing program");
                break;
            }
            if (string.IsNullOrWhiteSpace(input)) // is null or empty or white space
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
    
    public static long Inpute(long phi)
    {
        bool isValidInput = false;
        long result = 0;
        string input="";
        while(!isValidInput)
        {
            
            Console.Write("Write number for e variable or 'exit' to finish: ");
            input += Console.ReadLine()?.Trim();


            if (input == "exit" )
            {
                Console.WriteLine("Finishing program");
                break;
            }
            if (string.IsNullOrWhiteSpace(input)) // is null or empty or white space
            {
                Console.WriteLine("incorrect input");

            }
            else
            {
                
                ParseHelper.CheckParseE(input, ref result, phi);
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