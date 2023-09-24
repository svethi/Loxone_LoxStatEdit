namespace LoxStatEdit
{
    partial class LoxStatFileForm
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
            System.Windows.Forms.Label fileInfoLabel;
            System.Windows.Forms.Button saveButton;
            System.Windows.Forms.Button loadButton;
            System.Windows.Forms.Button browseButton;
            System.Windows.Forms.Label fileNameLabel;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoxStatFileForm));
            this._problemButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this._fileInfoTextBox = new System.Windows.Forms.TextBox();
            this._dataGridView = new System.Windows.Forms.DataGridView();
            this.indexColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._timestampColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._valueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._fileNameTextBox = new System.Windows.Forms.TextBox();
            this._chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            fileInfoLabel = new System.Windows.Forms.Label();
            saveButton = new System.Windows.Forms.Button();
            loadButton = new System.Windows.Forms.Button();
            browseButton = new System.Windows.Forms.Button();
            fileNameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._chart)).BeginInit();
            this.SuspendLayout();
            // 
            // fileInfoLabel
            // 
            fileInfoLabel.Location = new System.Drawing.Point(12, 43);
            fileInfoLabel.Name = "fileInfoLabel";
            fileInfoLabel.Size = new System.Drawing.Size(60, 13);
            fileInfoLabel.TabIndex = 4;
            fileInfoLabel.Text = "File Info:";
            // 
            // saveButton
            // 
            saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            saveButton.Location = new System.Drawing.Point(800, 38);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(75, 23);
            saveButton.TabIndex = 7;
            saveButton.Text = "&Save";
            this.toolTip.SetToolTip(saveButton, "ALT + S");
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // loadButton
            // 
            loadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            loadButton.Location = new System.Drawing.Point(800, 12);
            loadButton.Name = "loadButton";
            loadButton.Size = new System.Drawing.Size(75, 23);
            loadButton.TabIndex = 3;
            loadButton.Text = "&Load";
            this.toolTip.SetToolTip(loadButton, "ALT + L");
            loadButton.UseVisualStyleBackColor = true;
            loadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // browseButton
            // 
            browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            browseButton.Location = new System.Drawing.Point(719, 12);
            browseButton.Name = "browseButton";
            browseButton.Size = new System.Drawing.Size(75, 23);
            browseButton.TabIndex = 2;
            browseButton.Text = "&Browse...";
            this.toolTip.SetToolTip(browseButton, "ALT + B");
            browseButton.UseVisualStyleBackColor = true;
            browseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // fileNameLabel
            // 
            fileNameLabel.Location = new System.Drawing.Point(12, 17);
            fileNameLabel.Name = "fileNameLabel";
            fileNameLabel.Size = new System.Drawing.Size(60, 13);
            fileNameLabel.TabIndex = 0;
            fileNameLabel.Text = "File Name:";
            // 
            // _problemButton
            // 
            this._problemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._problemButton.Enabled = false;
            this._problemButton.ForeColor = System.Drawing.Color.Red;
            this._problemButton.Location = new System.Drawing.Point(719, 38);
            this._problemButton.Name = "_problemButton";
            this._problemButton.Size = new System.Drawing.Size(75, 23);
            this._problemButton.TabIndex = 6;
            this._problemButton.Text = "&Problems";
            this.toolTip.SetToolTip(this._problemButton, "ALT + P");
            this._problemButton.UseVisualStyleBackColor = true;
            this._problemButton.Click += new System.EventHandler(this.ProblemButton_Click);
            // 
            // _fileInfoTextBox
            // 
            this._fileInfoTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._fileInfoTextBox.Location = new System.Drawing.Point(78, 40);
            this._fileInfoTextBox.Name = "_fileInfoTextBox";
            this._fileInfoTextBox.ReadOnly = true;
            this._fileInfoTextBox.Size = new System.Drawing.Size(635, 20);
            this._fileInfoTextBox.TabIndex = 5;
            this._fileInfoTextBox.Text = "(Enter or browse a file name end press the Load button)";
            // 
            // _dataGridView
            // 
            this._dataGridView.AllowUserToAddRows = false;
            this._dataGridView.AllowUserToDeleteRows = false;
            this._dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.indexColumn,
            this._timestampColumn,
            this._valueColumn});
            this._dataGridView.Location = new System.Drawing.Point(15, 66);
            this._dataGridView.Name = "_dataGridView";
            this._dataGridView.RowHeadersWidth = 30;
            this._dataGridView.Size = new System.Drawing.Size(376, 483);
            this._dataGridView.TabIndex = 8;
            this._dataGridView.VirtualMode = true;
            this._dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._dataGridView_CellContentClick);
            this._dataGridView.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.DataGridView_CellValueNeeded);
            this._dataGridView.CellValuePushed += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.DataGridView_CellValuePushed);
            this._dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this._dataGridView_KeyDown);
            // 
            // indexColumn
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.indexColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.indexColumn.HeaderText = "Index";
            this.indexColumn.MinimumWidth = 8;
            this.indexColumn.Name = "indexColumn";
            this.indexColumn.ReadOnly = true;
            this.indexColumn.Width = 60;
            // 
            // _timestampColumn
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Format = "G";
            dataGridViewCellStyle2.NullValue = null;
            this._timestampColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this._timestampColumn.HeaderText = "Timestamp";
            this._timestampColumn.MinimumWidth = 8;
            this._timestampColumn.Name = "_timestampColumn";
            this._timestampColumn.Width = 130;
            // 
            // _valueColumn
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N3";
            dataGridViewCellStyle3.NullValue = null;
            this._valueColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this._valueColumn.HeaderText = "Value";
            this._valueColumn.MinimumWidth = 8;
            this._valueColumn.Name = "_valueColumn";
            this._valueColumn.Width = 130;
            // 
            // _fileNameTextBox
            // 
            this._fileNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._fileNameTextBox.Location = new System.Drawing.Point(78, 14);
            this._fileNameTextBox.Name = "_fileNameTextBox";
            this._fileNameTextBox.Size = new System.Drawing.Size(635, 20);
            this._fileNameTextBox.TabIndex = 1;
            // 
            // _chart
            // 
            this._chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._chart.BorderSkin.BorderColor = System.Drawing.Color.IndianRed;
            chartArea1.AxisX.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.MinorTickMark.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.MinorTickMark.LineColor = System.Drawing.Color.LightGray;
            chartArea1.Name = "ChartArea1";
            this._chart.ChartAreas.Add(chartArea1);
            this._chart.Cursor = System.Windows.Forms.Cursors.Cross;
            this._chart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Name = "Legend1";
            this._chart.Legends.Add(legend1);
            this._chart.Location = new System.Drawing.Point(396, 66);
            this._chart.Margin = new System.Windows.Forms.Padding(2);
            this._chart.Name = "_chart";
            this._chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this._chart.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.Lime,
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))))};
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.EmptyPointStyle.LabelForeColor = System.Drawing.Color.Bisque;
            series1.IsValueShownAsLabel = true;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this._chart.Series.Add(series1);
            this._chart.Size = new System.Drawing.Size(477, 483);
            this._chart.TabIndex = 9;
            this._chart.Text = "chart1";
            // 
            // LoxStatFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this._chart);
            this.Controls.Add(this._problemButton);
            this.Controls.Add(saveButton);
            this.Controls.Add(this._dataGridView);
            this.Controls.Add(this._fileInfoTextBox);
            this.Controls.Add(fileInfoLabel);
            this.Controls.Add(fileNameLabel);
            this.Controls.Add(loadButton);
            this.Controls.Add(this._fileNameTextBox);
            this.Controls.Add(browseButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoxStatFileForm";
            this.Text = "Loxone Stats Editor - File Editor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox _fileInfoTextBox;
        private System.Windows.Forms.DataGridView _dataGridView;
        private System.Windows.Forms.Button _problemButton;
        private System.Windows.Forms.TextBox _fileNameTextBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart _chart;
        private System.Windows.Forms.DataGridViewTextBoxColumn indexColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn _timestampColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn _valueColumn;
        private System.Windows.Forms.ToolTip toolTip;
    }
}

