using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Collections.ObjectModel;
using TodoAzurePcl.TodoAzurePcl;

namespace TodoAzurePcl
{
	public class ThreadsListPage : ContentPage
	{
		private ObservableCollection<Thread> _threadsList = new ObservableCollection<Thread> ();
		public static ThreadRepository ThreadRepo { get; set; }
		private ActivityIndicator _refreshIndicatior = new ActivityIndicator (){ IsRunning = false, IsVisible = false };
		private ListView listView = new ListView ();

		public ThreadsListPage (string groupId, string groupName)
		{
			Entry newthreadEntry = new Entry () {
				Placeholder = "New Thread Title",
				HorizontalOptions = LayoutOptions.FillAndExpand
			};

			var addThreadButton = new Button () {
				Text = "+",
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill
			};
			addThreadButton.Clicked += (object sender, EventArgs e) => {
				var threadItem = new Thread { Title = newthreadEntry.Text, GroupId = groupId, GroupName = groupName };
				newthreadEntry.Text = "";
				_threadsList.Add(threadItem);
				ThreadRepo.SaveTaskAsync (threadItem);
			};

			var newThreadStack = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				Children = {
					newthreadEntry,
					addThreadButton
				}
			};

			listView.ItemTemplate = new DataTemplate (typeof(TextCell));
			listView.ItemTemplate.SetBinding (TextCell.TextProperty, "Title");
			listView.ItemsSource = _threadsList;
			listView.VerticalOptions = LayoutOptions.FillAndExpand;

			Title = "GroupId:" + groupId;
			Content = new StackLayout {
				Children = { newThreadStack, _refreshIndicatior, listView },
				VerticalOptions = LayoutOptions.FillAndExpand
			};


			listView.ItemSelected += async (object sender, SelectedItemChangedEventArgs e) => {
				try {
					if (e.SelectedItem != null) {
						var thread = (Thread)e.SelectedItem;
						listView.SelectedItem = null;
						this.Navigation.PushAsync(new ThreadPage(thread.Id, thread.Title ));
					}
				} catch (Exception ex) {
					this.DisplayAlert("error", ex.ToString(), "Cancel button");
				}
			};

			// Create and initialize ToolbarItem.
			ToolbarItem purgeOld = new ToolbarItem
			{
				Name = "Purge",
				Icon = Device.OnPlatform("new.png",
					"ic_action_discard.png",
					"Images/add.png"),
				Order = ToolbarItemOrder.Primary
			};
			purgeOld.Activated += async (sender, args) =>
			{
				bool purge = await this.DisplayAlert("Purge", "Clear Old Items?", "Yes", "No");
				if (purge) 
				{
					await ThreadRepo.PurgeAsync();
					this.DisplayAlert("Purge Status", "Done Purging", "Ok");
				}
			};
			this.ToolbarItems.Add(purgeOld);

			// Create and initialize ToolbarItem.
			ToolbarItem refreshItems = new ToolbarItem
			{
				Name = "Refresh",
				Icon = Device.OnPlatform("new.png",
					"ic_action_new.png",
					"Images/add.png"),
				Order = ToolbarItemOrder.Primary
			};
			refreshItems.Activated += async (sender, args) =>
			{
				try {
					_refreshIndicatior.IsRunning = true;
					_refreshIndicatior.IsVisible = true;
					await ThreadRepo.SyncAsync();
					await GetAllThreads();
				} catch (Exception ex) {
					this.DisplayAlert("Error", "Exception: " + ex.ToString(), "Ok");
				}
			};
			this.ToolbarItems.Add(refreshItems);
		}

		public async Task GetAllThreads(){
			_refreshIndicatior.IsRunning = true;
			_refreshIndicatior.IsVisible = true;
			var list = await ThreadRepo.GetTasksAsync();
			_threadsList = new ObservableCollection<Thread> ();
			listView.ItemsSource = _threadsList;
			foreach(var threadItem in list.Where(p => p.__createdAt != null).OrderByDescending(p => p.__createdAt)){
				_threadsList.Add (threadItem);
			}
			_refreshIndicatior.IsRunning = false;
			_refreshIndicatior.IsVisible = false;
		}

		protected async override void OnAppearing ()
		{
			await GetAllThreads ();
			base.OnAppearing ();
		}
	}

}

