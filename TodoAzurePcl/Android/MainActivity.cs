using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;
using Microsoft.WindowsAzure.MobileServices;

using Xamarin.Forms.Labs;
using Xamarin.Forms.Labs.Droid;

namespace TodoAzurePcl.Android
{
	[Activity (Label = "TodoAzurePcl.Android.Android", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : XFormsApplicationDroid //AndroidActivity
	{
		MobileServiceClient Client;
		IMobileServiceTable<TodoItem> todoTable;
		TodoItemManager todoItemManager;

		protected override async void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);

			#region Azure stuff
			CurrentPlatform.Init ();
			Client = new MobileServiceClient (
				Constants.Url, 
				Constants.Key);		
			todoTable = Client.GetTable<TodoItem>(); 
			todoItemManager = new TodoItemManager(todoTable);

			App.SetTodoItemManager (todoItemManager);
			#endregion region

			var page = await App.GetMainPage();
			SetPage(page);
			App.LoadItems ();
		}
	}
}

