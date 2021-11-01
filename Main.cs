using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Main
    {
        private static int _OperationAuthority;
        public int OperationAuthority
        {
            get { return _OperationAuthority; }
            set { _OperationAuthority = value; }
        }
    }
}
