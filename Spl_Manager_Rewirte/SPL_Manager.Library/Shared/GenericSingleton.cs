using System;
using System.Collections.Generic;
using System.Text;

namespace SPL_Manager.Library.Shared
{
    public class GenericSingleton<T>
        where T : class , new()
    {
        private static readonly Lazy<T> _instance = new Lazy<T>(() => new T());
        public static T Instance
        {
            get
            {
                return _instance.Value;
            }
        }
    }

}
