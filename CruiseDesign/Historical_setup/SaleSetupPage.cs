﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CruiseDAL;
using CruiseDAL.DataObjects;


namespace CruiseDesign.Historical_setup
{
   public partial class SaleSetupPage : Form
   {
      #region Constructor
      
      public SaleSetupPage(CruiseDesignMain Main)
      {

         InitializeComponent();
         df.cdDAL = Main.cdDAL;
      }
      #endregion

      #region Properties

        
      public String tempFile, regNum, forNum, defUOM;
      string dalFile;
      bool canCreate;

      

      struct dataFiles
      {
         public DAL cdDAL { get; set; }
         public DAL tmpDAL { get; set; }
         public string tpFile;
         public string SaleNumber;
         public string Name;
         public string Purpose;
         public string Regnum;
         public string Forest;
         public string District;
         public string DefaultUOM;
      };
      dataFiles df;

      #endregion

      private void buttonBrowse_Click(object sender, EventArgs e)
      {

         openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\CruiseFiles\\Templates";
         if (openFileDialog1.ShowDialog() == DialogResult.OK)
         {
            tempFile = openFileDialog1.FileName;

            textBoxFile.Text = openFileDialog1.SafeFileName;

         }
           //open new cruise DAL
      }


      
      private void buttonFinish_Click(object sender, EventArgs e)
      {

         if (string.IsNullOrEmpty(tempFile))
         {
            MessageBox.Show(null,"No Template selected.","Warning", MessageBoxButtons.OK);
            return;
         }
         if (string.IsNullOrEmpty(textBoxName.Text.ToString()))
         {
            MessageBox.Show(null, "No Sale Name provided.", "Warning", MessageBoxButtons.OK);
            return;
         }
         if (string.IsNullOrEmpty(textBoxNum.Text.ToString()))
         {
            MessageBox.Show(null, "No Sale Number provided.", "Warning", MessageBoxButtons.OK);
            return;
         }
         if (string.IsNullOrEmpty(regNum) || string.IsNullOrEmpty(forNum))
         {
            MessageBox.Show(null, "No Region or Forest selected.", "Warning", MessageBoxButtons.OK);
            return;
         }
         this.UseWaitCursor = true;
          
         setWorking(true);

         df.tpFile = tempFile;
         df.SaleNumber = textBoxNum.Text.ToString();
         df.Name = textBoxName.Text.ToString();
         df.Purpose = comboBoxPurpose.SelectedItem.ToString();
         df.Regnum = regNum;
         df.Forest = forNum;
         df.District = textBoxDist.Text.ToString();
         df.DefaultUOM = defUOM;
         // create backgroundworker
         this.backgroundWorker1.RunWorkerAsync(df);


          //end backgroundworker
                
      }
      private void setWorking(bool working)
      {
         labelWorking.Visible = working;
         pictureBox1.Visible = working;
         pictureBox1.Enabled = working;

         if (working)
         {
            buttonBrowse.Enabled = false;
            groupBox1.Enabled = false;
            buttonFinish.Enabled = false;
         }
         else
         {
            buttonBrowse.Enabled = true;
            groupBox1.Enabled = true;
            buttonFinish.Enabled = true;
         }
      
      
      }

      private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
      {
         createNewDatabase((dataFiles) e.Argument);

      }
      
      private void createNewDatabase(dataFiles df)
      {
         try
         {
            df.tmpDAL = new DAL(df.tpFile);
         }
         catch (System.IO.IOException ie)
         {
            Logger.Log.E(ie);
         }
         catch (System.Exception ie)
         {
            Logger.Log.E(ie);
         }

         copyTemplateData(df);

         copySaleData(df);

      }

