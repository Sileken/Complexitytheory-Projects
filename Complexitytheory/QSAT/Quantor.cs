using Complexitytheory.SAT.FormulaComponents;

namespace Complexitytheory.QSat
{
    public class Quantor
    {
        public enum QuantorTypes
        {
            AllQuantor,
            ExistenceQuantor
        }

        public QuantorTypes Type { get; private set; }

        public Variable Variable { get; private set; }

        public Quantor(QuantorTypes pQuantorType, Variable pVariable)
        {
            this.Type = pQuantorType;
            this.Variable = pVariable;
        }
    }
}
