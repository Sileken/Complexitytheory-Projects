using System;
using System.Collections.Generic;
using System.Linq;
using Complexitytheory.SAT.FormulaComponents;

namespace Complexitytheory.SAT
{
    public class SatUtil
    {
        public static bool[] GetMaxSatisfiebleAssignment(List<bool[]> pSatisfiableAssignments)
        {
            return pSatisfiableAssignments.OrderByDescending(assignment => assignment.Count(constant => constant))
                .First();
        }

        public static bool[] GetMinSatisfiebleAssignment(List<bool[]> pSatisfiableAssignments)
        {
            return pSatisfiableAssignments.OrderByDescending(assignment => assignment.Count(constant => constant))
                .Last();
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
