using System;
using System.Collections.Generic;
using System.ComponentModel;
using CruiseDAL;
using CruiseDAL.DataObjects;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CruiseDesign.Stats
{
   public partial class userSelectStratum : Form
   {
      public userSelectStratum(string method, List<StratumDO> reconStr, string code, string descrip)
      {
         InitializeComponent();
         

         // initialize data bindings
         InitializeDataBindings(reconStr);
         labelStr.Text = "Stratum = " + code + " " + descrip + " " + method;

      }
      public DAL cdDAL;
      public DAL rDAL;
      public StratumDO currentStratum;
      public long? stratumCN;

      private void InitializeDataBindings(List<StratumDO> reconStrata)
      {
         bindingSourceReconStratum.DataSource = reconStrata;
      }

      private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
      {
         
      }

      private void buttonDone_Click(object sender, EventArgs e)
      {
         Close();
      }

      private void selectedItemsGridView1_SelectionChanged(object sender, EventArgs e)
      {
         currentStratum = bindingSourceReconStratum.Current as StratumDO;
      }
   }
}
