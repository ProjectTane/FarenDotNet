namespace FarenDotNet.NewGame.UI
{
	partial class SelectMastersPage
	{
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナで生成されたコード

		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.pnl_Information = new System.Windows.Forms.Panel();
			this.lbl_Explanation = new System.Windows.Forms.Label();
			this.lbl_Difficulty = new System.Windows.Forms.Label();
			this.lbl_MasterName = new System.Windows.Forms.Label();
			this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.pnl_Information.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnl_Information
			// 
			this.pnl_Information.Controls.Add(this.lbl_Explanation);
			this.pnl_Information.Controls.Add(this.lbl_Difficulty);
			this.pnl_Information.Controls.Add(this.lbl_MasterName);
			this.pnl_Information.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnl_Information.Location = new System.Drawing.Point(0, 204);
			this.pnl_Information.Name = "pnl_Information";
			this.pnl_Information.Size = new System.Drawing.Size(355, 74);
			this.pnl_Information.TabIndex = 0;
			// 
			// lbl_Explanation
			// 
			this.lbl_Explanation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lbl_Explanation.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lbl_Explanation.Location = new System.Drawing.Point(3, 23);
			this.lbl_Explanation.Name = "lbl_Explanation";
			this.lbl_Explanation.Size = new System.Drawing.Size(349, 51);
			this.lbl_Explanation.TabIndex = 8;
			this.lbl_Explanation.Text = "説明１\r\n説明２\r\n説明３";
			// 
			// lbl_Difficulty
			// 
			this.lbl_Difficulty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lbl_Difficulty.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lbl_Difficulty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
			this.lbl_Difficulty.Location = new System.Drawing.Point(261, 0);
			this.lbl_Difficulty.Name = "lbl_Difficulty";
			this.lbl_Difficulty.Size = new System.Drawing.Size(91, 23);
			this.lbl_Difficulty.TabIndex = 7;
			this.lbl_Difficulty.Text = "★★★★★";
			this.lbl_Difficulty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl_MasterName
			// 
			this.lbl_MasterName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lbl_MasterName.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lbl_MasterName.ForeColor = System.Drawing.Color.Maroon;
			this.lbl_MasterName.Location = new System.Drawing.Point(3, 0);
			this.lbl_MasterName.Name = "lbl_MasterName";
			this.lbl_MasterName.Size = new System.Drawing.Size(252, 23);
			this.lbl_MasterName.TabIndex = 3;
			this.lbl_MasterName.Text = "マスター名";
			// 
			// flowLayoutPanel
			// 
			this.flowLayoutPanel.AutoScroll = true;
			this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel.Name = "flowLayoutPanel";
			this.flowLayoutPanel.Size = new System.Drawing.Size(355, 204);
			this.flowLayoutPanel.TabIndex = 1;
			// 
			// SelectMastersPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.Controls.Add(this.flowLayoutPanel);
			this.Controls.Add(this.pnl_Information);
			this.Name = "SelectMastersPage";
			this.Size = new System.Drawing.Size(355, 278);
			this.pnl_Information.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnl_Information;
		private System.Windows.Forms.Label lbl_MasterName;
		private System.Windows.Forms.Label lbl_Difficulty;
		private System.Windows.Forms.Label lbl_Explanation;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
	}
}
