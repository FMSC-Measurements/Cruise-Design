namespace CruiseDesign.Strata_setup
{
   partial class TDV_Select
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

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TDV_Select));
         this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
         this.panel1 = new System.Windows.Forms.Panel();
         this.buttonReturn = new System.Windows.Forms.Button();
         this.buttonNew = new System.Windows.Forms.Button();
         this.selectedItemsGridView1 = new FMSC.Controls.SelectedItemsGridView();
         this.bindingSourceTreeDV = new System.Windows.Forms.BindingSource(this.components);
         this.speciesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.validatorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.treeDefaultValueCNDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.primaryProductDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.liveDeadDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.cullPrimaryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.hiddenPrimaryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.cullSecondaryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.hiddenSecondaryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.recoverableDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.contractSpeciesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.merchHeightLogLengthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.merchHeightTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.formClassDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.treeGradeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.barkThicknessRatioDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.averageZDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.referenceHeightPercentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.rowIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.dALDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.isPersistedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
         this.tagDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.errorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.tableLayoutPanel1.SuspendLayout();
         this.panel1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.selectedItemsGridView1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.bindingSourceTreeDV)).BeginInit();
         this.SuspendLayout();
         // 
         // tableLayoutPanel1
         // 
         this.tableLayoutPanel1.ColumnCount = 1;
         this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
         this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
         this.tableLayoutPanel1.Controls.Add(this.selectedItemsGridView1, 0, 0);
         this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
         this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
         this.tableLayoutPanel1.Name = "tableLayoutPanel1";
         this.tableLayoutPanel1.RowCount = 2;
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
         this.tableLayoutPanel1.Size = new System.Drawing.Size(334, 248);
         this.tableLayoutPanel1.TabIndex = 0;
         // 
         // panel1
         // 
         this.panel1.BackColor = System.Drawing.Color.LemonChiffon;
         this.panel1.Controls.Add(this.buttonReturn);
         this.panel1.Controls.Add(this.buttonNew);
         this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel1.Location = new System.Drawing.Point(2, 218);
         this.panel1.Margin = new System.Windows.Forms.Padding(2);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(330, 28);
         this.panel1.TabIndex = 1;
         // 
         // buttonReturn
         // 
         this.buttonReturn.BackColor = System.Drawing.SystemColors.ButtonFace;
         this.buttonReturn.Dock = System.Windows.Forms.DockStyle.Right;
         this.buttonReturn.Location = new System.Drawing.Point(198, 0);
         this.buttonReturn.Margin = new System.Windows.Forms.Padding(2);
         this.buttonReturn.Name = "buttonReturn";
         this.buttonReturn.Size = new System.Drawing.Size(132, 28);
         this.buttonReturn.TabIndex = 1;
         this.buttonReturn.Text = "Add Selected Species";
         this.buttonReturn.UseVisualStyleBackColor = false;
         this.buttonReturn.Click += new System.EventHandler(this.buttonReturn_Click);
         // 
         // buttonNew
         // 
         this.buttonNew.BackColor = System.Drawing.SystemColors.ButtonFace;
         this.buttonNew.Dock = System.Windows.Forms.DockStyle.Left;
         this.buttonNew.Location = new System.Drawing.Point(0, 0);
         this.buttonNew.Margin = new System.Windows.Forms.Padding(2);
         this.buttonNew.Name = "buttonNew";
         this.buttonNew.Size = new System.Drawing.Size(121, 28);
         this.buttonNew.TabIndex = 0;
         this.buttonNew.Text = "Create New Species";
         this.buttonNew.UseVisualStyleBackColor = false;
         this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
         // 
         // selectedItemsGridView1
         // 
         this.selectedItemsGridView1.AllowUserToAddRows = false;
         this.selectedItemsGridView1.AllowUserToDeleteRows = false;
         this.selectedItemsGridView1.AutoGenerateColumns = false;
         this.selectedItemsGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.selectedItemsGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.speciesDataGridViewTextBoxColumn,
            this.validatorDataGridViewTextBoxColumn,
            this.treeDefaultValueCNDataGridViewTextBoxColumn,
            this.primaryProductDataGridViewTextBoxColumn,
            this.liveDeadDataGridViewTextBoxColumn,
            this.cullPrimaryDataGridViewTextBoxColumn,
            this.hiddenPrimaryDataGridViewTextBoxColumn,
            this.cullSecondaryDataGridViewTextBoxColumn,
            this.hiddenSecondaryDataGridViewTextBoxColumn,
            this.recoverableDataGridViewTextBoxColumn,
            this.contractSpeciesDataGridViewTextBoxColumn,
            this.merchHeightLogLengthDataGridViewTextBoxColumn,
            this.merchHeightTypeDataGridViewTextBoxColumn,
            this.formClassDataGridViewTextBoxColumn,
            this.treeGradeDataGridViewTextBoxColumn,
            this.barkThicknessRatioDataGridViewTextBoxColumn,
            this.averageZDataGridViewTextBoxColumn,
            this.referenceHeightPercentDataGridViewTextBoxColumn,
            this.rowIDDataGridViewTextBoxColumn,
            this.dALDataGridViewTextBoxColumn,
            this.isPersistedDataGridViewCheckBoxColumn,
            this.tagDataGridViewTextBoxColumn,
            this.errorDataGridViewTextBoxColumn});
         this.selectedItemsGridView1.DataSource = this.bindingSourceTreeDV;
         this.selectedItemsGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.selectedItemsGridView1.Location = new System.Drawing.Point(2, 2);
         this.selectedItemsGridView1.Margin = new System.Windows.Forms.Padding(2);
         this.selectedItemsGridView1.MultiSelect = false;
         this.selectedItemsGridView1.Name = "selectedItemsGridView1";
         this.selectedItemsGridView1.RowHeadersVisible = false;
         this.selectedItemsGridView1.RowTemplate.Height = 24;
         this.selectedItemsGridView1.SelectedItems = null;
         this.selectedItemsGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
         this.selectedItemsGridView1.Size = new System.Drawing.Size(330, 212);
         this.selectedItemsGridView1.TabIndex = 0;
         this.selectedItemsGridView1.VirtualMode = true;
         // 
         // bindingSourceTreeDV
         // 
         this.bindingSourceTreeDV.DataSource = typeof(CruiseDAL.DataObjects.TreeDefaultValueDO);
         this.bindingSourceTreeDV.Sort = "";
         // 
         // speciesDataGridViewTextBoxColumn
         // 
         this.speciesDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
         this.speciesDataGridViewTextBoxColumn.DataPropertyName = "Species";
         this.speciesDataGridViewTextBoxColumn.HeaderText = "Species";
         this.speciesDataGridViewTextBoxColumn.Name = "speciesDataGridViewTextBoxColumn";
         this.speciesDataGridViewTextBoxColumn.Width = 70;
         // 
         // validatorDataGridViewTextBoxColumn
         // 
         this.validatorDataGridViewTextBoxColumn.DataPropertyName = "Validator";
         this.validatorDataGridViewTextBoxColumn.HeaderText = "Validator";
         this.validatorDataGridViewTextBoxColumn.Name = "validatorDataGridViewTextBoxColumn";
         this.validatorDataGridViewTextBoxColumn.ReadOnly = true;
         this.validatorDataGridViewTextBoxColumn.Visible = false;
         // 
         // treeDefaultValueCNDataGridViewTextBoxColumn
         // 
         this.treeDefaultValueCNDataGridViewTextBoxColumn.DataPropertyName = "TreeDefaultValue_CN";
         this.treeDefaultValueCNDataGridViewTextBoxColumn.HeaderText = "TreeDefaultValue_CN";
         this.treeDefaultValueCNDataGridViewTextBoxColumn.Name = "treeDefaultValueCNDataGridViewTextBoxColumn";
         this.treeDefaultValueCNDataGridViewTextBoxColumn.ReadOnly = true;
         this.treeDefaultValueCNDataGridViewTextBoxColumn.Visible = false;
         // 
         // primaryProductDataGridViewTextBoxColumn
         // 
         this.primaryProductDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
         this.primaryProductDataGridViewTextBoxColumn.DataPropertyName = "PrimaryProduct";
         this.primaryProductDataGridViewTextBoxColumn.HeaderText = "Prod";
         this.primaryProductDataGridViewTextBoxColumn.Name = "primaryProductDataGridViewTextBoxColumn";
         this.primaryProductDataGridViewTextBoxColumn.ToolTipText = "Primary Product";
         this.primaryProductDataGridViewTextBoxColumn.Width = 54;
         // 
         // liveDeadDataGridViewTextBoxColumn
         // 
         this.liveDeadDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
         this.liveDeadDataGridViewTextBoxColumn.DataPropertyName = "LiveDead";
         this.liveDeadDataGridViewTextBoxColumn.HeaderText = "L/D";
         this.liveDeadDataGridViewTextBoxColumn.Name = "liveDeadDataGridViewTextBoxColumn";
         this.liveDeadDataGridViewTextBoxColumn.ToolTipText = "Live/Dead";
         this.liveDeadDataGridViewTextBoxColumn.Width = 51;
         // 
         // cullPrimaryDataGridViewTextBoxColumn
         // 
         this.cullPrimaryDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
         this.cullPrimaryDataGridViewTextBoxColumn.DataPropertyName = "CullPrimary";
         this.cullPrimaryDataGridViewTextBoxColumn.HeaderText = "C-P";
         this.cullPrimaryDataGridViewTextBoxColumn.Name = "cullPrimaryDataGridViewTextBoxColumn";
         this.cullPrimaryDataGridViewTextBoxColumn.ToolTipText = "Cull Defect Primary Product";
         this.cullPrimaryDataGridViewTextBoxColumn.Width = 49;
         // 
         // hiddenPrimaryDataGridViewTextBoxColumn
         // 
         this.hiddenPrimaryDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
         this.hiddenPrimaryDataGridViewTextBoxColumn.DataPropertyName = "HiddenPrimary";
         this.hiddenPrimaryDataGridViewTextBoxColumn.HeaderText = "H-P";
         this.hiddenPrimaryDataGridViewTextBoxColumn.Name = "hiddenPrimaryDataGridViewTextBoxColumn";
         this.hiddenPrimaryDataGridViewTextBoxColumn.ToolTipText = "Hidden Defect Primary Product";
         this.hiddenPrimaryDataGridViewTextBoxColumn.Width = 50;
         // 
         // cullSecondaryDataGridViewTextBoxColumn
         // 
         this.cullSecondaryDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
         this.cullSecondaryDataGridViewTextBoxColumn.DataPropertyName = "CullSecondary";
         this.cullSecondaryDataGridViewTextBoxColumn.HeaderText = "C-S";
         this.cullSecondaryDataGridViewTextBoxColumn.Name = "cullSecondaryDataGridViewTextBoxColumn";
         this.cullSecondaryDataGridViewTextBoxColumn.ToolTipText = "Cull Defect Secondary Product";
         this.cullSecondaryDataGridViewTextBoxColumn.Width = 49;
         // 
         // hiddenSecondaryDataGridViewTextBoxColumn
         // 
         this.hiddenSecondaryDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
         this.hiddenSecondaryDataGridViewTextBoxColumn.DataPropertyName = "HiddenSecondary";
         this.hiddenSecondaryDataGridViewTextBoxColumn.HeaderText = "H - S";
         this.hiddenSecondaryDataGridViewTextBoxColumn.Name = "hiddenSecondaryDataGridViewTextBoxColumn";
         this.hiddenSecondaryDataGridViewTextBoxColumn.ToolTipText = "Hidden Defect Secondary Product";
         this.hiddenSecondaryDataGridViewTextBoxColumn.Width = 56;
         // 
         // recoverableDataGridViewTextBoxColumn
         // 
         this.recoverableDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
         this.recoverableDataGridViewTextBoxColumn.DataPropertyName = "Recoverable";
         this.recoverableDataGridViewTextBoxColumn.HeaderText = "Recov";
         this.recoverableDataGridViewTextBoxColumn.Name = "recoverableDataGridViewTextBoxColumn";
         this.recoverableDataGridViewTextBoxColumn.ToolTipText = "Recoverable Percent";
         this.recoverableDataGridViewTextBoxColumn.Width = 64;
         // 
         // contractSpeciesDataGridViewTextBoxColumn
         // 
         this.contractSpeciesDataGridViewTextBoxColumn.DataPropertyName = "ContractSpecies";
         this.contractSpeciesDataGridViewTextBoxColumn.HeaderText = "ContractSpecies";
         this.contractSpeciesDataGridViewTextBoxColumn.Name = "contractSpeciesDataGridViewTextBoxColumn";
         this.contractSpeciesDataGridViewTextBoxColumn.Visible = false;
         // 
         // merchHeightLogLengthDataGridViewTextBoxColumn
         // 
         this.merchHeightLogLengthDataGridViewTextBoxColumn.DataPropertyName = "MerchHeightLogLength";
         this.merchHeightLogLengthDataGridViewTextBoxColumn.HeaderText = "MerchHeightLogLength";
         this.merchHeightLogLengthDataGridViewTextBoxColumn.Name = "merchHeightLogLengthDataGridViewTextBoxColumn";
         this.merchHeightLogLengthDataGridViewTextBoxColumn.Visible = false;
         // 
         // merchHeightTypeDataGridViewTextBoxColumn
         // 
         this.merchHeightTypeDataGridViewTextBoxColumn.DataPropertyName = "MerchHeightType";
         this.merchHeightTypeDataGridViewTextBoxColumn.HeaderText = "MerchHeightType";
         this.merchHeightTypeDataGridViewTextBoxColumn.Name = "merchHeightTypeDataGridViewTextBoxColumn";
         this.merchHeightTypeDataGridViewTextBoxColumn.Visible = false;
         // 
         // formClassDataGridViewTextBoxColumn
         // 
         this.formClassDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
         this.formClassDataGridViewTextBoxColumn.DataPropertyName = "FormClass";
         this.formClassDataGridViewTextBoxColumn.HeaderText = "Form";
         this.formClassDataGridViewTextBoxColumn.Name = "formClassDataGridViewTextBoxColumn";
         this.formClassDataGridViewTextBoxColumn.ToolTipText = "Form Class";
         this.formClassDataGridViewTextBoxColumn.Width = 55;
         // 
         // treeGradeDataGridViewTextBoxColumn
         // 
         this.treeGradeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
         this.treeGradeDataGridViewTextBoxColumn.DataPropertyName = "TreeGrade";
         this.treeGradeDataGridViewTextBoxColumn.HeaderText = "Grade";
         this.treeGradeDataGridViewTextBoxColumn.Name = "treeGradeDataGridViewTextBoxColumn";
         this.treeGradeDataGridViewTextBoxColumn.ToolTipText = "Tree Grade";
         this.treeGradeDataGridViewTextBoxColumn.Width = 61;
         // 
         // barkThicknessRatioDataGridViewTextBoxColumn
         // 
         this.barkThicknessRatioDataGridViewTextBoxColumn.DataPropertyName = "BarkThicknessRatio";
         this.barkThicknessRatioDataGridViewTextBoxColumn.HeaderText = "BarkThicknessRatio";
         this.barkThicknessRatioDataGridViewTextBoxColumn.Name = "barkThicknessRatioDataGridViewTextBoxColumn";
         this.barkThicknessRatioDataGridViewTextBoxColumn.Visible = false;
         // 
         // averageZDataGridViewTextBoxColumn
         // 
         this.averageZDataGridViewTextBoxColumn.DataPropertyName = "AverageZ";
         this.averageZDataGridViewTextBoxColumn.HeaderText = "AverageZ";
         this.averageZDataGridViewTextBoxColumn.Name = "averageZDataGridViewTextBoxColumn";
         this.averageZDataGridViewTextBoxColumn.Visible = false;
         // 
         // referenceHeightPercentDataGridViewTextBoxColumn
         // 
         this.referenceHeightPercentDataGridViewTextBoxColumn.DataPropertyName = "ReferenceHeightPercent";
         this.referenceHeightPercentDataGridViewTextBoxColumn.HeaderText = "ReferenceHeightPercent";
         this.referenceHeightPercentDataGridViewTextBoxColumn.Name = "referenceHeightPercentDataGridViewTextBoxColumn";
         this.referenceHeightPercentDataGridViewTextBoxColumn.Visible = false;
         // 
         // rowIDDataGridViewTextBoxColumn
         // 
         this.rowIDDataGridViewTextBoxColumn.DataPropertyName = "rowID";
         this.rowIDDataGridViewTextBoxColumn.HeaderText = "rowID";
         this.rowIDDataGridViewTextBoxColumn.Name = "rowIDDataGridViewTextBoxColumn";
         this.rowIDDataGridViewTextBoxColumn.Visible = false;
         // 
         // dALDataGridViewTextBoxColumn
         // 
         this.dALDataGridViewTextBoxColumn.DataPropertyName = "DAL";
         this.dALDataGridViewTextBoxColumn.HeaderText = "DAL";
         this.dALDataGridViewTextBoxColumn.Name = "dALDataGridViewTextBoxColumn";
         this.dALDataGridViewTextBoxColumn.Visible = false;
         // 
         // isPersistedDataGridViewCheckBoxColumn
         // 
         this.isPersistedDataGridViewCheckBoxColumn.DataPropertyName = "IsPersisted";
         this.isPersistedDataGridViewCheckBoxColumn.HeaderText = "IsPersisted";
         this.isPersistedDataGridViewCheckBoxColumn.Name = "isPersistedDataGridViewCheckBoxColumn";
         this.isPersistedDataGridViewCheckBoxColumn.ReadOnly = true;
         this.isPersistedDataGridViewCheckBoxColumn.Visible = false;
         // 
         // tagDataGridViewTextBoxColumn
         // 
         this.tagDataGridViewTextBoxColumn.DataPropertyName = "Tag";
         this.tagDataGridViewTextBoxColumn.HeaderText = "Tag";
         this.tagDataGridViewTextBoxColumn.Name = "tagDataGridViewTextBoxColumn";
         this.tagDataGridViewTextBoxColumn.Visible = false;
         // 
         // errorDataGridViewTextBoxColumn
         // 
         this.errorDataGridViewTextBoxColumn.DataPropertyName = "Error";
         this.errorDataGridViewTextBoxColumn.HeaderText = "Error";
         this.errorDataGridViewTextBoxColumn.Name = "errorDataGridViewTextBoxColumn";
         this.errorDataGridViewTextBoxColumn.ReadOnly = true;
         this.errorDataGridViewTextBoxColumn.Visible = false;
         // 
         // TDV_Select
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(334, 248);
         this.Controls.Add(this.tableLayoutPanel1);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Margin = new System.Windows.Forms.Padding(2);
         this.Name = "TDV_Select";
         this.Text = "Select Tree Default Value";
         this.tableLayoutPanel1.ResumeLayout(false);
         this.panel1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.selectedItemsGridView1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.bindingSourceTreeDV)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Button buttonReturn;
      private System.Windows.Forms.Button buttonNew;
      public System.Windows.Forms.BindingSource bindingSourceTreeDV;
      public FMSC.Controls.SelectedItemsGridView selectedItemsGridView1;
      private System.Windows.Forms.DataGridViewTextBoxColumn persisterDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn speciesDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn validatorDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn treeDefaultValueCNDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn primaryProductDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn liveDeadDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn cullPrimaryDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn hiddenPrimaryDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn cullSecondaryDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn hiddenSecondaryDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn recoverableDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn contractSpeciesDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn merchHeightLogLengthDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn merchHeightTypeDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn formClassDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn treeGradeDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn barkThicknessRatioDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn averageZDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn referenceHeightPercentDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn rowIDDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn dALDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewCheckBoxColumn isPersistedDataGridViewCheckBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn tagDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn errorDataGridViewTextBoxColumn;
   }
}