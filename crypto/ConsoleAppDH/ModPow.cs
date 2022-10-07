namespace ConsoleAppDH;

public class ModPow
{
    // divide and conquer strategy 2^50 mod 23 -> (2^2)^25 mod 23 -> 2*(4^2)^12...
    public  static int Modpow_expsqr(int b, int e, int m)
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
    
        return (int)P;
    }
}