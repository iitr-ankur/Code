using Microsoft.WindowsAzure.Mobile.Service;

namespace testcodefirstService.DataObjects
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }

        public string Status { get; set; }
    }
}