using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;

namespace ToasoanTTXVN.UploadFileMulti
{
    public interface IFtpClient
    {
        /// <summary>
        /// Đẩy tệp lên ftp server
        /// </summary>
        /// <param name="sourcePath">Đường dẫn đến tệp cần đẩy</param>
        /// <param name="destinationPath">Đường dẫn + tên tệp cần lưu trên server</param>
        /// <returns>FtpStatusCode</returns>
        FtpStatusCode UploadFile(string sourcePath, string destinationPath);

        /// <summary>
        /// Đẩy tệp lên ftp server
        /// </summary>
        /// <param name="fileStream">Stream của tệp cần đẩy</param>
        /// <param name="destinationPath">Đường dẫn tên tệp cần lưu trên server</param>
        /// <returns>FtpStatusCode</returns>
        FtpStatusCode UploadFile(Stream fileStream, string destinationPath);

        /// <summary>
        /// Đẩy tệp lên ftp server
        /// </summary>
        /// <param name="fileStream">Stream của tệp cần đẩy</param>
        /// <param name="destinationPath">Đường dẫn tên tệp cần lưu trên server</param>
        /// <returns>FtpStatusCode</returns>
        FtpStatusCode UploadFile(byte[] fileData, string destinationPath);


        /// <summary>
        /// Tạo folder mới trên ftp server
        /// </summary>
        /// <param name="folderPath">Tên folder tạo</param>
        /// <returns>FtpStatusCode</returns>
        FtpStatusCode CreateFolder(string folderPath);

        /// <summary>
        /// Tải tệp từ ftp server
        /// </summary>
        /// <param name="filePath">Đường dẫn đến tệp cần tải</param>
        /// <returns>Stream của tệp</returns>
        byte[] DownloadFile(string filePath);

        /// <summary>
        /// Xóa tệp trên ftp server
        /// </summary>
        /// <param name="filePath">Đường đến tệp cần xóa</param>
        /// <returns>FtpStatusCode</returns>
        //FtpStatusCode DeleteFile(string filePath);

        /// <summary>
        /// Kiểm tra xem folder có tồn tại trên server hay không
        /// </summary>
        /// <param name="folderPath">Đường dẫn đến folder</param>
        /// <returns>Boolean: true nếu folder tồn tại</returns>
        bool IsExistFolder(string folderPath);
    }

    public class FtpClient : IFtpClient
    {
        private static ICredentials ftpCredential;
        private static string ftpRootPath;
        private static IWebProxy ftpProxy;

        private const string FTPSERVERKEY = @"ftpServer";
        private const string FTPUSERNAMEKEY = @"ftpUser";
        private const string FTPPASSWORDKEY = @"ftpPassword";
        private const string FTPPROXYKEY = @"ftpProxy";

        public FtpClient()
        {
            NameValueCollection collection = ConfigurationManager.AppSettings;
            ftpRootPath = collection[FTPSERVERKEY];
            ftpCredential = new NetworkCredential(collection[FTPUSERNAMEKEY], collection[FTPPASSWORDKEY]);
            var proxy = collection[FTPPROXYKEY];
            if (!string.IsNullOrEmpty(proxy))
            {
                ftpProxy = new WebProxy(collection[FTPPROXYKEY]);
            }
            else
            {
                ftpProxy = null;
            }
            if (string.IsNullOrEmpty(ftpRootPath))
            {
                // Thêm code throw lỗi không có đường dẫn vào đây
            }
        }

        public FtpClient(string ftpServer, string ftpuser, string ftpPass, string ftpProxyKey)
        {
            ftpRootPath = ftpServer;
            ftpCredential = new NetworkCredential(ftpuser, ftpPass);
            var proxy = ftpProxyKey;
            if (!string.IsNullOrEmpty(proxy))
            {
                ftpProxy = new WebProxy(ftpProxyKey);
            }
            else
            {
                ftpProxy = null;
            }
            if (string.IsNullOrEmpty(ftpRootPath))
            {
                // Thêm code throw lỗi không có đường dẫn vào đây
            }
        }


        public FtpStatusCode UploadFile(string sourcePath, string destinationPath)
        {
            destinationPath = ClearnPath(destinationPath);
            string folder = GetFolderPath(destinationPath);
            if (!string.IsNullOrEmpty(folder))
            {
                if (!IsExistFolder(folder))
                {
                    CreateFolder(folder);
                }
            }
            string remoteUri = Path.Combine(ftpRootPath, destinationPath);
            var request = WebRequest.Create(remoteUri);
            request.Proxy = ftpProxy;
            request.Credentials = ftpCredential;
            request.Method = WebRequestMethods.Ftp.UploadFile;
            byte[] fileContents = null;
            using (FileStream fs = File.OpenRead(sourcePath))
            {
                fileContents = ReadFully(fs);
            }

            request.ContentLength = fileContents.Length;
            var requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);

            requestStream.Close();

            var response = (FtpWebResponse)request.GetResponse();
            FtpStatusCode status = response.StatusCode;
            response.Close();
            return status;
        }

