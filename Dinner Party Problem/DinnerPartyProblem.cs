using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Complexitytheory.SAT;
using Complexitytheory.SAT.FormulaComponents;

namespace DinnerPartyProblem
{
    class DinnerPartyProblem
    {
        static void Main(string[] args)
        {
            Variable arya = new Variable("Arya");
            Variable brandon = new Variable("Brandon");
            Variable cersei = new Variable("Cersei");
            Variable dany = new Variable("Dany");
            Variable eddard = new Variable("Eddard");
            Variable gilly = new Variable("Gilly");
            Variable jon = new Variable("Jon");
            Variable melisandre = new Variable("Melisandre");
            Variable rob = new Variable("Rob");
            Variable sansa = new Variable("Sansa");

            Operator and = new Operator(Operator.Types.And);
            Operator or = new Operator(Operator.Types.Or);
            Operator not = new Operator(Operator.Types.Not);

            var dinnerPartyProblem = new Formula()
            {
                new Bracket() { not, arya, or, not, brandon},
                and,
                new Bracket() { not, arya, or, not, cersei},
                and,
                new Bracket() { not, arya, or, not, gilly},
                and,

                new Bracket() { not, brandon, or, not, arya},
                and,
                new Bracket() { not, brandon, or, not, jon},
                and,
                new Bracket() { not, brandon, or, not, melisandre},
                and,

                new Bracket() { not, cersei, or, not, arya},
                and,
                new Bracket() { not, cersei, or, not, dany},
                and,
                new Bracket() { not, cersei, or, not, rob},
                and,

                new Bracket() { not, dany, or, not, cersei},
                and,
                new Bracket() { not, dany, or, not, eddard},
                and,
                new Bracket() { not, dany, or, not, melisandre},
                and,

                new Bracket() { not, eddard, or, not, dany},
                and,
                new Bracket() { not, eddard, or, not, gilly},
                and,
                new Bracket() { not, eddard, or, not, jon},
                and,

                new Bracket() { not, gilly, or, not, arya},
                and,
                new Bracket() { not, gilly, or, not, eddard},
                and,
                new Bracket() { not, gilly, or, not, sansa},
                and,
            
                new Bracket() { not, jon, or, not, brandon},
                and,
                new Bracket() { not, jon, or, not, eddard},
                and,
                new Bracket() { not, jon, or, not, rob},
                and,

                new Bracket() { not, melisandre, or, not, brandon},
                and,
                new Bracket() { not, melisandre, or, not, dany},
                and,
                new Bracket() { not, melisandre, or, not, sansa},
                and,

                new Bracket() { not, rob, or, not, cersei},
                and,
                new Bracket() { not, rob, or, not, jon},
                and,
                new Bracket() { not, rob, or, not, sansa},
                and,

                new Bracket() { not, sansa, or, not, gilly},
                and,
                new Bracket() { not, sansa, or, not, melisandre},
                and,
                new Bracket() { not, sansa, or, not, rob}
            };
            
            var satResolver = new SatResolver();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var satisfiableInfo = satResolver.IsSatisfiable(dinnerPartyProblem);

            stopwatch.Stop();
            Console.WriteLine($"{Console.Out.NewLine}Time elapsed: {stopwatch.Elapsed}");

            bool[] maxSatisfiebleAssignment = SatUtil.GetMaxSatisfiebleAssignment(satisfiableInfo.SatisfiableAssignments);
            if (satisfiableInfo.IsSatisfiable && maxSatisfiebleAssignment.Count(a => a) > 0)
            {
                Console.WriteLine("\nThe dinner party problem is satisfiable!");
                for (int i = 0; i < satisfiableInfo.VariableList.Count; i++)
                {
                    Console.WriteLine("{0}:{1}", satisfiableInfo.VariableList[i].Name, maxSatisfiebleAssignment[i]);
                }
            }
            else
            {
                Console.WriteLine("\nThe dinner party problem is not satisfiable!");
            }

            Console.ReadLine();
        }
    }
}
