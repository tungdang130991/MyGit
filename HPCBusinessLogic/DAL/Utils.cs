using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.Configuration;

namespace HPCBusinessLogic
{
    public static class Utils
    {
        private static int _maxFileSize;

        public static string ResolveApplicationUrl(string applicationPath, string url)
        {
            if (url.StartsWith("~/"))
            {
                url = applicationPath + url.Substring(1);
            }

            return url;
        }

        /// <summary>
        /// Execute action on first recursively founded or do nothing if control not found
        /// </summary>
        /// <typeparam name="T">Control type</typeparam>
        /// <param name="control">Parent control</param>
        /// <param name="action">Action to execute</param>
        /// <returns>Returns true if control was found and action executed, otherwise returns false.</returns>
        public static bool ForFirst<T>(Control control, Action<T> action) where T : Control
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (control is T)
            {
                action((T)control);
                return true;
            }
            else if (control.Controls != null && control.Controls.Count > 0)
            {
                foreach (Control c in control.Controls)
                {
                    if (ForFirst(c, action))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static string GetSafeFileName(string directory, string name)
        {
            if (string.IsNullOrEmpty(directory))
            {
                throw new ArgumentNullException("directory");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            // Replace special characters
            StringBuilder sb = new StringBuilder(name);
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                sb.Replace(c, '_');
            }
            name = sb.ToString();

            // Rename duplicates
            if (File.Exists(Path.Combine(directory, name)))
            {
                int i = 1;
                string baseName = Path.GetFileNameWithoutExtension(name);
                string ext = Path.GetExtension(name);
                while (File.Exists(Path.Combine(directory, baseName + "_" + i.ToString() + ext)))
                {
                    i++;
                }
                return baseName + "_" + i.ToString() + ext;
            }
            else
            {
                return name;
            }
        }

        public static void ValidateFileExtension(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            name = name.ToLowerInvariant();

            List<string> allowedExtensionsList = new List<string>();
            allowedExtensionsList.AddRange(WebConfigurationManager.AppSettings["AllowedFileExtension"].ToLowerInvariant().Replace("*", "").Split(';'));
            string ext = Path.GetExtension(name);
            if (ext != null && allowedExtensionsList.Contains(ext))
            {
                return;
            }

            // Return 403 Forbidden status code
            string errorMessage = string.Format("File \"{0}\" can not be uploaded. \"{1}\" files are not allowed.", name, System.IO.Path.GetExtension(name));
            if (HttpContext.Current != null && HttpContext.Current.Response != null)
            {
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.StatusCode = 403;
                response.Write(errorMessage);
                response.End(); // Abort request
            }
            else
            {
                throw new ApplicationException(errorMessage);
            };
        }

        internal static void ValidateFileSize(HttpPostedFile file)
        {
            if (_maxFileSize == 0)
            {
                if (!int.TryParse(WebConfigurationManager.AppSettings["MaxFileSize"], out _maxFileSize))
                {
                    _maxFileSize = 1024 * 1024 * 5; // 5 MB
                }
            }

            if (file.ContentLength > _maxFileSize)
            {
                string errorMessage = string.Format("File \"{0}\" can not be uploaded. File size is lager {1} bytes.", file.FileName, _maxFileSize);
                if (HttpContext.Current != null && HttpContext.Current.Response != null)
                {
                    HttpResponse response = HttpContext.Current.Response;
                    response.Clear();
                    response.StatusCode = 403;
                    response.Write(errorMessage);
                    response.End(); // Abort request
                }
                else
                {
                    throw new ApplicationException(errorMessage);
                };
            }
        }

        public static string LockedUser(string Nguoikhoa, int UserID)
        {
            string _userLock = "";
            int _prmEditorID = Convert.ToInt32(Nguoikhoa);
            if (_prmEditorID != 0)
            {
                if (_prmEditorID != UserID)
                    _userLock = "<br /><font style='color:red;font-size:13px;font-family:arial;font-weight:bold;font-style:italic'> " + (string)HttpContext.GetGlobalResourceObject("cms.language", "lblNguoixuly") + ": " + UltilFunc.GetUserFullName(int.Parse(Nguoikhoa)) + "</font> ";
            }
            return _userLock;
        }
        public static bool IsEnable(bool _Role, string Nguoikhoa, int UserID)
        {
            bool _isEnabled = _Role;
            int _prmEditorID = Convert.ToInt32(Nguoikhoa);
            if (_prmEditorID != 0)
            {
                if (_prmEditorID != UserID)
                    _isEnabled = false;

            }
            return _isEnabled;
        }

    }
}
