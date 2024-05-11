using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace custosWorker.Forms
{
    public partial class BroadCast : Form
    {

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        public BroadCast(string title, string message)
        {
            InitializeComponent();
            label1.Text = title;
            richTextBox1.Text = message;
            TopMost = true;
            SetWindowPos(this.Handle, HWND_TOPMOST, 100, 100, 300, 300, TOPMOST_FLAGS);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
