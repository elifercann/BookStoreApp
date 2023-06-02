using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public abstract class BadRequestException:Exception
    {
        //abstract class newlenemedigi icin ctor protected yapıldı
        protected BadRequestException(string message):base(message)
        {
            
        }
    }
}
