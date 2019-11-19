using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystem.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdministratorController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdministratorController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(AdministratorViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList", "Administrator");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult RoleList()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var roles = await roleManager.FindByIdAsync(id);
            if (roles == null)
            {
                ViewBag.Message = $"The {id} is not found in any role";
                return View("NotFound");
            }
            var model = new RoleEditViewModel()
            {
                ID = roles.Id,
                RoleName = roles.Name
            };
            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, model.RoleName))
                {
                    model.RoleMembers.Add(user.UserName);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(RoleEditViewModel model)
        {
            var roles = await roleManager.FindByIdAsync(model.ID);
            if (roles == null)
            {
                ViewBag.Message = $"The {model.ID} is not found in any role";
                return View("NotFound");
            }
            else
            {
                roles.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(roles);
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUserInRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            ViewBag.RoleId = id;
            if (role == null)
            {
                ViewBag.Message = $"The {id} is not found in any role";
                return View("NotFound");
            }
            else
            {
                var model = new List<UpdateUserInRoleViewModel>();
                foreach (var user in userManager.Users)
                {
                    UpdateUserInRoleViewModel updateUserInRoleViewModel = new UpdateUserInRoleViewModel();
                    updateUserInRoleViewModel.UserId = user.Id;
                    updateUserInRoleViewModel.UserName = user.UserName;
                    if (await userManager.IsInRoleAsync(user, role.Name))
                    {
                        updateUserInRoleViewModel.IsSelected = true;
                    }
                    else
                    {
                        updateUserInRoleViewModel.IsSelected = false;
                    }
                    model.Add(updateUserInRoleViewModel);
                }
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserInRole(List<UpdateUserInRoleViewModel> models, string roleID)
        {
            IdentityRole role = await roleManager.FindByIdAsync(roleID);

            if(role == null)
            {
                ViewBag.Message = $"The {roleID} is not found in any role";
                return View("NotFound");
            }
            else
            {
                for(int i = 0; i < models.Count; i++)
                {
                    IdentityResult result = null;
                    ApplicationUser user = await userManager.FindByIdAsync(models[i].UserId);
                    if(models[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                    {
                        result = await userManager.AddToRoleAsync(user, role.Name);
                    }
                    else if (!models[i].IsSelected && (await userManager.IsInRoleAsync(user, role.Name)))
                    {
                        result = await userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                    else
                    {
                        continue;
                    }
                    if (result.Succeeded)
                    {
                        if(i < models.Count - 1)
                        {
                            continue;
                        }
                        else
                        {
                            return RedirectToAction("EditRole", new { id = roleID });
                        }
                    }
                }

                return RedirectToAction("EditRole", new { id = roleID });
            }
        }

        [HttpGet]
        public IActionResult UserList()
        {
            var users = userManager.Users;
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userRole = await userManager.GetRolesAsync(user);
                var userClaims = await userManager.GetClaimsAsync(user);

                EditUserModelView model = new EditUserModelView
                {
                    ID = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Gender = user.Gender,
                    UserRoles = userRole,
                    UserClaim = userClaims.Select(y => y.Value).ToList()
                };
                return View(model);
            }
            else
            {
                ViewBag.Message = $"User Id {id} is not found";
                return View("NotFound");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserModelView model)
        {
            ApplicationUser user = await userManager.FindByIdAsync(model.ID);
            if (user != null)
            {
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.Gender = model.Gender;

                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserList");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                return View(model);
            }
            else
            {
                ViewBag.Message = $"User Id {model.ID} is not found";
                return View("NotFound");
            }
        }
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.Message = $"User Id {id} is not found";
                return View("NotFound");
            }
            else
            {
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserList");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View("UserList");
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.Message = $"The {id} is not found in any role";
                return View("NotFound");
            }
            else
            {
                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                        return View("RoleList");
                    }
                }
                return View("RoleList");
            }
        }
    }
}
