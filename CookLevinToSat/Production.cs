namespace CookLevinToSat
{
    internal class Production
    {
        public string MatchState { get; private set; }
        public char[] MatchSymbols { get; private set; }
        public char[] MoveInfos { get; private set; }
        public string NewState { get; private set; }
        public char[] NewSymbols { get; private set; }

        public Production(string pMatchState, char[] pMatchSymbols, string pNewState, char[] pMoveInfos,
            char[] pNewSymbols)
        {
            this.MatchState = pMatchState;
            this.MatchSymbols = pMatchSymbols;
            this.MoveInfos = pMoveInfos;
            this.NewState = pNewState;
            this.NewSymbols = pNewSymbols;
        }
    }
}