namespace Crypto;



public static class Cesar 
{
    public static string DefaultAlphabet = "ABCDEFGHIJKLMNOPQRSTUVXYZ";
    public static string Encryption(string alphabet, int shift, string text)
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
