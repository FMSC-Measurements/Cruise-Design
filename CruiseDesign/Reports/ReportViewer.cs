using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CruiseDesign.Reports
{
   public partial class ReportViewer : Form
   {
      public ReportViewer()
      {
         InitializeComponent();

      }


      public void setSaleInfo(string num, string name, string reg, string forst, string dist, string err, string cost, string vol)
      {
         richTextBox1.Text = "  Cruise Design Report ";
         richTextBox1.Text += "\n\n\nSale Number:  " + num;
         richTextBox1.Text += "\t\t\t\t\tSale Name:  " + name;
         richTextBox1.Text += "\n\nRegion:  " + reg;
         richTextBox1.Text += "\t\t\t\t\tExpected Sale Error:  " + err;
         richTextBox1.Text += "\n\nForest:  " + forst;
         richTextBox1.Text += "\t\t\t\t\tExpected Sale Volume:  " + vol;
         richTextBox1.Text += "\n\nDistrict:  " + dist;
         richTextBox1.Text += "\t\t\t\t\t\tEstimated Cruise Cost:  " + cost;
      }
      public void createStratumTable(string Code, string Meth, string Err, string TotVol, string Desc, string baf, string fps, string PlotSpacing, string totAcres, string units)
      {
         // create label for Stratum number
         richTextBox1.Text += "\n------------------------------------";
         richTextBox1.Text += "\n\nStratum:  " + Code;
         // create label for Units
         richTextBox1.Text += "\n\nUnits: " + units;
         // create labels
         richTextBox1.Text += "\n\nMethod:  " + Meth;
         richTextBox1.Text += "\t\t\t\t\tDescription:  " + Desc;
         richTextBox1.Text += "\n\nExpected Error:  " + Err;
         richTextBox1.Text += "\t\t\t\t\tExpected Volume:  " + TotVol;
         richTextBox1.Text += "\n\nStratum Acres:  " + totAcres;
         switch (Meth)
         {
            case "PNT":
            case "P3P":
            case "PCM":
            case "3PPNT":
               richTextBox1.Text += "\t\t\t\t\tBasal Area Factor:  " + baf;
               richTextBox1.Text += "\n\nPlot Spacing:  " + PlotSpacing;
               break;
            case "FIX":
            case "F3P":
            case "FCM":
            case "FIXCNT":
               richTextBox1.Text += "\t\t\t\t\tFixed Plot Size:  " + fps;
               richTextBox1.Text += "\n\nPlot Spacing:  " + PlotSpacing;
               break;
            default:
               break;
         }

      }

      public void createSgTable_100(int sgCnt, string meth)
      {
         string plots = "Plots ";
         if (meth == "100") plots = "Trees ";
         
         richTextBox1.Text += "\n\nSampGroup";
         richTextBox1.Text += "\tSg Err\t";
         richTextBox1.Text += plots;
         richTextBox1.Text +="\tCV   ";
         richTextBox1.Text += "\tTrees/acre";
         richTextBox1.Text += "\tVol/acre";
         richTextBox1.Text += "\tDescription";
         richTextBox1.Text += "\n";
      }

      public void createSgTable_str(int sgCnt, string meth)
      {
         string freq = "Frequency";
         if (meth == "3P") freq = "KZ Value";
         richTextBox1.Text += "\n\nSampGroup";
         richTextBox1.Text += "\tSg Err ";
         richTextBox1.Text += "\tTrees\t";
         richTextBox1.Text += freq;
         richTextBox1.Text += "\tInsurance";
         richTextBox1.Text += "\tCV     ";
         richTextBox1.Text += "\tTrees/acre";
         richTextBox1.Text +=  "\tVol/acre";
         richTextBox1.Text += "\tDescription";
         richTextBox1.Text += "\n"; 

         //this.flowLayoutPanel1.Controls.Add(table2);

      }
      public void createSgTable_s3p(int sgCnt)
      {
         richTextBox1.Text += "\n\nSampGroup";
         richTextBox1.Text += "\tSg Err";
         richTextBox1.Text += "\t3PTree";
         richTextBox1.Text += "\tTrees ";
         richTextBox1.Text += "\tFreq  ";
         richTextBox1.Text += "\tKZ    ";
         richTextBox1.Text += "\tInsurance";
         richTextBox1.Text += "\tCV 1 Stage";
         richTextBox1.Text += "\tCV 2 Stage";
         richTextBox1.Text += "\tTrees/acre";
         richTextBox1.Text += "\tVol/acre";
         richTextBox1.Text += "\tDescription";
         richTextBox1.Text += "\n"; 

      }
      public void createSgTable_pcm(int sgCnt, string meth)
      {
         string plot = "\tPlots";
         string trees = "\tTrees";
         string freq = "\tFrequencey";
         if (meth == "3PPNT")
         {
            plot = "\t3P Plots";
            trees = "\tPlots";
            freq = "\tKZ";
         }
         else if (meth == "F3P" || meth == "P3P")
         {
            freq = "\tKZ   ";
         }
         richTextBox1.Text += "\n\nSampGroup";
         richTextBox1.Text += "\tSg Err";
         richTextBox1.Text += plot;
         richTextBox1.Text += trees;
         richTextBox1.Text += freq;
         richTextBox1.Text += "\tCV 1 Stage";
         richTextBox1.Text += "\tCV 2 Stage";
         richTextBox1.Text += "\tTrees/acre";
         richTextBox1.Text += "\tVol/acre";
         richTextBox1.Text += "\tDescription";
         richTextBox1.Text += "\n";
      }

      public void createAddSgRow_100(int rowcnt, string sg, string err, string tree, string cv, string tpa, string vpa, string desc)
      {

         richTextBox1.Text += sg;
         richTextBox1.Text += "\t"; 
         richTextBox1.Text += err;
         richTextBox1.Text += "\t";
         richTextBox1.Text += tree;
         richTextBox1.Text += "\t";
         richTextBox1.Text += cv;
         richTextBox1.Text += "\t";
         richTextBox1.Text += tpa;
         richTextBox1.Text += "\t";
         richTextBox1.Text += vpa;
         richTextBox1.Text += "\t";
         richTextBox1.Text += desc;
         richTextBox1.Text += "\n"; 

      }
      public void createAddSgRow_str(int rowcnt, string sg, string err, string tree, string freq, string insur, string cv, string tpa, string vpa, string desc)
      {
         richTextBox1.Text += sg;
         richTextBox1.Text += "\t";
         richTextBox1.Text += err;
         richTextBox1.Text += "\t";
         richTextBox1.Text += tree;
         richTextBox1.Text += "\t";
         richTextBox1.Text += freq;
         richTextBox1.Text += "\t";
         richTextBox1.Text += insur;
         richTextBox1.Text += "\t";
         richTextBox1.Text += cv;
         richTextBox1.Text += "\t";
         richTextBox1.Text += tpa;
         richTextBox1.Text += "\t";
         richTextBox1.Text += vpa;
         richTextBox1.Text += "\t";
         richTextBox1.Text += desc;
         richTextBox1.Text += "\n";

      } 
      
      public void createAddSgRow_s3p(int rowcnt, string sg, string err, string tree, string tree2, string freq, string kz, string insur, string cv, string cv2, string tpa, string vpa, string desc)
      {
         richTextBox1.Text += sg;
         richTextBox1.Text += "\t";
         richTextBox1.Text += err;
         richTextBox1.Text += "\t";
         richTextBox1.Text += tree;
         richTextBox1.Text += "\t";
         richTextBox1.Text += tree2;
         richTextBox1.Text += "\t";
         richTextBox1.Text += freq;
         richTextBox1.Text += "\t";
         richTextBox1.Text += kz;
         richTextBox1.Text += "\t";
         richTextBox1.Text += insur;
         richTextBox1.Text += "\t";
         richTextBox1.Text += cv;
         richTextBox1.Text += "\t";
         richTextBox1.Text += cv2;
         richTextBox1.Text += "\t";
         richTextBox1.Text += tpa;
         richTextBox1.Text += "\t";
         richTextBox1.Text += vpa;
         richTextBox1.Text += "\t";
         richTextBox1.Text += desc;
         richTextBox1.Text += "\n";
      }
      public void createAddSgRow_pcm(int rowcnt, string sg, string err, string plot, string tree, string freq, string cv, string cv2, string tpa, string vpa, string desc)
      {
         //table2.RowCount++;
         //int rowcnt = table2.RowCount - 1;
         richTextBox1.Text += sg;
         richTextBox1.Text += "\t";
         richTextBox1.Text += err;
         richTextBox1.Text += "\t";
         richTextBox1.Text += plot;
         richTextBox1.Text += "\t";
         richTextBox1.Text += tree;
         richTextBox1.Text += "\t";
         richTextBox1.Text += freq;
         richTextBox1.Text += "\t";
         richTextBox1.Text += cv;
         richTextBox1.Text += "\t";
         richTextBox1.Text += cv2;
         richTextBox1.Text += "\t";
         richTextBox1.Text += tpa;
         richTextBox1.Text += "\t";
         richTextBox1.Text += vpa;
         richTextBox1.Text += "\t";
         richTextBox1.Text += desc;
         richTextBox1.Text += "\n"; 

      }

   }
/*      private void buttonPrint_Click(object sender, EventArgs e)
      {
         CaptureScreen();
         printDialog1.Document = printDocument1;
         DialogResult result = printDialog1.ShowDialog();

         if (result == DialogResult.OK)
         {
            printDocument1.Print();
         }
      
      }

      private void CaptureScreen()
      {
         Graphics myGraphics = flowLayoutPanel1.CreateGraphics();
         Size s = flowLayoutPanel1.Size;
         memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
         Graphics memoryGraphics = Graphics.FromImage(memoryImage);
         Point screenLoc = PointToScreen(flowLayoutPanel1.Location);
         memoryGraphics.CopyFromScreen(screenLoc.X, screenLoc.Y, 0, 0, s);
      }

      private void printDocument1_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
      {
         e.Graphics.DrawImage(memoryImage, 0, 0);
      }

   */
}
