using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB03
{
    public class Random
    {
        public void RandomNum(int n)
        {
            Console.WriteLine("Value: " + n);

            System.Random random = new System.Random();
            List<int> numbers = new List<int>();

            for (int i = 0; i < n; i++)
            {
                int value = random.Next(100);
                numbers.Add(value);
            }

            numbers.Sort();

            Console.WriteLine("List:");
            foreach (int number in numbers)
            {
                Console.Write(number + " ");
            }
        }
    }
}