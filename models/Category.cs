using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace taskManager_API.models
{
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Tasks> Tasks { get; set; }
    }
}