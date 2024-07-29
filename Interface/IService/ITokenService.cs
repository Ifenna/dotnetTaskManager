using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskManager_API.models;

namespace taskManager_API.Interface.IService
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}