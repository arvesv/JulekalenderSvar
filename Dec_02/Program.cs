using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dec_02
{
    class Program
    {
        static IEnumerable<string> ReadData(string url)
        {
            using (var reader = new StreamReader(new HttpClient().GetStreamAsync(url).Result))
            {
                for (var line = reader.ReadLine(); line != null; line = reader.ReadLine())
                {
                    yield return line;
                }
            }
        }

        static void Main(string[] args)
        {
            var x = ReadData("https://s3-eu-west-1.amazonaws.com/knowit-julekalender-2018/input-rain.txt")
                .Select(line =>
                {
                    var m = Regex.Match(line, @"\((\d+),\s*(\d+)\)\;\s*\((\d+),\s*(\d+)\)");
                    return Math.Atan2(int.Parse(m.Groups[3].Value) - int.Parse(m.Groups[1].Value), int.Parse(m.Groups[4].Value) - int.Parse(m.Groups[2].Value));
                })
                .GroupBy(
                    angle => angle,
                    (key, result) => new {Angle = key, Count = result.Count()})
                .OrderByDescending(e => e.Count).First().Count;

            Console.WriteLine(x);
        }
    }
}
