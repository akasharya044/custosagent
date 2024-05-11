using System.Net;
using custos.Common;
using Newtonsoft.Json;
using HtmlAgilityPack;

namespace custos.Controls.SubControl
{
    public partial class ChatControl : Form
    {
        private readonly HttpClient httpClient;
        private readonly HashSet<string> visitedUrls;

        public ChatControl()
        {
            InitializeComponent();
            httpClient = new HttpClient();
            visitedUrls = new HashSet<string>();
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {


           // webView21.Source = new Uri("about:blank");
            QuestionDetailText.Text = "Type your query here...";

            QuestionDetailText.ForeColor = Color.Silver;

            pictureBox1.Enabled = true;
           // webView21.Visible = true;
        }



        private async void bindQuestion(string text)
        {
            try
            {

                var questionsdata = webGetMethod("http://65.2.100.52:1050/api/QuestionAnswer/questions/text?text=" + text);
                var data = JsonConvert.DeserializeObject<dynamic>(questionsdata.ToString());
                APIUrls.questiondata = JsonConvert.DeserializeObject<QuestionData>(data.data.ToString());
                if ( APIUrls.questiondata.answer == null || APIUrls.questiondata.answer == "")
                {

                    ClearWebView();
                    string searchQuery = text.Trim();
                    searchQuery = searchQuery.Replace(" ", "+");
                    string googleSearchUrl = $"https://www.google.com/search?q={Uri.EscapeDataString(searchQuery)}&safe=active";
                    await CrawlUrl(googleSearchUrl);



                }
                else
                {
                    // webView21.CoreWebView2.ExecuteScriptAsync($"document.body.innerHTML = '<p>{APIUrls.questiondata.answer}</p>';");
                    textBox1.Text = APIUrls.questiondata.answer;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("IT Assit Is Not Able To Communicate From Server. Please Try After Sometime!!!", "IT Assist");

            }
        }



        private async Task CrawlUrl(string url)
        {
            try
            {
                textBox1.Text = "Searching Please Wait...";
                //webView21.CoreWebView2.ExecuteScriptAsync($"document.body.innerHTML = '<p>Searching Please Wait...</p>';");
                var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                var response = await httpClient.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();
                htmlDocument.LoadHtml(content);

                var snippetNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='BNeawe s3v9rd AP7Wnd']");

                if (snippetNode != null)
                {
                    string snippetText = snippetNode.InnerText.Trim();
                   ClearWebView();
                    //await webView21.CoreWebView2.ExecuteScriptAsync($"document.body.innerHTML = '<p>{snippetText}</p>';");
                    textBox1.Text = snippetText;

                }
                else
                {
                    var websiteSnippetNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='tF2Cxc']//div[@class='IsZvec']//span[@class='aCOpRe']");

                    if (websiteSnippetNode != null)
                    {
                        string websiteSnippetText = websiteSnippetNode.InnerText.Trim();
                        ClearWebView();
                       // await webView21.CoreWebView2.ExecuteScriptAsync($"document.body.innerHTML = '<p>{websiteSnippetText}</p>';");
                       textBox1.Text = websiteSnippetText;
                    }
                    else
                    {
                        // await webView21.CoreWebView2.ExecuteScriptAsync("document.body.innerHTML = ' not found.';");
                        textBox1.Text = "not found";

                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }



        private void bindAnswer(int questionId)
        {
            try
            {
                var answersdata = webGetMethod("http://65.2.100.52:1050/api/QuestionAnswer/" + questionId + "/answers");
                var datas = JsonConvert.DeserializeObject<dynamic>(answersdata.ToString());
                APIUrls.answerdata = JsonConvert.DeserializeObject<List<AnswerData>>(datas!.data.ToString());
                foreach (var anss in APIUrls.answerdata)
                {
                    //textBox1.DocumentText = anss.Answer;
                    //webView21.CoreWebView2.ExecuteScriptAsync($"document.body.innerHTML = '<p>{anss.Answer}</p>';");
                    textBox1.Text = anss.Answer;

                }

                // textBox1.Visible = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("IT Assit Is Not Able To Communicate From Server. Please Try After Sometime!!!", "IT Assist");

            }
        }
        public string webGetMethod(string URL)
        {
            string jsonString = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Method = "GET";
                request.Credentials = CredentialCache.DefaultCredentials;
                ((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 7.1; Trident/5.0)";
                request.Accept = "/";
                request.UseDefaultCredentials = true;
                request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                request.ContentType = "application/x-www-form-urlencoded";

                WebResponse response = request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream());
                jsonString = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("IT Assit Is Not Able To Communicate From Server. Please Try After Sometime!!!", "IT Assist");

            }
            return jsonString;
        }
        private void QuestionDetailText_KeyDown(object sender, KeyEventArgs e)
        {
            if (QuestionDetailText.Text == "Type your query here...")
            {
                QuestionDetailText.Text = "";
                QuestionDetailText.ForeColor = Color.Black;

            }

        }
        private void QuestionDetails_MouseEnter(object sender, EventArgs e)
        {
            if (QuestionDetailText.Text == "Type your query here...")
            {
                QuestionDetailText.Text = "";
                QuestionDetailText.ForeColor = Color.Black;

            }
        }
        private void QuestionDetails_MouseLeave(object sender, EventArgs e)
        {
            if (QuestionDetailText.Text == "")
            {
                QuestionDetailText.Text = "Type your query here...";
                QuestionDetailText.ForeColor = Color.Silver;

            }
        }
        private void QuestionDetails_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

            }
        }
        private void QuestionDetailText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {

            }
            if (e.KeyData == Keys.Enter)
            {
                GoQuestionDetails_Click(sender, e);
            }
        }
        private void GoQuestionDetails_Click(object sender, EventArgs e)
        {
            var searchText = QuestionDetailText.Text;
            //textBox1.Clear();
            ClearWebView();
            if (searchText.Length >= 3 && searchText != "Type your query here...")
            {


                bindQuestion(searchText);


            }
            else
            {

                MessageBox.Show("Please Enter Atleast Three(3) Letter.");
                return;
            }
        }
        //private void userlistBox_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (questionlistBox.SelectedIndex > -1)
        //        {
        //            if (APIUrls.questiondata != null)
        //            {
        //                if (APIUrls.questiondata.Count > 0)
        //                {
        //                    var data = APIUrls.questiondata.Find(x => x.Question.ToString() == questionlistBox.SelectedItem?.ToString());
        //                    if (data != null)
        //                    {
        //                        bindAnswer(data.Id);
        //                    }

