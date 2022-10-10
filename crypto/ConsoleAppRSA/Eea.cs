namespace ConsoleAppRSA;

public class Eea
{
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
}