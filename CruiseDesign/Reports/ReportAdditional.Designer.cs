namespace CruiseDesign.Reports
{
   partial class ReportAdditional
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportAdditional));
         this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
         this.toolStrip1 = new System.Windows.Forms.ToolStrip();
         this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
         this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
         this.panel1 = new System.Windows.Forms.Panel();
         this.label1 = new System.Windows.Forms.Label();
         this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
         this.panel2 = new System.Windows.Forms.Panel();
         this.labelName = new System.Windows.Forms.Label();
         this.labelNumber = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.printDocument1 = new System.Drawing.Printing.PrintDocument();
         this.printDialog1 = new System.Windows.Forms.PrintDialog();
         this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
         this.flowLayoutPanel1.SuspendLayout();
         this.toolStrip1.SuspendLayout();
         this.panel1.SuspendLayout();
         this.tableLayoutPanel1.SuspendLayout();
         this.panel2.SuspendLayout();
         this.SuspendLayout();
         // 
         // flowLayoutPanel1
         // 
         this.flowLayoutPanel1.AutoScroll = true;
         this.flowLayoutPanel1.AutoSize = true;
         this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;
         this.flowLayoutPanel1.Controls.Add(this.toolStrip1);
         this.flowLayoutPanel1.Controls.Add(this.panel1);
         this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel1);
         this.flowLayoutPanel1.Controls.Add(this.label2);
         this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
         this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
         this.flowLayoutPanel1.Name = "flowLayoutPanel1";
         this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
         this.flowLayoutPanel1.Size = new System.Drawing.Size(777, 662);
         this.flowLayoutPanel1.TabIndex = 1;
         this.flowLayoutPanel1.WrapContents = false;
         // 
         // toolStrip1
         // 
         this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripButton,
            this.printToolStripButton});
         this.toolStrip1.Location = new System.Drawing.Point(4, 0);
         this.toolStrip1.Name = "toolStrip1";
         this.toolStrip1.Size = new System.Drawing.Size(731, 25);
         this.toolStrip1.TabIndex = 2;
         this.toolStrip1.Text = "toolStrip1";
         // 
         // saveToolStripButton
         // 
         this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
         this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.saveToolStripButton.Name = "saveToolStripButton";
         this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
         this.saveToolStripButton.Text = "&Save";
         // 
         // printToolStripButton
         // 
         this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
         this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.printToolStripButton.Name = "printToolStripButton";
         this.printToolStripButton.Size = new System.Drawing.Size(23, 22);
         this.printToolStripButton.Text = "&Print";
         // 
         // panel1
         // 
         this.panel1.Controls.Add(this.label1);
         this.panel1.Location = new System.Drawing.Point(7, 28);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(725, 37);
         this.panel1.TabIndex = 0;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.label1.Location = new System.Drawing.Point(255, 6);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(265, 24);
         this.label1.TabIndex = 0;
         this.label1.Text = "Supplemental Samples Report";
         // 
         // tableLayoutPanel1
         // 
         this.tableLayoutPanel1.BackColor = System.Drawing.Color.AliceBlue;
         this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
         this.tableLayoutPanel1.ColumnCount = 1;
         this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
         this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
         this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
         this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 71);
         this.tableLayoutPanel1.Name = "tableLayoutPanel1";
         this.tableLayoutPanel1.RowCount = 1;
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 71F));
         this.tableLayoutPanel1.Size = new System.Drawing.Size(725, 72);
         this.tableLayoutPanel1.TabIndex = 1;
         // 
         // panel2
         // 
         this.panel2.BackColor = System.Drawing.Color.GhostWhite;
         this.panel2.Controls.Add(this.labelName);
         this.panel2.Controls.Add(this.labelNumber);
         this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel2.Location = new System.Drawing.Point(4, 4);
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(717, 64);
         this.panel2.TabIndex = 0;
         // 
         // labelName
         // 
         this.labelName.AutoSize = true;
         this.labelName.Location = new System.Drawing.Point(5, 38);
         this.labelName.Name = "labelName";
         this.labelName.Size = new System.Drawing.Size(45, 16);
         this.labelName.TabIndex = 1;
         this.labelName.Text = "label3";
         // 
         // labelNumber
         // 
         this.labelNumber.AutoSize = true;
         this.labelNumber.Location = new System.Drawing.Point(5, 12);
         this.labelNumber.Name = "labelNumber";
         this.labelNumber.Size = new System.Drawing.Size(45, 16);
         this.labelNumber.TabIndex = 0;
         this.labelNumber.Text = "label2";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(7, 146);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(0, 16);
         this.label2.TabIndex = 3;
         // 
         // printDialog1
         // 
         this.printDialog1.UseEXDialog = true;
         // 
         // ReportAdditional
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(777, 662);
         this.Controls.Add(this.flowLayoutPanel1);
         this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Margin = new System.Windows.Forms.Padding(4);
         this.MinimumSize = new System.Drawing.Size(793, 38);
         this.Name = "ReportAdditional";
         this.Text = "Supplimental Samples Report";
         this.flowLayoutPanel1.ResumeLayout(false);
         this.flowLayoutPanel1.PerformLayout();
         this.toolStrip1.ResumeLayout(false);
         this.toolStrip1.PerformLayout();
         this.panel1.ResumeLayout(false);
         this.panel1.PerformLayout();
         this.tableLayoutPanel1.ResumeLayout(false);
         this.panel2.ResumeLayout(false);
         this.panel2.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
      private System.Windows.Forms.ToolStrip toolStrip1;
      private System.Windows.Forms.ToolStripButton saveToolStripButton;
      private System.Windows.Forms.ToolStripButton printToolStripButton;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
      private System.Windows.Forms.Panel panel2;
      public System.Windows.Forms.Label labelName;
      public System.Windows.Forms.Label labelNumber;
      private System.Drawing.Printing.PrintDocument printDocument1;
      private System.Windows.Forms.PrintDialog printDialog1;
      private System.Windows.Forms.SaveFileDialog saveFileDialog1;
   }
}