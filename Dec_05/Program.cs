using System;
using System.Collections.Generic;

namespace Dec_05
{
    internal enum Op
    {
        add,
        sub,
        concat
    }


    internal class Program
    {
        private static readonly int[] numbers = {1, 2, 3, 4, 5, 6, 7, 8, 7, 6, 5, 4, 3, 2, 1};
        //static readonly int[] numbers = { 1, 2, 3, 4, 5, 6, 7 };


        private static bool Is42(IList<int> numbers, IList<Op> ops)
        {
            long result = 0;
            var starting = true;


            for (var i = 0; i < numbers.Count; i++)
            {
                var number = "";
                var j = i;
                number += numbers[j].ToString();
                while (j < ops.Count && ops[j] == Op.concat)
                {
                    j++;
                    number += numbers[j].ToString();
                }

                var num = long.Parse(number);


                if (starting)
                {
                    result = num;
                    starting = false;
                }
                else
                {
                    switch (ops[i - 1])
                    {
                        case Op.add:
                            result += num;
                            break;
                        case Op.sub:
                            result -= num;
                            break;
                        case Op.concat:
                            throw new Exception();
                    }
                }

                i = j;
            }

            return result == 42;
        }


        private static IEnumerable<IList<T>> Permutations<T>(IList<T> objects, int dimensions)
        {
            foreach (var o in objects)
            {
                var result = new T[dimensions];
                result[0] = o;
                if (dimensions == 1)
                    yield return result;
                else
                    foreach (var sublist in Permutations(objects, dimensions - 1))
                    {
                        for (var i = 1; i < dimensions; i++) result[i] = sublist[i - 1];
                        yield return result;
                    }
            }
        }


        private static void Main(string[] args)
        {
            var ops = new List<Op> {Op.add, Op.sub, Op.concat};

            var count = 0;
            var q = Permutations(ops, numbers.Length - 1);
            foreach (var p in q)
                if (Is42(numbers, p))
                {
                    Console.WriteLine(p);
                    count++;
                }

            Console.WriteLine(count);
        }
    }
}