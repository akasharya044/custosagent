using custos.Common;
using custos.Controls.SubControl;
using custos.DTO;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace custos.Forms
{
    public partial class SplashScreen : Form
    {
        private System.Windows.Forms.Timer timer;

        private int progressValue;

        public SplashScreen()
        {
            InitializeComponent();
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 15; // Set the interval in milliseconds
            timer.Tick += Timer_Tick;

            // Start the timer
            timer.Start();


        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            // Update the progress bar
            progressBar1.Value = progressValue++;

            // Check if the progress is complete
            if (progressValue >= 100)
            {
                // Stop the timer
                timer.Stop();

                // Close the splash screen and open the dashboard
                this.Hide();
                if (await GetInformation())
                {
                    Dashboard dash = new Dashboard();
                    dash.Show();
                }
                else
                {
                    Registration register = new Registration();
                    register.Show();


                }

            }
        }




        public async Task<bool> GetInformation()
        {
            try
             {
                string systemid = System.Environment.MachineName.ToString();
               
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(APIUrls.GetRegistration_url+systemid);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Response responseModel = JsonConvert.DeserializeObject<Response>(responseBody);
                        if (responseModel.Data != null)
                        {
                            var dbdata = JsonConvert.DeserializeObject<UserRegistrationDto>(responseModel.Data.ToString());
                            return dbdata.IsRegistered;

                        }
                        else
                        {
                            // Handle unsuccessful response
                            return false;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            return false;
        }
    }
}
