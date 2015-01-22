using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TodoAzurePcl
{
	public class NotifyPropertyChanged : INotifyPropertyChanged
	{
		public NotifyPropertyChanged ()
		{
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(propertyName));
		}

		public bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
		{
			if (Object.Equals(storage, value))
				return false;
			storage = value;
			OnPropertyChanged(propertyName);
			return true;
		}
	}
}

