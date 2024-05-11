using custosWorker.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace custosWorker.Service
{
    public class RabbitBus : IBus
    {
        private readonly IModel _channel;
        
        internal RabbitBus(IModel channel)
        {
            _channel = channel;
        }


        public async Task SendAsync<T>(string queue, T message)
        {
            await Task.Run(() =>
            {
                _channel.QueueDeclare(queue, true, false, false);
                var properties = _channel.CreateBasicProperties();
                properties.Persistent = false;
                if (message.GetType() == typeof(string))
                {
                    var output1 = message.ToString();
                    _channel.BasicPublish(string.Empty, queue, null,
                    Encoding.UTF8.GetBytes(output1));
                }
                else
                {
                    var output = JsonConvert.SerializeObject(message);
                    _channel.BasicPublish(string.Empty, queue, null,
                    Encoding.UTF8.GetBytes(output));
                }

            });
        }
        public async Task CreateExchange<T>(string exchangename, T message)
        {
            await Task.Run(() =>
            {


                _channel.ExchangeDeclare(exchange: exchangename, type: ExchangeType.Fanout);


                if (message.GetType() == typeof(string))
                {
                    var output1 = message.ToString();
                    _channel.BasicPublish(exchange: exchangename,
                                   routingKey: string.Empty,
                                   basicProperties: null,
                                   body: Encoding.UTF8.GetBytes(output1));
                }
                else
                {
                    var output = JsonConvert.SerializeObject(message);
                    _channel.BasicPublish(exchange: exchangename,
                                   routingKey: string.Empty,
                                   basicProperties: null,
                                   body: Encoding.UTF8.GetBytes(output));
                }

            });
        }
        public async Task ReceiveAsync<T>(string queue, Action<string> onMessage)
        {
            _channel.QueueDeclare(queue, true, false, false);
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += async (s, e) =>
            {
                //Console.WriteLine("Message Received ");
                byte[] body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                onMessage(message);
                await Task.Yield();
            };
            _channel.BasicConsume(queue, true, consumer);
            await Task.Yield();
        }
        public async Task ReceiveFromExchangeAsync<T>(string exchangename, Action<string> onMessage)
        {

            _channel.ExchangeDeclare(exchange: exchangename, type: ExchangeType.Fanout);

            // declare a server-named queue
            var queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: queueName,
                              exchange: exchangename,
                              routingKey: string.Empty);


            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (s, e) =>
            {
                byte[] body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                rabbitDTO data = JsonConvert.DeserializeObject<rabbitDTO>(message);

                string commandid = data.commandid;
                string systemid = data.systemid;
                string processname = data.processname;
                string broadcast = data.message;
                string commandvalue = data.commandarguments;
                int pid = (int)data.processid;
                List<string> systemids = data.systemids;
                string onlinedownloadfile = data.onlinedownloadpath;
                if (commandid == "102")
                {
                    if(systemids.Contains(System.Environment.MachineName))
                    {
                        await ShowMessageBoxAsync(broadcast);
                    }
                }
                else if (commandid == "105")
                {
                    if (systemids.Contains(System.Environment.MachineName))
                    {
                        await Onlinedownloading(onlinedownloadfile);
                            
                        }
                }

                else
                {
                    if (systemid == System.Environment.MachineName)
                    {
                        switch (commandid)
                        {
                            case "101":
                                await DeleteProcessAsync(processname);
                                break;
                            case "103":
                                await ShutdownExecuteAsync(commandvalue);
                                break;
                            case "104":
                                await KillProcessAndChildren(pid);
                                break;
                            
                            default:
                                break;
                        }
                    }
                }



            };

            _channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);


            await Task.Yield();
        }


        public async Task RabbitReceiveAsync<T>(string queue, Action<string> onMessage)
        {
            
        }



		private async Task Onlinedownloading(string appUrl)
		{
			string tempZipFile = "temp.zip"; // Temporary ZIP file name
			string tempExtractFolder = "temp"; // Temporary folder for extraction

			try
			{
				// Check if the destination file already exists and delete it if necessary
				if (File.Exists(tempZipFile))
				{
					File.Delete(tempZipFile);
				}

				using (var webClient = new WebClient())
				{
					// Download the ZIP file asynchronously
					await webClient.DownloadFileTaskAsync(appUrl, tempZipFile);
				}

				

				// Extract the ZIP file asynchronously
				await Task.Run(() => ZipFile.ExtractToDirectory(tempZipFile, tempExtractFolder));

				// Find the MSI file in the extracted folder
				string[] msiFiles = Directory.GetFiles(tempExtractFolder, "*.msi");

				if (msiFiles.Length == 0)
				{
					Console.WriteLine("MSI file not found in the ZIP archive.");
					return;
				}

				// Execute the MSI installer
				Process installer = Process.Start("msiexec", $"/i \"{msiFiles[0]}\" /qn"); // /i for install, /qn for silent mode
				await Task.Run(() => installer.WaitForExit());

				// Check the installer exit code
				if (installer.ExitCode == 0)
				{
					Console.WriteLine("Installation successful!");
				}
				else
				{
					Console.WriteLine("Installation failed!");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
			}
			finally
			{
				// Clean up
				if (File.Exists(tempZipFile))
					File.Delete(tempZipFile);

				if (Directory.Exists(tempExtractFolder))
					Directory.Delete(tempExtractFolder, true);
			}
		}





		private async Task DeleteProcessAsync(string runningProcess)
        {
            Process[] processes = Process.GetProcessesByName(runningProcess);
            if (processes.Length > 0)
            {
                
                foreach (Process process in processes)
                {
                    try
                    {
                        process.Kill();
                        await Task.Delay(100);


                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message.ToString());
                    }
                    await Task.Delay(100);
                }
            }
        }

        private async Task ShowMessageBoxAsync(string broadcast)
        {
            try
            {
                BroadCast cast = new BroadCast("BroadCast", broadcast);
                cast.ShowDialog();
                 

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }

        private async Task ShutdownExecuteAsync(string commmand)
        {
            try
            {
                

                //var processInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
                var processinfo = new ProcessStartInfo("shutdown.exe" , commmand )
                {
                    CreateNoWindow = false,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true

                };

                var process = Process.Start(processinfo);

                if (process != null)
                {
                    process.WaitForExit();
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    //MessageBox.Show("Output: " + output);
                    Console.WriteLine("Error: " + error);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            await Task.Delay(100);
        }

        private async Task  KillProcessAndChildren(int pid)
        {
            
            if (pid == 0)
            {
                return;
            }
            ManagementObjectSearcher searcher = new ManagementObjectSearcher
                    ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection moc = searcher.Get();
            foreach (ManagementObject mo in moc)
            {
                KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
            }
            try
            {
                Process proc = Process.GetProcessById(pid);
                proc.Kill();
            }
            catch (ArgumentException)
            {
                
            }
        }

        
       
        public class rabbitDTO
        {
            public string? commandid { get; set; }
            public string? systemid { get; set; }
            public string? processname { get; set; }

            public string? message { get; set; }
            public string? commandarguments { get; set; }

            public int? processid { get; set; }

            public List<string>? systemids { get; set; }
            public string? onlinedownloadpath { get; set; }
        }












    }
}
