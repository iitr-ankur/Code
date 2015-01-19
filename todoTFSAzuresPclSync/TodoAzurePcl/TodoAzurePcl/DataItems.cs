using System;
using Newtonsoft.Json;

namespace TodoAzurePcl
{
//	public class TodoItem : EntityData
//	{
//		public TodoItem ()
//		{
//		}
//
//		private static bool save = false;
//
//		//public string Id { get; set; }
//
//		[JsonProperty(PropertyName = "text")]
//		public string Text { get; set; }
//
//		private bool _complete;
//		[JsonProperty(PropertyName = "complete")]
//		public bool Complete { 
//			get { return _complete; } 
//			set {
//				if (_complete != value) {
//					_complete = value;
//					if (save) {
//						App.todoItemManager.SaveTaskAsync (this);
//					}
//				}
//
//			} 
//		}
//
//		public static void SetSaveMode(bool savemode){
//			save = savemode;
//		}
//	}

	public class TodoTask : EntityData
	{
		[JsonProperty(PropertyName = "taskDescription")]
		public string TaskDescription{get; set;}

		[JsonProperty(PropertyName = "history")]
		public string  History{get; set;}

		[JsonProperty(PropertyName = "assignedToId")]
		public string AssignedToId{get; set;}

		[JsonProperty(PropertyName = "createdById")]
		public string CreatedById{get; set;}

		[JsonProperty(PropertyName = "taskStatus")]
		public int TaskStatus{get; set;}

		[JsonProperty(PropertyName = "parentTaskId")]
		public string ParentTaskId{get; set;}

		// Pending:
		//public List<Attachment> Attachments { get; set; }
	}

	public class Contact : EntityData
	{
		[JsonProperty(PropertyName = "name")]
		public string Name{get; set;}

		[JsonProperty(PropertyName = "phoneNum")]
		public string PhoneNum{get; set;}
	}
}

