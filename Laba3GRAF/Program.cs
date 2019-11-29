﻿using System;
using System.IO;
using System.Collections;
using System.Linq;
using System.Text;

namespace Laba3GRAF
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1 - Поиск в ширину\n2 - Поиск в глубину");
            int turn = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите название вершины,которую необходимо найти ");
            char goal = Convert.ToChar(Console.Read());
            ArrayList road = new ArrayList();
            ArrayList open = new ArrayList();
            ArrayList close = new ArrayList();
            string path = @"C:\Users\Айнур\Desktop\ИиЭС\3Лаба\data.txt";
            switch (turn)
            {
                case 1:
                    using (StreamReader streamReader = new StreamReader(path)) // открываем поток считывания по указанному
                    {                                                          // в path адресу
                        int dimension;
                        string first_line;
                        first_line = streamReader.ReadLine(); // Считываю первую строку
                        dimension = Convert.ToInt32(first_line); // Преобразую ее в int получая размерность массива
                        char[,] matr_adjacency = new char[dimension, dimension];  //создаю массив типа char 
                        for (int i = 0; i < dimension; i++) // считываю все остальное в массив посимвольно
                        {
                            for (int j = 0; streamReader.Peek() != ';'; j++) // считывать до знака ;
                            {
                                matr_adjacency[i, j] = (char)streamReader.Read();
                            }
                            streamReader.ReadLine(); // переход на новую строку
                        }
                        open.Add(matr_adjacency[0, 0]); /// добавляю в список первую вершину(А)
                        for (int i = 0; i < dimension; i++) ///захватывая все строки
                        {
                            for (int index = 0; index < open.Count; index++) /// проверка списка Open на элементы
                            {                                                /// списка Close
                                for (int indX = 0; indX < close.Count; indX++)// проверка идет по всем элементам
                                {
                                    if ((char)close[indX] == (char)open[index])
                                    {
                                        open.RemoveAt(index); break;
                                    }
                                }
                            }
                            for (int j = 1; matr_adjacency[i, j] != 0; j++) // считываем всю строку
                            {
                                open.Add(matr_adjacency[i, j]); // добавляем в список Open
                                open.Remove(','); // если в список попала ',' удаляем ее
                            }
                            Console.WriteLine("open: \t\t\t close:"); /// вывожу название список чтобы
                            for (int index = 0; index < open.Count; index++)/// выглядело как таблица
                            {
                                Console.Write(open[index]); Console.Write(' '); // вывожу весь список Open
                            }
                            if (open.Count < 4) // табуляция между списками(чтобы они были в одной строке)
                            {
                                Console.Write("\t\t\t\t");
                            }
                            else if (close.Count < 5 || open.Count < 8)
                                Console.Write("\t\t\t");
                            else
                                Console.Write("\t\t");
                            for (int index = 0; index < close.Count; index++)
                            {
                                Console.Write(close[index]); Console.Write(' '); // Вывожу список Close
                            }
                            if (open.Count < 1)
                            {
                                Console.WriteLine("\nNo");
                                Console.ReadKey();
                                Environment.Exit(100);
                            }
                            if ((char)open[0] == goal) // Если первый элемент списка Open наша цель то
                            {
                                Console.WriteLine("\nYes\n"); // Выводим Да 
                                Console.ReadKey();  // Ждем нажатия любой клавишы
                                Environment.Exit(100); //Закрытие программы
                            }                            
                            Console.WriteLine(); // Переход на новую строку
                            open.RemoveAt(0); /// Удаляем отработанную вершину(ту которую уже раскрыли)
                            close.Add(matr_adjacency[i, 0]); ///и добавляем ее в Close
                        }
                    }
                    break;
                case 2:
                    using (StreamReader streamReader = new StreamReader(path))
                    {
                        char current;
                        int dimension;
                        string first_line;
                        first_line = streamReader.ReadLine();
                        dimension = Convert.ToInt32(first_line);
                        char[,] matr_adjacency = new char[dimension, dimension];
                        for (int i = 0; i < dimension; i++)
                        {
                            for (int j = 0; streamReader.Peek() != ';'; j++)
                            {
                                matr_adjacency[i, j] = (char)streamReader.Read();
                            }
                            streamReader.ReadLine();
                        }
                        open.Add(matr_adjacency[0, 0]);close.Add(matr_adjacency[0, 0]);
                        for(int index = 0;open[0] != null;index++)
                        {
                            current = (char)open[0];
                            for(int strok = 0; strok < close.Count;strok ++)
                            {
                                if(open[0] == close[strok])
                                {
                                    open.RemoveAt(0);
                                }
                            }
                            if (open.Count < 1)
                            {
                                Console.WriteLine("No");
                                Console.ReadKey();
                                Environment.Exit(200); 
                            }
                            for (int strok = 0; strok < dimension;strok ++)
                            {
                                if((char)current == matr_adjacency[strok,0])
                                {
                                     for(int column = 2; matr_adjacency[strok,column] != 0;column++)
                                     {
                                        open.Insert(0, matr_adjacency[strok, column]);
                                        open.Remove(',');
                                     }
                                }
                            }
                            close.Add(open[0]);
                            ////////////////////////////////////////////////////////Output
                            Console.WriteLine("Open:\t\t\tClose:");
                            for(int strok = 0;strok < open.Count;strok ++)
                            {
                                Console.Write($"{open[strok]} ");
                            }
                            if (open.Count < 4)
                                Console.Write("\t\t\t\t");
                            else
                                Console.Write("\t\t\t");
                            for(int strok = 0; strok < close.Count;strok ++)
                            {
                                Console.Write($" {close[strok]}");
                            }
                            Console.WriteLine();
                            /////////////////////////////////////////////////////find_goal
                            if ((char)open[0] == goal)
                            {
                                Console.WriteLine("Yes");
                                Console.ReadKey();
                                Environment.Exit(200);
                            }                           
                        }                        
                        break;
                    }
            }
        }


    }
}