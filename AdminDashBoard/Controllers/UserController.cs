using AdminDashBoard.Models;
using Domain.Models.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AdminDashBoard.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager )
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var Users = await _userManager.Users.Select(U => new UserViewModel()
            {
                Id = U.Id,
                DisplayName = U.DisplayName,
                Email = U.Email!,
                UserName = U.UserName!,

                Roles = _userManager.GetRolesAsync(U).Result
            }).ToListAsync();


            return View(Users);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id) { 
        var user=await _userManager.FindByIdAsync(id);

            var role=await _roleManager.Roles.ToListAsync();


            var UserRole = new UserRoleViewModel()
            {
              
                UserName = user.UserName
                  ,
                Roles = role.Select(r => new UpdateRoleViewModel()
                {
                    Id = r.Id,
                    Name = r.Name,

                    IsSelected = _userManager.IsInRoleAsync(user,r.Name).Result

                }).ToList()
            };  

            return View(UserRole);
        
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string id, UserRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(id);

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in model.Roles)
            {

                if (roles.Any(r => r == role.Name) && !role.IsSelected)
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                else if (!roles.Any(r => r == role.Name) && role.IsSelected)
                    await _userManager.AddToRoleAsync(user, role.Name);


            }

            return RedirectToAction(nameof(Index));
        }
    }
}
