using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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
                            _name = FileName;
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
            _fileItems = msMap.Select(e => e.Key).Union(localMap.Select(e => e.Key)).
                Select(f => new FileItem
                {
                    MsFileInfo = msMap[f].FirstOrDefault(),
                    FileInfo = localMap[f].FirstOrDefault(),
                }).
                OrderBy(f => f).
                ToList();
            _dataGridView.RowCount = _fileItems.Count;
            _dataGridView.Refresh();
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

        private void Download(FileItem fileItem)
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

            }
            catch (WebException ex)
            {
                var response = ex.Response as FtpWebResponse;
                if (response != null)
                {
                    MessageBox.Show($"# Message\n{ex.Message}\n\n# Data\n{ex.Data}\n\n# StackTrace\n{ex.StackTrace}", "Error  - FTP connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"# Message\n{ex.Message}\n\n# Data\n{ex.Data}\n\n# StackTrace\n{ex.StackTrace}", "Error - IList", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Upload(FileItem fileItem)
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
            }
            catch (WebException ex)
            {
                var response = ex.Response as FtpWebResponse;
                if (response != null)
                {
                    MessageBox.Show($"# Message\n{ex.Message}\n\n# Data\n{ex.Data}\n\n# StackTrace\n{ex.StackTrace}", "Error  - FTP connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"# Message\n{ex.Message}\n\n# Data\n{ex.Data}\n\n# StackTrace\n{ex.StackTrace}", "Error - IList", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void DataGridView_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                var fileItem = _fileItems[e.RowIndex];
                switch(e.ColumnIndex)
                {
                    case 0: e.Value = fileItem.Name; break;
                    case 1: e.Value = fileItem.YearMonth; break;
                    case 2: e.Value = fileItem.Status; break;
                    case 3: e.Value = "Download"; break;
                    case 4: e.Value = "Edit"; break;
                    case 5: e.Value = "Upload"; break;
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
                case 3: //Download
                    Download(fileItem);
                    RefreshLocal();
                    RefreshGridView();
                    break;
                case 4: //Edit
                    using(var form = new LoxStatFileForm(fileItem.FileInfo.FullName))
                        form.ShowDialog(this);
                    break;
                case 5: //Upload
                    Upload(fileItem);
                    RefreshMs();
                    RefreshGridView();
                    break;
            }
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in _dataGridView.SelectedRows)
                Download(_fileItems[row.Index]);
            RefreshLocal();
            RefreshGridView();
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in _dataGridView.SelectedRows)
                Upload(_fileItems[row.Index]);
            RefreshMs();
            RefreshGridView();
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
