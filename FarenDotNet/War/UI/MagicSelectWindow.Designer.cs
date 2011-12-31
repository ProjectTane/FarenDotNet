namespace FarenDotNet.War.UI
{
	partial class MagicSelectWindow
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
			this.labelLine3 = new System.Windows.Forms.Label();
			this.labelLine2 = new System.Windows.Forms.Label();
			this.labelLine1 = new System.Windows.Forms.Label();
			this.flowButtonPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.SuspendLayout();
			// 
			// labelLine3
			// 
			this.labelLine3.AutoSize = true;
			this.labelLine3.Font = new System.Drawing.Font("MS UI Gothic", 12F);
			this.labelLine3.Location = new System.Drawing.Point(12, 46);
			this.labelLine3.Name = "labelLine3";
			this.labelLine3.Size = new System.Drawing.Size(193, 16);
			this.labelLine3.TabIndex = 8;
			this.labelLine3.Text = "射程： 目の前 / 範囲： 周囲";
			// 
			// labelLine2
			// 
			this.labelLine2.AutoSize = true;
			this.labelLine2.Font = new System.Drawing.Font("MS UI Gothic", 12F);
			this.labelLine2.Location = new System.Drawing.Point(12, 25);
			this.labelLine2.Name = "labelLine2";
			this.labelLine2.Size = new System.Drawing.Size(84, 16);
			this.labelLine2.TabIndex = 7;
			this.labelLine2.Text = "相手は死ぬ";
			// 
			// labelLine1
			// 
			this.labelLine1.AutoSize = true;
			this.labelLine1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
			this.labelLine1.Location = new System.Drawing.Point(12, 4);
			this.labelLine1.Name = "labelLine1";
			this.labelLine1.Size = new System.Drawing.Size(256, 16);
			this.labelLine1.TabIndex = 6;
			this.labelLine1.Text = "エターナルフォースブリザード 消費MP ∞";
			// 
			// flowButtonPanel
			// 
			this.flowButtonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.flowButtonPanel.Location = new System.Drawing.Point(-1, 65);
			this.flowButtonPanel.Name = "flowButtonPanel";
			this.flowButtonPanel.Size = new System.Drawing.Size(272, 245);
			this.flowButtonPanel.TabIndex = 5;
			this.flowButtonPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.flowButtonPanel_MouseClick);
			// 
			// MagicSelectWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(271, 315);
			this.Controls.Add(this.labelLine3);
			this.Controls.Add(this.labelLine2);
			this.Controls.Add(this.labelLine1);
			this.Controls.Add(this.flowButtonPanel);
			this.Name = "MagicSelectWindow";
			this.TabText = "MagicSelectWindow";
			this.Text = "MagicSelectWindow";
			this.Load += new System.EventHandler(this.MagicSelectWindow_Load);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MagicSelectWindow_MouseClick);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MagicSelectWindow_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelLine3;
		private System.Windows.Forms.Label labelLine2;
		private System.Windows.Forms.Label labelLine1;
		private System.Windows.Forms.FlowLayoutPanel flowButtonPanel;
	}
}