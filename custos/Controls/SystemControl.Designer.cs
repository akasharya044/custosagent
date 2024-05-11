namespace custos.Controls
{
	partial class SystemControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            tabControl1 = new TabControl();
            sysPage = new TabPage();
            softwareInfo1 = new SoftwareInfo();
            hardwareInfo1 = new HardwareInfo();
            tabPage1 = new TabPage();
            hardwareInfo2 = new HardwareInfo();
            tabPage2 = new TabPage();
            osCoreInformation1 = new SubControl.OSCoreInformation();
            tabPage3 = new TabPage();
            portControl1 = new SubControl.PortControl();
            panel1.SuspendLayout();
            tabControl1.SuspendLayout();
            sysPage.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(tabControl1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(312, 222);
            panel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(sysPage);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Margin = new Padding(3, 2, 3, 2);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(312, 222);
            tabControl1.TabIndex = 0;
            // 
            // sysPage
            // 
            sysPage.Controls.Add(softwareInfo1);
            sysPage.Controls.Add(hardwareInfo1);
            sysPage.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sysPage.Location = new Point(4, 24);
            sysPage.Margin = new Padding(3, 2, 3, 2);
            sysPage.Name = "sysPage";
            sysPage.Padding = new Padding(3, 2, 3, 2);
            sysPage.Size = new Size(304, 194);
            sysPage.TabIndex = 0;
            sysPage.Text = "Software Information";
            sysPage.UseVisualStyleBackColor = true;
            // 
            // softwareInfo1
            // 
            softwareInfo1.BackColor = SystemColors.Control;
            softwareInfo1.BorderStyle = BorderStyle.FixedSingle;
            softwareInfo1.Dock = DockStyle.Fill;
            softwareInfo1.Location = new Point(3, 2);
            softwareInfo1.Margin = new Padding(3, 2, 3, 2);
            softwareInfo1.Name = "softwareInfo1";
            softwareInfo1.Size = new Size(298, 190);
            softwareInfo1.TabIndex = 0;
            softwareInfo1.TabStop = false;
            softwareInfo1.Load += SoftwareInfo_Load;
            // 
            // hardwareInfo1
            // 
            hardwareInfo1.BackColor = SystemColors.Control;
            hardwareInfo1.BorderStyle = BorderStyle.FixedSingle;
            hardwareInfo1.Dock = DockStyle.Fill;
            hardwareInfo1.Location = new Point(3, 2);
            hardwareInfo1.Margin = new Padding(3, 2, 3, 2);
            hardwareInfo1.Name = "hardwareInfo1";
            hardwareInfo1.Size = new Size(298, 190);
            hardwareInfo1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(hardwareInfo2);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Margin = new Padding(3, 2, 3, 2);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(304, 194);
            tabPage1.TabIndex = 1;
            tabPage1.Text = "Hardware Information";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // hardwareInfo2
            // 
            hardwareInfo2.BackColor = SystemColors.Control;
            hardwareInfo2.BorderStyle = BorderStyle.FixedSingle;
            hardwareInfo2.Dock = DockStyle.Fill;
            hardwareInfo2.Location = new Point(0, 0);
            hardwareInfo2.Margin = new Padding(4);
            hardwareInfo2.Name = "hardwareInfo2";
            hardwareInfo2.Size = new Size(304, 194);
            hardwareInfo2.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(osCoreInformation1);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Size = new Size(304, 194);
            tabPage2.TabIndex = 2;
            tabPage2.Text = "Window Core Information";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // osCoreInformation1
            // 
            osCoreInformation1.Dock = DockStyle.Fill;
            osCoreInformation1.Location = new Point(0, 0);
            osCoreInformation1.Name = "osCoreInformation1";
            osCoreInformation1.Size = new Size(304, 194);
            osCoreInformation1.TabIndex = 0;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(portControl1);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(304, 194);
            tabPage3.TabIndex = 3;
            tabPage3.Text = "Port Information";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // portControl1
            // 
            portControl1.Dock = DockStyle.Fill;
            portControl1.Location = new Point(0, 0);
            portControl1.Name = "portControl1";
            portControl1.Size = new Size(304, 194);
            portControl1.TabIndex = 0;
            // 
            // SystemControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "SystemControl";
            Size = new Size(312, 222);
            panel1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            sysPage.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TabControl tabControl1;
        private TabPage sysPage;
        private SoftwareInfo softwareInfo1;
        private HardwareInfo hardwareInfo1;
        private TabPage tabPage1;
        private HardwareInfo hardwareInfo2;
        private TabPage tabPage2;
        private SubControl.OSCoreInformation osCoreInformation1;
        private TabPage tabPage3;
        private SubControl.PortControl portControl1;
        private SubControl.Loader loader1;
    }
}
