using System;
using Xamarin.Forms;

namespace TodoAzurePcl
{
    public static class FileHelper
    {
        static IFileHelper fileHelper = DependencyService.Get<IFileHelper>();

        public static bool Exists(string filename)
        {
            return fileHelper.Exists(filename);
        }

        public static void WriteAllText(string filename, string text)
        {
            fileHelper.WriteAllText(filename, text);
        }

        public static string ReadAllText(string filename)
        {
            return fileHelper.ReadAllText(filename);
        }
    }
}
