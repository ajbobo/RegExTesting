namespace RegularExpressionTester
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.lblExpression = new System.Windows.Forms.Label();
			this.rtbExpression = new System.Windows.Forms.RichTextBox();
			this.lblData = new System.Windows.Forms.Label();
			this.rtbData = new System.Windows.Forms.RichTextBox();
			this.rtbOutput = new System.Windows.Forms.RichTextBox();
			this.btnTest = new System.Windows.Forms.Button();
			this.btnLiveUpdate = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// lblExpression
			// 
			this.lblExpression.AutoSize = true;
			this.lblExpression.Location = new System.Drawing.Point(13, 33);
			this.lblExpression.Name = "lblExpression";
			this.lblExpression.Size = new System.Drawing.Size(61, 13);
			this.lblExpression.TabIndex = 0;
			this.lblExpression.Text = "Expression:";
			// 
			// rtbExpression
			// 
			this.rtbExpression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.rtbExpression.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtbExpression.Location = new System.Drawing.Point(106, 10);
			this.rtbExpression.Name = "rtbExpression";
			this.rtbExpression.Size = new System.Drawing.Size(466, 58);
			this.rtbExpression.TabIndex = 1;
			this.rtbExpression.Text = "(?<First>\\S+)\\s+(?<Last>\\S+)";
			this.rtbExpression.TextChanged += new System.EventHandler(this.Evaluate_RegEx);
			// 
			// lblData
			// 
			this.lblData.AutoSize = true;
			this.lblData.Location = new System.Drawing.Point(13, 93);
			this.lblData.Name = "lblData";
			this.lblData.Size = new System.Drawing.Size(57, 13);
			this.lblData.TabIndex = 2;
			this.lblData.Text = "Test Data:";
			// 
			// rtbData
			// 
			this.rtbData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.rtbData.DetectUrls = false;
			this.rtbData.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtbData.Location = new System.Drawing.Point(106, 76);
			this.rtbData.Name = "rtbData";
			this.rtbData.Size = new System.Drawing.Size(385, 69);
			this.rtbData.TabIndex = 3;
			this.rtbData.Text = "A.J. Bobo";
			this.rtbData.TextChanged += new System.EventHandler(this.Evaluate_RegEx);
			// 
			// rtbOutput
			// 
			this.rtbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.rtbOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtbOutput.Location = new System.Drawing.Point(16, 151);
			this.rtbOutput.Name = "rtbOutput";
			this.rtbOutput.ReadOnly = true;
			this.rtbOutput.Size = new System.Drawing.Size(556, 280);
			this.rtbOutput.TabIndex = 4;
			this.rtbOutput.TabStop = false;
			this.rtbOutput.Text = "";
			// 
			// btnTest
			// 
			this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnTest.Location = new System.Drawing.Point(497, 76);
			this.btnTest.Name = "btnTest";
			this.btnTest.Size = new System.Drawing.Size(75, 69);
			this.btnTest.TabIndex = 5;
			this.btnTest.Text = "&Test";
			this.btnTest.UseVisualStyleBackColor = true;
			this.btnTest.Click += new System.EventHandler(this.Evaluate_RegEx);
			// 
			// btnLiveUpdate
			// 
			this.btnLiveUpdate.AutoSize = true;
			this.btnLiveUpdate.Checked = true;
			this.btnLiveUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
			this.btnLiveUpdate.Location = new System.Drawing.Point(16, 109);
			this.btnLiveUpdate.Name = "btnLiveUpdate";
			this.btnLiveUpdate.Size = new System.Drawing.Size(84, 17);
			this.btnLiveUpdate.TabIndex = 6;
			this.btnLiveUpdate.Text = "Live Update";
			this.btnLiveUpdate.UseVisualStyleBackColor = true;
			this.btnLiveUpdate.CheckedChanged += new System.EventHandler(this.btnLiveUpdate_CheckedChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 443);
			this.Controls.Add(this.btnLiveUpdate);
			this.Controls.Add(this.btnTest);
			this.Controls.Add(this.rtbOutput);
			this.Controls.Add(this.rtbData);
			this.Controls.Add(this.lblData);
			this.Controls.Add(this.rtbExpression);
			this.Controls.Add(this.lblExpression);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Regular Expression Tester";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblExpression;
		private System.Windows.Forms.RichTextBox rtbExpression;
		private System.Windows.Forms.Label lblData;
		private System.Windows.Forms.RichTextBox rtbData;
		private System.Windows.Forms.RichTextBox rtbOutput;
		private System.Windows.Forms.Button btnTest;
		private System.Windows.Forms.CheckBox btnLiveUpdate;
	}
}

