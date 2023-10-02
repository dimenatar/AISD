using AISD.BinaryTrees;
using AISD.BTrees;
using AISD.Graphs;
using AISD.HashTables;

namespace AISD
{
    internal static partial class TasksHandler
    {
        public static void PerformLab5()
        {
            int n = 11;
            int minValue = 44000;
            int maxValue = 73000;
            int searchStep = 1;
            int m = 15;

            // инициализируем массив, объект рандома и объект хэш-таблицы
            int[] array = new int[n];
            Random random = new Random();
            HashTable hashTable = new HashTable(m, searchStep);

            // забиваем массив рандомными элементами от минимального до максимального включительно и сразу выводим на экран
            Console.WriteLine("Исходный массив:");
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(minValue, maxValue + 1);
                Console.Write(array[i] + " ");
            }
            Console.WriteLine();

            // забиваем таблицу нажим массивом
            foreach (int value in array)
            {
                hashTable.Insert(value);
            }

            // и выводим ее
            Console.WriteLine("Хеш-таблица:");

            hashTable.PrintTable();

            // считываем с клавиатуры значение, которое мы хотим найти. Если оно корректное - ищем
            Console.WriteLine("Введите значение для поиска в таблице");
            if (int.TryParse(Console.ReadLine(), out int searchValue))
            {
                Console.WriteLine($"Поиск элемента {searchValue}:");

                bool found = hashTable.Search(searchValue);
                Console.WriteLine(found ? "Элемент найден" : "Элемент не найден");
            }
        }

