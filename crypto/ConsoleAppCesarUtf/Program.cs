// See https://aka.ms/new-console-template for more information

using ConsoleAppCesarUtf;

Console.WriteLine("Hello, Vigenere utf!");


// if you try to print the shifting bytes from the utf8 encoding you will fall into incorrect encondings that are not possible
// to decode.  UTF-8 is a charset with a specific encoding format. arbitrary binary data is not valid UTF-8 data.
   

try
{
    var text = HelperInput.InputText();
    var passBytes = HelperInput.InputText2();
    var encryption = HelperInput.Encryption(text, passBytes);
    Console.WriteLine($"Encryption: {encryption}");
    var b64Text = HelperInput.InputText3();
    var decryption = HelperInput.Decryption(b64Text, passBytes);
    Console.WriteLine($"Decryption: {decryption}");



}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}