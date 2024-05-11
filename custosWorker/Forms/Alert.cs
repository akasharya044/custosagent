using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace custosWorker.Forms
{
    public partial class Alert : Form
    {
        public Alert(string title, string text)
        {

            InitializeComponent();
            label1.Text = title;
            label2.Text = text;
            //timer = new System.Windows.Forms.Timer();
            // 1 minute interval
            Timer_Tick();


        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private System.Windows.Forms.Timer timer;

        //public YourForm()
        //{
        //    InitializeComponent();

        //    // Initialize the timer
        //    timer = new System.Windows.Forms.Timer();
        //    timer.Interval = 60000; // 1 minute interval
        //    timer.Tick += Timer_Tick;
        //    timer.Start();
        //}

        private void Timer_Tick()
        {
            // Close other instances of the same form
            CloseOtherForms();
        }

        private void CloseOtherForms()
        {
            // Loop through all open forms in the application
            foreach (Form form in Application.OpenForms)
            {
                // Check if the form is not the current form and is of the same type
                if (form != this && form.GetType() == typeof(Alert))
                {
                    // Close the form
                    form.Close();
                }
            }
        }
    }
}
