namespace FarenDotNet.War.UI
{
	partial class CommandWindow
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
			if (disposing && (components != null)) {
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
			this.components = new System.ComponentModel.Container();
			this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// flowLayoutPanel
			// 
			this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel.Name = "flowLayoutPanel";
			this.flowLayoutPanel.Size = new System.Drawing.Size(376, 152);
			this.flowLayoutPanel.TabIndex = 1;
			// 
			// CommandWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(376, 152);
			this.Controls.Add(this.flowLayoutPanel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CommandWindow";
			this.TabText = "CommandWindow";
			this.Text = "CommandWindow";
			this.Load += new System.EventHandler(this.CommandWindow_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
		private System.Windows.Forms.ToolTip toolTip;
	}
}