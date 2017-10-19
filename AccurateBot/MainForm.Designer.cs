namespace AccurateBot
{
	partial class MainForm
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
			this.frameBox = new System.Windows.Forms.PictureBox();
			this.outlineBox = new System.Windows.Forms.PictureBox();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.frameBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.outlineBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			this.SuspendLayout();
			// 
			// frameBox
			// 
			this.frameBox.Location = new System.Drawing.Point(12, 12);
			this.frameBox.Name = "frameBox";
			this.frameBox.Size = new System.Drawing.Size(300, 300);
			this.frameBox.TabIndex = 0;
			this.frameBox.TabStop = false;
			this.frameBox.Click += new System.EventHandler(this.FrameBox_Click);
			this.frameBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FrameBox_MouseClick);
			// 
			// outlineBox
			// 
			this.outlineBox.Location = new System.Drawing.Point(318, 12);
			this.outlineBox.Name = "outlineBox";
			this.outlineBox.Size = new System.Drawing.Size(300, 300);
			this.outlineBox.TabIndex = 1;
			this.outlineBox.TabStop = false;
			// 
			// trackBar1
			// 
			this.trackBar1.Location = new System.Drawing.Point(624, 12);
			this.trackBar1.Maximum = 255;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(213, 45);
			this.trackBar1.TabIndex = 2;
			this.trackBar1.Scroll += new System.EventHandler(this.TrackBar1_Scroll);
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Items.AddRange(new object[] {
            "Min Diff Above",
            "Max Diff Above",
            "Min Diff Below",
            "Max Diff Blow",
            "Max Diff Sides",
            "Min Red"});
			this.listBox1.Location = new System.Drawing.Point(624, 53);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(120, 95);
			this.listBox1.TabIndex = 3;
			this.listBox1.SelectedIndexChanged += new System.EventHandler(this.ListBox1_SelectedIndexChanged);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(624, 154);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(100, 20);
			this.textBox1.TabIndex = 4;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1084, 319);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.trackBar1);
			this.Controls.Add(this.outlineBox);
			this.Controls.Add(this.frameBox);
			this.Name = "MainForm";
			this.Text = "Gmail";
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.frameBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.outlineBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox frameBox;
		private System.Windows.Forms.PictureBox outlineBox;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.TextBox textBox1;
	}
}

