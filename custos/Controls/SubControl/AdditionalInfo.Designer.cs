namespace custos.Controls.SubControl
{
    partial class AdditionalInfo
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
            components = new System.ComponentModel.Container();
            panel1 = new Panel();
            progressBar1 = new ProgressBar();
            label1 = new Label();
            panel2 = new Panel();
            panel4 = new Panel();
            pictureBox2 = new PictureBox();
            label3 = new Label();
            panel3 = new Panel();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            panel9 = new Panel();
            pictureBox6 = new PictureBox();
            label7 = new Label();
            panel8 = new Panel();
            pictureBox5 = new PictureBox();
            label6 = new Label();
            panel7 = new Panel();
            pictureBox4 = new PictureBox();
            label5 = new Label();
            panel6 = new Panel();
            pictureBox3 = new PictureBox();
            label4 = new Label();
            splitter2 = new Splitter();
            splitter1 = new Splitter();
            osinformation1 = new osinformation();
            antivirusControl1 = new AntivirusControl();
            deviceinfo1 = new Deviceinfo();
            hardDiskinfo1 = new HardDiskinfo();
            errorProvider1 = new ErrorProvider(components);
            services1 = new Services();
            installedSoftware1 = new InstalledSoftware();
            loader1 = new Loader();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Info;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(progressBar1);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(962, 28);
            panel1.TabIndex = 0;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(346, 10);
            progressBar1.Margin = new Padding(3, 2, 3, 2);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(579, 11);
            progressBar1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.MenuHighlight;
            label1.Location = new Point(9, 2);
            label1.Name = "label1";
            label1.Size = new Size(286, 21);
            label1.TabIndex = 0;
            label1.Text = "Gathering Infromation Please Wait...";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ButtonHighlight;
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(panel9);
            panel2.Controls.Add(panel8);
            panel2.Controls.Add(panel7);
            panel2.Controls.Add(panel6);
            panel2.Controls.Add(splitter2);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 28);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(283, 572);
            panel2.TabIndex = 1;
            // 
            // panel4
            // 
            panel4.Controls.Add(pictureBox2);
            panel4.Controls.Add(label3);
            panel4.Location = new Point(4, 270);
            panel4.Margin = new Padding(3, 2, 3, 2);
            panel4.Name = "panel4";
            panel4.Size = new Size(267, 40);
            panel4.TabIndex = 34;
            panel4.Click += software_click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.WhatsApp_Image_2024_03_06_at_16_34_37;
            pictureBox2.Location = new Point(11, 8);
            pictureBox2.Margin = new Padding(3, 2, 3, 2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(32, 26);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            pictureBox2.Click += software_click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.8F);
            label3.Location = new Point(46, 11);
            label3.Name = "label3";
            label3.Size = new Size(128, 20);
            label3.TabIndex = 0;
            label3.Text = "Installed Software";
            label3.Click += software_click;
            // 
            // panel3
            // 
            panel3.Controls.Add(pictureBox1);
            panel3.Controls.Add(label2);
            panel3.Location = new Point(6, 219);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(267, 40);
            panel3.TabIndex = 33;
            panel3.Click += service_click;
            panel3.Paint += panel3_Paint;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.service__1_;
            pictureBox1.Location = new Point(11, 8);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(32, 26);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            pictureBox1.Click += service_click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.8F);
            label2.Location = new Point(46, 11);
            label2.Name = "label2";
            label2.Size = new Size(127, 20);
            label2.TabIndex = 0;
            label2.Text = "Windows Services";
            label2.Click += service_click;
            // 
            // panel9
            // 
            panel9.Controls.Add(pictureBox6);
            panel9.Controls.Add(label7);
            panel9.Location = new Point(6, 165);
            panel9.Margin = new Padding(3, 2, 3, 2);
            panel9.Name = "panel9";
            panel9.Size = new Size(267, 40);
            panel9.TabIndex = 31;
            panel9.Click += harddisk_click;
            // 
            // pictureBox6
            // 
            pictureBox6.Image = Properties.Resources.hard_disk;
            pictureBox6.Location = new Point(11, 8);
            pictureBox6.Margin = new Padding(3, 2, 3, 2);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(32, 26);
            pictureBox6.TabIndex = 1;
            pictureBox6.TabStop = false;
            pictureBox6.Click += harddisk_click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10.8F);
            label7.Location = new Point(46, 11);
            label7.Name = "label7";
            label7.Size = new Size(152, 20);
            label7.TabIndex = 0;
            label7.Text = "HardDisk Information";
            label7.Click += harddisk_click;
            // 
            // panel8
            // 
            panel8.Controls.Add(pictureBox5);
            panel8.Controls.Add(label6);
            panel8.Location = new Point(6, 113);
            panel8.Margin = new Padding(3, 2, 3, 2);
            panel8.Name = "panel8";
            panel8.Size = new Size(267, 40);
            panel8.TabIndex = 30;
            panel8.Click += device_click;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = Properties.Resources.responsive;
            pictureBox5.Location = new Point(11, 8);
            pictureBox5.Margin = new Padding(3, 2, 3, 2);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(32, 26);
            pictureBox5.TabIndex = 1;
            pictureBox5.TabStop = false;
            pictureBox5.Click += device_click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10.8F);
            label6.Location = new Point(46, 11);
            label6.Name = "label6";
            label6.Size = new Size(104, 20);
            label6.TabIndex = 0;
            label6.Text = "Device Details";
            label6.Click += device_click;
            // 
            // panel7
            // 
            panel7.Controls.Add(pictureBox4);
            panel7.Controls.Add(label5);
            panel7.Location = new Point(6, 60);
            panel7.Margin = new Padding(3, 2, 3, 2);
            panel7.Name = "panel7";
            panel7.Size = new Size(267, 40);
            panel7.TabIndex = 29;
            panel7.Click += anti_click;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.shield;
            pictureBox4.Location = new Point(11, 8);
            pictureBox4.Margin = new Padding(3, 2, 3, 2);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(32, 26);
            pictureBox4.TabIndex = 1;
            pictureBox4.TabStop = false;
            pictureBox4.Click += anti_click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.8F);
            label5.Location = new Point(46, 11);
            label5.Name = "label5";
            label5.Size = new Size(148, 20);
            label5.TabIndex = 0;
            label5.Text = "Antivirus Information";
            label5.Click += anti_click;
            // 
            // panel6
            // 
            panel6.Controls.Add(pictureBox3);
            panel6.Controls.Add(label4);
            panel6.Location = new Point(6, 8);
            panel6.Margin = new Padding(3, 2, 3, 2);
            panel6.Name = "panel6";
            panel6.Size = new Size(267, 40);
            panel6.TabIndex = 28;
            panel6.Click += os_click;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.operating_system;
            pictureBox3.Location = new Point(11, 8);
            pictureBox3.Margin = new Padding(3, 2, 3, 2);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(32, 26);
            pictureBox3.TabIndex = 1;
            pictureBox3.TabStop = false;
            pictureBox3.Click += os_click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.8F);
            label4.Location = new Point(43, 10);
            label4.Name = "label4";
            label4.Size = new Size(209, 20);
            label4.TabIndex = 0;
            label4.Text = "Operating System Information";
            label4.Click += os_click;
            // 
            // splitter2
            // 
            splitter2.Location = new Point(0, 0);
            splitter2.Margin = new Padding(3, 2, 3, 2);
            splitter2.Name = "splitter2";
            splitter2.Size = new Size(329, 572);
            splitter2.TabIndex = 25;
            splitter2.TabStop = false;
            // 
            // splitter1
            // 
            splitter1.Location = new Point(283, 28);
            splitter1.Margin = new Padding(3, 2, 3, 2);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(4, 572);
            splitter1.TabIndex = 2;
            splitter1.TabStop = false;
            // 
            // osinformation1
            // 
            osinformation1.BorderStyle = BorderStyle.FixedSingle;
            osinformation1.Dock = DockStyle.Fill;
            osinformation1.Location = new Point(287, 28);
            osinformation1.Margin = new Padding(3, 2, 3, 2);
            osinformation1.Name = "osinformation1";
            osinformation1.Size = new Size(675, 572);
            osinformation1.TabIndex = 3;
            // 
            // antivirusControl1
            // 
            antivirusControl1.BackColor = SystemColors.Control;
            antivirusControl1.Dock = DockStyle.Fill;
            antivirusControl1.Location = new Point(287, 28);
            antivirusControl1.Margin = new Padding(3, 2, 3, 2);
            antivirusControl1.Name = "antivirusControl1";
            antivirusControl1.Size = new Size(675, 572);
            antivirusControl1.TabIndex = 4;
            // 
            // deviceinfo1
            // 
            deviceinfo1.Dock = DockStyle.Fill;
            deviceinfo1.Location = new Point(287, 28);
            deviceinfo1.Margin = new Padding(3, 2, 3, 2);
            deviceinfo1.Name = "deviceinfo1";
            deviceinfo1.Size = new Size(675, 572);
            deviceinfo1.TabIndex = 5;
            // 
            // hardDiskinfo1
            // 
            hardDiskinfo1.Dock = DockStyle.Fill;
            hardDiskinfo1.Location = new Point(287, 28);
            hardDiskinfo1.Margin = new Padding(3, 2, 3, 2);
            hardDiskinfo1.Name = "hardDiskinfo1";
            hardDiskinfo1.Size = new Size(675, 572);
            hardDiskinfo1.TabIndex = 6;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // services1
            // 
            services1.Dock = DockStyle.Fill;
            services1.Location = new Point(287, 28);
            services1.Margin = new Padding(3, 2, 3, 2);
            services1.Name = "services1";
            services1.Size = new Size(675, 572);
            services1.TabIndex = 7;
            // 
            // installedSoftware1
            // 
            installedSoftware1.Dock = DockStyle.Fill;
            installedSoftware1.Location = new Point(287, 28);
            installedSoftware1.Name = "installedSoftware1";
            installedSoftware1.Size = new Size(675, 572);
            installedSoftware1.TabIndex = 8;
            // 
            // loader1
            // 
            loader1.BackColor = SystemColors.ButtonHighlight;
            loader1.Dock = DockStyle.Fill;
            loader1.Location = new Point(287, 28);
            loader1.Margin = new Padding(3, 2, 3, 2);
            loader1.Name = "loader1";
            loader1.Size = new Size(675, 572);
            loader1.TabIndex = 9;
            // 
            // AdditionalInfo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(loader1);
            Controls.Add(installedSoftware1);
            Controls.Add(services1);
            Controls.Add(hardDiskinfo1);
            Controls.Add(deviceinfo1);
            Controls.Add(antivirusControl1);
            Controls.Add(osinformation1);
            Controls.Add(splitter1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "AdditionalInfo";
            Size = new Size(962, 600);
            Load += additionalinfo_load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private Panel panel9;
        private PictureBox pictureBox6;
        private Label label7;
        private Panel panel8;
        private PictureBox pictureBox5;
        private Label label6;
        private Panel panel7;
        private PictureBox pictureBox4;
        private Label label5;
        private Panel panel6;
        private PictureBox pictureBox3;
        private Label label4;
        private Splitter splitter2;
        private Splitter splitter1;
        private osinformation osinformation1;
        private AntivirusControl antivirusControl1;
        private Deviceinfo deviceinfo1;
        private HardDiskinfo hardDiskinfo1;
        private ProgressBar progressBar1;
        private ErrorProvider errorProvider1;
        private Panel panel3;
        private PictureBox pictureBox1;
        private Label label2;
        private Services services1;
        private Panel panel4;
        private PictureBox pictureBox2;
        private Label label3;
        private Loader loader1;
        private InstalledSoftware installedSoftware1;
    }
}
