namespace FarenDotNet.Reign.UI
{
	partial class MoveUnitWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose (bool disposing)
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
		private void InitializeComponent ()
		{
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this._panel = new System.Windows.Forms.Panel();
			this.btn_LtR = new System.Windows.Forms.Button();
			this.btn_RtL = new System.Windows.Forms.Button();
			this.flp_Left = new System.Windows.Forms.FlowLayoutPanel();
			this.flp_Right = new System.Windows.Forms.FlowLayoutPanel();
			this.btn_Finish = new System.Windows.Forms.Button();
			this.btn_Cancel = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this._panel.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this._panel, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.flp_Left, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.flp_Right, 2, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(444, 166);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// _panel
			// 
			this._panel.Controls.Add(this.btn_LtR);
			this._panel.Controls.Add(this.btn_RtL);
			this._panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this._panel.Location = new System.Drawing.Point(209, 3);
			this._panel.Name = "_panel";
			this._panel.Size = new System.Drawing.Size(26, 160);
			this._panel.TabIndex = 0;
			// 
			// btn_LtR
			// 
			this.btn_LtR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.btn_LtR.Location = new System.Drawing.Point(0, 63);
			this.btn_LtR.Name = "btn_LtR";
			this.btn_LtR.Size = new System.Drawing.Size(26, 23);
			this.btn_LtR.TabIndex = 1;
			this.btn_LtR.Text = ">>";
			this.btn_LtR.UseVisualStyleBackColor = true;
			this.btn_LtR.Click += new System.EventHandler(this.btn_LtR_Click);
			// 
			// btn_RtL
			// 
			this.btn_RtL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.btn_RtL.Location = new System.Drawing.Point(0, 34);
			this.btn_RtL.Name = "btn_RtL";
			this.btn_RtL.Size = new System.Drawing.Size(26, 23);
			this.btn_RtL.TabIndex = 0;
			this.btn_RtL.Text = "<<";
			this.btn_RtL.UseVisualStyleBackColor = true;
			this.btn_RtL.Click += new System.EventHandler(this.btn_RtL_Click);
			// 
			// flp_Left
			// 
			this.flp_Left.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flp_Left.Location = new System.Drawing.Point(3, 3);
			this.flp_Left.Name = "flp_Left";
			this.flp_Left.Size = new System.Drawing.Size(200, 160);
			this.flp_Left.TabIndex = 1;
			// 
			// flp_Right
			// 
			this.flp_Right.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flp_Right.Location = new System.Drawing.Point(241, 3);
			this.flp_Right.Name = "flp_Right";
			this.flp_Right.Size = new System.Drawing.Size(200, 160);
			this.flp_Right.TabIndex = 2;
			// 
			// btn_Finish
			// 
			this.btn_Finish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Finish.Location = new System.Drawing.Point(366, 172);
			this.btn_Finish.Name = "btn_Finish";
			this.btn_Finish.Size = new System.Drawing.Size(66, 23);
			this.btn_Finish.TabIndex = 1;
			this.btn_Finish.Text = "終了(&F)";
			this.btn_Finish.UseVisualStyleBackColor = true;
			this.btn_Finish.Click += new System.EventHandler(this.btn_Finish_Click);
			// 
			// btn_Cancel
			// 
			this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Cancel.Location = new System.Drawing.Point(276, 172);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Size = new System.Drawing.Size(84, 23);
			this.btn_Cancel.TabIndex = 2;
			this.btn_Cancel.Text = "キャンセル(&C)";
			this.btn_Cancel.UseVisualStyleBackColor = true;
			this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
			// 
			// MoveUnitWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Navy;
			this.ClientSize = new System.Drawing.Size(444, 207);
			this.Controls.Add(this.btn_Cancel);
			this.Controls.Add(this.btn_Finish);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MoveUnitWindow";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "MoveUnitWindow";
			this.tableLayoutPanel1.ResumeLayout(false);
			this._panel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel _panel;
		private System.Windows.Forms.Button btn_LtR;
		private System.Windows.Forms.Button btn_RtL;
		private System.Windows.Forms.FlowLayoutPanel flp_Left;
		private System.Windows.Forms.FlowLayoutPanel flp_Right;
		private System.Windows.Forms.Button btn_Finish;
		private System.Windows.Forms.Button btn_Cancel;
	}
}