namespace AISD
{
    internal static class FileHelper
    {
        public static string[,] GetMatrixFromFile(string path)
        {
            // проверка на существование файла
            if (!File.Exists(path)) { return new string[0, 0]; }

            //using (StreamReader sr = new StreamReader(path))

            StreamReader sr = new StreamReader(path);

            List<string> lines = new List<string>();

            //пока не достигли конца файла, считываем все строки
            while (!sr.EndOfStream)
            {
                lines.Add(sr.ReadLine());
            }

            int rows = lines.Count;
            int columns = lines[0].Split(' ').Length;

            string[,] matrix = new string[rows, columns];
            string[] line;

            // по всем строкам делим каждую из них по пробелам и записываем в матрицу
            for (int i = 0; i < lines.Count; i++)
            {
                line = lines[i].Split(' ');

                for (int j = 0; j < line.Length; j++)
                {
                    matrix[i, j] = line[j];
                }
            }
            sr.Close();
            return matrix;
        }

        public static void SaveHashFile(string path, HashFile hashFile)
        {
            FileStream fs = new FileStream(path, FileMode.CreateNew);
            using (StreamWriter sw = new StreamWriter(fs))
            {
                var segments = hashFile.Segments;
                for (int i = 0; i < segments.Count; i++)
                {
                    List<Block> blocks = segments[i].GetBlocks();
                    for (int j = 0; j < blocks.Count; j++)
                    {
                        foreach (var element in blocks[j].Values)
                        {
                            sw.Write($"{element} ");
                        }
                        if (j != blocks.Count - 1) sw.Write("-> ");
                    }
                    if (i != blocks.Count - 1) sw.WriteLine();
                }
            }
        }
    }
}
