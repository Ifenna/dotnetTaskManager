using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using taskManager_API.Dtos;
using taskManager_API.Dtos.Accounts;
using taskManager_API.models;
using taskManager_API.Services;

namespace taskManager_API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;
        private readonly SignInManager<AppUser> _sign;
        public AccountController(UserManager<AppUser> userManager, TokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _sign = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto){
            if(!ModelState.IsValid){
                return BadRequest("Invalid request");
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);
            if(user == null) return Unauthorized("invalid credentials");

            var result = await _sign.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(!result.Succeeded) return Unauthorized("invalid credential");

            return Ok(
                new NewUserdto{
                     Email = user.Email,
                    UserName = user.UserName,
                     Token = _tokenService.CreateToken(user)
                    
                }
            );

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var appUser = new AppUser{
                    UserName = registerDto.UserName,
                    Email = registerDto.Email
                };
                var createUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                if (createUser.Succeeded)
                {
                    var addRole = await _userManager.AddToRoleAsync(appUser, "User");
                    if(addRole.Succeeded)
                    {
                        // var tes =  _tokenService.CreateToken(appUser);
                        return Ok(
                            new NewUserdto{
                                Email = appUser.Email,
                                UserName = appUser.UserName,
                                // Token = _tokenService.CreateToken(appUser)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, addRole.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createUser.Errors);
                }
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e);
            }
        }
        
    }
}