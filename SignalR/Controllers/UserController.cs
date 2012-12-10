using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using muagicungban.Models;
using muagicungban.Abstract;
using muagicungban.Entities;
using muagicungban.Repositories;

namespace muagicungban.Controllers
{
    public class UserController : Controller
    {
        // Paging with 30 element per page
        public const int pageSize = 30;

        private IMemberRepository membersRepository;
        private UserRolesRepository userRolesRepository;
        private RolesRepository rolesRepository;

        public UserController()
        {
            membersRepository = new MembersRepository(Connection.connectionString);
            userRolesRepository = new UserRolesRepository(Connection.connectionString);
            rolesRepository = new RolesRepository(Connection.connectionString);
        }
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ViewResult List(int page)
        {
            User user = membersRepository.Members.Single(u => u.Username == HttpContext.User.Identity.Name);
            List<User> userList =  new List<User>();

            // DO NOT LIST ADMIN, MANAGER ACCOUNT IF USER HAVE ONLY ROLE MANAGER
            if (user.Roles.Any(r => r.Role.RoleName == "Manager"))
            {
                foreach (var mem in membersRepository.Members.ToList())
                {
                    if (!mem.Roles.Any(r => r.Role.RoleName == "Manager" || r.Role.RoleName == "Admin"))
                        userList.Add(mem);
                }
            }

            // DO NOT LIST ADMIN ACCOUNT IF USER ARE ADMIN
            if (user.Roles.Any(r => r.Role.RoleName == "Admin"))
            {
                foreach (var mem in membersRepository.Members.ToList())
                {
                    if (!mem.Roles.Any(r => r.Role.RoleName == "Admin")) 
                        userList.Add(mem);
                }
                //userList = membersRepository.Members.Where(m => !m.Roles.Any(_r => _r.Role.RoleName == "Admin")).ToList();
            }
            

            ViewData["pageSize"] = pageSize;
            ViewData["totalItems"] = userList.Count();
            ViewData["currentPage"] = page;
            return View(userList.Skip((page - 1) * pageSize).Take(pageSize).ToList());
        }

        [Authorize]
        public ActionResult Active(string id)
        {
            User member = membersRepository.Members.Single(m => m.Username == id);
            User user = membersRepository.Members.Single(u => u.Username == HttpContext.User.Identity.Name);
            if (user.Roles.Any(r => r.Role.RoleName == "Manager" || r.Role.RoleName == "Admin"))
            {
                member.IsActive = true;
                membersRepository.Save(member);
                return Content("Success");
            }
            return Content("Failed");
        }

