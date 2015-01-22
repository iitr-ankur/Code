using System;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TodoAzurePcl
{
	public class TodoTask : EntityData
	{
		private string _taskDescription;
		[JsonProperty(PropertyName = "taskDescription")]
		public string TaskDescription{ get { return _taskDescription; } set { SetProperty (ref _taskDescription, value); } }

		private string _history;
		[JsonProperty(PropertyName = "history")]
		public string  History{ get { return _history; } set { SetProperty (ref _history, value); } }

		private string _assignedToId;
		[JsonProperty(PropertyName = "assignedToId")]
		public string AssignedToId{ get { return _assignedToId; } set { SetProperty (ref _assignedToId, value); } }

		private string _createdById;
		[JsonProperty(PropertyName = "createdById")]
		public string CreatedById{ get { return _createdById; } set { SetProperty (ref _createdById, value); } }

		private int _taskStatus;
		[JsonProperty(PropertyName = "taskStatus")]
		public int TaskStatus{ get { return _taskStatus; } set { SetProperty (ref _taskStatus, value); } }

		private string _parentTaskId;
		[JsonProperty(PropertyName = "parentTaskId")]
		public string ParentTaskId{ get { return _parentTaskId; } set { SetProperty (ref _parentTaskId, value); } }

		private string _assignedTo;
        [JsonProperty(PropertyName = "assignedTo")]
		public string AssignedTo { get { return _assignedTo; } set { SetProperty (ref _assignedTo, value); } }

		private string _createdBy;
        [JsonProperty(PropertyName = "createdBy")]
		public string CreatedBy { get { return _createdBy; } set { SetProperty (ref _createdBy, value); } }

		// Pending:
		//public List<Attachment> Attachments { get; set; }
	}

	public class Contact : EntityData
	{
		private string _name;
		[JsonProperty(PropertyName = "name")]
		public string Name{ get { return _name; } set { SetProperty (ref _name, value); } }

		private string _phoneNum;
		[JsonProperty(PropertyName = "phoneNum")]
		public string PhoneNum{ get { return _phoneNum; } set { SetProperty (ref _phoneNum, value); } }
	}
}

