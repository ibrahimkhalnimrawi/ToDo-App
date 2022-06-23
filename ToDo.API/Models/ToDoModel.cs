using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ToDo.API.Models
{
    public class ToDoModel 
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public bool IsChecked { get; set; }
 
    }
}
