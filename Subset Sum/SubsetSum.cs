using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Complexitytheory.SAT;
using Complexitytheory.SAT.FormulaComponents;
using Complexitytheory.SubsetSum;

namespace SubSetSum
{
    class SubsetSum
    {
        static void Main(string[] args)
        {
            SubSetSumInfo subSetSumExample = new SubSetSumInfo(new double[] { 15, 22, 14, 26, 32, 9, 16, 8 }, 53);
            Console.WriteLine($"Try to find subset sum for {subSetSumExample.TargetSum} with {{ {string.Join(", ", subSetSumExample.Weights)} }}");
            var subsetSumResolver1 = new SubsetSumResolver();
            List<double[]> resolvedSubsets = subsetSumResolver1.ResolveSubsetSum(subSetSumExample);

            foreach (var set in resolvedSubsets)
            {
                Console.WriteLine($"Subset Sum: {string.Join("+", set)} = 53");
            }
            Console.WriteLine($"Nodes generated {subsetSumResolver1.GeneratedNodesCount}");


            Console.WriteLine($"---------------------------------------------------------------------");
            Operator and = new Operator(Operator.Types.And);
            Operator or = new Operator(Operator.Types.Or);
            Operator not = new Operator(Operator.Types.Not);
            Variable x1 = new Variable("x1");
            Variable x2 = new Variable("x2");
            Variable x3 = new Variable("x3");
            Variable x4 = new Variable("x4");

            //var subSetSumReductionInfo = ThreeSatReducer.ReduceToSubsetSum(new Formula()
            //{
            //    new Bracket() {x1, or, x2, or, x3},
            //    and,
            //    new Bracket() {not, x1, or, not, x2, or, x3},
            //    and,
            //    new Bracket() {not, x1, or, x2, or, not, x3},
            //    and,
            //    new Bracket() {x1, or, not, x2, or, x3}
            //});

            //var subSetSumReductionInfo = ThreeSatReducer.ReduceToSubsetSum(new Formula()
            //{
            //    new Bracket() { x1, or, x2, or, not, x3 },
            //    and,
            //    new Bracket() { not, x1, or, not, x2, or, x4 },
            //    and,
            //    new Bracket() { not, x2, or, not, x4 }
            //});

            var subSetSumReductionInfo = ThreeSatReducer.ReduceToSubsetSum(new Formula()
            {
                new Bracket() { x1, or, not, x2 },
                and,
                new Bracket() { x1, or, not, x3 },
            });

            Console.WriteLine($"Try to find subset sum for {subSetSumReductionInfo.SubSetSumInfo.TargetSum} with {{ {string.Join(", ", subSetSumReductionInfo.SubSetSumInfo.Weights)} }}");
            var subsetSumResolver = new SubsetSumResolver();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<double[]> subSets = subsetSumResolver.ResolveSubsetSum(subSetSumReductionInfo.SubSetSumInfo);

            stopwatch.Stop();
            Console.WriteLine($"{Console.Out.NewLine}Time elapsed: {stopwatch.Elapsed}");

            Console.WriteLine();
            Console.WriteLine("Satisfiable Assignments:");
            foreach (var subSet in subSets)
            {
                double minVariableNumber = Math.Pow(10, subSetSumReductionInfo.ClausesCount);
                List<double> numbersFromVariables = subSet.Where(n => n.CompareTo(minVariableNumber) >= 0).ToList();

                Console.WriteLine("--------------------");
                if (numbersFromVariables.Count == subSetSumReductionInfo.VariableCount)
                {
                    foreach (var numberFromVariable in numbersFromVariables)
                    {
                        string[] variableInfo =
                            subSetSumReductionInfo.NumberVariableMapping[numberFromVariable].Split('|');
                        Console.WriteLine(variableInfo[0].Equals("p")
                            ? $"{variableInfo[1]}=true"
                            : $"{variableInfo[1]}=false");
                    }
                }
            }

            Console.ReadLine();
        }
    }
}

