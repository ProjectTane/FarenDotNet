namespace FarenDotNet.War.UI
{
	partial class UnitInfoWindow
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
			this.SuspendLayout();
			// 
			// UnitInfoWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Navy;
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UnitInfoWindow";
			this.ShowInTaskbar = false;
			this.Text = "UnitInfoWindow";
			this.TopMost = true;
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.UnitInfoWindow_Paint);
			this.ResumeLayout(false);

		}

		#endregion
	}
}