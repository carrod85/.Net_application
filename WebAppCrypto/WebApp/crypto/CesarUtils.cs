namespace WebApp.Crypto;


public static class CesarUtils
{
    public static string CleaningAlpha(string? userAlphabet)
    {
        if (userAlphabet == null)
        {
            userAlphabet = Crypto.Cesar.DefaultAlphabet;
        }
        var charSet = new HashSet<char>();

        foreach (var chr in userAlphabet.ToUpper()) //using uppercase letters only
        {
            if (charSet.Add(chr) == false)
            {
                throw new ArgumentException($"Your alphabet is not unique - duplicate '{chr}' found!");
            }

        }

        return userAlphabet.ToUpper();
    }

    public static int CleaningShift(int shift, string alphabet)
    {
        int result = shift;
        result %= alphabet.Length;
        
        if (result == 0)
        {
            throw new ArgumentException("You are not producing any shift.");
        }
        
        if (result < 0)
        {
            result += alphabet.Length;
        }

        return result;
    }

    public static string CleaningText(string text, string alphabet)
    {
        alphabet = alphabet.ToUpper();
        foreach (var letter in text.ToUpper()) 
        {
            if (alphabet.IndexOf(letter) == -1)
            {
                throw new ArgumentException("Input contains letters not defined in alphabet.");
            }
        }

        return text.ToUpper();
    }
    
}