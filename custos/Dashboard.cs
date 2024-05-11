using custos.Controls;
using custos.Controls.SubControl;
using custos.Forms;
using custos.Methods;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Windows.Forms;

namespace custos
{
    public partial class Dashboard : Form
    {

        SystemInfoMethod systemInfoMethod = new SystemInfoMethod();
        SelfHealMethod checkagain = new SelfHealMethod();
        private System.Windows.Forms.Timer timer;
        public Dashboard()
        {
            InitializeComponent();
            InitializeTimer();

        }


        private void Dashboard_Load(object sender, EventArgs e)
        {
            //additionalInfo1.Visible = true;
            home1.Visible = true;
            pictureBox4.BackgroundImage = null;

            string processname = "custosWorker";
            Process[] processes = Process.GetProcessesByName(processname);
            {
                if (!(processes.Length > 0))
                {
                    Directory.SetCurrentDirectory(Path.GetDirectoryName
                    (Assembly.GetExecutingAssembly().Location));
                   Process.Start(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\custosWorker.exe");

                }
                else
                {

                }
            }





        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //home1.Visible = true;
            //additionalInfo1.Visible = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //home1.Visible = false;
            //additionalInfo1.Visible = true;
        }

        private void additionalInfo2_Load(object sender, EventArgs e)
        {

        }

        private void home1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string processName = "custos";


            Process[] processes = Process.GetProcessesByName(processName);

            if (processes.Length > 0)
            {
                processes[0].Kill();

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void additionalInfo1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private bool IsInternetConnected()
        {
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send("www.google.com");
                return reply.Status == IPStatus.Success;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            if (IsInternetConnected())
            {

                // pictureBox4.BackgroundImage = Properties.Resources.circle__2_;
                pictureBox4.Image = Properties.Resources.circle__2_;

            }
            else
            {
                pictureBox4.Image = Properties.Resources.circle__4_;
            }
        }

        private void InitializeTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 5000; // Check every 5 seconds
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void help_hover(object sender, EventArgs e)
        {

        }

        private void help_leave(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            ChatControl chatControl = new ChatControl();
            chatControl.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            RaiseTicket ticket = new RaiseTicket();
            ticket.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Registration rg = new Registration();
            rg.Show();
        }

        private void pictureBox7_Click_1(object sender, EventArgs e)
        {
            Registration rg = new Registration();
            rg.Show();
        }
    }
}
