using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace YemekApp.Controllers
{
    public class HesaplarController : Controller
    {
        private readonly IHesapService _hesapService;

        private readonly IUlkeService _ulkeService;
        private readonly ISehirService _sehirService;

        public HesaplarController(IHesapService hesapService, IUlkeService ulkeService, ISehirService sehirService)
        {
            _hesapService = hesapService;
            _ulkeService = ulkeService;
            _sehirService = sehirService;
        }

        public IActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Giris(KullaniciGirisModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _hesapService.Giris(model);
                if (result.IsSuccessful)
                {
                    //Demo için her admin kullanıcısı girişi yapıldığında verilerin sıfırlanması
                    //if (result.Data.RolId == (int)Business.Enums.Rol.Admin)
                    //    Seed();

                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, result.Data.Adi),
                        new Claim(ClaimTypes.Surname, result.Data.Soyadi),
                        new Claim(ClaimTypes.userName result.Data.KullaniciAdi),
                        new Claim(ClaimTypes.Role, result.Data.RolAdiDisplay),
                        new Claim(ClaimTypes.Email, result.Data.KullaniciDetayi.Eposta), // istenirse Email gibi claim tipleri üzerinden kullanıcının e-posta'sı gibi claim'ler eklenebilir
                        new Claim(ClaimTypes.Sid, result.Data.Id.ToString())
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(model);
        }

        public async Task<IActionResult> Cikis()
        {
            await HttpContent.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
