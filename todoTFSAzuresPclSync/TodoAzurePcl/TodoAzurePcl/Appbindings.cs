using System;
using System.ComponentModel;

namespace TodoAzurePcl
{
	public class Appbindings : NotifyPropertyChanged
	{
        public Appbindings()
        {
            LoadingContacts = true;
            LoadingTasks = true;
        }

		private bool _loadingContacts;
		public bool LoadingContacts
		{
			get { return _loadingContacts; }
			set
			{
				SetProperty (ref _loadingContacts, value);
			}
		}

        private bool _loadingTasks;
        public bool LoadingTasks
        {
            get { return _loadingTasks; }
            set
            {
				SetProperty (ref _loadingTasks, value);
            }
        }
	}
}

