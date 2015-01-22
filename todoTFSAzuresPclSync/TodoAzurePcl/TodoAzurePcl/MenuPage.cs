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
                };

            Button myProfile = new Button { Text = "Show Profile" };
            myProfile.Clicked += (sender, e) => this.Navigation.PushAsync(new ProfilePage());

            Button users = new Button { Text = "My Contacts" };
            users.Clicked += (sender, e) => this.Navigation.PushAsync(new UsersPage());

            this.Title = "Menu Items";
            this.Content = new StackLayout { Children = { newTask, showTasks, myProfile, users }, Orientation = StackOrientation.Vertical };
        }

        protected override async void OnAppearing()
        {
            if(!App.IsConfigAvailable())
            {
                System.Diagnostics.Debug.WriteLine("Diagnostic Log: Config file not available");
                App.LoadContacts(true);
                App.LoadTasks(true);
				await this.Navigation.PushAsync (new ProfilePage ());
				//return new NavigationPage(new ProfilePage());
			} else if(!App.IsConfigLoaded){
                System.Diagnostics.Debug.WriteLine("Diagnostic Log: Load Config");
                App.LoadConfig();
                //return new NavigationPage(new MenuPage());
            }
            base.OnAppearing();
        }
        

        
    }
}
