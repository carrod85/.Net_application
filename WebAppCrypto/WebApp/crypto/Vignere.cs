// See https://aka.ms/new-console-template for more information


using System.Text.RegularExpressions;

namespace WebApp.Crypto;

public static class Vigenere
{
    public static bool IsValidBase64(string? base64Text)
    {
        string strRegex = @"(^([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)?$)";
        
        Regex re = new Regex(strRegex);
         
        // The IsMatch method is used to validate
        // a string or to ensure that a string
        // conforms to a particular pattern.
        if (re.IsMatch(base64Text))
            return true;
        
        return false;
    }
    
    public static string Encryption(string message, string key)
    {
            
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(message);
        var passBytes = System.Text.Encoding.UTF8.GetBytes(key);
        byte[] encryptedBytes = new Byte[plainTextBytes.Length];


        for (int k=0,i=0; i < plainTextBytes.Length; i++,k++)
        {
            var a = (int)plainTextBytes[i];
            var b = (int)passBytes[k];
            var c = (a + b) % 256;
            encryptedBytes[i] = (byte) c;


            if (k == passBytes.Length - 1)
                k = -1;

        }
        //encrypted bytes converted to base 64 string
        var b64Text = System.Convert.ToBase64String(encryptedBytes);
            
        return b64Text;
    }
    
    public static string Decryption(string? b64Text, string passBytes)
    {
        //check if b64Text is really b64Text
        if (!IsValidBase64(b64Text))
            throw new ArgumentException("characters assigned to encrypted text don't belong to base64");
            
        var bytesEncrypted = System.Convert.FromBase64String(b64Text);
        byte[] byteconversion = new Byte[bytesEncrypted.Length];
        for (int k=0, i = 0; i < bytesEncrypted.Length; i++,k++)
        {
            var a = (int)bytesEncrypted[i];
            var b = (int)passBytes[k];
            var c = (a - b) % 256;
            if (c < 0)
                c += 256;
            byteconversion[i] = (byte) c;
        
        
            if (k == passBytes.Length-1 )
                k = -1; 
        }
            
        var decryptedText = System.Text.Encoding.UTF8.GetString(byteconversion);
        return decryptedText;
    }

    public static void CheckInput(string? plainText, string? b64)
    {
        if (plainText == null && b64 == null ||
            plainText != null && b64 != null)
        {
            throw new ArgumentException("both plainText and b64 encrypted text cannot be empty or be filled in.");
            
        }
    }
    
       
}