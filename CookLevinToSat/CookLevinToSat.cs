using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Complexitytheory.SAT;
using Complexitytheory.SAT.FormulaComponents;

namespace CookLevinToSat
{
    class CookLevinToSat
    {
        static void Main(string[] args)
        {
            var formula = ReduceTuringMachineToSat(GetPalindromTuringMachineInfo());

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var satisfiableInfo = new SatResolver().IsSatisfiable(formula);

            stopwatch.Stop();
            Console.WriteLine($"{Console.Out.NewLine}Time elapsed: {stopwatch.Elapsed}");

            bool[] maxSatisfiebleAssignment = SatUtil.GetMaxSatisfiebleAssignment(satisfiableInfo.SatisfiableAssignments);
            if (satisfiableInfo.IsSatisfiable && maxSatisfiebleAssignment.Count(a => a) > 0)
            {
                Console.WriteLine("\nFor the turing machine exists a calculation!");
            }
            else
            {
                Console.WriteLine("\nFor the turing machine does not exists a calculation!");
            }
        }

        private static TuringMachineInfo GetPalindromTuringMachineInfo()
        {
            string startState = "qStart";
            string holtState = "qHalt";
            char startSymbol = 's';
            char emptySymbol = 'e';

            List<Production> productions = new List<Production>();
            productions.Add(new Production("qStart", new char[] {'s', 's', 's'}, "q1", new char[] {'s', 's'},
                new char[] {'r', 'r', 'r'}));
            productions.Add(new Production("q1", new char[] {'0', 'e', 'e'}, "q1", new char[] {'0', 'e'},
                new char[] {'r', 'r', 's'}));
            productions.Add(new Production("q1", new char[] {'1', 'e', 'e'}, "q1", new char[] {'1', 'e'},
                new char[] {'r', 'r', 's'}));
            productions.Add(new Production("q1", new char[] {'e', 'e', 'e'}, "q2", new char[] {'e', 'e'},
                new char[] {'s', 'l', 's'}));
            productions.Add(new Production("q2", new char[] {'e', '0', 'e'}, "q2", new char[] {'0', 'e'},
                new char[] {'s', 'l', 's'}));
            productions.Add(new Production("q2", new char[] {'e', '1', 'e'}, "q2", new char[] {'1', 'e'},
                new char[] {'s', 'l', 's'}));
            productions.Add(new Production("q2", new char[] {'e', 's', 'e'}, "q3", new char[] {'s', 'e'},
                new char[] {'l', 'r', 's'}));
            productions.Add(new Production("q3", new char[] {'0', '0', 'e'}, "q3", new char[] {'0', 'e'},
                new char[] {'l', 'r', 's'}));
            productions.Add(new Production("q3", new char[] {'1', '1', 'e'}, "q3", new char[] {'1', 'e'},
                new char[] {'l', 'r', 's'}));
            productions.Add(new Production("q3", new char[] {'1', '0', 'e'}, "qHalt", new char[] {'0', '0'},
                new char[] {'s', 's', 's'}));
            productions.Add(new Production("q3", new char[] {'0', '1', 'e'}, "qHalt", new char[] {'1', '0'},
                new char[] {'s', 's', 's'}));
            productions.Add(new Production("q3", new char[] {'s', 'e', 'e'}, "qHalt", new char[] {'s', '1'},
                new char[] {'s', 's', 's'}));

            return new TuringMachineInfo(startState, holtState, startSymbol, emptySymbol, productions);
        }

        private static Formula ReduceTuringMachineToSat(TuringMachineInfo pTmInfo)
        {
            Operator and = new Operator(Operator.Types.And);

            Formula formula = new Formula
            {
                HeadExactlyInOnePlace(pTmInfo),
                and,
                HeadInTheBeginningAtTheStart(),
                and,
                InTheBeginningStartState(),
                and,
                InitialiseTape(pTmInfo),
                and,
                NotMoreThanOneSymbolPerCell(pTmInfo),
                and,
                AtLeastOneSymbolPerCell(pTmInfo),
                and,
                AlwaysOnlyOneState(pTmInfo),
                and,
                SomeTimeAcceptance(pTmInfo),
                and,
                TransitionFunction(pTmInfo)
            };


            return formula;
        }

        private static Bracket HeadExactlyInOnePlace(TuringMachineInfo pTmInfo)
        {
            Operator not = new Operator(Operator.Types.Not);
            Operator or = new Operator(Operator.Types.Or);

            Bracket formula = new Bracket();

            new Bracket()
            {
                not,
                new Variable("Hi,k"),
                or,
                not,
                new Variable("Hi,k")
            };

            return formula;
        }

        private static Bracket HeadInTheBeginningAtTheStart()
        {
            Bracket formula = new Bracket();

            formula.Add(new Bracket()
            {
                new Variable("H0,0")
            });

            return formula;
        }

        private static Bracket InTheBeginningStartState()
        {
            Bracket formula = new Bracket();
            
            formula.Add(new Bracket()
            {
                new Variable("Q0,0")
            });

            return formula;
        }

        private static Bracket InitialiseTape(TuringMachineInfo pTmInfo)
        {
            Bracket formula = new Bracket();



            return formula;
        }

        private static Bracket NotMoreThanOneSymbolPerCell(TuringMachineInfo pTmInfo)
        {
            Bracket formula = new Bracket();



            return formula;
        }

        private static Bracket AtLeastOneSymbolPerCell(TuringMachineInfo pTmInfo)
        {
            Bracket formula = new Bracket();



            return formula;
        }

        private static Bracket AlwaysOnlyOneState(TuringMachineInfo pTmInfo)
        {
            Bracket formula = new Bracket();



            return formula;
        }

        private static Bracket SomeTimeAcceptance(TuringMachineInfo pTmInfo)
        {
            Bracket formula = new Bracket();



            return formula;
        }

        private static Bracket TransitionFunction(TuringMachineInfo pTmInfo)
        {
            Bracket formula = new Bracket();



            return formula;
        }
    }
}
