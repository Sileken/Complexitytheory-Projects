using System.Collections.Generic;
using System.Linq;

namespace CookLevinToSat
{
    class TuringMachineInfo
    {
        public List<char> Alphabet { get; private set; }

        public char StartSymbol { get; private set; }

        public char EmptySymbol { get; private set; }

        public int Tapes { get; private set; }

        public List<string> States { get; private set; }

        public string StartState { get; private  set; }

        public string HaltState { get; private set; }

        public List<Production> Productions { get; private set; }

        public TuringMachineInfo(string pStartState, string pHaltState, char pStartSymbol, char pEmptySymbol, List<Production> pProductions)
        {
            this.StartSymbol = pStartSymbol;
            this.EmptySymbol = pEmptySymbol;
            this.Productions = pProductions;
            this.Tapes = Productions.First().MatchState.Length;
            this.StartState = pStartState;
            this.HaltState = pHaltState;

            Alphabet = new List<char>();
            States = new List<string>();

            foreach (Production production in Productions)
            {
                if (!States.Contains(production.MatchState))
                {
                    States.Add(production.MatchState);
                }

                if (!States.Contains(production.NewState))
                {
                    States.Add(production.NewState);
                }

                foreach (var symbol in production.MatchSymbols)
                {
                    if (!Alphabet.Contains(symbol))
                    {
                        Alphabet.Add(symbol);
                    }
                }

                foreach (var symbol in production.NewSymbols)
                {
                    if (!Alphabet.Contains(symbol))
                    {
                        Alphabet.Add(symbol);
                    }
                }
            }
        }
    }
}