        //
        // GET: /User/Details/5
        [Authorize]
        public ActionResult Details(string id)
        {
            User member = membersRepository.Members.Single(m => m.Username == id);
            User user = membersRepository.Members.Single(m => m.Username == HttpContext.User.Identity.Name);

            if (user.Username == member.Username || user.Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
            {
                return View(member);
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /User/Create

        [Authorize]
        public ActionResult Create()
        {
            User user = membersRepository.Members.Single(u => u.Username == HttpContext.User.Identity.Name);
            if (user.Roles.Any(r => r.Role.RoleName == "Manager" || r.Role.RoleName == "Admin"))
            {
                List<Role> roles = new List<Role>();
                if (user.Roles.Any(r => r.Role.RoleName == "Manager"))
                    roles = rolesRepository.Roles.Where(r => r.RoleName != "Admin" && r.RoleName != "Manager").ToList();
                if (user.Roles.Any(r => r.Role.RoleName == "Admin"))
                    roles = rolesRepository.Roles.Where(r => r.RoleName != "Admin").ToList();

                ViewData["roleChkBox"] = roles;
                return View(new User());
            }
            return RedirectToAction("index");
        }
        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            string Username = collection["Username"];
            {
                var roldIDs = collection["role"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                List<UserRoles> userRoles = new List<UserRoles>();
                if (roldIDs != null)
                    foreach (var item in roldIDs)
                    {
                        UserRoles _roles = new UserRoles();
                        _roles.RoleID = int.Parse(item);
                        _roles.UserID = Username;
                        userRoles.Add(_roles);
                    }
                User member = new User();
                member.Username = Username;
                member.Name = collection["Name"];
                member.Password = collection["Password"];
                member.Phone = collection["Phone"];
                member.Email = collection["Email"];

                membersRepository.Save(member);
                userRolesRepository.DeleteAll(userRolesRepository.UserRoles.Where(m => m.UserID == Username).ToList());
                userRolesRepository.AddAll(userRoles);
                return RedirectToAction("List", new { page = 1 });
            }
        }
        
        //
        // GET: /User/Edit/5
        [Authorize]
        public ActionResult Edit(string Username)
        {
            try
            {
                User member = new User();
                if (membersRepository.Members.Any(m => m.Username == Username))
                    member = membersRepository.Members.Single(m => m.Username == Username);
                User user = membersRepository.Members.Single(u => u.Username == HttpContext.User.Identity.Name);
                if (user.Username == member.Username || user.Roles.Any(r => r.Role.RoleName == "Manager" || r.Role.RoleName == "Admin"))
                {
                    List<Role> roles = new List<Role>();
                    if (user.Roles.Any(r => r.Role.RoleName == "Manager"))
                        roles = rolesRepository.Roles.Where(r => r.RoleName != "Admin" && r.RoleName != "Manager").ToList();
                    if (user.Roles.Any(r => r.Role.RoleName == "Admin"))
                        roles = rolesRepository.Roles.Where(r => r.RoleName != "Admin").ToList();

                    ViewData["roleChkBox"] = roles;
                    ViewData["roleChecked"] = new List<UserRoles>(userRolesRepository.UserRoles.Where(r => r.UserID == Username).ToList());
                    return View(member);
                }
                else
                    this.Edit(HttpContext.User.Identity.Name);
                return RedirectToAction("Edit", new { Username = HttpContext.User.Identity.Name });
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /User/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(string Username, FormCollection collection)
        {
            ////try
            User mem = membersRepository.Members.Single(m => m.Username == Username);
            User user = membersRepository.Members.Single(u => u.Username == HttpContext.User.Identity.Name);
            if (user.Username == mem.Username || user.Roles.Any(r => r.Role.RoleName == "Manager" || r.Role.RoleName == "Admin"))
            {
                var roldIDs = collection["role"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                List<UserRoles> userRoles = new List<UserRoles>();
                if (roldIDs != null)
                    foreach (var item in roldIDs)
                    {
                        UserRoles _roles = new UserRoles();
                        _roles.RoleID = int.Parse(item);
                        _roles.UserID = Username;
                        userRoles.Add(_roles);
                    }
                User member = new User();
                member.Username = Username;
                member.Name = collection["Name"];
                member.Password = collection["Password"];
                member.Phone = collection["Phone"];
                member.Email = collection["Email"];
                membersRepository.Save(member);
                userRolesRepository.DeleteAll(userRolesRepository.UserRoles.Where(m => m.UserID == Username).ToList());
                userRolesRepository.AddAll(userRoles);
                
            }
            return RedirectToAction("List", new { page = 1 });
            //catch
            //{
            //    return RedirectToAction("Index");
            //}
        }

        //
        // GET: /User/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /User/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public CaptchaImageResult ShowCaptchaImage()
        {
            return new CaptchaImageResult();
        }

        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Register register)
        {
            if (ModelState.IsValid)
            {
                if (membersRepository.Members.Any(m => m.Username == register.Username))
                {
                    TempData["username-error"] = "Username already be used, try other";
                    return View(register);
                }
                if (register.Captcha == HttpContext.Session["captchastring"].ToString())
                {
                    if (register.Password != register.ConfirmPassword)
                    {
                        TempData["PasswordNotMatch"] = "Confirm password didn't match, please re try";
                        return View(register);
                    }
                    else
                    {
                        User user = new User();
                        user.RegisDate = DateTime.Now;
                        user.Username = register.Username;
                        user.Password = register.Password;
                        user.Name = register.Name;
                        user.Email = register.Email;
                        user.Address = register.Address;
                        user.Birthday = register.Birthday;
                        user.Phone = register.Phone;
                        user.IsActive = false;
                        membersRepository.Add(user);
                        return Redirect(Url.Action("Index","Item"));
                    }
                }
                else
                {
                    TempData["WrongCaptcha"] = "Wrong captcha value, re enter it";
                    Register _temp = new Register();
                    _temp.Username = register.Username;
                    _temp.Password = register.Password;
                    _temp.Name = register.Name;
                    _temp.Email = register.Email;
                    _temp.Address = register.Address;
                    _temp.Birthday = register.Birthday;
                    _temp.Phone = register.Phone;
                    _temp.Captcha = "";
                    return View(_temp);
                }
            }
            else
                return View(register);
        }

        public ActionResult LogOn()
        {
            if (!Request.IsAuthenticated)
                return View();
            else
                return RedirectToAction("SignOut");
        }

        [HttpPost]
        public ActionResult LogOn(LogOn model, string returnUrl)
        {
            User member = membersRepository.Members.Single(m => m.Username == model.Username);
            if (ModelState.IsValid)
            {
                if (!Membership.ValidateUser(model.Username, model.Password))
                    ModelState.AddModelError("", "Thông tin đăng nhập không đúng.!!!");
                else if (!member.IsActive)
                {
                    ModelState.AddModelError("", "Tài khoản đang chờ được kích hoạt, Vui lòng kích hoạt..!!!");
                }
                else if (member.IsBan)
                {
                    ModelState.AddModelError("", "Tài khoản của bạn đã bị khóa, liên hệ admin để biết thêm.!!!");
                }                
                else
                {

                    FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                    HttpContext.Session.Add("Roles", member.Roles);
                    HttpContext.Session.Add("Profile", member);
                    return Redirect(returnUrl ?? Url.Action("Index", "Item"));
                }
            }
            return View(model);
        }

        public ViewResult SignOut()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("http://" + Request.Url.Authority);
        }

        [Authorize]
        public ActionResult CheckBan(string id, string isban)
        {
            User employee = membersRepository.Members.Single(m => m.Username == HttpContext.User.Identity.Name);
            User user = membersRepository.Members.Single(m => m.Username == id);
            List<Role> userRoles = new List<Role>();
            List<Role> employeeRoles = new List<Role>();

            foreach (UserRoles _role in user.Roles)
            {
                userRoles.Add(_role.Role);
            }

            foreach (UserRoles _role in employee.Roles)
            {
                employeeRoles.Add(_role.Role);
            }

            if (userRoles.Any(r => r.RoleName == "Admin")) return Content("");
            if (userRoles.Any(r => r.RoleName == "Manager") && !employeeRoles.Any(e => e.RoleName == "Admin")) return Content("");
            if (employeeRoles.Any(r => r.RoleName == "Admin" || r.RoleName == "Manager"))
            {
                if (isban != null)
                {
                    user.IsBan = true;
                }
                else
                    user.IsBan = false;
                membersRepository.Save(user);
            }
            return Content("");
        }

        [Authorize]
        public ActionResult CheckActive(string id)
        {
            User employee = membersRepository.Members.Single(m => m.Username == HttpContext.User.Identity.Name);
            User user = membersRepository.Members.Single(m => m.Username == id);
            List<Role> userRoles = new List<Role>();
            List<Role> employeeRoles = new List<Role>();
            foreach (UserRoles _role in user.Roles)
            {
                userRoles.Add(_role.Role);
            }

            foreach (UserRoles _role in employee.Roles)
            {
                employeeRoles.Add(_role.Role);
            }
            if (userRoles.Any(r => r.RoleName == "Admin")) return Content("");
            if (userRoles.Any(r => r.RoleName == "Manager") && !employeeRoles.Any(e => e.RoleName == "Admin")) return Content("");
            if (employeeRoles.Any(r => r.RoleName == "Admin" || r.RoleName == "Manager"))
            {
                user.IsActive = true;
                membersRepository.Save(user);
            }
            return Content("");
        }
    }

}
