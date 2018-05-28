using System.Collections.Generic;
using Complexitytheory.SAT.FormulaComponents;

namespace Complexitytheory.SAT
{
    public class SatisfiabilityInfo
    {
        private object _mutex = new object();
        private List<bool[]> _satisfiableAssignments = new List<bool[]>();
        private List<Variable> _variableList = new List<Variable>();
        private bool _isSatisfiable = false;

        public List<bool[]> SatisfiableAssignments
        {
            get
            {
                lock (_mutex)
                {
                    return _satisfiableAssignments;
                }
            }
            set
            {
                lock (_mutex)
                {
                    _satisfiableAssignments = value;
                }
            }
        }

        public List<Variable> VariableList
        {
            get
            {
                lock (_mutex)
                {
                    return _variableList;
                }
            }
            set
            {
                lock (_mutex)
                {
                    _variableList = value;
                }
            }
        }

        public bool IsSatisfiable
        {
            get
            {
                lock (_mutex)
                {
                    return _isSatisfiable;
                }
            }
            set
            {
                lock (_mutex)
                {
                    _isSatisfiable = value;
                }
            }
        }
    }
}