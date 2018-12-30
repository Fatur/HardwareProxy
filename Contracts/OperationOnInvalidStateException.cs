using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public class OperationOnInvalidStateException:ApplicationException
    {
        public OperationOnInvalidStateException(ShareStatus shareStatus)
            : base($"Operasi tidak bisa dijalankan pada state {shareStatus}") { }
       
    }
}
