using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace taskManager_API.models
{
    public class Tags
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
         public List<TaskTag> TaskTags { get; set; }
    }
}