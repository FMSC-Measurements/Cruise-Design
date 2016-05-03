using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Windows.Documents;
using System.Windows.Markup;

namespace CruiseDesign.Reports
{
   public partial class ReportAdditional : Form
   {
      TableLayoutPanel table2;
      //Bitmap memoryImage;
      int layoutPrintStartHeight, layoutPrintEndHeight, layoutStartWidth, strCnt = 0;
      int[] strHeights;
      int sHts;

      public ReportAdditional(int strNum)
      {
         InitializeComponent();

         strHeights = new int[strNum];
         printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

      }

      public void setSaleInfo(string num, string name)
      {
         labelNumber.Text = "Sale Number:  " + num;
         labelName.Text = "Sale Name:  " + name;
      }
      public void createStratumTable(string Code, string Meth, string units, string sPlots, string sSupp)
      {
         // create label for Stratum number
         Label labelStratum = new Label() { Text = "Stratum:  " + Code + "   Method: " + Meth + "    Units: " + units, Padding = new Padding(3, 3, 0, 3), AutoSize = true };
         labelStratum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.flowLayoutPanel1.Controls.Add(labelStratum);

         if (Meth == "PNT" || Meth == "P3P" || Meth == "PCM" || Meth == "3PPNT" || Meth == "FIX" || Meth == "F3P" || Meth == "FCM")
         {
            Label labelPlots = new Label() { Text = "Current Number of Plots:  " + sPlots + "   Supplimental Plots: +" + sSupp, Padding = new Padding(3, 3, 0, 3), AutoSize = true };
            labelStratum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowLayoutPanel1.Controls.Add(labelPlots);
         
         }
      }
      public void createSgTable(int sgCnt, string meth)
      {
         int depth = 25 * (sgCnt + 1);

         table2 = new TableLayoutPanel() { ColumnCount = 4, RowCount = sgCnt + 1, Size = new Size(725, depth) };
         for (int i = 0; i < sgCnt + 1; i++)
            this.table2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
         Panel panel0 = new Panel() { Dock = DockStyle.Fill };
         Panel panel1 = new Panel() { Dock = DockStyle.Fill };
         Panel panel2 = new Panel() { Dock = DockStyle.Fill };
         Panel panel3 = new Panel() { Dock = DockStyle.Fill };
         table2.Controls.Add(panel0, 0, 0);
         table2.Controls.Add(panel1, 1, 0);
         table2.Controls.Add(panel2, 2, 0);
         table2.Controls.Add(panel3, 3, 0);
         Label labelSg = new Label() { Text = "SampGrp", Padding = new Padding(3, 0, 0, 0), AutoSize = true };
         panel0.Controls.Add(labelSg);
         Label labelSamp = new Label() { Text = "Current Samples", AutoSize = true };
         panel1.Controls.Add(labelSamp);
         Label labelSupp = new Label() { Text = "Supplemental", AutoSize = true };
         panel2.Controls.Add(labelSupp);
         Label labelIns = new Label() { Text = "Insurance Available", AutoSize = true };
         panel3.Controls.Add(labelIns);

         //this.flowLayoutPanel1.Controls.Add(table2);
      }
      public void createAddSgRow(int rowcnt, string sg, string samples, string supp, string insur)
      {
         //table2.RowCount++;
         //int rowcnt = table2.RowCount - 1;
         Panel panel0 = new Panel() { Dock = DockStyle.Fill };
         Panel panel1 = new Panel() { Dock = DockStyle.Fill };
         Panel panel2 = new Panel() { Dock = DockStyle.Fill };
         Panel panel3 = new Panel() { Dock = DockStyle.Fill };
         table2.Controls.Add(panel0, 0, rowcnt);
         table2.Controls.Add(panel1, 1, rowcnt);
         table2.Controls.Add(panel2, 2, rowcnt);
         table2.Controls.Add(panel3, 3, rowcnt);
         Label labelSg = new Label() { Text = sg, Padding = new Padding(3, 0, 0, 0), AutoSize = true };
         Label labelSamp = new Label() { Text = samples, AutoSize = true };
         Label labelSupp = new Label() { Text = supp, AutoSize = true };
         Label labelIns = new Label() { Text = insur, AutoSize = true };
         panel0.Controls.Add(labelSg);
         panel1.Controls.Add(labelSamp);
         panel2.Controls.Add(labelSupp);
         panel3.Controls.Add(labelIns);
      }
      public void AddTable2()
      {
         table2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
         this.flowLayoutPanel1.Controls.Add(table2);
         Label labelSpace = new Label() { Text = "    ", AutoSize = true };
         this.flowLayoutPanel1.Controls.Add(labelSpace);
         strHeights[strCnt] = flowLayoutPanel1.DisplayRectangle.Height;
         strCnt++;

      }

      private void printToolStripMenuItem_Click(object sender, EventArgs e)
      {
         printSetup(sender, e);
      }
      private void printToolStripButton_Click(object sender, EventArgs e)
      {
         printSetup(sender, e);
      }
      private void printSetup(object sender, EventArgs e)
      {
         printDialog1.Document = printDocument1;
         printDocument1.PrintPage += this.printDocument1_PrintPage;

         layoutStartWidth = 0;
         // find page height

         DialogResult result = printDialog1.ShowDialog();

         if (result == DialogResult.OK)
         {
            // determine number of pages to print
            // loop through array, stop when strHeights > 1012
            int startHt = 0;
            int endHt = 0;
            int cnt = 0;

            do
            {
               cnt++;
               endHt = startHt + 1000;
               layoutPrintStartHeight = startHt;

               for (int i = 0; i < strCnt; i++)
               {
                  if (strHeights[i] < endHt)
                     layoutPrintEndHeight = strHeights[i];
               }

               this.Height = (layoutPrintEndHeight - layoutPrintStartHeight) + 25;
               flowLayoutPanel1.AutoScrollPosition = new Point(0, layoutPrintStartHeight);


               printDocument1.Print();

               startHt = layoutPrintEndHeight;

            } while (endHt < flowLayoutPanel1.DisplayRectangle.Height);
         }
      }

      private void printDocument1_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
      {
         //float x = e.MarginBounds.Left;
         //float y = e.MarginBounds.Top;
         //printDocument1.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);
         //printDocument1.PrinterSettings.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);

         //         Bitmap bmp = new Bitmap(flowLayoutPanel1.Width, layoutPrintEndHeight - layoutPrintStartHeight);
         Bitmap bmp = new Bitmap(752, layoutPrintEndHeight - layoutPrintStartHeight);
         flowLayoutPanel1.DrawToBitmap(bmp, new Rectangle(0, 0, 736, layoutPrintEndHeight - layoutPrintStartHeight));
         //         flowLayoutPanel1.DrawToBitmap(bmp, new Rectangle(0, 0, flowLayoutPanel1.Width, layoutPrintEndHeight - layoutPrintStartHeight));
         e.Graphics.DrawImage((Image)bmp, 25, 25);
      }

      private void saveToolStripButton_Click(object sender, EventArgs e)
      {
         MessageBox.Show("Save function not working at this time.");
      }

   }
}
