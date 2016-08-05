using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace FMSC.Controls
{
    interface ILabelTextBox
    {
        //events 
        [Category("Action")]
        event EventHandler TextChanged;

        //propertys
        [Category("Appearance")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue("")]
        [Bindable(true)]
        string Text { get; set; }

        [Category("Appearance")]
        [Bindable(true)]
        string LabelText { get; set; }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        ContentAlignment LabelTextAlignment { get; set; }

        [Category("Appearance")]
        [DefaultValue(false)]
        [Bindable(true)]
        bool HasError { get; set; }


        [Category("Behavior")]
        [DefaultValue(false)]
        [Bindable(true)]
        bool ReadOnly { get; set; }
        


        //methods
        void Clear();
        void SelectAll();

        

    }
}
