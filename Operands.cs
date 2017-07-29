using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace csharpfun
{
  static class Operands {
    public static Collection<IOperand> All {
      get {
        return new Collection<IOperand>()
        {
          new Multiplication(),
          new Sum(),
          new Division(),
          new Subtraction(),
        };
      }
    } 
  }

  interface IOperand {
    double resolve(double a, double b);
  }

  class Multiplication : IOperand
  {
    public double resolve(double a, double b)
    {
      return a * b;
    }

    public override string ToString() {
      return "*";
    }
  }

  class Sum : IOperand
  {
    public double resolve(double a, double b)
    {
      return a + b;
    }
    public override string ToString() {
      return "+";
    }
  }
  
  class Subtraction : IOperand
  {
    public double resolve(double a, double b)
    {
      return a - b;
    }
    public override string ToString() {
      return "-";
    }
  }
  
  class Division : IOperand
  {
    public double resolve(double a, double b)
    {
      return a / b;
    }
    public override string ToString() {
      return "/";
    }
  }

}