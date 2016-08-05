using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FMSC.Controls
{
    public partial class SideLabelTextBox : UserControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox1 = new FMSC.Controls.ErrorDisplayingTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(312, 25);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.HasError = false;
            this.textBox1.Location = new System.Drawing.Point(100, 2);
            this.textBox1.Margin = new System.Windows.Forms.Padding(0);
            this.textBox1.Name = "textBox1";
            this.textBox1.CausesValidation = true;
            this.textBox1.Size = new System.Drawing.Size(212, 20);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SideLabelTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SideLabelTextBox";
            this.Size = new System.Drawing.Size(312, 25);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private FMSC.Controls.ErrorDisplayingTextBox textBox1;
        private System.Windows.Forms.Label label1;
        #endregion

        [Category("Action")]
        [Browsable(true)]
        public new event EventHandler TextChanged
        {
            add
            {
                textBox1.TextChanged += new EventHandler(value);
            }
            remove
            {
                textBox1.TextChanged -= value;
            }
        }


        [Category("Appearance")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue("")]
        [Bindable(true)]
        public override string Text
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }

        [Category("Appearance")]
        [Bindable(true)]
        public string LableText
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public ContentAlignment LabelTextAlignment
        {
            get
            {
                return label1.TextAlign;
            }
            set
            {
                label1.TextAlign = value;
            }
        }

        [Category("Appearance")]
        [DefaultValue(false)]
        [Bindable(true)]
        public bool HasError
        {
            get
            {
                return textBox1.HasError;
            }
            set
            {
                textBox1.HasError = value;
            }
        }

        [Category("Layout")]
        [DefaultValue(100.0)]
        public float LabelWidth
        {
            get
            {
                return tableLayoutPanel1.ColumnStyles[0].Width;
            }
            set
            {
                tableLayoutPanel1.ColumnStyles[0].Width = value;
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        [Bindable(true)]
        public bool ReadOnly
        {
            get
            {
                return textBox1.ReadOnly;
            }
            set
            {
                textBox1.ReadOnly = value;
            }
        }

        [Category("Behavior")]
        [DefaultValue(true)]
        [Bindable(true)]
        public new bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                textBox1.Enabled = value;
                label1.Enabled = value;
                this.Enabled = value;
            }
        }

        [Category("Behavior")]
        [DefaultValue(true)]
        [Bindable(true)]
        public new bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                textBox1.Visible = value;
                label1.Visible = value;
                this.Visible = value;
            }
        }

        public void Clear()
        {
            textBox1.Clear();
        }

        public void SelectAll()
        {
            textBox1.SelectAll();
        }


        public SideLabelTextBox()
        {
            InitializeComponent();
        }
    }
}
