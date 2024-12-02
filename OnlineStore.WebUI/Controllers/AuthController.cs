using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.WebUI.Models;
using System.Security.Claims;
using OnlineStore.Business.Dtos;
using OnlineStore.Business.AbstractServices;
using OnlineStore.Business.Types;
using AutoMapper;

namespace OnlineStore.WebUI.Controllers
{
    //Kimlik Doğrulama(Authentication) ve  Yetkilendirme (Authorization)
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("KayitOl")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("{controller}/{action}")]
        [Route("KayitOl")]
        public async Task<IActionResult> Register(RegisterViewModel formData)
        {
            //Validation, forma girilen veriler uygun mu kontrolü
            if (!ModelState.IsValid)
            {
                return View(formData);
            }

            var userDto = _mapper.Map<UserAddDto>(formData);
           
            var result = await  _userService.AddUser(userDto);

            if (result.IsSucceed)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = result.Message;
                return View(formData);
            }


        }


        public async Task<IActionResult> Login(LoginViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            var loginDto = new UserLoginDto()
            {
                Email = formData.Email,
                Password = formData.Password
            };

            var userInfo = await _userService.LoginUser(loginDto);

            if (userInfo is null)
            {
               
                return RedirectToAction("Index", "Home");
            }

            // Buraya kadar geldiyse demek ki oturum açabilirim.
            HttpContext.Session.SetInt32("id", userInfo.Id);
            var claims = new List<Claim>();

            claims.Add(new Claim("id", userInfo.Id.ToString()));
            claims.Add(new Claim("email", userInfo.Email));
            claims.Add(new Claim("firstName", userInfo.FirstName));
            claims.Add(new Claim("lastName", userInfo.LastName));
            claims.Add(new Claim("userType", userInfo.UserType.ToString()));

            // YETKILENDIRME (AUTHORIZATION ICIN) GEREKLI OLAN ALTTAKI KOD
            claims.Add(new Claim(ClaimTypes.Role, userInfo.UserType.ToString())); 
            // ClaimTypes.Role -> .net içerisinde authorization mekanizması ile paralel çalışacak.

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var autProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = new DateTimeOffset(DateTime.Now.AddHours(48))//48 saat kalıcı olsun oturum
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimIdentity), autProperties);

            TempData["SuccessMessage"] = "Kullanıcı girişi başarılı.";


            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            TempData["SuccessMessage"] = "Oturum sonlandırıldı.";
            return RedirectToAction("Index", "Home");
        }

      
    }
}
