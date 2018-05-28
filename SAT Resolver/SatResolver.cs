using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SAT_Resolver
{
    public class SATResolver
    {
        private SatisfiableInfo _satisfiableInfo;
        private object _mutex = new object();

        public SatisfiableInfo IsSatisfiable(List<object> formula)
        {
            _satisfiableInfo = new SatisfiableInfo();
            PrintFormular(formula);

            if (formula.Count > 0)
            {
                AddBracketsForAnds(formula);

                PrintFormular(formula);

                List<Variable> variableList = GetVariables(formula);
                _satisfiableInfo.VariableList = variableList;

                var parallelOptions = new ParallelOptions();
                parallelOptions.MaxDegreeOfParallelism = -1;
                var assignmentCount = Math.Pow(2, variableList.Count);
                Parallel.For(0, Convert.ToInt64(assignmentCount), parallelOptions, i => {
                    var variableAssignment = new bool[variableList.Count];
                    for (var k = 0; k < variableList.Count; k++)
                    {
                        var assignment = ((i >> k) & 1) == 1;
                        variableAssignment[k] = assignment;
                    }

                    var tempSatisfiable = EvaluateClause(formula, variableAssignment, variableList);

                    if (tempSatisfiable && variableAssignment.Length > 0)
                    {
                        lock (_mutex)
                        {
                            _satisfiableInfo.SatisfiableAssignments.Add(variableAssignment);

                            int tempMaxTrueVariation = variableAssignment.Count(assignm => assignm);
                            if (_satisfiableInfo.MaxTrueVaraition < tempMaxTrueVariation)
                            {
                                _satisfiableInfo.MaxTrueVaraition = tempMaxTrueVariation;
                                _satisfiableInfo.MaxSatisfiebleAssignment = variableAssignment;
                            }
                        }                    
                    }

                    _satisfiableInfo.IsSatisfiable = _satisfiableInfo.IsSatisfiable || tempSatisfiable;
                });

                return _satisfiableInfo;
            }

            return _satisfiableInfo;
        }

        private bool EvaluateClause(List<object> clause, bool[] variableAssignment, List<Variable> variableList)
        {
            var initializedEvaluation = false;
            var assignmentEvaluation = false;
            Operator currentOperator = null;
            var hasNegation = 0;

            for (var i = 0; i < clause.Count(); i++)
            {
                var current = clause[i];

                if (current is Operator tempOperator)
                {
                    if (tempOperator.Type == Operator.OperatorType.Not)
                    {
                        hasNegation++;
                    }
                    else
                    {
                        currentOperator = tempOperator;
                    }
                }
                else
                {
                    var tempEvaluation = current is Bracket bracket
                        ? EvaluateClause(bracket.Clause, variableAssignment, variableList)
                        : current is Variable variable
                            ? variableAssignment[variableList.IndexOf(variable)]
                            : current is Constant constant && constant.Type == Constant.ConstantType.True;

                    if (hasNegation % 2 > 0)
                    {
                        tempEvaluation = !tempEvaluation;
                        hasNegation = 0;
                    }

                    if (!initializedEvaluation)
                    {
                        assignmentEvaluation = tempEvaluation;
                        initializedEvaluation = true;
                    }
                    else
                    {
                        if (currentOperator.Type == Operator.OperatorType.And)
                        {
                            assignmentEvaluation = assignmentEvaluation && tempEvaluation;
                        }
                        else
                        {
                            assignmentEvaluation = assignmentEvaluation || tempEvaluation;
                        }

                        currentOperator = null;
                    }
                }
            }

            return assignmentEvaluation;
        }

        private List<Variable> GetVariables(List<object> clause, List<Variable> currentList = null)
        {
            var variables = currentList ?? new List<Variable>();

            foreach (var variable in clause.OfType<Variable>())
            {
                if (!variables.Contains(variable))
                {
                    variables.Add(variable);
                }
            }

            foreach (var bracket in clause.OfType<Bracket>())
            {
                variables = GetVariables(bracket.Clause, variables);
            }

            return variables;
        }
        
        private void AddBracketsForAnds(List<object> pClause, int pDepth = 0)
        {
            if (pDepth == 0)
            {
                Console.WriteLine($"{Console.Out.NewLine}Optimize formula for resolving...");
            }

            var lastOrOperator = -1;
            for (var i = 0; i < pClause.Count; i++)
            {
                var current = pClause[i];
                if (current is Operator op)
                {
                    if (op.Type == Operator.OperatorType.Or)
                    {
                        lastOrOperator = i;
                    }
                    else if (op.Type == Operator.OperatorType.And)
                    {
                        var nextOrOperator = -1;
                        for (var k = i; k < pClause.Count; k++)
                        {
                            var nextCurrent = pClause[k];
                            if (nextCurrent is Operator op2 && op2.Type == Operator.OperatorType.Or)
                            {
                                nextOrOperator = k;
                                break;
                            }
                        }

                        if (lastOrOperator != -1 || nextOrOperator != -1)
                        {
                            var tempBracket = new Bracket(pClause.GetRange(lastOrOperator + 1,
                                nextOrOperator != -1 ? nextOrOperator : i + 1));
                            pClause.RemoveRange(lastOrOperator + 1,
                                nextOrOperator != -1 ? nextOrOperator : i + 1);
                            pClause.Insert(lastOrOperator + 1, tempBracket);

                            i = lastOrOperator + 1;
                            lastOrOperator = -1;
                        }
                    }
                }
                else if (current is Bracket bracket)
                {
                    AddBracketsForAnds(bracket.Clause, pDepth + 1);
                }
            }
        }

        private void PrintFormular(List<object> pClause, int pDepth = 0)
        {
            if(pDepth == 0)
            {
                Console.Write("Formular:");
            }

            foreach (var obj in pClause)
            {
                if (obj is Variable variable)
                {
                    Console.Write($" {variable.Name}");
                }
                else if (obj is Operator op)
                {
                    Console.Write($" {op.Type.ToString().ToUpper()}");
                }
                else if (obj is Constant constant)
                {
                    Console.Write($" {constant.Type.ToString().ToLower()}");
                }
                else if (obj is Bracket bracket)
                {
                    Console.Write(" (");
                    PrintFormular(bracket.Clause, pDepth + 1);
                    Console.Write(" )");
                }
            }

            if(pDepth == 0)
            {
                Console.Write(Console.Out.NewLine);
            }
        }

        private void PrintSatisfiableAssignments(List<List<bool>> pPrintSatisfiableAssignments,
            List<Variable> pVariableList)
        {
            Console.WriteLine($"{Console.Out.NewLine}Satisfiable assignments: ");

            for (var i = 0; i < pVariableList.Count; i++)
            {
                if (i == 0)
                {
                    Console.Write("{0}", pVariableList[i].Name);
                }
                else
                {
                    Console.Write("\t{0}", pVariableList[i].Name);
                }
            }

            foreach (var satisfiableAssignment in pPrintSatisfiableAssignments)
            {
                for (var i = 0; i < pVariableList.Count; i++)
                {
                    if (i == 0)
                    {
                        Console.WriteLine();
                        Console.Write($"{satisfiableAssignment[i]}");
                    }
                    else
                    {
                        Console.Write($"\t{satisfiableAssignment[i]}");
                    }
                }
            }
        }
    }
}