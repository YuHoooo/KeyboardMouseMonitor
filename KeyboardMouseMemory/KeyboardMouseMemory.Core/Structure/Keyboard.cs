using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardMouseMonitor.Core.Structure
{
    public struct Keyboard
    {
        public Keys Key;
        public bool Ctrl;
        public bool Alt;
        public bool Shift;
        public bool CapsLock;
        public Keyboard(Keys Key = Keys.None, bool Ctrl = false, bool Alt = false, bool Shift = false, bool CapsLock = false)
        {
            this.Key = Key;
            this.Ctrl = Ctrl;
            this.Alt = Alt;
            this.Shift = Shift;
            this.CapsLock = CapsLock;
        }

        public void Clear()
        {
            Key = Keys.None;
            Ctrl = false;
            Alt = false;
            Shift = false;
            CapsLock = false;
        }
    }
}
