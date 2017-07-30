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
        (value_i, value_j, operad) => operand.resolve(value_i, value_j);

      return createNewArray<double>(baseNumbers, i, j, operand, thunk);
    }

    private Transformation[] createNewTransformations(int i, int j, IOperand operand)
    {
      Func<Transformation, Transformation, IOperand, Transformation> thunk =
        (value_i, value_j, operand_) => new Transformation(value_i, value_j, operand_);

      return createNewArray<Transformation>(baseTransformations, i, j, operand, thunk);
    }

    private T[] createNewArray<T>(
      T[] array, int position_i, int position_j, IOperand operand,
      Func<T, T, IOperand, T> thunk
    ) {
      var newElement = thunk(array[position_i], array[position_j], operand);
      var newArray = array.Where((_, index) => index != position_j).ToArray();
      newArray[position_i] = newElement;

      return newArray;
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
