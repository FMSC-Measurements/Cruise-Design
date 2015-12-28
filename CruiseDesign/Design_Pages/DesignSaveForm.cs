using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CruiseDesign.Design_Pages
{
   public partial class DesignSaveForm : Form
   {
      public DesignSaveForm()
      {
         InitializeComponent();

      }

      private void buttonCancel_Click(object sender, EventArgs e)
      {
         this.DialogResult = DialogResult.Cancel;
         this.Close();
      }

      private void buttonSave_Click(object sender, EventArgs e)
      {
         this.DialogResult = DialogResult.OK;
         this.Close();
      }

      private void textBox1_TextChanged(object sender, EventArgs e)
      {
         if (textBox1.Text.Length > 0)
            buttonSave.Enabled = true;
         else
            buttonSave.Enabled = false;
      }
   }
}
