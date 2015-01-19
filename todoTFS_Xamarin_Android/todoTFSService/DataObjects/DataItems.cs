using Microsoft.WindowsAzure.Mobile.Service;
using System.Collections.Generic;

namespace todoTFSService.DataObjects
{
    public class TodoTask : EntityData
    {
        public string TaskDescription{get; set;}
		public string  History{get; set;}
		public string AssignedToId{get; set;}
		public string CreatedById{get; set;}
		public int TaskStatus{get; set;}
        public string ParentTaskId{get; set;}

        // Pending:
        //public List<Attachment> Attachments { get; set; }
    }

    public class Contact : EntityData
    {
        public string Name{get; set;}
        public string PhoneNum{get; set;}
    }

    // Pending:
    //public class Attachment
    //{
    //    public string LocalFilePath{get; set;}
    //    public string AzureFilePath{get; set;}
    //}


}