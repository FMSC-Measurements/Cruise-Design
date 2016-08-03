using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Printing;
using System.Windows.Forms;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Windows.Documents;
using System.Windows.Markup;

namespace CruiseDesign.Reports
{
   public partial class ReportForm : Form
   {
      TableLayoutPanel table2;
      //Bitmap memoryImage;
      int layoutPrintStartHeight, layoutPrintEndHeight, layoutStartWidth, strCnt = 0;
      int[] strHeights;

      public ReportForm(int strNum)
      {
         InitializeComponent();
         
         strHeights = new int[strNum];
         printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
      }
      public void setSaleInfo(string num, string name, string reg, string forst, string dist, string err, string cost,string vol,string uom)
      {
         labelNumber.Text =   "Sale Number:  " + num;
         labelName.Text =     "Sale Name:  " + name;
         labelRegion.Text =   "Region:  " + reg;
         labelForest.Text =   "Forest:  " + forst;
         labelDistrict.Text = "District:  " + dist;
         labelError.Text =  "Expected Sale Error:  " + err;
         labelVolume.Text = "Expected Sale Volume:  " + vol;
         if (uom == "01")
         {
            labelVolume.Text += "  MBF";
         }
         else if (uom == "03")
         {
            labelVolume.Text += "  CCF";
         }
         labelCost.Text = "Estimated Cruise Cost:  " + cost;
      }
      public void createStratumTable(string Code, string Meth, string Err, string TotVol, string Desc, string baf, string fps, string PlotSpacing, string totAcres, string units)
      {
         Label labelPlot;
         Label labelSpacing;
         // create label for Stratum number
         Label labelStratum = new Label() { Text = "Stratum:  " + Code, Padding = new Padding(3,0,0,0),AutoSize = true};
         labelStratum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.flowLayoutPanel1.Controls.Add(labelStratum);
         // create label for Units
         Label labelUnits = new Label() { Text = "Units: " + units, Padding = new Padding(3, 0, 0, 0), AutoSize = true };
         this.flowLayoutPanel1.Controls.Add(labelUnits);
         // create table
         TableLayoutPanel table1 = new TableLayoutPanel() { ColumnCount = 2, RowCount = 1, Size = new Size(770, 160), BackColor = Color.WhiteSmoke };
         table1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
         table1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
         table1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

         //table1.ColumnCount = 2;
         //table1.RowCount = 1;
         //table1.Size = new Size(982, 175);
         //table1.BackColor = Color.Cornsilk;
         // create panels
         Panel panel1 = new Panel(){ Dock = DockStyle.Fill };
         Panel panel2 = new Panel(){ Dock = DockStyle.Fill };
         table1.Controls.Add(panel1, 0, 0);
         table1.Controls.Add(panel2, 1, 0);
         // create labels
         Label labelStr = new Label() { Location = new Point(6, 13), Text = "Stratum:  " + Code, AutoSize = true };
         Label labelMeth = new Label() { Location = new Point(6, 41), Text = "Method:  " + Meth, AutoSize = true };
         Label labelErr = new Label() { Location = new Point(6, 69), Text = "Expected Error:  " + Err, AutoSize = true };
         Label labelVol = new Label() { Location = new Point(6, 97), Text = "Expected Volume:  " + TotVol, AutoSize = true };
         Label labelDesc = new Label() { Location = new Point(14, 13), Text = "Description:  " + Desc, AutoSize = true };
         Label labelAcres = new Label() { Location = new Point(14, 41), Text = "Stratum Acres:  " + totAcres, AutoSize = true };
         switch (Meth)
         {
            case "PNT":
            case "P3P":
            case "PCM":
            case "3PPNT":
               labelPlot = new Label() { Location = new Point(14, 69), Text = "Basal Area Factor:  " + baf, AutoSize = true };
               labelSpacing = new Label() { Location = new Point(14, 97), Text = "Plot Spacing:  " + PlotSpacing, AutoSize = true };
               break;
            case "FIX":
            case "F3P":
            case "FCM":
            case "FIXCNT":
               labelPlot = new Label() { Location = new Point(14, 69), Text = "Fixed Plot Size:  " + fps, AutoSize = true };
               labelSpacing = new Label() { Location = new Point(14, 97), Text = "Plot Spacing:  " + PlotSpacing, AutoSize = true };
               break;
            default:
               labelPlot = new Label() { Location = new Point(14, 69), Text = "  ", AutoSize = true };
               labelSpacing = new Label() { Location = new Point(14, 97), Text = " ", AutoSize = true };
               break;
         }

         panel1.Controls.Add(labelStr);
         panel1.Controls.Add(labelMeth);
         panel1.Controls.Add(labelErr);
         panel1.Controls.Add(labelVol);
         panel2.Controls.Add(labelDesc);
         panel2.Controls.Add(labelPlot);
         panel2.Controls.Add(labelSpacing);
         panel2.Controls.Add(labelAcres);

         this.flowLayoutPanel1.Controls.Add(table1);
      }

