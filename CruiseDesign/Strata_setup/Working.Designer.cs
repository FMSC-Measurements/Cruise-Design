﻿namespace CruiseDesign.Strata_setup
{
   partial class Working
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Working));
         this.label1 = new System.Windows.Forms.Label();
         this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
         this.buttonOK = new System.Windows.Forms.Button();
         this.pictureBox1 = new System.Windows.Forms.PictureBox();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
         this.SuspendLayout();
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(40, 38);
         this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(176, 16);
         this.label1.TabIndex = 0;
         this.label1.Text = "Copying Data From Recon...";
         // 
         // backgroundWorker1
         // 
         this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
         this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
         // 
         // buttonOK
         // 
         this.buttonOK.BackColor = System.Drawing.SystemColors.ButtonFace;
         this.buttonOK.Enabled = false;
         this.buttonOK.Location = new System.Drawing.Point(112, 99);
         this.buttonOK.Name = "buttonOK";
         this.buttonOK.Size = new System.Drawing.Size(75, 23);
         this.buttonOK.TabIndex = 1;
         this.buttonOK.Text = "OK";
         this.buttonOK.UseVisualStyleBackColor = false;
         this.buttonOK.Visible = false;
         this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
         // 
         // pictureBox1
         // 
         this.pictureBox1.Image = global::CruiseDesign.Properties.Resources.bouncingProgressBar;
         this.pictureBox1.Location = new System.Drawing.Point(43, 57);
         this.pictureBox1.Name = "pictureBox1";
         this.pictureBox1.Size = new System.Drawing.Size(207, 14);
         this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
         this.pictureBox1.TabIndex = 2;
         this.pictureBox1.TabStop = false;
         // 
         // Working
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.BackColor = System.Drawing.Color.LemonChiffon;
         this.ClientSize = new System.Drawing.Size(291, 134);
         this.Controls.Add(this.pictureBox1);
         this.Controls.Add(this.buttonOK);
         this.Controls.Add(this.label1);
         this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Margin = new System.Windows.Forms.Padding(4);
         this.Name = "Working";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "Working";
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label label1;
      private System.ComponentModel.BackgroundWorker backgroundWorker1;
      private System.Windows.Forms.Button buttonOK;
      private System.Windows.Forms.PictureBox pictureBox1;
   }
}