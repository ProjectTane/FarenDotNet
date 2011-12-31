namespace FarenDotNet.Reign.UI
{
	partial class PrepareWarWindow
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
			this.btn_Cancel = new System.Windows.Forms.Button();
			this.btn_Finish = new System.Windows.Forms.Button();
			this._splitContainer = new System.Windows.Forms.SplitContainer();
			this._tabControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.flp_Battler = new System.Windows.Forms.FlowLayoutPanel();
			this.btn_AllBattler = new System.Windows.Forms.Button();
			this._splitContainer.Panel1.SuspendLayout();
			this._splitContainer.Panel2.SuspendLayout();
			this._splitContainer.SuspendLayout();
			this._tabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btn_Cancel
			// 
			this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Cancel.BackColor = System.Drawing.Color.Navy;
			this.btn_Cancel.Location = new System.Drawing.Point(349, 231);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
			this.btn_Cancel.TabIndex = 0;
			this.btn_Cancel.Text = "キャンセル(&C)";
			this.btn_Cancel.UseVisualStyleBackColor = false;
			this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
			// 
			// btn_Finish
			// 
			this.btn_Finish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Finish.BackColor = System.Drawing.Color.Navy;
			this.btn_Finish.Location = new System.Drawing.Point(268, 231);
			this.btn_Finish.Name = "btn_Finish";
			this.btn_Finish.Size = new System.Drawing.Size(75, 23);
			this.btn_Finish.TabIndex = 1;
			this.btn_Finish.Text = "編成終了(&F)";
			this.btn_Finish.UseVisualStyleBackColor = false;
			this.btn_Finish.Click += new System.EventHandler(this.btn_Finish_Click);
			// 
			// _splitContainer
			// 
			this._splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._splitContainer.Location = new System.Drawing.Point(12, 12);
			this._splitContainer.Name = "_splitContainer";
			// 
			// _splitContainer.Panel1
			// 
			this._splitContainer.Panel1.Controls.Add(this._tabControl);
			// 
			// _splitContainer.Panel2
			// 
			this._splitContainer.Panel2.Controls.Add(this.flp_Battler);
			this._splitContainer.Size = new System.Drawing.Size(412, 213);
			this._splitContainer.SplitterDistance = 208;
			this._splitContainer.TabIndex = 2;
			// 
			// _tabControl
			// 
			this._tabControl.Controls.Add(this.tabPage1);
			this._tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this._tabControl.Location = new System.Drawing.Point(0, 0);
			this._tabControl.Margin = new System.Windows.Forms.Padding(0);
			this._tabControl.Multiline = true;
			this._tabControl.Name = "_tabControl";
			this._tabControl.Padding = new System.Drawing.Point(0, 0);
			this._tabControl.SelectedIndex = 0;
			this._tabControl.Size = new System.Drawing.Size(208, 213);
			this._tabControl.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.Color.Navy;
			this.tabPage1.Controls.Add(this.flowLayoutPanel1);
			this.tabPage1.Location = new System.Drawing.Point(4, 21);
			this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(200, 188);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.BackColor = System.Drawing.Color.Navy;
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 188);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// flp_Battler
			// 
			this.flp_Battler.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flp_Battler.Location = new System.Drawing.Point(0, 0);
			this.flp_Battler.Name = "flp_Battler";
			this.flp_Battler.Size = new System.Drawing.Size(200, 213);
			this.flp_Battler.TabIndex = 0;
			// 
			// btn_AllBattler
			// 
			this.btn_AllBattler.BackColor = System.Drawing.Color.Navy;
			this.btn_AllBattler.Location = new System.Drawing.Point(12, 231);
			this.btn_AllBattler.Name = "btn_AllBattler";
			this.btn_AllBattler.Size = new System.Drawing.Size(75, 23);
			this.btn_AllBattler.TabIndex = 3;
			this.btn_AllBattler.Text = "全て右へ";
			this.btn_AllBattler.UseVisualStyleBackColor = false;
			this.btn_AllBattler.Click += new System.EventHandler(this.btn_AllBattler_Click);
			// 
			// PrepareWarWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Navy;
			this.ClientSize = new System.Drawing.Size(436, 266);
			this.Controls.Add(this.btn_AllBattler);
			this.Controls.Add(this._splitContainer);
			this.Controls.Add(this.btn_Finish);
			this.Controls.Add(this.btn_Cancel);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "PrepareWarWindow";
			this.Text = "PrepareWarWindow";
			this.Load += new System.EventHandler(this.PrepareWarWindow_Load);
			this._splitContainer.Panel1.ResumeLayout(false);
			this._splitContainer.Panel2.ResumeLayout(false);
			this._splitContainer.ResumeLayout(false);
			this._tabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btn_Cancel;
		private System.Windows.Forms.Button btn_Finish;
		private System.Windows.Forms.SplitContainer _splitContainer;
		private System.Windows.Forms.FlowLayoutPanel flp_Battler;
		private System.Windows.Forms.TabControl _tabControl;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button btn_AllBattler;
	}
}