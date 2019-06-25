using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardMouseMonitor.Core.Hook
{
    public class HookRelated
    {
        public enum MouseConduct
        {
            WM_NONE = 0,
            WM_MOUSEMOVE = 0x0200, //移动鼠标 
            WM_LBUTTONDOWN = 0x0201, //按下鼠标左键 
            WM_LBUTTONUP = 0x0202, //释放鼠标左键 
            WM_LBUTTONDBLCLK = 0x0203, //双击鼠标左键 
            WM_RBUTTONDOWN = 0x0204, //按下鼠标右键 
            WM_RBUTTONUP = 0x0205, //释放鼠标右键 
            WM_RBUTTONDBLCLK = 0x0206, //双击鼠标右键 
            WM_MBUTTONDOWN = 0x0207, //按下鼠标中键 
            WM_MBUTTONUP = 0x0208, //释放鼠标中键 
            WM_MBUTTONDBLCLK = 0x0209, //双击鼠标中键 
            WM_MOUSEWHEEL = 0x020A //当鼠标轮子转动时 
        }

        public enum KeyboardConduct
        {
            WM_KEYDOWN = 0x100,
            WM_KEYUP = 0x101,
            WM_SYSKEYDOWN = 0x104,
            WM_SYSKEYUP = 0x105
        }

        public enum KeyboardASCII
        {
            BackSpace = 8,
            Tab,
            Clear = 12,
            Enter,
            Shift = 16,
            Control,
            Alt,
            CapeLock = 20,
            ESC = 27,
            Spacebar = 32,
            PageUp,
            PageDown,
            End,
            Home,
            LeftArrow,
            UpArrow,
            RightArrow,
            DownArrow,
            Insert = 45,
            Delete,
            NumLock = 144,
            Colon = 186,        // 冒号、分号 :;
            Equal,              // =+
            Sub,                // -_
            Dot,                // .>
            LeftSlash,          // /?
            GraveAccent,        // `~
            LeftBracket,        // [{
            RightSlash,         // \|
            RightBracket        // ]}
        }

        public enum HookConduct
        {
            WH_MSGFILTER = -1,
            WH_JOURNALRECORD = 0,
            WH_JOURNALPLAYBACK,
            WH_KEYBOARD,
            WH_GETMESSAGE,
            WH_CALLWNDPROC,
            WH_CBT,
            WH_SYSMSGFILTER,
            WH_MOUSE,
            WH_DEBUG = 9,
            WH_SHELL,
            WH_FOREGROUNDIDLE,
            WH_CALLWNDPROCRET,
            WH_KEYBOARD_LL,
            WH_MOUSE_LL
        }

        public enum MouseSendConduct
        {
            MOUSEEVENTF_MOVE = 1,
            MOUSEEVENTF_LEFTDOWN,
            MOUSEEVENTF_LEFTUP = 4,
            MOUSEEVENTF_RIGHTDOWN = 8,
            MOUSEEVENTF_RIGHTUP = 10,
            MOUSEEVENTF_MIDDLEDOWN = 0x0020,
            MOUSEEVENTF_MIDDLEUP = 0x0040,
            MOUSEEVENTF_XDOWN = 0x0080,
            MOUSEEVENTF_XUP = 0x0100,
            MOUSEEVENTF_WHEEL = 0x0800,
            MOUSEEVENTF_HWHEEL = 0x01000,
            MOUSEEVENTF_MOVE_NOCOALESCE = 0x2000,
            MOUSEEVENTF_VIRTUALDESK = 0x4000,
            MOUSEEVENTF_ABSOLUTE = 0x8000
        }

        /// <summary>
        /// 鼠标滑轮
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct MSLLHOOKSTRUCT
        {
            public Point Point;
            public int MouseData;
            public int Flags;
            public int Time;
            public int ExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class MouseHookStruct
        {
            public Point pt;
            public int hwnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }

        #region 模拟键鼠
        [StructLayout(LayoutKind.Explicit)]

        public struct Input

        {
            [FieldOffset(0)]
            public int type; //0-鼠标  1-键盘   2-硬件
            [FieldOffset(4)]
            public tagMouseInput mi;
            [FieldOffset(4)]
            public tagKEYBDINPUT ki;
            [FieldOffset(4)]
            public tagHARDWAREINPUT info;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct tagMouseInput
        {
            public int dx;
            public int dy;
            public int Mousedata;
            public int dwFlag;
            public int time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct tagKEYBDINPUT
        {
            public short wVk;
            public short wScan;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct tagHARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }
        #endregion

        /// <summary>
        /// 钩子委托声明
        /// </summary>
        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 键盘Hook结构函数
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class KeyboardHookStruct
        {
            public int vkCode;  //定一个虚拟键码。该代码必须有一个价值的范围1至254
            public int scanCode; // 指定的硬件扫描码的关键
            public int flags;  // 键标志
            public int time; // 指定的时间戳记的这个讯息
            public int dwExtraInfo; // 指定额外信息相关的信息
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class WINDOWINFO
        {
            public int structSize;
            public RECT windowRect;
            public RECT ClientRect;
            public uint Style;
            public uint ExStyle;
            public uint WindowStatus;
            public uint width;
            public uint height;
            public ushort AtomWindowType;
            public ushort CreatorVersion;
        }

        /// <summary>
        /// 设置钩子
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        /// <summary>
        /// 取消钩子
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        /// <summary>
        /// 往下传递信息，调用下一个钩子
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 取得模块句柄 
        /// </summary>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        /// <summary>
        /// 寻找目标进程窗口
        /// </summary>
        [DllImport("user32.DLL")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 设置进程窗口到最前 
        /// </summary>
        [DllImport("user32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// 模拟键盘事件 
        /// </summary>
        [DllImport("user32.dll")]
        public static extern void keybd_event(Byte bVk, Byte bScan, Int32 dwFlags, Int32 dwExtraInfo);

        /// <summary>
        /// 判定窗体特征
        /// </summary>
        [DllImport("user32.dll")]
        public static extern int GetWindowInfo(IntPtr hWnd, ref WINDOWINFO pwi);

        /// <summary>
        /// 查找窗体信息
        /// </summary>
        [DllImport("user32.dll")]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        /// <summary>
        /// 发送信息
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        /// <summary>
        /// ToAscii职能的转换指定的虚拟键码和键盘状态的相应字符或字符
        /// </summary>
        [DllImport("user32")]
        public static extern int ToAscii(int uVirtKey, //[in] 指定虚拟关键代码进行翻译。
                                         int uScanCode, // [in] 指定的硬件扫描码的关键须翻译成英文。高阶位的这个值设定的关键，如果是（不压）
                                         byte[] lpbKeyState, // [in] 指针，以256字节数组，包含当前键盘的状态。每个元素（字节）的数组包含状态的一个关键。
                                                             // 如果高阶位的字节是一套，关键是下跌（按下）。在低比特，如果设置表明，关键是对切换。在此功能，只有肘位的CAPS LOCK键是相关的。在切换状态的NUM个锁和滚动锁定键被忽略。
                                         byte[] lpwTransKey, // [out] 指针的缓冲区收到翻译字符或字符。
                                         int fuState); // [in] Specifies whether a menu is active. This parameter must be 1 if a menu is active, or 0 otherwise.
        /// <summary>
        /// 获取按键的状态
        /// </summary>
        [DllImport("user32")]
        public static extern int GetKeyboardState(byte[] pbKeyState);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern short GetKeyState(int vKey);

        //从指定内存中读取字节集数据
        [DllImportAttribute("kernel32.dll", EntryPoint = "ReadProcessMonitor")]
        public static extern bool ReadProcessMonitor(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, IntPtr lpNumberOfBytesRead);

        //从指定内存中写入字节集数据
        [DllImportAttribute("kernel32.dll", EntryPoint = "WriteProcessMonitor")]
        public static extern bool WriteProcessMonitor(IntPtr hProcess, IntPtr lpBaseAddress, int[] lpBuffer, int nSize, IntPtr lpNumberOfBytesWritten);

        //打开一个已存在的进程对象，并返回进程的句柄
        [DllImportAttribute("kernel32.dll", EntryPoint = "OpenProcess")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        //关闭一个内核对象。其中包括文件、文件映射、进程、线程、安全和同步对象等。
        [DllImport("kernel32.dll")]
        private static extern void CloseHandle(IntPtr hObject);

        /// <summary>
        /// 模拟键鼠
        /// </summary>
        [DllImport("user32.dll")]
        public static extern uint SendInput(uint nInputs, Input[] pInputs, int cbSize);
    }
}
