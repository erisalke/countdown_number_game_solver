namespace csharpfun
{
  internal class Transformation
  {
    public Transformation(double value) {
      this.value = value.ToString();
    }

    public Transformation(string valueA, string valueB, IOperand operand) {
      this.value = $"({valueA} {operand.ToString()} {valueB})";
    }
    private string value;

    public override string ToString() {
      return value;
    }
  }
}