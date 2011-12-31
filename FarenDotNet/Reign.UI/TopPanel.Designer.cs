namespace FarenDotNet.Reign.UI
{
	partial class TopPanel
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

		#region コンポーネント デザイナで生成されたコード

		/// <summary> 
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.lbl_Name = new System.Windows.Forms.Label();
			this.lbl_Turn = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lbl_Name
			// 
			this.lbl_Name.BackColor = System.Drawing.Color.Navy;
			this.lbl_Name.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lbl_Name.Dock = System.Windows.Forms.DockStyle.Left;
			this.lbl_Name.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lbl_Name.ForeColor = System.Drawing.Color.White;
			this.lbl_Name.Location = new System.Drawing.Point(1, 1);
			this.lbl_Name.Name = "lbl_Name";
			this.lbl_Name.Size = new System.Drawing.Size(191, 31);
			this.lbl_Name.TabIndex = 0;
			this.lbl_Name.Text = "Name";
			this.lbl_Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl_Turn
			// 
			this.lbl_Turn.BackColor = System.Drawing.Color.Navy;
			this.lbl_Turn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lbl_Turn.Dock = System.Windows.Forms.DockStyle.Right;
			this.lbl_Turn.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lbl_Turn.ForeColor = System.Drawing.Color.White;
			this.lbl_Turn.Location = new System.Drawing.Point(309, 1);
			this.lbl_Turn.Name = "lbl_Turn";
			this.lbl_Turn.Size = new System.Drawing.Size(162, 31);
			this.lbl_Turn.TabIndex = 1;
			this.lbl_Turn.Text = "第 0 ターン";
			this.lbl_Turn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// TopPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lbl_Name);
			this.Controls.Add(this.lbl_Turn);
			this.Name = "TopPanel";
			this.Padding = new System.Windows.Forms.Padding(1);
			this.Size = new System.Drawing.Size(472, 33);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lbl_Name;
		private System.Windows.Forms.Label lbl_Turn;
	}
}