        public static void PerformLab13() 
        {
            // считываем название файла
            Console.WriteLine("Введите название файла с .txt");
            string filename = Console.ReadLine();

            // получаем граф из файла
            Graph graph = new Graph(FileHelper.GetMatrixFromFile(filename));

            //вводим вершину
            Console.WriteLine("Введите индекс первой вершины, начиная с 1");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                graph.FindPathesByDijkstra(index);
            }
        }

        public static void PerformKR1()
        {
            // меню на задания
            Console.Clear();
            PerformInput("1. Бинарные деревья \n2. Прошитые деревья", out byte outerChoice);
            switch(outerChoice)
            {
                case 1:
                    {
                        // создания экземпляра класса
                        Console.Clear();
                        BinaryTree br = new BinaryTree();
                        while (true)
                        {
                            //меню на бинарные дервья
                            Console.WriteLine("1. Ручной ввод");
                            Console.WriteLine("2. Заранее заданные значения");
                            Console.WriteLine("3. Поиск");
                            Console.WriteLine("4. Удаление");
                            Console.WriteLine("5. Вывод дерева");
                            Console.WriteLine("6. Выход");

                            if (byte.TryParse(Console.ReadLine(), out byte choice))
                            {
                                
                                switch (choice)
                                {
                                    case 1:
                                        {
                                            //с помощью дополнительных методов вводим количество элементов
                                            PerformInput("Введите количество элементов", out int elementCount);
                                            for (int i = 0; i < elementCount; i++)
                                            {
                                                PerformInput($"Введите элемент {i + 1}", out int element);
                                                br.Insert(element);
                                            }

                                            break;
                                        }
                                    case 2:
                                        {
                                            // ввод хардкорных значений
                                            br.Insert(50);
                                            br.Insert(40);
                                            br.Insert(70);
                                            br.Insert(30);
                                            br.Insert(60);
                                            br.Insert(75);
                                            br.Insert(20);
                                            br.Insert(35);
                                            br.Insert(55);
                                            br.Insert(72);
                                            br.Insert(37);

                                            // вывод дерева
                                            OutputTree(br);

                                            break;
                                        }
                                    case 3:
                                        {
                                            // поиск
                                            PerformInput("Введите значение для поиска", out int value);
                                            Console.WriteLine(br.SearchValue(value) ? "найдено" : "не найдено");
                                            break;
                                        }
                                    case 4:
                                        {
                                            // удаление
                                            PerformInput("Введите значение для удаления", out int value);
                                            br.Delete(value);
                                            OutputTree(br);
                                            break;
                                        }
                                    case 5:
                                        {
                                            // вывод дерева
                                            OutputTree(br);
                                            break;
                                        }
                                    case 6:
                                        {
                                            return;
                                        }
                                    default:
                                        {
                                            Console.WriteLine("Неправильный пункт меню");
                                            break;
                                        }
                                }
                            }
                        }
                        break;
                        
                    }
                case 2:
                    {
                        Console.Clear();
                        // создания бинарного дерева
                        BinaryTree br = new BinaryTree();
                        while (true)
                        {
                            Console.WriteLine("1. Ручной ввод");
                            Console.WriteLine("2. Заранее заданные значения");
                            Console.WriteLine("3. Поиск");
                            Console.WriteLine("4. Вывод дерева");
                            Console.WriteLine("5. Выход");

                            if (byte.TryParse(Console.ReadLine(), out byte choice))
                            {
                                

                                switch (choice)
                                {
                                    case 1:
                                        {
                                            // ввод элементов через клавиатуру
                                            PerformInput("Введите количество элементов", out int elementCount);
                                            for (int i = 0; i < elementCount; i++)
                                            {
                                                PerformInput($"Введите элемент {i + 1}", out int element);
                                                br.InsertThreaded(element);
                                                br.PrintThreadedTree();
                                            }

                                            break;
                                        }
                                    case 2:
                                        {
                                            // ввод хардкорных значений
                                            br.InsertThreaded(50);
                                            br.InsertThreaded(40);
                                            br.InsertThreaded(70);
                                            br.InsertThreaded(30);
                                            br.InsertThreaded(60);
                                            br.InsertThreaded(75);
                                            br.InsertThreaded(20);
                                            br.InsertThreaded(35);
                                            br.InsertThreaded(55);
                                            br.InsertThreaded(72);
                                            br.InsertThreaded(37);

                                            // вывод дерева
                                            br.PrintThreadedTree();

                                            break;
                                        }
                                    case 3:
                                        {
                                            // поиск
                                            PerformInput("Введите значение для поиска", out int value);
                                            Console.WriteLine(br.SearchValueThreaded(value) ? "найдено" : "не найдено");
                                            break;
                                        }
                                    case 4:
                                        {
                                            // вывод
                                            br.PrintThreadedTree();
                                            break;
                                        }
                                    case 5:
                                        {
                                            return;
                                        }
                                    default:
                                        {
                                            Console.WriteLine("Неправильный пункт меню");
                                            break;
                                        }
                                }
                            }
                        }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Неправильный номер");
                        break;
                    }
            }
            

            
        }

        public static void PerformLab14()
        {
            PerformInput("Введите размерность блока", out int blockSize);
            HashFile hashFile = new HashFile(blockSize);

            while (true)
            {

                Console.WriteLine("1. Добавить элемент");
                Console.WriteLine("2. Вывести хеш-файл");
                Console.WriteLine("3. Удалить элемент");
                Console.WriteLine("4. Найти элемент");
                Console.WriteLine("5. Сохранить хеш-файл на диск");
                Console.WriteLine("6. Выход");

                PerformInput("Выберите пункт", out byte choice);

                switch (choice)
                {
                    case 1:
                        {
                            PerformInput("Введите элемент", out string value);
                            hashFile.AddElement(value);
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine(hashFile.ToString());
                            break;
                        }
                    case 3:
                        {
                            PerformInput("Введите элемент", out string value);
                            Console.WriteLine(hashFile.RemoveElement(value) ? $"элемент {value} успешно удален!" : $"элемент {value} не найден");
                            break;
                        }
                    case 4:
                        {
                            PerformInput("Введите элемент", out string value);
                            Console.WriteLine(hashFile.SearchElement(value) ? $"элемент {value} успешно удален!" : $"элемент {value} не найден");
                            break;
                        }
                    case 5:
                        {
                            PerformInput("Введите путь и название файла", out string path);
                            FileHelper.SaveHashFile(path, hashFile);
                            break;
                        }
                    case 6:
                        {
                            return;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong input");
                            break;
                        }
                }
            }

        }

        public static void PerformLab15()
        {
            PerformInput("Введите m", out int m);
            BTree bTree = new BTree(m);

            while (true)
            {
                //меню на бинарные дервья
                Console.WriteLine("1. Ручной ввод");
                Console.WriteLine("2. Заранее заданные значения");
                Console.WriteLine("3. Поиск");
                Console.WriteLine("4. Удаление");
                Console.WriteLine("5. Вывод дерева");
                Console.WriteLine("6. Выход");

                if (byte.TryParse(Console.ReadLine(), out byte choice))
                {

                    switch (choice)
                    {
                        case 1:
                            {
                                //с помощью дополнительных методов вводим количество элементов
                                PerformInput("Введите количество элементов", out int elementCount);
                                for (int i = 0; i < elementCount; i++)
                                {
                                    PerformInput($"Введите элемент {i + 1}", out int element);
                                    bTree.Add(element);
                                }

                                break;
                            }
                        case 2:
                            {
                                // ввод хардкорных значений
                                bTree.Add(-50);
                                bTree.Add(0);
                                bTree.Add(50);
                                bTree.Add(-20);
                                bTree.Add(20);
                                bTree.Add(100);
                                bTree.Add(-30);
                                bTree.PrintTree();

                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Неправильный пункт меню");
                                break;
                            }
                    }
                }
            }
        }

        private static void OutputTree(BinaryTree tree)
        {
            Console.WriteLine("Прямой обход:");
            tree.PrintTreePreOrder();

            Console.WriteLine("Симметричный обход:");
            tree.PrintTreeInOrder();

            Console.WriteLine("Обратный обход:");
            tree.PrintTreePostOrder();
        }

        private static T PerformInput<T>(string message, out T variable) where T : IConvertible
        {
            Console.WriteLine(message);

            while (!TryParse(Console.ReadLine(), out variable))
            {
                Console.WriteLine("неверное значение");
            }

            return variable;
        }

        private static bool TryParse<T>(string input, out T value)
        {
            value = default(T);
            if (input == null) return false;

            try
            {
                value = (T)Convert.ChangeType(input, typeof(T));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
