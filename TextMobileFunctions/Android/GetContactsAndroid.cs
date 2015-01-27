using System;
using System.Collections.Generic;
using Xamarin.Contacts;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Java.Lang;
using System.Linq;
using Xamarin.Media;

[assembly: Dependency(typeof(TextMobileFunctions.Android.AccessMobile))]
namespace TextMobileFunctions.Android
{
	public class AccessMobile : IAccessMobile
	{
		public static MainActivity currentActivity;

		public async Task<string> getcontacts()
		{
			string contacts = string.Empty;

			var book = new AddressBook(currentActivity);
//			bool allowed = await book.RequestPermission ();
//			if (!allowed) {
//				Console.WriteLine ("Permission denied by user or manifest");
//				return "Permision denied";
//			}
			var contactIterator = book.GetEnumerator();
			while (contactIterator.MoveNext ()) {
				contacts += string.Format ("{0} {1} {2}|| ", contactIterator.Current.FirstName, contactIterator.Current.LastName, 
					contactIterator.Current.Phones.First().Number);
			}
			return contacts;
		}

		private bool IsCameraAvailable()
		{
			var picker = new MediaPicker (currentActivity);
			if (!picker.IsCameraAvailable) {
				Console.WriteLine ("No camera!");
				return false;
			}
			return true;

		}

		public void TakePhoto ()
		{
			if (IsCameraAvailable ()) {
				var picker = new MediaPicker (currentActivity);
				var intent = picker.GetTakePhotoUI (new StoreCameraMediaOptions {
					Name = "test.jpg",
					Directory = "MediaPickerSample"
				});
				currentActivity.StartActivityForResult(intent, (int)MediaAction.TakePhoto);
			}
		}

		public void PickPhoto()
		{
			if (IsCameraAvailable ()) {
				var picker = new MediaPicker (currentActivity);
				var intent = picker.GetPickPhotoUI ();
				currentActivity.StartActivityForResult(intent, (int)MediaAction.PickPhoto);
			}
		}

		public void TakeVideo()
		{
			if (IsCameraAvailable ()) {
				var picker = new MediaPicker (currentActivity);
				var videoOptions = new StoreVideoOptions ();
				videoOptions.Directory = "testVideo";
				videoOptions.Name = "videoTestFile";
				videoOptions.Quality = VideoQuality.Low;
				var intent = picker.GetTakeVideoUI (videoOptions);
				currentActivity.StartActivityForResult(intent, (int)MediaAction.TakeVideo);
			}
		}

		public void PickVideo()
		{
			if (IsCameraAvailable ()) {
				var picker = new MediaPicker (currentActivity);
				var intent = picker.GetPickVideoUI ();
				currentActivity.StartActivityForResult(intent, (int)MediaAction.PickVideo);
			}
		}
	}
}

