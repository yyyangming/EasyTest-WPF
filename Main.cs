using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
    [Serializable]
    class FormStatus
    {
        public bool JogOpen { get; set; }

       
    }
}
