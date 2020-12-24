using System;
using System.Collections.Generic;

namespace Graphs
{
    class GraphTester
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------------\n");

            Console.WriteLine("Graph 1\n");
            Graph graph = new Graph(9);
            (int, int)[] edges = new[] 
            { 
                (0,1), 
                (0, 2), 
                (0, 4),
                (1, 2),
                (4, 6),

                (5, 3),
                (5, 7)
            };
            GraphOutput(graph, edges);
            Console.WriteLine("\n------------------\n");

            // -----------------------------------

            Console.WriteLine($"Graph 2\n");
            graph = new Graph(5);
            edges = new[]
            {
                (0, 1),
                (0, 3),
                (1, 2),
                (2, 4),
                (3, 4)
            };
            GraphOutput(graph, edges);
            Console.WriteLine("\n------------------\n");

            // -----------------------------------

            Console.WriteLine($"Graph 3\n");
            graph = new Graph(6);
            edges = new[]
            {
                (0, 1),
                (0, 3),
                (2, 1),
                (2, 3),
                (4, 3)
            };
            GraphOutput(graph, edges);
            Console.WriteLine("\n------------------\n");

            // -----------------------------------

            Console.ReadKey();
        }

        public static void GraphOutput(Graph graph, (int, int)[] edges)
        {
            foreach ((int, int) edge in edges)
            {
                graph.AddEdge(edge.Item1, edge.Item2);
            }

            Console.WriteLine("Vertex neighbors:");
            List<List<int>> contiguity = graph.GetContiguityMatrix();
            for (int i = 0; i < contiguity.Count; i++)
            {
                Console.Write($"{i} | ");
                foreach (int vertex in contiguity[i])
                {
                    Console.Write($"{vertex} ");
                }
                if (contiguity[i].Count == 0)
                {
                    Console.Write("isolated");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nComponents of graph:");
            List<List<int>> components = graph.FindСomponents();
            for (int i = 0; i < components.Count; i++)
            {
                Console.Write($"{i + 1} | ");
                components[i].Sort();
                foreach (int vertex in components[i])
                {
                    Console.Write($"{vertex} ");
                }
                Console.WriteLine();
            }

            Console.WriteLine($"\nIs Euler graph: {graph.IsEuler}");
            if (graph.IsEuler)
            {
                Console.Write("Euler cycle: ");
                foreach (int vertex in graph.FindEulerCycle(0))
                {
                    Console.Write($"{vertex} ");
                }
                Console.WriteLine();
            }

            Console.WriteLine($"\nIs bipartite graph: {graph.IsBipartite}");
            if (graph.IsBipartite)
            {
                Console.Write("first part  | ");
                List<int> firstPart, secondPart;
                graph.FindBipartiteParts(out firstPart, out secondPart);
                foreach (int vertex in firstPart)
                {
                    Console.Write($"{vertex} ");
                }
                Console.Write("\nsecond part | ");
                foreach (int vertex in secondPart)
                {
                    Console.Write($"{vertex} ");
                }
                Console.WriteLine();
            }
        }
    }
}
