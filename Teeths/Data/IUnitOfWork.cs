using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teeths.Data
{
    interface IUnitOfWork:IDisposable
    {
        Repository<T> Repository<T>() where T : class;
    }
}
