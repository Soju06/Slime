using System;
using System.IO;
using System.Net;
namespace FTPClient
{
    // FTP 클라이언트를 제공합니다.
    /// <summary>        
    /// FTP 클라이언트를 제공합니다.
    /// </summary>
    public class FTP
    {
        #region Download
        // FTP에서 파일을 다운로드합니다.
        /// <summary>        
        /// FTP에서 파일을 다운로드합니다.
        /// </summary>
        /// <returns>
        /// 성공시 true, 실패시 false를 리턴합니다.
        /// </returns>
        /// <example>
        /// <code>
        /// if (FTP.Download("D:\FTPClient.dll","ftp://ftp.soju.cf/FTPClient.dll","admin","a1a2a3a4"))
        ///     Console.WriteLine("Download success!");
        /// else
        ///     Console.WriteLine("Download failed!");
        /// </code>
        /// </example>
        public static bool Download(string Path, string ftpPath, string user, string password)
        {
            try
            {
                FileInfo file = new FileInfo(Path);
                if (!file.Directory.Exists)
                    file.Directory.Create();
                using (WebClient cli = new WebClient())
                {
                    cli.Credentials = new NetworkCredential(user, password);
                    cli.DownloadFile(ftpPath, Path);
                    if(cli != null)
                        cli.Dispose();
                }
                if (File.Exists(Path))
                    return true;
                else return false;
            }
            catch { return false; }
        }
        // 익명인증을 사용하여 FTP에서 파일을 다운로드합니다.
        /// <summary>        
        /// 익명인증을 사용하여 FTP에서 파일을 다운로드합니다.
        /// </summary>
        /// <returns>
        /// 성공시 true, 실패시 false를 리턴합니다.
        /// </returns>
        /// <example>
        /// <code>
        /// if (FTP.Download("D:\FTPClient.dll","ftp://ftp.soju.cf/FTPClient.dll"))
        ///     Console.WriteLine("Download success!");
        /// else
        ///     Console.WriteLine("Download failed!");
        /// </code>
        /// </example>
        public static bool Download(string Path, string ftpPath)
        {
            try
            {
                FileInfo file = new FileInfo(Path);
                if (!file.Directory.Exists)
                    file.Directory.Create();
                using (WebClient cli = new WebClient())
                {
                    cli.Credentials = new NetworkCredential("anonymous", "");
                    cli.DownloadFile(ftpPath, Path);
                    if (cli != null)
                        cli.Dispose();
                }
                if (File.Exists(Path))
                    return true;
                else return false;
            }
            catch { return false; }
        }
        #endregion
        #region Upload
        //FTP에 파일을 업로드합니다.
        /// <summary>
        /// FTP에 파일을 업로드합니다.
        /// </summary>
        /// <returns>
        /// 성공시 true, 실패시 false를 리턴합니다.
        /// </returns>
        /// <example>
        /// <code>
        /// if (FTP.Upload("D:\FTPClient.dll","ftp://ftp.soju.cf/FTPClient.dll","admin","a1a2a3a4"))
        ///     Console.WriteLine("Upload success!");
        /// else
        ///     Console.WriteLine("Upload failed!");
        /// </code>
        /// </example>
        public static bool Upload(string FilePath, string ftpPath, string user, string password)
        {
            try
            {
                using (WebClient cli = new WebClient())
                {
                    cli.Credentials = new NetworkCredential(user, password);
                    cli.UploadFile(ftpPath, FilePath);
                    if (cli != null)
                        cli.Dispose();
                }
                if (Exists(ftpPath))
                    return true;
                else return false;
            }
            catch { return false; }
        }
        //익명인증을 사용하여 FTP에 파일을 업로드합니다.
        /// <summary>        
        /// 익명인증을 사용하여 FTP에 파일을 업로드합니다.
        /// </summary>
        /// <returns>
        /// 성공시 true, 실패시 false를 리턴합니다.
        /// </returns>
        /// <example>
        /// <code>
        /// if (FTP.Upload("D:\FTPClient.dll","ftp://ftp.soju.cf/FTPClient.dll))
        ///     Console.WriteLine("Upload success!");
        /// else
        ///     Console.WriteLine("Upload failed!");
        /// </code>
        /// </example>
        public static bool Upload(string FilePath, string ftpPath)
        {
            try
            {
                using (WebClient cli = new WebClient())
                {
                    cli.Credentials = new NetworkCredential("anonymous", "");
                    cli.UploadFile(ftpPath, FilePath);
                    if (cli != null)
                        cli.Dispose();
                }
                if (Exists(ftpPath))
                    return true;
                else return false;
            }
            catch { return false; }
        }
        #endregion
        #region CreateDirectory
        //FTP에 폴더를 생성합니다.
        /// <summary>
        /// FTP에 폴더를 생성합니다.
        /// </summary>
        /// <returns>
        /// 성공시 true, 실패시 false를 리턴합니다.
        /// </returns>
        /// <example>
        /// <code>
        /// if (FTP.CreateDirectory("ftp://ftp.soju.cf/FTPClient","admin","a1a2a3a4"))
        ///     Console.WriteLine("Create success!");
        /// else
        ///     Console.WriteLine("Create failed!");
        /// </code>
        /// </example>
        public static bool CreateDirectory(string ftpPath, string user, string password)
        {
            try
            {
                FtpWebRequest ftpRequest = WebRequest.Create(new Uri(ftpPath)) as FtpWebRequest;
                if (ftpRequest == null)
                    return false;
                ftpRequest.Credentials = new NetworkCredential(user, password);
                ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                FtpWebResponse ftpResponse = ftpRequest.GetResponse() as FtpWebResponse;
                if (ftpResponse == null)
                {
                    ftpRequest.Abort();
                    return false;
                }
                using (ftpResponse)
                {
                    bool st = ftpResponse.StatusCode.Equals(FtpStatusCode.PathnameCreated);
                    ftpRequest.Abort();
                    return st; 
                }
            }
            catch { return false; }
        }
        //익명인증을 사용하여 FTP에 폴더를 생성합니다.
        /// <summary>
        /// 익명인증을 사용하여 FTP에 폴더를 생성합니다.
        /// </summary>
        /// <returns>
        /// 성공시 true, 실패시 false를 리턴합니다.
        /// </returns>
        /// <example>
        /// <code>
        /// if (FTP.CreateDirectory("ftp://ftp.soju.cf/FTPClient"))
        ///     Console.WriteLine("Create success!");
        /// else
        ///     Console.WriteLine("Create failed!");
        /// </code>
        /// </example>
        public static bool CreateDirectory(string ftpPath)
        {
            try
            {
                FtpWebRequest ftpRequest = WebRequest.Create(new Uri(ftpPath)) as FtpWebRequest;
                if (ftpRequest == null)
                    return false;
                ftpRequest.Credentials = new NetworkCredential("anonymous", "");
                ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                FtpWebResponse ftpResponse = ftpRequest.GetResponse() as FtpWebResponse;
                if (ftpResponse == null)
                {
                    ftpRequest.Abort();
                    return false;
                }
                using (ftpResponse)
                {
                    bool st = ftpResponse.StatusCode.Equals(FtpStatusCode.PathnameCreated);
                    ftpRequest.Abort();
                    return st;
                }
            }
            catch { return false; }
        }
        #endregion
        #region Exists
        //FTP에 파일이 있는지 확인합니다
        /// <summary>
        /// FTP에 파일이 있는지 확인합니다
        /// </summary>
        /// <returns>
        /// 성공시 true, 실패시 false를 리턴합니다.
        /// </returns>
        /// <example>
        /// <code>
        /// if (FTP.Exists("ftp://ftp.soju.cf/FTPClient.dll"))
        ///     Console.WriteLine("Exist!");
        /// else
        ///     Console.WriteLine("not exist!");
        /// </code>
        /// </example>
        public static bool Exists(string FilePath, string user, string password)
        {
            bool IsExists = true;
            FtpWebRequest reqFTP = null;
            FtpWebResponse respFTP = null;
            try
            {
                UriBuilder URI = new UriBuilder(FilePath);

                URI.Scheme = "ftp";
                reqFTP = (FtpWebRequest)WebRequest.Create(URI.Uri);
                reqFTP.Credentials = new NetworkCredential(user, password);
                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                respFTP = (FtpWebResponse)reqFTP.GetResponse();
                if (respFTP.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    IsExists = false;

            }
            catch { IsExists = false; }
            if (reqFTP != null)
                reqFTP.Abort();
            if (respFTP != null)
                respFTP.Dispose();
            return IsExists;
        }
        //익명인증을 사용하여 FTP에 파일이 있는지 확인합니다
        /// <summary>
        /// 익명인증을 사용하여 FTP에 파일이 있는지 확인합니다
        /// </summary>
        /// <returns>
        /// 성공시 true, 실패시 false를 리턴합니다.
        /// </returns>
        /// <example>
        /// <code>
        /// if (FTP.Exists("ftp://ftp.soju.cf/FTPClient.dll"))
        ///     Console.WriteLine("Exist!");
        /// else
        ///     Console.WriteLine("not exist!");
        /// </code>
        /// </example>
        public static bool Exists(string FilePath)
        {
            bool IsExists = true;
            FtpWebRequest reqFTP = null;
            FtpWebResponse respFTP = null;
            try
            {
                UriBuilder URI = new UriBuilder(FilePath);

                URI.Scheme = "ftp";
                reqFTP = (FtpWebRequest)WebRequest.Create(URI.Uri);
                reqFTP.Credentials = new NetworkCredential("anonymous", "");
                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                respFTP = (FtpWebResponse)reqFTP.GetResponse();
                if (respFTP.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    IsExists = false;
            }
            catch { IsExists = false; }
            if (reqFTP != null)
                reqFTP.Abort();
            if (respFTP != null)
                respFTP.Dispose();
            return IsExists;
        }
        #endregion
    }
}