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

            //ActivityIndicator activityIndicator = new ActivityIndicator{
            //    Color = Device.OnPlatform(Color.Black, Color.Default, Color.Default),
            //    IsRunning = true,
            //    VerticalOptions = LayoutOptions.Center};
            
            this.Title = "Contacts";
            this.Content = new StackLayout() { Children = { users }, Orientation = StackOrientation.Vertical };
        }
    }

}
