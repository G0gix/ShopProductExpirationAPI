using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopProductExpirationAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopProductExpirationAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ShopEmploye> _userManager;
        private readonly SignInManager<ShopEmploye> _signInManager;

        public AccountController(UserManager<ShopEmploye> userManager, SignInManager<ShopEmploye> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(string userName, string password)
        {

            //var result = await _signInManager.PasswordSignInAsync("newwwAdmin", "AdminPass123@", false, false);
            var result = await _signInManager.PasswordSignInAsync(userName, password, true, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(userName);
                Dictionary<string, string> serOutput = new Dictionary<string, string>()
                {
                    {"Succeeded", $"{result.Succeeded}" },
                    {"Id",$"{user.Id}" },
                    {"UserName",$"{user.UserName}" },
                    {"EmployeName",$"{user.EmployeName}" }
                };

                return Ok(serOutput);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{number}")]
        public async Task<IActionResult> SignOut(int number)
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
