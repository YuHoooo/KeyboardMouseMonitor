using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardMouseMonitor.Core.Interface
{
    public interface IOperation : IDisposable
    {
        bool Start();
        bool Stop();
        bool Suspend();
        bool Continue();
    }

    public abstract class Operation
    {

    }
}
