using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace TodoAzurePcl
{
    public class ShowTasksPage : ContentPage
    {
        public ShowTasksPage()
        {
            ListView tasks = new ListView();
            tasks.ItemTemplate = new DataTemplate(typeof(TextCell));
            tasks.ItemTemplate.SetBinding(TextCell.TextProperty, "TaskDescription");
            tasks.ItemTemplate.SetBinding(TextCell.DetailProperty, "AssignedToId");
            tasks.ItemsSource = App.Tasks;
            tasks.ItemSelected += async (sender, e) =>
            {
                if(e.SelectedItem != null)
                {
                    var selectedTask = (TodoTask)e.SelectedItem;
                    tasks.SelectedItem = null;
                    await this.Navigation.PushAsync(new CreateTaskPage(selectedTask));
                }
            };

            //ActivityIndicator activityIndicator = new ActivityIndicator{
            //    Color = Device.OnPlatform(Color.Black, Color.Default, Color.Default),
            //    IsRunning = true,
            //    VerticalOptions = LayoutOptions.Center};

            this.Title = "Tasks List";
            this.Content = new StackLayout() { Children = { tasks }, Orientation = StackOrientation.Vertical };
        }
    }
}
