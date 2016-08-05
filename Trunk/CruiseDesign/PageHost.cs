//Notes: this is a customized control designed to switch between multiple controls and 
//display them as if they were a page. 
//when another control is hosted inside this control, its dock property is set to fill
//to best use the PageHost Control, design a user control containing the elements that
//you want, then in code create an instance of the user control and add it to the PageHost.
//


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using CruiseDAL;

//using System.Windows.Forms.Design;
//using System.ComponentModel.Design;

namespace CruiseDesign.Controls
{
    //[Designer(typeof(PageHost.PageHostDesigner))]
    class PageHost : UserControl
    {
        //public DAL rDAL { get; set; }

        #region Fields 
        //private PanelCollection panels = null;
        private Control selectedControl = null;
        private int selectedIndex = -1;
        //private List<Control> pages = null; 
        #endregion


        #region Designer Generated Code
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        private void InitializeComponent()
        {
           this.SuspendLayout();
           // 
           // PageHost
           // 
           this.Name = "PageHost";
           this.Size = new System.Drawing.Size(206, 183);
           this.Load += new System.EventHandler(this.PageHost_Load);
           this.ResumeLayout(false);

        }
        #endregion


        #region Ctor & Dispose
        public PageHost()
        {
            this.InitializeComponent();
 
            //base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //base.SetStyle(ControlStyles.ResizeRedraw, true);

        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion


        #region Properties
        //[DefaultValue(DockStyle.Fill)]
        //[Category("Layout")]
        //[Description("Gets or sets which edge of the parent container a control is docked to.")]
        //public new DockStyle Dock
        //{
        //    get
        //    {
        //        return base.Dock;
        //    }
        //    set
        //    {
        //        base.Dock = value;
        //    }
        //}

        //[Category("Panels")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //[Description("Gets the collection of panels in this control")]
        //public PanelCollection Panels
        //{
        //    get
        //    {
        //        return this.panels;
        //    }
        //}


        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Control CurrentPage
        {
            get
            {
                return this.selectedControl;
            }
            set
            {
                this.Display(value);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CurrentPageIndex {
            get
            {
                return this.selectedIndex;
            }
            set
            {
                if (this.Controls.Count == 0)
                {
                    this.Display(-1);
                    return;
                }
                
                if (value < -1 || value >= this.Controls.Count)
                {
                    throw new ArgumentOutOfRangeException("SelectedIndex", value, "value out of range");
                }

                this.Display(value);
            }
        }

        #endregion

        #region Methods
        public void Add(Control value)
        {
            base.Controls.Add(value);
        }

        public void Display(Control value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            if (this.Controls.Contains(value) == false)
            {
                return;
            }

            //hide current control
            if (this.selectedControl != null)
            {
                this.selectedControl.Visible = false;
            }

            this.selectedControl = value;

            this.selectedControl.Parent = this;
            if (this.Contains(this.selectedControl) == false)
            {
                this.Container.Add(this.selectedControl);
            }

            this.selectedIndex = base.Controls.IndexOf(value);

            this.selectedControl.SetBounds(0, 0, this.Width, this.Height);
            this.selectedControl.Visible = true;
            this.selectedControl.BringToFront();
            this.FocusFirstTabIndex(this.selectedControl);

            this.selectedControl.Invalidate();

        }

        public void Display(int index)
        {
            if (index < 0 || index >= this.Controls.Count)
            {
                return;
            }
            Control control = this.Controls[index];

            this.Display(control);

        }

        public void FocusFirstTabIndex(Control container)
        {
            Control searchResult = null;

            foreach (Control c in container.Controls)
            {
                if (c.CanFocus && (searchResult == null || c.TabIndex < searchResult.TabIndex))
                {
                    searchResult = c;
                }
            }
            if (searchResult != null)
            {
                searchResult.Focus();
            }
            else
            {
                container.Focus();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (this.Controls.Count > 0)
            {
                this.Display(0);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (this.selectedControl != null)
            {
                this.selectedControl.SetBounds(0, 0, this.Width, this.Height);
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            e.Control.Visible = false;
            base.OnControlAdded(e);
        }

        
        #endregion

        private void PageHost_Load(object sender, EventArgs e)
        {

        }
    }
}
