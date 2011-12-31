namespace FarenDotNet.NewGame.UI
{
	partial class PageBinder
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
			this.btn_Cancel = new System.Windows.Forms.Button();
			this.btn_Next = new System.Windows.Forms.Button();
			this.btn_Back = new System.Windows.Forms.Button();
			this.panel = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// btn_Cancel
			// 
			this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Cancel.Location = new System.Drawing.Point(184, 207);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Size = new System.Drawing.Size(80, 23);
			this.btn_Cancel.TabIndex = 0;
			this.btn_Cancel.Text = "キャンセル(&C)";
			this.btn_Cancel.UseVisualStyleBackColor = true;
			// 
			// btn_Next
			// 
			this.btn_Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Next.Location = new System.Drawing.Point(88, 207);
			this.btn_Next.Name = "btn_Next";
			this.btn_Next.Size = new System.Drawing.Size(75, 23);
			this.btn_Next.TabIndex = 1;
			this.btn_Next.Text = "次へ(&N)";
			this.btn_Next.UseVisualStyleBackColor = true;
			this.btn_Next.Click += new System.EventHandler(this.btn_Next_Click);
			// 
			// btn_Back
			// 
			this.btn_Back.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Back.Location = new System.Drawing.Point(8, 207);
			this.btn_Back.Name = "btn_Back";
			this.btn_Back.Size = new System.Drawing.Size(75, 23);
			this.btn_Back.TabIndex = 2;
			this.btn_Back.Text = "戻る(&B)";
			this.btn_Back.UseVisualStyleBackColor = true;
			this.btn_Back.Click += new System.EventHandler(this.btn_Back_Click);
			// 
			// panel
			// 
			this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.panel.Location = new System.Drawing.Point(8, 8);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(256, 193);
			this.panel.TabIndex = 3;
			// 
			// PageBinder
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel);
			this.Controls.Add(this.btn_Back);
			this.Controls.Add(this.btn_Next);
			this.Controls.Add(this.btn_Cancel);
			this.Name = "PageBinder";
			this.Size = new System.Drawing.Size(272, 239);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btn_Cancel;
		private System.Windows.Forms.Button btn_Next;
		private System.Windows.Forms.Button btn_Back;
		private System.Windows.Forms.Panel panel;
	}
}
