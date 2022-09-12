using ConsoleApp01;

Console.WriteLine("Hello, Cesar!");

string defaultAlphabet = "ABCDEFGHIJKLMNOPQRSTUVXYZ!?.";

var alphabet = Helper.GetUserAlphabet(defaultAlphabet);
Console.WriteLine($"Alphabet: {alphabet}");



// TODO: write a input function that would correctly validate user input and return valid numeric value at the end
// look into int.TryParse()

var checkInput = ShiftHelp.GetUserShift(alphabet.Length);
Console.WriteLine($"Shift: {checkInput}");
