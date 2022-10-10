namespace ConsoleAppRSA;

public class Calculations
{
    public static (long, long) nAndPhi(int p, int q)
    {
        long n;
        long phi;
        try
        {
            n = checked(p * q);
            phi = (p - 1) * (q - 1);
        }
        catch (OverflowException e)
        {

            throw new Exception("Your resulting number overflows 8 bytes length");
        }

        return (n, phi);
    }
    
    public static long Gcd(long smaller, long b) 
    { 
        if (smaller == 0) 
            return b; 
        return Gcd(b % smaller, smaller); 
    }
    
    public static void ValidExponent(ref long e, long phi)
    {
        while (true)
        {
            if (Gcd(e, phi) == 1)
                break;
            e++;
        }
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
    public  static long Modpow_expsqr(long b, long e, long m)
    {
        long B = b;
        long P = 1;
    
        while(e != 0)
        {
            if((e & 1) == 1)
            {
                P = (P * B) % m;
            }        
            B = (B * B) % m; // this operation is done in the last iteration even though it is not used
            e >>= 1; // shift bytes one position right so it makes the result half always even, except the last iteration
            // which is 10 then 01 which is one so last iteration we go use the remaining P and B obtaining the expected
            // result.
        }
    
        return P;
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

    

}