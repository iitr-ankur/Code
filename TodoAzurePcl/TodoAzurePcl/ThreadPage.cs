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
	public class ThreadPage : ContentPage
	{
		private string _threadId;
		private string _title;
		private ObservableCollection<Message> _messageList = new ObservableCollection<Message> ();
		public static MessageRepository messageRepo{ get; set; }


		public ThreadPage(string threadId, string title)
		{
			_title = title;
			_threadId = threadId;
			var titleLabel = new Label () {
				Text = title, 
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Font = Font.SystemFontOfSize(NamedSize.Large)
			};

			var messageList = new ListView ();
			messageList.ItemTemplate = new DataTemplate (typeof(TextCell));
			messageList.ItemTemplate.SetBinding (TextCell.TextProperty, "Text");
			messageList.ItemsSource = _messageList;
			messageList.VerticalOptions = LayoutOptions.FillAndExpand;

			Entry _newMessage = new Entry(){HorizontalOptions = LayoutOptions.FillAndExpand, Placeholder = "Enter Message here"};

			var sendButton = new Button{ Text = "Send", HorizontalOptions = LayoutOptions.Fill };
			sendButton.Clicked += (sender, e) => {
				var newMessage = new Message {
					Text = _newMessage.Text,
					ThreadId = _threadId,
					UserId = "12345"
				};
				// Update UI
				_messageList.Add (newMessage);
				_newMessage.Text = "";
				// Update Db
				messageRepo.SaveTaskAsync(newMessage);

			};

			this.Content = new StackLayout ()
			{
					Children = {
						titleLabel, 
						messageList,
						new StackLayout () {
							Children = { _newMessage, sendButton },
							Orientation = StackOrientation.Horizontal
						}
					},
					VerticalOptions = LayoutOptions.FillAndExpand
			};
			Title = "Thread:" + title;
		}

		public async Task UpdateMessages(string threadId){
			var messages = await messageRepo.GetTasksAsync(threadId);
			foreach (var item in messages.OrderByDescending(p => p.__createdAt)) {
				_messageList.Add (item);
			}
		}

		public void ClearMessages(){
			_messageList = new ObservableCollection<Message> ();
		}

		protected override void OnDisappearing ()
		{
			ClearMessages ();
			base.OnDisappearing ();
		}

		protected async override void OnAppearing ()
		{
			base.OnAppearing ();
			UpdateMessages (_threadId);
		}
	}
}

