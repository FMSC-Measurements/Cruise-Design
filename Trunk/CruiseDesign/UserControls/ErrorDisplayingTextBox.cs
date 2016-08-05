using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace FMSC.Controls
{
    public partial class ErrorDisplayingTextBox : TextBox
    {
        private bool hasError = false;
        [Category("Data")]
        public bool HasError
        {
            get
            {
                return hasError;
            }
            set
            {
                hasError = value;
                UpdateErrorApearance();
            }
        }

        private void UpdateErrorApearance()
        {
            if (hasError)
            {
                this.BackColor = Color.LightCoral;
            }
            else
            {
                this.BackColor = Color.White;
            }
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);
            if (e.Cancel == true)
            {
                HasError = true;
            }
            else
            {
                HasError = false;
            }

        }

        public ErrorDisplayingTextBox() : base() { }
    }
}
