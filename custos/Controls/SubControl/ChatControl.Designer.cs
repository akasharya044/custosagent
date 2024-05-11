namespace custos.Controls.SubControl
{
	partial class ChatControl
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
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatControl));
            QuestionDetailText = new TextBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            panel2 = new Panel();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            pictureBox4 = new PictureBox();
            textBox1 = new TextBox();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // QuestionDetailText
            // 
            QuestionDetailText.BorderStyle = BorderStyle.FixedSingle;
            QuestionDetailText.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            QuestionDetailText.Location = new Point(41, 96);
            QuestionDetailText.MaxLength = 255;
            QuestionDetailText.MinimumSize = new Size(4, 40);
            QuestionDetailText.Name = "QuestionDetailText";
            QuestionDetailText.Size = new Size(676, 40);
            QuestionDetailText.TabIndex = 43;
            QuestionDetailText.Text = "Type your query here...";
            QuestionDetailText.TextChanged += QuestionDetailText_TextChanged;
            QuestionDetailText.KeyDown += QuestionDetailText_KeyDown;
            QuestionDetailText.KeyUp += QuestionDetailText_KeyUp;
            QuestionDetailText.Leave += QuestionDetailText_Leave;
            QuestionDetailText.MouseDown += QuestionDetails_MouseDown;
            QuestionDetailText.MouseEnter += QuestionDetails_MouseEnter;
            QuestionDetailText.MouseLeave += QuestionDetails_MouseLeave;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.GrayText;
            panel2.Controls.Add(pictureBox2);
            panel2.Controls.Add(pictureBox3);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(pictureBox1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(825, 45);
            panel2.TabIndex = 54;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Image = Properties.Resources.close__1_;
            pictureBox2.Location = new Point(782, 8);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(38, 32);
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Transparent;
            pictureBox3.BackgroundImageLayout = ImageLayout.None;
            pictureBox3.Image = Properties.Resources.minimize;
            pictureBox3.Location = new Point(731, 9);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(33, 29);
            pictureBox3.TabIndex = 3;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(45, 7);
            label1.Name = "label1";
            label1.Size = new Size(116, 28);
            label1.TabIndex = 0;
            label1.Text = "CustOs Bot";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.bot;
            pictureBox1.Location = new Point(11, 7);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(31, 33);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ButtonHighlight;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(pictureBox4);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(825, 633);
            panel1.TabIndex = 5;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.human_resources;
            pictureBox4.Location = new Point(722, 95);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(41, 40);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 2;
            pictureBox4.TabStop = false;
            pictureBox4.Click += pictureBox4_Click;
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Location = new Point(40, 155);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(719, 420);
            textBox1.TabIndex = 3;
            // 
            // ChatControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(825, 633);
            Controls.Add(panel2);
            Controls.Add(QuestionDetailText);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Location = new Point(650, 100);
            Name = "ChatControl";
            StartPosition = FormStartPosition.CenterScreen;
            Load += ChatForm_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.TextBox QuestionDetailText;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private Panel panel2;
		private Label label1;
		private PictureBox pictureBox1;
		private PictureBox pictureBox2;
		private PictureBox pictureBox3;
		private Panel panel1;
        private PictureBox pictureBox4;
        private TextBox textBox1;

        //#endregion
    }
}