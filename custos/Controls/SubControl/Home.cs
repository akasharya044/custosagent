using custos.Methods;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static custos.Methods.DeviceInformation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
//using static custos.Methods.SystemInfoDTO;

namespace custos.Controls.SubControl
{
    public partial class Home : UserControl
    {

        DeviceInformation device = new DeviceInformation();
        Harddiskinfoma disk = new Harddiskinfoma();
        osinformation os = new osinformation();
        Deviceinfo info = new Deviceinfo();



        public Home()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
			try
			{
				
				Dashboard existingDash = Application.OpenForms.OfType<Dashboard>().FirstOrDefault();

				
				if (existingDash != null)
				{
					existingDash.tabControl1.SelectedIndex = 1;
				}
				else
				{
					
					Dashboard dash = new Dashboard();
					dash.Show();
					dash.tabControl1.SelectedIndex = 1;
				}
			}
			catch (Exception ex)
			{
				
			}
		}



        private void label14_Click(object sender, EventArgs e)
        {

        }

        private async void Home_Load(object sender, EventArgs e)
        {



            //await Imageurl();

           
            label1.Text = "Welcome " + Environment.UserName;
            await Thought();

            // Assuming 'quotesData' is a class-level variable containing the fetched quotes
            if (quotesData != null && quotesData.Count > 0)
            {
                Random random = new Random();
                int randomIndex = random.Next(0, quotesData.Count);
                QuoteModel randomQuote = quotesData[randomIndex];

                // Display the random quote in label2
                label2.Text = $"{randomQuote.Text} - {randomQuote.Author}";
                label9.Text = device.DeviceInfo().DisplayManufacturer.ToString();
                label12.Text = disk.HardDiskInfomation().DriveName.ToString();
                label11.Text = disk.HardDiskInfomation().TotalSize.ToString();
                label10.Text = Environment.OSVersion.Platform.ToString();
            }
        }


        string apiUrl = "https://type.fit/api/quotes";
        string imageUrl = "https://api.nasa.gov/planetary/apod?api_key=VO2zNf3KLViz7sL27PWeRhgUIaLxkaa7XkUKrreX&count=1";
       // string imageUrl = "https://api.nasa.gov/techtransfer/patent/?engine&api_key=VO2zNf3KLViz7sL27PWeRhgUIaLxkaa7XkUKrreX&count=1";
        List<QuoteModel> quotesData;
        List<ImageModel> imageModels;

        public async Task Thought()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        quotesData = JsonConvert.DeserializeObject<List<QuoteModel>>(responseBody);
                    }
                    else
                    {
                        // Handle the error response
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        // Handle the error as needed
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
        }

        public async Task Imageurl()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(imageUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        imageModels = JsonConvert.DeserializeObject<List<ImageModel>>(responseBody);
                        using (var webClient = new WebClient())
                        {
                            byte[] imageData = webClient.DownloadData(imageModels.FirstOrDefault().url);
                            using (var memoryStream = new System.IO.MemoryStream(imageData))
                            {
                                // Create an Image object from the downloaded data
                                Image image = Image.FromStream(memoryStream);
                                // Set the PictureBox image
                                GraphicsPath path = new GraphicsPath();
                                path.AddEllipse(0, 0, pictureBox1.Width, pictureBox1.Height);
                                pictureBox1.Region = new Region(path);
                                pictureBox1.Image = image;
                                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                            }
                        }
                    }
                    else
                    {
                        // Handle the error response
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        // Handle the error as needed
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
        }

        public class QuoteModel
        {
            public string Text { get; set; }
            public string Author { get; set; }
        }

        public class ImageModel
        {
            public String url { get; set; }

            private void pictureBox1_Click(object sender, EventArgs e)
            {

            }
        }
    }
}
