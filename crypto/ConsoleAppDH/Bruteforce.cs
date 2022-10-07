namespace ConsoleAppDH;

public class Bruteforce
{
    public static int Brute(int g, int m, int result)
    {
        var found = false;
        int trial = 0;
        do
        {
            ++trial;
            if (ModPow.Modpow_expsqr(g, trial, m) == result)
                found = true;
            //Console.WriteLine($"trial: {trial}");


        }while(found == false);

        return trial;
    }
}