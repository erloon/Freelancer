using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FR.Identity.API.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FR.Identity.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }
        [HttpPost]
        public async Task<Result> Register([FromBody]Register registerUser)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = null;
                var user = await _userManager.FindByEmailAsync(registerUser.Email);
 
                if (user != null)
                {
                    return new Result
                    {
                        Status = Status.Error,
                        Message = "Invalid data",
                        Data = "User already exists"
                    };
                }
 
                user = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = registerUser.Email,
                    CompanyName = registerUser.CompanyName
                };
 
                result = await _userManager.CreateAsync(user, registerUser.Password);
 
                if (result.Succeeded)
                {
                    return new Result
                    {
                        Status = Status.Success,
                        Message = "User Created",
                        Data = user
                    };
                }
                else
                {
                    var resultErrors = result.Errors.Select(e => e.Description);
                    return new Result
                    {
                        Status = Status.Error,
                        Message = "Invalid data",
                        Data = string.Join("", resultErrors)
                    };
                }
            }
 
            var errors = ModelState.Keys.Select(e => e);
            return new Result
            {
                Status = Status.Error,
                Message = "Invalid data",
                Data = string.Join("", errors)
            };
        }
        [HttpPost]
        public async Task<Result> Login([FromBody]Login model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
 
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                    identity.AddClaim(new Claim("CompanyName", user.CompanyName));
 
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
 
                    return new Result
                    {
                        Status = Status.Success,
                        Message = "Successful login",
                        Data = model
                    };
                }
 
                return new Result
                {
                    Status = Status.Error,
                    Message = "Invalid data",
                    Data = "Invalid Username or Password"
                };
            }
 
            var errors = ModelState.Keys.Select(e => e);
            return new Result
            {
                Status = Status.Error,
                Message = "Invalid data",
                Data = string.Join("", errors)
            };
        }

        [HttpGet]
        public async Task<UserState> Authenticated()
        {
            return await Task.Run(() =>
                new UserState
                {
                    IsAuthenticated = User.Identity.IsAuthenticated,
                    Username = User.Identity.IsAuthenticated ? User.Identity.Name : string.Empty
                });
        }
 
        [HttpPost]
        public async Task SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}