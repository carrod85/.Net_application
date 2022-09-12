namespace ConsoleApp01;

public static class ShiftHelp
{
    public static int GetUserShift(int lenghtAlpha)
    {
        bool isValidInput = false;
        int result = 0;
        
        while(!isValidInput)
        {
            Console.Write("Cesar shift amount or type 'exit' to finish: ");
            string? userInput = Console.ReadLine();


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
                if (result >= lenghtAlpha || result <= 0)
                {
                    Console.WriteLine($"number greater than 0 and less than {lenghtAlpha} alphabet needed!!");
                    continue;

                }

                isValidInput = true;

            }


        }

        return result;
    }
}