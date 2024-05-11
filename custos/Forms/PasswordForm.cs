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
	public partial class PasswordForm : Form
	{
		public bool status = false;
		public PasswordForm()
		{
			InitializeComponent();
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		public void button1_Click(object sender, EventArgs e)
		{
			if (label2.Text != System.Environment.MachineName)
			{
				label3.Text = "Wrong Password , Try Again";
			}
			else
			{
				status = true;
			}
			
		}

		
	}
}
