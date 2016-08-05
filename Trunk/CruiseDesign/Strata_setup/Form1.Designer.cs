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
         this.selectedItemsGridView1 = new FMSC.Controls.SelectedItemsGridView();
         this.bindingSourceTDV = new System.Windows.Forms.BindingSource(this.components);
         this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
         this.speciesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.validatorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.persisterDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.treeDefaultValueCNDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.primaryProductDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.liveDeadDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.cullPrimaryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.hiddenPrimaryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.cullSecondaryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.hiddenSecondaryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.recoverableDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.chargeableDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.contractSpeciesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.treeGradeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.merchHeightLogLengthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.merchHeightTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.formClassDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.barkThicknessRatioDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.averageZDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.referenceHeightPercentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.rowIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.dALDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.isPersistedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
         this.tagDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.errorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
         this.tableLayoutPanel1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.selectedItemsGridView1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.bindingSourceTDV)).BeginInit();
         this.SuspendLayout();
         // 
         // tableLayoutPanel1
         // 
         this.tableLayoutPanel1.ColumnCount = 1;
         this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
         this.tableLayoutPanel1.Controls.Add(this.selectedItemsGridView1, 0, 0);
         this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
         this.tableLayoutPanel1.Name = "tableLayoutPanel1";
         this.tableLayoutPanel1.RowCount = 2;
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
         this.tableLayoutPanel1.Size = new System.Drawing.Size(388, 305);
         this.tableLayoutPanel1.TabIndex = 0;
         // 
         // selectedItemsGridView1
         // 
         this.selectedItemsGridView1.AllowUserToAddRows = false;
         this.selectedItemsGridView1.AutoGenerateColumns = false;
         this.selectedItemsGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
         this.selectedItemsGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.speciesDataGridViewTextBoxColumn,
            this.validatorDataGridViewTextBoxColumn,
            this.persisterDataGridViewTextBoxColumn,
            this.treeDefaultValueCNDataGridViewTextBoxColumn,
            this.primaryProductDataGridViewTextBoxColumn,
            this.liveDeadDataGridViewTextBoxColumn,
            this.cullPrimaryDataGridViewTextBoxColumn,
            this.hiddenPrimaryDataGridViewTextBoxColumn,
            this.cullSecondaryDataGridViewTextBoxColumn,
            this.hiddenSecondaryDataGridViewTextBoxColumn,
            this.recoverableDataGridViewTextBoxColumn,
            this.chargeableDataGridViewTextBoxColumn,
            this.contractSpeciesDataGridViewTextBoxColumn,
            this.treeGradeDataGridViewTextBoxColumn,
            this.merchHeightLogLengthDataGridViewTextBoxColumn,
            this.merchHeightTypeDataGridViewTextBoxColumn,
            this.formClassDataGridViewTextBoxColumn,
            this.barkThicknessRatioDataGridViewTextBoxColumn,
            this.averageZDataGridViewTextBoxColumn,
            this.referenceHeightPercentDataGridViewTextBoxColumn,
            this.rowIDDataGridViewTextBoxColumn,
            this.dALDataGridViewTextBoxColumn,
            this.isPersistedDataGridViewCheckBoxColumn,
            this.tagDataGridViewTextBoxColumn,
            this.errorDataGridViewTextBoxColumn});
         this.selectedItemsGridView1.DataSource = this.bindingSourceTDV;
         this.selectedItemsGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.selectedItemsGridView1.Location = new System.Drawing.Point(3, 3);
         this.selectedItemsGridView1.MultiSelect = false;
         this.selectedItemsGridView1.Name = "selectedItemsGridView1";
         this.selectedItemsGridView1.RowHeadersVisible = false;
         this.selectedItemsGridView1.RowTemplate.Height = 24;
         this.selectedItemsGridView1.SelectedItems = null;
         this.selectedItemsGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
         this.selectedItemsGridView1.Size = new System.Drawing.Size(382, 259);
         this.selectedItemsGridView1.TabIndex = 0;
         this.selectedItemsGridView1.VirtualMode = true;
         // 
         // bindingSourceTDV
         // 
         this.bindingSourceTDV.DataSource = typeof(CruiseDAL.DataObjects.TreeDefaultValueDO);
         // 
         // dataGridViewCheckBoxColumn1
         // 
         this.dataGridViewCheckBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
         this.dataGridViewCheckBoxColumn1.HeaderText = "Select";
         this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
         this.dataGridViewCheckBoxColumn1.Width = 53;
         // 
         // speciesDataGridViewTextBoxColumn
         // 
         this.speciesDataGridViewTextBoxColumn.DataPropertyName = "Species";
         this.speciesDataGridViewTextBoxColumn.HeaderText = "Species";
         this.speciesDataGridViewTextBoxColumn.Name = "speciesDataGridViewTextBoxColumn";
         // 
         // validatorDataGridViewTextBoxColumn
         // 
         this.validatorDataGridViewTextBoxColumn.DataPropertyName = "Validator";
         this.validatorDataGridViewTextBoxColumn.HeaderText = "Validator";
         this.validatorDataGridViewTextBoxColumn.Name = "validatorDataGridViewTextBoxColumn";
         this.validatorDataGridViewTextBoxColumn.ReadOnly = true;
         this.validatorDataGridViewTextBoxColumn.Visible = false;
         // 
         // persisterDataGridViewTextBoxColumn
         // 
         this.persisterDataGridViewTextBoxColumn.DataPropertyName = "Persister";
         this.persisterDataGridViewTextBoxColumn.HeaderText = "Persister";
         this.persisterDataGridViewTextBoxColumn.Name = "persisterDataGridViewTextBoxColumn";
         this.persisterDataGridViewTextBoxColumn.ReadOnly = true;
         this.persisterDataGridViewTextBoxColumn.Visible = false;
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
         this.primaryProductDataGridViewTextBoxColumn.DataPropertyName = "PrimaryProduct";
         this.primaryProductDataGridViewTextBoxColumn.HeaderText = "PProd";
         this.primaryProductDataGridViewTextBoxColumn.Name = "primaryProductDataGridViewTextBoxColumn";
         // 
         // liveDeadDataGridViewTextBoxColumn
         // 
         this.liveDeadDataGridViewTextBoxColumn.DataPropertyName = "LiveDead";
         this.liveDeadDataGridViewTextBoxColumn.HeaderText = "L/D";
         this.liveDeadDataGridViewTextBoxColumn.Name = "liveDeadDataGridViewTextBoxColumn";
         // 
         // cullPrimaryDataGridViewTextBoxColumn
         // 
         this.cullPrimaryDataGridViewTextBoxColumn.DataPropertyName = "CullPrimary";
         this.cullPrimaryDataGridViewTextBoxColumn.HeaderText = "C-P";
         this.cullPrimaryDataGridViewTextBoxColumn.Name = "cullPrimaryDataGridViewTextBoxColumn";
         this.cullPrimaryDataGridViewTextBoxColumn.ToolTipText = "Cull Defect Primary Product";
         // 
         // hiddenPrimaryDataGridViewTextBoxColumn
         // 
         this.hiddenPrimaryDataGridViewTextBoxColumn.DataPropertyName = "HiddenPrimary";
         this.hiddenPrimaryDataGridViewTextBoxColumn.HeaderText = "H-P";
         this.hiddenPrimaryDataGridViewTextBoxColumn.Name = "hiddenPrimaryDataGridViewTextBoxColumn";
         this.hiddenPrimaryDataGridViewTextBoxColumn.ToolTipText = "Hidden Defect Primary Product";
         // 
         // cullSecondaryDataGridViewTextBoxColumn
         // 
         this.cullSecondaryDataGridViewTextBoxColumn.DataPropertyName = "CullSecondary";
         this.cullSecondaryDataGridViewTextBoxColumn.HeaderText = "C-S";
         this.cullSecondaryDataGridViewTextBoxColumn.Name = "cullSecondaryDataGridViewTextBoxColumn";
         this.cullSecondaryDataGridViewTextBoxColumn.ToolTipText = "Cull Defect Secondary Product";
         // 
         // hiddenSecondaryDataGridViewTextBoxColumn
         // 
         this.hiddenSecondaryDataGridViewTextBoxColumn.DataPropertyName = "HiddenSecondary";
         this.hiddenSecondaryDataGridViewTextBoxColumn.HeaderText = "H - S";
         this.hiddenSecondaryDataGridViewTextBoxColumn.Name = "hiddenSecondaryDataGridViewTextBoxColumn";
         this.hiddenSecondaryDataGridViewTextBoxColumn.ToolTipText = "Hidden Defect Secondary Product";
         // 
         // recoverableDataGridViewTextBoxColumn
         // 
         this.recoverableDataGridViewTextBoxColumn.DataPropertyName = "Recoverable";
         this.recoverableDataGridViewTextBoxColumn.HeaderText = "Recoverable";
         this.recoverableDataGridViewTextBoxColumn.Name = "recoverableDataGridViewTextBoxColumn";
         // 
         // chargeableDataGridViewTextBoxColumn
         // 
         this.chargeableDataGridViewTextBoxColumn.DataPropertyName = "Chargeable";
         this.chargeableDataGridViewTextBoxColumn.HeaderText = "Chargeable";
         this.chargeableDataGridViewTextBoxColumn.Name = "chargeableDataGridViewTextBoxColumn";
         // 
         // contractSpeciesDataGridViewTextBoxColumn
         // 
         this.contractSpeciesDataGridViewTextBoxColumn.DataPropertyName = "ContractSpecies";
         this.contractSpeciesDataGridViewTextBoxColumn.HeaderText = "ContractSpecies";
         this.contractSpeciesDataGridViewTextBoxColumn.Name = "contractSpeciesDataGridViewTextBoxColumn";
         // 
         // treeGradeDataGridViewTextBoxColumn
         // 
         this.treeGradeDataGridViewTextBoxColumn.DataPropertyName = "TreeGrade";
         this.treeGradeDataGridViewTextBoxColumn.HeaderText = "TreeGrade";
         this.treeGradeDataGridViewTextBoxColumn.Name = "treeGradeDataGridViewTextBoxColumn";
         // 
         // merchHeightLogLengthDataGridViewTextBoxColumn
         // 
         this.merchHeightLogLengthDataGridViewTextBoxColumn.DataPropertyName = "MerchHeightLogLength";
         this.merchHeightLogLengthDataGridViewTextBoxColumn.HeaderText = "MerchHeightLogLength";
         this.merchHeightLogLengthDataGridViewTextBoxColumn.Name = "merchHeightLogLengthDataGridViewTextBoxColumn";
         // 
         // merchHeightTypeDataGridViewTextBoxColumn
         // 
         this.merchHeightTypeDataGridViewTextBoxColumn.DataPropertyName = "MerchHeightType";
         this.merchHeightTypeDataGridViewTextBoxColumn.HeaderText = "MerchHeightType";
         this.merchHeightTypeDataGridViewTextBoxColumn.Name = "merchHeightTypeDataGridViewTextBoxColumn";
         // 
         // formClassDataGridViewTextBoxColumn
         // 
         this.formClassDataGridViewTextBoxColumn.DataPropertyName = "FormClass";
         this.formClassDataGridViewTextBoxColumn.HeaderText = "FormClass";
         this.formClassDataGridViewTextBoxColumn.Name = "formClassDataGridViewTextBoxColumn";
         // 
         // barkThicknessRatioDataGridViewTextBoxColumn
         // 
         this.barkThicknessRatioDataGridViewTextBoxColumn.DataPropertyName = "BarkThicknessRatio";
         this.barkThicknessRatioDataGridViewTextBoxColumn.HeaderText = "BarkThicknessRatio";
         this.barkThicknessRatioDataGridViewTextBoxColumn.Name = "barkThicknessRatioDataGridViewTextBoxColumn";
         // 
         // averageZDataGridViewTextBoxColumn
         // 
         this.averageZDataGridViewTextBoxColumn.DataPropertyName = "AverageZ";
         this.averageZDataGridViewTextBoxColumn.HeaderText = "AverageZ";
         this.averageZDataGridViewTextBoxColumn.Name = "averageZDataGridViewTextBoxColumn";
         // 
         // referenceHeightPercentDataGridViewTextBoxColumn
         // 
         this.referenceHeightPercentDataGridViewTextBoxColumn.DataPropertyName = "ReferenceHeightPercent";
         this.referenceHeightPercentDataGridViewTextBoxColumn.HeaderText = "ReferenceHeightPercent";
         this.referenceHeightPercentDataGridViewTextBoxColumn.Name = "referenceHeightPercentDataGridViewTextBoxColumn";
         // 
         // rowIDDataGridViewTextBoxColumn
         // 
         this.rowIDDataGridViewTextBoxColumn.DataPropertyName = "rowID";
         this.rowIDDataGridViewTextBoxColumn.HeaderText = "rowID";
         this.rowIDDataGridViewTextBoxColumn.Name = "rowIDDataGridViewTextBoxColumn";
         // 
         // dALDataGridViewTextBoxColumn
         // 
         this.dALDataGridViewTextBoxColumn.DataPropertyName = "DAL";
         this.dALDataGridViewTextBoxColumn.HeaderText = "DAL";
         this.dALDataGridViewTextBoxColumn.Name = "dALDataGridViewTextBoxColumn";
         // 
         // isPersistedDataGridViewCheckBoxColumn
         // 
         this.isPersistedDataGridViewCheckBoxColumn.DataPropertyName = "IsPersisted";
         this.isPersistedDataGridViewCheckBoxColumn.HeaderText = "IsPersisted";
         this.isPersistedDataGridViewCheckBoxColumn.Name = "isPersistedDataGridViewCheckBoxColumn";
         // 
         // tagDataGridViewTextBoxColumn
         // 
         this.tagDataGridViewTextBoxColumn.DataPropertyName = "Tag";
         this.tagDataGridViewTextBoxColumn.HeaderText = "Tag";
         this.tagDataGridViewTextBoxColumn.Name = "tagDataGridViewTextBoxColumn";
         // 
         // errorDataGridViewTextBoxColumn
         // 
         this.errorDataGridViewTextBoxColumn.DataPropertyName = "Error";
         this.errorDataGridViewTextBoxColumn.HeaderText = "Error";
         this.errorDataGridViewTextBoxColumn.Name = "errorDataGridViewTextBoxColumn";
         this.errorDataGridViewTextBoxColumn.ReadOnly = true;
         // 
         // TDV_Select
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(388, 305);
         this.Controls.Add(this.tableLayoutPanel1);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Name = "TDV_Select";
         this.Text = "Select Tree Default Value";
         this.tableLayoutPanel1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.selectedItemsGridView1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.bindingSourceTDV)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
      private FMSC.Controls.SelectedItemsGridView selectedItemsGridView1;
      private System.Windows.Forms.BindingSource bindingSourceTDV;
      private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
      private System.Windows.Forms.DataGridViewTextBoxColumn speciesDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn validatorDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn persisterDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn treeDefaultValueCNDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn primaryProductDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn liveDeadDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn cullPrimaryDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn hiddenPrimaryDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn cullSecondaryDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn hiddenSecondaryDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn recoverableDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn chargeableDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn contractSpeciesDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn treeGradeDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn merchHeightLogLengthDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn merchHeightTypeDataGridViewTextBoxColumn;
      private System.Windows.Forms.DataGridViewTextBoxColumn formClassDataGridViewTextBoxColumn;
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