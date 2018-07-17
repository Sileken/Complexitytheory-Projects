using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Complexitytheory.SAT.FormulaComponents;

namespace Complexitytheory.SAT
{
    public class SatResolver
    {
        private SatisfiabilityInfo _satisfiableInfo;
        private object _mutex = new object();

        public bool StopOnFirstSatisfiableAssignment { get; set; } = false;

        public SatisfiabilityInfo IsSatisfiable(Formula pFormula)
        {
            _satisfiableInfo = new SatisfiabilityInfo();

            if (pFormula.Count > 0)
            {
                AddBracketsForAnds(pFormula);

                List<Variable> variableList = pFormula.GetVariables();
                _satisfiableInfo.VariableList = variableList;
                BigInteger assignmentCount = BigInteger.Pow(2, variableList.Count);
 
                for (BigInteger i = 0; i < assignmentCount; i++)
                {
                    var variableAssignment = new bool[variableList.Count];
                    for (var k = 0; k < variableList.Count; k++)
                    {
                        var assignment = ((i >> k) & 1) == 1;
                        variableAssignment[k] = assignment;
                    }

                    var tempSatisfiable = EvaluateFormula(pFormula, variableAssignment, variableList);

                    if (tempSatisfiable && variableAssignment.Length > 0)
                    {
                        _satisfiableInfo.SatisfiableAssignments.Add(variableAssignment);
                    }

                    _satisfiableInfo.IsSatisfiable = _satisfiableInfo.IsSatisfiable || tempSatisfiable;

                    if (_satisfiableInfo.IsSatisfiable && StopOnFirstSatisfiableAssignment)
                    {
                        break;
                    }

                    if (_satisfiableInfo.IsSatisfiable && variableAssignment.Count(c => c) == 7)
                    {
                        Console.WriteLine("---------------------------------------------------");
                        for (int z = 0; z < variableList.Count; z++)
                        {
                            Console.WriteLine("{0}:{1}", variableList[z].Name, variableAssignment[z]);
                        }
                        Console.WriteLine("---------------------------------------------------");
                    }
                }
            }
         
            return _satisfiableInfo;
        }

        private bool EvaluateFormula(Formula pFormula, bool[] pVariableAssignment, List<Variable> pVariableList)
        {
            var initializedEvaluation = false;
            var assignmentEvaluation = false;

            Operator currentOperator = null;
            var negationCount = 0;

            foreach(IFormulaComponent comp in pFormula)
            {
                if (comp is Operator tempOperator)
                {
                    if (tempOperator.Type == Operator.Types.Not)
                    {
                        negationCount++;
                    }
                    else
                    {
                        currentOperator = tempOperator;
                    }
                }
                else
                {
                    var tempEvaluation = comp is Bracket bracket
                        ? EvaluateFormula(bracket, pVariableAssignment, pVariableList)
                        : comp is Variable variable
                            ? pVariableAssignment[pVariableList.IndexOf(variable)]
                            : comp is Constant constant && constant.Type == Constant.Types.True;

                    if (negationCount % 2 == 1)
                    {
                        tempEvaluation = !tempEvaluation;
                        negationCount = 0;
                    }

                    if (!initializedEvaluation)
                    {
                        assignmentEvaluation = tempEvaluation;
                        initializedEvaluation = true;
                    }
                    else
                    {
                        if (currentOperator == null)
                        {
                            throw new ArgumentException("Detected invalid pFormula.");
                        }

                        if (currentOperator.Type == Operator.Types.And)
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
        
        private void AddBracketsForAnds(Formula pFormula)
        {
            var lastOrOperator = -1;
            for (var i = 0; i < pFormula.Count; i++)
            {
                var current = pFormula[i];
                if (current is Operator op)
                {
                    if (op.Type == Operator.Types.Or)
                    {
                        lastOrOperator = i;
                    }
                    else if (op.Type == Operator.Types.And)
                    {
                        var nextOrOperator = -1;
                        for (var k = i; k < pFormula.Count; k++)
                        {
                            var nextCurrent = pFormula[k];
                            if (nextCurrent is Operator op2 && op2.Type == Operator.Types.Or)
                            {
                                nextOrOperator = k;
                                break;
                            }
                        }

                        if (lastOrOperator != -1 || nextOrOperator != -1)
                        {
                            var tempBracket = new Bracket(pFormula.GetRange(lastOrOperator + 1,
                                nextOrOperator != -1 ? nextOrOperator : i + 1));
                            pFormula.RemoveRange(lastOrOperator + 1,
                                nextOrOperator != -1 ? nextOrOperator : i + 1);
                            pFormula.Insert(lastOrOperator + 1, tempBracket);

                            i = lastOrOperator + 1;
                            lastOrOperator = -1;
                        }
                    }
                }
                else if (current is Bracket bracket)
                {
                    AddBracketsForAnds(bracket);
                }
            }
        }
    }
}