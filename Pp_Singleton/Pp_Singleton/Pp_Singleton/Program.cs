using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pp_Singleton
{
    class Singleton
    {
        public static int Count;
        private Singleton()
        {
            Count = 0;
        }
        private static Singleton _instance;
        public static Singleton Getinstance()
        {
            if (_instance == null)
            {
                return new Singleton();
            }
            return _instance;
        }
        public static void someBusinessLogic()
        {
            Count++;
            Console.WriteLine($"Количество обращений = {Count}.");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Singleton s1 = Singleton.Getinstance();
            Singleton s2 = Singleton.Getinstance();
            if (s1 == s2)
            {
                Console.WriteLine("Singleton works.");
            }
            else
            {
                Console.WriteLine("Singleton failed.");
            }
            Singleton.someBusinessLogic();
            Singleton.someBusinessLogic();
            Console.ReadLine();

        }
    }
}
