using System;
using Newtonsoft.Json;

namespace TodoAzurePcl
{
	public class Thread : EntityData
	{
		public string Title { get; set; }
		public string GroupId { get; set; }

		public string GroupName { get; set;}
	}

	public class Message : EntityData
	{
		public string ThreadId { get; set; }
		public string UserId { get; set; }
		public string Text { get; set; }
	}
}

