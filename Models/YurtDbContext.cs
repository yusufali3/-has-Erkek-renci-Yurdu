using Microsoft.EntityFrameworkCore;

namespace ihlaserkekogrenciyurdu.Models
{
    public class YurtDbContext : DbContext
    {
        public YurtDbContext(DbContextOptions<YurtDbContext> options) : base(options) { }

        // Tablolar için DbSet tanımları
        public DbSet<Kullanicilar> Kullanicilar { get; set; }
    }

    public class Kullanicilar
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Soyad { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Sifre { get; set; } = string.Empty;
        public DateTime DogumTarihi { get; set; } = DateTime.MinValue;
        public string Bolum { get; set; } = string.Empty;
        public string TelNo { get; set; } = string.Empty;

    }
}