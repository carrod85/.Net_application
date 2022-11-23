using System.Text.RegularExpressions;

namespace Crypto;

public class RsaUtils
{
    public static int IsPrime(int N)
    {
        // Initializing with the value 2
        // from where the number is checked
        int i = 2;

        // Computing the square root of
        // the number N
        int k =(int) Math.Ceiling(Math.Sqrt(N));

        // While loop till the
        // square root of N
        while(i<= k){

            // If any of the numbers between
            // [2, sqrt(N)] is a factor of N
            // Then the number is composite
            if(N % i == 0)
                return 0;
            
            i += 1;
        }

        // If none of the numbers is a factor,
        // then it is a prime number
        return 1;
    }
    public static void CheckModulo(int val)
    {
        if (val <= 2)
            throw new ArgumentException("value must be greater than 2.");
    }
    
    public static void PandQ(int p, int q)
    {
        if (p==q)
            throw new ArgumentException("Insecure!! p and q must be different.");
    }

    public static void CheckE(long input, long phi)
    {
        //controlling that input is gretar than 1 , odd and less than phi
        if (input % 2 == 0 || input <= 1 || input >= phi)
            throw new ArgumentException(
                "value must be 1.-Odd to possible be coprime to phi, 2.-Greater than 1 and 3.-Less than value of phi");
    }

    public static long Gcd(long smaller, long b) 
    { 
        if (smaller == 0) 
            return b; 
        return Gcd(b % smaller, smaller); 
    }
    
    
    public static byte[] NumberToBytes(long encryption, int nbytes)
    {
        byte[] bytes = new byte[nbytes];
        int i = 0;
        var cbytes = nbytes;
        while (i < cbytes)
        {
            bytes[i] = (byte)(encryption >> (8 * (nbytes - 1)));
            ++i;
            --nbytes;
        }

        return bytes;

    }

    public static long BytesToNumber(byte[] ar)
    {
        long i = 0;
        int j = (ar.Length-1)*2;
        foreach (var b in ar)
        {
            i +=b *(long) Math.Pow(16, j);
            j-=2;
        }

        return i;
    }
    
    public static int NumberOfBytes(long n)
    {
        int numberBytes = 0;
        while (n != 0)
        {
            n >>= 8;
            numberBytes++;
        }

        return numberBytes;
    }
    
    // check if it is valid base64Text
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
    
    
}