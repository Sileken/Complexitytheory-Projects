using System;
using System.Collections.Generic;
using Complexitytheory.QSat;
using Complexitytheory.QSAT;
using Complexitytheory.SAT;
using Complexitytheory.SAT.FormulaComponents;

namespace QSat
{
    class QSat
    {
        static void Main(string[] args)
        {
            Operator and = new Operator(Operator.Types.And);
            Operator or = new Operator(Operator.Types.Or);
            Operator not = new Operator(Operator.Types.Not);

            var x = new Variable("x");
            var y = new Variable("y");

            Formula xor = new Formula()
            {
                new Bracket() {x, and, not, y},
                or,
                new Bracket() {not, x, and, y},
            };

            var quantorsf1 = new List<Quantor>()
            {
                new Quantor(Quantor.QuantorTypes.AllQuantor, x),
                new Quantor(Quantor.QuantorTypes.ExistenceQuantor, y)
            };

            var quantorsf2 = new List<Quantor>()
            {
                new Quantor(Quantor.QuantorTypes.ExistenceQuantor, x),
                new Quantor(Quantor.QuantorTypes.AllQuantor, y)
            };

            bool f1Result = QSatResolvercs.CheckQSat(quantorsf1, xor);
            Console.WriteLine("Check exists x.forall y. x xor y: " + f1Result);

            bool f2Result = QSatResolvercs.CheckQSat(quantorsf2, xor);
            Console.WriteLine("Check exists x.forall y. x xor y: " + f2Result);
            Console.ReadLine();
        }
    }
}
