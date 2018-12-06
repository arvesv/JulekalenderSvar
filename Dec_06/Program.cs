using System;
using System.Diagnostics;
using System.Linq;

namespace Dec_06
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();

            Console.WriteLine(Enumerable
                .Range(1, 18163106)
                .Where(num =>
                {
                    var digits = num.ToString();
                    var numzerodigits = digits.Count(c => c == '0');
                    return numzerodigits > (digits.Length - numzerodigits);
                })
                .Select(i => (long) i)
                .Sum());

            stopWatch.Stop();
            var x = stopWatch.Elapsed;
        }
    }
}
