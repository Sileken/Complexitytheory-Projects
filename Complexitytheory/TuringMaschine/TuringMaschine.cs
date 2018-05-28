using System;
using System.Collections.Generic;
using System.Linq;

namespace Complexitytheory.TuringMaschine
{
    public class TuringMaschine
    {
        private readonly string _haltState;
        private readonly char _emptySymbol;

        private string _currentState;
        private readonly List<char>[] _tapes;
        private readonly int[] _tapesPosition;
        private readonly List<Production> _productions;

        public TuringMaschine(string pStartState, string pHaltState, char pStartSymbol, char pEmptySymbol, List<Production> pProductions)
        {
            Console.WriteLine("Initialize Turing Machine.");

            if (pProductions.Count == 0)
            {
                throw new ArgumentException("Productions can't be empty!");
            }

            if (pProductions[0].MatchSymbols.Length < 2)
            {
                throw new ArgumentException("Tape count is to less. Tape count should be greater or equals 2.");
            }

            Console.WriteLine($"Start State: {pStartState}");
            Console.WriteLine($"End State: {pHaltState}");
            Console.WriteLine($"Start Symbol: {pStartSymbol}");
            Console.WriteLine($"Empty Symbol: {pEmptySymbol}");

            this._currentState = pStartState;
            Console.WriteLine($"Start State: {this._currentState}");

            int symbolCount = pProductions[0].MatchSymbols.Length;
            _tapes = new List<char>[symbolCount];
            _tapesPosition = new int[symbolCount];

            Console.WriteLine();
            Console.WriteLine("Initialze Tapes ...");
            for (int i = 0; i < _tapes.Length; i++)
            {
                Console.WriteLine($"Initialze Tapes {i}");
                List<char> tape = new List<char> {pStartSymbol};
                _tapes[i] = tape;
                _tapesPosition[i] = 0;
            }

            this._haltState = pHaltState;
            this._emptySymbol = pEmptySymbol;
            this._productions = pProductions;
        }

        public List<char> ProcessInput(List<char> pInput)
        {
            InitializeInputTape(pInput);

            while (!_currentState.Equals(_haltState))
            {
                Production matchedProduction = GetMatchedProduction();
                _currentState = matchedProduction.NewState;
                //System.out.println("Current State: " + currentState);
                ReplaceSymbols(matchedProduction);
                MoveTapesCursors(matchedProduction);
            }

            return BuildOutput();
        }

        private void InitializeInputTape(List<char> pInput)
        {
            String inputString = "";
            foreach (char character in pInput)
            {
                inputString += character;
            }

            Console.WriteLine($"Process Input: {inputString}");

            _tapes[0].AddRange(pInput);
        }

        private Production GetMatchedProduction()
        {
            Production matchedProduction = _productions.Where(p =>
            {
                bool match = p.MatchState.Equals(_currentState);
                if (match == true)
                {
                    char[] matchsymbols = p.MatchSymbols;
                    for (int tapeIndex = 0; tapeIndex < matchsymbols.Length; tapeIndex++)
                    {
                        char matchSymbol = matchsymbols[tapeIndex];

                        char tapeSymbol = _emptySymbol;
                        try
                        {
                            tapeSymbol = _tapes[tapeIndex][_tapesPosition[tapeIndex]];
                        }
                        catch (Exception ex)
                        {
                        }

                        match &= matchSymbol == tapeSymbol;
                        if (match == false)
                        {
                            break;
                        }
                    }
                }

                return match;
            }).First();

            return matchedProduction;
        }

        private void ReplaceSymbols(Production pProduction)
        {
            char[] newSymbols = pProduction.NewSymbols;
            for (int i = 1; i < pProduction.MatchSymbols.Length; i++)
            {
                List<char> tape = _tapes[i];
                int tapePositioning = _tapesPosition[i];
                if (tape.Count > tapePositioning)
                {
                    tape[tapePositioning] = newSymbols[i - 1];
                }
                else
                {
                    tape.Insert(tapePositioning, newSymbols[i - 1]);
                }
            }
        }

        private void MoveTapesCursors(Production pProduction)
        {
            char[] moveInfos = pProduction.MoveInfos;
            for (int i = 0; i < moveInfos.Length; i++)
            {
                int currentPosition = _tapesPosition[i];
                char moveInfo = moveInfos[i];
                if (currentPosition == 0 && moveInfo == 'l')
                {
                    throw new IndexOutOfRangeException("Invalid tape position.");
                }
                else if (moveInfo == 'l')
                {
                    currentPosition--;
                }
                else if (moveInfo == 'r')
                {
                    currentPosition++;
                }

                _tapesPosition[i] = currentPosition;
            }
        }

        private List<char> BuildOutput()
        {
            List<char> output = new List<char>();

            List<char> outputTape = _tapes[_tapes.Length - 1];
            for (int i = 1; i < outputTape.Count; i++)
            {
                char symbol = outputTape[i];
                if (symbol.Equals(_emptySymbol))
                {
                    break;
                }

                output.Add(symbol);
            }

            return output;
        }
    }
}
