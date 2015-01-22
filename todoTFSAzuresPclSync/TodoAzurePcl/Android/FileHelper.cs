using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(TodoAzurePcl.Android.FileHelper))]
namespace TodoAzurePcl.Android
{
    class FileHelper : IFileHelper
    {
        public bool Exists(string filename)
        {
            return File.Exists(GetFilePath(filename));
        }

        public void WriteAllText(string filename, string text)
        {
            File.WriteAllText(GetFilePath(filename), text);
        }

        public string ReadAllText(string filename)
        {
            return File.ReadAllText(GetFilePath(filename));
        }

        string GetFilePath(string filename)
        {
            string docsPath = Environment.GetFolderPath(
                                Environment.SpecialFolder.Personal);
            return Path.Combine(docsPath, filename);
        }
    }
}