﻿namespace CruiseDesign.Reports
{
   partial class ReportViewer
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportViewer));
         this.menuStrip1 = new System.Windows.Forms.MenuStrip();
         this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.richTextBox1 = new System.Windows.Forms.RichTextBox();
         this.printDialog1 = new System.Windows.Forms.PrintDialog();
         this.printDocument1 = new System.Drawing.Printing.PrintDocument();
         this.menuStrip1.SuspendLayout();
         this.SuspendLayout();
         // 
         // menuStrip1
         // 
         this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
         this.menuStrip1.Location = new System.Drawing.Point(0, 0);
         this.menuStrip1.Name = "menuStrip1";
         this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
         this.menuStrip1.Size = new System.Drawing.Size(884, 24);
         this.menuStrip1.TabIndex = 0;
         this.menuStrip1.Text = "menuStrip1";
         // 
         // fileToolStripMenuItem
         // 
         this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.printToolStripMenuItem});
         this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
         this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
         this.fileToolStripMenuItem.Text = "File";
         // 
         // saveToolStripMenuItem
         // 
         this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
         this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
         this.saveToolStripMenuItem.Text = "Save";
         // 
         // printToolStripMenuItem
         // 
         this.printToolStripMenuItem.Name = "printToolStripMenuItem";
         this.printToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
         this.printToolStripMenuItem.Text = "Print";
         // 
         // richTextBox1
         // 
         this.richTextBox1.AcceptsTab = true;
         this.richTextBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
         this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
         this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.richTextBox1.Location = new System.Drawing.Point(0, 24);
         this.richTextBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
         this.richTextBox1.Name = "richTextBox1";
         this.richTextBox1.ReadOnly = true;
         this.richTextBox1.Size = new System.Drawing.Size(884, 683);
         this.richTextBox1.TabIndex = 1;
         this.richTextBox1.Text = "";
         // 
         // printDialog1
         // 
         this.printDialog1.UseEXDialog = true;
         // 
         // ReportViewer
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(884, 707);
         this.Controls.Add(this.richTextBox1);
         this.Controls.Add(this.menuStrip1);
         this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MainMenuStrip = this.menuStrip1;
         this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
         this.Name = "ReportViewer";
         this.Text = "ReportViewer";
         this.menuStrip1.ResumeLayout(false);
         this.menuStrip1.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.MenuStrip menuStrip1;
      private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
      private System.Windows.Forms.RichTextBox richTextBox1;
      private System.Windows.Forms.PrintDialog printDialog1;
      private System.Drawing.Printing.PrintDocument printDocument1;
   }
}