using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CruiseDesign.Strata_setup;


namespace CruiseDesign
{
    partial class StrataSetupWizard
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
           System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StrataSetupWizard));
           this.pageHost1 = new CruiseDesign.Controls.PageHost();
           this.SuspendLayout();
           // 
           // pageHost1
           // 
           this.pageHost1.Dock = System.Windows.Forms.DockStyle.Fill;
           this.pageHost1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.pageHost1.Location = new System.Drawing.Point(0, 0);
           this.pageHost1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
           this.pageHost1.Name = "pageHost1";
           this.pageHost1.Size = new System.Drawing.Size(650, 475);
           this.pageHost1.TabIndex = 0;
           // 
           // StrataSetupWizard
           // 
           this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
           this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           this.ClientSize = new System.Drawing.Size(650, 475);
           this.Controls.Add(this.pageHost1);
           this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
           this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
           this.MinimumSize = new System.Drawing.Size(650, 475);
           this.Name = "StrataSetupWizard";
           this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
           this.Text = "Strata Setup";
           this.ResumeLayout(false);

        }

        #endregion

//        private CruiseDesign.Controls.PageHost pageHost;
        private CruiseDesign.Controls.PageHost pageHost1;

    }
}