      private void copyTemplateData(dataFiles df)
      {
         //copy TreeDefaultValues table
         df.cdDAL.DirectCopy(df.tmpDAL, CruiseDAL.Schema.TREEDEFAULTVALUE._NAME, null, OnConflictOption.Ignore);
         //copy globals table
         df.cdDAL.DirectCopy(df.tmpDAL, CruiseDAL.Schema.GLOBALS._NAME, null, OnConflictOption.Ignore);
         //copy logfieldsetupdefault
         df.cdDAL.DirectCopy(df.tmpDAL, CruiseDAL.Schema.LOGFIELDSETUPDEFAULT._NAME, null, OnConflictOption.Ignore);
         //copy messagelog
         df.cdDAL.DirectCopy(df.tmpDAL, CruiseDAL.Schema.MESSAGELOG._NAME, null, OnConflictOption.Ignore);
         //copy reports
         df.cdDAL.DirectCopy(df.tmpDAL, "Reports", null, OnConflictOption.Ignore);
         //copy treefieldsetupdefault
         df.cdDAL.DirectCopy(df.tmpDAL, CruiseDAL.Schema.TREEFIELDSETUPDEFAULT._NAME, null, OnConflictOption.Ignore);
         //copy volumeequations
         df.cdDAL.DirectCopy(df.tmpDAL, CruiseDAL.Schema.VOLUMEEQUATION._NAME, null, OnConflictOption.Ignore);
         //copy treeauditvalue
         df.cdDAL.DirectCopy(df.tmpDAL, CruiseDAL.Schema.TREEAUDITVALUE._NAME, null, OnConflictOption.Ignore);
         //copy treedefaultvaluetreeauditvalue
         df.cdDAL.DirectCopy(df.tmpDAL, CruiseDAL.Schema.TREEDEFAULTVALUETREEAUDITVALUE._NAME, null, OnConflictOption.Ignore);
         //copy tally
         df.cdDAL.DirectCopy(df.tmpDAL, CruiseDAL.Schema.TALLY._NAME, null, OnConflictOption.Ignore);
         //copy cruise methods
         df.cdDAL.DirectCopy(df.tmpDAL, CruiseDAL.Schema.CRUISEMETHODS._NAME, null, OnConflictOption.Ignore);
 
      }

      private void copySaleData(dataFiles df)
      {
         SaleDO sale = new SaleDO(df.cdDAL);
         sale.SaleNumber = df.SaleNumber;
         sale.Name = df.Name;
         sale.Purpose = df.Purpose;
         sale.Region = df.Regnum;
         sale.Forest = df.Forest;
         sale.District = df.District;
         sale.DefaultUOM = df.DefaultUOM;
         sale.Remarks = "Historical Design";

         sale.Save();
     
      }
      
      private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
         
         setWorking(false);
         this.UseWaitCursor = false;
 
         MessageBox.Show(null, "File Created.", "Information", MessageBoxButtons.OK);

