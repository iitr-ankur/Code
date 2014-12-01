using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testcodefirstService.DataObjects
{
    public class Thread : EntityData
    {
        public string Title { get; set; }

        public string GroupId { get; set; }
    }
}