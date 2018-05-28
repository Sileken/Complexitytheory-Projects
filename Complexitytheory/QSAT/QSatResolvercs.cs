 using System;
using System.Collections.Generic;
using System.Linq;
using Complexitytheory.QSat;
using Complexitytheory.SAT;
using Complexitytheory.SAT.FormulaComponents;

namespace Complexitytheory.QSAT
{
    public class QSatResolvercs
    {
        public static bool CheckQSat(List<Quantor> pQuantors, Formula pFormular)
        {
            bool qSatCheckResult = false;

            if (pQuantors != null && pQuantors.Count != 0)
            {
                Quantor currentQuantor = pQuantors[0];
                List<Quantor> remainingQunators = pQuantors.ToList();
                remainingQunators.RemoveAt(0);

                Formula one = ReplaceVarialbe(pFormular, currentQuantor.Variable, true);
                Formula zero = ReplaceVarialbe(pFormular, currentQuantor.Variable, false);

                if (currentQuantor.Type == Quantor.QuantorTypes.AllQuantor)
                {
                    qSatCheckResult = CheckQSat(remainingQunators, one) && CheckQSat(remainingQunators, zero);
                }
                else
                {
                    qSatCheckResult = CheckQSat(remainingQunators, one) || CheckQSat(remainingQunators, zero);
                }
            }
            else
            {
                SatResolver satResolver = new SatResolver();
                SatisfiabilityInfo satisfiableInfo = satResolver.IsSatisfiable(pFormular);
                qSatCheckResult = satisfiableInfo.IsSatisfiable;
            }

            return qSatCheckResult;
        }

        public static Formula ReplaceVarialbe(Formula pFormular, Variable pVariable, bool pConstantValue)
        {
            var copyFormula = new Formula(pFormular.ToList());

            Constant constant = new Constant(pConstantValue ? Constant.Types.True : Constant.Types.False);

            for (int i = 0; i < copyFormula.Count; i++)
            {
                var curObj = copyFormula[i];
                if (curObj is Variable variable && variable.Name.Equals(pVariable.Name))
                {
                    copyFormula[i] = constant;
                }

                if (curObj is Bracket bracket)
                {
                    copyFormula[i] = new Bracket(ReplaceVarialbe(bracket, pVariable, pConstantValue));
                }
            }

            return copyFormula;
        }
    }
}