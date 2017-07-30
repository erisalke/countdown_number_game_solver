using System;
using System.Collections.Generic;
using System.Linq;

namespace csharpfun
{
    class Program
    {
        static void Main(string[] args)
        {
            // ------------------------------
            var arr = new int[] {100, 23, 15, 7, 2, 3};
            var target = 542;
            // ------------------------------

            var data = prepareData(arr);
            
            var solver = new Solver(data.Item1, data.Item2);
            solver.play();
            var solutions = solver.getSolutions(target);

            print<string>(solutions, target);

            Console.Write("Terminated");
            Console.Read();
        }

    private static void print<T>(ICollection<T> collection, int target) {
        var solutions = collection.Select(x => x).Distinct();
        foreach (var solution in solutions) {
            Console.WriteLine($"{target} = {solution.ToString()}");
        }
        Console.WriteLine();
        Console.WriteLine($"Number of uniq solutions: {solutions.Count()}");
    }

    private static Tuple<double[], Transformation[]> prepareData(int[] arr) {
        var length = arr.Length;
        var doubles = arr.Select(el => (double)el).ToArray();
        var transformations = new Transformation[length];

        for (var i=0; i<length; ++i) {
            transformations[i] = new Transformation(doubles[i]);
        }

        return new Tuple<double[], Transformation[]>(doubles, transformations);
    }
  }
}
