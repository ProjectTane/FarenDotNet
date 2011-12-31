namespace FarenDotNet.War.UI
{
	partial class MiniUnitInfoWindow
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
			// MiniUnitInfoWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Navy;
			this.ClientSize = new System.Drawing.Size(196, 75);
			this.ControlBox = false;
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "MiniUnitInfoWindow";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "MiniUnitInfoWindow";
			this.Deactivate += new System.EventHandler(this.MiniUnitInfoWindow_Deactivate);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MiniUnitInfoWindow_MouseUp);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.UnitInfoWindow_Paint);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MiniUnitInfoWindow_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MiniUnitInfoWindow_MouseMove);
			this.ResumeLayout(false);

		}

		#endregion
	}
}