using System;

namespace Complexitytheory.TuringMaschine
{
    public class Production
    {
        public string MatchState { get; private set; }
        public char[] MatchSymbols { get; private set; }

        public string NewState { get; private set; }
        public char[] NewSymbols { get; private set; }
        public char[] MoveInfos { get; private set; }

        public Production(String matchState, char[] pMatchSymbols, String pNewState, char[] pNewSymbols,
            char[] pMoveInfos)
        {
            this.MatchState = matchState;
            this.MatchSymbols = pMatchSymbols;
            this.NewState = pNewState;
            this.NewSymbols = pNewSymbols;
            this.MoveInfos = pMoveInfos;
        }
    }
}