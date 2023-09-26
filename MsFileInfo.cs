using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LoxStatEdit
{
    public class MsFileInfo
    {
        public string FileName { get; private set; }
        public DateTime Date { get; private set; }
        public int Size { get; private set; }

        public static IList<MsFileInfo> Load(Uri uri)
        {
            try
            {
                var list = new List<MsFileInfo>();
                var ftpWebRequest = (FtpWebRequest)FtpWebRequest.Create(uri);
                ftpWebRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                using (var response = ftpWebRequest.GetResponse())
                using (var ftpStream = response.GetResponseStream())
                using (var streamReader = new StreamReader(ftpStream))
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();

                    // string pattern that matches Miniserver Gen 1 and Miniserver Gen 2
                    string pattern = @"[-rwx]{10}\s+[0-9]+\s+[0-9]+\s+[0-9]+\s+([0-9]+)\s+([A-Za-z]{3}\s+[0-9]{1,2}\s+[0-9:]+)\s+([0-9a-z_\-\.]+)";
                    var result = Regex.Match(line, pattern);

                    if (result.Success)
                    {
                        var groups = result.Groups;
                        int.TryParse(groups[1].Value, out int size);

                        DateTime dateTime;
                        if (DateTime.TryParseExact(groups[2].Value.Replace("  ", " "), "MMM dd HH:mm",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime)) ;
                        else if (DateTime.TryParseExact(groups[2].Value.Replace("  ", " "), "MMM dd yyyy",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime)) ;

                        var fileName = groups[3].Value;
                        
                        list.Add(new MsFileInfo
                        {
                            FileName = fileName,
                            Date = dateTime,
                            Size = size,
                        });
                    }
                }
                return list;
            }
            catch (WebException ex)
            {
                var response = ex.Response as FtpWebResponse;
                if (response != null)
                {
                    MessageBox.Show(ex.Message, "Error  - FTP connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error - IList", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
        }
    }
}
