namespace custos.Forms
{
	partial class PasswordForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		public void InitializeComponent()
		{
			panel1 = new Panel();
			label3 = new Label();
			button1 = new Button();
			textBox1 = new TextBox();
			label2 = new Label();
			panel2 = new Panel();
			pictureBox1 = new PictureBox();
			label1 = new Label();
			panel1.SuspendLayout();
			panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			// 
			// panel1
			// 
			panel1.BackColor = SystemColors.ButtonHighlight;
			panel1.BorderStyle = BorderStyle.FixedSingle;
			panel1.Controls.Add(label3);
			panel1.Controls.Add(button1);
			panel1.Controls.Add(textBox1);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(panel2);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new Size(416, 228);
			panel1.TabIndex = 3;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.ForeColor = Color.Red;
			label3.Location = new Point(152, 153);
			label3.Name = "label3";
			label3.Size = new Size(0, 20);
			label3.TabIndex = 4;
			// 
			// button1
			// 
			button1.BackColor = Color.Green;
			button1.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
			button1.ForeColor = SystemColors.ControlLightLight;
			button1.Location = new Point(298, 181);
			button1.Name = "button1";
			button1.Size = new Size(94, 34);
			button1.TabIndex = 3;
			button1.Text = "OK";
			button1.UseVisualStyleBackColor = false;
			button1.Click += button1_Click;
			// 
			// textBox1
			// 
			textBox1.BorderStyle = BorderStyle.FixedSingle;
			textBox1.Location = new Point(73, 113);
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(277, 27);
			textBox1.TabIndex = 2;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
			label2.Location = new Point(99, 73);
			label2.Name = "label2";
			label2.Size = new Size(227, 25);
			label2.TabIndex = 1;
			label2.Text = "Enter Password To Uninstall";
			// 
			// panel2
			// 
			panel2.BackColor = SystemColors.GrayText;
			panel2.Controls.Add(pictureBox1);
			panel2.Controls.Add(label1);
			panel2.Dock = DockStyle.Top;
			panel2.Location = new Point(0, 0);
			panel2.Name = "panel2";
			panel2.Size = new Size(414, 45);
			panel2.TabIndex = 0;
			// 
			// pictureBox1
			// 
			pictureBox1.Image = Properties.Resources.close__1_;
			pictureBox1.Location = new Point(373, 5);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(35, 34);
			pictureBox1.TabIndex = 1;
			pictureBox1.TabStop = false;
			pictureBox1.Click += pictureBox1_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label1.ForeColor = SystemColors.ButtonHighlight;
			label1.Location = new Point(11, 9);
			label1.Name = "label1";
			label1.Size = new Size(177, 28);
			label1.TabIndex = 0;
			label1.Text = "Password Prompt";
			// 
			// PasswordForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(416, 228);
			Controls.Add(panel1);
			FormBorderStyle = FormBorderStyle.None;
			Name = "PasswordForm";
			Text = "PasswordForm";
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panel2.ResumeLayout(false);
			panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private Panel panel1;
		private Panel panel2;
		private Label label1;
		public Button button1;
		private TextBox textBox1;
		private Label label2;
		private PictureBox pictureBox1;
		private Label label3;
	}
}