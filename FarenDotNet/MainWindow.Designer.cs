namespace FarenDotNet
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
            if (disposing && (components != null))
            {
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
			this.btn_WorldMap = new System.Windows.Forms.Button();
			this.btn_NewGame = new System.Windows.Forms.Button();
			this.btn_War = new System.Windows.Forms.Button();
			this.btn_Load = new System.Windows.Forms.Button();
			this._openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// btn_WorldMap
			// 
			this.btn_WorldMap.Location = new System.Drawing.Point(16, 112);
			this.btn_WorldMap.Name = "btn_WorldMap";
			this.btn_WorldMap.Size = new System.Drawing.Size(112, 32);
			this.btn_WorldMap.TabIndex = 1;
			this.btn_WorldMap.Text = "内政デモ";
			this.btn_WorldMap.UseVisualStyleBackColor = true;
			this.btn_WorldMap.Click += new System.EventHandler(this.btn_WorldMap_Click);
			// 
			// btn_NewGame
			// 
			this.btn_NewGame.Location = new System.Drawing.Point(16, 8);
			this.btn_NewGame.Name = "btn_NewGame";
			this.btn_NewGame.Size = new System.Drawing.Size(112, 32);
			this.btn_NewGame.TabIndex = 0;
			this.btn_NewGame.Text = "新規ゲーム";
			this.btn_NewGame.UseVisualStyleBackColor = true;
			this.btn_NewGame.Click += new System.EventHandler(this.btn_NewGame_Click);
			// 
			// btn_War
			// 
			this.btn_War.Location = new System.Drawing.Point(16, 152);
			this.btn_War.Name = "btn_War";
			this.btn_War.Size = new System.Drawing.Size(112, 32);
			this.btn_War.TabIndex = 2;
			this.btn_War.Text = "戦争デモ";
			this.btn_War.UseVisualStyleBackColor = true;
			this.btn_War.Click += new System.EventHandler(this.btn_War_Click);
			// 
			// btn_Load
			// 
			this.btn_Load.Location = new System.Drawing.Point(16, 48);
			this.btn_Load.Name = "btn_Load";
			this.btn_Load.Size = new System.Drawing.Size(112, 32);
			this.btn_Load.TabIndex = 3;
			this.btn_Load.Text = "ロード";
			this.btn_Load.UseVisualStyleBackColor = true;
			this.btn_Load.Click += new System.EventHandler(this.btn_Load_Click);
			// 
			// _openFileDialog
			// 
			this._openFileDialog.DefaultExt = "save";
			this._openFileDialog.FileName = "セーブデータ";
			this._openFileDialog.Filter = "セーブデータ|*.save";
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 263);
			this.Controls.Add(this.btn_Load);
			this.Controls.Add(this.btn_War);
			this.Controls.Add(this.btn_NewGame);
			this.Controls.Add(this.btn_WorldMap);
			this.Name = "MainWindow";
			this.Text = "ファーレントゥーガ.NET";
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Button btn_WorldMap;
		private System.Windows.Forms.Button btn_NewGame;
		private System.Windows.Forms.Button btn_War;
		private System.Windows.Forms.Button btn_Load;
		private System.Windows.Forms.OpenFileDialog _openFileDialog;
    }
}

