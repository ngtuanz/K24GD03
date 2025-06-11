using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bai_tap
{
    internal class bieuthuc
    {
        public float tinhbieuthuc(int x, int y)
        {
            if (2 * x - y == 0)
            {
                throw new DivideByZeroException("Giá trị nhập vào làm cho mẫu bằng 0");
            }
            if((3 * x + 2 * y) / (2 * x - y) < 0)
            {
                throw new DivideByZeroException("Giá trị trong căn nhỏ hơn 0 nên không ra kết quả");
            }
            float H = (float)Math.Sqrt((3 * x + 2 * y) / (2 * x - y));
            return H;
        }
    }
}
