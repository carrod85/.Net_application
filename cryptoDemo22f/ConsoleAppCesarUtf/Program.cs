// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, Vignere utf!");

//Console.WriteLine("shift amount: ");
//var amount = int.Parse(Console.ReadLine()!);

Console.Write("plain text: ");
var plainText = Console.ReadLine();
Console.Write("passphrase: ");
var pass = Console.ReadLine();

var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText!);
var passBytes = System.Text.Encoding.UTF8.GetBytes(pass!);

int j = passBytes.Length;
byte[] encryptedBytes = new Byte[plainTextBytes.Length];


for (int k=0,i = 0; i < plainTextBytes.Length; i++,k++)
{
    var a = (int)plainTextBytes[i];
    var b = (int)passBytes[k];
    var c = (a + b) % 256;
    encryptedBytes[i] = (byte) c;
   
    
    if (k == passBytes.Length-1 )
        k = 0; 
}


byte[] byteconversion = new Byte[plainTextBytes.Length];
for (int k=0, i = 0; i < encryptedBytes.Length; i++,k++)
{
    var a = (int)encryptedBytes[i];
    var b = (int)passBytes[k];
    var c = (a - b) % 256;
    if (c < 0)
        c += 256;
    byteconversion[i] = (byte) c;
    
    
    if (k == passBytes.Length-1 )
        k = 0; 
}
// if you try to print the shifting bytes from the utf8 encoding you will fall into incorrect encondings that are not possible
// to decode.  UTF-8 is a charset with a specific encoding format. arbitrary binary data is not valid UTF-8 data.
var b64Text = System.Convert.ToBase64String(encryptedBytes);
Console.WriteLine("encryptedText: " + b64Text);
var decryptedText = System.Text.Encoding.UTF8.GetString(byteconversion);
Console.WriteLine("decryptedText: " + decryptedText);



/*
foreach (var b in plainTextBytes)
{
    var a = b + 20;
    Console.WriteLine(b);
    Console.WriteLine(a);
    
}
//shift the bytes
var b64Text = System.Convert.ToBase64String(plainTextBytes);
Console.WriteLine("b64 text: " + b64Text);
//unshift the bytes
var decryptedBytes = System.Convert.FromBase64String(b64Text);
var decryptedText = System.Text.Encoding.UTF8.GetString(decryptedBytes);
Console.WriteLine("decryptedText: " + decryptedText);
*/