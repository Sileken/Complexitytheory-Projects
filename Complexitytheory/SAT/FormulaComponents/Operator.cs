namespace Complexitytheory.SAT.FormulaComponents
{
    public class Operator : IFormulaComponent
    {
        public enum Types
        {
            And,
            Or,
            Not
        }

        public Types Type { get; private set; }

        public Operator(Types pType)
        {
            this.Type = pType;
        }

        public override string ToString()
        {
            return $"{Type.ToString().ToLower()}";
        }
    }
}