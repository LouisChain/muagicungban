using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using muagicungban;
using muagicungban.Models;
using muagicungban.Abstract;
using muagicungban.Entities;
using muagicungban.Repositories;

namespace muagicungban.Controllers
{
    public class UserController : Controller
    {
        // Paging with 30 element per page
        public const int pageSize = 20;

        private IMemberRepository membersRepository;
        private UserRolesRepository userRolesRepository;
        private RolesRepository rolesRepository;
        private ItemsRepository itemsRepository;
        private EmailActiveRepository emailActiveRepository;

        public UserController()
        {
            membersRepository = new MembersRepository(Connection.connectionString);
            userRolesRepository = new UserRolesRepository(Connection.connectionString);
            rolesRepository = new RolesRepository(Connection.connectionString);
            itemsRepository = new ItemsRepository(Connection.connectionString);
            emailActiveRepository = new EmailActiveRepository(Connection.connectionString);
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

        //[Authorize]
        //public ActionResult Active(string id)
        //{
        //    User member = membersRepository.Members.Single(m => m.Username == id);
        //    User user = membersRepository.Members.Single(u => u.Username == HttpContext.User.Identity.Name);
        //    if (user.Roles.Any(r => r.Role.RoleName == "Manager" || r.Role.RoleName == "Admin"))
        //    {
        //        member.IsActive = true;
        //        membersRepository.Save(member);
        //        return Content("Success");
        //    }
        //    return Content("Failed");
        //}

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
        [Authorize]
        public ActionResult Create(FormCollection collection)
        {
            User user = membersRepository.Members.Single(m => m.Username == HttpContext.User.Identity.Name);
            if (user.Roles.Any(r => r.Role.RoleName == "Manager" || r.Role.RoleName == "Admin"))
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
                    member.Password = collection["Password"].md5();
                    member.Phone = collection["Phone"];
                    member.Email = collection["Email"];
                    member.Address = collection["Address"];
                    member.RegisDate = DateTime.Now;
                    member.IsActive = true;
                    member.Birthday = DateTime.Parse(collection["Birthday"]);

                    membersRepository.Save(member);
                    userRolesRepository.DeleteAll(userRolesRepository.UserRoles.Where(m => m.UserID == Username).ToList());
                    userRolesRepository.AddAll(userRoles);
                    return RedirectToAction("List", new { page = 1 });
                }
            }
            return RedirectToAction("Index");
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
            if (mem != null)
            {
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
                    if (collection["Name"] != "")
                        mem.Name = collection["Name"];
                    if (collection["Password"] != "")
                        mem.Password = collection["Password"].md5();
                    if (collection["Phone"] != "")
                        mem.Phone = collection["Phone"];
                    if (collection["Email"] != "")
                        mem.Email = collection["Email"];
                    if (collection["Address"] != "")
                        mem.Address = collection["Address"];
                    if (collection["Birthday"] != "")
                        mem.Birthday = DateTime.Parse(collection["Birthday"]);
                    membersRepository.Save(mem);
                    userRolesRepository.DeleteAll(userRolesRepository.UserRoles.Where(m => m.UserID == Username).ToList());
                    userRolesRepository.AddAll(userRoles);

                }
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
                    TempData["username-error"] = "Tài khoản này đã có người sử dụng";
                    return View(register);
                }
                if (register.Captcha == HttpContext.Session["captchastring"].ToString())
                {
                    if (register.Password != register.ConfirmPassword)
                    {
                        TempData["PasswordNotMatch"] = "Mật khẩu so sánh không khớp, vui lòng nhập lại";
                        return View(register);
                    }
                    else
                    {
                        User user = new User();
                        user.RegisDate = DateTime.Now;
                        user.Username = register.Username;
                        user.Password = register.Password.md5();
                        user.Name = register.Name;
                        user.Email = register.Email;
                        user.Address = register.Address;
                        user.Birthday = register.Birthday;
                        user.Phone = register.Phone;
                        user.IsActive = false;
                        membersRepository.Add(user);

                        UserRoles userRoles = new UserRoles();
                        userRoles.RoleID = rolesRepository.Roles.Single(r => r.RoleName == "Buyer").RoleID;
                        userRoles.UserID = user.Username;
                        userRolesRepository.Add(userRoles);

                        userRoles = new UserRoles();
                        userRoles.RoleID = rolesRepository.Roles.Single(r => r.RoleName == "Seller").RoleID;
                        userRoles.UserID = user.Username;
                        userRolesRepository.Add(userRoles);

                        userRoles = new UserRoles();
                        userRoles.UserID = user.Username;
                        userRoles.RoleID = rolesRepository.Roles.Single(r => r.RoleName == "Bidder").RoleID;
                        userRolesRepository.Add(userRoles);

                        // GENERATE EMAIL ACTIVE INFORMATION
                        string code = Extension.AutoString(10).md5();
                        EmailActive active = new EmailActive();
                        active.Username = user.Username;
                        active.Code = code;
                        emailActiveRepository.Add(active);
                        string message = "Vui lòng click vào đường link dưới đây để thực hiện kích hoạt <br> " +
                                         "<a href=\"http://" + Request.Url.Authority + "/user/active/" + user.Username + "?code="
                                         + active.Code + "\" >http://" + Request.Url.Authority + "/user/active/" + user.Username + "?code="
                                         + active.Code + "</a>";
                        Extension.SendEmail(user.Email, "Thông tin kích hoạt tài khoản", message);

                        ViewData["message"] = "Đăng ký tài khoản thành công, hệ thống sẽ gửi email kích hoạt tài khoản trong giây lát" +
                                              ". Vui lòng kiểm tra email có địa chỉ " + user.Email + "để kích hoạt";
                        return View("RegisterMessage");
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
                if (!membersRepository.Members.Any(m => m.Username == model.Username && m.Password == model.Password.md5()))
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

                    // For showing number of win items
                    int i = 0;
                    foreach (var item in itemsRepository.Items.Where(a => a.EndDate < DateTime.Now))
                        if (item.CurUser == model.Username)
                            i++;
                    HttpContext.Session.Add("Win", i);

                    // For showing number of join items
                    i = 0;
                    foreach (var item in itemsRepository.Items)
                        if (item.Bids.Any(b => b.BidderID == model.Username))
                            i++;
                    HttpContext.Session.Add("Join", i);

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

        public ActionResult Active(string id, string code)
        {
            if (emailActiveRepository.EmailActives.Any(e => e.Username == id))
            {
                EmailActive active = emailActiveRepository.EmailActives.Single(i => i.Username == id);
                if (active.Code == code)
                {
                    User user = membersRepository.Members.Single(m => m.Username == id);
                    user.IsActive = true;
                    membersRepository.Save(user);
                    emailActiveRepository.Delete(active);
                    ViewData["message"] = "Kích hoạt thành công, bạn đã có thể thực hiện đăng nhập";
                    return View("RegisterMessage");
                }
            }
            else
            {
                User user = membersRepository.Members.Single(m => m.Username == id);
                if (user.IsActive)
                {
                    ViewData["message"] = "Tài khoản đã được kích hoạt trước đó, bạn không cần kích hoạt nữa";
                    return View("RegisterMessage");
                }
            }
            return Content("Kích hoạt thất bại!!!");
        }
    }

}
