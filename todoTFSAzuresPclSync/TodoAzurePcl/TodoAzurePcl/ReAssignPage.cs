using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace TodoAzurePcl
{
    public class ReAssignPage : ContentPage
    {
        public ReAssignPage(string taskId)
        {
            ListView users = new ListView();
            users.ItemTemplate = new DataTemplate(typeof(TextCell));
            users.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");
            users.ItemTemplate.SetBinding(TextCell.DetailProperty, "PhoneNum");
            users.ItemsSource = App.MyContacts;
            users.ItemSelected += (sender, e) =>
                {
                    if (e.SelectedItem != null)
                    {
                        var reassignTo = (Contact)e.SelectedItem;
                        MessagingCenter.Send<ReAssignPage, Contact>(this, Constants.ReassignTag + taskId, reassignTo);
                        users.SelectedItem = null;
                        this.Navigation.PopAsync();
                    }
                };

            
            
            //ActivityIndicator activityIndicator = new ActivityIndicator{
            //    Color = Device.OnPlatform(Color.Black, Color.Default, Color.Default),
            //    IsRunning = true,
            //    VerticalOptions = LayoutOptions.Center};

            this.Title = "Re-assign To:";
            this.Content = new StackLayout() { Children = { users }, Orientation = StackOrientation.Vertical };

        }
    }
}
