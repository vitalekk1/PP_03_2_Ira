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
        static List<int> listSimpleNumber;
        static List<int> listPolindromNumber;
        static bool isBreak = false;
        static int n;
        static void Main(string[] args)
        {
            Console.WriteLine("Введите N:");
            n = Convert.ToInt32(Console.ReadLine());
            listSimpleNumber = new List<int>();
            listPolindromNumber = new List<int>();

            lock ((object)listSimpleNumber)
            {
                Thread thCheckSimpleNumber = new Thread(IsPrimeNumber);
                Thread thCheckPolindromNumber = new Thread(checkPolindrom);

                thCheckSimpleNumber.Start();
                thCheckPolindromNumber.Start();

                thCheckSimpleNumber.Join();
                thCheckPolindromNumber.Join();
            }

            Console.ReadKey();

        }

        private static void checkPolindrom()
        {
            int num; int temp; int template;
            while (!isBreak || (listSimpleNumber.Count > 0))
            {
                template = temp = listSimpleNumber.First(); num = 0;
                while (temp != 0)
                {
                    num = num * 10 + (temp % 10);
                    temp /= 10;
                }
                try
                {
                    if (num == template)
                    {
                        listPolindromNumber.Add(template);
                    }
                    listSimpleNumber.RemoveAt(0);
                }
                catch (Exception ex) { }
            }

            foreach (int i in listPolindromNumber)
            {
                Console.WriteLine(i);
            }
        }

        private static void IsPrimeNumber()
        {
            bool check = true;
            for (int i = 2; i <= n; i++)
            {
                check = true;
                int sqrtNumber = (int)(Math.Sqrt(i));
                for (int j = 2; j <= sqrtNumber; j++)
                {
                    if (i % j == 0)
                    {
                        check = false;
                        break;
                    }
                }
                if (check)
                {
                    listSimpleNumber.Add(i);
                }
            }
            isBreak = true;
        }
    }
}
