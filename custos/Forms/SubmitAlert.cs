using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace custos.Forms
{
    public partial class SubmitAlert : Form
    {
        public SubmitAlert(string title, string text)
        {
            InitializeComponent();
            label1.Text = title;
            label2.Text = text;
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
