using System.Diagnostics;
using System.Runtime.InteropServices;
using custos.Common;
using System.ServiceProcess;
using System.Management;
using System.Text.RegularExpressions;
using System.Security.Principal;
using static custos.Methods.SelfHealDTO;
using Microsoft.Win32;

namespace custos.Methods;

public class SelfHealMethod
{


    [DllImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool SetProcessWorkingSetSize(IntPtr process, IntPtr minimumWorkingSetSize, IntPtr maximumWorkingSetSize);

    CommonMethod commonmethod = new CommonMethod();
    string output = "";

    string[] profiles = new string[]
        {
            "Default",
            "Guest Profile",
            "Profile 1",
            "Profile 2",
            "Profile 3",
            "Profile 4",
            "Profile 5",
            "Profile 6",
            "Profile 7",
            "Profile 8",
            "Profile 9",
            "Profile 10",

        };
    private static DateTime _bootDateTime;

    public Cursor Cursor { get; private set; }

    [DllImport("Shell32.dll")]
    static extern int SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlag dwFlags);
    enum RecycleFlag : int
    {

        SHERB_NOCONFIRMATION = 0x00000001, // No confirmation, when emptying

        SHERB_NOPROGRESSUI = 0x00000001, // No progress tracking window during the emptying of the recycle bin