        public FtpStatusCode UploadFile(Stream fileStream, string destinationPath)
        {
            destinationPath = ClearnPath(destinationPath);
            string folder = GetFolderPath(destinationPath);
            if (!string.IsNullOrEmpty(folder))
            {
                if (!IsExistFolder(folder))
                {
                    CreateFolder(folder);
                }
            }
            fileStream.Position = 0;
            string remoteUri = Path.Combine(ftpRootPath, destinationPath);
            var request = WebRequest.Create(remoteUri);
            request.Proxy = ftpProxy;
            request.Credentials = ftpCredential;
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Timeout = -1;
            byte[] fileContents = ReadFully(fileStream);
            request.ContentLength = fileContents.Length;
            var requestStream = request.GetRequestStream();
            //byte[] buffer = new byte[2048];
            //int read = 0;
            //while ((read = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            //{
            //    requestStream.Write(buffer, 0, read);
            //}
            //requestStream.Flush();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();

            var response = (FtpWebResponse)request.GetResponse();
            FtpStatusCode status = response.StatusCode;
            response.Close();
            return status;
        }

        public FtpStatusCode UploadFile(byte[] fileData, string destinationPath)
        {
            destinationPath = ClearnPath(destinationPath);
            string folder = GetFolderPath(destinationPath);
            if (!string.IsNullOrEmpty(folder))
            {
                if (!IsExistFolder(folder))
                {
                    CreateFolder(folder);
                }
            }
            string remoteUri = Path.Combine(ftpRootPath, destinationPath);
            var request = WebRequest.Create(remoteUri);
            request.Proxy = ftpProxy;
            request.Credentials = ftpCredential;
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Timeout = -1;
            request.ContentLength = fileData.Length;
            var requestStream = request.GetRequestStream();

            requestStream.Write(fileData, 0, fileData.Length);
            requestStream.Close();

            var response = (FtpWebResponse)request.GetResponse();
            FtpStatusCode status = response.StatusCode;
            response.Close();
            return status;
        }

        public FtpStatusCode CreateFolder(string folderPath)
        {
            folderPath = ClearnPath(folderPath);
            var folderArray = folderPath.Split('/');
            if (folderPath.Any())
            {
                var traversalPath = "";
                foreach (var f in folderArray)
                {
                    if (string.IsNullOrEmpty(traversalPath))
                    {
                        traversalPath = f;
                    }
                    else
                    {
                        traversalPath = Path.Combine(traversalPath, f);
                    }
                    if (!IsExistFolder(traversalPath))
                    {
                        try
                        {
                            string remoteUri = Path.Combine(ftpRootPath, traversalPath);
                            var request = WebRequest.Create(remoteUri);
                            request.Proxy = ftpProxy;
                            request.Credentials = ftpCredential;
                            request.Method = WebRequestMethods.Ftp.MakeDirectory;
                            var response = (FtpWebResponse)request.GetResponse();
                            FtpStatusCode status = response.StatusCode;
                            response.Close();
                        }
                        catch
                        { }
                    }
                }
            }
            return FtpStatusCode.Undefined;
        }

        public byte[] DownloadFile(string filePath)
        {
            filePath = ClearnPath(filePath);

            string remoteUri = Path.Combine(ftpRootPath, filePath);
            var request = (FtpWebRequest)WebRequest.Create(remoteUri);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = ftpCredential;
            request.Proxy = ftpProxy;

            var response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            byte[] fileData = ReadFully(responseStream);

            response.Close();
            return fileData;
        }

        public void DeleteFile(string filePath)
        {
            filePath = ClearnPath(filePath);
            string remoteUri = Path.Combine(ftpRootPath, filePath);
            var request = (WebRequest)WebRequest.Create(remoteUri);
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = ftpCredential;
            request.Proxy = ftpProxy;
            request.GetResponse();
            //FtpStatusCode status = response.StatusCode;
            //response.Close();
            //return status;
        }

        public bool IsExistFolder(string folderPath)
        {
            bool isExist = true;
            try
            {
                folderPath = ClearnPath(folderPath);
                string remoteUri = Path.Combine(ftpRootPath, folderPath);
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(remoteUri);
                request.Credentials = ftpCredential;
                request.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    var response = (FtpWebResponse)ex.Response;
                    if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        isExist = false;
                    }
                }
            }
            return isExist;
        }

        /// <summary>
        /// Làm sạch đường dẫn của file hoặc folder truyền vào
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string ClearnPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }
            path = path.Replace(@"//", @"/").Replace(@"\\", @"/").Replace(@"\", @"/");
            if (path.EndsWith(@"\") || path.EndsWith(@"/"))
            {
                path = path.Substring(0, path.Length - 1);
            }
            if (path.StartsWith("/"))
                path = path.Substring(1, path.Length - 1);
            return path;
        }

        /// <summary>
        /// Lấy ftp folder của filePath truyền vào
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private string GetFolderPath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return "";
            }
            List<string> pathArray = filePath.Split('/').ToList();
            if (pathArray.Count >= 2)
            {
                pathArray.RemoveAt(pathArray.Count - 1);
                return string.Join(@"/", pathArray.ToArray());
            }
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static byte[] ReadFully(Stream input)
        {
            if (input == null)
            {
                return new byte[0];
            }
            if (input.CanSeek == true && input.Position > 0)
            {
                input.Position = 0;
            }
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
