using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Laba4SearchToGraph
{
    class Depth // Класс для поиска в глубину
    {
        public void depth(int strok, int column, int finish_strok, int finish_column, ref List<int> open, ref List<int> close, int[,] labirint)
        {
            int current_stroka, current_column;
            link1:
            while (open[0] < strok || open[1] < column) // пока индексы в списке не вышел за пределы лабиринта 
            {
                current_stroka = open[0]; current_column = open[1]; // переменные для работы с текущим индексом
                if (open[0] == finish_strok && open[1] == finish_column) // проверка на целевую вершину
                {
                    Console.WriteLine($"Персонаж в заданной точке '9'");
                    close.Add(current_stroka); close.Add(current_column);
                    labirint[current_stroka, current_column] = 9;
                    break;
                }
                open.RemoveRange(0, 2); // очистить список open от обработанных вершин
                for (int index = 0; index < close.Count - 1; index += 2) // проверка на наличие текущей вершины в списке close
                {
                    if (close[index] == current_stroka && close[index + 1] == current_column)
                        goto link1; // перейти к link1
                }
                if (current_stroka < strok && labirint[current_stroka + 1, current_column] == 1) // если снизу от текущей ячейки единица
                {
                    open.Insert(0, current_column); // добавить в начало списка
                    open.Insert(0, current_stroka + 1);
                }
                if (current_column < column && labirint[current_stroka, current_column + 1] == 1) // если справа от текущей ячейки единица
                {
                    open.Insert(0, current_column + 1);
                    open.Insert(0, current_stroka);
                }
                if (current_column > 0 && current_column < column && labirint[current_stroka, current_column - 1] == 1) // если слева от текущей ячейки единица
                {
                    open.Insert(0, current_column - 1);
                    open.Insert(0, current_stroka);
                }
                if (current_stroka > 0 && labirint[current_stroka - 1, current_column] == 1) // если сверху от текущей ячейки единица
                {
                    open.Insert(0, current_column);
                    open.Insert(0, current_stroka - 1);
                }
                close.Add(current_stroka); close.Add(current_column); // добавить отработанные индексы в список close
            }
        }
    }
    class Width
    {
        public void width(int strok, int column, int finish_strok, int finish_column, ref List<int> open, ref List<int> close, int[,] labirint)
        {
            int current_stroka, current_column;
            link1:
            while (open[0] < strok || open[1] < column)
            {
                current_stroka = open[0]; current_column = open[1];
                if (open[0] == finish_strok && open[1] == finish_column) // проверка на наличие целевой вершины
                {
                    Console.WriteLine($"Персонаж в заданной точке '9'");
                    close.Add(current_stroka); close.Add(current_column);
                    labirint[current_stroka, current_column] = 9;
                    break;
                }
                open.RemoveRange(0, 2);
                for (int index = 0; index < close.Count - 1; index += 2)
                {
                    if (close[index] == current_stroka && close[index + 1] == current_column)
                        goto link1; // перейти к link1
                }
                if (current_stroka < strok && labirint[current_stroka + 1, current_column] == 1)
                {
                    open.Add(current_stroka + 1); // добавить в конец списка
                    open.Add(current_column);
                }
                if (current_column < column && labirint[current_stroka, current_column + 1] == 1)
                {
                    open.Add(current_stroka);
                    open.Add(current_column + 1);
                }
                if (current_column > 0 && current_column < column && labirint[current_stroka, current_column - 1] == 1)
                {
                    open.Add(current_stroka);
                    open.Add(current_column - 1);
                }
                if (current_stroka > 0 && labirint[current_stroka - 1, current_column] == 1)
                {
                    open.Add(current_stroka - 1);
                    open.Add(current_column);
                }
                close.Add(current_stroka); close.Add(current_column);
            }
        }
    }
    class Program
    {
        static void read_file_dimension(ref int strok, ref int column,string path)
        {
            string buffer_line;
            using (StreamReader streamReader = new StreamReader(path))
            {
                buffer_line = streamReader.ReadLine();
                strok = Convert.ToInt32(buffer_line);
                buffer_line = streamReader.ReadLine();
                column = Convert.ToInt32(buffer_line);
            }
        }
        static void read_file(int strok, int column, ref int[,] labirint, string path) // чтение данных с файла
        {
            char buffer;
            
            using (StreamReader streamReader = new StreamReader(path))
            {
                streamReader.ReadLine();                
                streamReader.ReadLine();
                for (int index = 0; index < strok; index++)
                {
                    for (int secIndex = 0; secIndex < column; secIndex++)
                    {
                        buffer = (char)streamReader.Read();
                        labirint[index, secIndex] = Convert.ToInt32(buffer.ToString());
                    }
                    streamReader.ReadLine();
                }
            }

        }
        static void output(int strok, int column,List<int> close, int[,] labirint) // вывод пути и всего лабиринта
        {           
            Console.WriteLine("\nВывод пути:");
            for (int i = 0; i < strok; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    for (int j_index = 0; j_index < close.Count - 1; j_index += 2)
                    {
                        if (close[j_index] == i && close[j_index + 1] == j) // если вершина с данным индексом есть в списке close вывести
                        {
                            Console.Write(labirint[i, j]);
                            break;
                        }
                    }
                    if (labirint[i, j] == 0) // иначе вывести пробел
                        Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Весь лабиринт:");
            for (int i = 0; i < strok; i++) // вывод всего лабиринта
            {
                for (int j = 0; j < column; j++)
                {
                    Console.Write(labirint[i, j]);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            int strok = 1, column = 1;
            int start_strok = 0, start_column = 0;
            int finish_strok = 0, finish_column = 0;
            try
            {
                Console.WriteLine("Для установления режима по умолчанию введите любой символ");
                Console.Write($"Введите начальное положение:\nКоордината Х:");
                start_strok = Convert.ToInt32(Console.ReadLine());
                Console.Write("Координата Y:");
                start_column = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                start_strok = 0;
                start_column = 0;
            } // задаем нач вершины
            try
            {
                Console.Write($"Введите целевое положение:\nКоордината Х:");
                finish_strok = Convert.ToInt32(Console.ReadLine());
                Console.Write("Координата Y:");
                finish_column = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                finish_strok = 3;
                finish_column = 3;
            } // задаем целевые вершины
            List<int> cost = new List<int>();
            List<int> open = new List<int>();
            List<int> close = new List<int>();
            open.Add(start_strok); open.Add(start_column);
            //////////////////////////////////////////////////
            var path = System.IO.Path.GetFullPath(@"labirinth.txt"); // инициализируем путь
            read_file_dimension(ref strok, ref column,path);
            int[,] labirint = new int[strok, column];
            read_file(strok ,column ,ref labirint, path);
            //////////////////////////////////////////////////
            Console.WriteLine("1 - поиск в глубину\n2 - поиск в ширину");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice) // выбор поиска
            {
                case 1:
                    Depth D = new Depth();
                    D.depth(strok, column, finish_strok, finish_column, ref open, ref close, labirint);
                    break;
                case 2:
                    Width W = new Width();
                    W.width(strok, column, finish_strok, finish_column, ref open, ref close, labirint);
                    break;
                default:
                    Environment.Exit(100); break;
            }
            //////////////////////////////////////////////////               
            output(strok,column,close, labirint); // вывод
        }
    }
}
