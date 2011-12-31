namespace FarenDotNet.War
{
	partial class MainWindow
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
			this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.labelTurn = new System.Windows.Forms.ToolStripLabel();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// dockPanel
			// 
			this.dockPanel.ActiveAutoHideContent = null;
			this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingSdi;
			this.dockPanel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(128)));
			this.dockPanel.Location = new System.Drawing.Point(0, 25);
			this.dockPanel.Name = "dockPanel";
			this.dockPanel.Size = new System.Drawing.Size(984, 716);
			this.dockPanel.TabIndex = 0;
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelTurn});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(984, 25);
			this.toolStrip.TabIndex = 1;
			this.toolStrip.Text = "toolStrip";
			// 
			// labelTurn
			// 
			this.labelTurn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.labelTurn.Name = "labelTurn";
			this.labelTurn.Size = new System.Drawing.Size(63, 22);
			this.labelTurn.Text = "0ターン目";
			// 
			// statusStrip
			// 
			this.statusStrip.Location = new System.Drawing.Point(0, 741);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(984, 22);
			this.statusStrip.TabIndex = 2;
			this.statusStrip.Text = "statusStrip";
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(984, 763);
			this.Controls.Add(this.dockPanel);
			this.Controls.Add(this.toolStrip);
			this.Controls.Add(this.statusStrip);
			this.IsMdiContainer = true;
			this.Name = "MainWindow";
			this.Text = "MainWindow";
			this.Load += new System.EventHandler(this.MainWindow_Load);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
#if !DOCUMENT
		private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
#endif
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripLabel labelTurn;

	}
}