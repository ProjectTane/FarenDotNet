﻿namespace FarenDotNet.Reign.UI
{
	partial class MoveAreaCandidateWindow
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
			this._label = new System.Windows.Forms.Label();
			this.btn_Cancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// _label
			// 
			this._label.AutoSize = true;
			this._label.Dock = System.Windows.Forms.DockStyle.Top;
			this._label.Location = new System.Drawing.Point(0, 0);
			this._label.Name = "_label";
			this._label.Size = new System.Drawing.Size(125, 12);
			this._label.TabIndex = 0;
			this._label.Text = "移動先を選択してください";
			// 
			// btn_Cancel
			// 
			this.btn_Cancel.Dock = System.Windows.Forms.DockStyle.Top;
			this.btn_Cancel.Location = new System.Drawing.Point(0, 12);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Size = new System.Drawing.Size(125, 23);
			this.btn_Cancel.TabIndex = 1;
			this.btn_Cancel.Text = "キャンセル";
			this.btn_Cancel.UseVisualStyleBackColor = true;
			this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
			// 
			// MoveAreaCandidateWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(125, 35);
			this.Controls.Add(this.btn_Cancel);
			this.Controls.Add(this._label);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MoveAreaCandidateWindow";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "MoveAreaCandidateWindow";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label _label;
		private System.Windows.Forms.Button btn_Cancel;
	}
}