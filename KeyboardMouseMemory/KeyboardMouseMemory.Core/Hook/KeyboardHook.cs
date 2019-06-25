using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardMouseMonitor.Core.Hook
{
    public class KeyboardHook
    {
        HookRelated.HookProc m_hProc;
        int m_Code;
        bool m_IsDownCapsLock = false;
        bool m_IsDownShift = false;
        bool m_IsDownCtrl = false;
        bool m_IsDownAlt = false;
        public delegate void KeyboardInputEventHandle(Structure.Keyboard keyboard);
        public event KeyboardInputEventHandle KeyboardIputEvent;

        public KeyboardHook()
        {
        }

        public int SetHook()
        {
            m_hProc = new HookRelated.HookProc(RememberKeyboard);
            m_Code = HookRelated.SetWindowsHookEx((int)HookRelated.HookConduct.WH_KEYBOARD_LL, m_hProc, IntPtr.Zero, 0);
            return m_Code;
        }

        public bool UnHook()
        {
            return HookRelated.UnhookWindowsHookEx(m_Code);
        }

        int RememberKeyboard(int code, IntPtr wParam, IntPtr lParam)
        {
            if(code >= 0)
            {

                HookRelated.KeyboardHookStruct keyboardHookStruct = 
                    (HookRelated.KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(HookRelated.KeyboardHookStruct));
                Keys key = (Keys)keyboardHookStruct.vkCode;
                Structure.Keyboard keyboardData = new Structure.Keyboard();
                byte[] ch = new byte[256];
                int a = Marshal.ReadInt32(lParam);
                Marshal.Copy(lParam, ch, 0, 255);
                switch ((int) wParam)
                {
                    case (int)HookRelated.KeyboardConduct.WM_SYSKEYDOWN:
                        //只有Alt在此
                        if (key == Keys.LMenu || key == Keys.RMenu)
                            m_IsDownAlt = true;
                        keyboardData = new Structure.Keyboard(key, m_IsDownCtrl, m_IsDownAlt, m_IsDownShift, m_IsDownCapsLock);
                        break;
                    case (int)HookRelated.KeyboardConduct.WM_SYSKEYUP:
                        //Alt+?按下,当?释放触发
                        break;
                    case (int)HookRelated.KeyboardConduct.WM_KEYDOWN:
                        if (key == Keys.LControlKey || key == Keys.RControlKey)
                            m_IsDownCtrl = true;
                        m_IsDownCapsLock = HookRelated.GetKeyState((int)HookRelated.KeyboardASCII.CapeLock) == 1 ? true : false;
                        m_IsDownShift = HookRelated.GetKeyState((int)HookRelated.KeyboardASCII.Shift) == 1 ? true : false;
                        byte[] keyState = new byte[256];
                        int n = HookRelated.GetKeyboardState(keyState);
                        byte[] inBuffer = new byte[2];
                        if(HookRelated.ToAscii(keyboardHookStruct.vkCode, keyboardHookStruct.scanCode, keyState, inBuffer, keyboardHookStruct.flags) == 1)
                        {
                            //KeyPressEventArgs e = new KeyPressEventArgs((char)inBuffer[0]);
                            keyboardData = new Structure.Keyboard(key, m_IsDownCtrl, m_IsDownAlt, m_IsDownShift, m_IsDownCapsLock);
                        }
                        //else
                        //{
                        //    if(m_IsDownCapsLock)
                        //        keyboardData = new Structure.Keyboard(Keys.CapsLock, m_IsDownCtrl, m_IsDownAlt, m_IsDownShift, m_IsDownCapsLock);
                        //    else if(m_IsDownCtrl)
                        //        keyboardData = new Structure.Keyboard(Keys.Control, m_IsDownCtrl, m_IsDownAlt, m_IsDownShift, m_IsDownCapsLock);
                        //    else if (m_IsDownShift)
                        //        keyboardData = new Structure.Keyboard(Keys.Shift, m_IsDownCtrl, m_IsDownAlt, m_IsDownShift, m_IsDownCapsLock);
                        //}
                        break;
                    case (int)HookRelated.KeyboardConduct.WM_KEYUP:
                        if (key == Keys.LControlKey || key == Keys.RControlKey)
                            m_IsDownCtrl = false;
                        if (key == Keys.LMenu || key == Keys.RMenu)
                            m_IsDownAlt = false;
                        break;
                }
                KeyboardIputEvent(keyboardData);
            }
            return HookRelated.CallNextHookEx(m_Code, code, wParam, lParam);
        }

    }
}
