namespace Complexitytheory.SAT.FormulaComponents
{
    public class Constant : IFormulaComponent
    {
        public enum Types
        {
            True,
            False
        }

        public Types Type { get; private set; }

        public Constant(Types pType)
        {
            this.Type = pType;
        }

        public override string ToString()
        {
            return $"{Type.ToString().ToLower()}";
        }
    }
}