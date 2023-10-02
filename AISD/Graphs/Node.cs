namespace AISD.Graphs
{
    internal class Node
    {
        private int _index;
        private Arc _inArc;
        private List<Arc> _outArcs;

        public int Index => _index;
        public Arc InArc { get => _inArc; set => _inArc = value; }
        public List<Arc> OutArcs => _outArcs;

        public Node (int index, Arc inArc = null)
        {
            _index = index;
            _inArc = inArc;
            _outArcs = new List<Arc>();
        }

        public void AddArc (Arc arc)
        {
            _outArcs.Add(arc);
        }
    }
}
