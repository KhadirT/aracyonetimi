using System;
using System.Collections.Generic;

namespace AracYonetimSistemi
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Giriş Türünü Seçiniz:");
                Console.WriteLine("1 - Kullanıcı Girişi");
                Console.WriteLine("2 - Admin Girişi");
                Console.WriteLine("q - Çıkış");
                Console.Write("Seçiminizi yapınız (1/2/q): ");
                var girisSecimi = Console.ReadLine();

                if (girisSecimi == "1")
                {
                    KullaniciArayuzu();
                }
                else if (girisSecimi == "2")
                {
                    if (AdminGiris())
                    {
                        AdminArayuzu();
                    }
                    else
                    {
                        Console.WriteLine("Admin girişi başarısız. Kullanıcı olarak devam ediliyor.");
                        KullaniciArayuzu();
                    }
                }
                else if (girisSecimi == "q")
                {
                    Console.WriteLine("Programdan çıkılıyor.");
                    return; // Programdan çık
                }
                else
                {
                    Console.WriteLine("Geçersiz seçim, lütfen tekrar deneyin.");
                }
            }
        }

        static bool AdminGiris()
        {
            Console.Write("Kullanıcı Adı: ");
            string kullaniciAdi = Console.ReadLine();
            Console.Write("Şifre: ");
            string sifre = Console.ReadLine();

            // Admin giriş
            return (kullaniciAdi == "KadirT" && sifre == "221120231036");
        }

        static void AdminArayuzu()
        {
            AracYonetimi aracYonetimi = new AracYonetimi();

            while (true)
            {
                AdminYapilacaklarListesi();
                var secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        AracEkle(aracYonetimi);
                        break;

                    case "2":
                        AracSil(aracYonetimi);
                        break;

                    case "3":
                        aracYonetimi.AracListele();
                        break;

                    case "q":
                        Console.WriteLine("Admin panelinden çıkılıyor.");
                        return;

                    default:
                        Console.WriteLine("Geçersiz seçim, lütfen tekrar deneyin.");
                        break;
                }
            }
        }

        static void KullaniciArayuzu()
        {
            AracYonetimi aracYonetimi = new AracYonetimi();
            aracYonetimi.AracEkle(new Araba("Tesla", 5000000));
            aracYonetimi.AracEkle(new Kamyonet("BMC", 10000000));

            while (true)
            {
                KullaniciYapilacaklarListesi();
                var secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        aracYonetimi.AracListele();
                        break;

                    case "2":
                        AracKirala(aracYonetimi);
                        break;

                    case "3":
                        AracSatınAl(aracYonetimi);
                        break;

                    case "4":
                        aracYonetimi.LoglariGoster();
                        break;

                    case "q":
                        Console.WriteLine("Kullanıcı panelinden çıkılıyor.");
                        return;

                    default:
                        Console.WriteLine("Geçersiz seçim, lütfen tekrar deneyin.");
                        break;
                }
            }
        }

        static void AdminYapilacaklarListesi()
        {
            Console.WriteLine("\nAdmin Yapılacaklar Listesi:");
            Console.WriteLine("1 - Araç Ekle");
            Console.WriteLine("2 - Araç Sil");
            Console.WriteLine("3 - Araçları Listele");
            Console.WriteLine("4 - Logları Göster");
            Console.WriteLine("q - Çıkış");
            Console.Write("Seçiminizi yapınız: ");
        }

        static void KullaniciYapilacaklarListesi()
        {
            Console.WriteLine("\nKullanıcı Yapılacaklar Listesi:");
            Console.WriteLine("1 - Araçları Listele");
            Console.WriteLine("2 - Araç Kirala");
            Console.WriteLine("3 - Araç Satın Al");
            Console.WriteLine("4 - Logları Göster");
            Console.WriteLine("q - Çıkış");
            Console.Write("Seçiminizi yapınız: ");
        }

        static void AracEkle(AracYonetimi aracYonetimi)
        {
            Console.Write("Araç Türü (1: Araba, 2: Kamyonet): ");
            int tür = Convert.ToInt32(Console.ReadLine());

            Console.Write("Araç Modeli: ");
            string model = Console.ReadLine();

            Console.Write("Araç Fiyatı: ");
            double fiyat = Convert.ToDouble(Console.ReadLine());

            Arac arac = null;
            if (tür == 1)
                arac = new Araba(model, fiyat);
            else if (tür == 2)
                arac = new Kamyonet(model, fiyat);

            if (arac != null)
            {
                aracYonetimi.AracEkle(arac);
                Console.WriteLine("Araç eklendi.");
            }
        }

        static void AracSil(AracYonetimi aracYonetimi)
        {
            Console.Write("Silinecek Araç ID: ");
            int silinecekId = Convert.ToInt32(Console.ReadLine());
            aracYonetimi.AracSil(silinecekId);
        }

        static void AracKirala(AracYonetimi aracYonetimi)
        {
            Console.Write("Kiralanacak Araç ID: ");
            int id = Convert.ToInt32(Console.ReadLine());
            var arac = aracYonetimi.AracBul(id);

            if (arac != null)
            {
                Console.Write("Kiralama Süresi (gün): ");
                int sure = Convert.ToInt32(Console.ReadLine());

                double toplamKiraTutari = arac.Fiyat * sure;
                double indirimliKiraTutari = toplamKiraTutari; 

                // İndirim koşulları
                if (sure >= 30 && sure < 90)
                {
                    indirimliKiraTutari -= toplamKiraTutari * 0.05; 
                }
                else if (sure >= 90)
                {
                    indirimliKiraTutari -= toplamKiraTutari * 0.15; 
                }

                Console.WriteLine($"Normal Fiyatı:{toplamKiraTutari}, İndirimli Fiyatı: {indirimliKiraTutari}");
            }
            else
            {
                Console.WriteLine("Araç bulunamadı.");
            }
        }

        static void AracSatınAl(AracYonetimi aracYonetimi)
        {
            Console.Write("Satın alınacak Araç ID: ");
            int id = Convert.ToInt32(Console.ReadLine());
            var arac = aracYonetimi.AracBul(id);

            if (arac != null)
            {
                double indirimliFiyat = arac.Fiyat; 

                // İndirim koşulları
                if (arac.Fiyat <= 100000)
                {
                    indirimliFiyat -= arac.Fiyat * 0.05; 
                }
                else if (arac.Fiyat > 100000 && arac.Fiyat <= 500000)
                {
                    indirimliFiyat -= arac.Fiyat * 0.10; 
                }
                else if (arac.Fiyat > 500000)
                {
                    indirimliFiyat -= arac.Fiyat * 0.15; 
                }

                Console.WriteLine($"Normal Fiyatı:{arac.Fiyat}  Araç Satın Alma Tutarı: {indirimliFiyat}");
            }
            else
            {
                Console.WriteLine("Araç bulunamadı.");
            }
        }
    }

    abstract class Arac
    {
        public int AracID { get; set; }
        public string Model { get; set; }
        public double Fiyat { get; set; }
        public string Tür { get; set; } // Araç türü için eklenen özellik

        private static int sayac = 1;

        public Arac(string model, double fiyat, string tür)
        {
            AracID = sayac++;
            Model = model;
            Fiyat = fiyat;
            Tür = tür; // Tür ataması
        }
    }

    class Araba : Arac
    {
        public Araba(string model, double fiyat) : base(model, fiyat, "Araba") { }
    }

    class Kamyonet : Arac
    {
        public Kamyonet(string model, double fiyat) : base(model, fiyat, "Kamyonet") { }
    }

    class AracYonetimi
    {
        private List<Arac> araclar = new List<Arac>();
        private List<string> loglar = new List<string>(); // Loglar için liste

        public void AracEkle(Arac arac)
        {
            araclar.Add(arac);
            loglar.Add($"Araç eklendi: {arac.Model}, Fiyat: {arac.Fiyat}, Tür: {arac.Tür}");
        }

        public void AracSil(int id)
        {
            var arac = araclar.Find(a => a.AracID == id);
            if (arac != null)
            {
                araclar.Remove(arac);
                loglar.Add($"Araç silindi: ID {id}");
                Console.WriteLine($"Araç ID {id} silindi.");
            }
            else
            {
                Console.WriteLine("Silinecek araç bulunamadı.");
            }
        }

        public void AracListele()
        {
            if (araclar.Count == 0)
            {
                Console.WriteLine("Listeleyecek araç yok.");
                return;
            }

            foreach (var arac in araclar)
            {
                Console.WriteLine($"ID: {arac.AracID}, Model: {arac.Model}, Fiyat: {arac.Fiyat}, Tür: {arac.Tür}");
            }
        }

        public Arac AracBul(int id)
        {
            return araclar.Find(a => a.AracID == id);
        }

        public void LoglariGoster()
        {
            if (loglar.Count == 0)
            {
                Console.WriteLine("Gösterilecek log yok.");
                return;
            }

            Console.WriteLine("Loglar:");
            foreach (var log in loglar)
            {
                Console.WriteLine(log);
            }
        }
    }
}
