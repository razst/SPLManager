using System;
using System.Collections.Generic;

namespace SPL_Manager.Library.Shared
{
    public static class GeneralExtentions
    {
        public static void Swap<T>(this IList<T> list, int index1, int index2)
        {
            T temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }

        public static dynamic Operator(this string logic, dynamic x, dynamic y)
        {
            return logic switch
            {
                ">" => x > y,
                "<" => x < y,
                "==" => x == y,
                "=" => x == y,
                "!=" => x != y,
                "-" => x - y,
                "+" => x + y,
                _ => throw new Exception("invalid logic"),
            };
        }
    }
}