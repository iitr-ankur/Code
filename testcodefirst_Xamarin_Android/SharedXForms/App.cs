using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
//using Xamarin.Forms.Labs.Controls;

namespace testcodefirst
{
    class ItemCell : ViewCell
    {
        public ItemCell()
        {

            var itemLabel = new Label()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            itemLabel.SetBinding(Label.TextProperty, "Text");

            var doneSwitch = new Switch();
            doneSwitch.HorizontalOptions = LayoutOptions.End;
            doneSwitch.SetBinding(Switch.IsToggledProperty, "Complete");



            //var itemCheckBox = new CheckBox();
            //itemCheckBox.SetBinding(CheckBox.DefaultTextProperty, "Text");
            //itemCheckBox.SetBinding(CheckBox.CheckedProperty, "Complete");


            //itemCheckBox.DefaultText = item.Text;
            //itemCheckBox.Checked = item.Complete;

            //itemCheckBox.SetBinding(itemCheckBox.)
            //var image = new Image
            //            {
            //                HorizontalOptions = LayoutOptions.Start
            //            };
            //image.SetBinding(Image.SourceProperty, new Binding("ImageUri"));
            //image.WidthRequest = image.HeightRequest = 40;

            //var nameLayout = CreateNameLayout();

            var viewLayout = new StackLayout()
                             {
                                 Orientation = StackOrientation.Horizontal,
                                 Children = { itemLabel, doneSwitch }
                             };
            View = viewLayout;
        }
    }


	public class App
	{
        private Entry newItem;
        private Button addItemButton;
        private ListView itemsListView;
        private List<ToDoItem> itemList;

        //Mobile Service Client reference
        private MobileServiceClient client;

        //Mobile Service Table used to access data
        private IMobileServiceTable<ToDoItem> toDoTable;

        const string applicationURL = @"https://testcodefirst.azure-mobile.net/";
        const string applicationKey = @"CaoUwmPaptqxVpSmNGvNZXfPyvxyOt60";

        public App()
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
            itemsListView = new ListView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            itemList = new List<ToDoItem>();
            
            CurrentPlatform.Init();
            // Create the Mobile Service Client instance, using the provided
            // Mobile Service URL and key
            client = new MobileServiceClient(
                applicationURL,
                applicationKey);

            // Get the Mobile Service Table instance to use
            toDoTable = client.GetTable<ToDoItem>();
            
        }

		public Page GetMainPage ()
		{
            itemsListView.ItemTemplate = new DataTemplate(typeof(ItemCell));
            itemsListView.ItemsSource = new List<ToDoItem>() {
                new ToDoItem(){Text = "item 1", Complete = true},
                new ToDoItem(){Text = "item 2",Complete = false},
                new ToDoItem(){Text = "item 3", Complete = false}
            };
                //itemList; //new List<string>() { "item 1", "item2", "item3" };

			return new ContentPage {
                //Content = new Label {
                //    Text = "Hello, Forms!",
                //    VerticalOptions = LayoutOptions.CenterAndExpand,
                //    HorizontalOptions = LayoutOptions.CenterAndExpand,
                //},
                Content = new StackLayout()
                {
                    Children =
                    {
                        new StackLayout()
                        {
                            Children = {newItem, addItemButton},
                            Orientation = StackOrientation.Horizontal
                        },
                        itemsListView
                    },
                    Orientation = StackOrientation.Vertical
                }
			};
		}

        async public Task RefreshItemsFromTableAsync()
        {
            try
            {
                // Get the items that weren't marked as completed and add them in the
                // adapter
				var list = await toDoTable.Where (item => item.Complete == false).ToListAsync();
                foreach (ToDoItem current in list)
                    itemList.Add(current);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
	}
}

