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
            using (var reader = new StreamReader(
                new HttpClient().GetStreamAsync(url).Result))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
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
                    var pattern = @"\((\d+),\s*(\d+)\)\;\s*\((\d+),\s*(\d+)\)";

                    var m = Regex.Match(line, pattern);
                    return new Tuple<int, int>(
                        int.Parse(m.Groups[3].Value) - int.Parse(m.Groups[1].Value),
                        int.Parse(m.Groups[4].Value) - int.Parse(m.Groups[2].Value));
                })
                .Select(point => Math.Atan2(point.Item1, point.Item2))
                
                .GroupBy(
                    angle => angle,
                    (key, result) => new {Angle = key, Count = result.Count()})
                .OrderByDescending(e => e.Count)
                .First()
                .Count;

            Console.WriteLine(x);
        }
    }
}
