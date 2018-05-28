using System;
using System.Collections.Generic;
using System.Linq;
using Complexitytheory.SAT.FormulaComponents;
using Complexitytheory.SAT.ReductionInfos;
using Complexitytheory.SubsetSum;

namespace Complexitytheory.SAT
{
    public class ThreeSatReducer
    {
        public static ThreeSatToSubsetSumReductionInfo ReduceToSubsetSum(Formula pForumula3Cnf)
        {
            Dictionary<string, double[]> numbers = new Dictionary<string, double[]>();
            List<Variable> variables = new List<Variable>();
            List<Formula> clauses = new List<Formula>();
            
            foreach (var obj in pForumula3Cnf)
            {
                if (obj is Bracket bracket)
                {
                    clauses.Add(bracket);

                    foreach (var clauseObj in bracket)
                    {
                        if (clauseObj is Variable variable && variables.All(v => v.Name != variable.Name))
                        {
                            variables.Add(variable);
                        }
                    }
                }
            }

            double[] targetSumNumber = new double[variables.Count + clauses.Count];

            for (int i = 0; i < variables.Count; i++)
            {
                Variable currVarialbe = variables[i];
                double[] numberPositive = new double[variables.Count + clauses.Count];
                double[] numberNegative = new double[variables.Count + clauses.Count];
                numberPositive[i] = 1;
                numberNegative[i] = 1;

                numbers.Add($"p|{currVarialbe.Name}", numberPositive);
                numbers.Add($"n|{currVarialbe.Name}", numberNegative);

                targetSumNumber[i] = 1;
            }

            for (int c = 0; c < clauses.Count; c++)
            {
                Formula clause = clauses[c];

                double[] numberPositive = new double[variables.Count + clauses.Count];
                double[] numberNegative = new double[variables.Count + clauses.Count];
                numberPositive[variables.Count + c] = 1;
                numberNegative[variables.Count + c] = 2;

                numbers.Add($"p|clause{c}", numberPositive);
                numbers.Add($"n|clause{c}", numberNegative);

                bool isNegation = false;
                foreach (IFormulaComponent obj in clause)
                {
                    if (obj is Operator op && op.Type == Operator.Types.Not)
                    {
                        isNegation = !isNegation;
                    } else if (obj is Variable variable)
                    {
                        if (isNegation)
                        {
                            numbers[$"n|{variable.Name}"][variables.Count + c] = 1;
                        } else {
                            numbers[$"p|{variable.Name}"][variables.Count + c] = 1;
                        }
                        isNegation = false;
                    }
                }

                targetSumNumber[variables.Count + c] = 4;
            }

            Dictionary<double, string> numberVariableMapping = new Dictionary<double, string>();
            double[] weight = new double[numbers.Count];
            var weightIndex = 0;
            foreach (var number in numbers)
            {
                double doubles = double.Parse(String.Join("", number.Value));
                if (!number.Key.StartsWith("p|clause") && !number.Key.StartsWith("n|clause"))
                {
                    numberVariableMapping.Add(doubles, number.Key);
                }
                weight[weightIndex] = doubles;
                weightIndex++;
            }

            double targetSum = double.Parse(String.Join("", targetSumNumber));
        
            return new ThreeSatToSubsetSumReductionInfo(numberVariableMapping, new SubSetSumInfo(weight, targetSum), variables.Count, clauses.Count);
        }
    }
}