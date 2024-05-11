using custos.Common;
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

    public partial class Selfheal : UserControl
    {
        SelfHealMethod self = new SelfHealMethod();

        public Selfheal()
        {
            InitializeComponent();
        }

        private void IST_Hover(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Blue;
            label3.ForeColor = Color.Blue;
        }

        private void IST_Leave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
            label3.ForeColor = Color.Black;

        }

        private void Excel_hover(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Blue;
            label4.ForeColor = Color.Blue;
        }

        private void Excel_Leave(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Black;
            label4.ForeColor = Color.Black;
        }

        private void wifi_Hover(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Blue;
            label6.ForeColor = Color.Blue;
        }

        private void wifi_Leave(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
            label6.ForeColor = Color.Black;
        }

        private void printer_hover(object sender, EventArgs e)
        {
            label8.ForeColor = Color.Blue;
            label9.ForeColor = Color.Blue;

        }

        private void printer_leave(object sender, EventArgs e)
        {
            label8.ForeColor = Color.Black;
            label9.ForeColor = Color.Black;
        }

        private void Image_Hover(object sender, EventArgs e)
        {
            label10.ForeColor = Color.Blue;
            label11.ForeColor = Color.Blue;
        }

        private void Image_Leave(object sender, EventArgs e)
        {
            label10.ForeColor = Color.Black;
            label11.ForeColor = Color.Black;
        }

        private void pdf_Hover(object sender, EventArgs e)
        {
            label12.ForeColor = Color.Blue;
            label13.ForeColor = Color.Blue;
        }

        private void pdf_leave(object sender, EventArgs e)
        {
            label12.ForeColor = Color.Black;
            label13.ForeColor = Color.Black;
        }

        private async void IST_click(object sender, EventArgs e)
        {


            PopUp pop = new PopUp("Set Time To IST", "Set Time Zone In Progress");
            int x = self.SetTimeZone();
            if (x == 6)
            {

                pop.Show();
                await Task.Delay(3000);
                pop.Close();
            }
        }

        private async void Excel_click(object sender, EventArgs e)
        {
            PopUp pop = new PopUp("Excel", "Excel Close In Progress...");
            int x = self.Excel();
            if (x == 6)
            {
                pop.Show();

                await Task.Delay(3000);
                pop.Close();
            }


        }

        private async void wifi_click(object sender, EventArgs e)
        {
            PopUp pop = new PopUp("Wi-Fi Reset", "Wifi Reset In Progress");
            int x = self.WiFiReset();
            if (x == 6)
            {
                pop.Show();

                await Task.Delay(3000);
                pop.Close();
            }
        }

        private async void Printer_click(object sender, EventArgs e)
        {
            PopUp pop = new PopUp("Repair Printer", "Printer Repair In Progress");
            int x = self.RepairPrinter();
            if (x == 6)
            {
                pop.Show();
                await Task.Delay(5000);
                pop.Close();

            }
        }

        private async void Image_cick(object sender, EventArgs e)
        {
            PopUp pop = new PopUp("Repair Image", "Image Repair in Progress");
            pop.Show();
            self.RepairImage();

            await Task.Delay(3000);
            pop.Close();

        }

        private async void PDF_click(object sender, EventArgs e)
        {
            PopUp pop = new PopUp("Repair PDF", "Repair PDF In Progress...");
            pop.Show();
            self.RepairPDF();
            await Task.Delay(3000);
            pop.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
