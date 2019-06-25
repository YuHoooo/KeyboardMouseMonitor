using KeyboardMouseMonitor.Core.Hook;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardMouseMonitor.Core
{
    public class Input : Interface.IOperation
    {
        bool m_Start;
        bool m_Suspend;
        Task m_Task;
        Structure.Mouse m_mData;
        MouseHook m_MouseHook;
        Structure.Keyboard m_kData;
        List<Structure.Unite> m_Data;
        Structure.Unite m_CurrentlyData;
        KeyboardHook m_KeyboardHook;
        CancellationTokenSource m_Task_cts;
        Dictionary<string, List<Structure.Unite>> m_AllData;

        public List<Structure.Unite> Data
        {
            get { return m_Data; }
        }

        public Structure.Unite CurrentlyData
        {
            get { return m_CurrentlyData; }
        }

        public Dictionary<string, List<Structure.Unite>> AllData
        {
            get { return m_AllData; }
        }

        public Input()
        {
            m_Start = false;
            m_Suspend = false;
            m_Data = new List<Structure.Unite>();
            m_Task = new Task(RecordData);
            m_Task_cts = new CancellationTokenSource();
            m_kData = new Structure.Keyboard();
            m_KeyboardHook = new Hook.KeyboardHook();
            m_KeyboardHook.KeyboardIputEvent += GetKeyboardData;
            m_MouseHook = new Hook.MouseHook();
            m_MouseHook.MouseIputEvent += GetMousedData;
            m_AllData = new Dictionary<string, List<Structure.Unite>>();
        }

        ~Input()
        {
            Stop();
            Dispose();
        }

        public void Dispose()
        {
            if (m_Task.Status == TaskStatus.Running)
                m_Task_cts.Cancel();
            //m_Task.Dispose();
            m_Task_cts.Dispose();
            //m_KeyboardHook.KeyboardIputEvent -= GetKeyboardData;
            //m_MouseHook.MouseIputEvent -= GetMousedData;
            //m_KeyboardHook = null;
            //m_MouseHook = null;
        }

        public bool Continue()
        {
            try
            {
                if (m_Suspend && m_Start)
                {
                    m_Task = new Task(RecordData);
                    m_Task.Start();
                    m_Suspend = false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Start()
        {
            try
            {
                if (!m_Start)
                {
                    m_KeyboardHook.SetHook();
                    m_MouseHook.SetHook();
                    m_Task.Start();
                    m_Start = true;
                }
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
                if (m_Start)
                {
                    m_Task_cts.Cancel();
                    m_KeyboardHook.UnHook();
                    m_MouseHook.UnHook();
                    for (int i = 0; i < m_Data.Count; ++i)
                    {//数据处理
                        if (m_Data[i].Mouse.mousePosition.X == 0
                            && m_Data[i].Mouse.mousePosition.Y == 0)
                        {
                            m_Data.RemoveAt(i);
                            --i;
                            continue;
                        }
                        if(i + 1 < m_Data.Count && m_Data[i].Mouse.mouseButton != (int)HookRelated.MouseConduct.WM_MOUSEMOVE
                            && m_Data[i].Mouse.mouseButton == m_Data[i + 1].Mouse.mouseButton)
                        {
                            m_Data.RemoveAt(i + 1);
                            --i;
                        }
                    }
                    List<Structure.Unite> temp = new List<Structure.Unite>(m_Data);
                    m_AllData.Add(DateTime.Now.ToString(), temp);
                    m_Data.Clear();
                    m_Start = false;
                }
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public bool Suspend()
        {
            try
            {
                if (!m_Suspend && m_Start)
                {
                    m_Task_cts.Cancel();
                    Thread.Sleep(100); //等待发送信号
                    m_Task_cts = new CancellationTokenSource();
                    m_Suspend = true;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        void RecordData()
        {
            while (true)
            {
                if (m_Task_cts.IsCancellationRequested || m_Task_cts.Token.IsCancellationRequested)
                    break;
                //m_mData = new Structure.Mouse(Control.MousePosition, Control.MouseButtons);//无法记录鼠标滚轮
                m_CurrentlyData = new Structure.Unite(m_mData, m_kData);
                m_Data.Add(new Structure.Unite(m_mData, m_kData));
                if (m_mData.mouseWheel != 0)
                {
                    Thread.Sleep(100);
                    m_mData.mouseWheel = 0;
                }
                Thread.Sleep(10);
            }
        }

        void GetKeyboardData(Structure.Keyboard keyboard)
        {
            m_kData = keyboard;
        }

        void GetMousedData(Structure.Mouse mouse)
        {
            m_mData = mouse;
        }

    }

}
