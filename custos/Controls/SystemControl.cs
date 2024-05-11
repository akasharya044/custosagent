
using custos.Forms;

namespace custos.Controls
{
    public partial class SystemControl : UserControl
    {


        public SystemControl()
        {
            InitializeComponent();
        }
        private async void SoftwareInfo_Load(object sender, EventArgs e)
        {

            //Loader Loader = new Loader();
            /// <summary>
            /// Loader.Show();
            /// </summary>
            /// <returns></returns>
            //await Task.Delay(1700);
            //Loader.Close();

        }

        private async void loader2_Load(object sender, EventArgs e)
        {
            await Task.Delay(500);

        }
    }
}
