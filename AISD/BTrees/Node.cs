namespace AISD.BTrees
{
    public class Node : IComparer<Node>
    {
        private int _size;
        private List<int> _values;
        private NodeComparer _nodeComparer;

        public Node Parent { get; set; }
        public List<Node> Children { get; set; } = new List<Node>();
        public int Index { get; set; }
        public bool IsOverflowedNonRoot { get; set; }
        public bool IsOverflowed { get; set; }
        public bool IsUnderflowed { get; set; }
        public bool IsLeaf { get; set; }
        public List<int> Values => new List<int>(_values);

        public event Action<Node> Overflowed;

        public Node(int size, List<int> values)
        {
            _size = size;
            _values = values;
            _nodeComparer = new NodeComparer();
        }

        public Node(int size)
        {
            _size = size;
            _values = new List<int>();
        }

        public override string ToString()
        {
            return $"index: {Index}, values: {string.Join(' ', Values)} \n ChildCount: {Children.Count}";
        }

        public bool IsEnoughSpace()
        {
            return _values.Count == _size;
        }

        public void Add(int value)
        {
            _values.Add(value);
            _values.Sort();
            if (_values.Count > _size - 1)
            {
                IsOverflowed = true;
                //Overflowed?.Invoke(this);
            }
        }

        public void Add(IEnumerable<int> values)
        {
            _values.AddRange(values);
            _values.Sort();
            if (_values.Count > _size - 1)
            {
                IsOverflowed = true;
                //Overflowed?.Invoke(this);
            }
        }

        public void Remove(int value)
        {
            _values.Remove(value);
            _values.Sort();
            if (_values.Count < _size / 2)
            {
                IsUnderflowed = true;
            }
        }

        public void ReplaceValues(IEnumerable<int> values)
        {
            _values = new List<int>(values);
        }

        public void CopyFrom(Node node)
        {
            _size = node._size;
            _values = node.Values;
            Parent = node.Parent;
            Children = node.Children;
            Index = node.Index;
            Overflowed = node.Overflowed;
            IsOverflowedNonRoot = node.IsOverflowedNonRoot;
        }

        public void AddChildren(Node child)
        {
            Children ??= new List<Node>();
            Children.Add(child);
            Children.Sort(this);
        }

        public void AddChildren(IEnumerable<Node> children)
        {
            Children ??= new List<Node>();
            Children.AddRange(children);
            Children.Sort(this);
        }

        public void RemoveChildren(Node child)
        {
            Children.Remove(child);
            Children.Sort(this);
        }

        public void RemoveChildren(IEnumerable<Node> children)
        {
            foreach (var child in children)
            {
                Children.Remove(child);
            }
            Children.Sort(this);
        }

        public int Compare(Node? x, Node? y)
        {
            return x.Values.First().CompareTo(y.Values.First());
        }
    }

    public class NodeComparer : IComparer<Node>
    {
        int IComparer<Node>.Compare(Node x, Node y)
        {
            return x.Values.First().CompareTo(y.Values.First());
        }
    }
}

