using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bai_tap
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bieuthuc tinh = new bieuthuc();
            Console.OutputEncoding = Encoding.UTF8;
            try
            {
                Console.Write("Nhập x: ");
                int x = int.Parse(Console.ReadLine());
                Console.Write("Nhập y: ");
                int y = int.Parse(Console.ReadLine());
                Console.WriteLine($"Kết quả của phép toán là:  + {tinh.tinhbieuthuc(x, y)}");
            }

            catch (DivideByZeroException ex)
            {
                Console.WriteLine("DivideByZeroException:" + ex.Message);
                Console.WriteLine("DivideByZeroException:" + ex.StackTrace);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("FormatException:" + ex.Message);
                Console.WriteLine("FormatException:" + ex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception test:" + ex.Message);
                Console.WriteLine("Exception test:" + ex.StackTrace);
            }
            finally
            {
                Console.WriteLine("Kết thúc chương trình lệnh");
            }
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}
