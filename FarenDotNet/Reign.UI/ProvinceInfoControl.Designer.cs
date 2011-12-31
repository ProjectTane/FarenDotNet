namespace FarenDotNet.Reign.UI
{
	partial class ProvinceInfoControl
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

		#region コンポーネント デザイナで生成されたコード

		/// <summary> 
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.ListView = new System.Windows.Forms.ListView();
			this.prov = new System.Windows.Forms.ColumnHeader();
			this.baseArea = new System.Windows.Forms.ColumnHeader();
			this.nArea = new System.Windows.Forms.ColumnHeader();
			this.money = new System.Windows.Forms.ColumnHeader();
			this.income = new System.Windows.Forms.ColumnHeader();
			this.ｎUnique = new System.Windows.Forms.ColumnHeader();
			this.power = new System.Windows.Forms.ColumnHeader();
			this.league = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// ListView
			// 
			this.ListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.ListView.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
			this.ListView.AllowColumnReorder = true;
			this.ListView.BackColor = System.Drawing.Color.Navy;
			this.ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.prov,
            this.baseArea,
            this.nArea,
            this.money,
            this.income,
            this.ｎUnique,
            this.power,
            this.league});
			this.ListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ListView.ForeColor = System.Drawing.Color.White;
			this.ListView.FullRowSelect = true;
			this.ListView.HideSelection = false;
			this.ListView.Location = new System.Drawing.Point(0, 0);
			this.ListView.MultiSelect = false;
			this.ListView.Name = "ListView";
			this.ListView.ShowGroups = false;
			this.ListView.Size = new System.Drawing.Size(491, 150);
			this.ListView.TabIndex = 0;
			this.ListView.UseCompatibleStateImageBehavior = false;
			this.ListView.View = System.Windows.Forms.View.Details;
			// 
			// prov
			// 
			this.prov.Text = "勢力名";
			this.prov.Width = 86;
			// 
			// baseArea
			// 
			this.baseArea.Text = "本拠地";
			this.baseArea.Width = 103;
			// 
			// nArea
			// 
			this.nArea.Text = "領地";
			this.nArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.nArea.Width = 46;
			// 
			// money
			// 
			this.money.Text = "軍資金";
			this.money.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.money.Width = 52;
			// 
			// income
			// 
			this.income.Text = "総収入";
			this.income.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.income.Width = 54;
			// 
			// ｎUnique
			// 
			this.ｎUnique.Text = "人材数";
			this.ｎUnique.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.ｎUnique.Width = 57;
			// 
			// power
			// 
			this.power.Text = "戦力指数";
			this.power.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.power.Width = 67;
			// 
			// league
			// 
			this.league.Text = "同盟";
			this.league.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.league.Width = 53;
			// 
			// ProvinceInfoControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ListView);
			this.Name = "ProvinceInfoControl";
			this.Size = new System.Drawing.Size(491, 150);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ColumnHeader prov;
		private System.Windows.Forms.ColumnHeader baseArea;
		private System.Windows.Forms.ColumnHeader nArea;
		private System.Windows.Forms.ColumnHeader money;
		private System.Windows.Forms.ColumnHeader income;
		private System.Windows.Forms.ColumnHeader ｎUnique;
		private System.Windows.Forms.ColumnHeader power;
		private System.Windows.Forms.ColumnHeader league;
		public System.Windows.Forms.ListView ListView;
	}
}
