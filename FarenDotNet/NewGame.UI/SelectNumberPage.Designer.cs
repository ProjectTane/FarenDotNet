namespace FarenDotNet.NewGame.UI
{
	partial class SelectNumberPage
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
			this.gbx_nPlayer = new System.Windows.Forms.GroupBox();
			this.player4 = new System.Windows.Forms.RadioButton();
			this.player3 = new System.Windows.Forms.RadioButton();
			this.player2 = new System.Windows.Forms.RadioButton();
			this.player1 = new System.Windows.Forms.RadioButton();
			this.player0 = new System.Windows.Forms.RadioButton();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.gbx_Level = new System.Windows.Forms.GroupBox();
			this.level5 = new System.Windows.Forms.RadioButton();
			this.level4 = new System.Windows.Forms.RadioButton();
			this.level3 = new System.Windows.Forms.RadioButton();
			this.level2 = new System.Windows.Forms.RadioButton();
			this.level1 = new System.Windows.Forms.RadioButton();
			this.gbx_nPlayer.SuspendLayout();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.gbx_Level.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbx_nPlayer
			// 
			this.gbx_nPlayer.Controls.Add(this.player4);
			this.gbx_nPlayer.Controls.Add(this.player3);
			this.gbx_nPlayer.Controls.Add(this.player2);
			this.gbx_nPlayer.Controls.Add(this.player1);
			this.gbx_nPlayer.Controls.Add(this.player0);
			this.gbx_nPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbx_nPlayer.Location = new System.Drawing.Point(0, 0);
			this.gbx_nPlayer.Name = "gbx_nPlayer";
			this.gbx_nPlayer.Size = new System.Drawing.Size(203, 298);
			this.gbx_nPlayer.TabIndex = 0;
			this.gbx_nPlayer.TabStop = false;
			this.gbx_nPlayer.Text = "プレイヤーの人数(&P)";
			// 
			// player4
			// 
			this.player4.AutoSize = true;
			this.player4.Location = new System.Drawing.Point(19, 115);
			this.player4.Name = "player4";
			this.player4.Size = new System.Drawing.Size(43, 16);
			this.player4.TabIndex = 4;
			this.player4.TabStop = true;
			this.player4.Text = "４人";
			this.player4.UseVisualStyleBackColor = true;
			// 
			// player3
			// 
			this.player3.AutoSize = true;
			this.player3.Location = new System.Drawing.Point(19, 93);
			this.player3.Name = "player3";
			this.player3.Size = new System.Drawing.Size(43, 16);
			this.player3.TabIndex = 3;
			this.player3.TabStop = true;
			this.player3.Text = "３人";
			this.player3.UseVisualStyleBackColor = true;
			// 
			// player2
			// 
			this.player2.AutoSize = true;
			this.player2.Location = new System.Drawing.Point(19, 71);
			this.player2.Name = "player2";
			this.player2.Size = new System.Drawing.Size(43, 16);
			this.player2.TabIndex = 2;
			this.player2.TabStop = true;
			this.player2.Text = "２人";
			this.player2.UseVisualStyleBackColor = true;
			// 
			// player1
			// 
			this.player1.AutoSize = true;
			this.player1.Location = new System.Drawing.Point(19, 49);
			this.player1.Name = "player1";
			this.player1.Size = new System.Drawing.Size(43, 16);
			this.player1.TabIndex = 1;
			this.player1.TabStop = true;
			this.player1.Text = "１人";
			this.player1.UseVisualStyleBackColor = true;
			// 
			// player0
			// 
			this.player0.AutoSize = true;
			this.player0.Location = new System.Drawing.Point(19, 27);
			this.player0.Name = "player0";
			this.player0.Size = new System.Drawing.Size(47, 16);
			this.player0.TabIndex = 0;
			this.player0.TabStop = true;
			this.player0.Text = "観戦";
			this.player0.UseVisualStyleBackColor = true;
			this.player0.CheckedChanged += new System.EventHandler(this.nPlayerChanged);
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new System.Drawing.Point(0, 0);
			this.splitContainer.Margin = new System.Windows.Forms.Padding(10);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.gbx_nPlayer);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.gbx_Level);
			this.splitContainer.Size = new System.Drawing.Size(434, 298);
			this.splitContainer.SplitterDistance = 203;
			this.splitContainer.TabIndex = 1;
			// 
			// gbx_Level
			// 
			this.gbx_Level.Controls.Add(this.level5);
			this.gbx_Level.Controls.Add(this.level4);
			this.gbx_Level.Controls.Add(this.level3);
			this.gbx_Level.Controls.Add(this.level2);
			this.gbx_Level.Controls.Add(this.level1);
			this.gbx_Level.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbx_Level.Location = new System.Drawing.Point(0, 0);
			this.gbx_Level.Name = "gbx_Level";
			this.gbx_Level.Size = new System.Drawing.Size(227, 298);
			this.gbx_Level.TabIndex = 0;
			this.gbx_Level.TabStop = false;
			this.gbx_Level.Text = "ゲームレベル(&L)";
			// 
			// level5
			// 
			this.level5.AutoSize = true;
			this.level5.Location = new System.Drawing.Point(19, 115);
			this.level5.Name = "level5";
			this.level5.Size = new System.Drawing.Size(107, 16);
			this.level5.TabIndex = 4;
			this.level5.TabStop = true;
			this.level5.Text = "レベル５ （難しい）";
			this.level5.UseVisualStyleBackColor = true;
			// 
			// level4
			// 
			this.level4.AutoSize = true;
			this.level4.Location = new System.Drawing.Point(19, 93);
			this.level4.Name = "level4";
			this.level4.Size = new System.Drawing.Size(60, 16);
			this.level4.TabIndex = 3;
			this.level4.TabStop = true;
			this.level4.Text = "レベル４";
			this.level4.UseVisualStyleBackColor = true;
			// 
			// level3
			// 
			this.level3.AutoSize = true;
			this.level3.Location = new System.Drawing.Point(19, 71);
			this.level3.Name = "level3";
			this.level3.Size = new System.Drawing.Size(100, 16);
			this.level3.TabIndex = 2;
			this.level3.TabStop = true;
			this.level3.Text = "レベル３ （普通）";
			this.level3.UseVisualStyleBackColor = true;
			// 
			// level2
			// 
			this.level2.AutoSize = true;
			this.level2.Location = new System.Drawing.Point(19, 49);
			this.level2.Name = "level2";
			this.level2.Size = new System.Drawing.Size(60, 16);
			this.level2.TabIndex = 1;
			this.level2.TabStop = true;
			this.level2.Text = "レベル２";
			this.level2.UseVisualStyleBackColor = true;
			// 
			// level1
			// 
			this.level1.AutoSize = true;
			this.level1.Location = new System.Drawing.Point(19, 27);
			this.level1.Name = "level1";
			this.level1.Size = new System.Drawing.Size(100, 16);
			this.level1.TabIndex = 0;
			this.level1.TabStop = true;
			this.level1.Text = "レベル１ （簡単）";
			this.level1.UseVisualStyleBackColor = true;
			// 
			// SelectNumberPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.Controls.Add(this.splitContainer);
			this.Name = "SelectNumberPage";
			this.Size = new System.Drawing.Size(434, 298);
			this.gbx_nPlayer.ResumeLayout(false);
			this.gbx_nPlayer.PerformLayout();
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.ResumeLayout(false);
			this.gbx_Level.ResumeLayout(false);
			this.gbx_Level.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gbx_nPlayer;
		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.GroupBox gbx_Level;
		private System.Windows.Forms.RadioButton player4;
		private System.Windows.Forms.RadioButton player3;
		private System.Windows.Forms.RadioButton player2;
		private System.Windows.Forms.RadioButton player1;
		private System.Windows.Forms.RadioButton player0;
		private System.Windows.Forms.RadioButton level5;
		private System.Windows.Forms.RadioButton level4;
		private System.Windows.Forms.RadioButton level3;
		private System.Windows.Forms.RadioButton level2;
		private System.Windows.Forms.RadioButton level1;
	}
}
