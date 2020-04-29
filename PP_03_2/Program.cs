using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PP_03_2
{
    class Program
    {
        struct startAndEnd{
            public int start;
            public int end;
        }
        static List<int> listNumber;
        private static int countThread = 3;
        private static int h;
        static void Main(string[] args)
        {
            Console.WriteLine("Введите N:");
            int n = Convert.ToInt32(Console.ReadLine());
            listNumber = new List<int>();
            devideCountN(2, n, countThread);
            int endT = 2 + h;
            List<Thread> threads = new List<Thread>();
            int countTh = 0;

            for (int i = 2; i < n; i = i + h)
            {
                startAndEnd struct3 = new startAndEnd();    
                endT = i + h;
                if (endT > n) { endT = n; }
                struct3.start = i;
                struct3.end = endT;
                lock ((object)listNumber)
                {
                    threads.Add(new Thread(check));
                    threads[countTh].Start(struct3);
                    countTh++;
                }
            }

            foreach(Thread thread in threads)
            {
                thread.Join();
            }

            listNumber = listNumber.Distinct().ToList();

            foreach (int i in listNumber)
            {
                Console.WriteLine(i);
            }

            Console.ReadKey();

        }

        private static void check(object struc)
        {
            startAndEnd struct1 = (startAndEnd)struc;
            int start = struct1.start;
            int end = struct1.end;
            int num; int temp;
            for (int i = start; i <= end; i++)
            {
                temp = i; num = 0;
                while (temp != 0)
                {
                    num = num * 10 + (temp % 10);
                    temp /= 10;
                }
                try
                {
                    if (num == i) listNumber.Add(i);
                }
                catch (Exception ex) { }
                
            }
        }

        private static void devideCountN(double start, double end, int countN)
        {
            h =(int) Math.Round((end - start) / countN);
        }


    }
}
