using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CruiseDAL;
using CruiseDAL.DataObjects;


namespace CruiseDesign
{
   public partial class CostSetupForm : Form
   {
      public CostSetupForm(CruiseDesignMain Main)
      {
         InitializeComponent();
         /*
         try
         {
            this.cdDAL = new DAL(dalPathDesign);
         }
         catch (System.IO.IOException e)
         {
            Logger.Log.E(e);
            MessageBox.Show("Error: Cannot open recon file");
            //TODO display error message to user
         }
         catch (System.Exception e)
         {
            Logger.Log.E(e);
            MessageBox.Show("Error: Cannot open recon file");
         }
          */
         cdDAL = Main.cdDAL;

         InitializeForm();
      }

      public DAL cdDAL { get; set; }
      public List<GlobalsDO> cdGlobals { get; set; }
      String str = "CruiseDesign";

      private void InitializeForm()
      {
            //  cdGlobals = cdDAL.Read<GlobalsDO>("Globals", "WHERE Block = ?",str);
            cdGlobals = cdDAL.From<GlobalsDO>().Where("Block = @p1").Read(str).ToList();
         if (cdGlobals.Count == 0 || cdGlobals == null)
         {
            useDefaultValues();
         }
         else
         {
            useSavedValues();
         }
      }

      private void useDefaultValues()
      {
         numericUpDownCrewCost.Value = 50;
         addGlobalRow("CrewCost","50");
         numericUpDownCrewSize.Value = 3;
         addGlobalRow("CrewSize","3");
         numericUpDownCostPaint.Value = 15;
         addGlobalRow("PaintCost","15");
         numericUpDownPaintTrees.Value = 35;
         addGlobalRow("PaintTrees","35");
         numericUpDownTavelTime.Value = 30;
         addGlobalRow("TravelTime","30");
         numericUpDownTimeTree.Value = 5;
         addGlobalRow("TimeTree","5");
         numericUpDownTimePlot.Value = 10;
         addGlobalRow("TimePlot","10");
         radioButtonWalkRate2.Checked = true;
         addGlobalRow("WalkRate","175");

      }

      private void useSavedValues()
      {
         //String cdKey;
         foreach (GlobalsDO gDO in cdGlobals)
         {
            switch (gDO.Key)
            {
               case "CrewCost":
                  numericUpDownCrewCost.Value = Convert.ToInt32(gDO.Value);
                  break;
               case "CrewSize":
                  numericUpDownCrewSize.Value = Convert.ToInt32(gDO.Value);
                  break;
               case "PaintCost":
                  numericUpDownCostPaint.Value = Convert.ToInt32(gDO.Value);
                  break;
               case "PaintTrees":
                  numericUpDownPaintTrees.Value = Convert.ToInt32(gDO.Value);
                  break;
               case "TravelTime":
                  numericUpDownTavelTime.Value = Convert.ToInt32(gDO.Value);
                  break;
               case "TimeTree":
                  numericUpDownTimeTree.Value = Convert.ToInt32(gDO.Value);
                  break;
               case "TimePlot":
                  numericUpDownTimePlot.Value = Convert.ToInt32(gDO.Value);
                  break;
               case "WalkRate":
                  int walkRate = Convert.ToInt32(gDO.Value);
                  if (walkRate == 260)
                     radioButtonWalkRate1.Checked = true;
                  else if (walkRate == 175)
                     radioButtonWalkRate2.Checked = true;
                  else if (walkRate == 90)
                     radioButtonWalkRate3.Checked = true;
                  else
                     radioButtonWalkRate4.Checked = true;
                  numericUpDownWalkRate.Value = walkRate;
                  break;
            }
         }

      }
      private void addGlobalRow(String key, String value)
      {
         GlobalsDO newGlobal = new GlobalsDO(cdDAL);
         newGlobal.Block = "CruiseDesign";
         newGlobal.Key = key;
         newGlobal.Value = value;

         newGlobal.Save();
      }
      private void button1_Click(object sender, EventArgs e)
      {
            // save all data to Global Form
            //         cdGlobals = cdDAL.Read<GlobalsDO>("Globals", "WHERE Block = ?", str);
            cdGlobals = cdDAL.From<GlobalsDO>().Where("Block = @p1").Read(str).ToList();
            foreach (GlobalsDO gDO in cdGlobals)
         {
            switch (gDO.Key)
            {
               case "CrewCost":
                  gDO.Value = numericUpDownCrewCost.Value.ToString();
                  break;
               case "CrewSize":
                  gDO.Value = numericUpDownCrewSize.Value.ToString();
                  break;
               case "PaintCost":
                  gDO.Value = numericUpDownCostPaint.Value.ToString();
                  break;
               case "PaintTrees":
                  gDO.Value = numericUpDownPaintTrees.Value.ToString();
                  break;
               case "TravelTime":
                  gDO.Value = numericUpDownTavelTime.Value.ToString();
                  break;
               case "TimeTree":
                  gDO.Value = numericUpDownTimeTree.Value.ToString();
                  break;
               case "TimePlot":
                  gDO.Value = numericUpDownTimePlot.Value.ToString();
                  break;
               case "WalkRate":
                  String walkRate;
                  if (radioButtonWalkRate1.Checked)
                     walkRate = "220";
                  else if (radioButtonWalkRate2.Checked)
                     walkRate = "130";
                  else if (radioButtonWalkRate3.Checked)
                     walkRate = "50";
                  else
                     walkRate = numericUpDownWalkRate.Value.ToString();
                  gDO.Value = walkRate;
                  break;
            }
            gDO.Save();
         }


         //cdDAL.Dispose();
         this.DialogResult = DialogResult.OK;
         this.Close();
      }
   }
}
