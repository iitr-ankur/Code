using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;
using Xamarin.Contacts;
using System.Threading.Tasks;
using Xamarin.Media;
using Android.Telephony;

namespace TextMobileFunctions.Android
{
	[Activity (Label = "TestMobileFunctions.Android.Android", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : AndroidActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);

			AccessMobile.currentActivity = this;

			SetPage (App.GetMainPage ());
		}

		protected override async void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			// User canceled
			if (resultCode == Result.Canceled)
				return;


			MediaFile file = await data.GetMediaFileExtraAsync (this);
//			file.
//			Console.WriteLine (file.Path);

			System.Diagnostics.Debug.WriteLine ("File path: {0}", file.Path);

		}
	}
}