      public void createSgTable_100(int sgCnt, string meth)
      {
         string plots = "Plots";
         if (meth == "100") plots = "Trees";
         int depth = 25 * (sgCnt + 1);

         table2 = new TableLayoutPanel() { ColumnCount = 7, RowCount = sgCnt + 1, Size = new Size(770, depth), BackColor = Color.WhiteSmoke};
         for (int i = 0; i < sgCnt + 1; i++)
            this.table2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 65));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 85));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

         table2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
         Panel panel0 = new Panel() { Dock = DockStyle.Fill };
         Panel panel1 = new Panel() { Dock = DockStyle.Fill };
         Panel panel2 = new Panel() { Dock = DockStyle.Fill };
         Panel panel3 = new Panel() { Dock = DockStyle.Fill };
         Panel panel4 = new Panel() { Dock = DockStyle.Fill };
         Panel panel5 = new Panel() { Dock = DockStyle.Fill };
         Panel panel6 = new Panel() { Dock = DockStyle.Fill };
         table2.Controls.Add(panel0, 0, 0);
         table2.Controls.Add(panel1, 1, 0);
         table2.Controls.Add(panel2, 2, 0);
         table2.Controls.Add(panel3, 3, 0);
         table2.Controls.Add(panel4, 4, 0);
         table2.Controls.Add(panel5, 5, 0);
         table2.Controls.Add(panel6, 6, 0);

         Label labelSg = new Label() { Text = "SampGrp", Padding = new Padding(3, 0, 0, 0), AutoSize = true };
         panel0.Controls.Add(labelSg);
         Label labelerr = new Label() { Text = "Sg Err", AutoSize = true };
         panel1.Controls.Add(labelerr);
         Label labelplot = new Label() { Text = plots, AutoSize = true };
         panel2.Controls.Add(labelplot);
         Label labelcv = new Label() { Text = "CV ", AutoSize = true };
         panel3.Controls.Add(labelcv);
         Label labeltpa = new Label() { Text = "Trees/acre", AutoSize = true };
         panel4.Controls.Add(labeltpa);
         Label labelvpa = new Label() { Text = "Vol/acre", AutoSize = true };
         panel5.Controls.Add(labelvpa);
         Label labeldesc = new Label() { Text = "Description", AutoSize = true };
         panel6.Controls.Add(labeldesc);
         //this.flowLayoutPanel1.Controls.Add(table2);
      }

      public void createSgTable_str(int sgCnt, string meth)
      {
         string freq = "Freq";
         if (meth == "3P") freq = "KZ ";
         int depth = 25 * (sgCnt+1);

         table2 = new TableLayoutPanel() { ColumnCount = 9, RowCount = sgCnt + 1, Size = new Size(770, depth), BackColor = Color.WhiteSmoke };
         for (int i = 0; i < sgCnt + 1; i++)
            this.table2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 65));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 85));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
         Panel panel0 = new Panel() { Dock = DockStyle.Fill };
         Panel panel1 = new Panel() { Dock = DockStyle.Fill };
         Panel panel2 = new Panel() { Dock = DockStyle.Fill };
         Panel panel3 = new Panel() { Dock = DockStyle.Fill };
         Panel panel4 = new Panel() { Dock = DockStyle.Fill };
         Panel panel5 = new Panel() { Dock = DockStyle.Fill };
         Panel panel6 = new Panel() { Dock = DockStyle.Fill };
         Panel panel7 = new Panel() { Dock = DockStyle.Fill };
         Panel panel8 = new Panel() { Dock = DockStyle.Fill };
         table2.Controls.Add(panel0, 0, 0);
         table2.Controls.Add(panel1, 1, 0);
         table2.Controls.Add(panel2, 2, 0);
         table2.Controls.Add(panel3, 3, 0);
         table2.Controls.Add(panel4, 4, 0);
         table2.Controls.Add(panel5, 5, 0);
         table2.Controls.Add(panel6, 6, 0);
         table2.Controls.Add(panel7, 7, 0);
         table2.Controls.Add(panel8, 8, 0);
         Label labelSg = new Label() { Text = "SampGrp", Padding = new Padding(3, 0, 0, 0), AutoSize = true };
         panel0.Controls.Add(labelSg);
         Label labelerr = new Label() { Text = "Sg Err", AutoSize = true };
         panel1.Controls.Add(labelerr);
         Label labelplot = new Label() { Text = "Trees", AutoSize = true };
         panel2.Controls.Add(labelplot);
         Label labeltree = new Label() { Text = freq, AutoSize = true };
         panel3.Controls.Add(labeltree);
         Label labelfreq = new Label() { Text = "Insur", AutoSize = true };
         panel4.Controls.Add(labelfreq);
         Label labelins = new Label() { Text = "CV", AutoSize = true };
         panel5.Controls.Add(labelins);
         Label labeltpa = new Label() { Text = "Trees/acre", AutoSize = true };
         panel6.Controls.Add(labeltpa);
         Label labelvpa = new Label() { Text = "Vol/acre", AutoSize = true };
         panel7.Controls.Add(labelvpa);
         Label labeldesc = new Label() { Text = "Description", AutoSize = true };
         panel8.Controls.Add(labeldesc);

         //this.flowLayoutPanel1.Controls.Add(table2);

      }
      public void createSgTable_s3p(int sgCnt)
      {
         int depth = 25 * (sgCnt + 1);

         table2 = new TableLayoutPanel() { ColumnCount = 12, RowCount = sgCnt + 1, Size = new Size(770, depth), BackColor = Color.WhiteSmoke };
         for (int i = 0; i < sgCnt + 1; i++)
            this.table2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 55));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 85));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
         table2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
         Panel panel0 = new Panel() { Dock = DockStyle.Fill };
         Panel panel1 = new Panel() { Dock = DockStyle.Fill };
         Panel panel2 = new Panel() { Dock = DockStyle.Fill };
         Panel panel3 = new Panel() { Dock = DockStyle.Fill };
         Panel panel4 = new Panel() { Dock = DockStyle.Fill };
         Panel panel5 = new Panel() { Dock = DockStyle.Fill };
         Panel panel6 = new Panel() { Dock = DockStyle.Fill };
         Panel panel7 = new Panel() { Dock = DockStyle.Fill };
         Panel panel8 = new Panel() { Dock = DockStyle.Fill };
         Panel panel9 = new Panel() { Dock = DockStyle.Fill };
         Panel panel10 = new Panel() { Dock = DockStyle.Fill };
         Panel panel11 = new Panel() { Dock = DockStyle.Fill };
         table2.Controls.Add(panel0, 0, 0);
         table2.Controls.Add(panel1, 1, 0);
         table2.Controls.Add(panel2, 2, 0);
         table2.Controls.Add(panel3, 3, 0);
         table2.Controls.Add(panel4, 4, 0);
         table2.Controls.Add(panel5, 5, 0);
         table2.Controls.Add(panel6, 6, 0);
         table2.Controls.Add(panel7, 7, 0);
         table2.Controls.Add(panel8, 8, 0);
         table2.Controls.Add(panel9, 9, 0);
         table2.Controls.Add(panel10, 10, 0);
         table2.Controls.Add(panel11, 11, 0);
         Label labelSg = new Label() { Text = "SampGrp", Padding = new Padding(3, 0, 0, 0), AutoSize = true };
         panel0.Controls.Add(labelSg);
         Label labelerr = new Label() { Text = "Sg Err", AutoSize = true };
         panel1.Controls.Add(labelerr);
         Label labelplot = new Label() { Text = "3PTree", AutoSize = true };
         panel2.Controls.Add(labelplot);
         Label labeltree = new Label() { Text = "Trees", AutoSize = true };
         panel3.Controls.Add(labeltree);
         Label labelfreq = new Label() { Text = "Freq", AutoSize = true };
         panel4.Controls.Add(labelfreq);
         Label labelkz = new Label() { Text = "KZ", AutoSize = true };
         panel5.Controls.Add(labelkz);
         Label labelins = new Label() { Text = "Insur", AutoSize = true };
         panel6.Controls.Add(labelins);
         Label labelcv = new Label() { Text = "CV 1 ", AutoSize = true };
         panel7.Controls.Add(labelcv);
         Label labelcv2 = new Label() { Text = "CV 2 ", AutoSize = true };
         panel8.Controls.Add(labelcv2);
         Label labeltpa = new Label() { Text = "Trees/acre", AutoSize = true };
         panel9.Controls.Add(labeltpa);
         Label labelvpa = new Label() { Text = "Vol/acre", AutoSize = true };
         panel10.Controls.Add(labelvpa);
         Label labeldesc = new Label() { Text = "Description", AutoSize = true };
         panel11.Controls.Add(labeldesc);

         //this.flowLayoutPanel1.Controls.Add(table2);

      }
      public void createSgTable_pcm(int sgCnt, string meth)
      {
         string plot = "Plots";
         string trees = "Trees";
         string freq = "Frequency";
         if (meth == "3PPNT")
         {
            plot = "3P Plots";
            trees = "Plots";
            freq = "KZ";
         }
         else if (meth == "F3P" || meth == "P3P")
         {
            freq = "KZ";
         }
         int depth = 25 * (sgCnt + 1);

         table2 = new TableLayoutPanel() { ColumnCount = 10, RowCount = sgCnt + 1, Size = new Size(770, depth), BackColor = Color.WhiteSmoke };
         for (int i = 0; i < sgCnt + 1; i++)
            this.table2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 65));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 85));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 85));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 85));
         table2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
         table2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
         Panel panel0 = new Panel() { Dock = DockStyle.Fill };
         Panel panel1 = new Panel() { Dock = DockStyle.Fill };
         Panel panel2 = new Panel() { Dock = DockStyle.Fill };
         Panel panel3 = new Panel() { Dock = DockStyle.Fill };
         Panel panel4 = new Panel() { Dock = DockStyle.Fill };
         Panel panel5 = new Panel() { Dock = DockStyle.Fill };
         Panel panel6 = new Panel() { Dock = DockStyle.Fill };
         Panel panel7 = new Panel() { Dock = DockStyle.Fill };
         Panel panel8 = new Panel() { Dock = DockStyle.Fill };
         Panel panel9 = new Panel() { Dock = DockStyle.Fill };
         table2.Controls.Add(panel0, 0, 0);
         table2.Controls.Add(panel1, 1, 0);
         table2.Controls.Add(panel2, 2, 0);
         table2.Controls.Add(panel3, 3, 0);
         table2.Controls.Add(panel4, 4, 0);
         table2.Controls.Add(panel5, 5, 0);
         table2.Controls.Add(panel6, 6, 0);
         table2.Controls.Add(panel7, 7, 0);
         table2.Controls.Add(panel8, 8, 0);
         table2.Controls.Add(panel9, 9, 0);
         Label labelSg = new Label() { Text = "SampGrp", Padding = new Padding(3, 0, 0, 0), AutoSize = true };
         panel0.Controls.Add(labelSg);
         Label labelerr = new Label() { Text = "Sg Err", AutoSize = true };
         panel1.Controls.Add(labelerr);
         Label labelplot = new Label() { Text = plot, AutoSize = true };
         panel2.Controls.Add(labelplot);
         Label labeltree = new Label() { Text = trees, AutoSize = true };
         panel3.Controls.Add(labeltree);
         Label labelfreq = new Label() { Text = freq, AutoSize = true };
         panel4.Controls.Add(labelfreq);
         Label labelins = new Label() { Text = "CV 1 Stg", AutoSize = true };
         panel5.Controls.Add(labelins);
         Label labelcv = new Label() { Text = "CV 2 Stg", AutoSize = true };
         panel6.Controls.Add(labelcv);
         Label labeltpa = new Label() { Text = "Trees/acre", AutoSize = true };
         panel7.Controls.Add(labeltpa);
         Label labelvpa = new Label() { Text = "Vol/acre", AutoSize = true };
         panel8.Controls.Add(labelvpa);
         Label labeldesc = new Label() { Text = "Description", AutoSize = true };
         panel9.Controls.Add(labeldesc);


      }
      public void createAddSgRow_100(int rowcnt, string sg, string err, string tree, string cv, string tpa, string vpa, string desc)
      {
         //table2.RowCount++;
         //int rowcnt = table2.RowCount - 1;

         Panel panel0 = new Panel() { Dock = DockStyle.Fill };
         Panel panel1 = new Panel() { Dock = DockStyle.Fill };
         Panel panel2 = new Panel() { Dock = DockStyle.Fill };
         Panel panel3 = new Panel() { Dock = DockStyle.Fill };
         Panel panel4 = new Panel() { Dock = DockStyle.Fill };
         Panel panel5 = new Panel() { Dock = DockStyle.Fill };
         Panel panel6 = new Panel() { Dock = DockStyle.Fill };
         
         table2.Controls.Add(panel0, 0, rowcnt);
         table2.Controls.Add(panel1, 1, rowcnt);
         table2.Controls.Add(panel2, 2, rowcnt);
         table2.Controls.Add(panel3, 3, rowcnt);
         table2.Controls.Add(panel4, 4, rowcnt);
         table2.Controls.Add(panel5, 5, rowcnt);
         table2.Controls.Add(panel6, 6, rowcnt);

         Label labelSg = new Label() { Text = sg, AutoSize = true };
         panel0.Controls.Add(labelSg);
         Label labelerr = new Label() { Text = err, AutoSize = true };
         panel1.Controls.Add(labelerr);
         Label labelplot = new Label() { Text = tree, AutoSize = true };
         panel2.Controls.Add(labelplot);
         Label labelcv = new Label() { Text = cv, AutoSize = true };
         panel3.Controls.Add(labelcv);
         Label labeltpa = new Label() { Text = tpa, AutoSize = true };
         panel4.Controls.Add(labeltpa);
         Label labelvpa = new Label() { Text = vpa, AutoSize = true };
         panel5.Controls.Add(labelvpa);
         Label labeldesc = new Label() { Text = desc, AutoSize = true };
         panel6.Controls.Add(labeldesc);
      
      }
      public void createAddSgRow_str(int rowcnt, string sg, string err, string tree, string freq, string insur, string cv, string tpa, string vpa, string desc)
      {
         //table2.RowCount++;
         //int rowcnt = table2.RowCount - 1;
         Panel panel0 = new Panel() { Dock = DockStyle.Fill };
         Panel panel1 = new Panel() { Dock = DockStyle.Fill };
         Panel panel2 = new Panel() { Dock = DockStyle.Fill };
         Panel panel3 = new Panel() { Dock = DockStyle.Fill };
         Panel panel4 = new Panel() { Dock = DockStyle.Fill };
         Panel panel5 = new Panel() { Dock = DockStyle.Fill };
         Panel panel6 = new Panel() { Dock = DockStyle.Fill };
         Panel panel7 = new Panel() { Dock = DockStyle.Fill };
         Panel panel8 = new Panel() { Dock = DockStyle.Fill };
         table2.Controls.Add(panel0, 0, rowcnt);
         table2.Controls.Add(panel1, 1, rowcnt);
         table2.Controls.Add(panel2, 2, rowcnt);
         table2.Controls.Add(panel3, 3, rowcnt);
         table2.Controls.Add(panel4, 4, rowcnt);
         table2.Controls.Add(panel5, 5, rowcnt);
         table2.Controls.Add(panel6, 6, rowcnt);
         table2.Controls.Add(panel7, 7, rowcnt);
         table2.Controls.Add(panel8, 8, rowcnt);
         Label labelSg = new Label() { Text = sg, Padding = new Padding(3, 0, 0, 0), AutoSize = true };
         Label labelerr = new Label() { Text = err, AutoSize = true };
         Label labelplot = new Label() { Text = tree, AutoSize = true };
         Label labeltree = new Label() { Text = freq, AutoSize = true };
         Label labelfreq = new Label() { Text = insur, AutoSize = true };
         Label labelins = new Label() { Text = cv, AutoSize = true };
         Label labeltpa = new Label() { Text = tpa, AutoSize = true };
         Label labelvpa = new Label() { Text = vpa, AutoSize = true };
         Label labeldesc = new Label() { Text = desc, AutoSize = true };
         panel0.Controls.Add(labelSg);
         panel1.Controls.Add(labelerr);
         panel2.Controls.Add(labelplot);
         panel3.Controls.Add(labeltree);
         panel4.Controls.Add(labelfreq);
         panel5.Controls.Add(labelins);
         panel6.Controls.Add(labeltpa);
         panel7.Controls.Add(labelvpa);
         panel8.Controls.Add(labeldesc);
      }
      public void createAddSgRow_s3p(int rowcnt, string sg, string err, string tree, string tree2, string freq, string kz, string insur, string cv, string cv2, string tpa, string vpa, string desc)
      {
         //table2.RowCount++;
         //int rowcnt = table2.RowCount - 1;
         Panel panel0 = new Panel() { Dock = DockStyle.Fill };
         Panel panel1 = new Panel() { Dock = DockStyle.Fill };
         Panel panel2 = new Panel() { Dock = DockStyle.Fill };
         Panel panel3 = new Panel() { Dock = DockStyle.Fill };
         Panel panel4 = new Panel() { Dock = DockStyle.Fill };
         Panel panel5 = new Panel() { Dock = DockStyle.Fill };
         Panel panel6 = new Panel() { Dock = DockStyle.Fill };
         Panel panel7 = new Panel() { Dock = DockStyle.Fill };
         Panel panel8 = new Panel() { Dock = DockStyle.Fill };
         Panel panel9 = new Panel() { Dock = DockStyle.Fill };
         Panel panel10 = new Panel() { Dock = DockStyle.Fill };
         Panel panel11 = new Panel() { Dock = DockStyle.Fill };
         table2.Controls.Add(panel0, 0, rowcnt);
         table2.Controls.Add(panel1, 1, rowcnt);
         table2.Controls.Add(panel2, 2, rowcnt);
         table2.Controls.Add(panel3, 3, rowcnt);
         table2.Controls.Add(panel4, 4, rowcnt);
         table2.Controls.Add(panel5, 5, rowcnt);
         table2.Controls.Add(panel6, 6, rowcnt);
         table2.Controls.Add(panel7, 7, rowcnt);
         table2.Controls.Add(panel8, 8, rowcnt);
         table2.Controls.Add(panel9, 9, rowcnt);
         table2.Controls.Add(panel10, 10, rowcnt);
         table2.Controls.Add(panel11, 11, rowcnt);
         Label labelSg = new Label() { Text = sg, Padding = new Padding(3, 0, 0, 0), AutoSize = true };
         panel0.Controls.Add(labelSg);
         Label labelerr = new Label() { Text = err, AutoSize = true };
         panel1.Controls.Add(labelerr);
         Label labelplot = new Label() { Text = tree, AutoSize = true };
         panel2.Controls.Add(labelplot);
         Label labeltree = new Label() { Text = tree2, AutoSize = true };
         panel3.Controls.Add(labeltree);
         Label labelfreq = new Label() { Text = freq, AutoSize = true };
         panel4.Controls.Add(labelfreq);
         Label labelkz = new Label() { Text = kz, AutoSize = true };
         panel5.Controls.Add(labelkz);
         Label labelins = new Label() { Text = insur, AutoSize = true };
         panel6.Controls.Add(labelins);
         Label labelcv = new Label() { Text = cv, AutoSize = true };
         panel7.Controls.Add(labelcv);
         Label labelcv2 = new Label() { Text = cv2, AutoSize = true };
         panel8.Controls.Add(labelcv2);
         Label labeltpa = new Label() { Text = tpa, AutoSize = true };
         panel9.Controls.Add(labeltpa);
         Label labelvpa = new Label() { Text = vpa, AutoSize = true };
         panel10.Controls.Add(labelvpa);
         Label labeldesc = new Label() { Text = desc, AutoSize = true };
         panel11.Controls.Add(labeldesc);
      }
      public void createAddSgRow_pcm(int rowcnt,string sg, string err, string plot, string tree, string freq, string cv, string cv2, string tpa, string vpa, string desc)
      {
         //table2.RowCount++;
         //int rowcnt = table2.RowCount - 1;
         Panel panel0 = new Panel() { Dock = DockStyle.Fill };
         Panel panel1 = new Panel() { Dock = DockStyle.Fill };
         Panel panel2 = new Panel() { Dock = DockStyle.Fill };
         Panel panel3 = new Panel() { Dock = DockStyle.Fill };
         Panel panel4 = new Panel() { Dock = DockStyle.Fill };
         Panel panel5 = new Panel() { Dock = DockStyle.Fill };
         Panel panel6 = new Panel() { Dock = DockStyle.Fill };
         Panel panel7 = new Panel() { Dock = DockStyle.Fill };
         Panel panel8 = new Panel() { Dock = DockStyle.Fill };
         Panel panel9 = new Panel() { Dock = DockStyle.Fill };
         table2.Controls.Add(panel0, 0, rowcnt);
         table2.Controls.Add(panel1, 1, rowcnt);
         table2.Controls.Add(panel2, 2, rowcnt);
         table2.Controls.Add(panel3, 3, rowcnt);
         table2.Controls.Add(panel4, 4, rowcnt);
         table2.Controls.Add(panel5, 5, rowcnt);
         table2.Controls.Add(panel6, 6, rowcnt);
         table2.Controls.Add(panel7, 7, rowcnt);
         table2.Controls.Add(panel8, 8, rowcnt);
         table2.Controls.Add(panel9, 9, rowcnt);
         Label labelSg = new Label() { Text = sg, Padding = new Padding(3, 0, 0, 0), AutoSize = true };
         panel0.Controls.Add(labelSg);
         Label labelerr = new Label() { Text = err, AutoSize = true };
         panel1.Controls.Add(labelerr);
         Label labelplot = new Label() { Text = plot, AutoSize = true };
         panel2.Controls.Add(labelplot);
         Label labeltree = new Label() { Text = tree, AutoSize = true };
         panel3.Controls.Add(labeltree);
         Label labelfreq = new Label() { Text = freq, AutoSize = true };
         panel4.Controls.Add(labelfreq);
         Label labelins = new Label() { Text = cv, AutoSize = true };
         panel5.Controls.Add(labelins);
         Label labelcv = new Label() { Text = cv2, AutoSize = true };
         panel6.Controls.Add(labelcv);
         Label labeltpa = new Label() { Text = tpa, AutoSize = true };
         panel7.Controls.Add(labeltpa);
         Label labelvpa = new Label() { Text = vpa, AutoSize = true };
         panel8.Controls.Add(labelvpa);
         Label labeldesc = new Label() { Text = desc, AutoSize = true };
         panel9.Controls.Add(labeldesc);

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
               endHt = startHt + 1012;
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

         Bitmap bmp = new Bitmap(flowLayoutPanel1.Width, layoutPrintEndHeight-layoutPrintStartHeight);
         flowLayoutPanel1.DrawToBitmap(bmp, new Rectangle(0, 0, flowLayoutPanel1.Width, layoutPrintEndHeight - layoutPrintStartHeight));
         e.Graphics.DrawImage((Image)bmp,0,0);
      }

      private void saveToolStripButton_Click(object sender, EventArgs e)
      {
         MessageBox.Show("Save function not working at this time.");
      }


   }
}
