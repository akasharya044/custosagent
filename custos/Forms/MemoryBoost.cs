using custos.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace custos.Forms
{
    public partial class MemoryBoost : Form
    {

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetProcessWorkingSetSize(IntPtr process, IntPtr minimumWorkingSetSize, IntPtr maximumWorkingSetSize);

        CommonMethod commonmethod = new CommonMethod();
        public MemoryBoost()
        {
            InitializeComponent();
        }

        private async void memory_Load(object sender, EventArgs e)
        {

            label3.Text = string.Empty;
            await Task.Delay(10000);
            MemoryBoosts();

        }

        public async void MemoryBoosts()
        {
            try
            {
                Events events = new Events();
                events.Event = "Memory Boost";
                events.EventDate = DateTime.Now;
                events.SystemId = System.Environment.MachineName;
                commonmethod.EventLog(events);



                try
                {


                    DisplayMemoryInformation("Current Memory Consumption:");
                    ClearStandbyList();
                    label3.ForeColor = Color.Red;
                    pictureBox2.Visible= false;
                    await Task.Delay(5000);
                    label3.ForeColor = Color.Green;
                    pictureBox2.Visible = false;
                    DisplayMemoryInformation("Memory Consumption After Optimization:");
                    label2.ForeColor = Color.Blue;
                    label2.Text = "Memory Boost Done";
                    
                    pictureBox2.Visible = false;

                }
                catch (Exception ex)
                {
                   // MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
            }

        }
        public void DisplayMemoryInformation(string message)
        {
            string totalMemory = $"Total Physical Memory: {GetTotalPhysicalMemory()} bytes";
            string availableMemory = $"Available Physical Memory: {GetAvailablePhysicalMemory()} bytes";

            //MessageBox.Show($"{message}\n\n{totalMemory}\n\n{availableMemory}", "Memory Information", MessageBoxButtons.OK, MessageBoxIcon.Information);



            label3.Text = "" + message + "\n\n" + totalMemory + "\n\n" + availableMemory;
        }
        static ulong GetTotalPhysicalMemory()
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    return Convert.ToUInt64(obj["TotalPhysicalMemory"]);
                }
            }
            return 0;
        }
        static ulong GetAvailablePhysicalMemory()
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    return Convert.ToUInt64(obj["FreePhysicalMemory"]);
                }
            }
            return 0;
        }
        static void ClearStandbyList()
        {
            Process currentProcess = Process.GetCurrentProcess();
            SetProcessWorkingSetSize(currentProcess.Handle, (IntPtr)(-1), (IntPtr)(-1));
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
