using custos.Forms;
using custos.Methods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace custos.Controls
{
    public partial class Cleaner : UserControl
    {
        SelfHealMethod self = new SelfHealMethod();
        public Cleaner()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cleaner_hover(object sender, EventArgs e)
        {
            //label1.ForeColor = Color.Red;
        }

        private void cleaner_remove(object sender, EventArgs e)
        {
            //label1.ForeColor = Color.Black;
        }

        private void disk_hover(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Blue;
            label3.ForeColor = Color.Blue;




        }

        private void disk_leave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
            label3.ForeColor = Color.Black;



        }

        private void memory_hover(object sender, EventArgs e)
        {

            label5.ForeColor = Color.Blue;
            label4.ForeColor = Color.Blue;
        }

        private void memory_leave(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Black;
            label4.ForeColor = Color.Black;
        }

        private void recycle_hover(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Blue;
            label6.ForeColor = Color.Blue;
        }

        private void recycle_leave(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
            label6.ForeColor = Color.Black;
        }

        private void Chrome_History_Hover(object sender, EventArgs e)
        {
            label9.ForeColor = Color.Blue;
            label8.ForeColor = Color.Blue;

        }

        private void chrome_history_leave(object sender, EventArgs e)
        {
            label9.ForeColor = Color.Black;
            label8.ForeColor = Color.Black;
        }

        private void chrome_cache_hover(object sender, EventArgs e)
        {
            label11.ForeColor = Color.Blue;
            label10.ForeColor = Color.Blue;
        }

        private void chrome_cache_leave(object sender, EventArgs e)
        {
            label11.ForeColor = Color.Black;
            label10.ForeColor = Color.Black;
        }

        private void bookmark_hover(object sender, EventArgs e)
        {
            label13.ForeColor = Color.Blue;
            label12.ForeColor = Color.Blue;
        }

        private void bookmark_leave(object sender, EventArgs e)
        {
            label13.ForeColor = Color.Black;
            label12.ForeColor = Color.Black;
        }

        private void cookies_hover(object sender, EventArgs e)
        {
            label15.ForeColor = Color.Blue;
            label14.ForeColor = Color.Blue;
        }

        private void cookies_leave(object sender, EventArgs e)
        {
            label15.ForeColor = Color.Black;
            label14.ForeColor = Color.Black;
        }

        private void reg_enter(object sender, EventArgs e)
        {
            label17.ForeColor = Color.Blue;
            label16.ForeColor = Color.Blue;
        }

        private void reg_leave(object sender, EventArgs e)
        {
            label17.ForeColor= Color.Black;
            label16.ForeColor= Color.Black;
        }

        private async void disk_click(object sender, EventArgs e)
        {


            PopUp pop = new PopUp("Disk Clean Up", "Disk Clean Up In Progress");
            pop.Show();
            self.DiskCleanUp();
            await Task.Delay(3000);
            pop.Close();



        }

        private void memory_click(object sender, EventArgs e)
        {
            MemoryBoost memoryBoost = new MemoryBoost();
            memoryBoost.Show();


        }

        private async void Recycle_click(object sender, EventArgs e)
        {


            PopUp pop = new PopUp("Recycle Bin", "Recycle Bin Clear In Progress");
            pop.Show();
            self.ClearRecyle();
            await Task.Delay(3000);
            pop.Close();

        }

        private async void history_click(object sender, EventArgs e)
        {


            PopUp pop = new PopUp("Chrome History Clear", "Chrome History Clear In Progress");
            int x = self.ClearHistory();
            if (x == 6)
            {
                pop.Show();
                await Task.Delay(3000);
                pop.Close();
            }

        }

        private async void cache_click(object sender, EventArgs e)
        {

            PopUp pop = new PopUp("Chrome Cache Clear", "Chrome Cache Clear In Progress");
            int x = self.ClearCache();
            if (x == 6)
            {
                pop.Show();
                await Task.Delay(3000);
                pop.Close();
            }
        }

        private async void chrome_bookmark(object sender, EventArgs e)
        {


            PopUp pop = new PopUp("Chrome BookMark Clear", "Chorme BookMarks Clear In Progress");
            int x = self.ClearBookmarks();
            if (x == 6)
            {
                pop.Show();
                await Task.Delay(3000);
                pop.Close();
            }

        }

        private async void cookies_click(object sender, EventArgs e)
        {


            PopUp pop = new PopUp("Chrome Cookies Clear", "Chrome Clear In Progress");
            int x = self.ClearCookies();
            if (x == 6)
            {
                pop.Show();
                await Task.Delay(3000);
                pop.Close();
            }
        }

        private async void Registrry_click(object sender, EventArgs e)
        {
            PopUp pop = new PopUp("Registry Clear", "Registry Clear In Progress");
            pop.Show();
            self.deleteReg();
            await Task.Delay(3000);
            pop.Close();
        }

        
    }
}
