using System.Runtime.CompilerServices;

namespace ConsoleAppDH;

public static class PrimeHelper
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

    public static int BiggestPrime(int N)
    {
        int temp = N;
        while (true)
        {
            if (IsPrime(temp) == 1)
            {
                break;
            }

            temp--;
            IsPrime(temp);

        }

        return temp;
    }
    
}