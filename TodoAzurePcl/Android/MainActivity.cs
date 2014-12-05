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
using System.IO;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace TodoAzurePcl.Android
{
	[Activity (Label = "TodoAzurePcl.Android.Android", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : XFormsApplicationDroid //AndroidActivity
	{
		MobileServiceClient Client;
		IMobileServiceSyncTable<TodoItem> todoTable;
		//TodoItemManager todoItemManager;

		protected override async void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);

			#region Azure stuff
			CurrentPlatform.Init ();
			Client = new MobileServiceClient (
				Constants.Url, 
				Constants.Key);

			#region Azure Sync stuff
			// http://azure.microsoft.com/en-us/documentation/articles/mobile-services-xamarin-android-get-started-offline-data/
			// new code to initialize the SQLite store
			string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "test4.db");

			if (!File.Exists(path))
			{
				File.Create(path).Dispose();
			}

			var store = new MobileServiceSQLiteStore(path);
			store.DefineTable<TodoItem>();
			store.DefineTable<User>();

			Client.SyncContext.InitializeAsync(store).Wait();
			#endregion

			todoTable = Client.GetSyncTable<TodoItem>(); 
			var userTable = Client.GetSyncTable<User>();
			//todoItemManager = new TodoItemManager(Client, todoTable);

			//App.SetTodoItemManager (todoItemManager);
			App.SetTodoItemManager(new SyncRepository<TodoItem>(Client, todoTable));
			UserPage.SetTodoItemManager(new SyncRepository<User>(Client, userTable));

			#endregion region

			//var page = await App.GetMainPage();
			var page = await UserPage.GetMainPage();
			SetPage(page);
			//App.LoadItems ();
			UserPage.LoadItems();
		}
	}
}

