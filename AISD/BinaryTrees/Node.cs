namespace AISD.BinaryTrees
{
    internal class Node
    {
        public int Value { get; set; }
        public Node LeftChild { get; set; } = null;
        public Node RightChild { get; set; } = null;

        public bool IsLeftThreaded { get; set; }
        public bool IsRightThreaded { get; set; }

        public Node() { }
        public Node(int value)
        {
            Value = value;
        }
    }
}
