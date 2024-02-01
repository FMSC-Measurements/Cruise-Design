namespace CruiseDesign.Historical_setup
{
   partial class SaleSetupPage
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
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaleSetupPage));
         this.textBoxFile = new System.Windows.Forms.TextBox();
         this.buttonBrowse = new System.Windows.Forms.Button();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.label7 = new System.Windows.Forms.Label();
         this.label6 = new System.Windows.Forms.Label();
         this.label5 = new System.Windows.Forms.Label();
         this.label4 = new System.Windows.Forms.Label();
         this.label3 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.label1 = new System.Windows.Forms.Label();
         this.textBoxDist = new System.Windows.Forms.TextBox();
         this.comboBoxFor = new System.Windows.Forms.ComboBox();
         this.comboBoxReg = new System.Windows.Forms.ComboBox();
         this.comboBoxUOM = new System.Windows.Forms.ComboBox();
         this.comboBoxPurpose = new System.Windows.Forms.ComboBox();
         this.textBoxName = new System.Windows.Forms.TextBox();
         this.textBoxNum = new System.Windows.Forms.TextBox();
         this.buttonFinish = new System.Windows.Forms.Button();
         this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
         this.labelWorking = new System.Windows.Forms.Label();
         this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
         this.pictureBox1 = new System.Windows.Forms.PictureBox();
         this.checkBoxLogData = new System.Windows.Forms.CheckBox();
         this.groupBox1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
         this.SuspendLayout();
         // 
         // textBoxFile
         // 
         this.textBoxFile.Location = new System.Drawing.Point(16, 15);
         this.textBoxFile.Margin = new System.Windows.Forms.Padding(4);
         this.textBoxFile.Name = "textBoxFile";
         this.textBoxFile.Size = new System.Drawing.Size(400, 22);
         this.textBoxFile.TabIndex = 0;
         // 
         // buttonBrowse
         // 
         this.buttonBrowse.BackColor = System.Drawing.SystemColors.ButtonFace;
         this.buttonBrowse.Location = new System.Drawing.Point(425, 15);
         this.buttonBrowse.Margin = new System.Windows.Forms.Padding(4);
         this.buttonBrowse.Name = "buttonBrowse";
         this.buttonBrowse.Size = new System.Drawing.Size(100, 28);
         this.buttonBrowse.TabIndex = 1;
         this.buttonBrowse.Text = "Browse";
         this.buttonBrowse.UseVisualStyleBackColor = false;
         this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
         // 
         // groupBox1
         // 
         this.groupBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
         this.groupBox1.Controls.Add(this.checkBoxLogData);
         this.groupBox1.Controls.Add(this.label7);
         this.groupBox1.Controls.Add(this.label6);
         this.groupBox1.Controls.Add(this.label5);
         this.groupBox1.Controls.Add(this.label4);
         this.groupBox1.Controls.Add(this.label3);
         this.groupBox1.Controls.Add(this.label2);
         this.groupBox1.Controls.Add(this.label1);
         this.groupBox1.Controls.Add(this.textBoxDist);
         this.groupBox1.Controls.Add(this.comboBoxFor);
         this.groupBox1.Controls.Add(this.comboBoxReg);
         this.groupBox1.Controls.Add(this.comboBoxUOM);
         this.groupBox1.Controls.Add(this.comboBoxPurpose);
         this.groupBox1.Controls.Add(this.textBoxName);
         this.groupBox1.Controls.Add(this.textBoxNum);
         this.groupBox1.Location = new System.Drawing.Point(13, 50);
         this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
         this.groupBox1.Size = new System.Drawing.Size(577, 216);
         this.groupBox1.TabIndex = 2;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Sale Info";
         // 
         // label7
         // 
         this.label7.AutoSize = true;
         this.label7.Location = new System.Drawing.Point(288, 128);
         this.label7.Name = "label7";
         this.label7.Size = new System.Drawing.Size(48, 16);
         this.label7.TabIndex = 13;
         this.label7.Text = "District";
         // 
         // label6
         // 
         this.label6.AutoSize = true;
         this.label6.Location = new System.Drawing.Point(288, 82);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(46, 16);
         this.label6.TabIndex = 12;
         this.label6.Text = "Forest";
         // 
         // label5
         // 
         this.label5.AutoSize = true;
         this.label5.Location = new System.Drawing.Point(288, 36);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(52, 16);
         this.label5.TabIndex = 11;
         this.label5.Text = "Region";
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(17, 174);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(39, 16);
         this.label4.TabIndex = 10;
         this.label4.Text = "UOM";
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(17, 128);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(59, 16);
         this.label3.TabIndex = 9;
         this.label3.Text = "Purpose";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(17, 82);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(76, 16);
         this.label2.TabIndex = 8;
         this.label2.Text = "Sale Name";
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(17, 36);
         this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(87, 16);
         this.label1.TabIndex = 7;
         this.label1.Text = "Sale Number";
         // 
         // textBoxDist
         // 
         this.textBoxDist.Location = new System.Drawing.Point(347, 125);
         this.textBoxDist.Margin = new System.Windows.Forms.Padding(4);
         this.textBoxDist.Name = "textBoxDist";
         this.textBoxDist.Size = new System.Drawing.Size(68, 22);
         this.textBoxDist.TabIndex = 6;
         // 
         // comboBoxFor
         // 
         this.comboBoxFor.FormattingEnabled = true;
         this.comboBoxFor.Location = new System.Drawing.Point(347, 79);
         this.comboBoxFor.Margin = new System.Windows.Forms.Padding(4);
         this.comboBoxFor.Name = "comboBoxFor";
         this.comboBoxFor.Size = new System.Drawing.Size(215, 24);
         this.comboBoxFor.TabIndex = 5;
         this.comboBoxFor.SelectionChangeCommitted += new System.EventHandler(this.comboBoxFor_SelectionChangeCommitted);
         // 
         // comboBoxReg
         // 
         this.comboBoxReg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxReg.FormattingEnabled = true;
         this.comboBoxReg.Items.AddRange(new object[] {
            "Northern",
            "Rocky Mountain",
            "Southwestern",
            "Intermountain",
            "Pacific Southwest",
            "Pacific Northwest",
            "Southern",
            "Eastern",
            "Alaska",
            "BLM",
            "DOD"});
         this.comboBoxReg.Location = new System.Drawing.Point(347, 33);
         this.comboBoxReg.Margin = new System.Windows.Forms.Padding(4);
         this.comboBoxReg.Name = "comboBoxReg";
         this.comboBoxReg.Size = new System.Drawing.Size(215, 24);
         this.comboBoxReg.TabIndex = 4;
         this.comboBoxReg.SelectedIndexChanged += new System.EventHandler(this.comboBoxReg_SelectedIndexChanged);
         // 
         // comboBoxUOM
         // 
         this.comboBoxUOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxUOM.FormattingEnabled = true;
         this.comboBoxUOM.Items.AddRange(new object[] {
            "01 - Board Foot",
            "02 - Cords",
            "03 - Cubic Foot",
            "04 - Piece Count",
            "05 - Weight"});
         this.comboBoxUOM.Location = new System.Drawing.Point(112, 171);
         this.comboBoxUOM.Margin = new System.Windows.Forms.Padding(4);
         this.comboBoxUOM.Name = "comboBoxUOM";
         this.comboBoxUOM.Size = new System.Drawing.Size(160, 24);
         this.comboBoxUOM.TabIndex = 3;
         this.comboBoxUOM.SelectionChangeCommitted += new System.EventHandler(this.comboBoxUOM_SelectionChangeCommitted);
         // 
         // comboBoxPurpose
         // 
         this.comboBoxPurpose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxPurpose.FormattingEnabled = true;
         this.comboBoxPurpose.Items.AddRange(new object[] {
            "Timber Sale",
            "Check Cruise",
            "Right of Way",
            "Recon",
            "Other"});
         this.comboBoxPurpose.Location = new System.Drawing.Point(112, 125);
         this.comboBoxPurpose.Margin = new System.Windows.Forms.Padding(4);
         this.comboBoxPurpose.Name = "comboBoxPurpose";
         this.comboBoxPurpose.Size = new System.Drawing.Size(160, 24);
         this.comboBoxPurpose.TabIndex = 2;
         // 
         // textBoxName
         // 
         this.textBoxName.Location = new System.Drawing.Point(112, 79);
         this.textBoxName.Margin = new System.Windows.Forms.Padding(4);
         this.textBoxName.Name = "textBoxName";
         this.textBoxName.Size = new System.Drawing.Size(132, 22);
         this.textBoxName.TabIndex = 1;
         // 
         // textBoxNum
         // 
         this.textBoxNum.Location = new System.Drawing.Point(112, 33);
         this.textBoxNum.Margin = new System.Windows.Forms.Padding(4);
         this.textBoxNum.Name = "textBoxNum";
         this.textBoxNum.Size = new System.Drawing.Size(132, 22);
         this.textBoxNum.TabIndex = 0;
         // 
         // buttonFinish
         // 
         this.buttonFinish.BackColor = System.Drawing.SystemColors.ButtonFace;
         this.buttonFinish.Location = new System.Drawing.Point(455, 273);
         this.buttonFinish.Name = "buttonFinish";
         this.buttonFinish.Size = new System.Drawing.Size(120, 23);
         this.buttonFinish.TabIndex = 3;
         this.buttonFinish.Text = "Finish";
         this.buttonFinish.UseVisualStyleBackColor = false;
         this.buttonFinish.Click += new System.EventHandler(this.buttonFinish_Click);
         // 
         // openFileDialog1
         // 
         this.openFileDialog1.Filter = "Cruise template|*.cut;*.crz3t|All files|*.*";
         // 
         // labelWorking
         // 
         this.labelWorking.AutoSize = true;
         this.labelWorking.Location = new System.Drawing.Point(14, 276);
         this.labelWorking.Name = "labelWorking";
         this.labelWorking.Size = new System.Drawing.Size(58, 16);
         this.labelWorking.TabIndex = 4;
         this.labelWorking.Text = "Working";
         this.labelWorking.Visible = false;
         // 
         // backgroundWorker1
         // 
         this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
         this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
         // 
         // pictureBox1
         // 
         this.pictureBox1.Enabled = false;
         this.pictureBox1.Image = global::CruiseDesign.Properties.Resources.bouncingProgressBar;
         this.pictureBox1.Location = new System.Drawing.Point(78, 276);
         this.pictureBox1.Name = "pictureBox1";
         this.pictureBox1.Size = new System.Drawing.Size(168, 20);
         this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
         this.pictureBox1.TabIndex = 5;
         this.pictureBox1.TabStop = false;
         this.pictureBox1.Visible = false;
         // 
         // checkBoxLogData
         // 
         this.checkBoxLogData.AutoSize = true;
         this.checkBoxLogData.Location = new System.Drawing.Point(322, 175);
         this.checkBoxLogData.Name = "checkBoxLogData";
         this.checkBoxLogData.Size = new System.Drawing.Size(136, 20);
         this.checkBoxLogData.TabIndex = 14;
         this.checkBoxLogData.Text = "Log Data Enabled";
         this.checkBoxLogData.UseVisualStyleBackColor = true;
         // 
         // SaleSetupPage
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.BackColor = System.Drawing.Color.Cornsilk;
         this.ClientSize = new System.Drawing.Size(603, 308);
         this.Controls.Add(this.pictureBox1);
         this.Controls.Add(this.labelWorking);
         this.Controls.Add(this.buttonFinish);
         this.Controls.Add(this.groupBox1);
         this.Controls.Add(this.buttonBrowse);
         this.Controls.Add(this.textBoxFile);
         this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Margin = new System.Windows.Forms.Padding(4);
         this.Name = "SaleSetupPage";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "Select Template";
         this.groupBox1.ResumeLayout(false);
         this.groupBox1.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox textBoxFile;
      private System.Windows.Forms.Button buttonBrowse;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.TextBox textBoxDist;
      private System.Windows.Forms.ComboBox comboBoxFor;
      private System.Windows.Forms.ComboBox comboBoxReg;
      private System.Windows.Forms.ComboBox comboBoxUOM;
      private System.Windows.Forms.ComboBox comboBoxPurpose;
      private System.Windows.Forms.TextBox textBoxName;
      private System.Windows.Forms.TextBox textBoxNum;
      private System.Windows.Forms.Label label7;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.Button buttonFinish;
      private System.Windows.Forms.OpenFileDialog openFileDialog1;
      private System.Windows.Forms.Label labelWorking;
      private System.Windows.Forms.PictureBox pictureBox1;
      private System.ComponentModel.BackgroundWorker backgroundWorker1;
      private System.Windows.Forms.CheckBox checkBoxLogData;
   }
}