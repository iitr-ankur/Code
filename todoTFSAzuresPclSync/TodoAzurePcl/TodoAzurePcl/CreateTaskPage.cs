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
				_task = new TodoTask {
					AssignedToId = App.MyProfile.Id,
					AssignedTo = App.MyProfile.Name,
					CreatedById = App.MyProfile.Id,
					CreatedBy = App.MyProfile.Name,
					TaskStatus = ((int)TaskStatus.Active),
					History = string.Empty,
					TaskDescription = string.Empty,
				};
            }
            else
            {
                _task = savedTask;
            }

            Label taskLabel = new Label { Text = "Task:"};
            _description = new Editor() { Text = _task.TaskDescription };


			var assigendToLbl = new Label{ Text = "Assigned To:" };
			var assignedTo = new Button { Text = ("AssignedTo: " + _task.AssignedTo) };
			assignedTo.BindingContext = _task;
			assignedTo.SetBinding (Button.TextProperty, "AssignedTo");
			_task.PropertyChanged += (sender, e) => {
				if (e.PropertyName == "AssignedTo") {
					_needsSaving = true;
				}
			};

            assignedTo.Clicked += (sender, e) =>
                {
					this.Navigation.PushAsync(new ReAssignPage(ref _task));
                };

			var createdBy = new Label { Text = _task.CreatedById, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };

			var status = new Button { Text = ((TaskStatus)_task.TaskStatus).ToString() };
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


            _comment = new Editor() { };

			var history = new ScrollView{ Content = new Label { Text = _task.History } };

//            var stack = new StackLayout
//            {
//                Orientation = StackOrientation.Vertical,
//                Children = {
//                    taskLabel,
//                    _description,
//					//new StackLayout{Orientation = StackOrientation.Horizontal, Children = {taskLabel, taskDescription}},
//                    //new StackLayout{Orientation = StackOrientation.Horizontal, Children = {status, assignedTo, createdBy}},
//					new StackLayout{Orientation = StackOrientation.Horizontal, Children  = {assigendToLbl, assignedTo}},
//                    status,
//                    createdBy,
//                    _comment,
//                    history,
//				}
//            };

            this.Title = _task.Id == null ? "New Task" : (_task.TaskDescription.Substring(0, _task.TaskDescription.Length > 10 ? 10 : _task.TaskDescription.Length) + "...");
            //Content = stack;


			Grid grid = new Grid
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				RowDefinitions = 
				{
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = GridLength.Auto },
//					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
//					new RowDefinition { Height = new GridLength(100, GridUnitType.Absolute) }
				},
				ColumnDefinitions = 
				{
					new ColumnDefinition { Width = GridLength.Auto },
					new ColumnDefinition { Width = GridLength.Auto },
//					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
//					new ColumnDefinition { Width = new GridLength(100, GridUnitType.Absolute) }
				}
			};

			grid.Children.Add (taskLabel, 0, 0);
			grid.Children.Add (_description, 1, 0);
			grid.Children.Add (assigendToLbl, 0, 1);
			grid.Children.Add (assignedTo, 1, 1);
			grid.Children.Add (new Label{ Text = "Status:" }, 0, 2);
			grid.Children.Add (status, 1, 2);
			grid.Children.Add (new Label{ Text = "Created By:" }, 0, 3);
			grid.Children.Add (createdBy, 1, 3);
			grid.Children.Add (new Label{ Text = "Add Comments:" }, 0, 4);
			grid.Children.Add (_comment, 1, 4);
			grid.Children.Add (new Label{ Text = "History:" }, 0, 5);
			grid.Children.Add (history, 1, 5);

			Content = grid;
        }

        protected override async void OnDisappearing()
        {
            if(!string.Equals(_description.Text.Trim(), _task.TaskDescription.Trim()))
            {
                _needsSaving = true;
            }

			if(!string.IsNullOrWhiteSpace(_comment.Text))
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
