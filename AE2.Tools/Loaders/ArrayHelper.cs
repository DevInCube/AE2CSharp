using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE2.Tools.Loaders
{
    public static class ArrayHelper
    {

        public static T[][] createArray<T>(int n, int m)
        {
            T[][] arr = new T[n][];
            for (int i = 0; i < n; i++)
                arr[i] = new T[m];
            return arr;
        }
    }
}
