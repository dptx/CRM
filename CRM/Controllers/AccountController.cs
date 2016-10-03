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
using CRM.Models;
using CRM.DataAccess.Models;
using AutoMapper;
using CRM.DataAccess.Repos;

namespace CRM.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private static IMapper mapper = AutoMapperConfig.Instance.Mapper;

        private IAuthenticationManager Authentication
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        public AccountController()
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var accountID = AccountRepo.Authenticate(new Account { Email = model.Email, Password = model.Password });

                if (accountID > 0)
                {
                    var identity = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.Name, model.Email),
                            new Claim("ID", accountID.ToString())
                        },
                        DefaultAuthenticationTypes.ApplicationCookie,
                        ClaimTypes.Name, ClaimTypes.Role);

                    Authentication.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe
                    }, identity);

                    return RedirectToAction("index", "customer");
                }
            }

            return View(model);
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
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
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View(new AccountModel());
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(AccountModel model)
        {
            if (ModelState.IsValid)
            {
                var accountID = AccountRepo.Register(mapper.Map<Account>(model));

                if (accountID == 0)
                {
                    if(! AccountRepo.IsEmailAvailable(model.Email))
                    {
                        ModelState["Email"].Errors.Add("Email address already in use");
                    }
                    else
                    {
                        model.Error = "Error attempting registration";
                    }
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }

            return View(model);
        }

        public JsonResult IsEmailAvailable(string email)
        {
            if(AccountRepo.IsEmailAvailable(email))
                return Json(true, JsonRequestBehavior.AllowGet);

            return Json("Email already in use", JsonRequestBehavior.AllowGet);
        }
        
    }
}