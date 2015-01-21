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

//using Xamarin.Forms.Labs;
//using Xamarin.Forms.Labs.Droid;
using System.IO;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Android.Util;

namespace TodoAzurePcl.Android
{
	[Activity (Label = "TodoTFS", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : AndroidActivity //XFormsApplicationDroid
	{
		MobileServiceClient _client;
		IMobileServiceSyncTable<Contact> _contacts;
        IMobileServiceSyncTable<TodoTask> _tasks;
		//TodoItemManager todoItemManager;

		protected override async void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);

			#region Azure stuff
			CurrentPlatform.Init ();
			_client = new MobileServiceClient (Constants.Url, Constants.Key);

			// http://azure.microsoft.com/en-us/documentation/articles/mobile-services-xamarin-android-get-started-offline-data/
			// new code to initialize the SQLite store
			string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "todoTFS.db");

			if (!File.Exists(path))
			{
				File.Create(path).Dispose();
			}

			var store = new MobileServiceSQLiteStore(path);
			store.DefineTable<Contact>();
			store.DefineTable<TodoTask>();

			_client.SyncContext.InitializeAsync(store).Wait();

            _contacts = _client.GetSyncTable<Contact>();
            _tasks = _client.GetSyncTable<TodoTask>();

            App.SetContactsRepo(new SyncRepository<Contact>(_client, _contacts));
            App.SetTasksRepo(new SyncRepository<TodoTask>(_client, _tasks));
            #endregion

            SetPage(App.GetMainPage());
			
            await App.LoadContacts(false);
			await App.LoadTasks (false);
		}

	}
}

