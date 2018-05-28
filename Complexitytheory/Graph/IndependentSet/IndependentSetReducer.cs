using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complexitytheory.SAT;
using Complexitytheory.SAT.FormulaComponents;

namespace Complexitytheory.Graph.IndependentSet
{
    public class IndependentSetReducer
    {
        private static readonly Operator And = new Operator(Operator.Types.And);
        private static readonly Operator Or = new Operator(Operator.Types.Or);
        private static readonly Operator Not = new Operator(Operator.Types.Not);

        public static Formula ReduceTo3Sat(AdjacentMap pGraph, Dictionary<string, Variable> pVariableStore)
        {
            Formula formular = new Formula();

            foreach (string key in pGraph.Keys)
            {
                Variable currentVertex;
                if (!pVariableStore.ContainsKey(key))
                {
                    currentVertex = new Variable(key);
                    pVariableStore.Add(key, currentVertex);
                }
                else
                {
                    currentVertex = pVariableStore[key];
                }

                foreach (string adjazen in pGraph[key])
                {
                    Variable adjazenVertex;
                    if (!pVariableStore.ContainsKey(adjazen))
                    {
                        adjazenVertex = new Variable(adjazen);
                        pVariableStore.Add(adjazen, adjazenVertex);
                    }
                    else
                    {
                        adjazenVertex = pVariableStore[adjazen];
                    }

                    formular.Add(new Bracket() {Not, currentVertex, Or, Not, adjazenVertex});
                    formular.Add(And);
                }
            }

            formular.RemoveAt(formular.Count - 1);

            return formular;
        }
    }
}
