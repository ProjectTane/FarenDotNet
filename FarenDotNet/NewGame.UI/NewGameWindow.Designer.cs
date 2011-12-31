namespace FarenDotNet.NewGame.UI
{
	partial class NewGameWindow
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
			this.pageBinder = new FarenDotNet.NewGame.UI.PageBinder();
			this.SuspendLayout();
			// 
			// pageBinder
			// 
			this.pageBinder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pageBinder.Location = new System.Drawing.Point(0, 0);
			this.pageBinder.Name = "pageBinder";
			this.pageBinder.Pages = null;
			this.pageBinder.Size = new System.Drawing.Size(400, 274);
			this.pageBinder.TabIndex = 0;
			this.pageBinder.FinishClick += new System.EventHandler(this.pageBinder_FinishClick);
			this.pageBinder.CancelClick += new System.EventHandler(this.pageBinder_CancelClick);
			// 
			// NewGameWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(400, 274);
			this.Controls.Add(this.pageBinder);
			this.Name = "NewGameWindow";
			this.Text = "NewGameWindow";
			this.ResumeLayout(false);

		}

		#endregion

		private PageBinder pageBinder;
	}
}