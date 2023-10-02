namespace AISD.Graphs
{
    internal class Arc
    {
        private Node _from;
        private Node _to;
        private int _weigth;

        public Node From => _from;
        public Node To => _to;
        public int Weigth => _weigth;

        public Arc(Node from, Node to, int weigth)
        {
            _from = from;
            _to = to;
            _weigth = weigth;
        }
    }
}
