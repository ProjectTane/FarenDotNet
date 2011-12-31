namespace FarenDotNet.Reign.UI
{
	partial class EmployWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this._splitContainer = new System.Windows.Forms.SplitContainer();
			this._listBox = new System.Windows.Forms.ListBox();
			this.btn_Cancel = new System.Windows.Forms.Button();
			this.btn_Employ = new System.Windows.Forms.Button();
			this.lbl_Spiece = new System.Windows.Forms.Label();
			this.lbl_Num = new System.Windows.Forms.Label();
			this._splitContainer.Panel1.SuspendLayout();
			this._splitContainer.Panel2.SuspendLayout();
			this._splitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// _splitContainer
			// 
			this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this._splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this._splitContainer.Location = new System.Drawing.Point(0, 0);
			this._splitContainer.Name = "_splitContainer";
			// 
			// _splitContainer.Panel1
			// 
			this._splitContainer.Panel1.Controls.Add(this._listBox);
			// 
			// _splitContainer.Panel2
			// 
			this._splitContainer.Panel2.Controls.Add(this.btn_Cancel);
			this._splitContainer.Panel2.Controls.Add(this.btn_Employ);
			this._splitContainer.Panel2.Controls.Add(this.lbl_Spiece);
			this._splitContainer.Panel2.Controls.Add(this.lbl_Num);
			this._splitContainer.Panel2.ForeColor = System.Drawing.Color.White;
			this._splitContainer.Size = new System.Drawing.Size(283, 175);
			this._splitContainer.SplitterDistance = 116;
			this._splitContainer.TabIndex = 0;
			// 
			// _listBox
			// 
			this._listBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this._listBox.FormattingEnabled = true;
			this._listBox.ItemHeight = 12;
			this._listBox.Location = new System.Drawing.Point(0, 0);
			this._listBox.Name = "_listBox";
			this._listBox.Size = new System.Drawing.Size(116, 172);
			this._listBox.TabIndex = 0;
			// 
			// btn_Cancel
			// 
			this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Cancel.BackColor = System.Drawing.Color.Navy;
			this.btn_Cancel.Location = new System.Drawing.Point(76, 140);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
			this.btn_Cancel.TabIndex = 3;
			this.btn_Cancel.Text = "やめる(&C)";
			this.btn_Cancel.UseVisualStyleBackColor = false;
			this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
			// 
			// btn_Employ
			// 
			this.btn_Employ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Employ.BackColor = System.Drawing.Color.Navy;
			this.btn_Employ.Location = new System.Drawing.Point(76, 111);
			this.btn_Employ.Name = "btn_Employ";
			this.btn_Employ.Size = new System.Drawing.Size(75, 23);
			this.btn_Employ.TabIndex = 2;
			this.btn_Employ.Text = "雇う(&E)";
			this.btn_Employ.UseVisualStyleBackColor = false;
			this.btn_Employ.Click += new System.EventHandler(this.btn_Employ_Click);
			// 
			// lbl_Spiece
			// 
			this.lbl_Spiece.AutoSize = true;
			this.lbl_Spiece.Location = new System.Drawing.Point(3, 40);
			this.lbl_Spiece.Name = "lbl_Spiece";
			this.lbl_Spiece.Size = new System.Drawing.Size(103, 24);
			this.lbl_Spiece.TabIndex = 1;
			this.lbl_Spiece.Text = "同種族の部隊は\r\n　　（同）で始まります";
			// 
			// lbl_Num
			// 
			this.lbl_Num.AutoSize = true;
			this.lbl_Num.Location = new System.Drawing.Point(3, 9);
			this.lbl_Num.Name = "lbl_Num";
			this.lbl_Num.Size = new System.Drawing.Size(102, 12);
			this.lbl_Num.TabIndex = 0;
			this.lbl_Num.Text = "雇用可能部隊数：X";
			// 
			// EmployWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Navy;
			this.ClientSize = new System.Drawing.Size(283, 175);
			this.Controls.Add(this._splitContainer);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "EmployWindow";
			this.Text = "EmployWindow";
			this._splitContainer.Panel1.ResumeLayout(false);
			this._splitContainer.Panel2.ResumeLayout(false);
			this._splitContainer.Panel2.PerformLayout();
			this._splitContainer.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer _splitContainer;
		private System.Windows.Forms.ListBox _listBox;
		private System.Windows.Forms.Label lbl_Spiece;
		private System.Windows.Forms.Label lbl_Num;
		private System.Windows.Forms.Button btn_Cancel;
		private System.Windows.Forms.Button btn_Employ;
	}
}