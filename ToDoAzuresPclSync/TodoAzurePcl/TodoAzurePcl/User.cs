using System;
using Newtonsoft.Json;

namespace TodoAzurePcl
{
	public class User : EntityData
	{
		public User ()
		{
		}

		private static bool save = false;

		//public string Id { get; set; }

		public string UserName { get; set; }

		public static void SetSaveMode(bool savemode){
			save = savemode;
		}
	}
}

