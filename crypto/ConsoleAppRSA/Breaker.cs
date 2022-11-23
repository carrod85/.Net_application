namespace ConsoleAppRSA;

public class Breaker
{
    public static long breakerMachine(long squareRootOfN, long n)
    {
        long searchValue = squareRootOfN;
        bool found = false;
        while (!found)
        {
            searchValue -= 1;
            if (n % searchValue == 0)
            {
                found = true;
            }
        }

        return searchValue;
    }
}