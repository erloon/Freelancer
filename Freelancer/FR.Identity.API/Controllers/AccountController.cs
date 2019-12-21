using System;
using System.Linq;
using System.Threading.Tasks;
using FR.IdentityServer.Infrastructure;
using FR.IdentityServer.Model;
using FR.IdentityServer.Model.API;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FR.IdentityServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;

        public AccountController(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _interaction = interaction ?? throw new ArgumentNullException(nameof(interaction));
            _clientStore = clientStore ?? throw new ArgumentNullException(nameof(clientStore));
            _schemeProvider = schemeProvider ?? throw new ArgumentNullException(nameof(schemeProvider));
            _events = events ?? throw new ArgumentNullException(nameof(events));
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
 
                user = new ApplicationUser
                {
                    UserName = registerUser.Name,
                    Name =  registerUser.Name,
                    Id = Guid.NewGuid().ToString(),
                    Email = registerUser.Email,
                    CompanyName = registerUser.CompanyName
                };
 
                result = await _userManager.CreateAsync(user, registerUser.Password);
 
                if (result.Succeeded)
                {
                     
                    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("userName", user.UserName));
                    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("name", user.Name));
                    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("email", user.Email));
                    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("role", Roles.Consumer));

                    return new Result
                    {
                        Status = Status.Success,
                        Message = "User Created"
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
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberLogin, lockoutOnFailure: true);
                var user = await _userManager.FindByNameAsync(model.Email);
                if (result.Succeeded)
                {
                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName));

                }
                return  new Result()
                {
                    Status = Status.Success,
                    Message = "Successful login",
                    Data = user
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
            if (User?.Identity.IsAuthenticated == true)
            {
                await _signInManager.SignOutAsync();

                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }
        }
    }
}