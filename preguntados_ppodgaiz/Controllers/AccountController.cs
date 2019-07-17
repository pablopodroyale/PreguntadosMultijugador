using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using preguntados_ppodgaiz.Models;
using preguntados_ppodgaiz.Models.Dominio;
using preguntados_ppodgaiz.Repositorio;
using preguntados_ppodgaiz.Models.Enums;
using preguntados_ppodgaiz.Models.ViewModels.Usuario;
using System.Collections.Generic;

namespace preguntados_ppodgaiz.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private const string Password = "123456";
        private ApplicationDbContext db;

        public AccountController()
        {
            this.db = new ApplicationDbContext();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Create()
        {
            UsuarioABMViewModel model = new UsuarioABMViewModel();
            return View(model);
        }

        [Authorize(Roles = RoleConst.Administrador)]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CheckExistingEmail(string Email)
        {
            try
            {
                return Json(!IsEmailExists(Email));
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        private bool IsEmailExists(string email)
        {
             return UserManager.FindByEmail(email) != null;
        }
     

        [Authorize(Roles = RoleConst.Administrador)]
        [HttpPost]
        public ActionResult Create(UsuarioABMViewModel model)
            {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

                var usuario = new Usuario()
                {
                    ApplicationUser = user,

                    NickName = model.Email,

                    Nombre = model.Nombre,

                    EMail = model.Email
                };
                var db = new ApplicationDbContext();
                new Repositorio<Usuario>(db).Crear(usuario);
               
                UserManager.AddPassword(usuario.ApplicationUser.Id, Password);

                foreach (var item in model.RolesSeleccionados)
                {
                    UserManager.AddToRole(usuario.ApplicationUser.Id, item.ToString());
                }
                //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                return RedirectToAction("Index", "Account");
            }

            return View("Create", model);

        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    var usuario = new Repositorio<Usuario>(db).TraerTodos().Where(u => u.ApplicationUser.UserName == model.Email).FirstOrDefault();
                    var rolename = UserManager.GetRoles(usuario.ApplicationUser.Id).FirstOrDefault().ToString();
                    if (rolename == RoleConst.Suscriptor)
                    {
                        return RedirectToAction("Lobby","Lobby",usuario);
                    }
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email};

                var usuario = new Usuario()
                {
                    ApplicationUser = user,
                    NickName = model.NickName,
                    Nombre = model.Email
                    
                };
                var db = new ApplicationDbContext();
                new Repositorio<Usuario>(db).Crear(usuario);

                UserManager.AddPassword(usuario.ApplicationUser.Id, model.Password);
               
                UserManager.AddToRole(usuario.ApplicationUser.Id, RoleConst.Suscriptor);

                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                var usuarioCreado = new Repositorio<Usuario>(db).TraerTodos().Where(u => u.ApplicationUser.UserName == model.Email).FirstOrDefault();
                var rolename = UserManager.GetRoles(usuarioCreado.ApplicationUser.Id).FirstOrDefault().ToString();
                if (rolename == RoleConst.Suscriptor)
                {
                    return RedirectToAction("Lobby", "Lobby", usuarioCreado);
                }
                return RedirectToAction("Lobby", "Lobby");
            }

            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        public ActionResult GetData()
        {
            string userid = HttpContext.User.Identity.Name;
            var nombre = Request.QueryString["Nombre"];
            var nickName = Request.QueryString["NickName"];
            var eMail = Request.QueryString["EMail"];
            var rol = Request.QueryString["Rol"];

            var start = Convert.ToInt32(Request.QueryString["start"]);
            var cantidad = Convert.ToInt32(Request.QueryString["length"]);
            var orderColumn = Request.QueryString["order[0][column]"];
            var orderDir = Request.QueryString["order[0][dir]"];

            var query = new Repositorio<Usuario>(db).TraerTodos().Where(u => u.EMail !=  userid);
            var cantidadTotal = query.Count();

            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(p => p.Nombre.Contains(nombre)).OrderBy(p => p.Nombre);
            }

            if (!string.IsNullOrEmpty(nickName))
            {
                query = query.Where(p => p.NickName.Contains(nickName)).OrderBy(p => p.NickName);
            }

            if (!string.IsNullOrEmpty(eMail))
            {
                query = query.Where(p => p.EMail.Contains(eMail)).OrderBy(p => p.EMail);
            }
            //if (!string.IsNullOrEmpty(rol))
            //{
            //    //query = query.Where(p => p.rol.Contains(rol)).OrderBy(p => p.EMail);
            //    _userManager.GetRoles
            //}

            var cantidadFiltradas = query.Count();

            if (orderDir == "asc")
            {
                if (orderColumn == "2")
                {
                    query = query.OrderBy(r => r.EMail);
                }

                else if (orderColumn == "1")
                {
                    query = query.OrderBy(r => r.NickName);
                }
                else
                    query = query.OrderBy(r => r.Nombre);
            }
            else
            {
                if (orderColumn == "2")
                {
                    query = query.OrderBy(r => r.EMail);
                }

                else if (orderColumn == "1")
                {
                    query = query.OrderBy(r => r.NickName);
                }
                else
                    query = query.OrderBy(r => r.Nombre);
            }

            var data = query.Skip(start).Take(cantidad).ToList().Select(p => new
            {
                Id = p.Id,
                Nombre = p.Nombre,
                NickName = p.NickName,
                EMail = p.EMail,
                Roles = UserManager.GetRolesAsync(p.ApplicationUser.Id).Result.ToList()
            }).ToList();

            var result = new { data = data, recordsTotal = cantidadTotal, recordsFiltered = cantidadTotal };

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        public ActionResult Delete(Guid id)
        {
            var errores = new List<string>();
            var repo = new Repositorio<Usuario>(db);
            repo.Eliminar(repo.Traer(id));
            if (errores.Any())
            {
                return Json(new { Result = "", Error = errores }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = "OK", Error = "" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Edit(Guid id)
        {
           
            var usuario = new Repositorio<Usuario>(db).Traer(id);
            //var apUser = new ApplicationUser { UserName = usuario.EMail, Email = usuario.EMail };
            ApplicationUserManager user = new ApplicationUserManager(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(db));
            var model = new UsuarioABMViewModel(usuario,user);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditAsync(UsuarioABMViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = new Repositorio<Usuario>(db);
                var usuario = repo.Traer(model.Id);
                usuario.Modificar(model);
                repo.Modificar(usuario);
                var roles = await UserManager.GetRolesAsync(usuario.ApplicationUser.Id);
                await UserManager.RemoveFromRolesAsync(usuario.ApplicationUser.Id, roles.ToArray());
                foreach (var item in model.RolesSeleccionados)
                {
                    UserManager.AddToRole(usuario.ApplicationUser.Id, item.ToString());
                }
                //UserManager.RemovePassword(usuario.ApplicationUser.Id);
                //UserManager.AddPassword(usuario.ApplicationUser.Id, model.PassWord);
                return RedirectToAction("Index");
            }
            return View(model);
        }



        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}