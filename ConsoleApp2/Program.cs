using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static long total = 0;
        static void Main(string[] args)
        {
            int[] array;
            int firstlimit = 0, secondlimit = 0;
            int size = 0, threadsAmount = 0;
            Console.WriteLine("Введіть розмір масиву :");
            if (int.TryParse(Console.ReadLine(), out size) && (size > 0))
            {
                Console.WriteLine("Введіть кількість частин ,на які ви бажаєте розбити даний масив:");
                if (int.TryParse(Console.ReadLine(), out threadsAmount) && (threadsAmount > 0))
                {
                    array = new int[size];
                    Random rand = new Random();
                    for (int i = 0; i < array.Length; i++)
                    {
                        array[i] = rand.Next(10);
                    }

                    Stopwatch SWparts = new Stopwatch();
                    Stopwatch SWtotal = new Stopwatch(); // Об'єкти для вимірювання часу
                    Thread[] threads = new Thread[threadsAmount]; // Масив потоків
                    SWtotal.Start();  // Початок вимірювання часу для всіх потоків

                    for (int i = 0; i < threads.Length; i++)
                    {
                        secondlimit = (i == threadsAmount - 1) ? size : (i + 1) * (array.Length / threadsAmount); // Розбиття масиву на частини
                        Thread t = new Thread(() => Sum(array, firstlimit, secondlimit)); // Створення потоку
                        threads[i] = t;
                        SWparts.Start();// Початок вимірювання часу для окремого потоку
                        t.Start(); // Запуск потоку
                        SWparts.Stop();
                        Console.WriteLine("Для потоку " + (i + 1) + " час виконання =" + SWparts.Elapsed.ToString());
                        SWparts.Reset(); // Зупинка вимірювання часу для окремого потоку та виведення результатів
                        firstlimit = secondlimit;// Оновлення значень лімітів для розбиття масиву на частини
                    }
                    SWtotal.Stop();  // Зупинка вимірювання часу для всіх потоків та виведення результатів
                    Console.WriteLine($"\nЗагальна сума чисел= {total},час виконання обчислення для масиву розміром {size} елементів = {SWtotal.Elapsed}");
                    Console.ReadKey();
                }
            }
        }
        static void Sum(int[] array, int firstlimit, int secondlimit)  // Функція для обчислення суми елементів масиву у заданих межах
        {
            for (int i = firstlimit; i < secondlimit; i++)
            {
                total += array[i];
            }
        }
    }
}
