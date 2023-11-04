using System.Drawing;
using System.Windows.Forms;

namespace LoxStatEdit
{
    partial class MiniserverForm
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
            if(disposing && (components != null))
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
            System.Windows.Forms.Label urlLabel;
            System.Windows.Forms.Button refreshFolderButton;
            System.Windows.Forms.Button refreshMsButton;
            System.Windows.Forms.Label folderLabel;
            System.Windows.Forms.Button downloadButton;
            System.Windows.Forms.Button uploadButton;
            System.Windows.Forms.Label aboutLabel;
            System.Windows.Forms.Button openFolderButton;
            System.Windows.Forms.Button browseFolderButton;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MiniserverForm));
            this._urlTextBox = new System.Windows.Forms.TextBox();
            this._folderTextBox = new System.Windows.Forms.TextBox();
            this._dataGridView = new System.Windows.Forms.DataGridView();
            this._nameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._descriptionCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._yearMonthCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._statusCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._downloadCol = new System.Windows.Forms.DataGridViewButtonColumn();
            this._editCol = new System.Windows.Forms.DataGridViewButtonColumn();
            this._uploadCol = new System.Windows.Forms.DataGridViewButtonColumn();
            this.githubLabel = new System.Windows.Forms.LinkLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.donateLabel = new System.Windows.Forms.LinkLabel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.progressLabel = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            urlLabel = new System.Windows.Forms.Label();
            refreshFolderButton = new System.Windows.Forms.Button();
            refreshMsButton = new System.Windows.Forms.Button();
            folderLabel = new System.Windows.Forms.Label();
            downloadButton = new System.Windows.Forms.Button();
            uploadButton = new System.Windows.Forms.Button();
            aboutLabel = new System.Windows.Forms.Label();
            openFolderButton = new System.Windows.Forms.Button();
            browseFolderButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // urlLabel
            // 
            urlLabel.Location = new System.Drawing.Point(12, 17);
            urlLabel.Name = "urlLabel";
            urlLabel.Size = new System.Drawing.Size(63, 18);
            urlLabel.TabIndex = 0;
            urlLabel.Text = "Miniserver:";
            // 
            // refreshFolderButton
            // 
            refreshFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            refreshFolderButton.Location = new System.Drawing.Point(882, 38);
            refreshFolderButton.Name = "refreshFolderButton";
            refreshFolderButton.Size = new System.Drawing.Size(75, 23);
            refreshFolderButton.TabIndex = 7;
            refreshFolderButton.Text = "Refresh &FS";
            this.toolTip.SetToolTip(refreshFolderButton, "Refresh Computer files (ALT + F)");
            refreshFolderButton.UseVisualStyleBackColor = true;
            refreshFolderButton.Click += new System.EventHandler(this.RefreshFolderButton_Click);
            // 
            // refreshMsButton
            // 
            refreshMsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            refreshMsButton.Location = new System.Drawing.Point(882, 12);
            refreshMsButton.Name = "refreshMsButton";
            refreshMsButton.Size = new System.Drawing.Size(75, 23);
            refreshMsButton.TabIndex = 2;
            refreshMsButton.Text = "Refresh &MS";
            this.toolTip.SetToolTip(refreshMsButton, "Refresh Miniserver files (ALT + M)");
            refreshMsButton.UseVisualStyleBackColor = true;
            refreshMsButton.Click += new System.EventHandler(this.RefreshMsButton_Click);
            // 
            // folderLabel
            // 
            folderLabel.Location = new System.Drawing.Point(12, 43);
            folderLabel.Name = "folderLabel";
            folderLabel.Size = new System.Drawing.Size(63, 18);
            folderLabel.TabIndex = 3;
            folderLabel.Text = "Computer:";
            // 
            // downloadButton
            // 
            downloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            downloadButton.Location = new System.Drawing.Point(729, 527);
            downloadButton.Name = "downloadButton";
            downloadButton.Size = new System.Drawing.Size(111, 23);
            downloadButton.TabIndex = 9;
            downloadButton.Text = "&Download selected";
            this.toolTip.SetToolTip(downloadButton, "Download selected statistic files (ALT + D)");
            downloadButton.UseVisualStyleBackColor = true;
            downloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // uploadButton
            // 
            uploadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            uploadButton.Location = new System.Drawing.Point(846, 527);
            uploadButton.Name = "uploadButton";
            uploadButton.Size = new System.Drawing.Size(111, 23);
            uploadButton.TabIndex = 10;
            uploadButton.Text = "&Upload selected";
            this.toolTip.SetToolTip(uploadButton, "Upload selected statistic files (ALT + U)");
            uploadButton.UseVisualStyleBackColor = true;
            uploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // aboutLabel
            // 
            aboutLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            aboutLabel.Location = new System.Drawing.Point(12, 532);
            aboutLabel.Name = "aboutLabel";
            aboutLabel.Size = new System.Drawing.Size(171, 18);
            aboutLabel.TabIndex = 100;
            aboutLabel.Text = "LoxStatEdit v1.0.4.0 (2023.11.03)";
            // 
            // openFolderButton
            // 
            openFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            openFolderButton.Location = new System.Drawing.Point(801, 38);
            openFolderButton.Name = "openFolderButton";
            openFolderButton.Size = new System.Drawing.Size(75, 23);
            openFolderButton.TabIndex = 6;
            openFolderButton.Text = "&Open";
            this.toolTip.SetToolTip(openFolderButton, "Open folder in Windows Explorer (ALT + O)");
            openFolderButton.UseVisualStyleBackColor = true;
            openFolderButton.Click += new System.EventHandler(this.openFolderButton_Click);
            // 
            // browseFolderButton
            // 
            browseFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            browseFolderButton.Location = new System.Drawing.Point(720, 38);
            browseFolderButton.Name = "browseFolderButton";
            browseFolderButton.Size = new System.Drawing.Size(75, 23);
            browseFolderButton.TabIndex = 5;
            browseFolderButton.Text = "&Browse...";
            this.toolTip.SetToolTip(browseFolderButton, "Browse folders (ALT + B)");
            browseFolderButton.UseVisualStyleBackColor = true;
            browseFolderButton.Click += new System.EventHandler(this.BrowseFolderButton_Click);
            // 
            // _urlTextBox
            // 
            this._urlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._urlTextBox.Location = new System.Drawing.Point(81, 14);
            this._urlTextBox.Name = "_urlTextBox";
            this._urlTextBox.Size = new System.Drawing.Size(795, 20);
            this._urlTextBox.TabIndex = 1;
            this._urlTextBox.Text = "ftp://adminname:adminpassword@miniserver-ip-or-hostname:21";
            this.toolTip.SetToolTip(this._urlTextBox, "Miniserver connection details");
            this._urlTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this._urlTextBox_KeyDown);
            // 
            // _folderTextBox
            // 
            this._folderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._folderTextBox.Location = new System.Drawing.Point(81, 40);
            this._folderTextBox.Name = "_folderTextBox";
            this._folderTextBox.Size = new System.Drawing.Size(633, 20);
            this._folderTextBox.TabIndex = 4;
            this.toolTip.SetToolTip(this._folderTextBox, "Path to folder on this computer where statistic files are saved");
            this._folderTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this._folderTextBox_KeyDown);
            // 
            // _dataGridView
            // 
            this._dataGridView.AllowUserToAddRows = false;
            this._dataGridView.AllowUserToDeleteRows = false;
            this._dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._nameCol,
            this._descriptionCol,
            this._yearMonthCol,
            this._statusCol,
            this._downloadCol,
            this._editCol,
            this._uploadCol});
            this._dataGridView.Location = new System.Drawing.Point(12, 67);
            this._dataGridView.Name = "_dataGridView";
            this._dataGridView.RowHeadersWidth = 30;
            this._dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dataGridView.Size = new System.Drawing.Size(945, 453);
            this._dataGridView.TabIndex = 8;
            this._dataGridView.VirtualMode = true;
            this._dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellContentClick);
            this._dataGridView.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.DataGridView_CellValueNeeded);
            this._dataGridView.CellFormatting += DataGridView_CellFormatting;
            // 
            // _nameCol
            // 
            this._nameCol.DataPropertyName = "Name";
            this._nameCol.HeaderText = "Name";
            this._nameCol.MinimumWidth = 8;
            this._nameCol.Name = "_nameCol";
            this._nameCol.ReadOnly = true;
            this._nameCol.Width = 250;
            // 
            // _descriptionCol
            // 
            this._descriptionCol.DataPropertyName = "Description";
            this._descriptionCol.HeaderText = "Description";
            this._descriptionCol.MinimumWidth = 8;
            this._descriptionCol.Name = "_descriptionCol";
            this._descriptionCol.ReadOnly = true;
            this._descriptionCol.Width = 250;
            // 
            // _yearMonthCol
            // 
            this._yearMonthCol.DataPropertyName = "YearMonth";
            dataGridViewCellStyle1.Format = "yyyy-MM";
            this._yearMonthCol.DefaultCellStyle = dataGridViewCellStyle1;
            this._yearMonthCol.HeaderText = "Year/Month";
            this._yearMonthCol.MinimumWidth = 8;
            this._yearMonthCol.Name = "_yearMonthCol";
            this._yearMonthCol.ReadOnly = true;
            this._yearMonthCol.Width = 90;
            // 
            // _statusCol
            // 
            this._statusCol.DataPropertyName = "Status";
            this._statusCol.HeaderText = "Status";
            this._statusCol.MinimumWidth = 8;
            this._statusCol.Name = "_statusCol";
            this._statusCol.ReadOnly = true;
            this._statusCol.Width = 100;
            // 
            // _downloadCol
            // 
            this._downloadCol.HeaderText = "Download";
            this._downloadCol.MinimumWidth = 8;
            this._downloadCol.Name = "_downloadCol";
            this._downloadCol.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._downloadCol.Text = "Download";
            this._downloadCol.Width = 60;
            // 
            // _editCol
            // 
            this._editCol.HeaderText = "Edit";
            this._editCol.MinimumWidth = 8;
            this._editCol.Name = "_editCol";
            this._editCol.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._editCol.Text = "Edit";
            this._editCol.Width = 50;
            // 
            // _uploadCol
            // 
            this._uploadCol.HeaderText = "Upload";
            this._uploadCol.MinimumWidth = 8;
            this._uploadCol.Name = "_uploadCol";
            this._uploadCol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this._uploadCol.Text = "Upload";
            this._uploadCol.Width = 60;
            // 
            // githubLabel
            // 
            this.githubLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.githubLabel.LinkArea = new System.Windows.Forms.LinkArea(0, 10);
            this.githubLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.githubLabel.LinkColor = System.Drawing.Color.Blue;
            this.githubLabel.Location = new System.Drawing.Point(184, 533);
            this.githubLabel.Name = "githubLabel";
            this.githubLabel.Size = new System.Drawing.Size(42, 18);
            this.githubLabel.TabIndex = 101;
            this.githubLabel.TabStop = true;
            this.githubLabel.Text = "GitHub";
            this.githubLabel.UseCompatibleTextRendering = true;
            this.githubLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.githubLinkLabel_LinkClicked);
            // 
            // donateLabel
            // 
            this.donateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.donateLabel.LinkArea = new System.Windows.Forms.LinkArea(0, 20);
            this.donateLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.donateLabel.LinkColor = System.Drawing.Color.Blue;
            this.donateLabel.Location = new System.Drawing.Point(232, 533);
            this.donateLabel.Name = "donateLabel";
            this.donateLabel.Size = new System.Drawing.Size(96, 18);
            this.donateLabel.TabIndex = 102;
            this.donateLabel.TabStop = true;
            this.donateLabel.Text = "Donate && Support";
            this.donateLabel.UseCompatibleTextRendering = true;
            this.donateLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.donateLabel_LinkClicked);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(563, 527);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(160, 23);
            this.progressBar.TabIndex = 103;
            // 
            // progressLabel
            // 
            this.progressLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressLabel.AutoSize = true;
            this.progressLabel.BackColor = System.Drawing.Color.Transparent;
            this.progressLabel.Location = new System.Drawing.Point(568, 532);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(24, 13);
            this.progressLabel.TabIndex = 104;
            this.progressLabel.Text = "Idle";
            this.progressLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MiniserverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 561);
            this.Controls.Add(refreshMsButton);
            this.Controls.Add(browseFolderButton);
            this.Controls.Add(openFolderButton);
            this.Controls.Add(refreshFolderButton);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(downloadButton);
            this.Controls.Add(uploadButton);
            this.Controls.Add(aboutLabel);
            this.Controls.Add(this.githubLabel);
            this.Controls.Add(this.donateLabel);
            this.Controls.Add(this._dataGridView);
            this.Controls.Add(urlLabel);
            this.Controls.Add(this._urlTextBox);
            this.Controls.Add(folderLabel);
            this.Controls.Add(this._folderTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(750, 270);
            this.Name = "MiniserverForm";
            this.Text = "Loxone Stats Editor - Miniserver Browser";
            this.Load += new System.EventHandler(this.MiniserverForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _urlTextBox;
        private System.Windows.Forms.TextBox _folderTextBox;
        private System.Windows.Forms.DataGridView _dataGridView;
        private LinkLabel githubLabel;
        private ToolTip toolTip;
        private LinkLabel donateLabel;
        private DataGridViewTextBoxColumn _nameCol;
        private DataGridViewTextBoxColumn _descriptionCol;
        private DataGridViewTextBoxColumn _yearMonthCol;
        private DataGridViewTextBoxColumn _statusCol;
        private DataGridViewButtonColumn _downloadCol;
        private DataGridViewButtonColumn _editCol;
        private DataGridViewButtonColumn _uploadCol;
        private ProgressBar progressBar;
        private Label progressLabel;
        private FolderBrowserDialog folderBrowserDialog;
    }
}