namespace AISD.BTrees
{
    internal class Node
    {
        private int _size;
        private List<int> _values;

        public Node Parent { get; set; }
        public List<Node> Children { get; set; } = new List<Node>();
        public int Index { get; set; }
        public bool IsOverflowedNonRoot { get; set; }
        public List<int> Values => new List<int>(_values);

        public event Action<Node> Overflowed;

        public Node(int size, List<int> values)
        {
            _size = size;
            _values = values;
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
                Overflowed?.Invoke(this);
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
    }
}
