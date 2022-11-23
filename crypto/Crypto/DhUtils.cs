namespace Crypto;


public static class DhUtils
{
    public static void CheckModulo(int val)
    {
        if (val <= 2)
            throw new ArgumentException("value must be greater than 2.");
            
        
    }
    
    
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
    


}
