using System.Threading;

namespace _8_Puzzle_CS
{
    public partial class Form1 : Form
    {
        #region De�i�kenler
        readonly List<int> hamleler = new List<int>(); // S�ras�yla yap�lan hamleler
        int hamle_no = 0; // ka��nc� hamle yap�ld�
        int[,] tahta = new int[1, 1]; // Tahtadaki par�alar�n konumlar�
        readonly List<Panel> resimParcalari = new List<Panel>();
        #endregion

        #region Yap�c� Metotlar
        public Form1()
        {
            InitializeComponent();
            ResmiOlustur("varsay�lan", (int)numGenislik.Value, (int)numYukseklik.Value);
        }
        #endregion

        #region Events
        // Resim Se� Butonuna T�kland���nda Resmin Se�ilebilece�i Pencere A��ls�n
        // Uygun Formatta Resim Se�ildi�inde Resim G�ncellensin
        private void BtnResimSec_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // Uygun Format Tan�mlamas�
            openFileDialog.Filter = "(*.png)|*.png|(*.jpg)|*.jpg";

            // Tamama Bas�ld�ysa
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = Path.GetFullPath(openFileDialog.FileName);
                ResmiOlustur(path, (int)numGenislik.Value, (int)numYukseklik.Value);
            }
        }

        // �imdilik �ptal
        private void checkNumaralandir_CheckedChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        // Kar��t�r Butonuna T�kland���nda
        private void BtnKaristir_Click(object sender, EventArgs e)
        {
            ResmiKaristir();
        }

        // Geri Al Butonuna T�kland���nda
        private void BtnGeriAl_Click(object sender, EventArgs e)
        {
            HamleGeriAl();
        }

        // �leri Al Butonuna T�kland���nda
        private void BtnIleriAl_Click(object sender, EventArgs e)
        {
            HamleIleriAl();
        }

        // S�rala Butonuna T�kland���nda
        // �imdilik �ptal
        private void BtnSirala_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        // ��z Butonuna T�kland���nda
        private void BtnCoz_Click(object sender, EventArgs e)
        {
            Thread thread1 = new Thread(new ThreadStart(BulmacayiCoz));
            thread1.Start();
        }
        #endregion

        #region Metotlar
        // Numara de�eri Tahtada Hangi Konumda Yaz�yor
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
            throw new Exception("B�yle bir numara bulunamad�.");
        }

        // Numara de�eri tahtada hangi konumda olmal�
        private Konum OlmasiGerekKonumuBul(int numara)
        {
            if (numara < 0 || numara >= tahta.Length)
                throw new Exception("B�yle bir numara bulunamad�.");

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

        // Yeni Resim Olu�turulsun
        // Butonlar� Olu�turulsun
        private void ResmiOlustur(string path, int genislik, int yukseklik)
        {
            // S�f�rlama ��lemleri
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
                    // Son H�cre Bo� Olacak
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
                    // Path Ge�erli ise Pathdeki Resmi, de�ilse varsay�lan resmi g�ster
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

        // Resmi Kar��t�r
        // ��z�m� �mkans�z Olan Bulmacalar �retebiliyor
        // De�i�tirilecek
        private void ResmiKaristir()
        {
            if (tahta == null) return;

            Random random = new Random();

            // Gerekli De�i�kenler
            int genislik = tahta.GetLength(1);
            int yukseklik = tahta.GetLength(0);

            int genislikPixel = pnlResim.Width;
            int yukseklikPixel = pnlResim.Height;

            int genislikBoyut = genislikPixel / genislik;
            int yukseklikBoyut = yukseklikPixel / yukseklik;

            HamleSifirla();

            // Tahtay� S�f�rla
            for (int i = 0; i < tahta.GetLength(0); i++)
                for (int j = 0; j < tahta.GetLength(1); j++)
                    tahta[i, j] = 0;

            for (int i = 1; i < tahta.Length; i++)
            {
                // Daha �nce Se�ilmemi� Rastgele Konum Se�
                int yeni_i, yeni_j;
                do
                {
                    yeni_i = random.Next(tahta.GetLength(0));
                    yeni_j = random.Next(tahta.GetLength(1));
                } while (tahta[yeni_i, yeni_j] != 0);

                // Se�ti�in Konuma Say�y� Yaz
                tahta[yeni_i, yeni_j] = i;
                // Resmi Do�ru Pozisyona G�t�r
                resimParcalari[i - 1].Location = new Point(yeni_j * genislikBoyut, yeni_i * yukseklikBoyut);
            }
        }

        // Resmin Herhangi Bir Par�as�na T�kland���nda
        private void Resim_Click(object? sender, EventArgs e)
        {
            if (sender == null) return;

            // Hangi Par�aya T�kland�
            PictureBox picture = (PictureBox)sender;
            int numara = Convert.ToInt32(picture.Name.Replace("pictureBox", ""));

            // E�er Hamle Yap�labiliyorsa
            // (T�klanan Par�an�n Kom�usunda Bo� Par�a Var ise)
            if (HamleYap(numara))
                HamleEkle(numara);
        }

        // T�m Hamle Ge�mi�ini Siler
        private void HamleSifirla()
        {
            hamle_no = 0;
            hamleler.Clear();
        }

        private void HamleEkle(int numara)
        {
            // Numaray� Hamlelere Ekle
            // E�er Son Hamlede De�ilsen Sonraki Hamleleri Sil
            while (hamleler.Count > hamle_no)
                hamleler.RemoveAt(hamleler.Count - 1);

            hamleler.Add(numara);
            hamle_no++;
        }

        // Numara: t�klanan resmin numaras�
        // Hareket: yap�lan hamlenin sonucu ekranda g�sterilsin mi
        private bool HamleYap(int numara, bool hareket = true)
        {
            // T�klanan Butonun Numaras�ndan, Butonun Konumunu Bul
            var konum = KonumBul(numara);
            int x = konum.X, y = konum.Y;

            // Bilinmeyen Hata
            if (x == -1) return false;

            // T�klanan Resmin 4 Kom�usunu Kontrol Et, Bo� Olan Varsa O Y�ne Hareket Ettir
            int[,] yonler = { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };
            for (int i = 0; i < 4; i++)
            {
                int yeniX = x + yonler[i, 0];
                int yeniY = y + yonler[i, 1];

                // O Y�n Yoksa (Limit D���ndaysa)
                if (yeniX < 0 || yeniX >= tahta.GetLength(0)) continue;
                if (yeniY < 0 || yeniY >= tahta.GetLength(1)) continue;

                // O Y�n Doluysa
                if (tahta[yeniX, yeniY] != 0) continue;

                // Bo�sa Diziyi G�ncelle
                tahta[yeniX, yeniY] = tahta[x, y];
                tahta[x, y] = 0;

                // Resmi Hareket Ettir
                if (hareket)
                {
                    resimParcalari[numara - 1].Left += yonler[i, 1] * resimParcalari[numara - 1].Width;
                    resimParcalari[numara - 1].Top += yonler[i, 0] * resimParcalari[numara - 1].Height;
                }
                // Hamle Yap�labiliyor (ve yap�ld�)
                return true;
            }

            // Bo� Kom�usu Yok, Hareket Edemez/Edemedi
            return false;
        }

        private void HamleGeriAl(bool hareket = true)
        {
            // Geri Al�nabilecek Bir �ey Yoksa
            if (hamle_no == 0) return;
            hamle_no--;
            HamleYap(hamleler[hamle_no], hareket);
        }

        private void HamleIleriAl()
        {
            // �leri Al�nabilecek Bir �ey Yoksa
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