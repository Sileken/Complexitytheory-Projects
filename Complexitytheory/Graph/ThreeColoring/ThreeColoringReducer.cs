using System;
using System.Collections.Generic;
using Complexitytheory.SAT;
using Complexitytheory.SAT.FormulaComponents;

namespace Complexitytheory.Graph.ThreeColoring
{
    public class ThreeColoringReducer
    {
        public static Formula ReduceTo3Sat(AdjacentMap pGraph)
        {
            Dictionary<String, Variable> variableStore = new Dictionary<string, Variable>();
            Operator and = new Operator(Operator.Types.And);
            Operator or = new Operator(Operator.Types.Or);
            Operator not = new Operator(Operator.Types.Not);

            var formula = new Formula();

            var isFirst = true;
            foreach (var node in pGraph.Keys)
            {
                Variable red;
                if (!variableStore.ContainsKey($"{node}|Red"))
                {
                    red = new Variable($"{node}|Red");
                    variableStore.Add($"{node}|Red", red);
                }
                else
                {
                    red = variableStore[$"{node}|Red"];
                }

                Variable blue;
                if (!variableStore.ContainsKey($"{node}|Blue"))
                {
                    blue = new Variable($"{node}|Blue");
                    variableStore.Add($"{node}|Blue", blue);
                }
                else
                {
                    blue = variableStore[$"{node}|Blue"];
                }

                Variable green;
                if (!variableStore.ContainsKey($"{node}|Green"))
                {
                    green = new Variable($"{node}|Green");
                    variableStore.Add($"{node}|Green", green);
                }
                else
                {
                    green = variableStore[$"{node}|Green"];
                }

                var nodeHasColor = new Bracket() { red, or, blue, or, green };

                if (!isFirst)
                {
                    formula.Add(and);
                }
                else
                {
                    isFirst = false;
                }
                formula.Add(nodeHasColor);

                foreach (var adjazenzNode in pGraph[node])
                {
                    Variable adjazenRed;
                    if (!variableStore.ContainsKey($"{adjazenzNode}|Red"))
                    {
                        adjazenRed = new Variable($"{adjazenzNode}|Red");
                        variableStore.Add($"{adjazenzNode}|Red", adjazenRed);
                    }
                    else
                    {
                        adjazenRed = variableStore[$"{adjazenzNode}|Red"];
                    }

                    Variable adjazenBlue;
                    if (!variableStore.ContainsKey($"{adjazenzNode}|Blue"))
                    {
                        adjazenBlue = new Variable($"{adjazenzNode}|Blue");
                        variableStore.Add($"{adjazenzNode}|Blue", adjazenBlue);
                    }
                    else
                    {
                        adjazenBlue = variableStore[$"{adjazenzNode}|Blue"];
                    }

                    Variable adjazenGreen;
                    if (!variableStore.ContainsKey($"{adjazenzNode}|Green"))
                    {
                        adjazenGreen = new Variable($"{adjazenzNode}|Green");
                        variableStore.Add($"{adjazenzNode}|Green", adjazenGreen);
                    }
                    else
                    {
                        adjazenGreen = variableStore[$"{adjazenzNode}|Green"];
                    }

                    formula.Add(and);
                    formula.Add(new Bracket() { not, red, or, not, adjazenRed });

                    formula.Add(and);
                    formula.Add(new Bracket() { not, blue, or, not, adjazenBlue });

                    formula.Add(and);
                    formula.Add(new Bracket() { not, green, or, not, adjazenGreen });
                }
            }

            return formula;
        }
    }
}
