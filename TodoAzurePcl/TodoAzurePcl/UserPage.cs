using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Collections.ObjectModel;


namespace TodoAzurePcl
{
	public static class UserPage
	{
		private static Entry newItem;
		private static Button addItemButton;
		private static ListView itemsListView;
		private static ObservableCollection<User> itemList = new ObservableCollection<User>();
		private static ObservableCollection<User> itemList2 = new ObservableCollection<User>();
		private static Button loadButton;
		private static Label itemCountLabel;
		private static ActivityIndicator activityIndicator;

		public async static Task<Page> GetMainPage ()
		{
			newItem = new Entry()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Placeholder = "New Item"
			};
			addItemButton = new Button()
			{
				Text = "+",
				HorizontalOptions = LayoutOptions.Fill
			};

			addItemButton.Clicked += (sender, e) => {
				var item = new User() {UserName = newItem.Text};
				itemList.Add(item);
				UserPage.todoItemManager.SaveTaskAsync (item);
				newItem.Text = "";
			};

			var refreshButton = new Button () {
				Text = "refresh",
				HorizontalOptions = LayoutOptions.Fill
			};
			refreshButton.Clicked += (sender, e) => {RefreshList();};

			itemsListView = new ListView()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand
			};

			itemCountLabel = new Label (){ Text = "0" };

			activityIndicator = new ActivityIndicator
			{
				Color = Device.OnPlatform(Color.Black, Color.Default, Color.Default),
				IsRunning = false,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			loadButton = new Button (){ Text = "Load" };
			loadButton.Clicked += async (object sender, EventArgs e) => {
				try {
					activityIndicator.IsRunning = true;
					var items = await todoItemManager.GetTasksAsync();
					foreach (var item in items.OrderBy(p => p.UserName)) {
						itemList.Add (item);
					}

					itemCountLabel.Text = itemList.Count.ToString();
					activityIndicator.IsRunning = false;
				} catch (Exception ex) {
					itemCountLabel.Text = ex.ToString();
				}
			};

			//itemsListView.ItemTemplate = new DataTemplate(typeof(ItemCell));
			itemsListView.ItemTemplate = new DataTemplate(typeof(TextCell));
			itemsListView.ItemTemplate.SetBinding (TextCell.TextProperty, "UserName");
			itemsListView.ItemsSource = itemList;

			var page = new ContentPage {
				Content = new StackLayout()
				{
					Children =
					{
						new StackLayout()
						{
							Children = {newItem, addItemButton, refreshButton},
							Orientation = StackOrientation.Horizontal
						},
						new StackLayout()
						{
							Children = {loadButton, activityIndicator},
							Orientation = StackOrientation.Horizontal
						},
						itemCountLabel,
						itemsListView
					},
					Orientation = StackOrientation.Vertical
				}
			};
			return page;
		}

		public static async Task LoadItems() {
			activityIndicator.IsRunning = true;
			var items = await todoItemManager.GetTasksAsync ();
			foreach (var item in items.OrderBy(p => p.UserName)) {
				itemList.Add (item);
			}
			itemCountLabel.Text = itemList.Count.ToString();
			activityIndicator.IsRunning = false;
			User.SetSaveMode (true);
		}

		public static async Task RefreshList(){
			activityIndicator.IsRunning = true;
			var items = await todoItemManager.GetTasksAsync ();
			foreach (var item in items.OrderBy(p => p.UserName)) {
				itemList2.Add (item);
			}
			itemCountLabel.Text = itemList.Count.ToString();
			activityIndicator.IsRunning = false;
			itemsListView.ItemsSource = itemList2;
			User.SetSaveMode (true);
		}

		#region Azure stuff
		//		public static TodoItemManager todoItemManager;
		//
		//		public static TodoItemManager TodoManager {
		//			get { return todoItemManager; }
		//			set { todoItemManager = value; }
		//		}
		//
		//		public static void SetTodoItemManager (TodoItemManager todoItemManager)
		//		{
		//			TodoManager = todoItemManager;
		//		}

		public static SyncRepository<User> todoItemManager;
		public static SyncRepository<User> TodoManager {
			get { return todoItemManager; }
			set { todoItemManager = value; }
		}

		public static void SetTodoItemManager (SyncRepository<User> todoItemManager)
		{
			TodoManager = todoItemManager;
		}
		#endregion
	}
}

