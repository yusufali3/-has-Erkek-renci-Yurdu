using System.Diagnostics;
using ihlaserkekogrenciyurdu.Models;
using Microsoft.AspNetCore.Mvc;

namespace ihlaserkekogrenciyurdu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly YurtDbContext _context; // DbContext tan�mland�

        public HomeController(ILogger<HomeController> logger, YurtDbContext context)
        {
            _logger = logger;
            _context = context; // DbContext constructor �zerinden al�nd�
        }

        public IActionResult Index()
        {
            var kullanicilar = _context.Kullanicilar.ToList(); // Veritaban�ndan t�m kullan�c�lar� �ekiyoruz
            return View(kullanicilar); // View'a kullan�c� verilerini g�nderiyoruz
        }

        public IActionResult Profil(int id)
        {
            // Veritaban�ndan id'ye g�re kullan�c�y� �ekiyoruz
            var kullanici = _context.Kullanicilar.FirstOrDefault(k => k.Id == id);

            if (kullanici == null)
            {
                return NotFound("Kullan�c� bulunamad�.");
            }

            // Kullan�c� bilgilerini profil.cshtml'e g�nderiyoruz
            return View(kullanici);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string sifre)
        {
            // Veritaban�ndan email ve �ifreye g�re kullan�c�y� buluyoruz
            var kullanici = _context.Kullanicilar.FirstOrDefault(k =>
                k.Email.ToLower().Trim() == email.ToLower().Trim() &&
                k.Sifre.Trim() == sifre.Trim());

            if (kullanici == null)
            {
                // Kullan�c� bulunamad�
                ViewBag.Mesaj = "Kullan�c� bulunamad�. Email veya �ifre hatal� olabilir.";
                return View();
            }

            // Kullan�c� bulundu, profil sayfas�na y�nlendirme
            return RedirectToAction("Profil", new { id = kullanici.Id });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Gallery()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Choose()
        {
            return View();
        }

        public IActionResult Sign_Up()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Sign_Up(string ad, string soyad, string email, string sifre, DateTime dogumtarihi, string telno)
        {
            // Yeni kullan�c� olu�tur
            var yeniKullanici = new Kullanicilar
            {
                Ad = ad,
                Soyad = soyad,
                Email = email,
                Sifre = sifre,
                DogumTarihi = dogumtarihi,
                TelNo = telno,
                Bolum = "Bilinmiyor" // Varsay�lan olarak B�l�m eklenebilir
            };

            // Veritaban�na ekle
            _context.Kullanicilar.Add(yeniKullanici);
            _context.SaveChanges();

            // Kay�t sonras� profil sayfas�na y�nlendirme
            return RedirectToAction("Profil", new { id = yeniKullanici.Id });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
