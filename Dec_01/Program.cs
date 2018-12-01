using System;
using System.IO;
using System.Linq;

namespace Vekksort
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var limit = 0;

            var y = File
                .ReadAllLines(args[0])
                .Select(int.Parse)
                .Where(num =>
                {
                    if (num < limit)
                    {
                        return false;
                    }

                    limit = num;
                    return true;
                })
                .Sum();

            Console.WriteLine(y);
        }      
    }
}