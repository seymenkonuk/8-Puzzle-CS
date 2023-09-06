using System.Threading;

namespace _8_Puzzle_CS
{
    public partial class Form1 : Form
    {
        #region Deðiþkenler
        readonly List<int> hamleler = new List<int>(); // Sýrasýyla yapýlan hamleler
        int hamle_no = 0; // kaçýncý hamle yapýldý
        int[,] tahta = new int[1, 1]; // Tahtadaki parçalarýn konumlarý
        readonly List<Panel> resimParcalari = new List<Panel>();
        #endregion

        #region Yapýcý Metotlar
        public Form1()
        {
            InitializeComponent();
            ResmiOlustur("varsayýlan", (int)numGenislik.Value, (int)numYukseklik.Value);
        }
        #endregion

        #region Events
        // Resim Seç Butonuna Týklandýðýnda Resmin Seçilebileceði Pencere Açýlsýn
        // Uygun Formatta Resim Seçildiðinde Resim Güncellensin
        private void BtnResimSec_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // Uygun Format Tanýmlamasý
            openFileDialog.Filter = "(*.png)|*.png|(*.jpg)|*.jpg";

            // Tamama Basýldýysa
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = Path.GetFullPath(openFileDialog.FileName);
                ResmiOlustur(path, (int)numGenislik.Value, (int)numYukseklik.Value);
            }
        }

        // Þimdilik Ýptal
        private void checkNumaralandir_CheckedChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        // Karýþtýr Butonuna Týklandýðýnda
        private void BtnKaristir_Click(object sender, EventArgs e)
        {
            ResmiKaristir();
        }

        // Geri Al Butonuna Týklandýðýnda
        private void BtnGeriAl_Click(object sender, EventArgs e)
        {
            HamleGeriAl();
        }

        // Ýleri Al Butonuna Týklandýðýnda
        private void BtnIleriAl_Click(object sender, EventArgs e)
        {
            HamleIleriAl();
        }

        // Sýrala Butonuna Týklandýðýnda
        // Þimdilik Ýptal
        private void BtnSirala_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        // Çöz Butonuna Týklandýðýnda
        private void BtnCoz_Click(object sender, EventArgs e)
        {
            Thread thread1 = new Thread(new ThreadStart(BulmacayiCoz));
            thread1.Start();
        }
        #endregion

        #region Metotlar
        // Numara deðeri Tahtada Hangi Konumda Yazýyor
        private Konum KonumBul(int numara)
        {
            Konum konum = new Konum();
            for (int i = 0; i < tahta.GetLength(0); i++)
                for (int j = 0; j < tahta.GetLength(1); j++)
                    if (tahta[i, j] == numara)
                    {
                        konum.X = i;
                        konum.Y = j;
                        return konum;
                    }
            throw new Exception("Böyle bir numara bulunamadý.");
        }

        // Numara deðeri tahtada hangi konumda olmalý
        private Konum OlmasiGerekKonumuBul(int numara)
        {
            if (numara < 0 || numara >= tahta.Length)
                throw new Exception("Böyle bir numara bulunamadý.");

            Konum konum = new Konum();

            if (numara == 0)
            {
                konum.X = tahta.GetLength(0) - 1;
                konum.Y = tahta.GetLength(1) - 1;
            }
            else
            {
                konum.Y = (numara - 1) % tahta.GetLength(0);
                konum.X = (numara - 1) / tahta.GetLength(0);
            }

            return konum;
        }

        // Yeni Resim Oluþturulsun
        // Butonlarý Oluþturulsun
        private void ResmiOlustur(string path, int genislik, int yukseklik)
        {
            // Sýfýrlama Ýþlemleri
            HamleSifirla();
            pnlResim.Controls.Clear();
            resimParcalari.Clear();
            tahta = new int[yukseklik, genislik];

            int genislikPixel = pnlResim.Width;
            int yukseklikPixel = pnlResim.Height;

            int genislikBoyut = genislikPixel / genislik;
            int yukseklikBoyut = yukseklikPixel / yukseklik;

            int deger = 1;
            for (int i = 0; i < yukseklik; i++)
            {
                for (int j = 0; j < genislik; j++)
                {
                    // Son Hücre Boþ Olacak
                    if (i == yukseklik - 1 && j == genislik - 1)
                    {
                        tahta[i, j] = 0;
                        continue;
                    }
                    tahta[i, j] = deger;

                    Panel panel = new Panel();
                    pnlResim.Controls.Add(panel);
                    panel.Location = new Point(j * genislikBoyut, i * yukseklikBoyut);
                    panel.Name = "pnl" + deger;
                    panel.Size = new Size(genislikBoyut, yukseklikBoyut);
                    panel.BorderStyle = BorderStyle.FixedSingle;

                    PictureBox pictureBox = new PictureBox();
                    panel.Controls.Add(pictureBox);
                    // Path Geçerli ise Pathdeki Resmi, deðilse varsayýlan resmi göster
                    try
                    {
                        pictureBox.BackgroundImage = Image.FromFile(path);
                    }
                    catch
                    {
                        pictureBox.BackgroundImage = Properties.Resources.varsayilan;
                    }
                    pictureBox.BackgroundImageLayout = ImageLayout.Stretch;
                    pictureBox.Location = new Point(-j * genislikBoyut, -i * yukseklikBoyut);
                    pictureBox.Name = "pictureBox" + deger;
                    pictureBox.Size = new Size(genislikPixel, yukseklikPixel);
                    pictureBox.TabIndex = 3;
                    pictureBox.TabStop = false;
                    pictureBox.Click += Resim_Click;
                    resimParcalari.Add(panel);
                    deger++;
                }
            }
        }

        // Resmi Karýþtýr
        // Çözümü Ýmkansýz Olan Bulmacalar Üretebiliyor
        // Deðiþtirilecek
        private void ResmiKaristir()
        {
            if (tahta == null) return;

            Random random = new Random();

            // Gerekli Deðiþkenler
            int genislik = tahta.GetLength(1);
            int yukseklik = tahta.GetLength(0);

            int genislikPixel = pnlResim.Width;
            int yukseklikPixel = pnlResim.Height;

            int genislikBoyut = genislikPixel / genislik;
            int yukseklikBoyut = yukseklikPixel / yukseklik;

            HamleSifirla();

            // Tahtayý Sýfýrla
            for (int i = 0; i < tahta.GetLength(0); i++)
                for (int j = 0; j < tahta.GetLength(1); j++)
                    tahta[i, j] = 0;

            for (int i = 1; i < tahta.Length; i++)
            {
                // Daha Önce Seçilmemiþ Rastgele Konum Seç
                int yeni_i, yeni_j;
                do
                {
                    yeni_i = random.Next(tahta.GetLength(0));
                    yeni_j = random.Next(tahta.GetLength(1));
                } while (tahta[yeni_i, yeni_j] != 0);

                // Seçtiðin Konuma Sayýyý Yaz
                tahta[yeni_i, yeni_j] = i;
                // Resmi Doðru Pozisyona Götür
                resimParcalari[i - 1].Location = new Point(yeni_j * genislikBoyut, yeni_i * yukseklikBoyut);
            }
        }

        // Resmin Herhangi Bir Parçasýna Týklandýðýnda
        private void Resim_Click(object? sender, EventArgs e)
        {
            if (sender == null) return;

            // Hangi Parçaya Týklandý
            PictureBox picture = (PictureBox)sender;
            int numara = Convert.ToInt32(picture.Name.Replace("pictureBox", ""));

            // Eðer Hamle Yapýlabiliyorsa
            // (Týklanan Parçanýn Komþusunda Boþ Parça Var ise)
            if (HamleYap(numara))
                HamleEkle(numara);
        }

        // Tüm Hamle Geçmiþini Siler
        private void HamleSifirla()
        {
            hamle_no = 0;
            hamleler.Clear();
        }

        private void HamleEkle(int numara)
        {
            // Numarayý Hamlelere Ekle
            // Eðer Son Hamlede Deðilsen Sonraki Hamleleri Sil
            while (hamleler.Count > hamle_no)
                hamleler.RemoveAt(hamleler.Count - 1);

            hamleler.Add(numara);
            hamle_no++;
        }

        // Numara: týklanan resmin numarasý
        // Hareket: yapýlan hamlenin sonucu ekranda gösterilsin mi
        private bool HamleYap(int numara, bool hareket = true)
        {
            // Týklanan Butonun Numarasýndan, Butonun Konumunu Bul
            var konum = KonumBul(numara);
            int x = konum.X, y = konum.Y;

            // Bilinmeyen Hata
            if (x == -1) return false;

            // Týklanan Resmin 4 Komþusunu Kontrol Et, Boþ Olan Varsa O Yöne Hareket Ettir
            int[,] yonler = { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };
            for (int i = 0; i < 4; i++)
            {
                int yeniX = x + yonler[i, 0];
                int yeniY = y + yonler[i, 1];

                // O Yön Yoksa (Limit Dýþýndaysa)
                if (yeniX < 0 || yeniX >= tahta.GetLength(0)) continue;
                if (yeniY < 0 || yeniY >= tahta.GetLength(1)) continue;

                // O Yön Doluysa
                if (tahta[yeniX, yeniY] != 0) continue;

                // Boþsa Diziyi Güncelle
                tahta[yeniX, yeniY] = tahta[x, y];
                tahta[x, y] = 0;

                // Resmi Hareket Ettir
                if (hareket)
                {
                    resimParcalari[numara - 1].Left += yonler[i, 1] * resimParcalari[numara - 1].Width;
                    resimParcalari[numara - 1].Top += yonler[i, 0] * resimParcalari[numara - 1].Height;
                }
                // Hamle Yapýlabiliyor (ve yapýldý)
                return true;
            }

            // Boþ Komþusu Yok, Hareket Edemez/Edemedi
            return false;
        }

        private void HamleGeriAl(bool hareket = true)
        {
            // Geri Alýnabilecek Bir Þey Yoksa
            if (hamle_no == 0) return;
            hamle_no--;
            HamleYap(hamleler[hamle_no], hareket);
        }

        private void HamleIleriAl()
        {
            // Ýleri Alýnabilecek Bir Þey Yoksa
            if (hamle_no >= hamleler.Count) return;
            HamleYap(hamleler[hamle_no]);
            hamle_no++;
        }

        #endregion
    }

    public struct Konum
    {
        public int X;
        public int Y;
    }
}