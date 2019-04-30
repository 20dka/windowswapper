using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NonInvasiveKeyboardHookLibrary;
using SimWinInput;
using System.Threading;

namespace swapWindows
{
    public partial class Form1 : Form
    {
        KeyboardHookManager keyboardHookManager = new KeyboardHookManager();

        MyUserSettings mus;

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int X);
        
        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool SetFocus(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mus = new MyUserSettings();

            numericX.Value = mus.secondX;
            numericY.Value = mus.secondY;

            keyboardHookManager.Start();
            keyboardHookManager.RegisterHotkey(NonInvasiveKeyboardHookLibrary.ModifierKeys.Alt, 0x60, () =>
            {
                Debug.WriteLine("Alt+NumPad0 detected");

                
                //click screen
                SimMouse.Click(MouseButtons.Left, (int)numericX.Value, (int)numericY.Value ); //right minus
                Thread.Sleep(100);

                //get active app handle
                IntPtr handle1 = GetForegroundWindow();

                //move app
                ShowWindow(handle1, 1);
                MoveWindow(handle1, 200, 200, 500, 500, true);


                //-------------------------------------

                //click screen
                SimMouse.Click(MouseButtons.Left, 1780, 0); //left minus
                Thread.Sleep(100);

                //get active app handle
                IntPtr handle2 = GetForegroundWindow();

                //move app
                ShowWindow(handle2, 1);
                MoveWindow(handle2, 2120, 363, 500, 500, true);

                
                ShowWindowAsync(handle1, 3);
                ShowWindowAsync(handle2, 3);
                

                /*
                // left to right
                SimMouse.Click(MouseButtons.Left, 1780, 0);
                Thread.Sleep(1000);
                SimMouse.Act(SimMouse.Action.LeftButtonDown, 1780, 0);
                Thread.Sleep(1000);
                SimMouse.Act(SimMouse.Action.LeftButtonUp, 1780, 200);
                Thread.Sleep(1000);
                SimMouse.Act(SimMouse.Action.LeftButtonDown, 1780, 200);
                Thread.Sleep(1000);
                SimMouse.Act(SimMouse.Action.LeftButtonUp, 2780, 200);
                Thread.Sleep(1000);
                SimMouse.Click(MouseButtons.Left, 3057, 163);
                Thread.Sleep(1000);

                //right to left
                SimMouse.Act(SimMouse.Action.LeftButtonDown, 3057, 163);
                Thread.Sleep(100);
                SimMouse.Act(SimMouse.Action.LeftButtonUp, 3057, 363);
                Thread.Sleep(100);
                SimMouse.Act(SimMouse.Action.LeftButtonDown, 3057, 363);
                Thread.Sleep(100);
                SimMouse.Act(SimMouse.Action.LeftButtonUp, 1780, 363);
                */

            });
            notifyIcon1.Visible = true;
            timer1.Stop();
            this.Hide();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            this.Text = string.Format("X: {0}  Y: {1}", Cursor.Position.X, Cursor.Position.Y);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
                timer1.Stop();
            }
        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
            timer1.Start();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            mus.secondX = numericX.Value;
            mus.secondY = numericY.Value;

            mus.Save();
        }
    }
}
