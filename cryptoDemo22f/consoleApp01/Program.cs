using ConsoleApp01;

Console.WriteLine("Hello, Cesar!");

string defaultAlphabet = "ABCDEFGHIJKLMNOPQRSTUVXYZ!?.";

/*get the alphabet with input validation*/




// TODO: write a input function that would correctly validate user input and return valid numeric value at the end
// look into int.TryParse()
/*
var checkShift = Helper.GetUserShift(alphabet.Length);
Console.WriteLine($"Shift: {checkShift}");
if (checkShift == 0)
{   Console.WriteLine("program finished");
    return 0;
}
*/
//int checkShift;
// string checkText;
//string encryption;

try
{ 
    var alphabet = Helper.GetUserAlphabet(defaultAlphabet);
    Console.WriteLine($"Alphabet: {alphabet}");
    int checkShift = Helper.GetUserShift(alphabet.Length);
    Console.WriteLine($"Shift: {checkShift}");
    string checkText = Helper.GetPlainText(alphabet);
    Console.WriteLine($"Text: {checkText}");
    string encryption = Helper.GetEncryption(alphabet, checkShift, checkText);
    Console.WriteLine($"Encryption: {encryption}");
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}


/*
var encryption = Helper.GetEncryption(alphabet, checkShift, checkText);
Console.WriteLine($"Encryption: {encryption}");
*/