        SHERB_NOSOUND = 0x00000004 // No sound when the emptying of the recycle bin is complete

    }
    private string[] GetUsersDirectory()
    {
        string[] directories = Directory.GetDirectories("C:\\Users");
        return directories;

    }
    private int KillRunningChrome()
    {
        int msg = (int)MessageBox.Show("This process will stop any running chrome instance. Do you want to continue?", "Cleaner", MessageBoxButtons.YesNo);
        if (msg == 6)
        {
            Process[] Path1 = Process.GetProcessesByName("chrome");
            foreach (Process p in Path1)
            {
                try
                {
                    p.Kill();
                }
                catch { }
                p.WaitForExit();
                p.Dispose();
            }
        }
        return msg;
    }
    static bool IsValidPath(string path)
    {
        try
        {
            Path.GetFullPath(path);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public int RepairPrinter()
    {
        try
        {
            Events events = new Events();
            events.Event = "Repair Printer";
            events.EventDate = DateTime.Now;
            events.SystemId = System.Environment.MachineName;
            commonmethod.EventLog(events);

            DialogResult result = MessageBox.Show("This Feature Require Adminstrator Rights. Are You An Administrator ?", "Repair Printer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                Cursor = Cursors.WaitCursor;

                // Stop the spooler.
                ServiceController service = new ServiceController("Spooler");
                if ((!service.Status.Equals(ServiceControllerStatus.Stopped)) &&
                    (!service.Status.Equals(ServiceControllerStatus.StopPending)))
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped);
                }
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running);

                Cursor = Cursors.Default;

                return 6;
            }

            else
            {
                MessageBox.Show("Process Terminated..", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show("Require Administrator Rights , Contact Your Administrator", "Repair Printer", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return 7;
    }
    public string RepairImage()
    {
        try
        {
            Events events = new Events();
            events.Event = "Repair Image";
            events.EventDate = DateTime.Now;
            events.SystemId = System.Environment.MachineName;
            commonmethod.EventLog(events);

            var directory = AppDomain.CurrentDomain.BaseDirectory;
            var filepath = Path.Combine(directory, "ScriptFiles", "SetUserFTA.exe");
            System.Diagnostics.Process apprun = new System.Diagnostics.Process();
            apprun.StartInfo.FileName = filepath;

            apprun.StartInfo.Arguments = ".jpeg AppX43hnxtbyyps62jhe9sqpdzxn1790zetc ";

            apprun.Start();
            return "Done";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error clearing temporary files: {ex.Message}");
        }
        return "";
    }
    public string RepairPDF()
    {
        try
        {
            Events events = new Events();
            events.Event = "Repair PDF";
            events.EventDate = DateTime.Now;
            events.SystemId = System.Environment.MachineName;
            commonmethod.EventLog(events);

            var directory = AppDomain.CurrentDomain.BaseDirectory;
            var filepath = Path.Combine(directory, "ScriptFiles", "SetUserFTA.exe");
            
            System.Diagnostics.Process apprun = new System.Diagnostics.Process();
            apprun.StartInfo.FileName = filepath;
            apprun.StartInfo.Arguments = ".pdf AcroExch.Document.DC";
            apprun.Start();




            return "Done";
        }
        catch (Exception ex)
        {
            //MessageBox.Show($"Error clearing temporary files: {ex.Message}");
        }
        return "";
    }
    public string RepairLotus()
    {
        try
        {
            Events events = new Events();
            events.Event = "Lotus Services Has Been Terminated";
            events.EventDate = DateTime.Now;
            events.SystemId = System.Environment.MachineName;
            commonmethod.EventLog(events);

            int msg = (int)MessageBox.Show("This process will stop any running lotus instance. Do you want to continue?", "Lotus Repair", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (msg == 6)
            {
                foreach (Process clsProcess in Process.GetProcesses())
                    if (clsProcess.ProcessName.Contains("nlnotes") || clsProcess.ProcessName.Contains("notes2"))  //Process Excel?
                        clsProcess.Kill();
                //MessageBox.Show("All Lotus Services Has Been Terminted");
            }
            else
            {
                //MessageBox.Show("Process Has Been Terminated By User..", "Proceess Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return "Done";

        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error clearing temporary files: {ex.Message}");
        }
        return "";

    }
    public void DiskCleanUp()
    {
        try
        {
            Events events = new Events();
            events.Event = "Disk Cleanup";
            events.EventDate = DateTime.Now;
            events.SystemId = System.Environment.MachineName;
            commonmethod.EventLog(events);


            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "Cleanmgr.exe",
                    Arguments = "/sagerun:1 /qn", // Use /sagerun:1 to apply the cleanup options
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // Start the process
                using (Process process = new Process { StartInfo = psi })
                {
                    process.Start();
                    process.WaitForExit();
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error clearing temporary files: {ex.Message}");
        }
    }
    public void MemoryBoost()
    {
        try
        {
            Events events = new Events();
            events.Event = "Memory Boost";
            events.EventDate = DateTime.Now;
            events.SystemId = System.Environment.MachineName;
            commonmethod.EventLog(events);



            try
            {
                DisplayMemoryInformation("Current Memory Consumption:");
                ClearStandbyList();
                DisplayMemoryInformation("Memory Consumption After Optimization:");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message.ToString());
        }

    }
    static void DisplayMemoryInformation(string message)
    {
        string totalMemory = $"Total Physical Memory: {GetTotalPhysicalMemory()} bytes";
        string availableMemory = $"Available Physical Memory: {GetAvailablePhysicalMemory()} bytes";

        MessageBox.Show($"{message}\n\n{totalMemory}\n\n{availableMemory}", "Memory Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    static ulong GetTotalPhysicalMemory()
    {
        using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem"))
        {
            foreach (ManagementObject obj in searcher.Get())
            {
                return Convert.ToUInt64(obj["TotalPhysicalMemory"]);
            }
        }
        return 0;
    }
    static ulong GetAvailablePhysicalMemory()
    {
        using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
        {
            foreach (ManagementObject obj in searcher.Get())
            {
                return Convert.ToUInt64(obj["FreePhysicalMemory"]);
            }
        }
        return 0;
    }
    static void ClearStandbyList()
    {
        Process currentProcess = Process.GetCurrentProcess();
        SetProcessWorkingSetSize(currentProcess.Handle, (IntPtr)(-1), (IntPtr)(-1));
    }



    public int SetTimeZone()
    {
        try
        {
            Events events = new Events();
            events.Event = "Time Zone Changed";
            events.EventDate = DateTime.Now;
            events.SystemId = System.Environment.MachineName;
            commonmethod.EventLog(events);

            DialogResult result = MessageBox.Show("This Feature Will Set Default Time Zone. Are You Sure ?", "Time Zone", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string timeZoneId = "India Standard Time";
                Process process = new Process();
                process.StartInfo.FileName = "tzutil";
                process.StartInfo.Arguments = "/s \"" + timeZoneId + "\"";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();

                return 6;
            }

            else
            {
                MessageBox.Show("Process Has Been Terminated By User..", "Proceess Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        catch (Exception ex)
        {
            MessageBox.Show($"Error clearing temporary files: {ex.Message}");
        }
        return 7;

    }
    public int Excel()
    {
        try
        {
            Events events = new Events();
            events.Event = "Excel Process Terminated";
            events.EventDate = DateTime.Now;
            events.SystemId = System.Environment.MachineName;
            commonmethod.EventLog(events);
            int msg = (int)MessageBox.Show("This process will stop any running excel instance. Do you want to continue?", "Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (msg == 6)
            {

                foreach (Process clsProcess in Process.GetProcesses())
                    if (clsProcess.ProcessName.Contains("EXCEL"))  //Process Excel?
                        clsProcess.Kill();
                return 6;

            }
            else
            {
                MessageBox.Show("Process Has Been Terminated By User..", "Proceess Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error clearing temporary files: {ex.Message}");
        }
        return 7;

    }
    public void WindowCache()
    {
        try
        {
            Events events = new Events();
            events.Event = "Cache Clear";
            events.EventDate = DateTime.Now;
            events.SystemId = System.Environment.MachineName;
            commonmethod.EventLog(events);

            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                MessageBox.Show(ex.Message.ToString());
            }


        }
        catch (Exception ex)
        {

        }
    }
    public void ClearRecyle()
    {
        try
        {
            Events events = new Events();
            events.Event = "Recycle Bin Cleared";
            events.EventDate = DateTime.Now;
            events.SystemId = System.Environment.MachineName;
            commonmethod.EventLog(events);

            SHEmptyRecycleBin(IntPtr.Zero, null, RecycleFlag.SHERB_NOSOUND | RecycleFlag.SHERB_NOCONFIRMATION);


        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error clearing temporary files: {ex.Message}");
        }
    }
    public int WiFiReset()
    {
        try
        {
            Events events = new Events();
            events.Event = "Wifi Reset";
            events.EventDate = DateTime.Now;
            events.SystemId = System.Environment.MachineName;
            commonmethod.EventLog(events);
            DialogResult result = MessageBox.Show("This Feature Will Wipe All The Key and Reset The Wifi Adapter! Are You Sure ?", "Wifi Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = "netsh.exe",
                        Arguments = "wlan delete profile * ",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    };

                    // Start the process
                    using (Process process = new Process { StartInfo = psi })
                    {
                        process.Start();
                        process.WaitForExit();
                    }
                    return 6;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("Process Terminated..", "Proceess Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message.ToString());
        }
        return 7;

    }
    public void ChromeDataDelete(string UserName, string fileName)
    {
        string rootDrive = Path.GetPathRoot(Environment.SystemDirectory); // for getting primary drive 
        string userName = UserName;// Environment.UserName; // for getting user name
        try
        {
            string path1 = userName + "\\AppData\\Local\\Google\\Chrome\\User Data\\";
            string path2 = userName + "\\AppData\\Roaming\\Google\\Chrome\\User Data\\";
            string path3 = rootDrive + "Users\\admin\\AppData\\Local\\Google\\Chrome\\User Data\\";
            try
            {
                foreach (string profile in profiles)
                {
                    string profilePath1 = Path.Combine(path1, profile);
                    string profilePath2 = Path.Combine(path2, profile);
                    string profilePath3 = Path.Combine(path3, profile);

                    if (IsValidPath(profilePath1))
                    {
                        DirectoryInfo downloadedMessageInfo = new DirectoryInfo(profilePath1);
                        if (downloadedMessageInfo.Exists)
                        {
                            foreach (FileInfo file in downloadedMessageInfo.GetFiles())
                            {
                                if (file.Name.Contains(fileName))
                                    file.Delete();
                            }
                            foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
                            {
                                if (dir.Name.Contains(fileName))
                                    dir.Delete(true);
                            }
                        }
                    }
                    if (IsValidPath(profilePath2))
                    {
                        DirectoryInfo RomingInfo = new DirectoryInfo(profilePath2);
                        if (RomingInfo.Exists)
                        {
                            foreach (FileInfo file in RomingInfo.GetFiles())
                            {
                                if (file.Name.Contains(fileName))
                                    file.Delete();
                            }
                            foreach (DirectoryInfo dir in RomingInfo.GetDirectories())
                            {
                                if (dir.Name.Contains(fileName))
                                    dir.Delete(true);
                            }
                        }
                    }
                    if (IsValidPath(profilePath3))
                    {
                        DirectoryInfo adminInfo = new DirectoryInfo(profilePath3);
                        if (adminInfo.Exists)
                        {
                            foreach (FileInfo file in adminInfo.GetFiles())
                            {
                                if (file.Name.Contains(fileName))
                                    file.Delete();
                            }
                            foreach (DirectoryInfo dir in adminInfo.GetDirectories())
                            {
                                if (dir.Name.Contains(fileName))
                                    dir.Delete(true);
                            }
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("History is not clear.");
            }
        }
        catch (IOException ex)
        {
            MessageBox.Show("History is not clear.");
        }
    }
    public void ChromeCookiesDelete(string UserName)
    {
        string rootDrive = Path.GetPathRoot(Environment.SystemDirectory);
        string userName = UserName;
        try
        {
            string path1 = userName + "\\AppData\\Local\\Google\\Chrome\\User Data\\";
            string path2 = userName + "\\AppData\\Roaming\\Google\\Chrome\\User Data\\";
            string path3 = rootDrive + "Users\\admin\\AppData\\Local\\Google\\Chrome\\User Data\\";
            try
            {
                foreach (string profile in profiles)
                {
                    string profilePath1 = Path.Combine(path1, profile);
                    string profilePath2 = Path.Combine(path2, profile);
                    string profilePath3 = Path.Combine(path3, profile);

                    if (IsValidPath(profilePath1))
                    {
                        DirectoryInfo downloadedMessageInfo = new DirectoryInfo(profilePath1);
                        if (downloadedMessageInfo.Exists)
                        {
                            foreach (FileInfo file in downloadedMessageInfo.GetFiles())
                            {
                                if (file.Name.Contains("Network") || file.Name.Contains("Session") || file.Name.Contains("Extensions"))
                                    file.Delete();
                            }
                            foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
                            {
                                if (dir.Name.Contains("Network") || dir.Name.Contains("Session") || dir.Name.Contains("Extensions"))
                                    dir.Delete(true);
                            }
                        }
                    }
                    if (IsValidPath(profilePath2))
                    {
                        DirectoryInfo RomingInfo = new DirectoryInfo(profilePath2);
                        if (RomingInfo.Exists)
                        {
                            foreach (FileInfo file in RomingInfo.GetFiles())
                            {
                                if (file.Name.Contains("Network") || file.Name.Contains("Session") || file.Name.Contains("Extensions"))
                                    file.Delete();
                            }
                            foreach (DirectoryInfo dir in RomingInfo.GetDirectories())
                            {
                                if (dir.Name.Contains("Network") || dir.Name.Contains("Session") || dir.Name.Contains("Extensions"))
                                    dir.Delete(true);
                            }
                        }
                    }
                    if (IsValidPath(profilePath3))
                    {
                        DirectoryInfo adminInfo = new DirectoryInfo(profilePath3);
                        if (adminInfo.Exists)
                        {
                            foreach (FileInfo file in adminInfo.GetFiles())
                            {
                                if (file.Name.Contains("Network") || file.Name.Contains("Session") || file.Name.Contains("Extensions"))
                                    file.Delete();
                            }
                            foreach (DirectoryInfo dir in adminInfo.GetDirectories())
                            {
                                if (dir.Name.Contains("Network") || dir.Name.Contains("Session") || dir.Name.Contains("Extensions"))
                                    dir.Delete(true);
                            }
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Cookies is not clear.");
            }
        }
        catch (IOException ex)
        {
            MessageBox.Show("Cookies is not clear.");
        }
    }
    public void AllChromeDataDelete(string UserName)
    {
        string rootDrive = Path.GetPathRoot(Environment.SystemDirectory);
        string userName = UserName;
        try
        {
            string path1 = userName + "\\AppData\\Local\\Google\\Chrome\\User Data";
            string path2 = userName + "\\AppData\\Roaming\\Google\\Chrome\\User Data";
            string path3 = rootDrive + "Users\\admin\\AppData\\Local\\Google\\Chrome\\User Data\\";
            try
            {
                foreach (string profile in profiles)
                {
                    string profilePath1 = Path.Combine(path1, profile);
                    string profilePath2 = Path.Combine(path2, profile);
                    string profilePath3 = Path.Combine(path3, profile);

                    if (IsValidPath(profilePath1))
                    {
                        DirectoryInfo downloadedMessageInfo = new DirectoryInfo(profilePath1);
                        if (downloadedMessageInfo.Exists)
                        {
                            foreach (FileInfo file in downloadedMessageInfo.GetFiles())
                            {
                                file.Delete();
                            }
                            foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
                            {
                                dir.Delete(true);
                            }
                        }
                    }
                    if (IsValidPath(profilePath2))
                    {
                        DirectoryInfo RomingInfo = new DirectoryInfo(profilePath2);
                        if (RomingInfo.Exists)
                        {
                            foreach (FileInfo file in RomingInfo.GetFiles())
                            {
                                file.Delete();
                            }
                            foreach (DirectoryInfo dir in RomingInfo.GetDirectories())
                            {
                                dir.Delete(true);
                            }
                        }
                    }
                    if (IsValidPath(profilePath3))
                    {
                        DirectoryInfo adminInfo = new DirectoryInfo(profilePath3);
                        if (adminInfo.Exists)
                        {
                            foreach (FileInfo file in adminInfo.GetFiles())
                            {
                                file.Delete();
                            }
                            foreach (DirectoryInfo dir in adminInfo.GetDirectories())
                            {
                                dir.Delete(true);
                            }
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("All is not clear.");
            }
        }
        catch (IOException ex)
        {
            MessageBox.Show("All is not clear.");
        }
    }
    public int ClearHistory()
    {
        try
        {
            Events events = new Events();
            events.Event = "Clear History";
            events.EventDate = DateTime.Now;
            events.SystemId = System.Environment.MachineName;
            commonmethod.EventLog(events);
            if (KillRunningChrome() == 7)

                return 7;

            string[] directories = GetUsersDirectory();
            for (int i = 0; i < directories.Length; i++)
            {
                ChromeDataDelete(directories[i], "History");
            }


        }
        catch (Exception ex)
        {
            MessageBox.Show("History is not clear.");

        }
        return 6;

    }
    public int ClearCache()
    {
        try
        {
            Events events = new Events();
            events.Event = "Clear Cache";
            events.EventDate = DateTime.Now;
            events.SystemId = System.Environment.MachineName;
            commonmethod.EventLog(events);
            if (KillRunningChrome() == 7)
                return 7;

            string[] directories = GetUsersDirectory();
            for (int i = 0; i < directories.Length; i++)
            {
                ChromeDataDelete(directories[i], "Cache");
            }


        }
        catch (Exception ex)
        {
            MessageBox.Show("Cache is not clear.");

        }
        return 6;
    }
    public int ClearBookmarks()
    {
        try
        {
            Events events = new Events();
            events.Event = "Clear Bookmarks";
            events.EventDate = DateTime.Now;
            events.SystemId = System.Environment.MachineName;
            commonmethod.EventLog(events);
            if (KillRunningChrome() == 7)
                return 7;

            string[] directories = GetUsersDirectory();
            for (int i = 0; i < directories.Length; i++)
            {
                ChromeDataDelete(directories[i], "Bookmarks");
            }


        }
        catch (Exception ex)
        {
            MessageBox.Show("Bookmarks is not clear.");

        }
        return 6;

    }
    public int ClearCookies()
    {
        try
        {
            Events events = new Events();
            events.Event = "Clear Cookies";
            events.EventDate = DateTime.Now;
            events.SystemId = System.Environment.MachineName;
            commonmethod.EventLog(events);
            if (KillRunningChrome() == 7)
                return 7;

            string[] directories = GetUsersDirectory();
            for (int i = 0; i < directories.Length; i++)
            {
                ChromeCookiesDelete(directories[i]);
            }


        }
        catch (Exception ex)
        {
            MessageBox.Show("Cookies is not clear.");

        }
        return 6;

    }
    public int ClearChrome()
    {
        try
        {
            Events events = new Events();
            events.Event = "Clear All";
            events.EventDate = DateTime.Now;
            events.SystemId = System.Environment.MachineName;
            commonmethod.EventLog(events);

            if (KillRunningChrome() == 7)
                return 7;

            string[] directories = GetUsersDirectory();
            for (int i = 0; i < directories.Length; i++)
            {
                AllChromeDataDelete(directories[i]);
            }
            MessageBox.Show("All Deleted successfully.");

        }
        catch (Exception ex)
        {
            MessageBox.Show("All is not clear.");

        }
        return 6;
    }



    public void Osinfo()
    {
        try
        {
            DateTime lastBootTime = GetLastBootTime();

            SelectQuery query1 = new SelectQuery("SELECT * FROM Win32_ComputerSystem");

            string Key = GetWindowsProductKey();
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");

            foreach (ManagementObject managementObject in mos.Get())
            {
                if (managementObject["Caption"] != null)
                {
                    string Caption = managementObject["Caption"].ToString();

                }
                if (managementObject["OSArchitecture"] != null)
                {
                    string OSArchitecture = managementObject["OSArchitecture"].ToString();

                }
                if (managementObject["Version"] != null)
                {
                    string Version = managementObject["Version"].ToString();

                }
                if (managementObject["BuildNumber"] != null)
                {
                    string BuildNumber = managementObject["BuildNumber"].ToString();

                }
                if (managementObject["Manufacturer"] != null)
                {
                    string Manufacturer = managementObject["Manufacturer"].ToString();

                }
                if (managementObject["LastBootUpTime"] != null)
                {
                    var lastBootTimeString = managementObject["LastBootUpTime"].ToString();
                    DateTime lastBootUpTime = ManagementDateTimeConverter.ToDateTime(lastBootTimeString);
                    var LastBootUpTime = lastBootUpTime;

                    // Calculate the difference in days using UTC time
                    TimeSpan lastBootDuration = DateTime.UtcNow - lastBootUpTime.ToUniversalTime();

                    // Round the total days to get an accurate count
                    var NoOfDaysLastBoot = (int)Math.Round(lastBootDuration.TotalDays);
                }
                if (managementObject["SerialNumber"] != null)
                {
                    string SerialNumber = managementObject["SerialNumber"].ToString();
                }

                string SystemId = System.Environment.MachineName;
            }

            using (ManagementObjectSearcher searcher1 = new ManagementObjectSearcher(query1))
            {
                ManagementObjectCollection collection = searcher1.Get();
                foreach (ManagementObject queryObj in collection)
                {
                    string membershipType = queryObj["DomainRole"]?.ToString();

                    if (!string.IsNullOrEmpty(membershipType))
                    {

                        if (membershipType == "0" || membershipType == "1")
                        {

                            string DomainRole = "Workgroup";

                        }
                        else if (membershipType == "2" || membershipType == "3" || membershipType == "4" || membershipType == "5")
                        {

                            string DomainRole = "Domain";


                        }
                        else
                        {

                            string DomainRole = "Unknown";


                        }
                    }
                }
            }

            string applicationId = GetWindowsActivationApplicationId();
            if (applicationId != null)
            {
                bool isActivated = IsWindowsActivated(applicationId);
                bool IsActivated = isActivated;

            }

            if (!string.IsNullOrEmpty(Key))
            {
                string windowskey = Key;

            }

            bool isAdmin = IsUserAdministrator();

            WindowsIdentity currentIdentity = WindowsIdentity.GetCurrent();


            WindowsPrincipal currentPrincipal = new WindowsPrincipal(currentIdentity);

            if (currentIdentity.Name != null)
            {
                string currentidentity = currentIdentity.Name;
            }

            if (currentPrincipal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                string role = "Admin";

            }
            else
            {
                string role = "Non Admin";
            }
            if (isAdmin)
            {
                string checkadmin = "Current User Is Admin";
            }
            else
            {
                string checkadmin = "Current User Is Non Admin";
            }








        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.Message.ToString());

        }

    }


    static DateTime GetLastBootTime()
    {
        DateTime lastBootTime = DateTime.MinValue;

        try
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT LastBootUpTime FROM Win32_OperatingSystem"))
            {
                ManagementObjectCollection results = searcher.Get();

                foreach (ManagementObject result in results)
                {
                    string lastBootTimeString = result["LastBootUpTime"]?.ToString();
                    lastBootTime = ManagementDateTimeConverter.ToDateTime(lastBootTimeString);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving last boot time: {ex.Message}");
        }

        return lastBootTime;
    }

    public static string GetWindowsActivationApplicationId()
    {
        try
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM SoftwareLicensingProduct WHERE LicenseStatus = 1");
            ManagementObjectCollection objCollection = searcher.Get();

            foreach (ManagementObject obj in objCollection)
            {
                // Assuming the first activated product is the one we want
                return obj["ApplicationID"]?.ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving Application ID: {ex.Message}");
        }

        return null;
    }

    public static bool IsWindowsActivated(string applicationId)
    {
        if (string.IsNullOrEmpty(applicationId))
        {
            Console.WriteLine("Application ID is null or empty.");
            return false;
        }

        ManagementScope scope = new ManagementScope(@"\\" + Environment.MachineName + @"\root\cimv2");
        scope.Connect();

        SelectQuery searchQuery = new SelectQuery($"SELECT * FROM SoftwareLicensingProduct WHERE ApplicationID = '{applicationId}' AND LicenseStatus = 1");
        ManagementObjectSearcher searcherObj = new ManagementObjectSearcher(scope, searchQuery);

        using (ManagementObjectCollection obj = searcherObj.Get())
        {
            return obj.Count > 0;
        }
    }


    public static string GetWindowsProductKey()
    {
        try
        {
            ProcessStartInfo psi = new ProcessStartInfo("wmic", "path softwarelicensingservice get OA3xOriginalProductKey");
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;

            using (Process process = new Process() { StartInfo = psi })
            {
                process.Start();
                string result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                string input = result;

                // Define the pattern for the product key
                string pattern = @"\b([A-Z0-9]{5}-[A-Z0-9]{5}-[A-Z0-9]{5}-[A-Z0-9]{5}-[A-Z0-9]{5})\b";

                // Use Regex.Match to find the first match in the input string
                Match match = Regex.Match(input, pattern);

                // Check if a match is found
                if (match.Success)
                {
                    // Extract the matched product key
                    string productKey = match.Groups[1].Value;
                    return productKey;
                    // Print the extracted product key
                    //Console.WriteLine(productKey);
                }
                else
                {
                    Console.WriteLine("No product key found in the input string.");
                }
                return result;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving Windows Product Key: {ex.Message}");
        }

        return null;
    }

    static bool IsUserAdministrator()
    {
        try
        {
            // Get the identity of the current user
            System.Security.Principal.WindowsIdentity currentIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();

            // Get the principal representing the current user
            System.Security.Principal.WindowsPrincipal currentPrincipal = new System.Security.Principal.WindowsPrincipal(currentIdentity);

            // Check if the user is a member of the Administrator group
            return currentPrincipal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }
        catch (Exception)
        {
            // If an exception occurs, return false (not an administrator)
            return false;
        }
    }

    public void deleteReg()
    {
        try
        {
            Events events = new Events();
            events.Event = "Registry Clear";
            events.EventDate = DateTime.Now;
            events.SystemId = System.Environment.MachineName;
            commonmethod.EventLog(events);

            // Define the registry path related to uninstalled programs
            string keyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";

            // Check if the specified registry key exists
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyPath))
            {
                if (key != null)
                {
                    // Check if the key has any subkeys
                    if (key.SubKeyCount > 0)
                    {
                        // Attempt to delete the specified registry key
                        try
                        {
                            Registry.LocalMachine.DeleteSubKeyTree(keyPath);

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
        }
        catch(Exception ex)
        {

        }

    }








}









public class SelfHealAPI
{

}









public class SelfHealDTO
{
    public class TcpConnection
    {
        public string LocalAddress { get; set; }
        public int LocalPort { get; set; }
        public string RemoteAddress { get; set; }
        public int RemotePort { get; set; }
        public string State { get; set; }

    }

    public class OsInfoDTO
    {
        public string caption { get; set; }
        public string OSArchitecture { get; set; }

        public string Version { get; set; }

        public string BuildNumber { get; set; }

        public string Manufacture { get; set; }

        public DateTime LastBootTime { get; set; }

        public int NoOfDays { get; set; }

        public string serialNo { get; set; }

        public string SystemId { get; set; }

        public bool IsAdmin { get; set; }

        public string membershiptype { get; set; }

        public string DomainRole { get; set; }

        public string WindowsKey { get; set; }

        public string CurrentIdentity { get; set; }

        public string role { get; set; }

        public string checkadmin { get; set; }



    }
}


