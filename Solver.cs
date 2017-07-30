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
      var transformations = createNewTransformations(i, j, operand);

      return new Solver(numbers, transformations);
    }

    private Transformation[] createNewTransformations(int i, int j, IOperand operand)
    {
      var a = baseTransformations[i];
      var b = baseTransformations[j];
      var newTransformation = new Transformation(a, b, operand);

      var transformations = baseTransformations.Where((_, index) => index != j).ToArray();
      transformations[i] = newTransformation;

      return transformations;
    }

    private double[] createNewNumbers(int i, int j, IOperand operand)
    {
      var a = baseNumbers[i];
      var b = baseNumbers[j];
      var newNumber = operand.resolve(a, b);

      var numbers = baseNumbers.Where((_, index) => index != j).ToArray();
      numbers[i] = newNumber;

      return numbers;
    }
  }
}
