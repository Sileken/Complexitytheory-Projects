using System;
using System.Collections.Generic;
using Complexitytheory.SAT;
using Complexitytheory.SAT.FormulaComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SatResolverTest
{
    [TestClass]
    public class SatResolverTest
    {
        private Variable _x1 = new Variable("x1");
        private Variable _x2 = new Variable("x2");
        private Variable _x3 = new Variable("x3");
        private Operator _and = new Operator(Operator.Types.And);
        private Operator _or = new Operator(Operator.Types.Or);
        private Operator _not = new Operator(Operator.Types.Not);

        [TestMethod]
        public void ConstantsTests()
        {
            var constantTests = new List<FormulaTestRow>
            {
                new FormulaTestRow
                {
                    ExpectedSatisfiability = false,
                    Formula = new Formula
                    {
                        new Constant(Constant.Types.False)
                    }
                },
                new FormulaTestRow
                {
                    ExpectedSatisfiability = true,
                    Formula = new Formula
                    {
                        new Constant(Constant.Types.True)
                    }
                },
                new FormulaTestRow
                {
                    ExpectedSatisfiability = false,
                    Formula = new Formula
                    {
                        _not,
                        new Constant(Constant.Types.True)
                    }
                },
                new FormulaTestRow
                {
                    ExpectedSatisfiability = false,
                    Formula = new Formula
                    {
                        _not,
                        _not,
                        _not,
                        new Constant(Constant.Types.True)
                    }
                },
                new FormulaTestRow
                {
                    ExpectedSatisfiability = true,
                    Formula = new Formula
                    {
                        _not,
                        _not,
                        new Constant(Constant.Types.True)
                    }
                },
                new FormulaTestRow
                {
                    ExpectedSatisfiability = true,
                    Formula = new Formula
                    {
                        _not,
                        _not,
                        _not,
                        _not,
                        new Constant(Constant.Types.True)
                    }
                }
            };

            foreach (var testCase in constantTests)
            {
                TestCase(testCase.Formula, testCase.ExpectedSatisfiability);
            }
        }

        [TestMethod]
        public void OtherTests()
        {
            var otherTests = new List<FormulaTestRow>
            {
                new FormulaTestRow
                {
                    ExpectedSatisfiability = true,
                    Formula = new Formula {_x1, _and, _x2}
                },
                new FormulaTestRow
                {
                    ExpectedSatisfiability = true,
                    Formula = new Formula
                    {
                        new Bracket() {_x1, _and, _x2},
                        _or,
                        new Bracket() {_x1, _and, _not, _x1},
                        _or,
                        _x2
                    }
                },
                new FormulaTestRow
                {
                    ExpectedSatisfiability = true,
                    Formula = new Formula
                    {
                        new Bracket() {_x1, _or, _x3},
                        _and,
                        new Bracket() {_x1, _and, new Bracket() {_x2, _or, _not, _x3}},
                        _and,
                        new Bracket() {_not, _x2, _or, _not, _x3},
                        _and,
                        new Bracket() {_not, _x1, _or, _not, _x2}
                    }
                },
                new FormulaTestRow
                {
                    ExpectedSatisfiability = false,
                    Formula = new Formula {_x1, _and, _not, _x1}
                },
                new FormulaTestRow
                {
                    ExpectedSatisfiability = false,
                    Formula = new Formula {_x2, _and, _not, _x2, _or, _x1, _and, _not, _x1}
                },
                new FormulaTestRow
                {
                    ExpectedSatisfiability = true,
                    Formula = new Formula
                    {
                        _x2,
                        _and,
                        _not,
                        _x2,
                        _or,
                        new Bracket() {_x1, _and, _not, _x1, _or, _x3}
                    }
                },
                new FormulaTestRow
                {
                    ExpectedSatisfiability = true,
                    Formula = new Formula
                    {
                        _x1,
                        _or,
                        _x2
                    }
                }
            };

            foreach (var testCase in otherTests)
            {
                TestCase(testCase.Formula, testCase.ExpectedSatisfiability);
            }
        }
        
        private void TestCase(Formula formula, bool pExpectedSatisfiability)
        {
            var satisfiabilityInfo = (new SatResolver()).IsSatisfiable(formula);
            Console.WriteLine(
                $"{Console.Out.NewLine}The formular is satisfiable: {satisfiabilityInfo.IsSatisfiable} and expected was {pExpectedSatisfiability}.");
            Assert.AreEqual(pExpectedSatisfiability, satisfiabilityInfo.IsSatisfiable);
        }
    }
}