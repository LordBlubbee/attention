using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ATTENTION.Resources.Scripts
{
    public interface IDataStore<T>
    {
        public bool CreateItem(T item);
        public T ReadItem();
        public bool UpdateItem(T item);
        public bool DeleteItem(T item);
    }
}