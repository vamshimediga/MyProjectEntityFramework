using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EntitiesViewModel;
using BusinessLayer;
using Entities;
using AutoMapper;


namespace MyProjectEntity.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILoginLogRepository _logRepo;
        private readonly IMapper _mapper;
        public AccountController(ILoginLogRepository logRepo,IMapper mapper)
        {
            _logRepo = logRepo;
            _mapper = mapper;
        }
        public async Task<IActionResult> Login()
        {
            //// ✅ Get IP Address & User-Agent
            //string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            //string userAgent = Request.Headers["User-Agent"].ToString();
            // ✅ Capture IP Address
            //var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            //// ✅ Capture User Agent (Browser + OS info)
            //var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();

            //// ✅ Optional: Capture Windows Username (for intranet apps)
            //var windowsUsername = Environment.UserName; // Only if app is hosted in intranet and Windows Authentication is enabled

            //// ✅ Log to DB
            //LoginLogViewModel logvm = new LoginLogViewModel
            //{
            //    LogId = "nologinid",
            //    Username = windowsUsername,
            //    IPAddress = ipAddress,
            //    UserAgent = userAgent,
            //    LoginTime = DateTime.Now
            //};
            //LoginLog loginLog = _mapper.Map<LoginLog>(logvm);
            //await _logRepo.LogLoginAsync(loginLog);
            // ✅ If user is already logged in, redirect to Home page
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (login.Username == "mediga.vamshi@gmail.com" && login.Password == "password")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login.Username),
                    new Claim(ClaimTypes.Role, "Admin") // ✅ Assign "Admin" role
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);




                // ✅ Get IP Address & User-Agent
                string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                string userAgent = Request.Headers["User-Agent"].ToString();

                // ✅ Log to DB
                LoginLogViewModel logvm = new LoginLogViewModel
                {
                    LogId= login.Username,
                    Username = login.Username,
                    IPAddress = ipAddress,
                    UserAgent = userAgent,
                    LoginTime = DateTime.Now
                };
                LoginLog loginLog = _mapper.Map<LoginLog>(logvm);
                await _logRepo.LogLoginAsync(loginLog);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "Invalid username or password";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
