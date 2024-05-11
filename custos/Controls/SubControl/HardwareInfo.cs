using custos.Forms;
using custos.Methods;


namespace custos.Controls;

public partial class HardwareInfo : UserControl
{
    SystemInfoMethod systemInfoMethod = new SystemInfoMethod();
    public HardwareInfo()
    {
        InitializeComponent();
    }
    private async void HardewareInfo_Load(object sender, EventArgs e)
    {
        Loader loader = new Loader();

        loader.Show();
        //GetHardwareInfo();
        await Task.Delay(1700);
        GetHardwareInfo();
        loader.Close();

        //GetHardwareInfo();
    }
    public async void GetHardwareInfo()
    {
        try
        {
            var data = await systemInfoMethod.HardwareData();
            HdList.DataSource = data;
        }
        catch (Exception ex)
        {
            

        }
    }

    private void HdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {




    }
}
