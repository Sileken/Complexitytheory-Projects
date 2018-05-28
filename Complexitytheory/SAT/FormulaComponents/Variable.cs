namespace Complexitytheory.SAT.FormulaComponents
{
    public class Variable : IFormulaComponent
    {
        public string Name { get; private set; }

        public Variable(string pName)
        {
            this.Name = pName;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}