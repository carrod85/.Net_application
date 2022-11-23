namespace WebApp.Crypto;




public static class Dh
    {
        public static int BiggestPrime(int N)
        {
            DhUtils.CheckModulo(N);
            int temp = N;
            while (true)
            {
                if (DhUtils.IsPrime(temp) == 1)
                {
                    break;
                }

                temp--;
                DhUtils.IsPrime(temp);

            }

            return temp;
        }
        
        // group number g check
        public static void CheckGroup(int val)
        {
            if (val <= 1)
                throw new ArgumentException("value must be greater than 1.");
            
        
        }
        
        // secrets
        public static void CheckSecret(int val, int primeNumber)
        {
            if (val <= 1 || val > primeNumber-1 )
                throw new ArgumentException($"value must be greater than 1 and less than prime number {primeNumber}");
               
        }
        
        
        
        //sharedSecret
        // for g^a || g^b and (g^a)^b || (g^b)^a  
        // 2 times must be called. First to compute g^a mod p and then (g^a)^b
        public  static int Modpow_expsqr(int b, int e, int m)
        {
            long B = b;
            long P = 1;
    
            while(e != 0)
            {
                if((e & 1) == 1)
                {
                    P = (P * B) % m;
                }        
                B = (B * B) % m; // this operation is done in the last iteration even though it is not used
                e >>= 1; // shift bytes one position right so it makes the result half always even, except the last iteration
                // which is 10 then 01 which is one so last iteration we go use the remaining P and B obtaining the expected
                // result.
            }
    
            return (int)P;
        }

    }

