using System;
using Newtonsoft.Json;

namespace TodoAzurePcl
{
	public class TodoItem : EntityData
	{
		public TodoItem ()
		{
		}

		private static bool save = false;

		//public string Id { get; set; }

		[JsonProperty(PropertyName = "text")]
		public string Text { get; set; }

		private bool _complete;
		[JsonProperty(PropertyName = "complete")]
		public bool Complete { 
			get { return _complete; } 
			set {
				if (_complete != value) {
					_complete = value;
					if (save) {
						App.todoItemManager.SaveTaskAsync (this);
					}
				}

			} 
		}

		public static void SetSaveMode(bool savemode){
			save = savemode;
		}
	}
}

