namespace CruiseDesign.Strata_setup
{
    partial class UserControlHistoricalSetup
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
            System.Windows.Forms.ListBox listBoxStrata;
            System.Windows.Forms.ListBox listBoxDefStrata;
            this.dataGridViewSG = new System.Windows.Forms.DataGridView();
            this.dataGridViewUnits = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxStratum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonViewTreeDef = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxFilename = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonFinish = new System.Windows.Forms.Button();
            listBoxStrata = new System.Windows.Forms.ListBox();
            listBoxDefStrata = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUnits)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxStrata
            // 
            listBoxStrata.FormattingEnabled = true;
            listBoxStrata.ItemHeight = 20;
            listBoxStrata.Location = new System.Drawing.Point(3, 231);
            listBoxStrata.Name = "listBoxStrata";
            this.tableLayoutPanel1.SetRowSpan(listBoxStrata, 2);
            listBoxStrata.Size = new System.Drawing.Size(154, 204);
            listBoxStrata.TabIndex = 6;
            listBoxStrata.UseWaitCursor = true;
            // 
            // listBoxDefStrata
            // 
            listBoxDefStrata.Dock = System.Windows.Forms.DockStyle.Fill;
            listBoxDefStrata.FormattingEnabled = true;
            listBoxDefStrata.ItemHeight = 20;
            listBoxDefStrata.Location = new System.Drawing.Point(3, 3);
            listBoxDefStrata.Name = "listBoxDefStrata";
            this.tableLayoutPanel1.SetRowSpan(listBoxDefStrata, 2);
            listBoxDefStrata.Size = new System.Drawing.Size(154, 184);
            listBoxDefStrata.TabIndex = 9;
            listBoxDefStrata.UseWaitCursor = true;
            // 
            // dataGridViewSG
            // 
            this.dataGridViewSG.AllowUserToAddRows = false;
            this.dataGridViewSG.AllowUserToDeleteRows = false;
            this.dataGridViewSG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSG.Location = new System.Drawing.Point(163, 231);
            this.dataGridViewSG.Name = "dataGridViewSG";
            this.dataGridViewSG.RowTemplate.Height = 24;
            this.dataGridViewSG.Size = new System.Drawing.Size(461, 175);
            this.dataGridViewSG.TabIndex = 1;
            this.dataGridViewSG.UseWaitCursor = true;
            // 
            // dataGridViewUnits
            // 
            this.dataGridViewUnits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUnits.Location = new System.Drawing.Point(163, 37);
            this.dataGridViewUnits.Name = "dataGridViewUnits";
            this.dataGridViewUnits.RowTemplate.Height = 24;
            this.dataGridViewUnits.Size = new System.Drawing.Size(461, 150);
            this.dataGridViewUnits.TabIndex = 8;
            this.dataGridViewUnits.UseWaitCursor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.textBoxDescription);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.textBoxStratum);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(163, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(461, 28);
            this.panel4.TabIndex = 10;
            this.panel4.UseWaitCursor = true;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(275, 0);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(186, 27);
            this.textBoxDescription.TabIndex = 3;
            this.textBoxDescription.UseWaitCursor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(179, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description";
            this.label2.UseWaitCursor = true;
            // 
            // textBoxStratum
            // 
            this.textBoxStratum.Location = new System.Drawing.Point(105, 0);
            this.textBoxStratum.Name = "textBoxStratum";
            this.textBoxStratum.Size = new System.Drawing.Size(62, 27);
            this.textBoxStratum.TabIndex = 1;
            this.textBoxStratum.UseWaitCursor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-4, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Stratum Code";
            this.label1.UseWaitCursor = true;
            // 
            // buttonViewTreeDef
            // 
            this.buttonViewTreeDef.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonViewTreeDef.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonViewTreeDef.Location = new System.Drawing.Point(300, 0);
            this.buttonViewTreeDef.Name = "buttonViewTreeDef";
            this.buttonViewTreeDef.Size = new System.Drawing.Size(161, 35);
            this.buttonViewTreeDef.TabIndex = 1;
            this.buttonViewTreeDef.Text = "View Tree Defaults";
            this.buttonViewTreeDef.UseVisualStyleBackColor = false;
            this.buttonViewTreeDef.UseWaitCursor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.buttonViewTreeDef);
            this.panel3.Controls.Add(this.buttonSave);
            this.panel3.Location = new System.Drawing.Point(163, 412);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(461, 35);
            this.panel3.TabIndex = 4;
            this.panel3.UseWaitCursor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonSave.Location = new System.Drawing.Point(0, 0);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(125, 35);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "Save Stratum";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.UseWaitCursor = true;
            // 
            // textBoxFilename
            // 
            this.textBoxFilename.BackColor = System.Drawing.Color.Gainsboro;
            this.textBoxFilename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxFilename.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.textBoxFilename.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFilename.Location = new System.Drawing.Point(162, 192);
            this.textBoxFilename.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxFilename.Name = "textBoxFilename";
            this.textBoxFilename.ReadOnly = true;
            this.textBoxFilename.Size = new System.Drawing.Size(463, 27);
            this.textBoxFilename.TabIndex = 3;
            this.textBoxFilename.UseWaitCursor = true;
            this.textBoxFilename.TextChanged += new System.EventHandler(this.textBoxFilename_TextChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.LemonChiffon;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.buttonOpenFile, 0, 2);
            this.tableLayoutPanel1.Controls.Add(listBoxStrata, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBoxFilename, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewSG, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewUnits, 1, 1);
            this.tableLayoutPanel1.Controls.Add(listBoxDefStrata, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 181F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(627, 493);
            this.tableLayoutPanel1.TabIndex = 4;
            this.tableLayoutPanel1.UseWaitCursor = true;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonOpenFile.Location = new System.Drawing.Point(5, 195);
            this.buttonOpenFile.Margin = new System.Windows.Forms.Padding(5);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(150, 27);
            this.buttonOpenFile.TabIndex = 2;
            this.buttonOpenFile.Text = "Select Cruise File";
            this.buttonOpenFile.UseVisualStyleBackColor = false;
            this.buttonOpenFile.UseWaitCursor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel2.Controls.Add(this.buttonFinish);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 449);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(627, 44);
            this.panel2.TabIndex = 6;
            this.panel2.UseWaitCursor = true;
            // 
            // buttonFinish
            // 
            this.buttonFinish.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonFinish.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFinish.Location = new System.Drawing.Point(7, 6);
            this.buttonFinish.Name = "buttonFinish";
            this.buttonFinish.Size = new System.Drawing.Size(125, 32);
            this.buttonFinish.TabIndex = 1;
            this.buttonFinish.Text = "Finish";
            this.buttonFinish.UseVisualStyleBackColor = false;
            this.buttonFinish.UseWaitCursor = true;
            // 
            // UserControlHistoricalSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UserControlHistoricalSetup";
            this.Size = new System.Drawing.Size(627, 493);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUnits)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button buttonViewTreeDef;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.TextBox textBoxFilename;
        private System.Windows.Forms.DataGridView dataGridViewSG;
        private System.Windows.Forms.DataGridView dataGridViewUnits;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxStratum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonFinish;
    }
}
