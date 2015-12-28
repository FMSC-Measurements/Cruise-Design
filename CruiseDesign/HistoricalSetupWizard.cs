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
using CruiseDesign.Historical_setup;
using System.Collections;

namespace CruiseDesign
{

   public partial class HistoricalSetupWizard : Form
   {
      #region Fields
      private UnitSetupPageHS unitPage = null;
      private HistoricalSetupPage histPage = null;
      //private SelectSGset sgselectPage = null;
      #endregion

      #region Constructor
      public HistoricalSetupWizard(CruiseDesignMain Main, bool canCreateNew)
      {
         InitializeComponent();
        /* try
         {
            this.cdDAL = new DAL(dalPathDesign, canCreateNew);
         }
         catch (System.IO.IOException e)
         {
            Logger.Log.E(e);
            //TODO display error message to user
         }
         catch (System.Exception e)
         {
            Logger.Log.E(e);
         }
         */
         cdDAL = Main.cdDAL;

         setSalePurpose();

         InitializeDataBindings();
         InitializePages();
      }
      #endregion
 
      #region Properties

      public ArrayList selectedUnits = new ArrayList();
      public String histFile,UOM;

      // add the binding lists
      public DAL cdDAL { get; set; }
      public DAL hDAL { get; set; }

      public SaleDO Sale { get; set; }
      // current design lists
      public StratumDO currentStratum;
      public SampleGroupStatsDO currentSgStats;
      public StratumStatsDO currentStratumStats;
      public TreeDefaultValueDO currentTreeDefaults;
      
      public BindingList<CuttingUnitDO> cdCuttingUnits { get; set; }
      public BindingList<StratumDO> cdStratum { get; set; }
      
      public List<TreeDefaultValueDO> cdTreeDefaults { get; set; }

      // selected historical cruise lists
      public StratumDO selectedStratum;
      public SampleGroupDO selectedSampleGroup;
      public POPDO selectedPOP;
      public List<LCDDO> selectedLCD { get; set; } 

      public BindingList<StratumDO> histStratum { get; set; }
      public BindingList<TreeDefaultValueDO> histTreeDefaults { get; set; }
      public BindingList<SampleGroupDO> histSampleGroup { get; set; }


      #endregion
      #region Initialization Methods
 
      private void InitializePages()
      {
         unitPage = new UnitSetupPageHS(this);
         pageHost2.Add(unitPage);

         histPage = new HistoricalSetupPage(this);
         pageHost2.Add(histPage);

         //sgselectPage = new SelectSGset(this);
         //pageHost2.Add(sgselectPage);

         this.DialogResult = DialogResult.Cancel;
      }
      #endregion

      #region Paging Methods
     
      public void GoToUnitPage()
      {

         pageHost2.Display(unitPage);

      }

      public void GoToHistPage()
      {
         // If from UnitPage - get new currentStratumStats
         currentStratumStats = (cdDAL.ReadSingleRow<StratumStatsDO>("StratumStats", "WHERE Stratum_CN = ? AND SgSet = 1", currentStratum.Stratum_CN));

         if (currentStratumStats != null)
         {
            // data already exists for stratum, delete stratum and continue?
            MessageBox.Show("Stratum data already exists for this stratum","Information");
            return;
        
         }
         
         currentStratumStats = new StratumStatsDO(cdDAL);
         currentStratumStats.Stratum = currentStratum;
         currentStratumStats.Code = currentStratum.Code;
         currentStratumStats.Description = currentStratum.Description;
         currentStratumStats.SgSet = 1;
         currentStratumStats.SgSetDescription = "";

         float totalAcres = 0;
         foreach (CuttingUnitDO cu in currentStratum.CuttingUnits)
         {
            float acres = cu.Area;
            totalAcres += acres;
         }
         currentStratumStats.TotalAcres = totalAcres;
//         Owner.currentStratumStats.Save();

         if (openFileDialog1.ShowDialog() == DialogResult.OK)
         {
            histFile = openFileDialog1.FileName;
            //set title bar with file name
            histPage.textBoxFile.Text = openFileDialog1.SafeFileName;
            //open new cruise DAL
            if (histFile.Length > 0)
            {
               try
               {
                  hDAL = new DAL(histFile);
               }
               catch (System.IO.IOException ie)
               {
                  Logger.Log.E(ie);
               }
               catch (System.Exception ie)
               {
                  Logger.Log.E(ie);
               }
            }
            else
            {
               return;
            }

            //set binding list for stratum
            histStratum = new BindingList<StratumDO>(hDAL.Read<StratumDO>("Stratum", null, null));
            histPage.bindingSourceStratum.DataSource = histStratum;

            pageHost2.Display(histPage);
         }
      }

      public void GoToSgPage()
      {
           //set binding list using currentStratum_cn
           //cdStratumStats = new BindingList<StratumStatsDO>(cdDAL.Read<StratumStatsDO>("StratumStats", "WHERE Stratum_CN = ?", currentStratum.Stratum_CN));
           //if (cdStratumStats == null)
           //{
           //   return;
           //}
           //sgselectPage.bindingSourceStratumStats.DataSource = cdStratumStats;
           //set sg binding list using stratumstats_cn
           //cdSampleGroup = new BindingList<SampleGroupStatsDO>(cdDAL.Read<SampleGroupStatsDO>("SampleGroupStats", "Where StratumStats_CN = ? AND SgSet = ?", currentStratumStats.StratumStats_CN, currentStratumStats.SgSet));
           //set TDV binding list using sgstats_tdv link
           //sgselectPage.bindingSourcSampleGroup.DataSource = cdSgStats;
           //pageHost2.Display(sgselectPage);
      }
      #endregion

      #region Other Methods
      //cancels the cruise wizard diolog and discards all resorurces

      private void setSalePurpose()
      {
         SaleDO sale = new SaleDO (cdDAL.ReadSingleRow<SaleDO>("Sale", null, null));

         if (sale.DefaultUOM == null)
            UOM = "03";
         else
            UOM = sale.DefaultUOM.ToString();
      }

      public void Cancel()
      {
            //if (MessageBox.Show("Are you sure you want to cancel? Entered information will not be saved", "Warning", MessageBoxButtons.YesNo)
            //    == DialogResult.Yes)
           // {

         this.DialogResult = DialogResult.Cancel;
         //if(cdDAL != null)
         //   cdDAL.Dispose();
         this.Close();
            //}
      }
      public void Finish()
      {
            this.DialogResult = DialogResult.OK;
            //if (cdDAL != null)
            //   cdDAL.Dispose();
            this.Close();

      }

      private void InitializeDataBindings()
      {
           cdCuttingUnits = new BindingList<CuttingUnitDO>(cdDAL.Read<CuttingUnitDO>("CuttingUnit", null, null));
           cdStratum = new BindingList<StratumDO>(cdDAL.Read<StratumDO>("Stratum", null, null));
           cdTreeDefaults = new List<TreeDefaultValueDO>(cdDAL.Read<TreeDefaultValueDO>("TreeDefaultValue",null,null));
           
      }

      private void createHistoricalData()
      {
         // copy stratum row to design
         // copy stratumstats to design
         // copy sample groups row to design
         // create samplegroupstats
         // for each samplegroup
         // get POP data
         // calculate statistics (based on method)
         // get LCD data
         // calculate trees/acre and volume/acre by sample group
         // save samplegroupstats row
         // loop through TDV information
         // check with current TDV values
         // create link
         // save everything
      }

      #endregion

   }
}
