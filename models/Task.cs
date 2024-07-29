using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace taskManager_API.models
{
    public class Tasks
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public string Priority { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string UserId { get; set; }
        public List<AppUser> Users {get; set;}
        public List<TaskTag> TaskTags { get; set; }
    }
}