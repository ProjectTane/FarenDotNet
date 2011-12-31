namespace FarenDotNet.Reign.UI
{
	partial class GameOver
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
			this._label = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// _label
			// 
			this._label.Dock = System.Windows.Forms.DockStyle.Fill;
			this._label.Font = new System.Drawing.Font("Century", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this._label.Location = new System.Drawing.Point(0, 0);
			this._label.Name = "_label";
			this._label.Size = new System.Drawing.Size(320, 180);
			this._label.TabIndex = 0;
			this._label.Text = "Game Over";
			this._label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this._label.Click += new System.EventHandler(this._label_Click);
			// 
			// GameOver
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(320, 180);
			this.Controls.Add(this._label);
			this.ForeColor = System.Drawing.Color.Red;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "GameOver";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "GameOver";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label _label;
	}
}