        //                }
        //            }

        //            QuestionDetailText.Text = questionlistBox.SelectedItem?.ToString();
        //            questionlistBox.Visible = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Some Error Occurs At Server. Please Try After Sometime!!!", "CIPL IT ASSIST");

        //    }

        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    //QuestionDetailText.Enabled = true;
        //    textBox1.Visible = false;
        //    questionlistBox.Visible = false;
        //    //GoQuestionDetails.Enabled = true;
        //    QuestionDetailText.Text = "";
        //}

        private void QuestionDetailText_TextChanged(object sender, EventArgs e)
        {

        }

        private void QuestionDetailText_Leave(object sender, EventArgs e)
        {
            //if(QuestionDetailText.Text == "")
            //{
            //	QuestionDetailText.Text = "Type your query here...";

            //         }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            RaiseTicket ticket = new RaiseTicket();
            ticket.Show();
            this.Close();
        }

        private void ClearWebView()
        {
            //webView21.CoreWebView2.ExecuteScriptAsync("document.body.innerHTML = '';");
            textBox1.Clear();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            var searchText = QuestionDetailText.Text;
            //textBox1.Clear();
            ClearWebView();
            if (searchText.Length >= 3 && searchText != "Type your query here...")
            {


                bindQuestion(searchText);


            }
            else
            {

                MessageBox.Show("Please Enter Atleast Three(3) Letter.");
                return;
            }
        }
    }
}
