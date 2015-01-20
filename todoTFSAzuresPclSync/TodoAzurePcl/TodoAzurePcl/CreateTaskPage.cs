using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TodoAzurePcl
{
    public class CreateTaskPage : ContentPage
    {
        private TodoTask _task;
        private Editor _comment;
        private Editor _description;
        private bool _needsSaving = false;

        public CreateTaskPage(TodoTask savedTask = null)
        {
            if(savedTask == null)
            {
                _task = new TodoTask
                {
                    AssignedToId = App.MyProfile.Id,
                    CreatedById = App.MyProfile.Id,
                    TaskStatus = ((int)TaskStatus.Active),
                    History = string.Empty,
                    TaskDescription = "Add Description"
                };
            }
            else
            {
                _task = savedTask;
            }

            Label taskLabel = new Label { Text = "Task:", VerticalOptions = LayoutOptions.Fill, HorizontalOptions = LayoutOptions.FillAndExpand };
            _description = new Editor() { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand, Text = _task.TaskDescription };

            var assignedTo = new Button { HorizontalOptions = LayoutOptions.FillAndExpand, Text = ("AssignedTo: " + _task.AssignedToId) };
            assignedTo.Clicked += (sender, e) =>
                {
                    MessagingCenter.Subscribe<ReAssignPage, Contact>(this, Constants.ReassignTag + _task.Id, (reassignPage, contact) =>
                        {
                            if (_task.AssignedToId != contact.Id)
                            {
                                _needsSaving = true;
                                _task.AssignedToId = contact.Id;
                                assignedTo.Text = ("AssignedTo: " + _task.AssignedToId);
                            }
                        });
                    this.Navigation.PushAsync(new ReAssignPage(_task.Id));
                };



            var createdBy = new Label { HorizontalOptions = LayoutOptions.FillAndExpand, Text = ("CreatedBy: " + _task.CreatedById) };

            var status = new Button { HorizontalOptions = LayoutOptions.FillAndExpand, Text = ("Status: " + ((TaskStatus)_task.TaskStatus).ToString()) };
            status.Clicked += async (sender, e) =>
                {
                    List<string> statusList = new List<string>();
                    foreach (var item in Enum.GetValues(typeof(TaskStatus)).Cast<TaskStatus>())
                    {
                        statusList.Add(item.ToString());
                    }
                    var action = await DisplayActionSheet("Change Status To:", null, null, statusList.ToArray());
                    var selectedStatus = (int)(TaskStatus)(Enum.Parse(typeof(TaskStatus), action));
                    if (_task.TaskStatus != selectedStatus)
                    {
                        _task.TaskStatus = selectedStatus;
                        status.Text = ("Status: " + ((TaskStatus)_task.TaskStatus).ToString());
                        _needsSaving = true;
                    }
                };


            _comment = new Editor() { Text = "Add Comments", VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };

			var history = new ScrollView{ Content = new Label { Text = _task.History } };

            var stack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = {
                    taskLabel,
                    _description,
					//new StackLayout{Orientation = StackOrientation.Horizontal, Children = {taskLabel, taskDescription}},
                    //new StackLayout{Orientation = StackOrientation.Horizontal, Children = {status, assignedTo, createdBy}},
                    status,
                    assignedTo,
                    createdBy,
                    _comment,
                    history,
				}
            };

            this.Title = _task.Id == null ? "New Task" : (_task.TaskDescription.Substring(0, _task.TaskDescription.Length > 10 ? 10 : _task.TaskDescription.Length) + "...");
            Content = stack;
            //Content = new ScrollView { Content = stack };
        }

        protected override async void OnDisappearing()
        {
            if(!string.Equals(_description.Text.Trim(), _task.TaskDescription.Trim()))
            {
                _needsSaving = true;
            }

			if(!string.Equals(_comment.Text.Trim(), "Add Comments"))
            {
                _needsSaving = true;
            }

            if(_needsSaving)
            {
                await SaveTask();
            }
 
            base.OnDisappearing();
        }

        private async Task SaveTask()
        {
			if (!string.Equals (_comment.Text.Trim (), "Add Comments")) {
				_task.History = string.Format ("On {0}, {1} added: \n {2}\n\n{3}", DateTime.UtcNow.ToString (), App.MyProfile.Name, _comment.Text.Trim (), _task.History);
			}
            _task.TaskDescription = _description.Text.Trim();
            await App.TasksRepo.SaveItemAsync(_task);
        }
    }
}
