using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB01
{
    internal class Program
    {
        
        
        static void Main(string[] args)
        {
            Tong tong = new Tong();
            Hieu hieu = new Hieu();
            Tich tich = new Tich();
            Thuong thuong = new Thuong();

            Console.OutputEncoding = Encoding.UTF8;
            Console.Write("Nhập số a: ");
            double a = Convert.ToDouble(Console.ReadLine());
            Console.Write("Nhập số b: ");
            double b = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine($"Tổng hai số a và b: {tong.Tong2s(a, b)} ");
            Console.WriteLine($"Hiệu hai số a và b: {hieu.Hieu2s(a, b)}");
            Console.WriteLine($"Tích hai số a và b: {tich.Tich2s(a,b)}");  
            if (b == 0)
            {
                Console.WriteLine("b không hợp lệ , b phải lớn hơn 0");
            }
            else
            {
                Console.WriteLine($"Thương hai số a và b: {thuong.Thuong2s(a, b)}");
            }
        }
    }
}
