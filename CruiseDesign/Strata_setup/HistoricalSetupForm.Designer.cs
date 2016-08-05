namespace CruiseDesign.Strata_setup
{
    partial class HistoricalSetupForm
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
            System.Windows.Forms.ListBox listBoxStrata;
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonViewTreeDef = new System.Windows.Forms.Button();
            this.listBoxDefStrata = new System.Windows.Forms.ListBox();
            this.dataGridViewUnits = new System.Windows.Forms.DataGridView();
            this.dataGridViewSG = new System.Windows.Forms.DataGridView();
            this.textBoxFilename = new System.Windows.Forms.TextBox();
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonFinish = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.sideLabelTextBoxStr = new FMSC.Controls.SideLabelTextBox();
            this.sideLabelTextBoxDescr = new FMSC.Controls.SideLabelTextBox();
            listBoxStrata = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUnits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSG)).BeginInit();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxStrata
            // 
            listBoxStrata.Dock = System.Windows.Forms.DockStyle.Fill;
            listBoxStrata.FormattingEnabled = true;
            listBoxStrata.ItemHeight = 17;
            listBoxStrata.Items.AddRange(new object[] {
            "Select Strata"});
            listBoxStrata.Location = new System.Drawing.Point(3, 251);
            listBoxStrata.Name = "listBoxStrata";
            listBoxStrata.Size = new System.Drawing.Size(154, 276);
            listBoxStrata.TabIndex = 6;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonViewTreeDef);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(277, 534);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(444, 28);
            this.panel1.TabIndex = 10;
            // 
            // buttonViewTreeDef
            // 
            this.buttonViewTreeDef.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonViewTreeDef.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonViewTreeDef.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonViewTreeDef.Location = new System.Drawing.Point(283, 0);
            this.buttonViewTreeDef.Name = "buttonViewTreeDef";
            this.buttonViewTreeDef.Size = new System.Drawing.Size(161, 28);
            this.buttonViewTreeDef.TabIndex = 2;
            this.buttonViewTreeDef.Text = "View Tree Defaults";
            this.buttonViewTreeDef.UseVisualStyleBackColor = false;
            // 
            // listBoxDefStrata
            // 
            this.listBoxDefStrata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxDefStrata.FormattingEnabled = true;
            this.listBoxDefStrata.ItemHeight = 17;
            this.listBoxDefStrata.Items.AddRange(new object[] {
            "Strata  \tUnits"});
            this.listBoxDefStrata.Location = new System.Drawing.Point(3, 3);
            this.listBoxDefStrata.Name = "listBoxDefStrata";
            this.tableLayoutPanel1.SetRowSpan(this.listBoxDefStrata, 2);
            this.listBoxDefStrata.Size = new System.Drawing.Size(154, 191);
            this.listBoxDefStrata.TabIndex = 9;
            this.listBoxDefStrata.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // dataGridViewUnits
            // 
            this.dataGridViewUnits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridViewUnits, 2);
            this.dataGridViewUnits.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.dataGridViewUnits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewUnits.Location = new System.Drawing.Point(163, 37);
            this.dataGridViewUnits.Name = "dataGridViewUnits";
            this.dataGridViewUnits.RowTemplate.Height = 24;
            this.dataGridViewUnits.Size = new System.Drawing.Size(558, 170);
            this.dataGridViewUnits.TabIndex = 8;
            // 
            // dataGridViewSG
            // 
            this.dataGridViewSG.AllowUserToAddRows = false;
            this.dataGridViewSG.AllowUserToDeleteRows = false;
            this.dataGridViewSG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridViewSG, 2);
            this.dataGridViewSG.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.dataGridViewSG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSG.Location = new System.Drawing.Point(163, 251);
            this.dataGridViewSG.Name = "dataGridViewSG";
            this.dataGridViewSG.RowTemplate.Height = 24;
            this.dataGridViewSG.Size = new System.Drawing.Size(558, 277);
            this.dataGridViewSG.TabIndex = 1;
            // 
            // textBoxFilename
            // 
            this.textBoxFilename.BackColor = System.Drawing.Color.Gainsboro;
            this.tableLayoutPanel1.SetColumnSpan(this.textBoxFilename, 2);
            this.textBoxFilename.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxFilename.Location = new System.Drawing.Point(163, 215);
            this.textBoxFilename.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.textBoxFilename.Name = "textBoxFilename";
            this.textBoxFilename.ReadOnly = true;
            this.textBoxFilename.Size = new System.Drawing.Size(558, 23);
            this.textBoxFilename.TabIndex = 3;
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonOpenFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonOpenFile.Location = new System.Drawing.Point(5, 215);
            this.buttonOpenFile.Margin = new System.Windows.Forms.Padding(5);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(150, 27);
            this.buttonOpenFile.TabIndex = 2;
            this.buttonOpenFile.Text = "Select Cruise File";
            this.buttonOpenFile.UseVisualStyleBackColor = false;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.buttonSave);
            this.panel3.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(163, 534);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(108, 28);
            this.panel3.TabIndex = 4;
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonSave.Location = new System.Drawing.Point(-3, 0);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(111, 28);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "Save Stratum";
            this.buttonSave.UseVisualStyleBackColor = false;
            // 
            // buttonFinish
            // 
            this.buttonFinish.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonFinish.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonFinish.Location = new System.Drawing.Point(3, 534);
            this.buttonFinish.Name = "buttonFinish";
            this.buttonFinish.Size = new System.Drawing.Size(125, 28);
            this.buttonFinish.TabIndex = 1;
            this.buttonFinish.Text = "Finish";
            this.buttonFinish.UseVisualStyleBackColor = false;
            this.buttonFinish.Click += new System.EventHandler(this.buttonFinish_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 114F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.buttonFinish, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.buttonOpenFile, 0, 2);
            this.tableLayoutPanel1.Controls.Add(listBoxStrata, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBoxFilename, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewSG, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewUnits, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.listBoxDefStrata, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.sideLabelTextBoxStr, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.sideLabelTextBoxDescr, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 176F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(724, 565);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // sideLabelTextBoxStr
            // 
            this.sideLabelTextBoxStr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.sideLabelTextBoxStr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sideLabelTextBoxStr.LabelTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.sideLabelTextBoxStr.LabelWidth = 752F;
            this.sideLabelTextBoxStr.LableText = "Code";
            this.sideLabelTextBoxStr.Location = new System.Drawing.Point(160, 0);
            this.sideLabelTextBoxStr.Margin = new System.Windows.Forms.Padding(0);
            this.sideLabelTextBoxStr.Name = "sideLabelTextBoxStr";
            this.sideLabelTextBoxStr.Size = new System.Drawing.Size(114, 34);
            this.sideLabelTextBoxStr.TabIndex = 11;
            this.sideLabelTextBoxStr.Text = "sideLabelTextBox1";
            this.sideLabelTextBoxStr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.sideLabelTextBoxStr_KeyPress);
            // 
            // sideLabelTextBoxDescr
            // 
            this.sideLabelTextBoxDescr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.sideLabelTextBoxDescr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sideLabelTextBoxDescr.LabelTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.sideLabelTextBoxDescr.LabelWidth = 1301F;
            this.sideLabelTextBoxDescr.LableText = "Description";
            this.sideLabelTextBoxDescr.Location = new System.Drawing.Point(274, 0);
            this.sideLabelTextBoxDescr.Margin = new System.Windows.Forms.Padding(0);
            this.sideLabelTextBoxDescr.Name = "sideLabelTextBoxDescr";
            this.sideLabelTextBoxDescr.Size = new System.Drawing.Size(450, 34);
            this.sideLabelTextBoxDescr.TabIndex = 12;
            this.sideLabelTextBoxDescr.Text = "sideLabelTextBox1";
            // 
            // HistoricalSetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(724, 565);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(668, 545);
            this.Name = "HistoricalSetupForm";
            this.Text = "Historical Data Setup Form";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUnits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSG)).EndInit();
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonViewTreeDef;
        private System.Windows.Forms.ListBox listBoxDefStrata;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonFinish;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.TextBox textBoxFilename;
        private System.Windows.Forms.DataGridView dataGridViewSG;
        private System.Windows.Forms.DataGridView dataGridViewUnits;
        private FMSC.Controls.SideLabelTextBox sideLabelTextBoxStr;
        private FMSC.Controls.SideLabelTextBox sideLabelTextBoxDescr;

    }
}