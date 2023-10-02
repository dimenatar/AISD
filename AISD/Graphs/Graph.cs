namespace AISD.Graphs
{
    internal class Graph
    {
        // список из вершин
        private List<Node> _nodes;

        // конструктор, принимающий на вход матрицу
        public Graph(string[,] array) 
        {
            // инициализируем все
            _nodes = new List<Node>();

            List<Arc> arcs = new List<Arc>();
            List<int> createdIndexes = new List<int>();

            // проходим по строкам, создавая необходимые вершины с индексами начиная с 1, еслм таких вершин еще не существует
            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (!createdIndexes.Contains(i+1))
                {
                    Node node = new Node(i + 1);
                    _nodes.Add(node);
                    createdIndexes.Add(i+1);

                }    

                // проходим по столбцам, если нашли связь между вершинами, тогда проверяем, если такая вершина не существует, тогда добавляем ее. Потом добавляем ребро между вершинами и задаем ей вес
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] != "0")
                    {

                        Node node = _nodes.Find(n => n.Index == j + 1);
                        Node prevNode = _nodes.Find(n => n.Index == i + 1);
                        if (node == null)
                        {
                            node = new Node(j + 1);
                            _nodes.Add(node);
                            createdIndexes.Add(j + 1);
                        }
                        Arc arc = new Arc(prevNode, node, int.Parse(array[i, j]));
                        prevNode.AddArc(arc);
                        node.InArc = arc;

                        Console.WriteLine($"{prevNode.Index} => {node.Index}");
                    }
                }
            }
        }

        // метод поиска кратчейших путей из указанной вершины
        public void FindPathesByDijkstra(int sourceIndex)
        {
            // проверка на то, существует ли такая вершина
            var foundNode = _nodes.Find(n => n.Index == sourceIndex);
            if (foundNode == null)
            {
                Console.WriteLine("вершины с таким индексом не существует");
                return;
            }
            FindPathesByDijkstra(_nodes, foundNode);
        }

        //метод поиска кратчайших путей с указанием всех вершин и начальной вершины
        public void FindPathesByDijkstra(List<Node> nodes, Node source)
        {
            // инициализация
            Dictionary<Node, int> distances = new Dictionary<Node, int>();
            Dictionary<Node, Node> previous = new Dictionary<Node, Node>();
            HashSet<Node> visited = new HashSet<Node>();

            // задача максимальных значений
            foreach (Node node in nodes)
            {
                distances[node] = int.MaxValue;
                previous[node] = null;
            }

            distances[source] = 0;

            // пока не посетили все вершины
            while (visited.Count < nodes.Count)
            {
                // находим ближайшую вершину
                Node current = GetMinimumDistanceNode(distances, visited);

                visited.Add(current);
                if (current == null) break;

                //ходим по соседним ребрам
                foreach (Arc arc in current.OutArcs)
                {
                    Node neighbor = arc.To;
                    int distance = distances[current] + arc.Weigth;

                    if (distance < distances[neighbor])
                    {
                        distances[neighbor] = distance;
                        previous[neighbor] = current;
                    }
                }
            }
            //выводим кратчайшие пути
            PrintShortestPaths(distances, previous, source);
        }

        private Node GetMinimumDistanceNode(Dictionary<Node, int> distances, HashSet<Node> visited)
        {
            Node minDistanceNode = null;
            int minDistance = int.MaxValue;

            // проходимся по всем доступым вершинам и ищем кратчайшее расстояние
            foreach (KeyValuePair<Node, int> pair in distances)
            {
                Node node = pair.Key;
                int distance = pair.Value;

                if (!visited.Contains(node) && distance < minDistance)
                {
                    minDistance = distance;
                    minDistanceNode = node;
                }
            }

            return minDistanceNode;
        }

        private void PrintShortestPaths(Dictionary<Node, int> distances, Dictionary<Node, Node> previous, Node source)
        {
            Console.WriteLine("Самое короткое расстраяние из {0}:", source.Index);

            // выводим кратчайшие расстояния для всех вершин
            foreach (KeyValuePair<Node, int> pair in distances)
            {
                if (pair.Key == source) continue;

                Node node = pair.Key;
                int distance = pair.Value;

                if (distance == int.MaxValue)
                {
                    Console.WriteLine("{0} -> {1}: Нет пути", source.Index, node.Index);
                }
                else
                {
                    Console.Write("{0} -> {1}: [", source.Index, node.Index);
                    // вызываем функцию-рекурсию для вывода всего пути
                    PrintPath(previous, source, node);
                    Console.WriteLine("] Расстояние: {0}", distance);
                }
            }
        }

        private void PrintPath(Dictionary<Node, Node> previous, Node source, Node destination)
        {
            if (destination == null)
            {
                return;
            }

            PrintPath(previous, source, previous[destination]);

            if (destination != source)
            {
                Console.Write(", ");
            }

            Console.Write(destination.Index);

        }
    }
}
