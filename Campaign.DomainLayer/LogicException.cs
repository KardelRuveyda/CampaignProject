using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.DomainLayer
{
    public class LogicException:Exception
    {
        public LogicException(string message): base(message)
        {

        }
    }
}
