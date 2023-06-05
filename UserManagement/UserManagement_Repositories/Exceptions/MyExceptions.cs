using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement_Repositories.Exceptions
{
    [Serializable]
    public class MyExceptions : Exception
    {
        public MyExceptions()
        {
        }

        public MyExceptions(string? message) : base(message)
        {
        }
    }
}
