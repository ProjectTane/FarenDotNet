namespace FarenDotNet.NewGame.UI
{
	partial class SelectScenarioPage
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
			this.splitOuter = new System.Windows.Forms.SplitContainer();
			this.lbx_SelectGame = new System.Windows.Forms.ListBox();
			this.splitInner = new System.Windows.Forms.SplitContainer();
			this.lbx_SelectScenario = new System.Windows.Forms.ListBox();
			this.tbx_Explanation = new System.Windows.Forms.TextBox();
			this.splitOuter.Panel1.SuspendLayout();
			this.splitOuter.Panel2.SuspendLayout();
			this.splitOuter.SuspendLayout();
			this.splitInner.Panel1.SuspendLayout();
			this.splitInner.Panel2.SuspendLayout();
			this.splitInner.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitOuter
			// 
			this.splitOuter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitOuter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitOuter.Location = new System.Drawing.Point(0, 0);
			this.splitOuter.Name = "splitOuter";
			// 
			// splitOuter.Panel1
			// 
			this.splitOuter.Panel1.Controls.Add(this.lbx_SelectGame);
			// 
			// splitOuter.Panel2
			// 
			this.splitOuter.Panel2.Controls.Add(this.splitInner);
			this.splitOuter.Size = new System.Drawing.Size(300, 300);
			this.splitOuter.SplitterDistance = 94;
			this.splitOuter.TabIndex = 0;
			// 
			// lbx_SelectGame
			// 
			this.lbx_SelectGame.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbx_SelectGame.FormattingEnabled = true;
			this.lbx_SelectGame.ItemHeight = 12;
			this.lbx_SelectGame.Location = new System.Drawing.Point(0, 0);
			this.lbx_SelectGame.Name = "lbx_SelectGame";
			this.lbx_SelectGame.Size = new System.Drawing.Size(94, 292);
			this.lbx_SelectGame.TabIndex = 2;
			this.lbx_SelectGame.SelectedIndexChanged += new System.EventHandler(this.lbx_SelectGame_SelectedIndexChanged);
			// 
			// splitInner
			// 
			this.splitInner.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitInner.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitInner.Location = new System.Drawing.Point(0, 0);
			this.splitInner.Margin = new System.Windows.Forms.Padding(0);
			this.splitInner.Name = "splitInner";
			this.splitInner.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitInner.Panel1
			// 
			this.splitInner.Panel1.Controls.Add(this.lbx_SelectScenario);
			// 
			// splitInner.Panel2
			// 
			this.splitInner.Panel2.Controls.Add(this.tbx_Explanation);
			this.splitInner.Size = new System.Drawing.Size(202, 300);
			this.splitInner.SplitterDistance = 88;
			this.splitInner.TabIndex = 0;
			// 
			// lbx_SelectScenario
			// 
			this.lbx_SelectScenario.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbx_SelectScenario.FormattingEnabled = true;
			this.lbx_SelectScenario.ItemHeight = 12;
			this.lbx_SelectScenario.Location = new System.Drawing.Point(0, 0);
			this.lbx_SelectScenario.Name = "lbx_SelectScenario";
			this.lbx_SelectScenario.Size = new System.Drawing.Size(202, 88);
			this.lbx_SelectScenario.TabIndex = 0;
			this.lbx_SelectScenario.SelectedIndexChanged += new System.EventHandler(this.lbx_SelectScenario_SelectedIndexChanged);
			// 
			// tbx_Explanation
			// 
			this.tbx_Explanation.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbx_Explanation.Location = new System.Drawing.Point(0, 0);
			this.tbx_Explanation.Multiline = true;
			this.tbx_Explanation.Name = "tbx_Explanation";
			this.tbx_Explanation.ReadOnly = true;
			this.tbx_Explanation.Size = new System.Drawing.Size(202, 208);
			this.tbx_Explanation.TabIndex = 0;
			// 
			// SelectScenarioPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.Controls.Add(this.splitOuter);
			this.Name = "SelectScenarioPage";
			this.Size = new System.Drawing.Size(300, 300);
			this.Load += new System.EventHandler(this.SelectScenarioPage_Load);
			this.splitOuter.Panel1.ResumeLayout(false);
			this.splitOuter.Panel2.ResumeLayout(false);
			this.splitOuter.ResumeLayout(false);
			this.splitInner.Panel1.ResumeLayout(false);
			this.splitInner.Panel2.ResumeLayout(false);
			this.splitInner.Panel2.PerformLayout();
			this.splitInner.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitOuter;
		private System.Windows.Forms.SplitContainer splitInner;
		private System.Windows.Forms.ListBox lbx_SelectGame;
		private System.Windows.Forms.ListBox lbx_SelectScenario;
		private System.Windows.Forms.TextBox tbx_Explanation;
	}
}
