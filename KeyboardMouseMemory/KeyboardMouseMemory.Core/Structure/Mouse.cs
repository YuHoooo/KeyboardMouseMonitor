using KeyboardMouseMonitor.Core.Hook;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardMouseMonitor.Core.Structure
{
    public struct Mouse
    {
        public Point mousePosition;
        public int mouseButton;         //HookRelated.MouseConduct
        public int mouseWheel;          //滚轮朝自己的方向为-
        public Mouse(Point mousePosition, int mouseButton = 0, int mouseWheel = 0)
        {
            this.mousePosition = mousePosition;
            this.mouseButton = mouseButton;
            this.mouseWheel = mouseWheel;
        }

        public void Clear()
        {
            mousePosition = new Point();
            mouseButton = 0;
            mouseWheel = 0;
        }
    }
}
