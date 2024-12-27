using System.Diagnostics;
using ihlaserkekogrenciyurdu.Models;
using Microsoft.AspNetCore.Mvc;

namespace ihlaserkekogrenciyurdu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly YurtDbContext _context; // DbContext tanýmlandý

        public HomeController(ILogger<HomeController> logger, YurtDbContext context)
        {
            _logger = logger;
            _context = context; // DbContext constructor üzerinden alýndý
        }

        public IActionResult Index()
        {
            var kullanicilar = _context.Kullanicilar.ToList(); // Veritabanýndan tüm kullanýcýlarý çekiyoruz
            return View(kullanicilar); // View'a kullanýcý verilerini gönderiyoruz
        }

        public IActionResult Profil(int id)
        {
            // Veritabanýndan id'ye göre kullanýcýyý çekiyoruz
            var kullanici = _context.Kullanicilar.FirstOrDefault(k => k.Id == id);

            if (kullanici == null)
            {
                return NotFound("Kullanýcý bulunamadý.");
            }

            // Kullanýcý bilgilerini profil.cshtml'e gönderiyoruz
            return View(kullanici);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string sifre)
        {
            // Veritabanýndan email ve þifreye göre kullanýcýyý buluyoruz
            var kullanici = _context.Kullanicilar.FirstOrDefault(k =>
                k.Email.ToLower().Trim() == email.ToLower().Trim() &&
                k.Sifre.Trim() == sifre.Trim());

            if (kullanici == null)
            {
                // Kullanýcý bulunamadý
                ViewBag.Mesaj = "Kullanýcý bulunamadý. Email veya þifre hatalý olabilir.";
                return View();
            }

            // Kullanýcý bulundu, profil sayfasýna yönlendirme
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
            // Yeni kullanýcý oluþtur
            var yeniKullanici = new Kullanicilar
            {
                Ad = ad,
                Soyad = soyad,
                Email = email,
                Sifre = sifre,
                DogumTarihi = dogumtarihi,
                TelNo = telno,
                Bolum = "Bilinmiyor" // Varsayýlan olarak Bölüm eklenebilir
            };

            // Veritabanýna ekle
            _context.Kullanicilar.Add(yeniKullanici);
            _context.SaveChanges();

            // Kayýt sonrasý profil sayfasýna yönlendirme
            return RedirectToAction("Profil", new { id = yeniKullanici.Id });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
