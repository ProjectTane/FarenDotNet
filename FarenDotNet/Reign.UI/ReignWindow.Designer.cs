namespace FarenDotNet.Reign.UI
{
	partial class ReignWindow
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
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this._scrollablePanel = new Paraiba.Windows.Forms.ScrollablePanel();
			this._timer = new System.Windows.Forms.Timer(this.components);
			this._topPanel = new FarenDotNet.Reign.UI.TopPanel();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(462, 24);
			this.menuStrip.TabIndex = 1;
			this.menuStrip.Text = "menuStrip1";
			// 
			// _scrollablePanel
			// 
			this._scrollablePanel.AutoSize = true;
			this._scrollablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._scrollablePanel.DragMoveButtons = System.Windows.Forms.MouseButtons.Left;
			this._scrollablePanel.HorizontalLocationStyle = Paraiba.Windows.Forms.HorizontalLocationStyle.Left;
			this._scrollablePanel.Location = new System.Drawing.Point(0, 57);
			this._scrollablePanel.Name = "_scrollablePanel";
			this._scrollablePanel.Panel = null;
			this._scrollablePanel.Size = new System.Drawing.Size(462, 216);
			this._scrollablePanel.TabIndex = 3;
			this._scrollablePanel.VerticalLocationStyle = Paraiba.Windows.Forms.VerticalLocationStyle.Top;
			// 
			// _topPanel
			// 
			this._topPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this._topPanel.Location = new System.Drawing.Point(0, 24);
			this._topPanel.Name = "_topPanel";
			this._topPanel.Padding = new System.Windows.Forms.Padding(1);
			this._topPanel.Size = new System.Drawing.Size(462, 33);
			this._topPanel.TabIndex = 2;
			// 
			// ReignWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
			this.CausesValidation = false;
			this.ClientSize = new System.Drawing.Size(462, 273);
			this.Controls.Add(this._scrollablePanel);
			this.Controls.Add(this._topPanel);
			this.Controls.Add(this.menuStrip);
			this.MainMenuStrip = this.menuStrip;
			this.Name = "ReignWindow";
			this.TabText = "MapWindow";
			this.Text = "ReignWindow";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip;
		private TopPanel _topPanel;
		private Paraiba.Windows.Forms.ScrollablePanel _scrollablePanel;
		private System.Windows.Forms.Timer _timer;

	}
}