using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complexitytheory.SAT;
using Complexitytheory.SAT.FormulaComponents;

namespace Complexitytheory.Graph.VertexCover
{
    public class VertexCoverReducer
    {
        private readonly Operator _and = new Operator(Operator.Types.And);
        private readonly Operator _or = new Operator(Operator.Types.Or);
        private readonly Operator _not = new Operator(Operator.Types.Not);

        public Formula ReduceVertexCoverToSat(AdjacentMap pUndirectedGraph,int pMinVertexCount)
        {
            Dictionary<string, Variable> variables = new Dictionary<string, Variable>();

            foreach (var key in pUndirectedGraph.Keys)
            {
                for (int i = 1; i <= pMinVertexCount; i++)
                {
                    variables.Add($"{key}|{i}", new Variable($"{key}|{i}"));
                }
            }

            Console.WriteLine("Created Variables");
            foreach (var variable in variables)
            {
                Console.WriteLine(variable.Value.ToString());
            }

            var formula = new Formula();

            for (int i = 1; i <= pMinVertexCount; i++)
            {
                if (i > 1)
                {
                    formula.Add(_and);
                }

                var atMostOneList = variables.Values.Where(v => v.Name.EndsWith($"|{i}")).ToList();
                var atMostOneFormular = AtMostOne(atMostOneList);
                formula.AddRange(atMostOneFormular);
            }

            List<string> processedEdges = new List<string>();

            foreach (var key in pUndirectedGraph.Keys)
            {
                foreach (var adjacent in pUndirectedGraph[key])
                {
                    if (!processedEdges.Contains($"{key}|{adjacent}") && !processedEdges.Contains($"{adjacent}|{key}"))
                    {
                        var atLeastOneList = new List<Variable>();

                        for (int i = 1; i <= pMinVertexCount; i++) {
                            atLeastOneList.Add(variables[$"{key}|{i}"]);
                            atLeastOneList.Add(variables[$"{adjacent}|{i}"]);
                        }

                        var atleastOneFormular = AtLeastOne(atLeastOneList);
                        formula.Add(_and);
                        formula.Add(atleastOneFormular);

                        processedEdges.Add($"{key}|{adjacent}");
                    }
                }
            }

            return formula;
        }

        private List<IFormulaComponent> AtMostOne(List<Variable> pVariables)
        {
            var formular = new List<IFormulaComponent>();

            for (int i = 0; i < pVariables.Count; i++)
            {
                var variable1 = pVariables[i];

                for (int k = i + 1; k < pVariables.Count; k++)
                {
                    var variable2 = pVariables[k];

                    if (i > 0 || k > 1)
                    {
                        formular.Add(_and);
                    }

                    formular.Add(new Bracket() { _not, variable1, _or, _not, variable2 });
                }
            }

            return formular;
        }

        private Bracket AtLeastOne(List<Variable> pVariables)
        {
            var formular = new Bracket();

            for (int i = 0; i < pVariables.Count; i++)
            {
                var variable1 = pVariables[i];

                if (i > 0)
                {
                    formular.Add(_or);
                }

                formular.Add(variable1);
            }

            return formular;
        }
    }
}
