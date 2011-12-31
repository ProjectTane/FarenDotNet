namespace FarenDotNet.War.UI
{
	partial class MapWindow
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
			this._scrollablePanel = new Paraiba.Windows.Forms.ScrollablePanel();
			this.SuspendLayout();
			// 
			// _scrollablePanel
			// 
			this._scrollablePanel.AutoSize = true;
			this._scrollablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._scrollablePanel.DragMoveButtons = System.Windows.Forms.MouseButtons.Left;
			this._scrollablePanel.HorizontalLocationStyle = Paraiba.Windows.Forms.HorizontalLocationStyle.Center;
			this._scrollablePanel.Location = new System.Drawing.Point(0, 0);
			this._scrollablePanel.Name = "_scrollablePanel";
			this._scrollablePanel.Panel = null;
			this._scrollablePanel.Size = new System.Drawing.Size(784, 563);
			this._scrollablePanel.TabIndex = 0;
			this._scrollablePanel.VerticalLocationStyle = Paraiba.Windows.Forms.VerticalLocationStyle.Center;
			// 
			// MapWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 563);
			this.Controls.Add(this._scrollablePanel);
			this.Name = "MapWindow";
			this.TabText = "MapWindow";
			this.Text = "MapWindow";
			this.Load += new System.EventHandler(this.MapWindow_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Paraiba.Windows.Forms.ScrollablePanel _scrollablePanel;

	}
}