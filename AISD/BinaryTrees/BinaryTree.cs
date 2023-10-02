namespace AISD.BinaryTrees
{
    internal class BinaryTree
    {
        // корень дерева
        private Node _root;

        // конструкторы
        public BinaryTree() { }

        public BinaryTree(Node root)
        {
            _root = root;
        }

        public BinaryTree(int startIndex)
        {
            _root = new Node(startIndex);
        }

        // вставка прошивкой
        public void InsertThreaded(int value)
        {
            Node node = new Node(value);
            _root = InsertThreaded(_root, node);
            Thread();
        }

        // поиск прошивкой. Запускаем рекурсивую функцию
        public bool SearchValueThreaded(int value)
        {
            return SearchValueThreaded(_root, value, new List<Node>());
        }

        // обычная вставка. Вызываем метод с таким же названием но с другим параметром
        public void Insert(int value)
        {
            Insert(new Node(value));
        }

        // вызываем рекурсивную функцию вставки от рут
        public void Insert(Node node)
        {
            _root = Insert(_root, node);
        }

        // вызываем рекурсивную функцию удаления от рут
        public void Delete(int value)
        {
            _root = DeleteNode(_root, value);
        }

        // прямой обход через курсию
        public void PrintTreePreOrder()
        {
            if (_root != null)
            {
                Console.WriteLine("корень: " + _root.Value);
                PrintTreePreOrder(_root);
            }
            else
            {
                Console.WriteLine("дерево не имеет корня!");
            }
        }

        // симметричный обход через рекурсию
        public void PrintTreeInOrder()
        {
            PrintTreeInOrder(_root);
        }

        // обратный обход через рекурсию
        public void PrintTreePostOrder()
        {
            PrinTreePostOrder(_root);
        }

        // поиск значения через рекурсию
        public bool SearchValue(int value)
        {
            return SearchValue(value, _root);
        }

        // вывод прошитого дерева
        public void PrintThreadedTree()
        {
            PrintThreadedTreeInOrder(_root, new List<Node>());
        }

        // Если нашего элемента не существует, возвращаем ложь. В обратном случае возвращаем рекурсивные значения
        private bool SearchValue(int value, Node current)
        {
            if (current == null) return false;
            if (current.Value != value) return SearchValue(value, current.LeftChild) || SearchValue(value, current.RightChild);
            return true;
        }

        //симметричный обход, вывод посередине рекурсивного вывода от левого и правого ребенка
        private Node PrintTreeInOrder(Node current)
        {
            if (current == null) return null;

            Node left = current.LeftChild;
            Node right = current.RightChild;

            PrintTreeInOrder(left);
            Console.WriteLine($"Текущий элемент: {current.Value}, левый потомок: {(left == null ? "отсутствует" : left.Value)}, правый потомок: {(right == null ? "отсутствует" : right.Value)}");
            PrintTreeInOrder(right);
            return current;
        }

        // обратный ход, вывод после рекурсивных вызовов
        private Node PrinTreePostOrder(Node current)
        {
            if (current == null) return null;

            Node left = current.LeftChild;
            Node right = current.RightChild;

            PrinTreePostOrder(left);
            PrinTreePostOrder(right);
            Console.WriteLine($"Текущий элемент: {current.Value}, левый потомок: {(left != null ? left.Value : "отсутствует")}, правый потомок: {(right != null ? right.Value : "отсутствует")}");
            return current;
        }

        // прямой ход, вывод до рекурсивных вызовов
        private Node PrintTreePreOrder(Node current)
        {
            if (current == null) return null;

            Node left = current.LeftChild;
            Node right = current.RightChild;

            Console.WriteLine($"Текущий элемент: {current.Value}, левый потомок: {(left == null ? "отсутствует" : left.Value)}, правый потомок: {(right == null ? "отсутствует" : right.Value)}");
            PrintTreePreOrder(left);
            PrintTreePreOrder(right);
            return current;
        }

        // симметричный обход прошитого дерева. Чтобы не повторяться, записываем прошедшие вершины в список
        private Node PrintThreadedTreeInOrder(Node current, List<Node> visited)
        {
            visited.Add(current);
            if (current == null) return null;

            Node left = current.LeftChild;
            Node right = current.RightChild;

            if (!visited.Contains(left))
            {
                PrintThreadedTreeInOrder(left, visited);
            }
            Console.WriteLine($"Текущий элемент: {current.Value}, левый потомок: {(left != null ? left.Value : "отсутствует")}, правый потомок: {(right != null ? right.Value : "отсутствует")}");
            if (!visited.Contains(right))
            {
                PrintThreadedTreeInOrder(right, visited);
            }
            return current;
        }

        // рекурсивная вставка. Если текущего элемента не существует, возвращаем значение. Оно и будет на этом месте
        private Node Insert(Node currentNode, Node value)
        {
            if (currentNode == null)
            {
                return value;
            }

            if (currentNode.Value > value.Value)
            {
                currentNode.LeftChild = Insert(currentNode.LeftChild, value);
            }
            else if (currentNode.Value <= value.Value)
            {
                currentNode.RightChild = Insert(currentNode.RightChild, value);
            }
            return currentNode;
        }

        // рекурсивное удаление. После самого фактического удаления нужно вставить на место удаленного элемент, который будет подходит по "очереди"
        private Node DeleteNode(Node currentNode, int value)
        {
            if (currentNode == null)
            {
                return null;
            }

            if (value < currentNode.Value)
            {
                currentNode.LeftChild = DeleteNode(currentNode.LeftChild, value);
            }
            else if (value > currentNode.Value)
            {
                currentNode.RightChild = DeleteNode(currentNode.RightChild, value);
            }
            else
            {
                if (currentNode.LeftChild == null)
                {
                    return currentNode.RightChild;
                }
                else if (currentNode.RightChild == null)
                {
                    return currentNode.LeftChild;
                }

                currentNode.Value = FindMinValue(currentNode.RightChild);
                currentNode.RightChild = DeleteNode(currentNode.RightChild, currentNode.Value);
            }

            return currentNode;
        }

        // доп метод для удаления, чтоб найти минимальный элемент в дереве
        private int FindMinValue(Node currentNode)
        {
            int minValue = currentNode.Value;

            while (currentNode.LeftChild != null)
            {
                minValue = currentNode.LeftChild.Value;
                currentNode = currentNode.LeftChild;
            }

            return minValue;
        }

        // рекурсивная функция поиска, чтоб не вечно циклиться, вводим доп список посещенных вершин
        private bool SearchValueThreaded(Node current, int value, List<Node> visited)
        {
            if (current.Value == value) return true;
            visited.Add(current);
            bool containsLeft = false;
            bool containsRight = false;
            // если такой элмент отсутствует, рекурсивно вызываем от левого потомка
            if (!visited.Contains(current.LeftChild))
            {
                containsLeft = SearchValueThreaded(current.LeftChild, value, visited);
            }
            // и правого
            if (!visited.Contains(current.RightChild))
            {
                containsRight = SearchValueThreaded(current.RightChild, value, visited);
            }
            return containsLeft || containsRight;
        }

        // собираем все вершины дерева в симметричном порядке и записываем в список
        private void CollectAllSymmetric(Node current, List<Node> visited)
        {
            if (current == null) return;

            if (!current.IsLeftThreaded)
                CollectAllSymmetric(current.LeftChild, visited);

            if (!visited.Contains(current))
            {
                visited.Add(current);
            }

            if (!current.IsRightThreaded)
                CollectAllSymmetric(current.RightChild, visited);

        }

        // вставка прошивкой через рекурсию
        private Node InsertThreaded(Node node, Node value)
        {
            // если текущая вершина не задана, значение будет вставлено вместо нее
            if (node == null)
            {
                return value;
            }

            //если значение меньше текущего, пытаемся вставить слева
            if (value.Value <= node.Value)
            {
                if (node.IsLeftThreaded)
                {
                    node.IsLeftThreaded = false;
                    node.LeftChild = null;
                }
                node.LeftChild = InsertThreaded(node.LeftChild, value);
            }
            // если больше, тогда справа
            else
            {
                if (node.IsRightThreaded)
                {
                    node.IsRightThreaded = false;
                    node.RightChild = null;
                }
                node.RightChild = InsertThreaded(node.RightChild, value);
            }

            return node;
        }

        // заново связываем все вершины, у которых присутствуют null-ссылки или прошивочные ссылки
        private void Thread()
        {
            // записываем все вершины по возрастанию
            List<Node> nodes = new List<Node>();
            CollectAllSymmetric(_root, nodes);

            // связываем первый и последний элемент
            nodes[0].LeftChild = _root;
            nodes[0].IsLeftThreaded = true;
            nodes[^1].RightChild = _root;
            nodes[^1].IsRightThreaded = true;

            // через цикл связываем вершины (у которых есть налл ссылки или нитьевые ссылки) с соседями
            for (int i = 0; i < nodes.Count; i++)
            {
                if ((nodes[i].LeftChild == null || nodes[i].IsLeftThreaded) && i > 0)
                {
                    nodes[i].LeftChild = nodes[i - 1];
                    nodes[i].IsLeftThreaded = true;
                }
                if ((nodes[i].RightChild == null || nodes[i].IsRightThreaded) && i < nodes.Count - 1)
                {
                    nodes[i].RightChild = nodes[i + 1];
                    nodes[i].IsRightThreaded = true;
                }
            }
        }
    }
}
