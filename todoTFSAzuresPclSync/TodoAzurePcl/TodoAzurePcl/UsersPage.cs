using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TodoAzurePcl
{
    public class UsersPage : ContentPage
    {
        public UsersPage()
        {
            ListView users = new ListView();
            users.ItemTemplate = new DataTemplate(typeof(TextCell));
            users.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");
            users.ItemTemplate.SetBinding(TextCell.DetailProperty, "PhoneNum");
            users.ItemsSource = App.MyContacts;

			ActivityIndicator activityIndicator = new ActivityIndicator {
				Color = Device.OnPlatform (Color.Black, Color.Default, Color.Default),
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center
			};
			activityIndicator.BindingContext = App.appBindings;
            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "LoadingContacts", BindingMode.TwoWay);
            activityIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, "LoadingContacts", BindingMode.TwoWay);

			Button loadContacts = new Button{ Text = "Load Contacts", HorizontalOptions = LayoutOptions.FillAndExpand };
			loadContacts.Clicked += async (object sender, EventArgs e) => {
				await App.LoadContacts (true);
			};

			this.Title = "Contacts";
			this.Content = new StackLayout () {
				Orientation = StackOrientation.Vertical,
				Children = {
					new StackLayout{Orientation = StackOrientation.Horizontal, Children = {	loadContacts, activityIndicator}},
					users
				},
			};
        }

        protected override async void OnAppearing()
        {
			base.OnAppearing();
            if (App.appBindings.LoadingContacts)
            {
                await App.LoadContacts(false);
            }
        }
    }

}
