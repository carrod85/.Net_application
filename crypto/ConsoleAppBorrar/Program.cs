// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

string a = "ab==";
var ByteAr = Convert.FromBase64String(a);
foreach (var ele in ByteAr)
{
    Console.WriteLine(ele);
}