using System;
using System.Linq;
//   System.Collections.Generic
namespace csharpfun
{
    class Program
    {
        public static int expected;

        static void Main(string[] args)
        {
            // ------------------------------
            var arr = new int[] {100, 323, 5, 2, 7, 32};
            var expected = 43;
            // ------------------------------

            var solver = prepareData(arr, expected);
            solver.play();

            Console.Write("Terminated");
            Console.Read();
        }

    private static Solver prepareData(int[] arr, int expected)
    {
        // global to not carry that value all around
        Program.expected = expected;

        var length = arr.Length;
        double[] doubles = arr.Select(el => (double)el).ToArray();
        Transformation[] transformations = new Transformation[length];

        for (var i=0; i<length; ++i) {
            transformations[i] = new Transformation(doubles[i]);
        }

        return new Solver(doubles, transformations);
    }
  }
}
