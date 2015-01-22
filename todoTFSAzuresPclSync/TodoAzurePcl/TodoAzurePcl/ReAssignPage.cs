﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using System.Threading.Tasks;

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
			users.ItemSelected += (sender, e) => {
				if (e.SelectedItem != null) {
					var reassignTo = (Contact)e.SelectedItem;
					users.SelectedItem = null;
					MessagingCenter.Send<ReAssignPage, Contact> (this, Constants.ReassignTag + taskId, reassignTo);
					this.Navigation.PopAsync ();
				}
			};

            this.Title = "Re-assign To:";
            this.Content = new StackLayout() { Children = { users }, Orientation = StackOrientation.Vertical };

        }
    }
}
