using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class EnumerableHelper
    {
        public static List<T> ArrayToList<T>(T[] array)
        {
            if (array == null)
            {
                return new List<T>();
            }
            return array.ToList();
        }

        public static T[] ListToArray<T>(List<T> list)
        {
            if (list == null)
            {
                return new T[0];
            }
            return list.ToArray();
        }
    }
}
