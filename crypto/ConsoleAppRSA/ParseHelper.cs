namespace ConsoleAppRSA;

public class ParseHelper
{
    public static void CheckParse(string input, ref int val)
        {//controlling than input dont overflow and that number is gretar than 1

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
    
    public static void CheckParseE(string input, ref long val, long phi)
    {//controlling than input dont overflow and that number is gretar than 1

        try
        {
            // getting parsed value
            val = long.Parse(input);
            if (val % 2 == 0 || val <=1 || val >= phi)
                throw new ArgumentException("value must be 1.-Odd to possible be coprime to phi, 2.-Greater than 1 and 3.-Less" +
                                            "than value of phi");
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