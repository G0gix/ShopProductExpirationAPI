using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopProductExpirationAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopProductExpirationAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<ShopEmploye> _userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ShopEmploye> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<ActionResult<IdentityRole[]>> Get()
        {
            return await _roleManager.Roles.ToArrayAsync();
        }

        [Authorize(Roles = "admin")]
        [HttpPost("{roleName}")]
        public async Task<IActionResult> CreateNewRole(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                else
                {
                    Dictionary<string, string> errors = new Dictionary<string, string>();
                    foreach (var error in result.Errors)
                    {
                        errors.Add(error.Code, error.Description);
                    }
                    return BadRequest(errors);
                }
            }
            return BadRequest();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateNewUserWithRole(string email, string password, string username, string employeName, string role)
        {
            ShopEmploye newEmploye = new ShopEmploye { Email = email, UserName = username, EmployeName = employeName };
            IdentityResult result = await _userManager.CreateAsync(newEmploye, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newEmploye, role);
                Dictionary<string, string> serOutput = new Dictionary<string, string>()
                {
                    {"Succeeded", $"{result.Succeeded}" },
                    {"Id",$"{newEmploye.Id}" },
                    {"UserName",$"{newEmploye.UserName}" },
                    {"EmployeName",$"{newEmploye.EmployeName}" }
                };

                return Ok(serOutput);
            }
            else
            {
                Dictionary<string, string> errors = new Dictionary<string, string>();
                foreach (var error in result.Errors)
                {
                    errors.Add(error.Code, error.Description);
                }
                return BadRequest(errors);
            }
        }
    }
}
