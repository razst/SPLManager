
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;



namespace packet_maker
{

    public static class ExtensionMethods
    {

        public static void Swap(this ListBox.ObjectCollection list, int index1, int index2)
        {
            object temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }
        public static void Swap<T>(this List<T> list, int index1, int index2)
        {
            T temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }

        public static dynamic Operator(this string logic, dynamic x, dynamic y)
        {
            switch (logic)
            {
                case ">": return x > y;
                case "<": return x < y;
                case "==": return x == y;
                case "=": return x == y;
                case "!=": return x != y;
                case "-": return x - y;
                case "+": return x + y;
                default: throw new Exception("invalid logic");
            }
        }

        public static void AddRange<T>(this ICollection<T> target, IEnumerable<T> source)
        {
            foreach (var element in source)
                target.Add(element);
        }
    }
}

