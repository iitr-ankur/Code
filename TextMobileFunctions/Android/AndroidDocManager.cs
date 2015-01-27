using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using System.IO;
using System.Net;

[assembly: Xamarin.Forms.Dependency(typeof(TextMobileFunctions.Android.AndroidDocManager))]
namespace TextMobileFunctions.Android
{
    public class AndroidDocManager : IDocumentManager
    {
        /// <summary>
        /// Open the downloaded file with an application picker
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<bool> OpenDocument(string fileName)
        {
            string docsPath = System.Environment.GetFolderPath(
                                System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(docsPath, fileName);
            var result = new TaskCompletionSource<bool>();

            if (!File.Exists(filePath))
            {
                var fileEx = new FileNotFoundException("File to open not found", filePath);
                result.SetException(fileEx);
                //return
            }

            try
            {
                var targetFile = new Java.IO.File(filePath);
                var fileExtension = Path.GetExtension(filePath);

                var targetUri = global::Android.Net.Uri.FromFile(targetFile);

                var intent = new Intent(Intent.ActionView);

                // Source: http://www.androidsnippets.com/open-any-type-of-file-with-default-intent
                //         http://forums.xamarin.com/discussion/comment/91923#Comment_91923
                var mimeType = "application/*";
                switch (fileExtension)
                {
                    case ".pdf":
                        mimeType = "application/pdf";
                        break;

                    case ".doc":
                    case ".docx":
                        mimeType = "application/msword";
                        break;

                    case ".txt":
                        mimeType = "text/plain";
                        break;

                    case ".ppt":
                    case ".pptx":
                        mimeType = "application/vnd.ms-powerpoint";
                        break;

                    case ".xls":
                    case ".xlsx":
                        mimeType = "application/vnd.ms-excel";
                        break;

                    case ".zip":
                    case ".rar":
                        mimeType = "application/zip";
                        break;

                    case ".rtf":
                        mimeType = "application/rtf";
                        break;

                    case ".wav":
                    case ".mp3":
                        mimeType = "audio/x-wav";
                        break;

                    case ".gif":
                    mimeType = "image/gif";
                    break;

                    case ".jpg":
                    case ".jpeg":
                    case ".png":
                        mimeType = "image/jpeg";
                        break;

                    case ".3gp":
                    case ".mpg":
                    case ".mpeg":
                    case ".mpe":
                    case ".mp4":
                    case ".avi":
                        mimeType = "video/*";
                        break;
                }

                intent.SetDataAndType(targetUri, mimeType);

                intent.SetFlags(ActivityFlags.NewTask);

                var context = global::Xamarin.Forms.Forms.Context;

                context.StartActivity(Intent.CreateChooser(intent, "Choose an Application:"));

                result.SetResult(true);
            }
            catch (Exception e)
            {
                result.SetException(e);
            }

            //Wait for the activity
            var tResult = await result.Task;

            return tResult;

        }

    }
}