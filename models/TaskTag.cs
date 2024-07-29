using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace taskManager_API.models
{
    public class TaskTag
    {
        public string TaskId { get; set; }
        public Tasks Task { get; set; }
        public string TagId { get; set; }
        public Tags Tag { get; set; }
    }
}