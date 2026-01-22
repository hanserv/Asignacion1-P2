using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystem.Persistence
{
    internal interface IPersistence<T>
    {
        public void Save(List<T>data);
        public List<T> Load();
    }
}
