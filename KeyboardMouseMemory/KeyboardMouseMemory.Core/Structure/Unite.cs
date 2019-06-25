using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardMouseMonitor.Core.Structure
{
    public struct Unite
    {
        public Keyboard Keyboard;
        public Mouse Mouse;
        public Unite(Mouse Mouse, Keyboard Keyboard)
        {
            this.Keyboard = Keyboard;
            this.Mouse = Mouse;
        }
    }
}
