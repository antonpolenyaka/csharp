using System;

namespace ConsoleApp7
{
    class Program
    {
        public static event EventHandler OnTest;

        static void Main(string[] args)
        {
            Console.WriteLine("start");

            Class4 class4 = new Class4()
            {
                SubClass = new Class5() { Id = 4 }
            };

            if (class4?.SubClass?.Id != 4)
            {
                Console.WriteLine("!=4");
            }

            Console.ReadLine();
        }

        static void Main2(string[] args)
        {
            Console.WriteLine("start");

            Class3 class3 = new Class3();
            Class2 class2 = new Class2();
            Class1 class1 = new Class1();

            OnTest += class2.ExecuteOnTest;
            OnTest += class3.ExecuteOnTest;
            OnTest += class1.ExecuteOnTest;

            OnTest.Invoke(args, EventArgs.Empty);

            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine($"main {i} {DateTime.Now:HH:mm:ss.fff}");
            }

            Console.ReadLine();
        }
    }
}
