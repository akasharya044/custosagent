using custos.Common;
using custos.DTO;
using custos.Methods;
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
    public partial class HardDiskinfo : UserControl
    {
        private List<HarddiskDetailsDto> jsondata = new List<HarddiskDetailsDto>();

        List<HarddiskDetailsDto> jsondataread = new List<HarddiskDetailsDto>();
        Harddiskinfoma harddiskinfo = new Harddiskinfoma();
        HarddiskDetailsDto diskdata;
        public HardDiskinfo()
        {
            InitializeComponent();
        }



       


        private async void CompareData()
        {
            try
            {
                //foreach (var newData in os_data)
                //{

                var particulardata = jsondataread.Find(x => x.SystemId == diskdata.SystemId);
                if (particulardata == null)
                {
                    // If the data doesn't exist in the database, insert it
                    await harddiskinfo.sendharddiskInfo(diskdata);
                }
                else if (!AreEqual(diskdata, particulardata))
                {
                    // If the data exists but has changed, update it
                    jsondataread.Clear();
                    jsondataread.Add(diskdata);
                    await harddiskinfo.sendharddiskInfo(diskdata);
                }

            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
        }


        private bool AreEqual(HarddiskDetailsDto newData, HarddiskDetailsDto existingData)
        {
            // Compare properties of newData and existingData to check if they are equal
            return newData.DriveType == existingData.DriveType &&
                   newData.DriveFormat == existingData.DriveFormat &&
                   newData.NonSystemDriveName == existingData.NonSystemDriveName &&
                   newData.DriveName == existingData.DriveName &&
                   newData.AvailableFreeSpace == existingData.AvailableFreeSpace &&
                   newData.FreeSpace == existingData.FreeSpace &&
                     newData.SerialNumber == existingData.SerialNumber &&
                       newData.SystemId == existingData.SystemId &&
                         newData.NonSystemFreeSpace == existingData.NonSystemFreeSpace &&
                           newData.NonSystemTotalSpace == existingData.NonSystemTotalSpace &&
                             newData.TotalSize == existingData.TotalSize;

        }





        public async void diskdataInformation()
        {

            SqLiteConn sqlite = new SqLiteConn();

            jsondataread = sqlite.ReadHarddiskTable("harddiskInformation");

            Harddiskinfoma disk = new Harddiskinfoma();
            diskdata = disk.HardDiskInfomation();


            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Drive Name: ");
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
            richTextBox1.AppendText(diskdata.DriveName + "\n\n");


            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Drive Type: ");
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
            richTextBox1.AppendText(diskdata.DriveType + "\n\n");


            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Drive Format: ");
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
            richTextBox1.AppendText(diskdata.DriveFormat + "\n\n");


            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Drive Total Size: ");
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
            richTextBox1.AppendText(diskdata.TotalSize + "\n\n");

            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Drive Free Space: ");
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
            richTextBox1.AppendText(diskdata.FreeSpace + "\n\n");

            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Drive Non System Drive Name: ");
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
            richTextBox1.AppendText(diskdata.NonSystemDriveName + "\n\n");


            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Drive Non System Total Space: ");
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
            richTextBox1.AppendText(diskdata.NonSystemTotalSpace + "\n\n");


            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Drive Non System Free Space: ");
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
            richTextBox1.AppendText(diskdata.NonSystemFreeSpace + "\n\n");



            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox1.AppendText("Drive Serial Number: ");
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular); // Reset font style to regular
            richTextBox1.AppendText(diskdata.SerialNumber + "\n\n");


            jsondata.Add(diskdata);

            List<Dictionary<string, object>> dict = SqLiteConn.ConvertObjectToDictionary(jsondata);
            if (jsondataread.Count() == 0)
            {
                sqlite.InsertDataIntoTable("harddiskInformation", dict);
                //jsondataread = sqlite.ReadOSTable("operatingSystem");
                await harddiskinfo.sendharddiskInfo(diskdata);
            }
            else
            {
                CompareData();
            }
            //await sendOSInfo(os_data);


        }

        private async void Harddisk_Load(object sender, EventArgs e)
        {
            await Task.Delay(2000);
            diskdataInformation();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
