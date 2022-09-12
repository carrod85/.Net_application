namespace ConsoleApp01;

public static class Helper
{
    public static string GetUserAlphabet(string defaultAlphabet)
    {
        var isValidInput = true;
        string? userAlphabet;
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

            foreach (var chr in userAlphabet)
            {
                if (charSet.Add(chr) == false)
                {
                    isValidInput = false;
                    Console.WriteLine($"Your alphabet is not unique - duplicate '{chr}' found!");
                    break;
                }
            }
        } while (isValidInput == false);

        return userAlphabet;
    }
}