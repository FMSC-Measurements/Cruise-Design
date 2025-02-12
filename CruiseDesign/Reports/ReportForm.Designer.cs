namespace CruiseDesign.Reports
{
   partial class ReportForm
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
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportForm));
         this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
         this.toolStrip1 = new System.Windows.Forms.ToolStrip();
         this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
         this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
         this.panel1 = new System.Windows.Forms.Panel();
         this.textBoxTitle = new System.Windows.Forms.TextBox();
         this.label1 = new System.Windows.Forms.Label();
         this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
         this.panel2 = new System.Windows.Forms.Panel();
         this.labelDistrict = new System.Windows.Forms.Label();
         this.labelForest = new System.Windows.Forms.Label();
         this.labelRegion = new System.Windows.Forms.Label();
         this.labelName = new System.Windows.Forms.Label();
         this.labelNumber = new System.Windows.Forms.Label();
         this.panel3 = new System.Windows.Forms.Panel();
         this.labelCost = new System.Windows.Forms.Label();
         this.labelVolume = new System.Windows.Forms.Label();
         this.labelError = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.printDialog1 = new System.Windows.Forms.PrintDialog();
         this.printDocument1 = new System.Drawing.Printing.PrintDocument();
         this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
         this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
         this.labelTime = new System.Windows.Forms.Label();
         this.flowLayoutPanel1.SuspendLayout();
         this.toolStrip1.SuspendLayout();
         this.panel1.SuspendLayout();
         this.tableLayoutPanel1.SuspendLayout();
         this.panel2.SuspendLayout();
         this.panel3.SuspendLayout();
         this.contextMenuStrip1.SuspendLayout();
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
         this.flowLayoutPanel1.TabIndex = 0;
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
         this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
         // 
         // printToolStripButton
         // 
         this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
         this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
         this.printToolStripButton.Name = "printToolStripButton";
         this.printToolStripButton.Size = new System.Drawing.Size(23, 22);
         this.printToolStripButton.Text = "&Print";
         this.printToolStripButton.Click += new System.EventHandler(this.printToolStripButton_Click);
         // 
         // panel1
         // 
         this.panel1.Controls.Add(this.labelTime);
         this.panel1.Controls.Add(this.textBoxTitle);
         this.panel1.Controls.Add(this.label1);
         this.panel1.Location = new System.Drawing.Point(7, 28);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(725, 60);
         this.panel1.TabIndex = 0;
         // 
         // textBoxTitle
         // 
         this.textBoxTitle.BackColor = System.Drawing.Color.White;
         this.textBoxTitle.Dock = System.Windows.Forms.DockStyle.Bottom;
         this.textBoxTitle.Location = new System.Drawing.Point(0, 38);
         this.textBoxTitle.Name = "textBoxTitle";
         this.textBoxTitle.Size = new System.Drawing.Size(725, 22);
         this.textBoxTitle.TabIndex = 1;
         this.textBoxTitle.Text = "Type to Add Description";
         this.textBoxTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.label1.Location = new System.Drawing.Point(255, 6);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(189, 24);
         this.label1.TabIndex = 0;
         this.label1.Text = "Cruise Design Report";
         // 
         // tableLayoutPanel1
         // 
         this.tableLayoutPanel1.BackColor = System.Drawing.Color.AliceBlue;
         this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
         this.tableLayoutPanel1.ColumnCount = 2;
         this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
         this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
         this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
         this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
         this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 94);
         this.tableLayoutPanel1.Name = "tableLayoutPanel1";
         this.tableLayoutPanel1.RowCount = 1;
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
         this.tableLayoutPanel1.Size = new System.Drawing.Size(725, 150);
         this.tableLayoutPanel1.TabIndex = 1;
         // 
         // panel2
         // 
         this.panel2.BackColor = System.Drawing.Color.GhostWhite;
         this.panel2.Controls.Add(this.labelDistrict);
         this.panel2.Controls.Add(this.labelForest);
         this.panel2.Controls.Add(this.labelRegion);
         this.panel2.Controls.Add(this.labelName);
         this.panel2.Controls.Add(this.labelNumber);
         this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel2.Location = new System.Drawing.Point(4, 4);
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(355, 142);
         this.panel2.TabIndex = 0;
         // 
         // labelDistrict
         // 
         this.labelDistrict.AutoSize = true;
         this.labelDistrict.Location = new System.Drawing.Point(5, 117);
         this.labelDistrict.Name = "labelDistrict";
         this.labelDistrict.Size = new System.Drawing.Size(38, 15);
         this.labelDistrict.TabIndex = 4;
         this.labelDistrict.Text = "label6";
         // 
         // labelForest
         // 
         this.labelForest.AutoSize = true;
         this.labelForest.Location = new System.Drawing.Point(5, 91);
         this.labelForest.Name = "labelForest";
         this.labelForest.Size = new System.Drawing.Size(38, 15);
         this.labelForest.TabIndex = 3;
         this.labelForest.Text = "label5";
         // 
         // labelRegion
         // 
         this.labelRegion.AutoSize = true;
         this.labelRegion.Location = new System.Drawing.Point(5, 65);
         this.labelRegion.Name = "labelRegion";
         this.labelRegion.Size = new System.Drawing.Size(38, 15);
         this.labelRegion.TabIndex = 2;
         this.labelRegion.Text = "label4";
         // 
         // labelName
         // 
         this.labelName.AutoSize = true;
         this.labelName.Location = new System.Drawing.Point(5, 38);
         this.labelName.Name = "labelName";
         this.labelName.Size = new System.Drawing.Size(38, 15);
         this.labelName.TabIndex = 1;
         this.labelName.Text = "label3";
         // 
         // labelNumber
         // 
         this.labelNumber.AutoSize = true;
         this.labelNumber.Location = new System.Drawing.Point(5, 12);
         this.labelNumber.Name = "labelNumber";
         this.labelNumber.Size = new System.Drawing.Size(38, 15);
         this.labelNumber.TabIndex = 0;
         this.labelNumber.Text = "label2";
         // 
         // panel3
         // 
         this.panel3.BackColor = System.Drawing.Color.GhostWhite;
         this.panel3.Controls.Add(this.labelCost);
         this.panel3.Controls.Add(this.labelVolume);
         this.panel3.Controls.Add(this.labelError);
         this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel3.Location = new System.Drawing.Point(366, 4);
         this.panel3.Name = "panel3";
         this.panel3.Size = new System.Drawing.Size(355, 142);
         this.panel3.TabIndex = 1;
         // 
         // labelCost
         // 
         this.labelCost.AutoSize = true;
         this.labelCost.Location = new System.Drawing.Point(12, 65);
         this.labelCost.Name = "labelCost";
         this.labelCost.Size = new System.Drawing.Size(38, 15);
         this.labelCost.TabIndex = 2;
         this.labelCost.Text = "label9";
         // 
         // labelVolume
         // 
         this.labelVolume.AutoSize = true;
         this.labelVolume.Location = new System.Drawing.Point(12, 38);
         this.labelVolume.Name = "labelVolume";
         this.labelVolume.Size = new System.Drawing.Size(38, 15);
         this.labelVolume.TabIndex = 1;
         this.labelVolume.Text = "label8";
         // 
         // labelError
         // 
         this.labelError.AutoSize = true;
         this.labelError.Location = new System.Drawing.Point(12, 12);
         this.labelError.Name = "labelError";
         this.labelError.Size = new System.Drawing.Size(38, 15);
         this.labelError.TabIndex = 0;
         this.labelError.Text = "label7";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(7, 247);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(0, 15);
         this.label2.TabIndex = 3;
         // 
         // printDialog1
         // 
         this.printDialog1.UseEXDialog = true;
         // 
         // contextMenuStrip1
         // 
         this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.printToolStripMenuItem});
         this.contextMenuStrip1.Name = "contextMenuStrip1";
         this.contextMenuStrip1.Size = new System.Drawing.Size(91, 48);
         // 
         // saveToolStripMenuItem
         // 
         this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
         this.saveToolStripMenuItem.Size = new System.Drawing.Size(90, 22);
         this.saveToolStripMenuItem.Text = "Save";
         // 
         // printToolStripMenuItem
         // 
         this.printToolStripMenuItem.Name = "printToolStripMenuItem";
         this.printToolStripMenuItem.Size = new System.Drawing.Size(90, 22);
         this.printToolStripMenuItem.Text = "Print";
         this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
         // 
         // saveFileDialog1
         // 
         this.saveFileDialog1.DefaultExt = "xps";
         this.saveFileDialog1.Filter = "Cruise Design Report|*.xps";
         // 
         // labelTime
         // 
         this.labelTime.AutoSize = true;
         this.labelTime.Location = new System.Drawing.Point(625, 13);
         this.labelTime.Name = "labelTime";
         this.labelTime.Size = new System.Drawing.Size(69, 15);
         this.labelTime.TabIndex = 2;
         this.labelTime.Text = "Time Stamp";
         // 
         // ReportForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.AutoScroll = true;
         this.ClientSize = new System.Drawing.Size(777, 662);
         this.ContextMenuStrip = this.contextMenuStrip1;
         this.Controls.Add(this.flowLayoutPanel1);
         this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Margin = new System.Windows.Forms.Padding(4);
         this.MinimumSize = new System.Drawing.Size(793, 38);
         this.Name = "ReportForm";
         this.Text = "Cruise Design Report";
         this.flowLayoutPanel1.ResumeLayout(false);
         this.flowLayoutPanel1.PerformLayout();
         this.toolStrip1.ResumeLayout(false);
         this.toolStrip1.PerformLayout();
         this.panel1.ResumeLayout(false);
         this.panel1.PerformLayout();
         this.tableLayoutPanel1.ResumeLayout(false);
         this.panel2.ResumeLayout(false);
         this.panel2.PerformLayout();
         this.panel3.ResumeLayout(false);
         this.panel3.PerformLayout();
         this.contextMenuStrip1.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
      private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
      private System.Windows.Forms.Panel panel2;
      private System.Windows.Forms.Panel panel3;
      public System.Windows.Forms.Label labelDistrict;
      public System.Windows.Forms.Label labelForest;
      public System.Windows.Forms.Label labelRegion;
      public System.Windows.Forms.Label labelName;
      public System.Windows.Forms.Label labelNumber;
      public System.Windows.Forms.Label labelCost;
      public System.Windows.Forms.Label labelVolume;
      public System.Windows.Forms.Label labelError;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.PrintDialog printDialog1;
      private System.Drawing.Printing.PrintDocument printDocument1;
      private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
      private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
      private System.Windows.Forms.ToolStrip toolStrip1;
      private System.Windows.Forms.ToolStripButton saveToolStripButton;
      private System.Windows.Forms.ToolStripButton printToolStripButton;
      public System.Windows.Forms.TextBox textBoxTitle;
      private System.Windows.Forms.SaveFileDialog saveFileDialog1;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label labelTime;
   }
}