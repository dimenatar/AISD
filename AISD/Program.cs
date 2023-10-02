using AISD;

byte choice;

while (true)
{
    //Console.Clear();
    Console.WriteLine("Меню");
    Console.WriteLine("1. Lab5 (хеш)");
    Console.WriteLine("2. Lab13 (графы)");
    Console.WriteLine("3. KR1 (бинарные деревья)");
    Console.WriteLine("4. Lab14 (хеш-файлы)");

    if (byte.TryParse(Console.ReadLine(), out choice))
    {
        switch(choice)
        {
            case 1:
                {
                    TasksHandler.PerformLab5();
                    break;
                }
                case 2:
                {
                    TasksHandler.PerformLab13(); break;
                }
                case 3:
                {
                    TasksHandler.PerformKR1();
                    break;
                }
                case 4:
                {
                    TasksHandler.PerformLab14();
                    break;
                }
            case 5:
                {
                    TasksHandler.PerformLab15();
                    break;
                }
            default:
                {
                    Console.WriteLine("Неправильный пункт");
                    break;
                }
        }
    }
}