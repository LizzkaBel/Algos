using System.Collections.Generic;
using System.Linq;

namespace Graphs
{
    public class Graph
    {
        private List<List<int>> contiguity;

        private bool _bipartiteStop;

        public Graph(int size = 0)
        {
            this.contiguity = new List<List<int>>();
            for (int i = 0; i < size; i++)
            {
                this.contiguity.Add(new List<int>());
            }
        }

        public int Count => contiguity.Count;

        public bool IsEuler
        {
            get
            {
                for (int i = 0; i < Count; i++)
                {
                    if (contiguity[i].Count % 2 != 0)
                    {
                        return false;
                    }
                }
                return FindСomponents().Count == 1;
            }
        }

        public bool IsBipartite
        {
            get
            {
                if (Count == 0 || Count == 1) {
                    return false; 
                }

                _bipartiteStop = false;
                List<char> verticesColor = new List<char>();
                for (int i = 0; i < Count; i++) { verticesColor.Add('n'); }

                return DepthColorSearch(0, verticesColor, 'f');
            }
        }

        public List<List<int>> GetContiguityMatrix()
        {
            List<List<int>> contiguityCopy = new List<List<int>>(Count);
            for(int i = 0; i < Count; i++)
            {
                contiguityCopy.Add(new List<int>(contiguity[i]));
            }
            return contiguityCopy;
        }

        public void AddVertex()
        {
            contiguity.Add(new List<int>());
        }

        public void RemoveVertex(int index)
        {
            contiguity.RemoveAt(index);
            foreach(List<int> neighbors in contiguity)
            {
                neighbors.Remove(index);
            }
        }

        public void AddEdge(int vertexA, int vertexB)
        {
            if (vertexA != vertexB && !contiguity[vertexA].Contains(vertexB))
            {
                contiguity[vertexA].Add(vertexB);
                contiguity[vertexB].Add(vertexA);
            }
        }

        public void RemoveEdge(int vertexA, int vertexB)
        {
            contiguity[vertexA].Remove(vertexB);
            contiguity[vertexB].Remove(vertexA);
        }

        public List<int> FindEulerCycle(int initialVertex) {
            List<int> cycle = new List<int>();
            Graph tempCloneGraph = this.Clone();
            Stack<int> nextVertices = new Stack<int>();

            nextVertices.Push(initialVertex);
            if (!IsEuler) { return cycle; }
            while (nextVertices.Count != 0) 
            {
                int vertex = nextVertices.Peek();
                if (tempCloneGraph.contiguity[vertex].Count != 0)
                {
                    int nextNeighbor = tempCloneGraph.contiguity[vertex].First();
                    nextVertices.Push(nextNeighbor);
                    tempCloneGraph.RemoveEdge(vertex, nextNeighbor);
                }
                else 
                {
                    nextVertices.Pop();
                    cycle.Add(vertex);
                }
            }

            return cycle;
        }

        public Graph Clone()
        {
            Graph cloneGraph = new Graph();
            cloneGraph.contiguity = this.GetContiguityMatrix();
            return cloneGraph;
        }

        public void FindBipartiteParts(out List<int> firstPart, out List<int> secondPart)
        {
            firstPart = new List<int>();
            secondPart = new List<int>();

            _bipartiteStop = false;
            List<char> verticesColor = new List<char>();
            for (int i = 0; i < Count; i++) { 
                verticesColor.Add('n'); 
            }

            if (Count == 0 || Count == 1 || !DepthColorSearch(0, verticesColor, 'f'))
            {
                return;
            }

            for (int i = 0; i < verticesColor.Count; i++)
            {
                if (verticesColor[i] == 'f')
                {
                    firstPart.Add(i);
                }
                else
                {
                    secondPart.Add(i);
                }
            }
        }

        public List<List<int>> FindСomponents()
        {
            List<List<int>> components = new List<List<int>>();
            List<bool> verticesUsed = new List<bool>();
            for(int i = 0; i < Count; i++)
            {
                verticesUsed.Add(false);
            }
            for (int i = 0; i < Count; i++)
            {
                if (!verticesUsed[i])
                {
                    List<int> verticesFound = new List<int>();
                    DepthSearch(i, verticesUsed, verticesFound);
                    components.Add(verticesFound);
                }
            }
            return components;
        }

        private void DepthSearch(int vertex, List<bool> verticesUsed, List<int> verticesFound)
        {
            verticesUsed[vertex] = true;
            verticesFound.Add(vertex);
            for (int i = 0; i < contiguity[vertex].Count; i++)
            {
                int nextVertex = contiguity[vertex][i];
                if (!verticesUsed[nextVertex]) 
                {
                    DepthSearch(nextVertex, verticesUsed, verticesFound); 
                }
            }
        }

        // 'n' - none, 'f' - first, 's' - second
        private bool DepthColorSearch(int vertex, List<char> verticesColor, char vertexColor)
        {
            if (_bipartiteStop) return false;
            verticesColor[vertex] = vertexColor;
            foreach (int neighbor in contiguity[vertex])
            {
                if (verticesColor[neighbor] == 'n')
                {
                    char nextColor = (vertexColor == 'f') ? 's' : 'f';
                    DepthColorSearch(neighbor, verticesColor, nextColor);
                }
                else if (verticesColor[neighbor] == vertexColor)
                {
                    _bipartiteStop = true;
                    return false;
                }
            }
            return !_bipartiteStop;
        }
    }
}
