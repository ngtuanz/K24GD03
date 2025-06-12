using System;
using System.Collections.Generic;
using System.Security;

namespace LAB03
{
    class Program
    {

        static void Main(string[] args)
        {
            Random rnd = new Random();
            Console.Write("Number: ");
            int n = int.Parse(Console.ReadLine());
            if (n > 0)
            {
                rnd.RandomNum(n);
            }
            else
            {
                Console.WriteLine("Number > 0.");
            }
        }
    }
}