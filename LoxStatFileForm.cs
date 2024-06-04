using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Text.RegularExpressions;

namespace LoxStatEdit
{
    public partial class LoxStatFileForm: Form
    {
        const int _valueColumnOffset = 2;
        readonly string[] _args;
        LoxStatFile _loxStatFile;
        IList<LoxStatProblem> _problems;

        public LoxStatFileForm(params string[] args)
        {
            _args = args;
            InitializeComponent();
        }

        private void LoadFile()
        {
            _fileNameTextBox.Text = Path.GetFullPath(_fileNameTextBox.Text);
            _loxStatFile = LoxStatFile.Load(_fileNameTextBox.Text);
            _dataGridView.RowCount = 0;
            var columns = _dataGridView.Columns;
            while(columns.Count > (_valueColumnOffset + 1))
                columns.RemoveAt(columns.Count - 1);
            _fileInfoTextBox.Text = string.Format("{0}: {1} data point{2} with {3} value{4}",
                            _loxStatFile.Text, 
                            _loxStatFile.DataPoints.Count, 
                            _loxStatFile.DataPoints.Count == 1 ? "" : "s", 
                            _loxStatFile.ValueCount, 
                            _loxStatFile.ValueCount == 1 ? "" : "s");
            for(int i = 2; i <= _loxStatFile.ValueCount; i++)
            {
                var newColumn = (DataGridViewColumn)columns[_valueColumnOffset].Clone();
                newColumn.HeaderText = string.Format("{0} {1}", newColumn.HeaderText, i);
                columns.Add(newColumn);
            }
            _dataGridView.RowCount = _loxStatFile.DataPoints.Count;
            _dataGridView.MultiSelect = true;

            RefreshProblems();
            RefreshChart();
        }

        private void RefreshProblems()
        {
            _problems = LoxStatProblem.GetProblems(_loxStatFile);
            _problemButton.Enabled = _problems.Any();
        }

        private void RefreshChart()
        {
            // Now we can set up the Chart:
            List<Color> colors = new List<Color> { Color.Green, Color.Red, Color.Black, Color.Blue, Color.Violet, Color.Turquoise, Color.YellowGreen, Color.AliceBlue, Color.Beige, Color.Chocolate, Color.CornflowerBlue, Color.Firebrick };

            _chart.Series.Clear();

            int skipHeaderColumns = 2;

            for (int i = skipHeaderColumns; i < _dataGridView.Columns.Count; i++)
            {
                Series S = _chart.Series.Add(_dataGridView.Columns[i].HeaderText);
                S.ChartType = SeriesChartType.Spline;
                S.Color = colors[i];
            }


            // and fill in all the values from the dgv to the chart:
            for (int i = 0; i < _dataGridView.Rows.Count; i++)
            {
                for (int j = 0; j < _chart.Series.Count; j++)
                {
                    int p = _chart.Series[j].Points.AddXY(_dataGridView[1, i].Value, _dataGridView[j+ skipHeaderColumns, i].Value);
                }
            }
        }

        private void ShowProblems()
        {
            MessageBox.Show
            (
                this,
                string.Format
                (
                    "{1} Problems (showing max 50):{0}{2}",
                    Environment.NewLine,
                    _problems.Count,
                    string.Join(Environment.NewLine, _problems.Take(50))
                ),
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = _fileNameTextBox.Text;
            openFileDialog.InitialDirectory = Path.GetDirectoryName(_fileNameTextBox.Text);
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                _fileNameTextBox.Text = openFileDialog.FileName;

            LoadFile();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            LoadFile();
        }

        private void DataGridView_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                var rowIndex = e.RowIndex;
                var dataPoint = _loxStatFile.DataPoints[rowIndex];
                var columnIndex = e.ColumnIndex;
                e.Value = (columnIndex == 0) ? (object)dataPoint.Index :
                    (columnIndex == 1) ? (object)dataPoint.Timestamp :
                    (object)dataPoint.Values[e.ColumnIndex - _valueColumnOffset];
            }
            catch
            {
                e.Value = null;
            }
        }

