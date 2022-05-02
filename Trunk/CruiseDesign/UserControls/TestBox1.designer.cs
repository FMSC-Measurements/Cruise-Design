namespace FMSC.Controls
{
    partial class TestBox1
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.topLabelTextBox1 = new FMSC.Controls.TopLabelTextBox();
            this.sideLabelTextBox1 = new FMSC.Controls.SideLabelTextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(60, 26);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            // 
            // topLabelTextBox1
            // 
            this.topLabelTextBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.topLabelTextBox1.LabelText = "label1";
            this.topLabelTextBox1.LabelTextAlignment = System.Drawing.ContentAlignment.BottomLeft;
            this.topLabelTextBox1.Location = new System.Drawing.Point(100, 256);
            this.topLabelTextBox1.Name = "topLabelTextBox1";
            this.topLabelTextBox1.ReadOnly = true;
            this.topLabelTextBox1.Size = new System.Drawing.Size(180, 44);
            this.topLabelTextBox1.TabIndex = 2;
            // 
            // sideLabelTextBox1
            // 
            this.sideLabelTextBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.sideLabelTextBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.sideLabelTextBox1.LabelTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.sideLabelTextBox1.LabelWidth = 100F;
            this.sideLabelTextBox1.LableText = "label1";
            this.sideLabelTextBox1.Location = new System.Drawing.Point(126, 186);
            this.sideLabelTextBox1.Margin = new System.Windows.Forms.Padding(0);
            this.sideLabelTextBox1.Name = "sideLabelTextBox1";
            this.sideLabelTextBox1.Size = new System.Drawing.Size(274, 25);
            this.sideLabelTextBox1.TabIndex = 1;
            this.sideLabelTextBox1.Text = "sideLabelTextBox1";
            // 
            // TestBox1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 437);
            this.Controls.Add(this.topLabelTextBox1);
            this.Controls.Add(this.sideLabelTextBox1);
            this.Controls.Add(this.textBox1);
            this.Name = "TestBox1";
            this.Text = "TestBox1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private SideLabelTextBox sideLabelTextBox1;
        private TopLabelTextBox topLabelTextBox1;
    }
}