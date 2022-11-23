using System.Net.Security;
using Crypto;


Console.WriteLine("Hello, Cesar!");
/*
try
{
    var alphabet = "tasd";
    var text = "t";
    
    var alphaCleaned = CesarUtils.CleaningAlpha(alphabet);
    var textCleaned = CesarUtils.CleaningText(text, alphabet);
    var shiftCleaned = CesarUtils.CleaningShift(4, alphabet);
    var encryptionResult = Cesar.Encryption(alphaCleaned, shiftCleaned, textCleaned);
    Console.WriteLine(encryptionResult);
}

catch(Exception e)
{
    Console.WriteLine(e.Message);
}



try
{
    string text = "0xC0";
    string pass = "ds";
    var result = Vigenere.Encryption(text, pass);
    Console.WriteLine(result);
}

catch(Exception e)
{
    Console.WriteLine(e.Message);
}



try
{
    /*
     *  modulo Prime number 
        Public key G shared
        Private key person X
        Private key person Y
        Shared secret
     
    int m = 200;
    int g = 8;
    int X = 1000;
    int Y = 77;
    var modulo = Dh.BiggestPrime(m);
    Dh.CheckGroup(g);
    Dh.CheckSecret(X, modulo);
    var gX =Dh.Modpow_expsqr(g, X, modulo);
    var gY = Dh.Modpow_expsqr(g, Y, modulo);
    var shared = Dh.Modpow_expsqr(gX, Y, modulo);
    var shared2 = Dh.Modpow_expsqr(gY, X, modulo);
    Console.WriteLine($"same Key (g^x)y {shared} == (g^y)x {shared2}");


}

catch(Exception e)
{
    Console.WriteLine(e.Message);
}

*/


try
{
    var p = 1111;
    var q = 231;
    long e = 23;
    string message = "as";
    var base64Input = "AcO6";
    var pPrime = Rsa.BiggestPrime(p);
    var qPrime = Rsa.BiggestPrime(q);
    RsaUtils.PandQ(pPrime,qPrime);
    var n = Rsa.NandPhi(pPrime, qPrime).Item1;
    var phi = Rsa.NandPhi(pPrime, qPrime).Item2;
    var eValid = Rsa.ValidExponent(e, phi);
    
    //Encryption
    var textNumber = Rsa.TextToNumber(n, message);
    var encryptedNumber = Rsa.Modpow_expsqr(textNumber, eValid, n);
    var nBytesEncryption = RsaUtils.NumberOfBytes(encryptedNumber);
    var byteEncrypted = Rsa.NumberToBytes(encryptedNumber, nBytesEncryption);
    var encryptedText = Rsa.Encryption(byteEncrypted);
    Console.Write($"Encrypted text {encryptedText}\n");
    //Decryption
    var d = Rsa.ExtendedEuclid(eValid, phi);
    var encryptedNumber2 = Rsa.Base64ToNumber(n, base64Input);
    var textNumber2 = Rsa.Modpow_expsqr(encryptedNumber2, d, n);
    var nBytesDecryption = RsaUtils.NumberOfBytes(textNumber2);
    var byteDecrypted = Rsa.NumberToBytes(textNumber2, nBytesDecryption);
    var decryptedText = Rsa.Decryption(byteDecrypted);
    Console.Write($"Decrypted text {decryptedText}");
}

catch (Exception e)
{
    Console.WriteLine(e.Message);
}