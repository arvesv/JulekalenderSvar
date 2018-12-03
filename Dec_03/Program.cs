using System;
using System.Collections.Generic;
using System.Linq;

namespace Dec_03
{
    internal class Program
    {
        private const int NoFactors = 24;
        private const ulong MaxJuleNumber = 4294967296;


        private static readonly List<ulong> Memory = new List<ulong>();

        private static ulong GetPrime(int primePos)
        {
            if (primePos >= Memory.Count)
            {
                if (primePos == 0) Memory.Add(2);

                for (var start = Memory.Count; start <= primePos; start++)
                    Memory.Add(GetNextPrime(Memory[Memory.Count - 1]));
            }

            return Memory[primePos];
        }

        private static ulong GetNextPrime(ulong p)
        {
            bool isPrime;
            do
            {
                p++;

                var sqrt = Math.Sqrt(p);

                isPrime = Memory.TakeWhile(oldprimes => oldprimes <= sqrt).All(y => p % y != 0);
            } while (!isPrime);

            return p;
        }

        private static ulong GetJuletall(IEnumerable<int> primes)
        {
            return primes.Aggregate<int, ulong>(1, (current, prime) => current * GetPrime(prime));
        }

        private static void Main(string[] args)
        {
            var answer = 0;
            var primes = new int[NoFactors];

            var tall = GetJuletall(primes);

            var moreRoom = true;
            do
            {
                if (tall < MaxJuleNumber)
                {
                    answer++;
/*                    Console.Write(tall);
                    foreach (var p in primes) Console.Write(" {0}", p);
                    Console.WriteLine();*/
                    primes[0]++;
                }
                else
                {
                    moreRoom = IncrementFlow(primes);
                }

                tall = GetJuletall(primes);
            } while (moreRoom);

            Console.WriteLine("Answer {0}", answer);
        }

        private static bool IncrementFlow(int[] primes)
        {
            var pos = 0;

            while (pos < NoFactors - 1)
            {
                if (primes[pos] > primes[pos + 1])
                {
                    primes[pos + 1]++;
                    for (var i = 0; i <= pos; i++) primes[i] = primes[pos + 1];

                    return true;
                }

            pos++;
        }

        return false;
    }
    }
}