using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoAzurePcl
{
    public class MenuPage : ContentPage
    {
        public MenuPage()
        {
            Button newTask = new Button { Text = "New Task" };
            newTask.Clicked += (sender, e) => this.Navigation.PushAsync(new CreateTaskPage());

            Button showTasks = new Button { Text = "Show Tasks" };
            showTasks.Clicked += async (sender, e) =>
                {
                    await this.Navigation.PushAsync(new ShowTasksPage());
                    await App.LoadTasks();
                };

            Button myProfile = new Button { Text = "Show Profile" };
            myProfile.Clicked += (sender, e) => this.Navigation.PushAsync(new ProfilePage());

            Button users = new Button { Text = "My Contacts" };
            users.Clicked += (sender, e) => this.Navigation.PushAsync(new UsersPage());

            this.Title = "Menu Items";
            this.Content = new StackLayout { Children = { newTask, showTasks, myProfile, users }, Orientation = StackOrientation.Vertical };
        }
        

        
    }
}
