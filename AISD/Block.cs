namespace AISD
{
    public class Block
    {
        private List<string> _values;

        public List<string> Values => new List<string>(_values);
        public int ValuesCount => _values.Count;

        public Block(List<string> values)
        {
            _values = values;
        }

        public Block()
        {
            _values = new List<string>();
        }

        public override string ToString()
        {
            return string.Join(' ', _values);
        }

        public void Insert(string value)
        {
            _values.Add(value);
        }

        public bool Remove(string value)
        {
            return _values.Remove(value);
        }

        public bool Contains(string value)
        {
            return _values.Contains(value);
        }
    }
}