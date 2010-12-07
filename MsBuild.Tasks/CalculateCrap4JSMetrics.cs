using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace MSBuild.Tasks
{
    public class CalculateCrap4JsMetrics : Task
    {
        private readonly Uri _jsmeterJSON = new Uri("http://jsmeter.info/jsmeter?mode=JSON");
        private readonly Uri _jsmeterTable = new Uri("http://jsmeter.info/jsmeter?mode=TABLE");

        [Required]
        public ITaskItem[] Files { get; set; }

        public override bool Execute()
        {
            foreach (var filePath in Files.Select(file => file.GetMetadata("FullPath")))
            {
                File.WriteAllText(filePath + ".complexity.json", POSTFileContentsTo(filePath, _jsmeterJSON));
                File.WriteAllText(filePath + ".complexity.table", POSTFileContentsTo(filePath, _jsmeterTable));
            }
            return true;
        }

        private string POSTFileContentsTo(string filePath, Uri uri)
        {
            var jsContent = File.ReadAllText(filePath);

            Log.LogMessage(string.Format("POST {1} data: contents of '{0}'", filePath, uri));
            return DoPost(uri, jsContent);
        }

        private string DoPost(Uri uri, string content)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = content.Length;
            using (Stream writeStream = request.GetRequestStream())
            {
                var encoding = new UTF8Encoding();
                byte[] bytes = encoding.GetBytes(content);
                writeStream.Write(bytes, 0, bytes.Length);
            }

            var result = string.Empty;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    using (var readStream = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        result = readStream.ReadToEnd();
                    }
                }
            }
            return result;
        }
    }
}