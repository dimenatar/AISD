namespace AISD.HashTables
{
    internal class HashTable
    {
        // необходимые нам поля. _table - массив, представляющий собой хэш-таблицу; _size - размер раблицы; _searchStep - шаг поиска; _insertedAmount - количество вставленных элементов, чтобы отслеживать переполнена ли таблица
        private int[] _table;
        private int _size;
        private int _searchStep = 1;
        private int _insertedAmount;

        //конструктор, на вход идет размерность и необязательный параметр шага поиска
        public HashTable(int size, int searchStep = 1)
        {
            _size = size;
            _table = new int[size];
            _searchStep = searchStep;
        }

        //метод вставки элемента. На вход идет сам элемент. Проверяем не заполнена ли таблица, а потом с помощтю хэш-функции проверяем, занят ли уже соответствующий индекс. Если да, то линейно идем дальше по таблице.
        public void Insert(int value)
        {
            if (_insertedAmount >= _size)
            {
                Console.WriteLine("Таблица заполнена!");
                return;
            }

            int index = HashFunction(value);
            int i = 0;
            
            while (_table[index] != 0)
            {
                i += _searchStep;
                index = (index + i) % _size;
            }
            _insertedAmount++;
            _table[index] = value;
        }

        // метод поиска. Индекс элемента в таблице определяется хэш-функцией. Если он не равен нашему элементу, линейно ищем дальше. если по этому индексу "0" - не нашли 
        public bool Search(int value)
        {
            int index = HashFunction(value);
            int i = 0;

            while (_table[index] != value)
            {
                if (_table[index] == 0)
                {
                    return false;
                }

                i += _searchStep;
                index = (index + i) % _size;
            }

            return true;
        }

        // метод вывода таблицы
        public void PrintTable()
        {
            for (int i = 0; i < _size; i++)
            {
                Console.WriteLine($"Индекс: {i}, Элемент: {_table[i]}");
            }
        }

        // сама хэш-функция
        private int HashFunction(int value)
        {
            return value % _size;
        }
    }
}
