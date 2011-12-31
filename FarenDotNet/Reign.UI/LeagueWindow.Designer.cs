namespace FarenDotNet.Reign.UI
{
	partial class LeagueWindow
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
			this._provInfoControl = new FarenDotNet.Reign.UI.ProvinceInfoControl();
			this.SuspendLayout();
			// 
			// _provInfoControl
			// 
			this._provInfoControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this._provInfoControl.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this._provInfoControl.Location = new System.Drawing.Point(0, 0);
			this._provInfoControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this._provInfoControl.Name = "_provInfoControl";
			this._provInfoControl.Size = new System.Drawing.Size(522, 210);
			this._provInfoControl.TabIndex = 0;
			// 
			// LeagueWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(522, 210);
			this.Controls.Add(this._provInfoControl);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LeagueWindow";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "LeagueWindow";
			this.ResumeLayout(false);

		}

		#endregion

		private ProvinceInfoControl _provInfoControl;
	}
}