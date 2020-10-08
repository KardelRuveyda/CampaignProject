using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.AppLayer
{
    public interface ICommandManager
    {
        void Execute(string command, string[] objs);
        TimeSpan GetTime();
    }
}
