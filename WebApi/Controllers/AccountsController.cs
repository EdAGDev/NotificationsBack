using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly AccountServices accountServices;

        public AccountsController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            AccountServices accountServices)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.accountServices = accountServices;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ResponseAuth>> Register(CredentialsUser credentialsUser)
        {
            var user = new IdentityUser
            {
                UserName = credentialsUser.Email,
                Email = credentialsUser.Email
            };
            var result = await userManager.CreateAsync(user, credentialsUser.Password);

            if (result.Succeeded)
            {
                return accountServices.CreateToken(credentialsUser);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseAuth>> Login(CredentialsUser credentialsUser)
        {
            var result = await signInManager.PasswordSignInAsync(credentialsUser.Email,
                credentialsUser.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return accountServices.CreateToken(credentialsUser);
            }
            else
            {
                return BadRequest("Login Error");
            }
        }
    }
}
