using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complexitytheory.TuringMaschine;

namespace Teilbar5TuringMaschine
{
    class Teilbar5TuringMaschine
    {
        static void Main(string[] args)
        {
            List<Production> productions = GetProductions();
            TuringMaschine turningMaschine = new TuringMaschine( "qStart", "qHalt", 's', 'e', productions);

            List<char> input = new List<char>();
            input.Add('1');
            input.Add('0');
            input.Add('0');
            input.Add('0');
            input.Add('1');
            input.Add('1');
            input.Add('0');
            input.Add('0');
            input.Add('1');
            input.Add('0');
            input.Add('1');

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

            productions.Add(new Production("qStart", new char[] { 's', 's' }, "r0", new char[] { 's' }, new char[] { 'r', 'r' }));

            productions.Add(new Production("r0", new char[] { '0', 'e' }, "r0", new char[] { 'e' }, new char[] { 'r', 's' }));
            productions.Add(new Production("r0", new char[] { '1', 'e' }, "r1", new char[] { 'e' }, new char[] { 'r', 's' }));

            productions.Add(new Production("r1", new char[] { '0', 'e' }, "r2", new char[] { 'e' }, new char[] { 'r', 's' }));
            productions.Add(new Production("r1", new char[] { '1', 'e' }, "r3", new char[] { 'e' }, new char[] { 'r', 's' }));

            productions.Add(new Production("r2", new char[] { '0', 'e' }, "r4", new char[] { 'e' }, new char[] { 'r', 's' }));
            productions.Add(new Production("r2", new char[] { '1', 'e' }, "r0", new char[] { 'e' }, new char[] { 'r', 's' }));

            productions.Add(new Production("r3", new char[] { '0', 'e' }, "r1", new char[] { 'e' }, new char[] { 'r', 's' }));
            productions.Add(new Production("r3", new char[] { '1', 'e' }, "r2", new char[] { 'e' }, new char[] { 'r', 's' }));

            productions.Add(new Production("r4", new char[] { '0', 'e' }, "r3", new char[] { 'e' }, new char[] { 'r', 's' }));
            productions.Add(new Production("r4", new char[] { '1', 'e' }, "r4", new char[] { 'e' }, new char[] { 'r', 's' }));

            productions.Add(new Production("r0", new char[] { 'e', 'e' }, "qHalt", new char[] { '1' }, new char[] { 's', 's' }));
            productions.Add(new Production("r1", new char[] { 'e', 'e' }, "qHalt", new char[] { '0' }, new char[] { 's', 's' }));
            productions.Add(new Production("r2", new char[] { 'e', 'e' }, "qHalt", new char[] { '0' }, new char[] { 's', 's' }));
            productions.Add(new Production("r3", new char[] { 'e', 'e' }, "qHalt", new char[] { '0' }, new char[] { 's', 's' }));
            productions.Add(new Production("r4", new char[] { 'e', 'e' }, "qHalt", new char[] { '0' }, new char[] { 's', 's' }));

            return productions;
        }
    }
}
