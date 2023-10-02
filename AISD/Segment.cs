namespace AISD
{
    public class Segment
    {
        private KeyValuePair<int, List<Block>> _blocks;

        public int BlockSize { get; protected set; }
        public int Hash => _blocks.Key;

        public Segment(int hash, int blockSize, List<Block> blocks)
        {
            BlockSize = blockSize;
            _blocks = new KeyValuePair<int, List<Block>>(hash, blocks);
        }

        public override string ToString()
        {
            return string.Join(" -> ", _blocks.Value.Select(block => block.ToString()));
        }

        public List<Block> GetBlocks()
        {
            return _blocks.Value;
        }

        public void Insert(string value)
        {
            if (_blocks.Value.Count > 0)
            {
                bool isInserted = false;
                int i = 0;
                List<Block> blocks = _blocks.Value.ToList();
                foreach (Block block in blocks)
                {
                    if (block.ValuesCount < BlockSize)
                    {
                        isInserted = true;
                        block.Insert(value);
                        break;
                    }

                    i++;

                    if (i == _blocks.Value.Count && !isInserted)
                    {
                        CreateBlock(value);
                        break;
                    }
                }
            }
            else
            {
                CreateBlock(value);
            }
        }

        public bool Remove(string value)
        {
            foreach (var block in _blocks.Value)
            {
                if (block.Remove(value)) return true;
            }
            return false;
        }

        public bool Search(string value)
        {
            foreach (var block in _blocks.Value)
            {
                if (block.Contains(value)) return true;
            }
            return false;
        }

        protected void CreateBlock(string value)
        {
            Block block = new Block();
            block.Insert(value);
            _blocks.Value.Add(block);
        }
    }
    
}
