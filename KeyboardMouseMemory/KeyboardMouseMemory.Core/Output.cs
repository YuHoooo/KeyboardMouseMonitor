using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KeyboardMouseMonitor.Core.Hook;
using System.Runtime.InteropServices;
using System.Threading;

namespace KeyboardMouseMonitor.Core
{
    public class Output : Interface.IOperation
    {
        Task m_Task;
        List<Structure.Unite> m_Data;
        CancellationTokenSource m_Task_cts;
        public Output()
        {
            m_Task = new Task(SendData);
            m_Task_cts = new CancellationTokenSource();
        }

        ~Output()
        {
            Stop();
            Dispose();
        }

        public void CurrentlyData(List<Structure.Unite> data)
        {
            m_Data = data;
        }

        public bool Continue()
        {
            try
            {
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
        }

        public bool Start()
        {
            try
            {
                m_Task.Start();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Stop()
        {
            try
            {
                m_Task_cts.Cancel();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Suspend()
        {
            try
            {
                return true;
            }
            catch
            {
                return false;
            }
        }

        void SendData()
        {
            for(int i = 0; i < m_Data.Count; ++i)
            {
                if(m_Task_cts.Token.IsCancellationRequested)
                {
                    break;
                }
                HookRelated.tagMouseInput mouse = new HookRelated.tagMouseInput();
                mouse.dx = m_Data[i].Mouse.mousePosition.X * (65535 / Screen.PrimaryScreen.WorkingArea.Width);
                mouse.dy = m_Data[i].Mouse.mousePosition.Y * (65535 / Screen.PrimaryScreen.WorkingArea.Height);
                mouse.Mousedata = m_Data[i].Mouse.mouseWheel;
                switch (m_Data[i].Mouse.mouseButton)
                {
                    case (int)HookRelated.MouseConduct.WM_NONE:
                        mouse.dwFlag = (int)HookRelated.MouseSendConduct.MOUSEEVENTF_ABSOLUTE | (int)HookRelated.MouseSendConduct.MOUSEEVENTF_MOVE;
                        break;
                    case (int)HookRelated.MouseConduct.WM_LBUTTONDOWN:
                        mouse.dwFlag = (int)HookRelated.MouseSendConduct.MOUSEEVENTF_ABSOLUTE | (int)HookRelated.MouseSendConduct.MOUSEEVENTF_LEFTDOWN;
                        break;
                    case (int)HookRelated.MouseConduct.WM_LBUTTONUP:
                        mouse.dwFlag = (int)HookRelated.MouseSendConduct.MOUSEEVENTF_ABSOLUTE | (int)HookRelated.MouseSendConduct.MOUSEEVENTF_LEFTUP;
                        break;
                    case (int)HookRelated.MouseConduct.WM_MBUTTONDOWN:
                        mouse.dwFlag = (int)HookRelated.MouseSendConduct.MOUSEEVENTF_ABSOLUTE | (int)HookRelated.MouseSendConduct.MOUSEEVENTF_MIDDLEDOWN;
                        break;
                    case (int)HookRelated.MouseConduct.WM_MBUTTONUP:
                        mouse.dwFlag = (int)HookRelated.MouseSendConduct.MOUSEEVENTF_ABSOLUTE | (int)HookRelated.MouseSendConduct.MOUSEEVENTF_MIDDLEUP;
                        break;
                    case (int)HookRelated.MouseConduct.WM_RBUTTONDOWN:
                        mouse.dwFlag = (int)HookRelated.MouseSendConduct.MOUSEEVENTF_ABSOLUTE | (int)HookRelated.MouseSendConduct.MOUSEEVENTF_RIGHTDOWN;
                        break;
                    case (int)HookRelated.MouseConduct.WM_RBUTTONUP:
                        mouse.dwFlag = (int)HookRelated.MouseSendConduct.MOUSEEVENTF_ABSOLUTE | (int)HookRelated.MouseSendConduct.MOUSEEVENTF_RIGHTUP;
                        break;
                }
                HookRelated.Input[] input = new HookRelated.Input[7];
                input[0].type = 0;
                input[0].mi = mouse;
                HookRelated.tagKEYBDINPUT keyboard = new HookRelated.tagKEYBDINPUT();
                if (m_Data[i].Keyboard.Ctrl)
                {
                    keyboard.wVk = (short)Keys.ControlKey;
                    input[1].type = input[4].type = 1;
                    input[1].ki = input[4].ki = keyboard;
                    input[4].ki.dwFlags = 0x0002;//释放
                }
                if (m_Data[i].Keyboard.Alt)
                {
                    keyboard.wVk = (short)Keys.Menu;
                    input[2].type = input[5].type = 1;
                    input[2].ki = input[5].ki = keyboard;
                    input[5].ki.dwFlags = 0x0002;
                }
                keyboard.wVk = (short)m_Data[i].Keyboard.Key;
                input[3].type = input[6].type = 0;
                input[3].ki = input[6].ki = keyboard;
                HookRelated.SendInput(7, input, Marshal.SizeOf(input[0].GetType()));
                Thread.Sleep(10);
            }
        }

    }
}
