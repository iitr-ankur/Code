using System;
using System.ComponentModel;

namespace TodoAzurePcl
{
	public class Appbindings : INotifyPropertyChanged
	{
		public Appbindings() { LoadingContacts = true; LoadingTasks = true; }

		public event PropertyChangedEventHandler PropertyChanged;
	
		private void OnPropertyChanged(string info)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(info));
			}
		}


		private bool _loadingContacts;
		public bool LoadingContacts
		{
			get { return _loadingContacts; }
			set
			{
				if (_loadingContacts != value) {
					_loadingContacts = value;
                    OnPropertyChanged("LoadingContacts");
				}
			}
		}

        private bool _loadingTasks;
        public bool LoadingTasks
        {
            get { return _loadingTasks; }
            set
            {
                if (_loadingTasks != value)
                {
                    _loadingTasks = value;
                    OnPropertyChanged("LoadingTasks");
                }
            }
        }
	}
}

