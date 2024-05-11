using custos.Common;
using custos.Forms;
using Newtonsoft.Json;
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
    public partial class RaiseTicket : Form
    {
        public RaiseTicket()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                SubmitAlert alert = new SubmitAlert("Alert", "Description Is Mandatory");
                alert.ShowDialog();
            }
            else
            {
                raiseticket();
            }
        }

        private async void raiseticket()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {

                    CustOsTicket ticket = new CustOsTicket();
                    ticket.SystemId = System.Environment.MachineName;
                    ticket.Description = richTextBox1.Text;
                    ticket.CategoryId = 1;
                    ticket.SubCategoryId = 1;
                    ticket.AreaId = 1;


                    string jsondata = JsonConvert.SerializeObject(ticket);
                    var content = new StringContent(jsondata, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(APIUrls.TicketPost_url, content);
                    if (response.IsSuccessStatusCode)
                    {

                        string responseBody = await response.Content.ReadAsStringAsync();
                        var responseModel = JsonConvert.DeserializeObject<Response>(responseBody.ToString());
                        if (responseModel.Data != null)
                        {
                            var ticketdata = JsonConvert.DeserializeObject<CustOsTicket>(responseModel.Data.ToString());
                            string ticketNo = ticketdata.Id.ToString();




                            SubmitAlert alert = new SubmitAlert("Ticket Generated Succesfully", "Your Ticket Number is " + ticketNo);
                            this.Close();
                            alert.ShowDialog();
                            

                        }


                    }
                    else
                    {
                        SubmitAlert alert = new SubmitAlert("Failed Ticket Generation", "Failed To Generate Ticket");
                        alert.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                SubmitAlert alert = new SubmitAlert("Failed Ticket Generation", "Failed To Generate Ticket");
                alert.ShowDialog();
            }
        }
    }
}
