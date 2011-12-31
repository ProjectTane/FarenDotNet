namespace FarenDotNet.Reign.UI
{
	partial class AreaInfoWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AreaInfoWindow));
			this.flp_Units = new System.Windows.Forms.FlowLayoutPanel();
			this.detail = new System.Windows.Forms.Panel();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.btn_Move = new System.Windows.Forms.Button();
			this.btn_Employ = new System.Windows.Forms.Button();
			this.btn_Chop = new System.Windows.Forms.Button();
			this.cbx_Action = new System.Windows.Forms.ComboBox();
			this.lbl_Species = new System.Windows.Forms.Label();
			this.areaInfo = new System.Windows.Forms.GroupBox();
			this.lbl_vRoad = new System.Windows.Forms.Label();
			this.lbl_Road = new System.Windows.Forms.Label();
			this.lbl_vWall = new System.Windows.Forms.Label();
			this.lbl_vCity = new System.Windows.Forms.Label();
			this.lbl_vAccess = new System.Windows.Forms.Label();
			this.lbl_vIncome = new System.Windows.Forms.Label();
			this.lbl_Wall = new System.Windows.Forms.Label();
			this.lbl_City = new System.Windows.Forms.Label();
			this.lbl_Access = new System.Windows.Forms.Label();
			this.lbl_Income = new System.Windows.Forms.Label();
			this.detail.SuspendLayout();
			this.tableLayoutPanel.SuspendLayout();
			this.areaInfo.SuspendLayout();
			this.SuspendLayout();
			// 
			// flp_Units
			// 
			this.flp_Units.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flp_Units.Location = new System.Drawing.Point(0, 0);
			this.flp_Units.Name = "flp_Units";
			this.flp_Units.Size = new System.Drawing.Size(198, 168);
			this.flp_Units.TabIndex = 0;
			// 
			// detail
			// 
			this.detail.Controls.Add(this.tableLayoutPanel);
			this.detail.Controls.Add(this.cbx_Action);
			this.detail.Controls.Add(this.lbl_Species);
			this.detail.Controls.Add(this.areaInfo);
			this.detail.Dock = System.Windows.Forms.DockStyle.Right;
			this.detail.ForeColor = System.Drawing.Color.White;
			this.detail.Location = new System.Drawing.Point(198, 0);
			this.detail.Name = "detail";
			this.detail.Padding = new System.Windows.Forms.Padding(5);
			this.detail.Size = new System.Drawing.Size(106, 168);
			this.detail.TabIndex = 1;
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.ColumnCount = 3;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.35F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.35F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
			this.tableLayoutPanel.Controls.Add(this.btn_Move, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.btn_Employ, 1, 0);
			this.tableLayoutPanel.Controls.Add(this.btn_Chop, 2, 0);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel.ForeColor = System.Drawing.Color.White;
			this.tableLayoutPanel.Location = new System.Drawing.Point(5, 139);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(96, 24);
			this.tableLayoutPanel.TabIndex = 3;
			// 
			// btn_Move
			// 
			this.btn_Move.BackColor = System.Drawing.Color.Navy;
			this.btn_Move.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btn_Move.Image = global::FarenDotNet.Properties.Resources.Move;
			this.btn_Move.Location = new System.Drawing.Point(0, 0);
			this.btn_Move.Margin = new System.Windows.Forms.Padding(0);
			this.btn_Move.Name = "btn_Move";
			this.btn_Move.Size = new System.Drawing.Size(32, 24);
			this.btn_Move.TabIndex = 0;
			this.btn_Move.UseVisualStyleBackColor = false;
			this.btn_Move.Click += new System.EventHandler(this.btn_Move_Click);
			// 
			// btn_Employ
			// 
			this.btn_Employ.BackColor = System.Drawing.Color.Navy;
			this.btn_Employ.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btn_Employ.Image = ((System.Drawing.Image)(resources.GetObject("btn_Employ.Image")));
			this.btn_Employ.Location = new System.Drawing.Point(32, 0);
			this.btn_Employ.Margin = new System.Windows.Forms.Padding(0);
			this.btn_Employ.Name = "btn_Employ";
			this.btn_Employ.Size = new System.Drawing.Size(32, 24);
			this.btn_Employ.TabIndex = 1;
			this.btn_Employ.UseVisualStyleBackColor = false;
			this.btn_Employ.Click += new System.EventHandler(this.btn_Employ_Click);
			// 
			// btn_Chop
			// 
			this.btn_Chop.BackColor = System.Drawing.Color.Navy;
			this.btn_Chop.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btn_Chop.Image = global::FarenDotNet.Properties.Resources.Fire;
			this.btn_Chop.Location = new System.Drawing.Point(64, 0);
			this.btn_Chop.Margin = new System.Windows.Forms.Padding(0);
			this.btn_Chop.Name = "btn_Chop";
			this.btn_Chop.Size = new System.Drawing.Size(32, 24);
			this.btn_Chop.TabIndex = 2;
			this.btn_Chop.UseVisualStyleBackColor = false;
			this.btn_Chop.Click += new System.EventHandler(this.btn_Chop_Click);
			// 
			// cbx_Action
			// 
			this.cbx_Action.Dock = System.Windows.Forms.DockStyle.Top;
			this.cbx_Action.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbx_Action.FormattingEnabled = true;
			this.cbx_Action.Items.AddRange(new object[] {
            "部隊訓練",
            "人材探索",
            "街開発",
            "道路建設",
            "城壁建設"});
			this.cbx_Action.Location = new System.Drawing.Point(5, 119);
			this.cbx_Action.Name = "cbx_Action";
			this.cbx_Action.Size = new System.Drawing.Size(96, 20);
			this.cbx_Action.TabIndex = 2;
			this.cbx_Action.SelectedIndexChanged += new System.EventHandler(this.cbx_Action_SelectedIndexChanged);
			// 
			// lbl_Species
			// 
			this.lbl_Species.AutoSize = true;
			this.lbl_Species.Dock = System.Windows.Forms.DockStyle.Top;
			this.lbl_Species.Location = new System.Drawing.Point(5, 103);
			this.lbl_Species.Margin = new System.Windows.Forms.Padding(0);
			this.lbl_Species.Name = "lbl_Species";
			this.lbl_Species.Padding = new System.Windows.Forms.Padding(5, 2, 5, 2);
			this.lbl_Species.Size = new System.Drawing.Size(77, 16);
			this.lbl_Species.TabIndex = 1;
			this.lbl_Species.Text = "同種族　0人";
			// 
			// areaInfo
			// 
			this.areaInfo.Controls.Add(this.lbl_vRoad);
			this.areaInfo.Controls.Add(this.lbl_Road);
			this.areaInfo.Controls.Add(this.lbl_vWall);
			this.areaInfo.Controls.Add(this.lbl_vCity);
			this.areaInfo.Controls.Add(this.lbl_vAccess);
			this.areaInfo.Controls.Add(this.lbl_vIncome);
			this.areaInfo.Controls.Add(this.lbl_Wall);
			this.areaInfo.Controls.Add(this.lbl_City);
			this.areaInfo.Controls.Add(this.lbl_Access);
			this.areaInfo.Controls.Add(this.lbl_Income);
			this.areaInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.areaInfo.ForeColor = System.Drawing.Color.White;
			this.areaInfo.Location = new System.Drawing.Point(5, 5);
			this.areaInfo.Name = "areaInfo";
			this.areaInfo.Size = new System.Drawing.Size(96, 98);
			this.areaInfo.TabIndex = 0;
			this.areaInfo.TabStop = false;
			this.areaInfo.Text = "エリア情報";
			// 
			// lbl_vRoad
			// 
			this.lbl_vRoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lbl_vRoad.Location = new System.Drawing.Point(44, 79);
			this.lbl_vRoad.Name = "lbl_vRoad";
			this.lbl_vRoad.Size = new System.Drawing.Size(46, 16);
			this.lbl_vRoad.TabIndex = 9;
			this.lbl_vRoad.Text = "0/0";
			this.lbl_vRoad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbl_Road
			// 
			this.lbl_Road.Location = new System.Drawing.Point(6, 79);
			this.lbl_Road.Name = "lbl_Road";
			this.lbl_Road.Size = new System.Drawing.Size(32, 16);
			this.lbl_Road.TabIndex = 8;
			this.lbl_Road.Text = "道路";
			this.lbl_Road.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl_vWall
			// 
			this.lbl_vWall.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lbl_vWall.Location = new System.Drawing.Point(44, 63);
			this.lbl_vWall.Name = "lbl_vWall";
			this.lbl_vWall.Size = new System.Drawing.Size(46, 16);
			this.lbl_vWall.TabIndex = 7;
			this.lbl_vWall.Text = "0/0";
			this.lbl_vWall.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbl_vCity
			// 
			this.lbl_vCity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lbl_vCity.Location = new System.Drawing.Point(44, 47);
			this.lbl_vCity.Name = "lbl_vCity";
			this.lbl_vCity.Size = new System.Drawing.Size(46, 16);
			this.lbl_vCity.TabIndex = 6;
			this.lbl_vCity.Text = "0/0";
			this.lbl_vCity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbl_vAccess
			// 
			this.lbl_vAccess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lbl_vAccess.Location = new System.Drawing.Point(44, 31);
			this.lbl_vAccess.Name = "lbl_vAccess";
			this.lbl_vAccess.Size = new System.Drawing.Size(46, 16);
			this.lbl_vAccess.TabIndex = 5;
			this.lbl_vAccess.Text = "×";
			this.lbl_vAccess.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbl_vIncome
			// 
			this.lbl_vIncome.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lbl_vIncome.Location = new System.Drawing.Point(44, 15);
			this.lbl_vIncome.Name = "lbl_vIncome";
			this.lbl_vIncome.Size = new System.Drawing.Size(46, 16);
			this.lbl_vIncome.TabIndex = 4;
			this.lbl_vIncome.Text = "0";
			this.lbl_vIncome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbl_Wall
			// 
			this.lbl_Wall.Location = new System.Drawing.Point(6, 63);
			this.lbl_Wall.Name = "lbl_Wall";
			this.lbl_Wall.Size = new System.Drawing.Size(32, 16);
			this.lbl_Wall.TabIndex = 3;
			this.lbl_Wall.Text = "城壁";
			this.lbl_Wall.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl_City
			// 
			this.lbl_City.Location = new System.Drawing.Point(6, 47);
			this.lbl_City.Name = "lbl_City";
			this.lbl_City.Size = new System.Drawing.Size(32, 16);
			this.lbl_City.TabIndex = 2;
			this.lbl_City.Text = "街";
			this.lbl_City.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl_Access
			// 
			this.lbl_Access.Location = new System.Drawing.Point(6, 31);
			this.lbl_Access.Name = "lbl_Access";
			this.lbl_Access.Size = new System.Drawing.Size(32, 16);
			this.lbl_Access.TabIndex = 1;
			this.lbl_Access.Text = "交通";
			this.lbl_Access.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbl_Income
			// 
			this.lbl_Income.Location = new System.Drawing.Point(6, 15);
			this.lbl_Income.Name = "lbl_Income";
			this.lbl_Income.Size = new System.Drawing.Size(32, 16);
			this.lbl_Income.TabIndex = 0;
			this.lbl_Income.Text = "収入";
			this.lbl_Income.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// AreaInfoWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Navy;
			this.ClientSize = new System.Drawing.Size(304, 168);
			this.Controls.Add(this.flp_Units);
			this.Controls.Add(this.detail);
			this.ForeColor = System.Drawing.Color.White;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AreaInfoWindow";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "AreaInfoWindow";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AreaInfoWindow_FormClosing);
			this.detail.ResumeLayout(false);
			this.detail.PerformLayout();
			this.tableLayoutPanel.ResumeLayout(false);
			this.areaInfo.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel flp_Units;
		private System.Windows.Forms.Panel detail;
		private System.Windows.Forms.GroupBox areaInfo;
		private System.Windows.Forms.Label lbl_vWall;
		private System.Windows.Forms.Label lbl_vCity;
		private System.Windows.Forms.Label lbl_vAccess;
		private System.Windows.Forms.Label lbl_vIncome;
		private System.Windows.Forms.Label lbl_Wall;
		private System.Windows.Forms.Label lbl_City;
		private System.Windows.Forms.Label lbl_Access;
		private System.Windows.Forms.Label lbl_Income;
		private System.Windows.Forms.Label lbl_vRoad;
		private System.Windows.Forms.Label lbl_Road;
		private System.Windows.Forms.Label lbl_Species;
		private System.Windows.Forms.ComboBox cbx_Action;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Button btn_Move;
		private System.Windows.Forms.Button btn_Employ;
		private System.Windows.Forms.Button btn_Chop;
	}
}