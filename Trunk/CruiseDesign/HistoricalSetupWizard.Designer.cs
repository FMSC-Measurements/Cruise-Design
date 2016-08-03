using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CruiseDesign.Historical_setup;


namespace CruiseDesign
{
   partial class HistoricalSetupWizard
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistoricalSetupWizard));
         this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
         this.pageHost2 = new CruiseDesign.Controls.PageHost();
         this.SuspendLayout();
         // 
         // openFileDialog1
         // 
         this.openFileDialog1.DefaultExt = "cruise";
         this.openFileDialog1.FileName = "*.cruise";
         this.openFileDialog1.Filter = "Cruise files|*.cruise|All files|*.*";
         // 
         // pageHost2
         // 
         this.pageHost2.Dock = System.Windows.Forms.DockStyle.Fill;
         this.pageHost2.Location = new System.Drawing.Point(0, 0);
         this.pageHost2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
         this.pageHost2.Name = "pageHost2";
         this.pageHost2.Size = new System.Drawing.Size(488, 386);
         this.pageHost2.TabIndex = 0;
         // 
         // HistoricalSetupWizard
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(488, 386);
         this.Controls.Add(this.pageHost2);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
         this.MinimumSize = new System.Drawing.Size(498, 419);
         this.Name = "HistoricalSetupWizard";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "Historical Setup";
         this.ResumeLayout(false);

      }

      #endregion
      private OpenFileDialog openFileDialog1;
      private CruiseDesign.Controls.PageHost pageHost2;
   }
}