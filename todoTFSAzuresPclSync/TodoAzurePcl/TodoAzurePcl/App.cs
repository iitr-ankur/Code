using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TodoAzurePcl.Helpers;

namespace TodoAzurePcl
{
	public static class App
    {
        public static Contact MyProfile { get; set; }
        public static ObservableCollection<Contact> MyContacts = new ObservableCollection<Contact>();
        public static ObservableCollection<TodoTask> Tasks = new ObservableCollection<TodoTask>();
        public static SyncRepository<Contact> ContactsRepo;
        public static SyncRepository<TodoTask> TasksRepo;
        public static string configFile = "todoTFS.config";
		public static bool IsConfigLoaded = false;

		public static Appbindings appBindings = new Appbindings ();

        public static void SetContactsRepo(SyncRepository<Contact> contactsDb)
        {
            ContactsRepo = contactsDb;
        }

        public static void SetTasksRepo(SyncRepository<TodoTask> tasksRepo)
        {
            TasksRepo = tasksRepo;
        }

        public static Page GetMainPage()
        {
			return new NavigationPage (new MenuPage ());
        }

        public static void LoadConfig()
        {
            MyProfile = new Contact { Name = Settings.Name, PhoneNum = Settings.Phone, Id = Settings.Id };
			IsConfigLoaded = true;
        }

        public static bool IsConfigAvailable()
        {
            return Settings.IsRegistered;
        }

        public static void SaveConfig()
        {
            Settings.Name = MyProfile.Name;
            Settings.Phone = MyProfile.PhoneNum;
            Settings.Id = MyProfile.Id;
            Settings.IsRegistered = true;
        }

		public static async Task LoadContacts(bool refresh)
        {
            if(refresh)
            {
                appBindings.LoadingContacts = true;
            }
			var contactsList = await ContactsRepo.GetAllItemsAsync(refresh);
            MyContacts.Clear();
            foreach (var item in contactsList.OrderBy(p => p.Name))
            {
                MyContacts.Add(item);
            }
            appBindings.LoadingContacts = false;
        }

        public static async Task LoadTasks(bool refresh)
        {
            if(refresh)
            {
                appBindings.LoadingTasks = true;
            }
			var tasksList = await TasksRepo.GetAllItemsAsync(refresh);
            Tasks.Clear();
            foreach (var item in tasksList.OrderBy(p => p.TaskStatus))
            {
                Tasks.Add(item);
            }
            appBindings.LoadingTasks = false;
        }
    }
}

