// See https://aka.ms/new-console-template for more information

using System.Numerics;
using System.Runtime.InteropServices;
using ConsoleAppDH;

Console.WriteLine("Hello, Diffie Hellman!");


// to do check that number is prime
// int number 0 to 2147483647
// input check must be int parse and no greater than 2147483647.
//Console.Write("Public key P (prime):");
try
{
    Console.WriteLine("Prime number");
    var value = InputHelper.InputUser();
    string priming = PrimeHelper.IsPrime(value)==1? "number is prime": "number not prime";
    Console.WriteLine(priming);
    int p = PrimeHelper.BiggestPrime(value);
    Console.WriteLine($"biggest prime up to your input is: {p}");
    Console.Write("Public key G (base number)...");
    var g = InputHelper.InputUser3();
    Console.Write("PersonX private key A...");
    var a = InputHelper.InputUser2(p);
    Console.Write("PersonY private key B...");
    var b = InputHelper.InputUser2(p);
    //serialize to save big number in byte array g^a
    byte[] pBytes = BitConverter.GetBytes(g);
    var conv = Serialize.TrimEnd(pBytes);
    byte[]? resultado = {};
    
    Serialize.Suming(conv, ref resultado, a, g);
    // simple check for up to double numbers. It works pretty well but very slow algorithm.
    
    double final=0;
    Console.WriteLine($"Double max value {Double.MaxValue}");

    for (int i = 0, j=0; i < resultado.Length; i++)
    {
        final+=resultado[i]*(double)(Math.Pow(16,j));
        j+=2;
    }
    
    Console.WriteLine(final);
    
    var computeA = ModPow.Modpow_expsqr(g, a, p);
    Console.WriteLine($"PersonX computes g^a: {computeA}");
    var computeB = ModPow.Modpow_expsqr(g, b, p);
    Console.WriteLine($"PersonY computes g^a: {computeB}");
    var finalResultA = ModPow.Modpow_expsqr(computeA, b, p);
    var finalResultB = ModPow.Modpow_expsqr(computeB, a, p);
    Console.WriteLine($"{finalResultA}={finalResultB}");
    // now bruteforce private key.
    var check = Bruteforce.Brute(g, p, computeA);
    Console.WriteLine($"brute force obtaining private key A:{a} == Bruteforce result:{check}");



}
catch(Exception e)
{
    Console.WriteLine(e.Message); 
}



