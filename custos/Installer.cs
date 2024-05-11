using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Reflection;
using custos.Forms;




namespace custos
{
    

    [RunInstaller(true)]
    public class InstallerClass : System.Configuration.Install.Installer
    {
        public InstallerClass()
          : base()
        {
            MessageBox.Show("I am working");
			this.BeforeInstall += new InstallEventHandler(BeforeInstallEventHandler);
			
			this.Committed += new InstallEventHandler(MyInstaller_Committed);
           
            this.Committing += new InstallEventHandler(MyInstaller_Committing);
        }
		private void BeforeInstallEventHandler(object sender, InstallEventArgs e)
		{
			try
			{
				string processName = "custosWorker";

				
				Process[] processes = Process.GetProcessesByName(processName);

				if (processes.Length > 0)
				{
					processes[0].Kill();
					
				}

                MessageBox.Show("I worked");

			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
			}
			
		}
		
		private void MyInstaller_Committing(object sender, InstallEventArgs e)
        {
            
        }

        
        private void MyInstaller_Committed(object sender, InstallEventArgs e)
        {
   
        }

       
        public override void Install(IDictionary savedState)
		{

			base.Install(savedState);
			
		}

        
        public override void Commit(IDictionary savedState)
        {
           
                

                try
                {
                    Directory.SetCurrentDirectory(Path.GetDirectoryName
                    (Assembly.GetExecutingAssembly().Location));
                    Process.Start(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\custosWorker.exe");
                   
                }
                catch(Exception ex)
                {
                    
                }

            
            

            base.Commit(savedState);
        }

       


        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }



        public override void Uninstall(IDictionary savedState)
        {

            try
            {
                PasswordForm pass = new PasswordForm();
                pass.ShowDialog();
                if (pass.status == true)
                {

                    string processName = "custosWorker";


                    Process[] processes = Process.GetProcessesByName(processName);

                    if (processes.Length > 0)
                    {
                        processes[0].Kill();

                    }
                }
                else
                {
					pass.Close();
					return;
                    
                }
            }
            catch (Exception ex)
            {
                
            }

     
            base.Uninstall(savedState);
        }


    }
}
