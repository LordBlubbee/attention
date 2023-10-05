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
    public class RemoteDataStore : IDataStore<Pet>
    {
        public bool CreateItem(Pet item)
        {
            return false;
        }
        public Pet ReadItem()
        {

            return new Pet();
        }
        public bool UpdateItem(Pet item)
        {
            return false;
        }
        public bool DeleteItem(Pet item)
        {
            return false;
        }
    }
}