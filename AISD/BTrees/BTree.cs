namespace AISD.BTrees
{
    internal class BTree
    {
        public int MinAmount => (int)Math.Ceiling((double)_m / 2 - 1);

        private int _m;
        private Node _root;

        protected Node Root 
        { 
            get => _root; 
            private set
            {
                _root = value;
            }
        }

        public BTree(int m)
        {
            _m = m;
            Root = new Node(m);
        }

        public void Add(int item)
        {
            Console.WriteLine($"add item {item}");
            Root = Add(item, Root);
            PrintTree();
        }

        public void Remove(int item)
        {

            Node foundNode = GetNodeWithValue(Root, item);
            if (foundNode.IsLeaf)
            {
                RemoveFromLeaf(item, foundNode);
            }
            else
            {
                RemoveFromNonLeaf(item, foundNode);
            }
        }

        public bool Search(int item)
        {
            return Search(item, Root);   
        }

        public void PrintTree()
        {
            PrintTree(Root);
        }

        private void RemoveFromLeaf(int item, Node currentNode)
        {
            currentNode.Remove(item);
            if (currentNode.IsUnderflowed)
            {
                ManageUnderflow(currentNode);
            }
        }

        private void ManageUnderflow(Node currentNode)
        {
            bool isRemoved = false;
            var left = GetFromLeft(currentNode);
            var right = GetFromRight(currentNode);
            if (left != null)
            {
                if (left.Values.Count > MinAmount)
                {
                    var updatedParentValue = left.Values.Last();
                    var updatedCurrentNodeValue = currentNode.Parent.Values.First();

                    left.Remove(updatedParentValue);
                    currentNode.Parent.Remove(updatedCurrentNodeValue);

                    currentNode.Parent.Add(updatedParentValue);
                    currentNode.Add(updatedCurrentNodeValue);
                    isRemoved = true;
                }
            }
            if (right != null && !isRemoved)
            {
                if (right.Values.Count > MinAmount)
                {
                    var updatedParentValue = right.Values.First();
                    var updatedCurrentNodeValue = currentNode.Parent.Values.Last();

                    right.Remove(updatedParentValue);
                    currentNode.Parent.Remove(updatedCurrentNodeValue);

                    currentNode.Parent.Add(updatedParentValue);
                    currentNode.Add(updatedCurrentNodeValue);

                    isRemoved = true;
                }
            }
            if (!isRemoved)
            {
                Merge(currentNode, left, right);
            }
            currentNode.IsUnderflowed = false;
        }

        private void Merge(Node currentNode, Node? left, Node? right)
        {
            // mergde
            Node merdgingNode = left != null ? left : right;


            int insertingParentElementIndex = 0;
            int indexOfCurrentChild = currentNode.Parent.Children.IndexOf(currentNode);
            if (indexOfCurrentChild == 0)
            {
                insertingParentElementIndex = 0;
            }
            else
            {
                insertingParentElementIndex = indexOfCurrentChild - 1;
            }
            // добавляем из верхнего узла нужный элемент
            currentNode.Add(currentNode.Parent.Values[insertingParentElementIndex]);
            // удаляем его же из верхнего узла
            currentNode.Parent.Remove(currentNode.Parent.Values[insertingParentElementIndex]);
            //удаляем из верхнего узла ссылку на узел, из которого мы взяли элементы
            currentNode.Parent.RemoveChildren(merdgingNode);
            currentNode.AddChildren(merdgingNode.Children);
            currentNode.Add(merdgingNode.Values);

            if (currentNode.Parent == Root)
            {
                if (Root.Values.Count == 0)
                {
                    var currentChildren = Root.Children;
                    foreach (var child in Root.Children)
                    {
                        // добавляем детей от детей рута, чтоб объединить
                        Root.AddChildren(child.Children);
                    }
                    Root.RemoveChildren(currentChildren);
                    // и пихаем в рут все элементы от детей
                    foreach (var pastChild in currentChildren)
                    {
                        Root.Add(pastChild.Values);
                    }
                }
            }
            else if (currentNode.Parent.IsUnderflowed)
            {
                ManageUnderflow(currentNode.Parent);
            }
        }

        private void RemoveFromNonLeaf(int item, Node currentNode)
        {
            int removingElementIndex = currentNode.Values.IndexOf(item);


            Node childFrom = FindLeaf(currentNode.Children[removingElementIndex]);

            int removingItemFromChild = childFrom.Values.Last();
            currentNode.Remove(item);
            currentNode.Add(removingItemFromChild);
            RemoveFromLeaf(removingItemFromChild, childFrom);
        }

        private Node FindLeaf(Node currentNode)
        {
            if (currentNode.IsLeaf) return currentNode;

            return FindLeaf(currentNode.Children[currentNode.Children.Count - 1]);
        }

        private bool Search(int item, Node currentNode)
        {
            if (currentNode.Values.Contains(item)) return true;
            else
            {
                foreach (var child in currentNode.Children)
                {
                    if (Search(item, child)) return true;
                }
                return false;
            }
        }

        private void PrintTree(Node current)
        {
            Console.WriteLine(current.ToString());
            for (int i = 0; i < current.Children.Count; i++)
            {
                Console.WriteLine($"Child {i} : {current.Children[i]}");
            }
            for (int i = 0; i < current.Children.Count; i++)
            {
                PrintTree(current.Children[i]);
            }
        }

        private Node Add(int item, Node current)
        {
            if (current.Children.Count == 0) //|| current.Values[0].CompareTo(item) < 0 && current.Values[current.Values.Count - 1].CompareTo(item) > 0)
            {
                Console.WriteLine("child count = 0");
                current.Add(item);
                if (current.IsOverflowed)
                {
                    var updated = SplitNode(current);
                    current.IsOverflowed = false;
                    return updated;
                }
                return current;
            }


            for (int i = 0; i < current.Values.Count; i++)
            {
                if (item < current.Values[i])
                {
                    current.Children[i] = Add(item, current.Children[i]);
                    break;
                }
                else if (i == current.Values.Count - 1 && item > current.Values[i])
                {
                    current.Children[i + 1] = Add(item, current.Children[i + 1]);
                    break;
                }
            }
            if (current.IsOverflowed)
            {
                var updated = SplitNode(current);
                current.IsOverflowed = false;
                return updated;
            }
            return current;
        }

        private Node SplitNode(Node node)
        {
            Console.WriteLine("split");

            Node left, right;
            left = new Node(_m);
            right = new Node(_m);

            int lengthStart = _m / 2;
            int lengthEnd = _m % 2 == 0 ? (_m - 1) / 2 : _m / 2;
            int centerIndex = node.Values.Count / 2;

            List<int> nodeValues = node.Values;

            int childCountStart = node.Children.Count / 2;
            int childCountEnd = node.Children.Count % 2 == 0 ? (node.Children.Count) / 2 : (node.Children.Count + 1) / 2;

            left.Children = new List<Node>(node.Children.Take(childCountStart));
            right.Children = new List<Node>(node.Children.TakeLast(childCountEnd));

            left.ReplaceValues(nodeValues.Take(lengthStart));
            right.ReplaceValues(nodeValues.TakeLast(lengthEnd));


            if (node == _root)
            {

                left.Parent = right.Parent = node;

                node.ReplaceValues(new List<int> { node.Values[centerIndex] });
                node.Children.Clear();
                node.AddChildren(left);
                node.AddChildren(right);
                left.ReparentChildren();
                right.ReparentChildren();
                return node;
            }
            else
            {
                node.IsOverflowedNonRoot = true;
                //node.Parent.AddChildren(left);
                node.Parent.AddChildren(right);
                //node.Parent.RemoveChildren(node);
                left.Parent = right.Parent = node.Parent;
                node.Parent.Add(node.Values[node.Values.Count / 2]);

                node.CopyFrom(left);
                node = left;
                node.ReparentChildren();
                left.ReparentChildren();
                right.ReparentChildren();
                return left;
            }
        }

        private Node GetFromLeft(Node current)
        {
            var index = current.Parent.Children.IndexOf(current);
            if (index > 0)
            {
                return current.Parent.Children[index - 1];
            }
            return null;
        }

        private Node GetFromRight(Node current)
        {
            var index = current.Parent.Children.IndexOf(current);
            if (index < current.Parent.Children.Count - 1)
            {
                return current.Parent.Children[index + 1];
            }
            return null;
        }

        private Node GetNodeWithValue(Node current, int value)
        {
            if (current.Values.Contains(value)) return current;
            else
            {
                if (current.Children.Count > 0)
                {
                    Node found;
                    foreach (var child in current.Children)
                    {
                        found = GetNodeWithValue(child, value);
                        if (found != null) return found;
                    }
                    return null;
                }
                else return null;
            }
        }
    }
}