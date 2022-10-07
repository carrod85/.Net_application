namespace ConsoleAppDH;

static public class Serialize
{
    static public byte[] TrimEnd(byte[] array)
    {
        int lastIndex = Array.FindLastIndex(array, b => b != 0);

        Array.Resize(ref array, lastIndex + 1);

        return array;
    }
    
    public static int Checking(byte[] numbers, int index)
    {
        if (numbers?.Length-1 < index)
        {
            return 0;
        }
        return (byte)(numbers?[index]);
    }
    
    public static byte[] DeepCopy(byte[] result)
    {
        byte[] copyResult =new byte[result.Length];
        for (int i = 0; i < result.Length; i++)
        {
            copyResult[i] = result[i];
        }

        return copyResult;
    }


    static public void Suming(byte[] a, ref byte[] result, int exp, int b)
    {
        while (exp > 0+1)
        {
            if (result.Length == 0) // i must initialize blank array
            {
                var bC = b;
                int carry;
                int i = 0; //iterator for the array
                int j = 0; //iterator for the array of a
                while (j < a.Length) // literally copy of the number in hex to result
                {
                    Array.Resize(ref result, result.Length + 1);
                    result[i] = a[j];
                    j++;
                    i++;

                }

                bC --;
                carry = 0;
                i = 0;
                j = 0;

                while (bC > 0) //first secuence for b:3 3*3(3+3+3)
                {
                    while (i < result.Length) // result is the longest number
                    {
                       
                        int sum = result[i] + Checking(a,j) + carry;
                        carry = sum >> 8;
                        //Console.WriteLine($"carry: {carry}");
                        //Array.Resize(ref result, result.Length + 1);
                        result[i] = (byte)(sum & 0xFF);
                        j++;
                        i++;
                        while (i == result.Length & carry != 0)
                        {
                            Array.Resize(ref result, result.Length + 1);
                            result[i] = (byte)(carry & 0xFF);
                            carry >>= 8;
                            i++;
                        }
                    }

                    bC -= 1;
                    i = 0;
                    j = 0;
                    carry = 0;

                }

            }
            else
            {
                var resultCopy = DeepCopy(result); // copy of initial value.
                var bC = b;
                int carry =0;
                int i = 0; //iterator for the array

                while (bC > 0+1)
                {
                    while (i < result.Length) // The array is longest for sure
                    {
            
                        int sum = result[i] + Checking(resultCopy,i) + carry;
                        carry = sum >> 8;
                        result[i] = (byte)(sum & 0xFF);
                        i++;
                        while (i >= result.Length & carry != 0)
                        {
                            Array.Resize(ref result, result.Length + 1);
                            result[i] = (byte)(carry & 0xFF);
                            carry >>= 8;
                            i++;
                        }



                    }

                    bC--;
                    i = 0;


                }
            }

            exp--;
        }

    }
}