using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;

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
            var text = FileHelper.ReadAllText(configFile);
            var lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            string name = lines[0].Trim().Replace("Name\t", "");
            string phoneNum = lines[1].Trim().Replace("Phone\t", "");
            string id = lines[2].Trim().Replace("Id\t", "");

            MyProfile = new Contact { Name = name, PhoneNum = phoneNum, Id = id };
			IsConfigLoaded = true;
        }

        public static bool IsConfigAvailable()
        {
            return FileHelper.Exists(configFile);
        }

        public static void SaveConfigFile()
        {
            string config = string.Empty;
            config += string.Format("Name\t{0}\n", MyProfile.Name);
            config += string.Format("Phone\t{0}\n", MyProfile.PhoneNum);
            config += string.Format("Id\t{0}\n", MyProfile.Id);
            FileHelper.WriteAllText(configFile, config);
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

