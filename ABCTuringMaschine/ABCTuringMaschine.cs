using System;
using System.Collections.Generic;
using Complexitytheory.TuringMaschine;

namespace ABCTuringMaschine
{
    class ABCTuringMaschine
    {
        static void Main(string[] args)
        {
            List<Production> productions = GetProductions();
            TuringMaschine turningMaschine = new TuringMaschine("qStart", "qHalt", 's', 'e', productions);

            List<char> input = new List<char>();
            input.Add('a');
            input.Add('a');
            input.Add('a');
            input.Add('a');
            input.Add('a');
            input.Add('b');
            input.Add('b');
            input.Add('b');
            input.Add('b');
            input.Add('b');
            input.Add('c');
            input.Add('c');
            input.Add('c');
            input.Add('c');
            input.Add('c');

            List<char> output = turningMaschine.ProcessInput(input);
            String outString = "Result: ";
            foreach (Char outputChar in output)
            {
                outString += outputChar;
            }
            Console.WriteLine(outString);
            Console.ReadLine();
        }

        private static List<Production> GetProductions()
        {
            List<Production> productions = new List<Production>();

            productions.Add(new Production("qStart", new char[] { 's', 's', 's', 's', 's' }, "q1", new char[] { 's', 's', 's', 's' }, new char[] { 'r', 'r', 'r', 'r', 'r' }));

            productions.Add(new Production("q1", new char[] { 'a', 'e', 'e', 'e', 'e' }, "q1", new char[] { 'a', 'e', 'e', 'e' }, new char[] { 'r', 'r', 's', 's', 's' }));
            productions.Add(new Production("q1", new char[] { 'b', 'e', 'e', 'e', 'e' }, "q1", new char[] { 'e', 'b', 'e', 'e' }, new char[] { 'r', 's', 'r', 's', 's' }));
            productions.Add(new Production("q1", new char[] { 'c', 'e', 'e', 'e', 'e' }, "q1", new char[] { 'e', 'e', 'c', 'e' }, new char[] { 'r', 's', 's', 'r', 's' }));
            productions.Add(new Production("q1", new char[] { 'e', 'e', 'e', 'e', 'e' }, "q2", new char[] { 'e', 'e', 'e', 'e' }, new char[] { 's', 'l', 'l', 'l', 's' }));

            productions.Add(new Production("q2", new char[] { 'e', 'a', 'b', 'c', 'e' }, "q2", new char[] { 'a', 'b', 'c', 'e' }, new char[] { 's', 'l', 'l', 'l', 's' }));
            productions.Add(new Production("q2", new char[] { 'e', 's', 's', 's', 'e' }, "qHalt", new char[] { 's', 's', 's', '1' }, new char[] { 's', 's', 's', 's', 's' }));

            productions.Add(new Production("q2", new char[] { 'e', 'a', 's', 's', 'e' }, "qHalt", new char[] { 's', 's', 's', '0' }, new char[] { 's', 's', 's', 's', 's' }));
            productions.Add(new Production("q2", new char[] { 'e', 's', 'b', 's', 'e' }, "qHalt", new char[] { 's', 's', 's', '0' }, new char[] { 's', 's', 's', 's', 's' }));
            productions.Add(new Production("q2", new char[] { 'e', 's', 's', 'c', 'e' }, "qHalt", new char[] { 's', 's', 's', '0' }, new char[] { 's', 's', 's', 's', 's' }));
            productions.Add(new Production("q2", new char[] { 'e', 'a', 'b', 's', 'e' }, "qHalt", new char[] { 's', 's', 's', '0' }, new char[] { 's', 's', 's', 's', 's' }));
            productions.Add(new Production("q2", new char[] { 'e', 'a', 's', 'c', 'e' }, "qHalt", new char[] { 's', 's', 's', '0' }, new char[] { 's', 's', 's', 's', 's' }));
            productions.Add(new Production("q2", new char[] { 'e', 's', 'b', 'c', 'e' }, "qHalt", new char[] { 's', 's', 's', '0' }, new char[] { 's', 's', 's', 's', 's' }));

            return productions;
        }
    }
}
