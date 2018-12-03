using System;
using System.Collections.Generic;
using System.Linq;

namespace Dec_03
{
    internal class Program
    {
        private const int NoFactors = 24;
        private const ulong MaxJuleNumber = 4294967296;


        private static List<ulong> memory = new List<ulong>();

        private static ulong GetPrime(int primePos)
        {
            if (primePos >= memory.Count)
            {
                if (primePos == 0)
                {
                    memory.Add(2);
                }

                for (var start = memory.Count; start <= primePos; start++)
                {
                    memory.Add(GetNextPrime(memory[memory.Count - 1]));
                }
            }

            return memory[primePos];
        }

        private static ulong GetNextPrime(ulong p)
        {
            bool isPrime;
            do
            {
                p++;

                var sqrt = Math.Sqrt(p);

                isPrime = !memory.TakeWhile(oldprimes => oldprimes <= sqrt).Any(y => p % y == 0);

            } while (!isPrime);

            return p;
        }

        private static ulong GetJuletall(int[] primes)
        {
            ulong result = 1;

            foreach (var prime in primes)
            {
                result *= GetPrime(prime);
            }

            return result;
        }

        private static void Main(string[] args)
        {
            int answer = 0;
            int primeNo = 0;

            int[] primes = new int[NoFactors];


            var tall = GetJuletall(primes);

            bool moreRoom = true;
            do
            {
                if (tall < MaxJuleNumber)
                {
                    answer++;
                    Console.Write(tall);
                    foreach (var p in primes)
                    {
                        Console.Write(" {0}", p);                      
                    }
                    Console.WriteLine();
                    primes[0]++;
                }
                else
                {
                    moreRoom = IncrementFlow(primes);
                }

                tall = GetJuletall(primes);

            } while (moreRoom);

            Console.WriteLine("Hello World! {0}", answer);
        }

        private static bool IncrementFlow(int[] primes)
        {
            int pos = 0;

            while (pos < NoFactors - 1)
            {

                if (primes[pos] > primes[pos + 1])
                {
                    primes[pos + 1]++;
                    for (int i = 0; i <= pos; i++)
                    {
                        primes[i] = primes[pos + 1];

                    }

                    return true;
                }
                else
                    pos++;
            }

            return false;
        }
    }
}
