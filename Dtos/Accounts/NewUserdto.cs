using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace taskManager_API.Dtos.Accounts
{
    public class NewUserdto
    {
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Token { get; set; }
    }
}