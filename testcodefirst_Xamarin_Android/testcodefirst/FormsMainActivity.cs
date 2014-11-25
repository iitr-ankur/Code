using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;
//using Xamarin.Forms.Labs.Droid;
using System.Threading.Tasks;

namespace testcodefirst
{

    [Activity(Label = "SharedXForms", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class FormsMainActivity : AndroidActivity
    {
		protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);

            var appObj = new App();
            try
            {
				await appObj.RefreshItemsFromTableAsync();

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            try
            {
                SetPage(appObj.GetMainPage());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}