using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace TodoAzurePcl
{
    public class CreateTaskPage : ContentPage
    {
        public CreateTaskPage(TodoTask savedTask = null)
        {
            Label taskLabel = new Label { Text = "Task:", VerticalOptions = LayoutOptions.Fill, HorizontalOptions = LayoutOptions.FillAndExpand };
            Editor taskDescription = new Editor() { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };

            Label assignedTo = new Label { HorizontalOptions = LayoutOptions.FillAndExpand };
            Label createdBy = new Label { HorizontalOptions = LayoutOptions.FillAndExpand };
            Label status = new Label { HorizontalOptions = LayoutOptions.FillAndExpand };

            Editor comment = new Editor() { Text = "Add Comments", VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };

            Button saveButton = new Button { Text = "Save" };
            saveButton.Clicked += async (sender, e) =>
                {
                    var newTask = new TodoTask();
                    newTask.AssignedToId = App.MyProfile.Id;
                    newTask.CreatedById = App.MyProfile.Id;
                    newTask.History = comment.Text;
                    newTask.TaskDescription = taskDescription.Text;
                    newTask.TaskStatus = (int)TaskStatus.Active;
                    await App.TasksRepo.SaveItemAsync(newTask);
                };

            if(null != savedTask)
            {
                assignedTo.Text = "AssignedTo: " + savedTask.AssignedToId;
                createdBy.Text = "CreatedBy: " + savedTask.CreatedById;
                status.Text = "Status: " + ((TaskStatus)savedTask.TaskStatus).ToString();
                taskDescription.Text = savedTask.TaskDescription;
            }
            else
            {
                assignedTo.Text = "AssignedTo: " +  App.MyProfile.Name;
                createdBy.Text = "CreatedBy: " + App.MyProfile.Name;
                status.Text = "Status: " + TaskStatus.Active.ToString();
                taskDescription.Text = "Add Description";
            }

            var stack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = {
                    taskLabel,
                    taskDescription,
					//new StackLayout{Orientation = StackOrientation.Horizontal, Children = {taskLabel, taskDescription}},
                    //new StackLayout{Orientation = StackOrientation.Horizontal, Children = {status, assignedTo, createdBy}},
                    status,
                    assignedTo,
                    createdBy,
                    comment,
                    saveButton
				}
            };

            if(null != savedTask)
            {
                stack.Children.Add(new Label{Text = "History: " + savedTask.History});
            }

            this.Title = "New Task";
            Content = stack;

        }
    }
}
