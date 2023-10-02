namespace AISD.Graphs
{
    internal class Path
    {
        private List<Node> _nodes;

        public Path() 
        {
            _nodes = new List<Node>();
        }

        public void AddNode(Node node)
        {
            _nodes.Add(node);
        }

        public string GetPath()
        {
            List<string> path = new List<string>();
            int weigth = 0;
            for (int i = _nodes.Count - 1; i >= 0; i--)
            {
                path.Add($"{{ {_nodes[i].Index} }}");
                if (i != 0)
                path.Add("=>");

                if (_nodes[i].InArc != null)
                {
                    weigth += _nodes[i].InArc.Weigth;
                }
            }
            path.Add($" Weight: {weigth}");
            return string.Concat(path);
        }
    }
}
