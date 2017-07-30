namespace csharpfun
{
  internal class Transformation
  {
    public Transformation(double value) {
      this.singleValue = value.ToString();
    }

    public Transformation(Transformation transA, Transformation transB, IOperand operand) {
      this.singleValue = null;
      this.transA = transA;
      this.transB = transB;
      this.operand = operand;
    }
    private string singleValue;
    private Transformation transA;
    private Transformation transB;
    private IOperand operand;

    public override string ToString() {
      return (singleValue != null)
        ? singleValue
        : $"({transA.ToString()} {operand.ToString()} {transB.ToString()})";
    }
  }
}