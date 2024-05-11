namespace custos.Controls;

    partial class AntivirusControl
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
        panel2 = new Panel();
        AVList = new RichTextBox();
        panel2.SuspendLayout();
        SuspendLayout();
        // 
        // panel2
        // 
        panel2.Controls.Add(AVList);
        panel2.Dock = DockStyle.Fill;
        panel2.Location = new Point(0, 0);
        panel2.Name = "panel2";
        panel2.Size = new Size(589, 376);
        panel2.TabIndex = 1;
        // 
        // AVList
        // 
        AVList.BackColor = SystemColors.ButtonHighlight;
        AVList.BorderStyle = BorderStyle.FixedSingle;
        AVList.Dock = DockStyle.Fill;
        AVList.Location = new Point(0, 0);
        AVList.Name = "AVList";
        AVList.ReadOnly = true;
        AVList.Size = new Size(589, 376);
        AVList.TabIndex = 0;
        AVList.Text = "";
        AVList.TextChanged += AVList_TextChanged;
        // 
        // AntivirusControl
        // 
        BackColor = SystemColors.Control;
        Controls.Add(panel2);
        Name = "AntivirusControl";
        Size = new Size(589, 376);
        Load += Antivirus_Load;
        panel2.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion
    private Panel panel2;
	private RichTextBox AVList;
}
