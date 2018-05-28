using System;
using System.Collections.Generic;
using Complexitytheory.TuringMaschine;

namespace PalindromTuringMaschine
{
    class PalindromTuringMaschine
    {
        static void Main(string[] args)
        {
            List<Production> productions = GetProductions();
            TuringMaschine turningMaschine = new TuringMaschine("qStart", "qHalt", 's', 'e', productions);

            List<char> input = new List<char>();
            input.Add('1');
            input.Add('0');
            input.Add('1');
            input.Add('0');
            input.Add('1');

            List<char> output = turningMaschine.ProcessInput(input);
            String outString = "Result: ";
            foreach (char outputChar in output)
            {
                outString += outputChar;
            }
            Console.WriteLine(outString);
            Console.ReadLine();
        }

        private static List<Production> GetProductions()
        {
            List<Production> productions = new List<Production>();

            productions.Add(new Production("qStart", new char[] { 's', 's', 's' }, "q1", new char[] { 's', 's' }, new char[] { 'r', 'r', 'r' }));
            productions.Add(new Production("q1", new char[] { '0', 'e', 'e' }, "q1", new char[] { '0', 'e' }, new char[] { 'r', 'r', 's' }));
            productions.Add(new Production("q1", new char[] { '1', 'e', 'e' }, "q1", new char[] { '1', 'e' }, new char[] { 'r', 'r', 's' }));
            productions.Add(new Production("q1", new char[] { 'e', 'e', 'e' }, "q2", new char[] { 'e', 'e' }, new char[] { 's', 'l', 's' }));
            productions.Add(new Production("q2", new char[] { 'e', '0', 'e' }, "q2", new char[] { '0', 'e' }, new char[] { 's', 'l', 's' }));
            productions.Add(new Production("q2", new char[] { 'e', '1', 'e' }, "q2", new char[] { '1', 'e' }, new char[] { 's', 'l', 's' }));
            productions.Add(new Production("q2", new char[] { 'e', 's', 'e' }, "q3", new char[] { 's', 'e' }, new char[] { 'l', 'r', 's' }));
            productions.Add(new Production("q3", new char[] { '0', '0', 'e' }, "q3", new char[] { '0', 'e' }, new char[] { 'l', 'r', 's' }));
            productions.Add(new Production("q3", new char[] { '1', '1', 'e' }, "q3", new char[] { '1', 'e' }, new char[] { 'l', 'r', 's' }));
            productions.Add(new Production("q3", new char[] { '1', '0', 'e' }, "qHalt", new char[] { '0', '0' }, new char[] { 's', 's', 's' }));
            productions.Add(new Production("q3", new char[] { '0', '1', 'e' }, "qHalt", new char[] { '1', '0' }, new char[] { 's', 's', 's' }));
            productions.Add(new Production("q3", new char[] { 's', 'e', 'e' }, "qHalt", new char[] { 's', '1' }, new char[] { 's', 's', 's' }));

            return productions;
        }
    }
}
