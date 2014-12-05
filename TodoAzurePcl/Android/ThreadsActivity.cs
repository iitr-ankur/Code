
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
using Android.Content.PM;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.IO;
using TodoAzurePcl.TodoAzurePcl;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

namespace TodoAzurePcl.Android
{
	[Activity (Label = "ThreadsActivity", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]			
	public class ThreadsActivity : AndroidActivity
	{
		MobileServiceClient Client;
		IMobileServiceSyncTable<Thread> threadsSyncTable;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here

			Xamarin.Forms.Forms.Init (this, bundle);

			#region Azure stuff
			CurrentPlatform.Init ();
			Client = new MobileServiceClient (
				Constants.Url, 
				Constants.Key);

			#region Azure Sync stuff
			// http://azure.microsoft.com/en-us/documentation/articles/mobile-services-xamarin-android-get-started-offline-data/
			// new code to initialize the SQLite store
			string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "test11.db");

			if (!File.Exists(path))
			{
				File.Create(path).Dispose();
			}

			var store = new MobileServiceSQLiteStore(path);
			store.DefineTable<Thread>();
			store.DefineTable<Message>();

			Client.SyncContext.InitializeAsync(store).Wait();
			#endregion

			threadsSyncTable = Client.GetSyncTable<Thread>();
			ThreadsListPage.ThreadRepo = new ThreadRepository(Client, Client.GetSyncTable<Thread>(), "0");
			ThreadPage.messageRepo = new MessageRepository(Client, Client.GetSyncTable<Message>());
			#endregion region

			//var page = await App.GetMainPage();
			//var page = ThreadsListPage.GetThreadsListPage ("0", "Group1");
			SetPage(new NavigationPage(new ThreadsListPage("0", "Group1")));
			//ThreadsListPage.GetAllThreads ();
		}
	}
}

