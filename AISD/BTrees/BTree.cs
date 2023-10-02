namespace AISD.BTrees
{
    internal class BTree
    {
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
            Root.Overflowed += SplitNode;
        }

        public void Add(int item)
        {
            Console.WriteLine($"add item {item}");
            Root = Add(item, Root);
            PrintTree();
        }

        public void Remove(int item)
        {

        }

        public bool Search(int item)
        {
            return true;
        }

        public void PrintTree()
        {
            PrintTree(Root);
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
                if (current.IsOverflowedNonRoot)
                {
                    current.IsOverflowedNonRoot = false;
                    return current;
                }
                else
                    return current;
            }

            //int lastBranchIndex = current.Children.Count - 1;
            //int mostLeft = current.Children[0].Values[0];
            //int mostRight = current.Children[lastBranchIndex].Values[current.Children[lastBranchIndex].Values.Count - 1];
            //if (item < mostLeft)
            //{
            //    current.Children[0] = Add(item, current.Children[0]);
            //}
            //else if (item > mostRight)
            //{
            //    current.Children[current.Children.Count - 1] = Add(item, current.Children[current.Children.Count - 1]);
            //}
            //else
            //{
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
            //}
            return current;
        }

        private void SplitNode(Node node) // -100, -75, -50
        {
            Console.WriteLine("split");

            Node left, right;
            left = new Node(_m);
            right = new Node(_m);

            int lengthStart = _m / 2;
            int lengthEnd = _m % 2 == 0 ? (_m - 1) / 2 : _m / 2;
            int centerIndex = node.Values.Count / 2;
            var nodeValues = node.Values;

            left.Children = new List<Node>(node.Children.Take(lengthStart));
            right.Children = new List<Node>(node.Children.TakeLast(lengthEnd));

            left.ReplaceValues(nodeValues.Take(lengthStart));
            right.ReplaceValues(nodeValues.TakeLast(lengthEnd));


            left.Overflowed += SplitNode;
            right.Overflowed += SplitNode;
            if (node == _root)
            {

                left.Parent = right.Parent = node;


                node.ReplaceValues(new List<int> { node.Values[centerIndex] });

                node.Children.Add(left);
                node.Children.Add(right);
            }
            else
            {
                node.IsOverflowedNonRoot = true;
                node.Parent.Children.Add(left);
                node.Parent.Children.Add(right);
                node.Parent.Children.Remove(node);
                left.Parent = right.Parent = node.Parent;
                node.Parent.Add(node.Values[node.Values.Count / 2]);

                node.CopyFrom(left);
            }


        }

        private void Split(Node node)
        {
            Node left, right = new Node(_m);

        }
    }
}
