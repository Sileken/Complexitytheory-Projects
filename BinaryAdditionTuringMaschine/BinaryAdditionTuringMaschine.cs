using System;
using System.Collections.Generic;
using Complexitytheory.TuringMaschine;

namespace BinaryAdditionTuringMaschine
{
    class BinaryAdditionTuringMaschine
    {
        static void Main(string[] args)
        {
            List<Production> productions = GetProductions();
            TuringMaschine turningMaschine = new TuringMaschine("qStart", "qHalt", 's', 'e', productions);

            // Beide Zahlen müssen gleiche viele stellen haben.
            List<char> input = new List<char>();
            input.Add('1');
            input.Add('0');
            input.Add('1');
            input.Add('0');
            input.Add('+');
            input.Add('1');
            input.Add('0');
            input.Add('1');
            input.Add('0');

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

            productions.Add(new Production("qStart", new char[] { 's', 's', 's', 's', 's' }, "q1", new char[] { 's', 's', 's', 's' }, new char[] { 'r', 'r', 'r', 'r', 'r' }));
            productions.Add(new Production("q1", new char[] { '0', 'e', 'e', 'e', 'e' }, "q1", new char[] { 'e', 'e', 'e', 'e' }, new char[] { 'r', 's', 's', 's', 's' }));
            productions.Add(new Production("q1", new char[] { '1', 'e', 'e', 'e', 'e' }, "q1", new char[] { 'e', 'e', 'e', 'e' }, new char[] { 'r', 's', 's', 's', 's' }));
            productions.Add(new Production("q1", new char[] { '+', 'e', 'e', 'e', 'e' }, "q2", new char[] { 'e', 'e', 'e', 'e' }, new char[] { 'r', 's', 's', 's', 's' }));

            productions.Add(new Production("q2", new char[] { '0', 'e', 'e', 'e', 'e' }, "q2", new char[] { '0', 'e', 'e', 'e' }, new char[] { 'r', 'r', 's', 's', 's' }));
            productions.Add(new Production("q2", new char[] { '1', 'e', 'e', 'e', 'e' }, "q2", new char[] { '1', 'e', 'e', 'e' }, new char[] { 'r', 'r', 's', 's', 's' }));
            productions.Add(new Production("q2", new char[] { 'e', 'e', 'e', 'e', 'e' }, "q3", new char[] { 'e', 'e', 'e', 'e' }, new char[] { 'l', 'l', 's', 's', 's' }));

            productions.Add(new Production("q3", new char[] { '0', '0', 'e', 'e', 'e' }, "q3", new char[] { '0', 'e', 'e', 'e' }, new char[] { 'l', 's', 's', 's', 's' }));
            productions.Add(new Production("q3", new char[] { '0', '1', 'e', 'e', 'e' }, "q3", new char[] { '1', 'e', 'e', 'e' }, new char[] { 'l', 's', 's', 's', 's' }));
            productions.Add(new Production("q3", new char[] { '1', '1', 'e', 'e', 'e' }, "q3", new char[] { '1', 'e', 'e', 'e' }, new char[] { 'l', 's', 's', 's', 's' }));
            productions.Add(new Production("q3", new char[] { '1', '0', 'e', 'e', 'e' }, "q3", new char[] { '0', 'e', 'e', 'e' }, new char[] { 'l', 's', 's', 's', 's' }));
            productions.Add(new Production("q3", new char[] { '+', '0', 'e', 'e', 'e' }, "q4", new char[] { '0', '0', 'e', 'e' }, new char[] { 'l', 's', 's', 's', 's' }));
            productions.Add(new Production("q3", new char[] { '+', '1', 'e', 'e', 'e' }, "q4", new char[] { '1', '0', 'e', 'e' }, new char[] { 'l', 's', 's', 's', 's' }));

            productions.Add(new Production("q4", new char[] { '0', '0', '0', 'e', 'e' }, "q4", new char[] { '0', '0', '0', 'e' }, new char[] { 'l', 'l', 's', 'r', 's' }));
            productions.Add(new Production("q4", new char[] { '1', '0', '0', 'e', 'e' }, "q4", new char[] { '0', '0', '1', 'e' }, new char[] { 'l', 'l', 's', 'r', 's' }));
            productions.Add(new Production("q4", new char[] { '1', '1', '0', 'e', 'e' }, "q4", new char[] { '1', '1', '0', 'e' }, new char[] { 'l', 'l', 's', 'r', 's' }));
            productions.Add(new Production("q4", new char[] { '1', '1', '1', 'e', 'e' }, "q4", new char[] { '1', '1', '1', 'e' }, new char[] { 'l', 'l', 's', 'r', 's' }));
            productions.Add(new Production("q4", new char[] { '0', '0', '1', 'e', 'e' }, "q4", new char[] { '0', '0', '1', 'e' }, new char[] { 'l', 'l', 's', 'r', 's' }));
            productions.Add(new Production("q4", new char[] { '0', '1', '0', 'e', 'e' }, "q4", new char[] { '1', '0', '1', 'e' }, new char[] { 'l', 'l', 's', 'r', 's' }));
            productions.Add(new Production("q4", new char[] { '0', '1', '1', 'e', 'e' }, "q4", new char[] { '1', '1', '0', 'e' }, new char[] { 'l', 'l', 's', 'r', 's' }));
            productions.Add(new Production("q4", new char[] { '1', '0', '1', 'e', 'e' }, "q4", new char[] { '0', '1', '0', 'e' }, new char[] { 'l', 'l', 's', 'r', 's' }));

            productions.Add(new Production("q4", new char[] { 's', 's', '1', 'e', 'e' }, "q5", new char[] { 's', 's', '1', 'e' }, new char[] { 's', 's', 's', 's', 's' }));
            productions.Add(new Production("q4", new char[] { 's', 's', '0', 'e', 'e' }, "q5", new char[] { 's', 's', '0', 'e' }, new char[] { 's', 's', 'l', 's', 's' }));

            productions.Add(new Production("q5", new char[] { 's', 's', 's', '0', 'e' }, "q5", new char[] { 's', 's', '0', '0' }, new char[] { 's', 's', 's', 'l', 'r' }));
            productions.Add(new Production("q5", new char[] { 's', 's', 's', '1', 'e' }, "q5", new char[] { 's', 's', '1', '1' }, new char[] { 's', 's', 's', 'l', 'r' }));
            productions.Add(new Production("q5", new char[] { 's', 's', 's', 's', 'e' }, "qHalt", new char[] { 's', 's', 's', 'e' }, new char[] { 's', 's', 's', 's', 'l' }));

            return productions;
        }
    }
}
