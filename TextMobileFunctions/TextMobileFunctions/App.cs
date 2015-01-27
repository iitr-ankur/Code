using System;
using Xamarin.Forms;

namespace TextMobileFunctions
{
	public class App
	{
		public static Page GetMainPage ()
		{
			var mobileAccess = DependencyService.Get<IAccessMobile> ();

			var getContactsbutton = new Button (){ Text = "GetContacts", HorizontalOptions = LayoutOptions.FillAndExpand };
			var label = new Label (){ Text = "Initial Contacts", HorizontalOptions = LayoutOptions.FillAndExpand };
			getContactsbutton.Clicked += (sender, e) => {
				label.Text = "Contacts: " + mobileAccess.getcontacts().Result;
			};

			var photoButton = new Button () {
				Text = "Take Picture", HorizontalOptions = LayoutOptions.FillAndExpand
			};
			photoButton.Clicked += (sender, e) => {
				mobileAccess.TakePhoto ();
			};

			var pickPhotoButton = new Button () {
				Text = "Pick Picture", HorizontalOptions = LayoutOptions.FillAndExpand
			};
			pickPhotoButton.Clicked += (sender, e) => {
				mobileAccess.PickPhoto ();
			};

			var takeVideoButton = new Button (){ Text = "Take Video", HorizontalOptions = LayoutOptions.FillAndExpand };
			takeVideoButton.Clicked += (sender, e) => mobileAccess.TakeVideo();

			var pickVideoButton = new Button (){ Text = "Pick Video", HorizontalOptions = LayoutOptions.FillAndExpand };
			pickVideoButton.Clicked += (sender, e) => mobileAccess.PickVideo();

            var openDocButton = new Button { Text = "Open Doc File" };
            openDocButton.Clicked += async (sender, e) => {
                var docManager = DependencyService.Get<IDocumentManager>();
                await docManager.OpenDocument("TestDoc.doc");
            };

            var openDocxButton = new Button { Text = "Open Docx File" };
            openDocxButton.Clicked += async (sender, e) =>
            {
                var docManager = DependencyService.Get<IDocumentManager>();
                await docManager.OpenDocument("TestDoc.docx");
            };

            var openPdfButton = new Button { Text = "Open Doc File" };
            openPdfButton.Clicked += async (sender, e) =>
            {
                var docManager = DependencyService.Get<IDocumentManager>();
                await docManager.OpenDocument("Intro_to_Xamarin.Forms.pdf");
            };

			return new ContentPage { 
				Content = new StackLayout () {
                    Children = { getContactsbutton, label, photoButton, pickPhotoButton, takeVideoButton, pickVideoButton, openDocButton, openDocxButton, openPdfButton },
					Orientation = StackOrientation.Vertical
				}
			};
		}
	}
}

