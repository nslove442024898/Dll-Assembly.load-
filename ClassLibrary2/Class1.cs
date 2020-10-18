using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ClassLibrary1;

namespace ClassLibrary2
{
    public class Class
    {
        public double MyFunc(double a, double b)
        {
            return ClassLibrary1.mycls.MyAdd(a, b);
        }
    }
}
