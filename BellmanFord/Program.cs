using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellmanFord
{
    class Program
    {
        private static List<string> vertices = new List<string>() { "S", "A", "B", "C", "D", "E" };

        static Dictionary<string, int> memo = new Dictionary<string, int>()
            {
                { "S", 0 },
                {"A", int.MaxValue },
                {"B", int.MaxValue },
                {"C", int.MaxValue },
                {"D", int.MaxValue },
                {"E", int.MaxValue },
            };

        static List<Path> graph = new List<Path>()
            {
                // Values given in original JavaScript version in book are wrong!
                new Path("S", "A", 4),
                new Path("S", "E", -5),
                new Path("A", "C", 6),
                new Path("B", "A", 3),
                new Path("C", "B", -2),
                new Path("D", "A", 10),
                new Path("D", "C", 3),
                new Path("E", "D", 8),
            };

        static void Main(string[] args)
        {
            // Implementation of Bellman Ford algorithm based on Rob Conery's "Imposters Handbook"
            // This is my conversion of JavaScript original in book

            foreach (string vertex in vertices)
            {
                if (!Iterate())
                    break;
            }

            foreach (KeyValuePair<string, int> memoItem in memo)
            {
                Console.WriteLine("{0} = {1}", memoItem.Key, memoItem.Value.ToString());
            }

            Console.ReadLine();

        }

        private static bool Iterate()
        {
            // Do we need another iteration? Decided below
            bool doItAgain = false;

            // Loop all vertices
            foreach (string fromVertex in vertices)
            {
                Path[] edges = graph.Where(x => x.From == fromVertex).ToArray();

                foreach (Path edge in edges)
                {
                    // If from is maxvalue, it's wrapping around, so handle that!
                    int potentialCost = memo[edge.From];
                    if (potentialCost != int.MaxValue)
                        potentialCost += edge.Cost;

                    if (potentialCost < memo[edge.To])
                    {
                        memo[edge.To] = potentialCost;
                        doItAgain = true;
                    }
                }
            }

            return doItAgain;
        }
    }
}
