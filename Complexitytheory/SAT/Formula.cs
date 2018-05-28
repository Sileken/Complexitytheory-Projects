using System.Collections.Generic;
using Complexitytheory.SAT.FormulaComponents;

namespace Complexitytheory.SAT
{
    public class Formula: List<IFormulaComponent> 
    {
        public Formula() : base()
        { }

        public Formula(IEnumerable<IFormulaComponent> pCollection) : base(pCollection)
        { }

        public List<Variable> GetVariables()
        {
            var variables = new List<Variable>();

            GetVariables(variables);

            return variables;
        }

        protected void GetVariables(List<Variable> pVariables)
        {
            foreach (IFormulaComponent component in this)
            {
                if (component is Variable variable && !pVariables.Contains(variable))
                {
                    pVariables.Add(variable);
                }
                else if (component is Bracket bracket)
                {
                    bracket.GetVariables(pVariables);
                }
            }
        }

        public override string ToString()
        {
            return $"( {string.Join(" ", this)} )";
        }
    }
}
