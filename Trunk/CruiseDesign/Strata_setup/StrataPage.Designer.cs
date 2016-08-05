namespace CruiseSystemManager.CruiseWizardPages
{
    partial class StrataPage
    {
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Panel panel1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StrataPage));
            this.SampleGroupButton = new System.Windows.Forms.Button();
            this.CurrentSTAddSGButton = new System.Windows.Forms.Button();
            this.CuttingUnitButton = new System.Windows.Forms.Button();
            this.MethodComboBox = new System.Windows.Forms.ComboBox();
            this.StrataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.YearComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CodeTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.MonthComboBox = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.BAFTextBox = new System.Windows.Forms.TextBox();
            this.FixedPlotSizeTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.CuttingUnitBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.StrataListBox = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.CuttingUnitListBox = new System.Windows.Forms.ListBox();
            panel1 = new System.Windows.Forms.Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StrataBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CuttingUnitBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator)).BeginInit();
            this.bindingNavigator.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            panel1.Controls.Add(this.SampleGroupButton);
            panel1.Controls.Add(this.CurrentSTAddSGButton);
            panel1.Controls.Add(this.CuttingUnitButton);
            panel1.Location = new System.Drawing.Point(0, 425);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(660, 45);
            panel1.TabIndex = 0;
            // 
            // SampleGroupButton
            // 
            this.SampleGroupButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SampleGroupButton.Location = new System.Drawing.Point(548, 12);
            this.SampleGroupButton.Margin = new System.Windows.Forms.Padding(10);
            this.SampleGroupButton.Name = "SampleGroupButton";
            this.SampleGroupButton.Size = new System.Drawing.Size(102, 23);
            this.SampleGroupButton.TabIndex = 0;
            this.SampleGroupButton.Text = "Sample Group >>";
            this.SampleGroupButton.UseVisualStyleBackColor = true;
            this.SampleGroupButton.Click += new System.EventHandler(this.SampleGroupButton_Click);
            // 
            // CurrentSTAddSGButton
            // 
            this.CurrentSTAddSGButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CurrentSTAddSGButton.Location = new System.Drawing.Point(116, 5);
            this.CurrentSTAddSGButton.Name = "CurrentSTAddSGButton";
            this.CurrentSTAddSGButton.Size = new System.Drawing.Size(167, 36);
            this.CurrentSTAddSGButton.TabIndex = 1;
            this.CurrentSTAddSGButton.Text = "Add Sample Groups For Current Stratum";
            this.CurrentSTAddSGButton.UseVisualStyleBackColor = true;
            this.CurrentSTAddSGButton.Click += new System.EventHandler(this.CurrentSTAddSGButton_Click);
            // 
            // CuttingUnitButton
            // 
            this.CuttingUnitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CuttingUnitButton.Location = new System.Drawing.Point(10, 12);
            this.CuttingUnitButton.Margin = new System.Windows.Forms.Padding(10);
            this.CuttingUnitButton.Name = "CuttingUnitButton";
            this.CuttingUnitButton.Size = new System.Drawing.Size(93, 23);
            this.CuttingUnitButton.TabIndex = 0;
            this.CuttingUnitButton.Text = "<< Cutting Units";
            this.CuttingUnitButton.UseVisualStyleBackColor = true;
            this.CuttingUnitButton.Click += new System.EventHandler(this.CuttingUnitButton_Click);
            // 
            // MethodComboBox
            // 
            this.MethodComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.StrataBindingSource, "Method", true));
            this.MethodComboBox.FormattingEnabled = true;
            this.MethodComboBox.Location = new System.Drawing.Point(156, 10);
            this.MethodComboBox.Name = "MethodComboBox";
            this.MethodComboBox.Size = new System.Drawing.Size(121, 21);
            this.MethodComboBox.TabIndex = 1;
            // 
            // StrataBindingSource
            // 
            this.StrataBindingSource.DataSource = typeof(CruiseDAL.DataObjects.StratumDO);
            this.StrataBindingSource.CurrentChanged += new System.EventHandler(this.StrataBindingSource_CurrentChanged);
            this.StrataBindingSource.AddingNew += new System.ComponentModel.AddingNewEventHandler(this.StrataBindingSource_AddingNew);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Code";
            // 
            // YearComboBox
            // 
            this.YearComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.StrataBindingSource, "Year", true));
            this.YearComboBox.FormattingEnabled = true;
            this.YearComboBox.Location = new System.Drawing.Point(177, 66);
            this.YearComboBox.Name = "YearComboBox";
            this.YearComboBox.Size = new System.Drawing.Size(100, 21);
            this.YearComboBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(150, 138);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(5, 3, 0, 0);
            this.label2.Size = new System.Drawing.Size(504, 20);
            this.label2.TabIndex = 44;
            this.label2.Text = "Descripton";
            // 
            // CodeTextBox
            // 
            this.CodeTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.StrataBindingSource, "Code", true));
            this.CodeTextBox.Location = new System.Drawing.Point(50, 7);
            this.CodeTextBox.Name = "CodeTextBox";
            this.CodeTextBox.Size = new System.Drawing.Size(40, 20);
            this.CodeTextBox.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(142, 69);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 13);
            this.label10.TabIndex = 54;
            this.label10.Text = "Year";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(106, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 46;
            this.label11.Text = "Method";
            // 
            // MonthComboBox
            // 
            this.MonthComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.StrataBindingSource, "Month", true));
            this.MonthComboBox.FormattingEnabled = true;
            this.MonthComboBox.Location = new System.Drawing.Point(60, 66);
            this.MonthComboBox.Name = "MonthComboBox";
            this.MonthComboBox.Size = new System.Drawing.Size(53, 21);
            this.MonthComboBox.TabIndex = 4;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 40);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(27, 13);
            this.label12.TabIndex = 48;
            this.label12.Text = "BAF";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 69);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 13);
            this.label13.TabIndex = 52;
            this.label13.Text = "Month";
            // 
            // BAFTextBox
            // 
            this.BAFTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.StrataBindingSource, "BasalAreaFactor", true));
            this.BAFTextBox.Location = new System.Drawing.Point(50, 37);
            this.BAFTextBox.Name = "BAFTextBox";
            this.BAFTextBox.Size = new System.Drawing.Size(40, 20);
            this.BAFTextBox.TabIndex = 2;
            // 
            // FixedPlotSizeTextBox
            // 
            this.FixedPlotSizeTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.StrataBindingSource, "FixedPlotSize", true));
            this.FixedPlotSizeTextBox.Location = new System.Drawing.Point(185, 37);
            this.FixedPlotSizeTextBox.Name = "FixedPlotSizeTextBox";
            this.FixedPlotSizeTextBox.Size = new System.Drawing.Size(92, 20);
            this.FixedPlotSizeTextBox.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(106, 40);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(73, 13);
            this.label14.TabIndex = 50;
            this.label14.Text = "Fixed PlotSize";
            // 
            // CuttingUnitBindingSource
            // 
            this.CuttingUnitBindingSource.DataSource = typeof(CruiseDAL.DataObjects.CuttingUnitDO);
            // 
            // bindingNavigator
            // 
            this.bindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bindingNavigator.BindingSource = this.StrataBindingSource;
            this.bindingNavigator.CountItem = null;
            this.bindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bindingNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripSeparator1,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.bindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator.MoveFirstItem = null;
            this.bindingNavigator.MoveLastItem = null;
            this.bindingNavigator.MoveNextItem = null;
            this.bindingNavigator.MovePreviousItem = null;
            this.bindingNavigator.Name = "bindingNavigator";
            this.bindingNavigator.PositionItem = null;
            this.bindingNavigator.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.bindingNavigator.Size = new System.Drawing.Size(146, 25);
            this.bindingNavigator.TabIndex = 56;
            this.bindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(37, 22);
            this.toolStripLabel1.Text = "Strata";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // StrataListBox
            // 
            this.StrataListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.StrataListBox.DataSource = this.StrataBindingSource;
            this.StrataListBox.DisplayMember = "Code";
            this.StrataListBox.FormattingEnabled = true;
            this.StrataListBox.IntegralHeight = false;
            this.StrataListBox.Location = new System.Drawing.Point(-2, 25);
            this.StrataListBox.Margin = new System.Windows.Forms.Padding(0);
            this.StrataListBox.Name = "StrataListBox";
            this.StrataListBox.ScrollAlwaysVisible = true;
            this.StrataListBox.Size = new System.Drawing.Size(150, 385);
            this.StrataListBox.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.bindingNavigator);
            this.panel2.Controls.Add(this.StrataListBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.tableLayoutPanel1.SetRowSpan(this.panel2, 6);
            this.panel2.Size = new System.Drawing.Size(150, 414);
            this.panel2.TabIndex = 59;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.richTextBox1, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.CuttingUnitListBox, 1, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(654, 414);
            this.tableLayoutPanel1.TabIndex = 60;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.MethodComboBox);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.YearComboBox);
            this.panel3.Controls.Add(this.FixedPlotSizeTextBox);
            this.panel3.Controls.Add(this.CodeTextBox);
            this.panel3.Controls.Add(this.BAFTextBox);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.MonthComboBox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(150, 20);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(504, 118);
            this.panel3.TabIndex = 60;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(150, 276);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(5, 3, 0, 0);
            this.label3.Size = new System.Drawing.Size(504, 20);
            this.label3.TabIndex = 61;
            this.label3.Text = "Cutting Units";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.RoyalBlue;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(150, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(5, 3, 0, 0);
            this.label4.Size = new System.Drawing.Size(504, 20);
            this.label4.TabIndex = 62;
            this.label4.Text = "Stratum";
            // 
            // richTextBox1
            // 
            this.richTextBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.StrataBindingSource, "Description", true));
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(150, 158);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(504, 118);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // CuttingUnitListBox
            // 
            this.CuttingUnitListBox.DataSource = this.CuttingUnitBindingSource;
            this.CuttingUnitListBox.DisplayMember = "Code";
            this.CuttingUnitListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CuttingUnitListBox.FormattingEnabled = true;
            this.CuttingUnitListBox.Location = new System.Drawing.Point(153, 299);
            this.CuttingUnitListBox.Name = "CuttingUnitListBox";
            this.CuttingUnitListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.CuttingUnitListBox.Size = new System.Drawing.Size(498, 108);
            this.CuttingUnitListBox.TabIndex = 1;
            this.CuttingUnitListBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CuttingUnitListBox_MouseClick);
            // 
            // StrataPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(panel1);
            this.Name = "StrataPage";
            this.Size = new System.Drawing.Size(660, 470);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.StrataBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CuttingUnitBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator)).EndInit();
            this.bindingNavigator.ResumeLayout(false);
            this.bindingNavigator.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SampleGroupButton;
        private System.Windows.Forms.Button CuttingUnitButton;
        private System.Windows.Forms.ComboBox MethodComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox YearComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CodeTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox MonthComboBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox BAFTextBox;
        private System.Windows.Forms.TextBox FixedPlotSizeTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.BindingSource StrataBindingSource;
        private System.Windows.Forms.BindingNavigator bindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ListBox StrataListBox;
        private System.Windows.Forms.Button CurrentSTAddSGButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.BindingSource CuttingUnitBindingSource;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ListBox CuttingUnitListBox;
    }
}
