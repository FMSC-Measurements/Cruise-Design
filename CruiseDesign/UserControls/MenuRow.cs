using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FMSC.Controls
{
    public class MenuRow : UserControl
    {
        #region designer code
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rootLayout = new System.Windows.Forms.TableLayoutPanel();
            this.button = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.rootLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // rootLayout
            // 
            this.rootLayout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rootLayout.ColumnCount = 3;
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.rootLayout.Controls.Add(this.button, 1, 1);
            this.rootLayout.Controls.Add(this.label, 0, 1);
            this.rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootLayout.Location = new System.Drawing.Point(0, 0);
            this.rootLayout.Margin = new System.Windows.Forms.Padding(0);
            this.rootLayout.Name = "rootLayout";
            this.rootLayout.RowCount = 3;
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.rootLayout.Size = new System.Drawing.Size(475, 189);
            this.rootLayout.TabIndex = 1;
            // 
            // button
            // 
            this.button.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button.Location = new System.Drawing.Point(409, 73);
            this.button.Margin = new System.Windows.Forms.Padding(4);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(42, 42);
            this.button.TabIndex = 2;
            this.button.UseVisualStyleBackColor = false;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.BackColor = System.Drawing.Color.Transparent;
            this.label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(0, 69);
            this.label.Margin = new System.Windows.Forms.Padding(0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(405, 50);
            this.label.TabIndex = 3;
            this.label.Text = "label1";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MenuRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rootLayout);
            this.Name = "MenuRow";
            this.Size = new System.Drawing.Size(475, 189);
            this.rootLayout.ResumeLayout(false);
            this.rootLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.TableLayoutPanel rootLayout;
        protected System.Windows.Forms.Button button;
        protected System.Windows.Forms.Label label;
        #endregion


        [Category("Action")]
        public event EventHandler ButtonClick
        {
            add
            {
                button.Click += new EventHandler(value);
            }
            remove
            {
                button.Click -= value;
            }
        }


        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override String Text
        {
            get
            {
                return label.Text;
                
            }
            set
            {
                label.Text = value;
            }
        }

        public bool ShouldSerializeText()
        {
            return true;
        }


        [Category("Appearance")]
        public Image ButtonBackGroundImage
        {
            get
            {
                return button.BackgroundImage;
            }
            set
            {
                button.BackgroundImage = value;
            }
        }

        [Category("Appearance")]
        public float ButtonHeight
        {
            get
            {
                return rootLayout.RowStyles[1].Height;
            }
            set
            {
                rootLayout.RowStyles[1].Height = value;
            }
        }

        [Category("Appearance")]
        public float ButtonWidth
        {
            get
            {
                return rootLayout.ColumnStyles[1].Width;
            }
            set
            {
                rootLayout.ColumnStyles[1].Width = value;
            }
        }

        [Category("Appearance")]
        public float ButtonRightMargin
        {
            get
            {
                return rootLayout.ColumnStyles[2].Width;
            }
            set
            {
                rootLayout.ColumnStyles[2].Width = value;
            }
        }


        [Category("Appearance")]
        public override Image BackgroundImage
        {
            get
            {
                return rootLayout.BackgroundImage;
            }
            set
            {
                rootLayout.BackgroundImage = value;
            }
        }

        public override ImageLayout BackgroundImageLayout
        {
            get
            {
                return rootLayout.BackgroundImageLayout;
            }
            set
            {
                rootLayout.BackgroundImageLayout = value;
            }
        }

        [Category("Appearance")]
        public FlatStyle ButtonFlatStyle
        {
            get
            {
                return button.FlatStyle;
            }
            set
            {
                button.FlatStyle = value;
            }
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public FlatButtonAppearance ButtonFlatAppearance
        {
            get
            {
                return button.FlatAppearance;
                
            }
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public override Font Font
        {
            get
            {
                return label.Font;
            }
            set
            {
                label.Font = value;
            }
        }


        public MenuRow()
        {
            InitializeComponent();
        }



    }
}
