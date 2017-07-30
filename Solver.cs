using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace csharpfun
{
  class Solver
  {
    public Solver(double[] baseNumbers, Transformation[] baseTransformations) {
      this.children = new Collection<Solver>();
      this.baseNumbers = baseNumbers;
      this.baseTransformations = baseTransformations;
    }

    private readonly double[] baseNumbers;
    private Transformation[] baseTransformations;
    private Collection<Solver> children;

    public void play()
    {
      prepareChildren();
      playChildren();
    }

    public List<string> getSolutions(int target) {
      var results = new List<string>();
      results.AddRange(scanThisSovler(target));

      foreach(var childSolver in children) {
        var list = childSolver.getSolutions(target);
        results.AddRange(list);
      }

      return results;
    }

    private void prepareChildren() {
      for (var i = 0; i < baseNumbers.Length; i++) {
        for (var j = i + 1; j < baseNumbers.Length; j++) {
          foreach(var operand in Operands.All) {
            var solver = createNewSolver(i, j, operand);
            children.Add(solver);
          }
        }
      }
    }
    private void playChildren()
    {
      foreach(var childSolver in children) {
        childSolver.play();
      }
    }

    private Solver createNewSolver(int i, int j, IOperand operand)
    {
      var numbers = createNewNumbers(i, j, operand);     
      var transformations = createNewTransformations(i,j,operand);

      return new Solver(numbers, transformations);
    }

    private double[] createNewNumbers(int i, int j, IOperand operand)
    {
      Func<double, double, IOperand, double> thunk =
        (a, b, op) => operand.resolve(a, b);

      return createNewArray<double>(baseNumbers, i, j, operand, thunk);
    }

    private Transformation[] createNewTransformations(int i, int j, IOperand operand)
    {
      Func<Transformation, Transformation, IOperand, Transformation> thunk =
        (a, b, op) => new Transformation(a, b, op);

      return createNewArray<Transformation>(baseTransformations, i, j, operand, thunk);
    }

    private T[] createNewArray<T>(
      T[] arr, int i, int j, IOperand operand,
      Func<T, T, IOperand, T> thunk
    ) {
      var newElement = thunk(arr[i], arr[j], operand);
      var newArr = arr.Where((_, index) => index != j).ToArray();
      newArr[i] = newElement;

      return newArr;
    }
    private List<string> scanThisSovler(int target)
    {
      var results = new List<string>();

      for (var i=0; i<baseNumbers.Length; i++) {
        if (baseNumbers[i] == target) {
          results.Add(baseTransformations[i].ToString());
        }
      }

      return results;
    }
  }
}
