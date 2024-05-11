namespace custos.Controls;

partial class MachineControl
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
		button1 = new Button();
		panel1.SuspendLayout();
		SuspendLayout();
		// 
		// panel1
		// 
		panel1.BackColor = SystemColors.HighlightText;
		panel1.Controls.Add(button1);
		panel1.Dock = DockStyle.Fill;
		panel1.Location = new Point(0, 0);
		panel1.Name = "panel1";
		panel1.Size = new Size(460, 377);
		panel1.TabIndex = 0;
		// 
		// button1
		// 
		button1.Location = new Point(148, 119);
		button1.Name = "button1";
		button1.Size = new Size(94, 29);
		button1.TabIndex = 0;
		button1.Text = "button1";
		button1.UseVisualStyleBackColor = true;
		button1.Click += button1_Click;
		// 
		// MachineControl
		// 
		AutoScaleDimensions = new SizeF(8F, 20F);
		AutoScaleMode = AutoScaleMode.Font;
		Controls.Add(panel1);
		Name = "MachineControl";
		Size = new Size(460, 377);
		panel1.ResumeLayout(false);
		ResumeLayout(false);
	}

	#endregion

	private Panel panel1;
	private Button button1;
}
