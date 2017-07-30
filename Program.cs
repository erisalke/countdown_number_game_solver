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

            double[] A = data.Item1;
            Transformation[] trans = data.Item2;
            
            var solver = new Solver(A, trans);

            solver.play();
            var solutions = solver.getSolutions(target);

            printResult(solutions, target);
        }

    private static void printResult(List<string> collection, int target) {
        var solutions = collection.Select(x => x).Distinct();
        foreach (var solution in solutions) {
            Console.WriteLine($"{target} = {solution.ToString()}");
        }
        Console.WriteLine();
        Console.WriteLine($"Number of uniq solutions: {solutions.Count()}");
        Console.Write("Terminated");
        Console.Read();
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
