using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testcodefirstService.DataObjects
{
    public class Message : EntityData
    {
        public string ThreadId {get; set;}
		public string UserId {get; set;}
        public string Text { get; set;}

    }
}
