// See https://aka.ms/new-console-template for more information

using System.Buffers.Binary;
using ConsoleAppRSA;

Console.WriteLine("Hello, RSA!");

try
{
    
    Console.WriteLine("Prime number");
    var value = InputHelper.Inputp();
    string priming = PrimeHelper.IsPrime(value) == 1 ? "number is prime" : "number not prime";
    Console.WriteLine(priming);
    int p = PrimeHelper.BiggestPrime(value);
    Console.WriteLine($"biggest prime from your input is: {p}");
    var value2 = InputHelper.Inputq();
    string priming2 = PrimeHelper.IsPrime(value2) == 1 ? "number is prime" : "number not prime";
    Console.WriteLine(priming2);
    int q = PrimeHelper.BiggestPrime(value2);
    Console.WriteLine($"biggest prime from your input is: {q}");
    if (p == q)
    {
        throw new ApplicationException("Insecure!! p and q must be different.");
    }

    var n = Calculations.nAndPhi(p, q).Item1;
    var phi = Calculations.nAndPhi(p, q).Item2;
    /*
    Console.WriteLine($"n value: {n}");
    Console.WriteLine($"phi value: {phi}");
    // e must be odd for sure since (p-1)*(q-1) is even for them to be coprime
    var exponent = InputHelper.Inpute(phi);
    Calculations.ValidExponent(ref exponent,phi);
    Console.WriteLine($"closest up valid exponent 'e' is(has to be coprime to phi): {exponent}");
    var d = Eea.ExtendedEuclid(exponent, phi);
    Console.WriteLine($"'d' is: {d}");
    var numberBytesKey = Calculations.NumberOfBytes(n);
    Console.WriteLine($"'bytes of key' is: {numberBytesKey}");
    var text = TextInput.InputText(n);
    var numberText = Calculations.BytesToNumber(text);
    Console.WriteLine($"'value of text number' is: {numberText}");
    var encryption = Calculations.Modpow_expsqr(numberText, exponent, n);
    Console.WriteLine($"'value of encryption' is: {encryption}");
    var decryption = Calculations.Modpow_expsqr(encryption, d, n);
    Console.WriteLine($"'value of decryption' is: {decryption}");
    */
    /*Using base64 for encrypted text*/
    /*
    var numberOfBytes = Calculations.NumberOfBytes(encryption);
    var byteAr = Calculations.NumberToBytes(encryption, numberOfBytes);
    var b64Text = System.Convert.ToBase64String(byteAr);
    Console.WriteLine($"encrypted text: {b64Text}");
    var bytesEnc = System.Convert.FromBase64String(b64Text);//byte array encrypted
    var numberEnc = Calculations.BytesToNumber(bytesEnc); // corresponding number to encrypted array
    var decryption2 = Calculations.Modpow_expsqr(numberEnc, d, n);//decryption number
    Console.WriteLine($"'number encrypted': {numberEnc}");
    var bytesOfDecription = Calculations.NumberOfBytes(decryption2);//number of bytes decryption number to host array of bytes
    var final = Calculations.NumberToBytes(decryption2,bytesOfDecription);//final byte array decrypted
    var result = System.Text.Encoding.UTF8.GetString(final);//string 
    Console.WriteLine($"'value of decryption' is: {result}");
    
    */
    //breaking rsa
    var squareOfN = (long)Math.Floor(Math.Sqrt(n));
    Console.WriteLine(squareOfN);
    var factor = Breaker.breakerMachine(squareOfN, n);
    Console.WriteLine(factor);



}
catch(Exception e)
{
    Console.WriteLine(e.Message); 
}