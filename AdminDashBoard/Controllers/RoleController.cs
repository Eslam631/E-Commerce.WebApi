using AdminDashBoard.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AdminDashBoard.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()

        {

           var Roles=await _roleManager.Roles.ToListAsync();
            return View(Roles);
        }


        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleView)
        {
            if(!ModelState.IsValid)
               return RedirectToAction(nameof(Index));
            else
            {
                var RoleExists = await _roleManager.RoleExistsAsync(roleView.Name);

                if (!RoleExists) {

                    var Role = await _roleManager.CreateAsync(new IdentityRole()
                    {
                        Name = roleView.Name,
                    });

                 return   RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Name", "Role Already Exists");
                    return View(nameof(Index),await _roleManager.Roles.ToListAsync() );
                }

           
              
            
              
                
                    
                
            }
        }

     
        public async Task<IActionResult> Delete(string Id)
        {
            var role=await _roleManager.FindByIdAsync(Id);

            if(role != null)
                await _roleManager.DeleteAsync(role);


                return RedirectToAction(nameof(Index));
                
        }


        #region Edit

        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            
            var role = await _roleManager.FindByIdAsync(Id);
            if(role != null)
           {
                var RoleView = new UpdateRoleViewModel()
                {
                    Name = role.Name,
                };
                
                return View(RoleView); }
            else
            {
                ModelState.AddModelError("Id", "Not Role Fount This Id");
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpPost]

        public async Task<IActionResult> Edit([FromRoute] string Id, UpdateRoleViewModel updateRoleView)
        {

            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(Id);

                role.Name=updateRoleView.Name;

                var RoleExists = await _roleManager.RoleExistsAsync(updateRoleView.Name);

                if (!RoleExists)
                {

                    await _roleManager.UpdateAsync(role);
                    return RedirectToAction(nameof(Index));


                }

                ModelState.AddModelError("Name", "Role Is Already Exits");



            }

            return View(updateRoleView);
        }
            #endregion

        }
}
