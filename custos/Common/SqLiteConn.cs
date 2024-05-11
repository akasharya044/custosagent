using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Newtonsoft.Json;
using custos.DTO;
using System.Reflection;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace custos.Common
{
    public class SqLiteConn
    {
        SQLiteConnection sqlite_conn;
        //private List<OSDto>jsondata = new List<OSDto>();
        List<WindowsServicesDto> jsondataread = new List<WindowsServicesDto>();
        List<OSDto> OSread = new List<OSDto>();
        List<HarddiskDetailsDto> harddiskread = new List<HarddiskDetailsDto>();
        List<DeviceDetailsDto> deviceread = new List<DeviceDetailsDto>();
        List<AntivirusDetailsDto> antiread = new List<AntivirusDetailsDto>();
        List<OSCoreDto> oscoreread = new List<OSCoreDto>();
        List<PortInformationDto> portread = new List<PortInformationDto>();
        List<InstalledSoftwareDto> installedsoftwareread = new List<InstalledSoftwareDto>();
        List<SoftwareInformationDto> softwareread = new List<SoftwareInformationDto>();
        List<HardwareInformationDto> hardwareread = new List<HardwareInformationDto>();



        public SqLiteConn()
        {


            sqlite_conn = CreateConnection();
            CreateTable(sqlite_conn);
            //InsertTable(sqlite_conn);
        }

        private static SQLiteConnection CreateConnection()
        {
            try
            {

                //SQLiteConnection sqlite_conn;
                //string dbpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                //dbpath = Path.Combine(dbpath, "CustOS");

                //sqlite_conn = new SQLiteConnection("Data Source=CustOS; New = True; Compress = True; ");
                //sqlite_conn.Open();
                //return sqlite_conn;

                string appdatapath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string dbFolder = Path.Combine(appdatapath, "CustOS");

                try
                {
                    if (Directory.Exists(dbFolder))
                    {
                        Directory.Delete(dbFolder, true);
                    }

                    else if (!Directory.Exists(dbFolder))
                    {
                        Directory.CreateDirectory(dbFolder);
                    }
                }
                catch(Exception ex) {

                    
                }

              
                string dbName = "CustOSSQLDB.db";  
                string dbPath = Path.Combine(dbFolder, dbName);

              
                SQLiteConnection sqlite_conn = new SQLiteConnection($"Data Source={dbPath}; Version=3; New=True; Compress=True;");

              
                sqlite_conn.Open();

                return sqlite_conn;



            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }


        }

        static void CreateTable(SQLiteConnection conn)
        {

            SQLiteCommand sqlite_cmd;

            //string Createsql = "CREATE TABLE IF NOT EXISTS Backgroundservice(UserId VARCHAR(50) , MemoryUsages float , CpuUsgaes float , NetworkUsgaes float , CPU_Alert VARCHAR(50) , CPU_TimeStamp datetime2 , Memory_Alert VARCHAR(50) , Memory_TimeStamp datetime2 , Network_Alert VARCHAR(50) , Network_TimeStamp datetime2)";


            string OsSql = "CREATE TABLE IF NOT EXISTS operatingSystem(SerialNumber VARCHAR(60), OS_Name VARCHAR(60), OS_Version VARCHAR(60), OS_Architecture VARCHAR(60), Build_Number VARCHAR(60), Manufacturer VARCHAR(60), LastBootUpTime VARCHAR(60), NoOfDaysLastSystemBoot VARCHAR(60), MembershipType VARCHAR(60), LastLogged VARCHAR(60), LastLoggedUserRole VARCHAR(60) , CurrentUser VARCHAR(60), TimeStamp datetime2 , SystemId VARCHAR(60))";

            string OSCoreSql = "CREATE TABLE IF NOT EXISTS OSCore(WindowProductKey VARCHAR(60) , WindowActivationStatus VARCHAR(60), SystemId VARCHAR(60),TimeStamp datetime2)";

            string AntiVirusSql = "CREATE TABLE IF NOT EXISTS AntivirusDetails(SystemId VARCHAR(60) , Id Int , AntivirusName VARCHAR(60) , TimeStamp datetime2)";

            string HarddiskSql = "CREATE TABLE IF NOT EXISTS harddiskInformation(SystemId VARCHAR(60), DriveName VARCHAR(60), DriveType VARCHAR(60), DriveFormat VARCHAR(60), SerialNumber VARCHAR(60), TotalSize VARCHAR(60), FreeSpace VARCHAR(60), AvailableFreeSpace VARCHAR(60), NonSystemDriveName VARCHAR(60), NonSystemTotalSpace VARCHAR(60), NonSystemFreeSpace VARCHAR(60), TimeStamp datetime2)";

            string DeviceInfoSql = "CREATE TABLE IF NOT EXISTS deviceInformation(BIOS VARCHAR(60) , DeviceVersion VARCHAR(60), DeviceName VARCHAR(60), MACAddress VARCHAR(60), IPAddress NAVCHAR(60), VirtualMemory NAVCHAR(60), AvailableVirtualMemory NAVCHAR(60), DisplayManufacturer NAVCHAR(60), DisplayDetails NAVCHAR(60), DisplayName NAVCHAR(60), TimeStamp datetime2 , SystemId NAVCHAR(60))";

            string PortSql = "CREATE TABLE IF NOT EXISTS PortInformation(Id int, LocalEndpoint VARCHAR(60) , LocalPort VARCHAR(60), RemoteEndpint VARCHAR(60), RemotePort VARCHAR(60), State VARCHAR(60), ProcessId int, Service VARCHAR(60), SystemId VARCHAR(60), TimeStamp datetime2)";

            string WindowServiceSql = "CREATE TABLE IF NOT EXISTS windowServices(ServiceName VARCHAR(60), ServiceDisplayName VARCHAR(60), ServiceStatus VARCHAR(60), Startup bit , TimeStamp datetime2 , SystemId VARCHAR(60))";

            string SoftwareInformationSql = "CREATE TABLE IF NOT EXISTS SoftwareInformation(Id int, WindowTitle VARCHAR(60), ProcessName VARCHAR(60), SystemId VARCHAR(60), MemorySize VARCHAR(60), ModuleName VARCHAR(60), Starttime NAVCHAR(60), Pid NAVCHAR(60), CpuUsage NAVCHAR(60), TimeStamp datetime2 )";

            string HardwareSql = "CREATE TABLE IF NOT EXISTS HardwareInformation(Id int, Name VARCHAR(60), Value VARCHAR(60), Type VARCHAR(60), IsLocal bit, IsArray bit, Origin VARCHAR(60), Qualifires VARCHAR(60), SystemId VARCHAR(60), TimeStamp datetime2)";


            string SoftwareSql = "CREATE TABLE IF NOT EXISTS InstalledSoftware(Id int, DisplayName VARCHAR(60), Publisher VARCHAR(60), InstalledOn VARCHAR(60), Size VARCHAR(60), SystemId VARCHAR(60), Version VARCHAR(60), TimeStamp datetime2)";




            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = OsSql;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = OSCoreSql;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = AntiVirusSql;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = HarddiskSql;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = DeviceInfoSql;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = PortSql;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = WindowServiceSql;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = SoftwareInformationSql;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = HardwareSql;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = SoftwareSql;
            sqlite_cmd.ExecuteNonQuery();


        }


        //public void InsertDataIntoTable(string tableName, List<Dictionary<string, object>> data)
        //{

        //    string columns = string.Join(", ", data.Keys);
        //    string values = string.Join(", ", data.Keys);
        //    string parameterNames = string.Join(", ", data.Keys);

        //    string insertQuery = $"INSERT INTO {tableName} ({columns}) VALUES ({parameterNames})";

        //    using (SQLiteCommand command = new SQLiteCommand(insertQuery, sqlite_conn))
        //    {
        //        foreach (var kvp in data)
        //        {
        //            command.Parameters.AddWithValue("@" + kvp.Key, kvp.Value);
        //        }

        //        command.ExecuteNonQuery();
        //    }
        //}

        //public void InsertDataIntoTable(string tableName, List<Dictionary<string, object>> dataList)
        //{
        //    if (dataList == null || dataList.Count == 0)
        //    {
        //        // Handle empty or null data list
        //        return;
        //    }

        //    // Extract columns from the first dictionary in the list
        //    var columns = string.Join(", ", dataList[0].Keys);
        //    // Create parameter placeholders for each column
        //    var parameterPlaceholders = string.Join(", ", dataList[0].Keys.Select(key => "@" + key));
        //    // Create the insert query
        //    var insertQuery = $"INSERT INTO {tableName} ({columns}) VALUES ({parameterPlaceholders})";

        //    using (SQLiteCommand command = new SQLiteCommand(insertQuery, sqlite_conn))
        //    {
        //        // Iterate through each dictionary in the data list
        //        foreach (var data in dataList)
        //        {
        //            // Add parameters for each column-value pair
        //            foreach (var kvp in data)
        //            {
        //                command.Parameters.AddWithValue("@" + kvp.Key, kvp.Value);
        //            }
        //            // Execute the query for each dictionary
        //            command.ExecuteNonQuery();
        //            // Clear parameters for next iteration
        //            command.Parameters.Clear();
        //        }
        //    }
        //}





        public void InsertDataIntoTable(string tableName, List<Dictionary<string, object>> newDataList)
        {
            // Check if the new data list is empty
            if (newDataList == null || newDataList.Count == 0)
            {
                // Handle empty or null data list
                return;
            }

            // Start a transaction to ensure atomicity
            using (var transaction = sqlite_conn.BeginTransaction())
            {
                try
                {
                    // Delete existing data from the table
                    var deleteQuery = $"DELETE FROM {tableName}";
                    using (SQLiteCommand deleteCommand = new SQLiteCommand(deleteQuery, sqlite_conn))
                    {
                        deleteCommand.ExecuteNonQuery();
                    }

                    // Extract columns from the first dictionary in the new data list
                    var columns = string.Join(", ", newDataList[0].Keys);
                    // Create parameter placeholders for each column
                    var parameterPlaceholders = string.Join(", ", newDataList[0].Keys.Select(key => "@" + key));
                    // Create the insert query
                    var insertQuery = $"INSERT INTO {tableName} ({columns}) VALUES ({parameterPlaceholders})";

                    // Insert new data into the table
                    using (SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, sqlite_conn))
                    {
                        // Iterate through each dictionary in the new data list
                        foreach (var data in newDataList)
                        {
                            // Add parameters for each column-value pair
                            foreach (var kvp in data)
                            {
                                insertCommand.Parameters.AddWithValue("@" + kvp.Key, kvp.Value);
                            }
                            // Execute the query for each dictionary
                            insertCommand.ExecuteNonQuery();
                            // Clear parameters for next iteration
                            insertCommand.Parameters.Clear();
                        }
                    }

                    // Commit the transaction
                    transaction.Commit();
                }
                catch (Exception)
                {
                    // Rollback the transaction if an exception occurs
                    transaction.Rollback();
                    throw; // Re-throw the exception
                }
            }
        }





        //public void InsertDataIntoTable(string tableName, List<Dictionary<string, object>> dataList)
        //{
        //    if (dataList == null || dataList.Count == 0)
        //    {
        //        // Handle empty or null data list
        //        return;
        //    }

        //    // Extract columns from the first dictionary in the list
        //    var columns = string.Join(", ", dataList[0].Keys);
        //    // Create parameter placeholders for each column
        //    var parameterPlaceholders = string.Join(", ", dataList[0].Keys.Select(key => "@" + key));
        //    // Create the insert query
        //    var insertQuery = $"INSERT INTO {tableName} ({columns}) VALUES ({parameterPlaceholders})";

        //    using (SQLiteCommand command = new SQLiteCommand(insertQuery, sqlite_conn))
        //    {
        //        // Iterate through each dictionary in the data list
        //        foreach (var data in dataList)
        //        {
        //            // Check if the data already exists in the table
        //            if (!IsDataExists(tableName, data))
        //            {
        //                // Add parameters for each column-value pair
        //                foreach (var kvp in data)
        //                {
        //                    command.Parameters.AddWithValue("@" + kvp.Key, kvp.Value);
        //                }
        //                // Execute the query for each dictionary
        //                command.ExecuteNonQuery();
        //                // Clear parameters for next iteration
        //                command.Parameters.Clear();
        //            }
        //        }
        //    }
        //}

        //private bool IsDataExists(string tableName, Dictionary<string, object> data)
        //{
        //    var selectQuery = $"SELECT EXISTS(SELECT 1 FROM {tableName} WHERE ";
        //    var conditions = new List<string>();
        //    foreach (var kvp in data)
        //    {
        //        conditions.Add($"{kvp.Key} = @{kvp.Key}");
        //    }
        //    selectQuery += string.Join(" AND ", conditions);
        //    selectQuery += ")";

        //    using (SQLiteCommand command = new SQLiteCommand(selectQuery, sqlite_conn))
        //    {
        //        // Add parameters for each key-value pair in the data dictionary
        //        foreach (var kvp in data)
        //        {
        //            command.Parameters.AddWithValue("@" + kvp.Key, kvp.Value);
        //        }
        //        // Execute the select query
        //        bool exists = Convert.ToInt32(command.ExecuteScalar()) == 1;
        //        return exists;
        //    }
        //}




        public static List<Dictionary<string, object>> ConvertObjectToDictionary(dynamic objList)
        {
            try
            {
                List<Dictionary<string, object>> dictionaryList = new List<Dictionary<string, object>>();

                foreach (object obj in objList)
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    if (obj != null)
                    {
                        PropertyInfo[] properties = obj.GetType().GetProperties();
                        foreach (PropertyInfo property in properties)
                        {
                            if (property.CanRead && property.GetIndexParameters().Length == 0)
                            {
                                object value = property.GetValue(obj);
                                dictionary.Add(property.Name, value);
                            }
                        }
                    }
                    dictionaryList.Add(dictionary);
                }
                return dictionaryList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<HardwareInformationDto> HardwareInformationTable(string tablename)
        {
            SQLiteDataReader reader;
            try
            {

                string ReadData = $"Select * from {tablename}";
                using (SQLiteCommand command = new SQLiteCommand(ReadData, sqlite_conn))
                {
                    using (SQLiteDataReader data = command.ExecuteReader())
                    {

                        while (data.Read())
                        {
                            //            string HardwareSql = "CREATE TABLE IF NOT EXISTS HardwareInformation(
                            //            Id int,
                            //            Name VARCHAR(60),
                            //            Value VARCHAR(60),
                            //            Type VARCHAR(60),
                            //            IsLocal bit,
                            //            IsArray bit,
                            //            Origin VARCHAR(60),
                            //            Qualifires VARCHAR(60),
                            //            SystemId VARCHAR(60),
                            //            TimeStamp datetime2)";


                            HardwareInformationDto pro = new HardwareInformationDto();
                            pro.Id = data.GetInt32(0);
                            pro.Name = data.GetString(1);
                            pro.Value = data.GetString(2);
                            pro.Type = data.GetString(3);
                            pro.IsLocal = data.GetBoolean(4);
                            pro.IsArray = data.GetBoolean(5);
                            pro.Origin = data.GetString(6);
                            pro.Qualifires = data.GetString(7);
                            pro.SystemId = data.GetString(8);
                            pro.TimeStamp = data.GetDateTime(9);

                            hardwareread.Add(pro);

                        }
                        return hardwareread;


                    }

                }

            }

            catch (Exception ex)
            {
                return null;

            }
        }

        public List<SoftwareInformationDto> ReadSoftwareInformationTable(string tablename)
        {

            SQLiteDataReader reader;
            try
            {

                string ReadData = $"Select * from {tablename}";
                using (SQLiteCommand command = new SQLiteCommand(ReadData, sqlite_conn))
                {
                    using (SQLiteDataReader data = command.ExecuteReader())
                    {

                        while (data.Read())
                        {

                            SoftwareInformationDto pro = new SoftwareInformationDto();
                            pro.Id = data.GetInt32(0);
                            pro.WindowTitle = data.GetString(1);
                            pro.ProcessName = data.GetString(2);
                            pro.SystemId = data.GetString(3);
                            pro.MemorySize = data.GetString(4);
                            pro.ModuleName = data.GetString(5);
                            pro.Starttime = data.GetString(6);
                            pro.Pid = data.GetString(7);
                            pro.CpuUsage = data.GetString(8);
                            pro.TimeStamp = data.GetDateTime(9);

                            softwareread.Add(pro);

                        }
                        return softwareread;


                    }

                }

            }

            catch (Exception ex)
            {
                return null;

            }
        }

        public List<InstalledSoftwareDto> ReadInstalledSoftwareTable(string tablename)
        {
            SQLiteDataReader reader;
            try
            {

                string ReadData = $"Select * from {tablename}";
                using (SQLiteCommand command = new SQLiteCommand(ReadData, sqlite_conn))
                {
                    using (SQLiteDataReader data = command.ExecuteReader())
                    {
                        HashSet<string> uniqueDisplayNames = new HashSet<string>();

                        while (data.Read())
                        {
                            string displayName = data.GetString(1);

                            if (!uniqueDisplayNames.Contains(displayName))
                            {
                                InstalledSoftwareDto pro = new InstalledSoftwareDto();
                                pro.Id = data.GetInt32(0);
                                pro.DisplayName = data.GetString(1);
                                pro.Publisher = data.GetString(2);
                                pro.InstalledOn = data.GetString(3);
                                pro.Size = data.GetString(4);
                                pro.SystemId = data.GetString(5);
                                pro.Version = data.GetString(6);

                                pro.TimeStamp = data.GetDateTime(7);
                                uniqueDisplayNames.Add(displayName);
                                installedsoftwareread.Add(pro);
                            }

                        }
                        return installedsoftwareread;


                    }

                }

            }

            catch (Exception ex)
            {
                return null;

            }
        }

        public List<PortInformationDto> ReadPortTable(string tablename)
        {
            SQLiteDataReader reader;
            try
            {

                string ReadData = $"Select * from {tablename}";
                using (SQLiteCommand command = new SQLiteCommand(ReadData, sqlite_conn))
                {
                    using (SQLiteDataReader data = command.ExecuteReader())
                    {

                        while (data.Read())
                        {



                            PortInformationDto pro = new PortInformationDto();
                            pro.Id = data.GetInt32(0);
                            pro.LocalEndpoint = data.GetString(1);
                            pro.LocalPort = data.GetString(2);
                            pro.RemoteEndpint = data.GetString(3);
                            pro.RemotePort = data.GetString(4);
                            pro.State = data.GetString(5);
                            pro.ProcessId = data.GetInt32(6);
                            pro.Service = data.GetString(7);
                            pro.SystemId = data.GetString(8);
                            pro.TimeStamp = data.GetDateTime(9);

                            portread.Add(pro);

                        }
                        return portread;


                    }

                }

            }

            catch (Exception ex)
            {
                return null;

            }

        }
        public List<OSCoreDto> ReadOSCoreTable(string tablename)
        {
            SQLiteDataReader reader;
            try
            {

                string ReadData = $"Select * from {tablename}";
                using (SQLiteCommand command = new SQLiteCommand(ReadData, sqlite_conn))
                {
                    using (SQLiteDataReader data = command.ExecuteReader())
                    {

                        while (data.Read())
                        {



                            OSCoreDto pro = new OSCoreDto();
                            pro.WindowActivationStatus = data.GetString(1);
                            pro.WindowProductKey = data.GetString(0);
                            pro.SystemId = data.GetString(2);
                            pro.TimeStamp = data.GetDateTime(3);

                            oscoreread.Add(pro);

                        }
                        return oscoreread;


                    }

                }

            }

            catch (Exception ex)
            {
                return null;

            }






        }
        public List<AntivirusDetailsDto> ReadantivirusTable(string tablename)
        {
            SQLiteDataReader reader;
            try
            {

                string ReadData = $"Select * from {tablename}";
                using (SQLiteCommand command = new SQLiteCommand(ReadData, sqlite_conn))
                {
                    using (SQLiteDataReader data = command.ExecuteReader())
                    {

                        while (data.Read())
                        {



                            AntivirusDetailsDto pro = new AntivirusDetailsDto();
                            pro.AntivirusName = data.GetString(2);
                            pro.TimeStamp = data.GetDateTime(3);
                            pro.SystemId = data.GetString(0);
                            //pro.Id = data.GetInt32(1);

                            antiread.Add(pro);

                        }
                        return antiread;


                    }

                }

            }

            catch (Exception ex)
            {
                return null;

            }












        }
        public List<DeviceDetailsDto> ReaddeviceTable(string tablename)
        {
            SQLiteDataReader reader;
            try
            {

                string ReadData = $"Select * from {tablename}";
                using (SQLiteCommand command = new SQLiteCommand(ReadData, sqlite_conn))
                {
                    using (SQLiteDataReader data = command.ExecuteReader())
                    {

                        while (data.Read())
                        {



                            DeviceDetailsDto pro = new DeviceDetailsDto();
                            pro.BIOS = data.GetString(0);
                            pro.DeviceVersion = data.GetString(1);
                            pro.DeviceName = data.GetString(2);
                            pro.MACAddress = data.GetString(3);
                            pro.IPAddress = data.GetString(4);
                            pro.VirtualMemory = data.GetString(5);
                            pro.AvailableVirtualMemory = data.GetString(6);
                            pro.DisplayManufacturer = data.GetString(7);
                            pro.DisplayDetails = data.GetString(8);
                            pro.DisplayName = data.GetString(9);
                            pro.SystemId = data.GetString(11);

                            pro.TimeStamp = data.GetDateTime(10);

                            deviceread.Add(pro);

                        }
                        return deviceread;


                    }

                }

            }

            catch (Exception ex)
            {
                return null;

            }
        }

        public List<HarddiskDetailsDto> ReadHarddiskTable(string tablename)
        {
            SQLiteDataReader reader;
            try
            {

                //SQLiteCommand sqlite_cmd;
                string ReadData = $"Select * from {tablename}";
                using (SQLiteCommand command = new SQLiteCommand(ReadData, sqlite_conn))
                {
                    using (SQLiteDataReader data = command.ExecuteReader())
                    {

                        while (data.Read())
                        {


                            HarddiskDetailsDto pro = new HarddiskDetailsDto();
                            pro.SystemId = data.GetString(0);
                            pro.DriveName = data.GetString(1);
                            pro.DriveType = data.GetString(2);
                            pro.DriveFormat = data.GetString(3);
                            pro.SerialNumber = data.GetString(4);
                            pro.TotalSize = data.GetString(5);
                            pro.FreeSpace = data.GetString(6);
                            pro.AvailableFreeSpace = data.GetString(7);
                            pro.NonSystemDriveName = data.GetString(8);
                            pro.NonSystemTotalSpace = data.GetString(9);
                            pro.NonSystemFreeSpace = data.GetString(10);

                            pro.TimeStamp = data.GetDateTime(11);

                            harddiskread.Add(pro);

                        }
                        return harddiskread;


                    }

                }

            }

            catch (Exception ex)
            {
                return null;

            }

        }

        public List<OSDto> ReadOSTable(string tablename)
        {
            SQLiteDataReader reader;
            try
            {

                //SQLiteCommand sqlite_cmd;
                string ReadData = $"Select * from {tablename}";
                using (SQLiteCommand command = new SQLiteCommand(ReadData, sqlite_conn))
                {
                    using (SQLiteDataReader data = command.ExecuteReader())
                    {

                        while (data.Read())
                        {

                            OSDto pro = new OSDto();
                            pro.SerialNumber = data.GetString(0);
                            pro.OS_Name = data.GetString(1);
                            pro.OS_Version = data.GetString(2);
                            pro.OS_Architecture = data.GetString(3);
                            pro.Build_Number = data.GetString(4);
                            pro.Manufacturer = data.GetString(5);
                            pro.LastBootUpTime = data.GetString(6);
                            pro.NoOfDaysLastSystemBoot = data.GetString(7);
                            pro.MembershipType = data.GetString(8);
                            pro.LastLogged = data.GetString(9);
                            pro.LastLoggedUserRole = data.GetString(10);
                            pro.CurrentUser = data.GetString(11);
                            pro.TimeStamp = data.GetDateTime(12);
                            pro.SystemId = data.GetString(13);
                            OSread.Add(pro);

                        }
                        return OSread;


                    }

                }

            }

            catch (Exception ex)
            {
                return null;

            }
        }





        public List<WindowsServicesDto> ReadTable(string tablename)
        {
            SQLiteDataReader reader;

            try
            {

                //SQLiteCommand sqlite_cmd;
                string ReadData = $"Select * from {tablename}";
                using (SQLiteCommand command = new SQLiteCommand(ReadData, sqlite_conn))
                {
                    using (SQLiteDataReader data = command.ExecuteReader())
                    {

                        while (data.Read())
                        {

                            WindowsServicesDto pro = new WindowsServicesDto();
                            //pro.UserId = reader.GetString(0);
                            //pro.MemoryUsages = reader.GetFloat(1);
                            //pro.CpuUsgaes = reader.GetFloat(2);
                            //pro.NetworkUsgaes = reader.GetFloat(3);
                            //pro.CPU_Alert = reader.GetString(4);
                            //pro.CPU_TimeStamp = reader.GetDateTime(5);
                            //pro.Memory_Alert = reader.GetString(6);
                            //pro.Memory_TimeStamp = reader.GetDateTime(7);
                            //pro.Network_Alert = reader.GetString(8);
                            //pro.Network_TimeStamp = reader.GetDateTime(9);
                            pro.Startup = data.GetBoolean(3);
                            pro.TimeStamp = data.GetDateTime(4);
                            pro.ServiceStatus = data.GetString(2);
                            pro.ServiceName = data.GetString(0);
                            pro.ServiceDisplayName = data.GetString(1);
                            pro.SystemId = data.GetString(5);
                            jsondataread.Add(pro);
                        }
                        //while (reader.Read())
                        //{

                        //    //ProgramData pro = new ProgramData();
                        //    //pro.UserId = reader.GetString(0);
                        //    //pro.MemoryUsages = reader.GetFloat(1);
                        //    //pro.CpuUsgaes = reader.GetFloat(2);
                        //    //pro.NetworkUsgaes = reader.GetFloat(3);
                        //    //pro.CPU_Alert = reader.GetString(4);
                        //    //pro.CPU_TimeStamp = reader.GetDateTime(5);
                        //    //pro.Memory_Alert = reader.GetString(6);
                        //    //pro.Memory_TimeStamp = reader.GetDateTime(7);
                        //    //pro.Network_Alert = reader.GetString(8);
                        //    //pro.Network_TimeStamp = reader.GetDateTime(9);
                        //    //jsondataread.Add(pro);

                        //}
                        return jsondataread;
                    }

                }
            }





            catch (Exception ex)
            {
                return null;

            }


        }


        //private async void ReadTable(SQLiteConnection conn)
        //{
        //    try
        //    {

        //        SQLiteCommand sqlite_cmd;
        //        string ReadData = "Select * from Backgroundservice";
        //        using (SQLiteCommand command = new SQLiteCommand(ReadData, conn))
        //        {
        //            using (SQLiteDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {

        //                    ProgramData pro = new ProgramData();
        //                    pro.UserId = reader.GetString(0);
        //                    pro.MemoryUsages = reader.GetFloat(1);
        //                    pro.CpuUsgaes = reader.GetFloat(2);
        //                    pro.NetworkUsgaes = reader.GetFloat(3);
        //                    pro.CPU_Alert = reader.GetString(4);
        //                    pro.CPU_TimeStamp = reader.GetDateTime(5);
        //                    pro.Memory_Alert = reader.GetString(6);
        //                    pro.Memory_TimeStamp = reader.GetDateTime(7);
        //                    pro.Network_Alert = reader.GetString(8);
        //                    pro.Network_TimeStamp = reader.GetDateTime(9);
        //                    jsondataread.Add(pro);

        //                }
        //            }

        //        }



        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //private async void CompareData()
        //{
        //    try
        //    {
        //        foreach (var newData in allprogramData)
        //        {
        //            var existingData = jsondataread.FirstOrDefault(x => x.UserId == newData.UserId);
        //            if (existingData == null)
        //            {
        //                // If the data doesn't exist in the database, insert it
        //                SendDataToApi(newData);
        //            }
        //            else if (!AreEqual(newData, existingData))
        //            {
        //                // If the data exists but has changed, update it
        //                SendDataToApi(newData);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions
        //    }
        //}

        //private bool AreEqual(ProgramData newData, ProgramData existingData)
        //{
        //    // Compare properties of newData and existingData to check if they are equal
        //    return newData.MemoryUsages == existingData.MemoryUsages &&
        //           newData.CpuUsgaes == existingData.CpuUsgaes &&
        //           newData.NetworkUsgaes == existingData.NetworkUsgaes &&
        //           newData.CPU_Alert == existingData.CPU_Alert &&
        //           newData.Memory_Alert == existingData.Memory_Alert &&
        //           newData.Network_Alert == existingData.Network_Alert;
        //}




        //ALl Get API


        public async Task<Response> GetDataToAPI(string url)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<Response>(jsonString.ToString());
                        return data;


                    }
                    else
                    {
                        Response response1 = new Response();
                        return response1;
                    }
                }
            }
            catch (Exception ex)
            {
                Response response1 = new Response();
                return response1;
                // Handle exceptions (e.g., log error, notify user)
            }
        }

    }
}
