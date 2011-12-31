namespace FarenDotNet.Reign.UI
{
	partial class MasterWindow
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
			this.components = new System.ComponentModel.Container();
			this.btn_Finish = new System.Windows.Forms.Button();
			this.lbl_Name = new System.Windows.Forms.Label();
			this.lbl_Income = new System.Windows.Forms.Label();
			this.lbl_vIncome = new System.Windows.Forms.Label();
			this.lbl_Measure1 = new System.Windows.Forms.Label();
			this.lbl_Measure2 = new System.Windows.Forms.Label();
			this.lbl_Measure3 = new System.Windows.Forms.Label();
			this.lbl_Cost = new System.Windows.Forms.Label();
			this.lbl_vCost = new System.Windows.Forms.Label();
			this.lbl_Money = new System.Windows.Forms.Label();
			this.lbl_vMoney = new System.Windows.Forms.Label();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this._saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.pbx_Flag = new System.Windows.Forms.PictureBox();
			this.pbx_Face = new System.Windows.Forms.PictureBox();
			this.btn_War = new System.Windows.Forms.Button();
			this.btn_League = new System.Windows.Forms.Button();
			this.btn_Save = new System.Windows.Forms.Button();
			this._toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.tableLayoutPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbx_Flag)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbx_Face)).BeginInit();
			this.SuspendLayout();
			// 
			// btn_Finish
			// 
			this.btn_Finish.BackColor = System.Drawing.Color.Navy;
			this.btn_Finish.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btn_Finish.Image = global::FarenDotNet.Properties.Resources.End;
			this.btn_Finish.Location = new System.Drawing.Point(120, 0);
			this.btn_Finish.Margin = new System.Windows.Forms.Padding(0);
			this.btn_Finish.Name = "btn_Finish";
			this.btn_Finish.Size = new System.Drawing.Size(40, 40);
			this.btn_Finish.TabIndex = 3;
			this._toolTip.SetToolTip(this.btn_Finish, "ターン終了");
			this.btn_Finish.UseVisualStyleBackColor = false;
			this.btn_Finish.Click += new System.EventHandler(this.btn_Finish_Click);
			// 
			// lbl_Name
			// 
			this.lbl_Name.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lbl_Name.Location = new System.Drawing.Point(0, 0);
			this.lbl_Name.Name = "lbl_Name";
			this.lbl_Name.Size = new System.Drawing.Size(136, 23);
			this.lbl_Name.TabIndex = 3;
			this.lbl_Name.Text = "Name";
			// 
			// lbl_Income
			// 
			this.lbl_Income.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lbl_Income.Location = new System.Drawing.Point(96, 24);
			this.lbl_Income.Name = "lbl_Income";
			this.lbl_Income.Size = new System.Drawing.Size(40, 16);
			this.lbl_Income.TabIndex = 4;
			this.lbl_Income.Text = "収入";
			this.lbl_Income.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// lbl_vIncome
			// 
			this.lbl_vIncome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lbl_vIncome.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lbl_vIncome.Location = new System.Drawing.Point(88, 40);
			this.lbl_vIncome.Name = "lbl_vIncome";
			this.lbl_vIncome.Size = new System.Drawing.Size(48, 16);
			this.lbl_vIncome.TabIndex = 5;
			this.lbl_vIncome.Text = "10";
			this.lbl_vIncome.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lbl_Measure1
			// 
			this.lbl_Measure1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lbl_Measure1.Location = new System.Drawing.Point(136, 40);
			this.lbl_Measure1.Name = "lbl_Measure1";
			this.lbl_Measure1.Size = new System.Drawing.Size(24, 16);
			this.lbl_Measure1.TabIndex = 6;
			this.lbl_Measure1.Text = "Ley";
			// 
			// lbl_Measure2
			// 
			this.lbl_Measure2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lbl_Measure2.Location = new System.Drawing.Point(136, 72);
			this.lbl_Measure2.Name = "lbl_Measure2";
			this.lbl_Measure2.Size = new System.Drawing.Size(23, 12);
			this.lbl_Measure2.TabIndex = 10;
			this.lbl_Measure2.Text = "Ley";
			// 
			// lbl_Measure3
			// 
			this.lbl_Measure3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lbl_Measure3.Location = new System.Drawing.Point(136, 104);
			this.lbl_Measure3.Name = "lbl_Measure3";
			this.lbl_Measure3.Size = new System.Drawing.Size(23, 12);
			this.lbl_Measure3.TabIndex = 11;
			this.lbl_Measure3.Text = "Ley";
			// 
			// lbl_Cost
			// 
			this.lbl_Cost.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lbl_Cost.Location = new System.Drawing.Point(96, 56);
			this.lbl_Cost.Name = "lbl_Cost";
			this.lbl_Cost.Size = new System.Drawing.Size(72, 16);
			this.lbl_Cost.TabIndex = 12;
			this.lbl_Cost.Text = "人材費";
			this.lbl_Cost.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// lbl_vCost
			// 
			this.lbl_vCost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lbl_vCost.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lbl_vCost.Location = new System.Drawing.Point(88, 72);
			this.lbl_vCost.Name = "lbl_vCost";
			this.lbl_vCost.Size = new System.Drawing.Size(48, 16);
			this.lbl_vCost.TabIndex = 13;
			this.lbl_vCost.Text = "2";
			this.lbl_vCost.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lbl_Money
			// 
			this.lbl_Money.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lbl_Money.Location = new System.Drawing.Point(96, 88);
			this.lbl_Money.Name = "lbl_Money";
			this.lbl_Money.Size = new System.Drawing.Size(72, 16);
			this.lbl_Money.TabIndex = 14;
			this.lbl_Money.Text = "軍事費";
			this.lbl_Money.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// lbl_vMoney
			// 
			this.lbl_vMoney.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lbl_vMoney.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lbl_vMoney.Location = new System.Drawing.Point(88, 104);
			this.lbl_vMoney.Name = "lbl_vMoney";
			this.lbl_vMoney.Size = new System.Drawing.Size(48, 16);
			this.lbl_vMoney.TabIndex = 15;
			this.lbl_vMoney.Text = "60";
			this.lbl_vMoney.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel.BackColor = System.Drawing.Color.Navy;
			this.tableLayoutPanel.ColumnCount = 4;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel.Controls.Add(this.btn_Finish, 3, 0);
			this.tableLayoutPanel.Controls.Add(this.btn_War, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.btn_League, 1, 0);
			this.tableLayoutPanel.Controls.Add(this.btn_Save, 2, 0);
			this.tableLayoutPanel.ForeColor = System.Drawing.Color.White;
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 120);
			this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(160, 40);
			this.tableLayoutPanel.TabIndex = 16;
			// 
			// _saveFileDialog
			// 
			this._saveFileDialog.DefaultExt = "save";
			this._saveFileDialog.Filter = "セーブデータ|*.save";
			// 
			// pbx_Flag
			// 
			this.pbx_Flag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pbx_Flag.Location = new System.Drawing.Point(128, 0);
			this.pbx_Flag.Name = "pbx_Flag";
			this.pbx_Flag.Size = new System.Drawing.Size(32, 32);
			this.pbx_Flag.TabIndex = 2;
			this.pbx_Flag.TabStop = false;
			// 
			// pbx_Face
			// 
			this.pbx_Face.Location = new System.Drawing.Point(0, 24);
			this.pbx_Face.Name = "pbx_Face";
			this.pbx_Face.Size = new System.Drawing.Size(96, 96);
			this.pbx_Face.TabIndex = 1;
			this.pbx_Face.TabStop = false;
			// 
			// btn_War
			// 
			this.btn_War.BackColor = System.Drawing.Color.Navy;
			this.btn_War.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btn_War.Image = global::FarenDotNet.Properties.Resources.War;
			this.btn_War.Location = new System.Drawing.Point(0, 0);
			this.btn_War.Margin = new System.Windows.Forms.Padding(0);
			this.btn_War.Name = "btn_War";
			this.btn_War.Size = new System.Drawing.Size(40, 40);
			this.btn_War.TabIndex = 0;
			this._toolTip.SetToolTip(this.btn_War, "戦争");
			this.btn_War.UseVisualStyleBackColor = false;
			this.btn_War.Click += new System.EventHandler(this.btn_War_Click);
			// 
			// btn_League
			// 
			this.btn_League.BackColor = System.Drawing.Color.Navy;
			this.btn_League.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btn_League.Image = global::FarenDotNet.Properties.Resources.League;
			this.btn_League.Location = new System.Drawing.Point(40, 0);
			this.btn_League.Margin = new System.Windows.Forms.Padding(0);
			this.btn_League.Name = "btn_League";
			this.btn_League.Size = new System.Drawing.Size(40, 40);
			this.btn_League.TabIndex = 2;
			this._toolTip.SetToolTip(this.btn_League, "同盟");
			this.btn_League.UseVisualStyleBackColor = false;
			this.btn_League.Click += new System.EventHandler(this.btn_League_Click);
			// 
			// btn_Save
			// 
			this.btn_Save.BackColor = System.Drawing.Color.Navy;
			this.btn_Save.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btn_Save.Image = global::FarenDotNet.Properties.Resources.Save;
			this.btn_Save.Location = new System.Drawing.Point(80, 0);
			this.btn_Save.Margin = new System.Windows.Forms.Padding(0);
			this.btn_Save.Name = "btn_Save";
			this.btn_Save.Size = new System.Drawing.Size(40, 40);
			this.btn_Save.TabIndex = 4;
			this._toolTip.SetToolTip(this.btn_Save, "保存");
			this.btn_Save.UseVisualStyleBackColor = false;
			this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
			// 
			// MasterWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Navy;
			this.ClientSize = new System.Drawing.Size(160, 160);
			this.Controls.Add(this.pbx_Flag);
			this.Controls.Add(this.pbx_Face);
			this.Controls.Add(this.tableLayoutPanel);
			this.Controls.Add(this.lbl_vMoney);
			this.Controls.Add(this.lbl_Money);
			this.Controls.Add(this.lbl_vCost);
			this.Controls.Add(this.lbl_Cost);
			this.Controls.Add(this.lbl_Measure3);
			this.Controls.Add(this.lbl_Measure2);
			this.Controls.Add(this.lbl_Measure1);
			this.Controls.Add(this.lbl_vIncome);
			this.Controls.Add(this.lbl_Income);
			this.Controls.Add(this.lbl_Name);
			this.ForeColor = System.Drawing.Color.White;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MasterWindow";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SelectActionWindow";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectActionWindow_FormClosing);
			this.tableLayoutPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbx_Flag)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbx_Face)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btn_Finish;
		private System.Windows.Forms.PictureBox pbx_Face;
		private System.Windows.Forms.PictureBox pbx_Flag;
		private System.Windows.Forms.Label lbl_Name;
		private System.Windows.Forms.Label lbl_Income;
		private System.Windows.Forms.Label lbl_vIncome;
		private System.Windows.Forms.Label lbl_Measure1;
		private System.Windows.Forms.Label lbl_Measure2;
		private System.Windows.Forms.Label lbl_Measure3;
		private System.Windows.Forms.Label lbl_Cost;
		private System.Windows.Forms.Label lbl_vCost;
		private System.Windows.Forms.Label lbl_Money;
		private System.Windows.Forms.Label lbl_vMoney;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Button btn_War;
		private System.Windows.Forms.Button btn_League;
		private System.Windows.Forms.Button btn_Save;
		private System.Windows.Forms.SaveFileDialog _saveFileDialog;
		private System.Windows.Forms.ToolTip _toolTip;
	}
}