using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace custos.Controls.SubControl
{
    public partial class Loader : UserControl
    {
        public Loader()
        {
            InitializeComponent();
        }

        private async void loader_load(object sender, EventArgs e)
        {
            //await Task.Delay(3000);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
    }
}
