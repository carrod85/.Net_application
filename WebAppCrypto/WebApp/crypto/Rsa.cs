namespace WebApp.Crypto;



public static class Rsa
{

    /* Atributes I need
     * 2 Prime numbers for modulus (if they are the same ->error)
     * 
     */
    // obtain bigges p and q primes.
    public static int BiggestPrime(int N)
    {
        RsaUtils.CheckModulo(N);
        int temp = N;
        while (true)
        {
            if (RsaUtils.IsPrime(temp) == 1)
            {
                break;
            }

            temp--;
            RsaUtils.IsPrime(temp);

        }

        return temp;
    }
    //check that q and p are different and dont overflow
    // calculate n and phi
    
    public static (long, long) NandPhi(int p, int q)
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
    
    //calculate e. It will be as output a valid exponent e (closest)

    public static long ValidExponent(long e, long phi)
    {
        RsaUtils.CheckE(e, phi);
        while (true)
        {
            if (RsaUtils.Gcd(e, phi) == 1)
                break;
            e++;
        }

        return e;
    }
    
    
    //calculate number of Bytes of key call NumberOfBytes
    
    
    //ENCRYPTION
    
    
    //check if number  of key is greater than encryptedText number, obtain byte array from text and corresponding long for that text
    public static long TextToNumber(long n, string inputText)
    {
        var codedBytes = System.Text.Encoding.UTF8.GetBytes(inputText);
        var numberString = RsaUtils.BytesToNumber(codedBytes);
        if (numberString > n)
            throw new ArgumentException("The text to encrypt is too long for key, please insert something shorter.");
        return numberString;
    }
    
    //obtain the encryption  - number
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
    
    // move this encrypted number to base64 nbytes obtain from function NumberOfBytes. for nbytes call NumberofBytes method
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
    
    // obtain base64 encryption text
    public static string Encryption(byte[] byteAr)
    {
        return System.Convert.ToBase64String(byteAr);
    }
        
    //DECRYPTION
    
    
    
    //calculate d
    public static long ExtendedEuclid(long e, long d) // d is phi
    {
        long[] x = {1, 0, d};
        long[] y = {0, 1, e};
        long[] t = {0, 0, 0};
        var i = 1;
        do
        {
            long q = 0;
            if (i == 1)
            {
                q = x[2] / y[2];
                for (var j = 0; j < 3; j++)
                {
                    t[j] = x[j] - (q * y[j]);
                }
            }
            else
            {
                for (var j = 0; j < 3; j++)
                {
                    x[j] = y[j];
                    y[j] = t[j];
                }

                q = x[2] / y[2];
                for (int j = 0; j < 3; j++)
                {
                    t[j] = x[j] - (q * y[j]);
                }
            }

            i++;
            if (y[2] == 0)
            {
                return 0;
            }
        } while (y[2] != 1);

        if (y[1] < 0)
            y[1] += d;
        return y[1];
    }
    public static long Base64ToNumber(long n, string b64Text)
    {
        if (!RsaUtils.IsValidBase64(b64Text))
            throw new ArgumentException("the base64 text is not valid");
        var codedBytes = System.Convert.FromBase64String(b64Text);
        var numberString = RsaUtils.BytesToNumber(codedBytes);
        if (numberString > n)
            throw new ArgumentException("The text to Decrypt is too long for key, please insert something shorter.");
        return numberString;
    }
    
    // call Modpow_expsqr with arguments (numberEnc, d, n) to obtain decrypted number.
    // call Number of Bytes and insert output in NumberToBytes
    public static string Decryption(byte[] byteAr)
    {
        return System.Text.Encoding.UTF8.GetString(byteAr);
    }
}


