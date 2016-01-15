namespace CruiseDesign
{
    partial class CruiseDesignMain
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
           System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CruiseDesignMain));
           this.RootLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
           this.sideBarPanel = new System.Windows.Forms.FlowLayoutPanel();
           this.buttonIcon = new System.Windows.Forms.Button();
           this.buttonFile = new System.Windows.Forms.Button();
           this.buttonSetup = new System.Windows.Forms.Button();
           this.buttonDesign = new System.Windows.Forms.Button();
           this.buttonTools = new System.Windows.Forms.Button();
           this.ExitButton = new System.Windows.Forms.Button();
           this.tableLayoutPanelFile = new System.Windows.Forms.TableLayoutPanel();
           this.labelRowOne = new System.Windows.Forms.Label();
           this.labelRowTwo = new System.Windows.Forms.Label();
           this.labelRowThree = new System.Windows.Forms.Label();
           this.buttonRowTwo = new System.Windows.Forms.Button();
           this.buttonRowThree = new System.Windows.Forms.Button();
           this.buttonRowOne = new System.Windows.Forms.Button();
           this.labelRowFour = new System.Windows.Forms.Label();
           this.buttonRowFour = new System.Windows.Forms.Button();
           this.TitleLabel = new System.Windows.Forms.Label();
           this.imageList1 = new System.Windows.Forms.ImageList(this.components);
           this.openFileDialogCruise = new System.Windows.Forms.OpenFileDialog();
           this.openFileDialogDesign = new System.Windows.Forms.OpenFileDialog();
           this.saveFileDialogDesign = new System.Windows.Forms.SaveFileDialog();
           this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
           this.RootLayoutPanel.SuspendLayout();
           this.sideBarPanel.SuspendLayout();
           this.tableLayoutPanelFile.SuspendLayout();
           this.SuspendLayout();
           // 
           // RootLayoutPanel
           // 
           this.RootLayoutPanel.BackColor = System.Drawing.Color.LemonChiffon;
           this.RootLayoutPanel.ColumnCount = 2;
           this.RootLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
           this.RootLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
           this.RootLayoutPanel.Controls.Add(this.sideBarPanel, 0, 0);
           this.RootLayoutPanel.Controls.Add(this.tableLayoutPanelFile, 1, 1);
           this.RootLayoutPanel.Controls.Add(this.TitleLabel, 1, 0);
           this.RootLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
           this.RootLayoutPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.RootLayoutPanel.Location = new System.Drawing.Point(0, 0);
           this.RootLayoutPanel.Margin = new System.Windows.Forms.Padding(2);
           this.RootLayoutPanel.Name = "RootLayoutPanel";
           this.RootLayoutPanel.RowCount = 1;
           this.RootLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
           this.RootLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
           this.RootLayoutPanel.Size = new System.Drawing.Size(548, 391);
           this.RootLayoutPanel.TabIndex = 3;
           // 
           // sideBarPanel
           // 
           this.sideBarPanel.AutoSize = true;
           this.sideBarPanel.BackColor = System.Drawing.Color.Transparent;
           this.sideBarPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("sideBarPanel.BackgroundImage")));
           this.sideBarPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
           this.sideBarPanel.Controls.Add(this.buttonIcon);
           this.sideBarPanel.Controls.Add(this.buttonFile);
           this.sideBarPanel.Controls.Add(this.buttonSetup);
           this.sideBarPanel.Controls.Add(this.buttonDesign);
           this.sideBarPanel.Controls.Add(this.buttonTools);
           this.sideBarPanel.Controls.Add(this.ExitButton);
           this.sideBarPanel.Dock = System.Windows.Forms.DockStyle.Fill;
           this.sideBarPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
           this.sideBarPanel.Location = new System.Drawing.Point(0, 0);
           this.sideBarPanel.Margin = new System.Windows.Forms.Padding(0);
           this.sideBarPanel.Name = "sideBarPanel";
           this.RootLayoutPanel.SetRowSpan(this.sideBarPanel, 2);
           this.sideBarPanel.Size = new System.Drawing.Size(130, 391);
           this.sideBarPanel.TabIndex = 1;
           this.sideBarPanel.TabStop = true;
           // 
           // buttonIcon
           // 
           this.buttonIcon.Anchor = System.Windows.Forms.AnchorStyles.Top;
           this.buttonIcon.Cursor = System.Windows.Forms.Cursors.Arrow;
           this.buttonIcon.FlatAppearance.BorderSize = 0;
           this.buttonIcon.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
           this.buttonIcon.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
           this.buttonIcon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
           this.buttonIcon.Image = ((System.Drawing.Image)(resources.GetObject("buttonIcon.Image")));
           this.buttonIcon.Location = new System.Drawing.Point(15, 7);
           this.buttonIcon.Margin = new System.Windows.Forms.Padding(15, 7, 15, 7);
           this.buttonIcon.Name = "buttonIcon";
           this.buttonIcon.Size = new System.Drawing.Size(105, 51);
           this.buttonIcon.TabIndex = 8;
           this.buttonIcon.UseVisualStyleBackColor = true;
           // 
           // buttonFile
           // 
           this.buttonFile.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonFile.BackgroundImage")));
           this.buttonFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
           this.buttonFile.Cursor = System.Windows.Forms.Cursors.Hand;
           this.buttonFile.FlatAppearance.BorderSize = 0;
           this.buttonFile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
           this.buttonFile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
           this.buttonFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
           this.buttonFile.Location = new System.Drawing.Point(15, 72);
           this.buttonFile.Margin = new System.Windows.Forms.Padding(15, 7, 12, 7);
           this.buttonFile.Name = "buttonFile";
           this.buttonFile.Size = new System.Drawing.Size(100, 40);
           this.buttonFile.TabIndex = 9;
           this.buttonFile.Text = "File";
           this.buttonFile.UseVisualStyleBackColor = true;
           this.buttonFile.Click += new System.EventHandler(this.buttonFile_Click);
           // 
           // buttonSetup
           // 
           this.buttonSetup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonSetup.BackgroundImage")));
           this.buttonSetup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
           this.buttonSetup.Cursor = System.Windows.Forms.Cursors.Hand;
           this.buttonSetup.Enabled = false;
           this.buttonSetup.FlatAppearance.BorderSize = 0;
           this.buttonSetup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
           this.buttonSetup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
           this.buttonSetup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
           this.buttonSetup.Location = new System.Drawing.Point(15, 126);
           this.buttonSetup.Margin = new System.Windows.Forms.Padding(15, 7, 12, 7);
           this.buttonSetup.Name = "buttonSetup";
           this.buttonSetup.Size = new System.Drawing.Size(100, 40);
           this.buttonSetup.TabIndex = 10;
           this.buttonSetup.Text = "Establish";
           this.buttonSetup.UseVisualStyleBackColor = true;
           this.buttonSetup.Click += new System.EventHandler(this.buttonSetup_Click);
           // 
           // buttonDesign
           // 
           this.buttonDesign.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonDesign.BackgroundImage")));
           this.buttonDesign.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
           this.buttonDesign.Cursor = System.Windows.Forms.Cursors.Hand;
           this.buttonDesign.Enabled = false;
           this.buttonDesign.FlatAppearance.BorderSize = 0;
           this.buttonDesign.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
           this.buttonDesign.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
           this.buttonDesign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
           this.buttonDesign.Location = new System.Drawing.Point(15, 180);
           this.buttonDesign.Margin = new System.Windows.Forms.Padding(15, 7, 12, 7);
           this.buttonDesign.Name = "buttonDesign";
           this.buttonDesign.Size = new System.Drawing.Size(100, 40);
           this.buttonDesign.TabIndex = 11;
           this.buttonDesign.Text = "Design";
           this.buttonDesign.UseVisualStyleBackColor = true;
           this.buttonDesign.Click += new System.EventHandler(this.buttonDesign_Click);
           // 
           // buttonTools
           // 
           this.buttonTools.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonTools.BackgroundImage")));
           this.buttonTools.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
           this.buttonTools.Cursor = System.Windows.Forms.Cursors.Hand;
           this.buttonTools.Enabled = false;
           this.buttonTools.FlatAppearance.BorderSize = 0;
           this.buttonTools.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
           this.buttonTools.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
           this.buttonTools.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
           this.buttonTools.Location = new System.Drawing.Point(15, 234);
           this.buttonTools.Margin = new System.Windows.Forms.Padding(15, 7, 12, 7);
           this.buttonTools.Name = "buttonTools";
           this.buttonTools.Size = new System.Drawing.Size(100, 40);
           this.buttonTools.TabIndex = 12;
           this.buttonTools.Text = "Tools";
           this.buttonTools.UseVisualStyleBackColor = true;
           this.buttonTools.Click += new System.EventHandler(this.buttonTools_Click);
           // 
           // ExitButton
           // 
           this.ExitButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ExitButton.BackgroundImage")));
           this.ExitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
           this.ExitButton.Cursor = System.Windows.Forms.Cursors.Hand;
           this.ExitButton.FlatAppearance.BorderSize = 0;
           this.ExitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
           this.ExitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
           this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
           this.ExitButton.Location = new System.Drawing.Point(15, 288);
           this.ExitButton.Margin = new System.Windows.Forms.Padding(15, 7, 12, 7);
           this.ExitButton.Name = "ExitButton";
           this.ExitButton.Size = new System.Drawing.Size(100, 40);
           this.ExitButton.TabIndex = 4;
           this.ExitButton.Text = "Exit";
           this.ExitButton.UseVisualStyleBackColor = true;
           this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
           // 
           // tableLayoutPanelFile
           // 
           this.tableLayoutPanelFile.AutoSize = true;
           this.tableLayoutPanelFile.BackColor = System.Drawing.Color.Cornsilk;
           this.tableLayoutPanelFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
           this.tableLayoutPanelFile.ColumnCount = 2;
           this.tableLayoutPanelFile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
           this.tableLayoutPanelFile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56F));
           this.tableLayoutPanelFile.Controls.Add(this.labelRowOne, 0, 0);
           this.tableLayoutPanelFile.Controls.Add(this.labelRowTwo, 0, 1);
           this.tableLayoutPanelFile.Controls.Add(this.labelRowThree, 0, 2);
           this.tableLayoutPanelFile.Controls.Add(this.buttonRowTwo, 1, 1);
           this.tableLayoutPanelFile.Controls.Add(this.buttonRowThree, 1, 2);
           this.tableLayoutPanelFile.Controls.Add(this.buttonRowOne, 1, 0);
           this.tableLayoutPanelFile.Controls.Add(this.labelRowFour, 0, 3);
           this.tableLayoutPanelFile.Controls.Add(this.buttonRowFour, 1, 3);
           this.tableLayoutPanelFile.Dock = System.Windows.Forms.DockStyle.Fill;
           this.tableLayoutPanelFile.Location = new System.Drawing.Point(130, 37);
           this.tableLayoutPanelFile.Margin = new System.Windows.Forms.Padding(0);
           this.tableLayoutPanelFile.Name = "tableLayoutPanelFile";
           this.tableLayoutPanelFile.RowCount = 5;
           this.tableLayoutPanelFile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
           this.tableLayoutPanelFile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
           this.tableLayoutPanelFile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
           this.tableLayoutPanelFile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
           this.tableLayoutPanelFile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
           this.tableLayoutPanelFile.Size = new System.Drawing.Size(418, 354);
           this.tableLayoutPanelFile.TabIndex = 2;
           // 
           // labelRowOne
           // 
           this.labelRowOne.AutoSize = true;
           this.labelRowOne.Dock = System.Windows.Forms.DockStyle.Fill;
           this.labelRowOne.Image = ((System.Drawing.Image)(resources.GetObject("labelRowOne.Image")));
           this.labelRowOne.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
           this.labelRowOne.Location = new System.Drawing.Point(2, 0);
           this.labelRowOne.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
           this.labelRowOne.Name = "labelRowOne";
           this.labelRowOne.Size = new System.Drawing.Size(358, 59);
           this.labelRowOne.TabIndex = 2;
           this.labelRowOne.Text = "Open Existing File";
           this.labelRowOne.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
           // 
           // labelRowTwo
           // 
           this.labelRowTwo.AutoSize = true;
           this.labelRowTwo.Dock = System.Windows.Forms.DockStyle.Fill;
           this.labelRowTwo.Image = ((System.Drawing.Image)(resources.GetObject("labelRowTwo.Image")));
           this.labelRowTwo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
           this.labelRowTwo.Location = new System.Drawing.Point(2, 59);
           this.labelRowTwo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
           this.labelRowTwo.Name = "labelRowTwo";
           this.labelRowTwo.Size = new System.Drawing.Size(358, 59);
           this.labelRowTwo.TabIndex = 3;
           this.labelRowTwo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
           this.labelRowTwo.Visible = false;
           // 
           // labelRowThree
           // 
           this.labelRowThree.AutoSize = true;
           this.labelRowThree.Dock = System.Windows.Forms.DockStyle.Fill;
           this.labelRowThree.Image = ((System.Drawing.Image)(resources.GetObject("labelRowThree.Image")));
           this.labelRowThree.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
           this.labelRowThree.Location = new System.Drawing.Point(4, 118);
           this.labelRowThree.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
           this.labelRowThree.Name = "labelRowThree";
           this.labelRowThree.Size = new System.Drawing.Size(354, 59);
           this.labelRowThree.TabIndex = 7;
           this.labelRowThree.Text = "Create New From Recon File";
           this.labelRowThree.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
           // 
           // buttonRowTwo
           // 
           this.buttonRowTwo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
           this.buttonRowTwo.BackColor = System.Drawing.SystemColors.ButtonFace;
           this.buttonRowTwo.Cursor = System.Windows.Forms.Cursors.Hand;
           this.buttonRowTwo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
           this.buttonRowTwo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
           this.buttonRowTwo.Location = new System.Drawing.Point(366, 63);
           this.buttonRowTwo.Margin = new System.Windows.Forms.Padding(4);
           this.buttonRowTwo.MaximumSize = new System.Drawing.Size(60, 54);
           this.buttonRowTwo.Name = "buttonRowTwo";
           this.buttonRowTwo.Size = new System.Drawing.Size(48, 45);
           this.buttonRowTwo.TabIndex = 5;
           this.buttonRowTwo.UseVisualStyleBackColor = false;
           this.buttonRowTwo.Visible = false;
           this.buttonRowTwo.Click += new System.EventHandler(this.buttonRowTwo_Click);
           // 
           // buttonRowThree
           // 
           this.buttonRowThree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
           this.buttonRowThree.BackColor = System.Drawing.SystemColors.ButtonFace;
           this.buttonRowThree.Cursor = System.Windows.Forms.Cursors.Hand;
           this.buttonRowThree.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
           this.buttonRowThree.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
           this.buttonRowThree.Image = global::CruiseDesign.Properties.Resources.openrecon48;
           this.buttonRowThree.Location = new System.Drawing.Point(366, 122);
           this.buttonRowThree.Margin = new System.Windows.Forms.Padding(4);
           this.buttonRowThree.MaximumSize = new System.Drawing.Size(60, 54);
           this.buttonRowThree.Name = "buttonRowThree";
           this.buttonRowThree.Size = new System.Drawing.Size(48, 45);
           this.buttonRowThree.TabIndex = 8;
           this.buttonRowThree.UseVisualStyleBackColor = false;
           this.buttonRowThree.Click += new System.EventHandler(this.buttonRowThree_Click);
           // 
           // buttonRowOne
           // 
           this.buttonRowOne.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
           this.buttonRowOne.BackColor = System.Drawing.SystemColors.ButtonFace;
           this.buttonRowOne.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
           this.buttonRowOne.Cursor = System.Windows.Forms.Cursors.Hand;
           this.buttonRowOne.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
           this.buttonRowOne.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
           this.buttonRowOne.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
           this.buttonRowOne.Image = ((System.Drawing.Image)(resources.GetObject("buttonRowOne.Image")));
           this.buttonRowOne.Location = new System.Drawing.Point(366, 4);
           this.buttonRowOne.Margin = new System.Windows.Forms.Padding(4);
           this.buttonRowOne.MaximumSize = new System.Drawing.Size(64, 54);
           this.buttonRowOne.Name = "buttonRowOne";
           this.buttonRowOne.Size = new System.Drawing.Size(48, 45);
           this.buttonRowOne.TabIndex = 4;
           this.buttonRowOne.UseVisualStyleBackColor = false;
           this.buttonRowOne.Click += new System.EventHandler(this.buttonRowOne_Click);
           // 
           // labelRowFour
           // 
           this.labelRowFour.AutoSize = true;
           this.labelRowFour.Dock = System.Windows.Forms.DockStyle.Fill;
           this.labelRowFour.Image = ((System.Drawing.Image)(resources.GetObject("labelRowFour.Image")));
           this.labelRowFour.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
           this.labelRowFour.Location = new System.Drawing.Point(2, 177);
           this.labelRowFour.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
           this.labelRowFour.Name = "labelRowFour";
           this.labelRowFour.Size = new System.Drawing.Size(358, 59);
           this.labelRowFour.TabIndex = 9;
           this.labelRowFour.Text = "Create New From Historical Data";
           this.labelRowFour.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
           // 
           // buttonRowFour
           // 
           this.buttonRowFour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
           this.buttonRowFour.BackColor = System.Drawing.SystemColors.ButtonFace;
           this.buttonRowFour.Cursor = System.Windows.Forms.Cursors.Hand;
           this.buttonRowFour.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
           this.buttonRowFour.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
           this.buttonRowFour.Image = global::CruiseDesign.Properties.Resources.opencruise48a;
           this.buttonRowFour.Location = new System.Drawing.Point(366, 181);
           this.buttonRowFour.Margin = new System.Windows.Forms.Padding(4);
           this.buttonRowFour.MaximumSize = new System.Drawing.Size(60, 54);
           this.buttonRowFour.Name = "buttonRowFour";
           this.buttonRowFour.Size = new System.Drawing.Size(48, 45);
           this.buttonRowFour.TabIndex = 10;
           this.buttonRowFour.UseVisualStyleBackColor = false;
           this.buttonRowFour.Click += new System.EventHandler(this.buttonRowFour_Click);
           // 
           // TitleLabel
           // 
           this.TitleLabel.AutoSize = true;
           this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
           this.TitleLabel.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.TitleLabel.Location = new System.Drawing.Point(132, 0);
           this.TitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
           this.TitleLabel.Name = "TitleLabel";
           this.TitleLabel.Size = new System.Drawing.Size(414, 37);
           this.TitleLabel.TabIndex = 3;
           this.TitleLabel.Text = "Cruise Design Program";
           this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
           // 
           // imageList1
           // 
           this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
           this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
           this.imageList1.Images.SetKeyName(0, "Cruisedesign48.gif");
           this.imageList1.Images.SetKeyName(1, "folder-cruise48.gif");
           this.imageList1.Images.SetKeyName(2, "folder-trees-recon48.gif");
           this.imageList1.Images.SetKeyName(3, "dollar48.png");
           this.imageList1.Images.SetKeyName(4, "content-reorder48.png");
           this.imageList1.Images.SetKeyName(5, "order48.png");
           this.imageList1.Images.SetKeyName(6, "addons48.png");
           // 
           // openFileDialogCruise
           // 
           this.openFileDialogCruise.DefaultExt = "cruise";
           this.openFileDialogCruise.DereferenceLinks = false;
           this.openFileDialogCruise.FileName = "*.cruise";
           this.openFileDialogCruise.Filter = "Cruise files|*.cruise|Cruise Design files|*.design|All files|*.*";
           this.openFileDialogCruise.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogCruise_FileOk);
           // 
           // openFileDialogDesign
           // 
           this.openFileDialogDesign.FileName = "*.design";
           this.openFileDialogDesign.Filter = "Cruise Design files|*.design|Cruise files|*.cruise|All files|*.*";
           // 
           // saveFileDialogDesign
           // 
           this.saveFileDialogDesign.DefaultExt = "*.design";
           this.saveFileDialogDesign.FileName = "*.design";
           this.saveFileDialogDesign.Filter = "Cruise Design files|*.design|All files|*.*";
           // 
           // CruiseDesignMain
           // 
           this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
           this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           this.AutoSize = true;
           this.ClientSize = new System.Drawing.Size(548, 391);
           this.Controls.Add(this.RootLayoutPanel);
           this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
           this.Margin = new System.Windows.Forms.Padding(2);
           this.MinimumSize = new System.Drawing.Size(473, 389);
           this.Name = "CruiseDesignMain";
           this.RightToLeft = System.Windows.Forms.RightToLeft.No;
           this.Text = "Cruise Design  2016.01.08 Beta";
           this.Load += new System.EventHandler(this.CruiseDesignMain_Load);
           this.RootLayoutPanel.ResumeLayout(false);
           this.RootLayoutPanel.PerformLayout();
           this.sideBarPanel.ResumeLayout(false);
           this.tableLayoutPanelFile.ResumeLayout(false);
           this.tableLayoutPanelFile.PerformLayout();
           this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel RootLayoutPanel;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.FlowLayoutPanel sideBarPanel;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.OpenFileDialog openFileDialogCruise;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Button buttonIcon;
        private System.Windows.Forms.Button buttonFile;
        private System.Windows.Forms.Button buttonDesign;
        private System.Windows.Forms.Button buttonTools;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFile;
        private System.Windows.Forms.Label labelRowOne;
        private System.Windows.Forms.Label labelRowTwo;
        private System.Windows.Forms.Label labelRowThree;
        private System.Windows.Forms.Button buttonRowTwo;
        private System.Windows.Forms.Button buttonRowThree;
        private System.Windows.Forms.Button buttonRowOne;
        private System.Windows.Forms.Label labelRowFour;
        private System.Windows.Forms.Button buttonRowFour;
        private System.Windows.Forms.OpenFileDialog openFileDialogDesign;
        private System.Windows.Forms.SaveFileDialog saveFileDialogDesign;
        private System.Windows.Forms.Button buttonSetup;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }}

