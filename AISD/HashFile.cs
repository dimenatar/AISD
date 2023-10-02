namespace AISD
{
    internal class HashFile
    {
        private int _blockSize;
        private List<Segment> _segments;

        public List<Segment> Segments => new List<Segment>(_segments);

        public HashFile(int blockSize, List<Segment> segments)
        {
            _blockSize = blockSize;
            _segments = segments;
        }

        public HashFile(int blockSize)
        {
            _blockSize = blockSize;
            _segments = new List<Segment>();
        }

        public override string ToString()
        {
            return string.Join('\n', _segments.Select(segment => segment.ToString()));
        }

        public void AddElement(string element)
        {
            int hash = GetHash(element);
            Segment foundSegment = _segments.Find(segment => segment.Hash == hash);
            if (foundSegment != null)
            {
                foundSegment.Insert(element);
            }
            else
            {
                Segment segment = new Segment(hash, _blockSize, new List<Block>());
                segment.Insert(element);
                _segments.Add(segment);
            }
        }

        public bool SearchElement(string element)
        {
            foreach (var segment in _segments)
            {
                if (segment.Search(element)) return true;
            }
            return false;
        }

        public bool RemoveElement(string element)
        {
            foreach (var segment in _segments)
            {
                if (segment.Remove(element)) return true;
            }
            return false;
        }

        protected int GetHash(string element)
        {
            if (!string.IsNullOrEmpty(element)) return element[0];
            else throw new Exception("строка должна быть валидной");
        }
    }
}
