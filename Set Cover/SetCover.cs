using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complexitytheory.SetCover;

namespace Set_Cover
{
    class SetCover
    {
        static void Main(string[] args)
        {
            List<string> set = new List<string>(){ "1", "2", "3", "4", "5", "6", "7", "8", "9"};
            List<List<string>> family = new List<List<string>>()
            {
                new List<string>() {"1", "2", "5", "6", "8", "9"},
                new List<string>() {"1", "3", "6", "8"},
                new List<string>() {"3", "4", "5", "6", "7"},
               // new List<string>() {"3", "4", "7"}
                //new List<string>(){ "1", "2", "3", "4", "5", "6", "7", "8", "9"}
            };

            var setCoverResolver = new SetCoverResolver();
            var approximatedSetsWithGreedy = setCoverResolver.ApproximateWithGreedy(set, family);
            
            string output = String.Empty;

            foreach (var subset in approximatedSetsWithGreedy)
            {
                if (!string.IsNullOrEmpty(output))
                {
                    output += ", ";
                }

                output += "{ " + string.Join(", ", subset) + " }";
            }

            output = "{ " + output + " }";

            Console.WriteLine($"Found smallest Set Cover with a greedy algo. : {output}");

            Console.ReadLine();
        }
    }
}