        private void DataGridView_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            var columnIndex = e.ColumnIndex;
            var dataPoint = _loxStatFile.DataPoints[e.RowIndex];
            if(columnIndex == 1)
                dataPoint.Timestamp = Convert.ToDateTime(e.Value);
            else if(columnIndex > 1)
                dataPoint.Values[columnIndex - _valueColumnOffset] =
                    Convert.ToDouble(e.Value.ToString());
            RefreshProblems();
            RefreshChart();
        }

        private void DataGridView_MouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("delete selected entries", mnuDeleteSelected_Click));
                m.MenuItems.Add(new MenuItem("insert entry", mnuInsertEntry_Click));
                m.MenuItems.Add(new MenuItem("Calc from row", mnuCalcfrom_Click));
                m.MenuItems.Add(new MenuItem("Calc selected", mnuCalcSelected_Click));
                //m.MenuItems[0].Enabled = false;
                //m.MenuItems[1].Enabled = false;

                if (_dataGridView.SelectedRows.Count < 1)
                {
                    m.MenuItems[0].Enabled = false;
                    m.MenuItems[1].Enabled = false;
                    m.MenuItems[2].Enabled = false;
                    m.MenuItems[3].Enabled = false;
                }

                if (_dataGridView.SelectedRows.Count < 2)
                {
                    //m.MenuItems[0].Enabled = false;
                    m.MenuItems[3].Enabled = false;
                }
                int currentMouseOverRow = e.RowIndex;

                m.Show(_dataGridView, new Point(e.Location.X, e.Location.Y));

            }
        }

        private void mnuCalcfrom_Click(object sender, EventArgs e)
        {
            string chrDec = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            string chrGrp = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
            string input = "V - 100";
            string pattern = "V";
            string replacement = "1";
            double myValue;
            string formula = "";
            bool isFormula = true;

            ShowInputDialog(ref input);

            if (chrDec == ",")
            {
                input = Regex.Replace(input, "/.", "");
                input = Regex.Replace(input, chrDec, chrGrp);
            }

            formula = Regex.Replace(input, pattern, replacement);
            try
            {
                myValue = Convert.ToDouble(new System.Data.DataTable().Compute(formula, null));
            } catch (System.Data.DataException de)
            {
                MessageBox.Show(de.Message);
                isFormula = false;
            }
            

            if (isFormula) {
                for (int x = _dataGridView.SelectedRows[0].Cells[0].RowIndex; x < _dataGridView.Rows.Count; x++)
                {
                    replacement = Convert.ToDouble(_dataGridView.Rows[x].Cells[2].Value).ToString("#.###", CultureInfo.CreateSpecificCulture("en-EN"));
                    formula = Regex.Replace(input, pattern, replacement);
                    try
                    {
                        myValue = Convert.ToDouble(new System.Data.DataTable().Compute(formula, null));
                    } catch (System.Data.DataException re)
                    {
                        myValue = Convert.ToDouble(replacement);
                    }

                    _dataGridView.Rows[x].Cells[2].Value = myValue.ToString();
                }
            }

        }

        private void mnuCalcSelected_Click(object sender, EventArgs e)
        {
            string input = "0";
            double myValue;
            ShowInputDialog(ref input);
            if (Double.TryParse(input, out myValue))
            {
                foreach (DataGridViewRow myRow in _dataGridView.SelectedRows)
                {
                    myRow.Cells[2].Value = (Convert.ToDouble(myRow.Cells[2].Value.ToString()) + Convert.ToDouble(input)).ToString();
                }
            }


        }

        private void mnuDeleteSelected_Click(object sender, EventArgs e)
        {
            int rowIndex = _dataGridView.SelectedRows[0].Index;
            //_loxStatFile.DataPoints.RemoveAt(rowIndex);
            for (int x = _dataGridView.SelectedRows.Count -1; x >= 0; x--)
            {
                _loxStatFile.DataPoints.RemoveAt(_dataGridView.SelectedRows[x].Index);
                _dataGridView.Rows.RemoveAt(_dataGridView.SelectedRows[x].Index);
            }
            //_dataGridView.Rows.Remove(_dataGridView.SelectedRows[0]);
            int rowCount = _dataGridView.RowCount;
            for (int x = rowIndex; x < rowCount; x++)
            {
                if (x == 0)
                {
                    _loxStatFile.DataPoints[x].Index = 0;
                    _dataGridView.Rows[x].Cells[0].Value = 0;
                }
                else
                {
                    _loxStatFile.DataPoints[x].Index = _loxStatFile.DataPoints[x - 1].Index + 1;
                    _dataGridView.Rows[x].Cells[0].Value = ((int)_dataGridView.Rows[x - 1].Cells[0].Value + 1).ToString();
                }
            }
            RefreshProblems();
            RefreshChart();
        }

        private void mnuInsertEntry_Click(object sender, EventArgs e)
        {

        }

        private static DialogResult ShowInputDialog(ref string input)
        {
            System.Drawing.Size size = new System.Drawing.Size(250, 130);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = "Name";

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 20, 23);
            textBox.Location = new System.Drawing.Point(10, 10);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Label lblInfo = new Label();
            lblInfo.Size = new Size(size.Width - 30, 60);
            lblInfo.Location = new Point(15, 37);
            lblInfo.Text = "type in a math formula including +,-,*,/. " +
                "Use V for the original value." +
                "Like: v - 100";
            inputBox.Controls.Add(lblInfo);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 99);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 99);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if((_args != null) && (_args.Length > 0))
            {
                _fileNameTextBox.Text = _args[0];
                LoadFile();
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            RefreshProblems();
            if(_problems.Any())
                ShowProblems();
            if(MessageBox.Show(
                this,
                "### DISCLAIMER ###\n" + 
                "- I will not (and can not) take any responsibility for any\n" + 
                "  issues of your Loxone installation whatsoever!\n" +
                "- Loxone will most likely not support any issues, if you use the\n" +
                "  files generated by this tool either!\n" +
                "- I am just a desparate Loxone user in need of a solution for a\n" +
                "  problem and lucky enough to be a professional coder!\n" +
                "- Loxone did not provide me a full file format documentation\n" +
                "  and I used Hexinator (thank you!) to decode the file format\n" + 
                "  to some degree!\n\n" +
                "### NOTICE ###\n" +
                "To apply the modified statistics further steps are needed!\n" +
                "1. Upload the modified statistics\n" +
                "2. Restart the Miniserver\n" + "" +
                "3. Clear the app cache\n" + 
                "If you don't know how to clear the app cache, then remove\n" +
                "and re-add the Miniserver in the app. Should this not help\n" + "" +
                "then uninstall and install the app again.\n\n" +
                "Would you still like to save the file?",
                "DISCLAIMER! USE AT YOUR OWN RISK!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
                ) != DialogResult.Yes)
                return;
            _loxStatFile.FileName = Path.GetFullPath(_fileNameTextBox.Text);
            _loxStatFile.Save();
            MessageBox.Show(this, "Save successful!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ProblemButton_Click(object sender, EventArgs e)
        {
            ShowProblems();
        }

        private void _dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void _dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {

                string CopiedContent = Clipboard.GetText();
                string[] Lines = CopiedContent.Split('\n');
                int StartingRow = _dataGridView.CurrentCell.RowIndex;
                int StartingColumn = _dataGridView.CurrentCell.ColumnIndex;
                foreach (var line in Lines)
                {
                    if (StartingRow <= (_dataGridView.Rows.Count - 1))
                    {
                        string[] cells = line.Split('\t');
                        int ColumnIndex = StartingColumn;
                        for (int i = 0; i < cells.Length && ColumnIndex <= (_dataGridView.Columns.Count - 1); i++)
                        {
                            if (!String.IsNullOrEmpty(cells[i]))
                            {
                                var columnIndex = ColumnIndex++;
                                var dataPoint = _loxStatFile.DataPoints[StartingRow];
                                dataPoint.Values[columnIndex - _valueColumnOffset] = Convert.ToDouble(cells[i].ToString());
                            }
                        }
                        StartingRow++;
                    }
                }
                RefreshProblems();
                RefreshChart();
                _dataGridView.Refresh();
            }
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            int distance = 5; // Set this to the desired distance between _chart and _dataGridView
            int availableWidth = this.ClientSize.Width - distance - 23; // window width - distance - padding

            _dataGridView.Width = (int)(availableWidth * 0.45);
            _chart.Left = _dataGridView.Right + distance;
            _chart.Width = (int)(availableWidth * 0.55);
        }

     
    }
}
