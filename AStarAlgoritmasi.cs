using Veri_Yapilari_CS.Heap;

/*
 *
 * A STAR ALGORİTMASI NEDİR?
 * En iyi çözümü bulmayı amaçlayan bir algoritmadır.
 * Bir graphta en kısa yolu bulmak için vb. de kullanılabilir.
 * En doğru çözümü bulmayı garanti eder.
 * 
 * Hamle yapmadan önce hamlelerini puanlar:
 * Bu hamleden sonra sezgisel olarak en kısa kaç hamlede bitirebilirim oyunu
 * Bu hamleye kadar ne kadar hamle harcadım
 * 
 * İki maliyet toplanır ve çözümün puanı olur.
 * 
 * Puanlanan hamleler bir yerde saklanır ve en az puana sahip
 * hamleden devam edilir.
 * 
 * Örneğin;
 * A şehrinden X şehrine gitmek istiyoruz.
 * 
 * A şehrinden B ve C ye gidebilir olalım.
 * 
 * A'dan B'ye maliyet:  7
 * A'dan C'ye maliyet: 14
 * 
 * B'den kuş uçuşu X'e maliyet: 100
 * C'den kuş uçuşu X'e maliyet:  30
 * 
 * B'nin puanı: 107
 * C'nin puanı:  44
 * 
 * Bu yüzden algoritma C'den gitmeye öncelik verecektir.
 * C'den diğer şehirlere gitmeyi değerlendirecek ve gerekirse
 * geri dönerek B şehrinden gitmeye karar verecek.
 * 
 *
 */

namespace _8_Puzzle_CS
{
    public partial class Form1
    {
        private void BulmacayiCoz()
        {
            int[,] orjTahta = new int[tahta.GetLength(0), tahta.GetLength(1)];

            // Orijinal Tahtayı Yedekle
            for (int i = 0; i < tahta.GetLength(0); i++)
                for (int j = 0; j < tahta.GetLength(1); j++)
                    orjTahta[i, j] = tahta[i, j];

            // Hamleleri Sıfırla
            HamleSifirla();

            // Daha Önce Görülmüş Tahta Konumları
            HashSet<string> bakilmisTahta = new HashSet<string>();
            bakilmisTahta.Add(TahtayiMetneDonustur());

            // Başlangıç Pozisyonunu Ekle
            MinHeap<Cozum> olasiCozumler = new MinHeap<Cozum>();
            olasiCozumler.Add(new Cozum(hamleler, SezgiselPuanHesapla() + hamleler.Count));

            // A STAR ALGORİTMASI
            // Olası En İyi Hamleyi Devam Ettir
            // Bulmacayı Tamamlayana Kadar
            while (olasiCozumler.Count > 0)
            {
                Cozum cozum = olasiCozumler.Delete();

                // Tahtayı Orijinal Konuma Döndür
                for (int i = 0; i < tahta.GetLength(0); i++)
                    for (int j = 0; j < tahta.GetLength(1); j++)
                        tahta[i, j] = orjTahta[i, j];

                // O çözümün tahtasına ulaşabilmek için
                // Yapılan tüm hamleleri yap
                HamleSifirla();
                foreach (int hamle in cozum.Hamleler)
                {
                    HamleYap(hamle, false);
                    HamleEkle(hamle);
                }

                // Doğru Çözüm Bulunduysa
                if (OyunBittiMi())
                {
                    // Tahtayı Orijinal Konuma Döndür
                    for (int i = 0; i < tahta.GetLength(0); i++)
                        for (int j = 0; j < tahta.GetLength(1); j++)
                            tahta[i, j] = orjTahta[i, j];
                    hamle_no = 0;
                    MessageBox.Show("Çözüldü");
                    return;
                }

                // Doğru Çözüm Bulunmadıysa, O Konumda Yapılabilecek
                // Tüm Hamleleri olasiCozumler'e Ekle
                for (int i = 1; i < tahta.Length; i++)
                {
                    // İlgili Hamle Yapılabiliyorsa Yap
                    if (HamleYap(i, false))
                    {
                        HamleEkle(i);
                        // Daha Önce Bakılmamış ise
                        // olasiCozumler'e ekle
                        if (!bakilmisTahta.Contains(TahtayiMetneDonustur()))
                        {
                            olasiCozumler.Add(new Cozum(hamleler, SezgiselPuanHesapla() + hamleler.Count));
                            bakilmisTahta.Add(TahtayiMetneDonustur());
                        }
                        HamleGeriAl(false);
                        hamleler.RemoveAt(hamleler.Count - 1);
                    }
                }
            }

            // Tahtayı Orijinal Konuma Döndür
            for (int i = 0; i < tahta.GetLength(0); i++)
                for (int j = 0; j < tahta.GetLength(1); j++)
                    tahta[i, j] = orjTahta[i, j];
            hamle_no = 0;

            throw new Exception("Çözüm Bulunamadı!");
        }

        public string TahtayiMetneDonustur()
        {
            string sonuc = "";
            for (int i = 0; i < tahta.GetLength(0); i++)
                for (int j = 0; j < tahta.GetLength(1); j++)
                    sonuc += tahta[i, j] + "-";
            return sonuc;
        }

        // Tahta Doğru Konuma Ulaştı Mı
        public bool OyunBittiMi()
        {
            int deger = 0;
            for (int i = 0; i < tahta.GetLength(0); i++)
                for (int j = 0; j < tahta.GetLength(1); j++)
                {
                    // Tahtanın Sonuna Geldiyse
                    // Buraya Gelene Kadar False Döndürmediyse Zaten Orada Boşluk (0) vardır
                    // true döndürebilir
                    if (i == tahta.GetLength(0) - 1
                     && j == tahta.GetLength(1) - 1)
                        continue;

                    // O hücrede doğru değer yazmıyorsa oyun bitmemiştir
                    deger++;
                    if (tahta[i, j] != deger)
                        return false;
                }
            return true;
        }

        // Puan Hesapla
        public int SezgiselPuanHesapla()
        {
            int puan = 0;
            for (int i = 1; i < tahta.Length; i++)
            {
                var bulunduguKonum = KonumBul(i);
                Konum olmasiGerekenKonum = OlmasiGerekKonumuBul(i);

                puan += Math.Abs(olmasiGerekenKonum.X - bulunduguKonum.X);
                puan += Math.Abs(olmasiGerekenKonum.Y - bulunduguKonum.Y);
            }
            return puan;
        }
    }

    public class Cozum : IComparable<Cozum>
    {
        public int Puan { get; set; }
        public List<int> Hamleler { get; set; } = new List<int>();

        public Cozum(List<int> hamleler, int puan)
        {
            foreach (var item in hamleler)
                Hamleler.Add(item);
            Puan = puan;
        }

        public int CompareTo(Cozum? other)
        {
            if (other == null) return 1;
            return Puan.CompareTo(other.Puan);
        }
    }
}
