using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Collections.ObjectModel;

namespace TodoAzurePcl
{
	public static class App
	{
		private static Entry newItem;
		private static Button addItemButton;
		private static ListView itemsListView;
		private static ObservableCollection<TodoItem> itemList = new ObservableCollection<TodoItem>();
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
				var item = new TodoItem() {Text = newItem.Text, Complete = false};
				itemList.Add(item);
				App.todoItemManager.SaveTaskAsync (item);
				newItem.Text = "";
			};

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
					foreach (var item in items.OrderBy(p => p.Text)) {
						itemList.Add (item);
					}

					itemCountLabel.Text = itemList.Count.ToString();
					activityIndicator.IsRunning = false;
				} catch (Exception ex) {
					itemCountLabel.Text = ex.ToString();
				}
			};

			itemsListView.ItemTemplate = new DataTemplate(typeof(ItemCell));
			itemsListView.ItemsSource = itemList;

			var page = new ContentPage {
				Content = new StackLayout()
				{
					Children =
					{
						new StackLayout()
						{
							Children = {newItem, addItemButton},
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
			foreach (var item in items.OrderBy(p => p.Text)) {
				itemList.Add (item);
			}
			itemCountLabel.Text = itemList.Count.ToString();
			activityIndicator.IsRunning = false;
			TodoItem.SetSaveMode (true);
		}

		public static void HandleToggled (object sender, ToggledEventArgs e)
		{

		}

		#region Azure stuff
		public static TodoItemManager todoItemManager;

		public static TodoItemManager TodoManager {
			get { return todoItemManager; }
			set { todoItemManager = value; }
		}

		public static void SetTodoItemManager (TodoItemManager todoItemManager)
		{
			TodoManager = todoItemManager;
		}
		#endregion
	}
}

