using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoxStatEdit
{
    public partial class MiniserverForm: Form
    {

        #region class FileItem
        class FileItem: IComparable<FileItem>
        {
            internal MsFileInfo MsFileInfo;
            internal FileInfo FileInfo;
            private string _name;

            public string FileName
            {
                get
                {
                    return (MsFileInfo != null) ? MsFileInfo.FileName : FileInfo.Name;
                }
            }

            public string Name
            {
                get
                {
                    if(_name == null)
                    {
                        try
                        {
                            if(FileInfo != null)
                                _name = LoxStatFile.Load(FileInfo.FullName, true).Text;
                        }
                        catch(Exception)
                        {
                            _name = null;
                        }
                        if(string.IsNullOrEmpty(_name))
                            //_name = FileName;
                            _name = "Download to view description";
                    }
                    return _name;
                }
            }

            public DateTime YearMonth
            {
                get
                {
                    return LoxStatFile.GetYearMonth(FileName);
                }
            }

            public string Status
            {
                get
                {
                    if(FileInfo == null)
                    {
                        return "Only on MS";
                    }
                    if(MsFileInfo == null)
                    {
                        return "Only on FS";
                    }
                    if(FileInfo.LastWriteTime > MsFileInfo.Date)
                    {
                        return "Newer on FS";
                    }
                    if(FileInfo.LastWriteTime < MsFileInfo.Date)
                    {
                        return "Newer on MS";
                    }
                    if(FileInfo.Length > MsFileInfo.Size)
                    {
                        return "Larger on FS";
                    }
                    if(FileInfo.Length < MsFileInfo.Size)
                    {
                        return "Larger on MS";
                    }
                    return "Same date/size";
                }
            }

            public override string ToString()
            {
                return string.Format("{0} ({1})", Name, Status);
            }

            public int CompareTo(FileItem other)
            {
                var c = StringComparer.OrdinalIgnoreCase.Compare(Name, other.Name);
                return (c != 0) ? c : YearMonth.CompareTo(other.YearMonth);
            }
        }

        #endregion

        #region Fields
        readonly string[] _args;
        IList<MsFileInfo> _msFolder = new MsFileInfo[0];
        IList<FileInfo> _localFolder = new FileInfo[0];
        IList<FileItem> _fileItems;

        #endregion

        #region Constructor
        public MiniserverForm(params string[] args)
        {
            _args = args;
            InitializeComponent();
        }

        #endregion

        #region Methods
        private void RefreshGridView()
        {
            // provide a empty list to prevent crash
            if (_msFolder == null)
            {
                _msFolder = new List<MsFileInfo>();
            }

            var msMap = _msFolder.ToLookup(e => e.FileName, StringComparer.OrdinalIgnoreCase);
            var localMap = _localFolder.ToLookup(e => e.Name, StringComparer.OrdinalIgnoreCase);

            // Create a new SortableBindingList
            _fileItems = new SortableBindingList<FileItem>();

            // Get the data
            var data = msMap.Select(e => e.Key).Union(localMap.Select(e => e.Key)).
                Select(f => new FileItem
                {
                    MsFileInfo = msMap[f].FirstOrDefault(),
                    FileInfo = localMap[f].FirstOrDefault(),
                }).
                // Order by filename if available else by name
                OrderBy(f => f.MsFileInfo?.FileName ?? f.FileInfo?.Name).
                ToList();

            // Add the data to the SortableBindingList
            foreach (var item in data)
            {
                _fileItems.Add(item);
            }

            // Disable automatic column generation
            _dataGridView.AutoGenerateColumns = false;

            // Clear existing columns
            _dataGridView.Columns.Clear();

            // Add columns manually in the order you want
            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn {
                DataPropertyName = "FileName",
                HeaderText = "File",
                Width = 250
            });
            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn {
                DataPropertyName = "Name",
                HeaderText = "Description",
                Width = 250
            });
            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn {
                DataPropertyName = "YearMonth",
                HeaderText = "Year/Month",
                Width = 90,
                DefaultCellStyle = { Format = "yyyy-MM" }
            });
            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn {
                DataPropertyName = "Status",
                HeaderText = "Status",
                Width = 100
            });
            _dataGridView.Columns.Add(new DataGridViewButtonColumn {
                DataPropertyName = "Download",
                HeaderText = "Download",
                Width = 60
            });
            _dataGridView.Columns.Add(new DataGridViewButtonColumn {
                DataPropertyName = "Edit",
                HeaderText = "Edit",
                Width = 50
            });
            _dataGridView.Columns.Add(new DataGridViewButtonColumn {
                DataPropertyName = "Upload",
                HeaderText = "Upload",
                Width = 60
            });

            // Bind the SortableBindingList to the DataGridView
            _dataGridView.DataSource = _fileItems;
        }

        private void RefreshMs()
        {
            var uriBuilder = GetMsUriBuilder();
            if (uriBuilder != null) {
                _msFolder = MsFileInfo.Load(uriBuilder.Uri);
            }
        }

        private void RefreshLocal()
        {
            Directory.CreateDirectory(_folderTextBox.Text);
            _localFolder = Directory.EnumerateFiles(_folderTextBox.Text).
                Select(fileName => new FileInfo(fileName)).ToList();
        }

        private UriBuilder GetMsUriBuilder()
        {
            try
            {
                // escape # in uri by replacing with hex
                var uriBuilder = new UriBuilder(new Uri(_urlTextBox.Text.Replace("#", "%23")).
                    GetComponents(UriComponents.Scheme | UriComponents.UserInfo |
                    UriComponents.Host | UriComponents.Port, UriFormat.UriEscaped));
                uriBuilder.Path = "stats";
                return uriBuilder;
            }
            catch (UriFormatException ex)
            {
                MessageBox.Show(ex.Message, "Error - UriBuilder", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
        }


        private Uri GetFileNameUri(string fileName)
        {
            var uriBuilder = GetMsUriBuilder();
            uriBuilder.Path += "/" + fileName;
            return uriBuilder.Uri;
        }

        private bool Download(FileItem fileItem)
        {
            try
            {
                var ftpWebRequest = (FtpWebRequest)FtpWebRequest.
                Create(GetFileNameUri(fileItem.FileName));
                ftpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                var targetFileName = Path.Combine(_folderTextBox.Text, fileItem.FileName);
                using(var response = ftpWebRequest.GetResponse())
                using(var ftpStream = response.GetResponseStream())
                using(var fileStream = File.OpenWrite(targetFileName))
                ftpStream.CopyTo(fileStream);                
                File.SetLastWriteTime(targetFileName, fileItem.MsFileInfo.Date);

                // Log FTP output
                // File.AppendAllText("./custom.log", $"Downloaded {fileItem.FileName} - Setting last write time to {fileItem.MsFileInfo.Date}\n\n");

                return true;
            }
            catch (WebException ex)
            {
                var response = ex.Response as FtpWebResponse;
                if (response != null)
                {
                    MessageBox.Show(ex.Message, "Error  - FTP connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"# Message\n{ex.Message}\n\n# Data\n{ex.Data}\n\n# StackTrace\n{ex.StackTrace}", "Error - IList", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        private bool Upload(FileItem fileItem)
        {
            try
            {
                var ftpWebRequest = (FtpWebRequest)FtpWebRequest.Create(GetFileNameUri(fileItem.FileName));
                ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;
                using (var fileStream = File.OpenRead(
                    Path.Combine(_folderTextBox.Text, fileItem.FileInfo.FullName)))
                using (var ftpStream = ftpWebRequest.GetRequestStream())
                    fileStream.CopyTo(ftpStream);
                using (var response = ftpWebRequest.GetResponse()) { }

                return true;
            }
            catch (WebException ex)
            {
                var response = ex.Response as FtpWebResponse;
                if (response != null)
                {
                    MessageBox.Show(ex.Message, "Error  - FTP connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"# Message\n{ex.Message}\n\n# Data\n{ex.Data}\n\n# StackTrace\n{ex.StackTrace}", "Error - IList", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        #endregion

        #region Events
        private void RefreshMsButton_Click(object sender, EventArgs e)
        {
            RefreshMs();
            RefreshGridView();
        }

        private void RefreshFolderButton_Click(object sender, EventArgs e)
        {
            RefreshLocal();
            RefreshGridView();
        }

        private void _urlTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(_folderTextBox.Text))
            {
                RefreshMsButton_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void _folderTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(_folderTextBox.Text))
            {
                RefreshFolderButton_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void MiniserverForm_Load(object sender, EventArgs e)
        {
            _folderTextBox.Text = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.MyDocuments), "LoxStatEdit");
            if(_args.Length >= 1)
            {
                _urlTextBox.Text = _args[0];
                RefreshMs();
            }
            if(_args.Length >= 2)
                _folderTextBox.Text = Path.GetFullPath(_args[1]);
            RefreshLocal();
            RefreshGridView();
        }

        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1) // The Name column is at index 1
            {
                var fileItem = _fileItems[e.RowIndex];
                if (fileItem.Name == "Download to view description")
                {
                    e.CellStyle.Font = new System.Drawing.Font(e.CellStyle.Font, System.Drawing.FontStyle.Italic);
                }
                else
                {
                    e.CellStyle.Font = new System.Drawing.Font(e.CellStyle.Font, System.Drawing.FontStyle.Regular);
                }
            }
            if (e.ColumnIndex == 3) // The Status column is at index 3
            {
                var fileItem = _fileItems[e.RowIndex];
                switch (fileItem.Status)
                {
                    case "Only on MS":
                        e.CellStyle.BackColor = System.Drawing.Color.LightGray;
                        break;
                    case "Only on FS":
                        e.CellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow;
                        break;
                    case "Newer on MS":
                        e.CellStyle.BackColor = System.Drawing.Color.LightGreen;
                        break;
                    case "Newer on FS":
                        e.CellStyle.BackColor = System.Drawing.Color.LightSkyBlue;
                        break;
                    case "Larger on MS":
                        e.CellStyle.BackColor = System.Drawing.Color.LightGreen;
                        break;
                    case "Larger on FS":
                        e.CellStyle.BackColor = System.Drawing.Color.LightSkyBlue;
                        break;
                    default:
                        e.CellStyle.BackColor = System.Drawing.Color.White;
                        break;
                }
            }

        }
        
        private void DataGridView_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                var fileItem = _fileItems[e.RowIndex];
                switch(e.ColumnIndex)
                {
                    case 0: e.Value = fileItem.FileName; break;
                    case 1: e.Value = fileItem.Name; break;
                    case 2: e.Value = fileItem.YearMonth; break;
                    case 3: e.Value = fileItem.Status; break;
                    case 4: e.Value = "Download"; break;
                    case 5: e.Value = "Edit"; break;
                    case 6: e.Value = "Upload"; break;
                    default: e.Value = null; break;
                }
            }
            catch
            {
            #if DEBUG
                Debugger.Break();
            #endif
                e.Value = null;
            }
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex < 0) return; //When clicking the header row
            var fileItem = _fileItems[e.RowIndex];
            switch(e.ColumnIndex)
            {
                case 4: //Download
                    progressBar.Maximum = 1;
                    progressBar.Value = 0;
                    progressLabel.Text = "Starting download...";
                    if(Download(fileItem))
                    {
                        progressBar.Value = 1;
                        progressLabel.Text = "Download complete!";
                        RefreshLocal();
                        RefreshGridView();
                    }
                    else
                    {
                        progressBar.Value = 1;
                        progressLabel.Text = "Download failed!";
                    }
                    break;
                case 5: //Edit
                    // show error message, if file is not downloaded yet
                    if (fileItem.FileInfo == null)
                    {
                        MessageBox.Show($"The file \"{fileItem.FileName}\" cannot be edited, since it's not available on the filesystem.\n\nPlease download it first.", "Error - File not downloaded", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }

                    Console.WriteLine(fileItem.FileInfo.FullName);
                    using(var form = new LoxStatFileForm(fileItem.FileInfo.FullName))
                        form.ShowDialog(this);
                    break;
                case 6: //Upload
                    // show error message, if there is no file to upload
                    if (fileItem.FileInfo == null)
                    {
                        MessageBox.Show($"The file \"{fileItem.FileName}\" cannot be uploaded, since it's not available on the filesystem.\n\nPlease download it first.", "Error - File not downloaded", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }

                    progressBar.Maximum = 1;
                    progressBar.Value = 0;
                    progressLabel.Text = "Starting upload...";
                    if(Upload(fileItem))
                    {
                        progressBar.Value = 1;
                        progressLabel.Text = "Upload complete!";
                        RefreshMs();
                        RefreshGridView();
                    }
                    else
                    {
                        progressBar.Value = 1;
                        progressLabel.Text = "Upload failed!";
                    }
                    break;
            }
        }

        private async void DownloadButton_Click(object sender, EventArgs e)
        {
            progressBar.Maximum = _dataGridView.SelectedRows.Count;
            progressBar.Value = 0;
            progressLabel.Text = "Starting download...";

            foreach(DataGridViewRow row in _dataGridView.SelectedRows)
            {
                int rowIndex = row.Index; // Capture the index for the closure
                bool result = await Task.Run(() => Download(_fileItems[rowIndex]));
                if (!result)
                {
                    progressBar.Value = progressBar.Maximum;
                    progressLabel.Text = "Download failed!";
                    return;
                }

                progressBar.Value++;
                progressLabel.Text = $"Downloaded {progressBar.Value} of {progressBar.Maximum}";
            }

            progressLabel.Text = "Download complete!";

            RefreshLocal();
            RefreshGridView();
        }

        private async void UploadButton_Click(object sender, EventArgs e)
        {
            progressBar.Maximum = _dataGridView.SelectedRows.Count;
            progressBar.Value = 0;
            progressLabel.Text = "Starting upload...";

            foreach(DataGridViewRow row in _dataGridView.SelectedRows)
            {
                int rowIndex = row.Index; // Capture the index for the closure

                // show error message, if there is no file to upload
                if (_fileItems[rowIndex].FileInfo == null)
                {
                    progressBar.Value = progressBar.Maximum;
                    progressLabel.Text = "Upload failed!";
                    MessageBox.Show($"The file \"{_fileItems[rowIndex].FileName}\" cannot be uploaded, since it's not available on the filesystem.\n\nPlease download it first.", "Error - File not downloaded", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool result = await Task.Run(() => Upload(_fileItems[rowIndex]));
                if (!result)
                {
                    progressBar.Value = progressBar.Maximum;
                    progressLabel.Text = "Download failed!";
                    return;
                }

                progressBar.Value++;
                progressLabel.Text = $"Uploaded {progressBar.Value} of {progressBar.Maximum}";
            }
            progressLabel.Text = "Upload complete!";
        }

        // Launch project link
        private void githubLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/mr-manuel/Loxone_LoxStatEdit");
        }

        private void donateLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.md0.eu/links/loxstatsedit-donate");
        }

        #endregion

    }
}
