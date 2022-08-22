using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RevenueAITask.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RevenueAITask.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace RevenueAITask.Models
{
    public class Permission
    {
        public string Entity { get; set; }
        public string Operation { get; set; }
    }
    public class LoginController : Controller
    {

        private readonly RevenueAIContext _context;

        public LoginController(RevenueAIContext context)
        {
            _context = context;
        }




        // GET: LoginController
        public ActionResult Index()
        {
            User Users = new User();
            return View(Users);
        }


        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(User userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _context.Users
                        .FirstOrDefault(x => x.UserName == userModel.UserName  && x.Password == userModel.Password.PW_Encrypt());
                    if (user == null)
                    {
                        //Add logic here to display some message to user    
                        ViewBag.Message = "Invalid Credential";
                        return View("Index", userModel);
                    }
                    else
                    {
                        user.LastLoginTime = DateTime.Now;
                        _context.Update(user);
                        _context.SaveChanges();

                        //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    
                        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.UserId)));
                        identity.AddClaim(new Claim(ClaimTypes.Name, Convert.ToString(user.FirstName + " " + user.LastName)));
                        identity.AddClaim(new Claim(ClaimTypes.Email, user.UserName));

                        SetPermissionClaims(user, identity);

                        //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
                        var principal = new ClaimsPrincipal(identity);

                        //SignInAsync is a Extension method for Sign in a principal for the specified scheme.    
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                        {
                            IsPersistent = false//objLoginModel.RememberLogin
                        });

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                return View("Index");
            }
        }

        private void SetPermissionClaims(User user, ClaimsIdentity identity)
        {
            // Add User Permissions
            Func<User, ClaimsIdentity, List<Permission>> permissionsFunc = user.UserTypeId switch
            {
                1 => SetPermissionClaimsForAdmin,
                2 => SetPermissionClaimsForUser,
                _ => SetPermissionClaimsForGuest
            };


            permissionsFunc(user, identity).ForEach(p =>
            {
                identity.AddClaim(new Claim(p.Entity, p.Operation));
            });

        }

        private List<Permission> SetPermissionClaimsForAdmin(User user, ClaimsIdentity identity)
        {

            var permissions = new List<Permission>
            {
                new Permission { Entity = "Accounts", Operation = "Create"},
                new Permission { Entity = "Accounts", Operation = "Read"},
                new Permission { Entity = "Accounts", Operation = "Update"},
                new Permission { Entity = "Accounts", Operation = "Delete"},

                new Permission { Entity = "Cards", Operation = "Create"},
                new Permission { Entity = "Cards", Operation = "Read"},
                new Permission { Entity = "Cards", Operation = "Update"},
                new Permission { Entity = "Cards", Operation = "Delete"},

                new Permission { Entity = "Transactions", Operation = "Create"},
                new Permission { Entity = "Transactions", Operation = "Read"},
                new Permission { Entity = "Transactions", Operation = "Update"},
                new Permission { Entity = "Transactions", Operation = "Delete"},

                new Permission { Entity = "Users", Operation = "Create"},
                new Permission { Entity = "Users", Operation = "Read"},
                new Permission { Entity = "Users", Operation = "Update"},
                new Permission { Entity = "Users", Operation = "Delete"},

                new Permission { Entity = "Vendors", Operation = "Create"},
                new Permission { Entity = "Vendors", Operation = "Read"},
                new Permission { Entity = "Vendors", Operation = "Update"},
                new Permission { Entity = "Vendors", Operation = "Delete"},

                new Permission { Entity = "Lookups", Operation = "Create"},
                new Permission { Entity = "Lookups", Operation = "Read"},
                new Permission { Entity = "Lookups", Operation = "Update"},
                new Permission { Entity = "Lookups", Operation = "Delete"},
            };

            return permissions;
        }

        private List<Permission> SetPermissionClaimsForUser(User user, ClaimsIdentity identity)
        {
            var permissions  = new List<Permission>
            {
                new Permission { Entity = "Accounts", Operation = "Create"},
                new Permission { Entity = "Accounts", Operation = "Read"},
                new Permission { Entity = "Accounts", Operation = "Update"},
                new Permission { Entity = "Accounts", Operation = "Delete"},

                new Permission { Entity = "Cards", Operation = "Create"},
                new Permission { Entity = "Cards", Operation = "Read"},
                new Permission { Entity = "Cards", Operation = "Update"},
                new Permission { Entity = "Cards", Operation = "Delete"},

                new Permission { Entity = "Transactions", Operation = "Create"},
                new Permission { Entity = "Transactions", Operation = "Read"},
                new Permission { Entity = "Transactions", Operation = "Update"},
                new Permission { Entity = "Transactions", Operation = "Delete"},
            };

            return permissions;
        }

        private List<Permission> SetPermissionClaimsForGuest(User user, ClaimsIdentity identity)
        {
            var permissions = new List<Permission>  { };

            return permissions;
        }



        public ActionResult ForgetPassword()
        {
            User Users = new User();
            return View(Users);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgetPassword(User userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _context.Users
                        .FirstOrDefault(x => x.UserName == userModel.UserName);
                    if (user == null)
                    {
                        //Add logic here to display some message to user    
                        ViewBag.Message = "Account Notfound!";
                        return View("Index", userModel);
                    }
                    else
                    {
                        string password = GeneratePassword.Generate(16, 5);


                        PasswordRecoveryHistory rp = new PasswordRecoveryHistory();
                        rp.SendDate = DateTime.Now;
                        rp.ExpireDate = DateTime.Now.AddMinutes(15);
                        rp.UserId = user.UserId;
                        rp.RecoveryInitialPassword = password;//.PW_Encrypt();

                        _context.Add(rp);
                        _context.SaveChanges();
                        await CommonFunctions.SendMail(user, password);

                        return RedirectToAction("RecoverPassword", "Login");
                    }
                }
                else
                {
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                return View("Index");
            }
        }

        public ActionResult RecoverPassword()
        {
            User Users = new User();
            return View(Users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RecoverPassword(User userModel,string RecoveryPassword)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var PasswordRecoveryHistoriesUser = await _context.PasswordRecoveryHistories
                                .Where(x => x.RecoveryInitialPassword == RecoveryPassword //.PW_Encrypt()
                                                                              && DateTime.Now >= x.SendDate && DateTime.Now <= x.ExpireDate)
                                .FirstOrDefaultAsync();

                    if (PasswordRecoveryHistoriesUser == null)
                    {
                        //Add logic here to display some message to user    
                        ViewBag.Message = "Account Notfound!";
                        return View("Index", userModel);
                    }
                    else
                    {
                        var user = _context.Users.FirstOrDefault(x=>x.UserName==userModel.UserName && x.UserId== PasswordRecoveryHistoriesUser.UserId);
                        if (user == null)
                        {
                            //Add logic here to display some message to user    
                            ViewBag.Message = "Account Notfound!";
                            return View("Index", userModel);
                        }

                        user.LastPasswordChangeDate = DateTime.Now;
                        user.Password = userModel.Password.PW_Encrypt();
                        _context.Update(user);

                        PasswordRecoveryHistoriesUser.IsUsed = "1";
                        _context.Update(PasswordRecoveryHistoriesUser);

                        _context.SaveChanges();

                        return RedirectToAction("Index", "Login");
                    }
                }
                else
                {
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                return View("Index");
            }
        }


        public ActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Login");
        }


    }
}
