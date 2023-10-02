namespace AISD.BTrees
{
    internal class Key<T>
    {
        public int Value { get; private set; }

        public Node LeftNode { get; set; }
        public Node RightNode { get; set; }

        public Key (int value)
        {
            Value = value;
        }
    }
}