         Finish();

      }
      private void Finish()
      {
         if(df.tmpDAL != null)
            df.tmpDAL.Dispose();
         //if (cdDAL != null)
         //   cdDAL.Dispose();
         Close();
      }
      private void comboBoxReg_SelectedIndexChanged(object sender, EventArgs e)
      {
         // fill the Forest Box
         comboBoxFor.Items.Clear();
         switch (comboBoxReg.SelectedIndex)
         {
            // region 1
            case 0:
               regNum = "01";
               comboBoxFor.Items.AddRange(new object[] 
               {
                  "02 Beaverhead-Deerlodge",
                  "03 Bitterroot",
                  "04 Idaho Panhandle",
                  "05 Clearwater",
                  "08 Custer",
                  "10 Flathead",
                  "11 Gallatin",
                  "12 Helena",
                  "14 Kootenai",
                  "15 Lewis & Clark",
                  "16 Lolo",
                  "17 Nezperce"
               });
               break;
            // region 2
            case 1:
               regNum = "02";
               comboBoxFor.Items.AddRange(new object[] 
               {
                  "02 Bighorn",
                  "03 Black Hills",
                  "04 GMUG",
                  "06 Medicine Bow-Routt",
                  "07 Nebraska",
                  "09 San Jaun-Rio Grande",
                  "10 Arapaho-Roosevelt",
                  "12 Pike-San Isabel",
                  "14 Shoshone",
                  "15 White River"
               });
               break;
            // region 3
            case 2:
               regNum = "03";
               comboBoxFor.Items.AddRange(new object[] 
               {
                  "01 Apache-Sitgreaves",
                  "02 Carson",
                  "03 Cibola",
                  "04 Coconino",
                  "05 Coronado",
                  "06 Gila",
                  "07 Kaibab",
                  "08 Lincoln",
                  "09 Prescott",
                  "10 Santa Fe",
                  "12 Tonto"
               });
               break;
            // region 4
            case 3:
               regNum = "04";
               comboBoxFor.Items.AddRange(new object[] 
               {
                  "01 Ashley",
                  "02 Boise",
                  "03 Bridger-Teton",
                  "05 Caribou",
                  "07 Dixie",
                  "08 Fishlake",
                  "09 Humboldt",
                  "10 Manti-LaSal",
                  "12 Payette",
                  "13 Challis",
                  "13 Salmon",
                  "14 Sawtooth",
                  "15 Targhee",
                  "17 Toiyabe",
                  "18 Uinta",
                  "19 Wasatch-Cache"
               });

               break;
            // region 5
            case 4:
               regNum = "05";
               comboBoxFor.Items.AddRange(new object[] 
               {
                  "01 Angeles",
                  "02 Cleveland",
                  "03 Eldorado",
                  "04 Inyo",
                  "05 Klamath",
                  "06 Lassen",
                  "07 Los Padres",
                  "08 Mendocino",
                  "09 Modoc",
                  "10 Six Rivers",
                  "11 Plumas",
                  "12 San Bernardino",
                  "13 Sequoia",
                  "14 Shasta-Trinity",
                  "15 Sierra",
                  "16 Stanislaus",
                  "17 Tahoe"
               });

               break;
            // region 6
            case 5:
               regNum = "06";
               comboBoxFor.Items.AddRange(new object[] 
               {
                  "01 Deschutes",
                  "02 Fremont - Winema",
                  "03 Gifford Pinchot",
                  "04 Malheur",
                  "05 Mt. Baker-Snoqualmie",
                  "06 Mt. Hood",
                  "07 Ochoco",
                  "09 Olympic",
                  "10 Rogue River - Siskiyou",
                  "12 Siuslaw",
                  "14 Umatilla",
                  "15 Umpqua",
                  "16 Wallowa-Whitman",
                  "17 Okanogan - Wenatchee",
                  "18 Willamette",
                  "21 Colville"
               });

               break;
            // region 8
            case 6:
               regNum = "08";
               comboBoxFor.Items.AddRange(new object[] 
               {
                  "01 National Forests Alabama",
                  "02 Daniel Boone",
                  "03 Chattahoochee-Oconee",
                  "04 Cherokee",
                  "05 National Forests Florida",
                  "06 Kisatchie",
                  "07 National Forests Mississippi",
                  "08 George Washington-Jefferson",
                  "09 Ouachita",
                  "10 Ozark-St. Francis",
                  "11 National Forests N.Carolina",
                  "12 Francis Marion-Sumter",
                  "13 National Forests Texas",
                  "16 Caribbean"
               });

               break;
            // region 9
            case 7:
               regNum = "09";
               comboBoxFor.Items.AddRange(new object[] 
               {
                  "02 Chequamegon",
                  "03 Chippewa",
                  "04 Huron-Manistee",
                  "05 Mark Twain",
                  "06 Nicolet",
                  "07 Ottawa",
                  "08 Shawnee",
                  "09 Superior",
                  "10 Hiawatha",
                  "11 Wayne-Hoosier",
                  "12 Hoosier",
                  "13 Chequamegon/Nicolet",
                  "14 Wayne",
                  "19 Allegheny",
                  "20 Green Mountain",
                  "21 Monongahela",
                  "22 White Mountain"
               });

               break;
            // region 10
            case 8:
               regNum = "10";
               comboBoxFor.Items.AddRange(new object[] 
               {
                  "04 Chugach",
                  "05 Tongass",
               });

               break;
            // BLM
            case 9:
               regNum = "BLM";
              comboBoxFor.Items.AddRange(new object[] 
               {
                  "01 Unknown Forest"
               });

               break;
            // DOD
            case 10:
               regNum = "DOD";
               comboBoxFor.Items.AddRange(new object[] 
               {
                  "01 Unknown Forest",
                  "02 JBLM"
               });

               break;
         }
      }

      private void comboBoxFor_SelectionChangeCommitted(object sender, EventArgs e)
      {
         String forNumText = comboBoxFor.SelectedItem.ToString();
         forNum = forNumText.Substring(0, 2);

      }

      private void comboBoxUOM_SelectionChangeCommitted(object sender, EventArgs e)
      {
         String defUOMText = comboBoxUOM.SelectedItem.ToString();
         defUOM = defUOMText.Substring(0, 2);
      }

   }
}
           
