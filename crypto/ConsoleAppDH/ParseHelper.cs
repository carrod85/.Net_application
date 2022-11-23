namespace ConsoleAppDH;

public class ParseHelper
{
    public static void CheckParse(string input, ref int val)
    {//controlling than input dont overflow and that number is greater than 1

        try
        {
            // getting parsed value
            val = int.Parse(input);
            if (val <= 2)
                throw new ArgumentException("value must be greater than 2.");
            Console.WriteLine("'{0}' parsed as {1}", input, val);
        }

        catch (FormatException)
        {
            throw new Exception("Format not valid");

        }
        catch (ArgumentNullException e)
        {
            
            throw new Exception("null value");
        }
        catch (OverflowException e)
        {

            throw new Exception("Too big number used. Overflow");
        }
    }
    
    public static void CheckParse2(string input, ref int val, int primeNumber)
    {

        try
        {
            // getting parsed value
            val = int.Parse(input);
            if (val <= 1 || val > primeNumber-1 )
                throw new ArgumentException($"value must be greater than 1 and less than prime number {primeNumber}");
            Console.WriteLine("'{0}' parsed as {1}", input, val);
        }

        catch (FormatException)
        {
            throw new Exception("Format not valid");

        }
        catch (ArgumentNullException e)
        {
            
            throw new Exception("null value");
        }
        catch (OverflowException e)
        {

            throw new Exception("Too big number used. Overflow");
        }
    }
    public static void CheckParse3(string input, ref int val)
    {//controlling than input dont overflow and that number is gretar than 1

        try
        {
            // getting parsed value
            val = int.Parse(input);
            if (val <= 1)
                throw new ArgumentException("value must be greater than 1.");
            Console.WriteLine("'{0}' parsed as {1}", input, val);
        }

        catch (FormatException)
        {
            throw new Exception("Format not valid");

        }
        catch (ArgumentNullException e)
        {
            
            throw new Exception("null value");
        }
        catch (OverflowException e)
        {

            throw new Exception("Too big number used. Overflow");
        }
    }
}
