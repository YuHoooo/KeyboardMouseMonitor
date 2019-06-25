using KeyboardMouseMonitor.Core;
using KeyboardMouseMonitor.Core.Hook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardMouseMonitor.UI
{
    public partial class Form1 : Form
    {
        Input m_Input;
        Output m_Output;
        delegate void ControlEventHandle(Input data);
        CancellationTokenSource m_cts = new CancellationTokenSource();

        public Form1()
        {
            InitializeComponent();
            m_Input = new Input();
            m_Output = new Output();
        }

        void GetData(Input data)
        {
            label1.Text = string.Format("x:{0}, y:{1}", data.CurrentlyData.Mouse.mousePosition.X.ToString(), data.CurrentlyData.Mouse.mousePosition.Y.ToString());
            label2.ForeColor = data.CurrentlyData.Mouse.mouseButton == (int)HookRelated.MouseConduct.WM_LBUTTONDOWN ? Color.Red : Color.Black;
            label3.ForeColor = data.CurrentlyData.Mouse.mouseButton == (int)HookRelated.MouseConduct.WM_MBUTTONDOWN ? Color.Red : Color.Black;
            label4.ForeColor = data.CurrentlyData.Mouse.mouseButton == (int)HookRelated.MouseConduct.WM_RBUTTONDOWN ? Color.Red : Color.Black;
            label5.ForeColor = data.CurrentlyData.Mouse.mouseWheel > 0 ? Color.Red : Color.Black;
            label6.ForeColor = data.CurrentlyData.Mouse.mouseWheel < 0 ? Color.Red : Color.Black;
            labCaps.ForeColor = data.CurrentlyData.Keyboard.CapsLock == true ? Color.Red : Color.Black;
            labShift.ForeColor = data.CurrentlyData.Keyboard.Shift == true ? Color.Red : Color.Black;
            labCtrl.ForeColor = data.CurrentlyData.Keyboard.Ctrl == true ? Color.Red : Color.Black;
            labAlt.ForeColor = data.CurrentlyData.Keyboard.Alt == true ? Color.Red : Color.Black;
            labKey.ForeColor = data.CurrentlyData.Keyboard.Key != Keys.None ? Color.Red : Color.Black;
            labKey.Text = data.CurrentlyData.Keyboard.Key.ToString();
            string str = data.CurrentlyData.Mouse.mousePosition.X.ToString() + "," + data.CurrentlyData.Mouse.mousePosition.Y.ToString()
                + " " + data.CurrentlyData.Mouse.mouseButton.ToString() + " " + data.CurrentlyData.Keyboard.Key.ToString() + " " + data.CurrentlyData.Mouse.mouseWheel.ToString();
            listBox1.Items.Add(str);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            listBox1.SelectedIndex = -1;
        }

        void Work(Input data)
        {
            ControlEventHandle dele = GetData;
            while (true)
            {
                //Invoke(dele, new object[] { data });
                BeginInvoke(dele, new object[] { data });
                Thread.Sleep(100);
            }
        }
        delegate void handel(string data);

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_Input.Stop();
            m_Input.Dispose();
            m_Output.Stop();
            m_Output.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            m_Input.Start();
            Thread.Sleep(100);
            Task t = new Task(() => Work(m_Input));
            t.Start();
        }
    }
}
