using System;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using custosWorker.Forms;
using System.Data.SQLite;

namespace custosWorker
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var builder = Host.CreateDefaultBuilder(args);
            builder.ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>();
            });

            

            var host = builder.Build();

            // Start the form in a separate thread
            Thread formThread = new Thread(() =>
            {
                Application.Run(new WorkerForm());
            });
            formThread.SetApartmentState(ApartmentState.STA); // Set apartment state for COM threading
            formThread.Start();

            // Run the host in the main thread
            host.Run();
        }
    }
}
