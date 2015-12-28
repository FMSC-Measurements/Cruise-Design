namespace CruiseSystemManager.CruiseWizardPages
{
    partial class CuttingUnitsPage
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
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CuttingUnitsPage));
            this.DescriptionTextBox = new System.Windows.Forms.RichTextBox();
            this.CuttingUnitBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PaymentUnitTextBox = new System.Windows.Forms.TextBox();
            this.LogMethComboBox = new System.Windows.Forms.ComboBox();
            this.AreaTextBox = new System.Windows.Forms.TextBox();
            this.CodeTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CancelButton = new System.Windows.Forms.Button();
            this.StrataButton = new System.Windows.Forms.Button();
            this.CuttingUnitsListBox = new System.Windows.Forms.ListBox();
            this.bindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CuttingUnitBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator)).BeginInit();
            this.bindingNavigator.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(137, 5);
            label5.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(70, 13);
            label5.TabIndex = 18;
            label5.Text = "Payment Unit";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(137, 31);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(52, 13);
            label4.TabIndex = 16;
            label4.Text = "Log Meth";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = System.Drawing.Color.LightSteelBlue;
            label3.Dock = System.Windows.Forms.DockStyle.Fill;
            label3.Location = new System.Drawing.Point(156, 115);
            label3.Margin = new System.Windows.Forms.Padding(0);
            label3.Name = "label3";
            label3.Padding = new System.Windows.Forms.Padding(5, 3, 0, 0);
            label3.Size = new System.Drawing.Size(470, 20);
            label3.TabIndex = 14;
            label3.Text = "Description";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(3, 31);
            label2.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(29, 13);
            label2.TabIndex = 12;
            label2.Text = "Area";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(3, 5);
            label1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(32, 13);
            label1.TabIndex = 11;
            label1.Text = "Code";
            // 
            // DescriptionTextBox
            // 
            this.DescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DescriptionTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.CuttingUnitBindingSource, "Description", true));
            this.DescriptionTextBox.Location = new System.Drawing.Point(159, 141);
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.Size = new System.Drawing.Size(464, 209);
            this.DescriptionTextBox.TabIndex = 0;
            this.DescriptionTextBox.Text = "";
            // 
            // CuttingUnitBindingSource
            // 
            this.CuttingUnitBindingSource.DataSource = typeof(CruiseDAL.DataObjects.CuttingUnitDO);
            // 
            // PaymentUnitTextBox
            // 
            this.PaymentUnitTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.CuttingUnitBindingSource, "PaymentUnit", true));
            this.PaymentUnitTextBox.Location = new System.Drawing.Point(213, 2);
            this.PaymentUnitTextBox.Name = "PaymentUnitTextBox";
            this.PaymentUnitTextBox.Size = new System.Drawing.Size(100, 20);
            this.PaymentUnitTextBox.TabIndex = 1;
            // 
            // LogMethComboBox
            // 
            this.LogMethComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.CuttingUnitBindingSource, "LoggingMethod", true));
            this.LogMethComboBox.FormattingEnabled = true;
            this.LogMethComboBox.Location = new System.Drawing.Point(213, 28);
            this.LogMethComboBox.Name = "LogMethComboBox";
            this.LogMethComboBox.Size = new System.Drawing.Size(100, 21);
            this.LogMethComboBox.TabIndex = 3;
            // 
            // AreaTextBox
            // 
            this.AreaTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.CuttingUnitBindingSource, "Area", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.AreaTextBox.Location = new System.Drawing.Point(41, 28);
            this.AreaTextBox.Name = "AreaTextBox";
            this.AreaTextBox.Size = new System.Drawing.Size(40, 20);
            this.AreaTextBox.TabIndex = 2;
            // 
            // CodeTextBox
            // 
            this.CodeTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.CuttingUnitBindingSource, "Code", true));
            this.CodeTextBox.HideSelection = false;
            this.CodeTextBox.Location = new System.Drawing.Point(41, 2);
            this.CodeTextBox.Name = "CodeTextBox";
            this.CodeTextBox.Size = new System.Drawing.Size(40, 20);
            this.CodeTextBox.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.CancelButton);
            this.panel1.Controls.Add(this.StrataButton);
            this.panel1.Location = new System.Drawing.Point(0, 369);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(639, 45);
            this.panel1.TabIndex = 62;
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelButton.Location = new System.Drawing.Point(10, 12);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(10);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // StrataButton
            // 
            this.StrataButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StrataButton.Location = new System.Drawing.Point(524, 12);
            this.StrataButton.Margin = new System.Windows.Forms.Padding(10);
            this.StrataButton.Name = "StrataButton";
            this.StrataButton.Size = new System.Drawing.Size(75, 23);
            this.StrataButton.TabIndex = 0;
            this.StrataButton.Text = "Strata >>";
            this.StrataButton.UseVisualStyleBackColor = true;
            this.StrataButton.Click += new System.EventHandler(this.StrataButton_Click);
            // 
            // CuttingUnitsListBox
            // 
            this.CuttingUnitsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CuttingUnitsListBox.DataSource = this.CuttingUnitBindingSource;
            this.CuttingUnitsListBox.DisplayMember = "Code";
            this.CuttingUnitsListBox.FormattingEnabled = true;
            this.CuttingUnitsListBox.IntegralHeight = false;
            this.CuttingUnitsListBox.Location = new System.Drawing.Point(0, 25);
            this.CuttingUnitsListBox.Margin = new System.Windows.Forms.Padding(0);
            this.CuttingUnitsListBox.Name = "CuttingUnitsListBox";
            this.CuttingUnitsListBox.ScrollAlwaysVisible = true;
            this.CuttingUnitsListBox.Size = new System.Drawing.Size(150, 321);
            this.CuttingUnitsListBox.TabIndex = 0;
            // 
            // bindingNavigator
            // 
            this.bindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bindingNavigator.BindingSource = this.CuttingUnitBindingSource;
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
            this.bindingNavigator.TabIndex = 64;
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
            this.toolStripLabel1.Size = new System.Drawing.Size(69, 22);
            this.toolStripLabel1.Text = "Cutting Units";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.bindingNavigator);
            this.panel2.Controls.Add(this.CuttingUnitsListBox);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.tableLayoutPanel1.SetRowSpan(this.panel2, 4);
            this.panel2.Size = new System.Drawing.Size(150, 350);
            this.panel2.TabIndex = 65;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(label3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.DescriptionTextBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label6, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 1);
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.Black;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 71.42857F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(629, 356);
            this.tableLayoutPanel1.TabIndex = 66;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.RoyalBlue;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(156, 3);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(5, 3, 0, 0);
            this.label6.Size = new System.Drawing.Size(470, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Cutting Unit";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.AreaTextBox);
            this.panel3.Controls.Add(this.PaymentUnitTextBox);
            this.panel3.Controls.Add(this.CodeTextBox);
            this.panel3.Controls.Add(label5);
            this.panel3.Controls.Add(this.LogMethComboBox);
            this.panel3.Controls.Add(label1);
            this.panel3.Controls.Add(label4);
            this.panel3.Controls.Add(label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(156, 26);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(470, 86);
            this.panel3.TabIndex = 1;
            // 
            // CuttingUnitsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Name = "CuttingUnitsPage";
            this.Size = new System.Drawing.Size(639, 414);
            ((System.ComponentModel.ISupportInitialize)(this.CuttingUnitBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
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

        private System.Windows.Forms.RichTextBox DescriptionTextBox;
        private System.Windows.Forms.TextBox PaymentUnitTextBox;
        private System.Windows.Forms.ComboBox LogMethComboBox;
        private System.Windows.Forms.TextBox AreaTextBox;
        private System.Windows.Forms.TextBox CodeTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button StrataButton;
        private System.Windows.Forms.ListBox CuttingUnitsListBox;
        private System.Windows.Forms.BindingNavigator bindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.BindingSource CuttingUnitBindingSource;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
    }
}
