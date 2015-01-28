using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TodoAzurePcl
{
    public class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            Entry nameEntry = new Entry();
            Entry phoneNumEntry = new Entry();

            if (App.MyProfile == null || string.IsNullOrWhiteSpace(App.MyProfile.Name))
            {
                nameEntry.Text = "Name";
                nameEntry.TextColor = Color.Gray;
            }
            else
            {
                nameEntry.Text = App.MyProfile.Name;
            }

            if (App.MyProfile == null || string.IsNullOrWhiteSpace(App.MyProfile.PhoneNum))
            {
                phoneNumEntry.Text = "Phone Number";
                phoneNumEntry.TextColor = Color.Gray;
            }
            else
            {
                phoneNumEntry.Text = App.MyProfile.PhoneNum;
            }

            var saveButton = new Button { Text = "Save" };
            saveButton.Clicked += async (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(phoneNumEntry.Text) || phoneNumEntry.Text == "Phone Number")
                    await this.DisplayAlert("Invalid Entry", "Phone Number can't be empty", "OK");
                else if (string.IsNullOrWhiteSpace(nameEntry.Text) || nameEntry.Text == "Name")
                    await this.DisplayAlert("Invalid Entry", "Name can't be empty", "OK");
                else
                {
                    var myContact = new Contact { Name = nameEntry.Text, PhoneNum = phoneNumEntry.Text };
                    if (App.MyProfile != null)
                    {
                        myContact.Id = App.MyProfile.Id;
                    }

                    await App.ContactsRepo.SaveItemAsync(myContact);
                    App.MyProfile = myContact;
                    App.SaveConfig();
					await this.Navigation.PopAsync();
                }
            };

            this.Title = "MyProfile";
            this.Content = new StackLayout { Children = { nameEntry, phoneNumEntry, saveButton }, Orientation = StackOrientation.Vertical };
        }

    }
}
