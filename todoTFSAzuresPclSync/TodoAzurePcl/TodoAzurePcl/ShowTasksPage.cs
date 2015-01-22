using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TodoAzurePcl
{
    public class ShowTasksPage : ContentPage
    {
        private ListView tasks = new ListView();

        public ShowTasksPage()
        {
            
            tasks.ItemTemplate = new DataTemplate(typeof(TextCell));
            tasks.ItemTemplate.SetBinding(TextCell.TextProperty, "TaskDescription");
            tasks.ItemTemplate.SetBinding(TextCell.DetailProperty, "AssignedTo");
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

            ActivityIndicator activityIndicator = new ActivityIndicator
            {
                Color = Device.OnPlatform(Color.Black, Color.Default, Color.Default),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            activityIndicator.BindingContext = App.appBindings;
            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "LoadingTasks", BindingMode.TwoWay);
            activityIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, "LoadingTasks", BindingMode.TwoWay);

            Button loadContacts = new Button { Text = "Load Tasks", HorizontalOptions = LayoutOptions.FillAndExpand };
            loadContacts.Clicked += async (object sender, EventArgs e) =>
            {
                await App.LoadTasks(true);
            };

            this.Title = "Tasks List";
            this.Content = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Children = { 
                    new StackLayout{Orientation = StackOrientation.Horizontal, Children = {loadContacts, activityIndicator}}, 
                    tasks },
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
			if(App.appBindings.LoadingTasks)
			{
				await App.LoadTasks(false);
			}
        }
    }
}
