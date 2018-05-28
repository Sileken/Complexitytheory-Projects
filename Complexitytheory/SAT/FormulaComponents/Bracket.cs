using System.Collections.Generic;

namespace Complexitytheory.SAT.FormulaComponents
{
    public class Bracket : Formula, IFormulaComponent
    {
        public Bracket() : base()
        { }

        public Bracket(IEnumerable<IFormulaComponent> pCollection) : base(pCollection)
        { }

        public override string ToString()
        {
            return $"( {string.Join(" ", this)} )";
        }
    }
}