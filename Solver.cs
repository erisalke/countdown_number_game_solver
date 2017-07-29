using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace csharpfun
{
  class Solver
  {
    public Solver(double[] baseNumbers, Transformation[] baseTransformations){ //, int expectedSolution) {
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

    private void prepareChildren() {
      for (var i = 0; i < baseNumbers.Length; i++) {
        for (var j = i + 1; j < baseNumbers.Length; j++) {
          foreach(var operand in Operands.All) {
            addNewSolverToChildren(i, j, operand);
          }
        }
      }
    }
    private void playChildren()
    {
      foreach(var child in children) {
        child.play();
      }
    }

    private void addNewSolverToChildren(int i, int j, IOperand operand)
    {
      var numbers = createNewNumbers(i, j, operand);
      var transformations = createNewTransformations(i, j, operand);

      children.Add(new Solver(numbers.Item1, transformations));

      checkIfDone(numbers.Item2, transformations[i]);
    }

    private void checkIfDone(double newNumber, Transformation transformation)
    {       
      if (newNumber == Program.expected) {
        Console.Write($"DONE, found: {newNumber}");
        Console.WriteLine($"= {transformation}");
        // Console.Read();
      }
    }

    private Transformation[] createNewTransformations(int i, int j, IOperand operand)
    {
      var a = baseTransformations[i].ToString();
      var b = baseTransformations[j].ToString();

      var newTransformation = new Transformation(a, b, operand);
      var transformations = baseTransformations.Where((_, index) => index != j).ToArray();
      transformations[i] = newTransformation;

      return transformations;
    }

    private Tuple<double[], double> createNewNumbers(int i, int j, IOperand operand)
    {
      var a = baseNumbers[i];
      var b = baseNumbers[j];
      var newNumber = operand.resolve(a, b);

      var numbers = baseNumbers.Where((_, index) => index != j).ToArray();
      numbers[i] = newNumber;

      return new Tuple<double[], double>(numbers, newNumber);
    }
  }
}
