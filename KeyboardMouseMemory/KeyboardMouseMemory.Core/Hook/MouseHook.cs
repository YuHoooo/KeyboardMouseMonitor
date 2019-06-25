using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardMouseMonitor.Core.Hook
{
    public class MouseHook
    {
        HookRelated.HookProc m_hProc;
        int m_Code;
        public delegate void MouseInputEventHandle(Structure.Mouse keyboard);
        public event MouseInputEventHandle MouseIputEvent;

        public MouseHook()
        {
        }

        public int SetHook()
        {
            m_hProc = new HookRelated.HookProc(RememberMouse);
            m_Code = HookRelated.SetWindowsHookEx((int)HookRelated.HookConduct.WH_MOUSE_LL, m_hProc, IntPtr.Zero, 0);
            return m_Code;
        }

        public bool UnHook()
        {
            return HookRelated.UnhookWindowsHookEx(m_Code);
        }

        int RememberMouse(int code, IntPtr wParam, IntPtr lParam)
        {//只能在鼠标活动触发，静止不会触发
            if (code >= 0)
            {
                HookRelated.MSLLHOOKSTRUCT mouseHookStruct = (HookRelated.MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(HookRelated.MSLLHOOKSTRUCT));
                //MouseButtons mb = MouseButtons.None;
                //int delta = 0;
                int wheel = 0;
                switch ((int)wParam)
                {
                    //case (int)HookRelated.MouseConduct.WM_LBUTTONDOWN:
                    //    mb = MouseButtons.Left;
                    //    break;
                    //case (int)HookRelated.MouseConduct.WM_MBUTTONDOWN:
                    //    mb = MouseButtons.Middle;
                    //    break;
                    //case (int)HookRelated.MouseConduct.WM_RBUTTONDOWN:
                    //    mb = MouseButtons.Right;
                    //    break;
                    case (int)HookRelated.MouseConduct.WM_MOUSEWHEEL:
                        //此处只能获得滚轮方向，无法获得滚轮数据。可重写Form的OnMouseWheel事件来获得数据
                        //delta = mouseHookStruct.MouseData / 65536;
                        //delta = (short)((mouseHookStruct.MouseData >> 16) & 0xffff);
                        if (mouseHookStruct.MouseData > 0)
                            ++wheel;
                        else
                            --wheel;
                        break;
                }
                MouseIputEvent(new Structure.Mouse(mouseHookStruct.Point, (int)wParam, wheel));
            }
            return HookRelated.CallNextHookEx(m_Code, code, wParam, lParam);
        }
        
    }
}
