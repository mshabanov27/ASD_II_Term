using System.Collections.Generic;

namespace ASD_Lab1
{
    public class Graph
    {
        private int _nodesCount;
        private List<int>[] _adjacencyList;
        //Створюємо копію списку суміжності, з якою будемо працювати,  щоб при видаленні вершин не втратити початкові дані
        private List<int>[] _tempAdjacencyList;
        
        private bool _isEuler;
        private bool _isHalfEuler;
        private string _eulerPath = "";

        public Graph(int[,] matrix)
        {
            _nodesCount = matrix.GetLength(0);
            //Перетворюємо матрицю на список суміжності (з матрицею алгоритм не буде працювати)
            _adjacencyList = _turnMatrixToList(matrix);
            _tempAdjacencyList = _adjacencyList;

            _isEuler = _checkOnEuler(_adjacencyList);
            if (!_isEuler)
            {
                _isHalfEuler = _checkOnHalfEuler(_adjacencyList);
            }
        }

        public void MakeEulerPath()
        {
            if (_isEuler)
            {
                _fleury(0);
            } 
            else if (_isHalfEuler)
            {
                _fleury(_findOddNode());
            }
            else
            { 
                _eulerPath = "There is no Euler path for this graph.";
            }
        }

        public string EulerPath
        {
            get { return _eulerPath; }
        }
        
        private void _fleury(int start)
        {
            for (int i = 0; i < _tempAdjacencyList[start].Count; i++) 
            { 
                if (isValidEdge(start, _tempAdjacencyList[start][i], i)) 
                { 
                    _eulerPath += $"{start} -> {_tempAdjacencyList[start][i]}\n";
                    
                    int next = _tempAdjacencyList[start][i];
                    _removeEdge(start, _tempAdjacencyList[start][i]);

                    _fleury(next);
                }
            }
        }

        private bool isValidEdge(int start, int end, int index)
        {
            if (_tempAdjacencyList[start].Count == 1)
                return true;
            
            bool[] visitedBeforeDelete = new bool[_nodesCount];
            int reachableBeforeDelete = dfsCount(start, visitedBeforeDelete);
            
            _removeEdge(start, end);
            
            bool[] visitedAfterDelete = new bool[_nodesCount];
            int reachableAfterDelete = dfsCount(start, visitedAfterDelete);
            
            _addEdge(start, end, index);
            if (reachableBeforeDelete == reachableAfterDelete)
                return true;
            
            return false;

        }

        private int dfsCount(int start, bool[] visited)
        {
            visited[start] = true;
            int count = 1;
         
            foreach(int i in _tempAdjacencyList[start])
            {
                if (!visited[i])
                {
                    count = count + dfsCount(i, visited);
                }
            }
            return count;
        }

        private int _findOddNode()
        {
            for (int i = 0; i < _tempAdjacencyList.Length; i++)
            {
                if (_tempAdjacencyList[i].Count % 2 == 1)
                    return i;
            }

            return -1;
        }
        
        private List<int>[] _turnMatrixToList(int[,] matrix)
        {
            List<int>[] adjList = new List<int>[_nodesCount];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                adjList[i] = new List<int>();
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (matrix[i, j] == 1)
                        adjList[i].Add(j);
            }

            return adjList;
        }

        private bool _checkOnEuler(List<int>[] adjList)
        {
            bool isProbablyEuler = true;
            
            for (int i = 0; i < adjList.Length && isProbablyEuler; i++)
            {
                if (adjList[i].Count % 2 == 1)
                    isProbablyEuler = false;
            }

            return isProbablyEuler;
        }

        private bool _checkOnHalfEuler(List<int>[] adjList)
        {
            int oddCount = 0;
            bool isProbablyHalfEuler = true;

            for (int i = 0; i < adjList.Length && isProbablyHalfEuler; i++)
            {
                if (adjList[i].Count % 2 == 1)
                    oddCount++;
                
                if (oddCount > 2)
                {
                    isProbablyHalfEuler = false;
                }
            }   

            return isProbablyHalfEuler;
        }

        private void _addEdge(int u, int v, int index)
        {
            _tempAdjacencyList[u].Insert(index, v);
            _tempAdjacencyList[v].Insert(index, u);
        }
        
        private void _removeEdge(int u, int v)
        {
            _tempAdjacencyList[u].Remove(v);
            _tempAdjacencyList[v].Remove(u);
        }

        
    }
}