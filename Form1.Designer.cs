namespace _8_Puzzle_CS
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnIleriAl = new Button();
            btnGeriAl = new Button();
            numGenislik = new NumericUpDown();
            btnSirala = new Button();
            btnCoz = new Button();
            checkNumaralandir = new CheckBox();
            numYukseklik = new NumericUpDown();
            btnKaristir = new Button();
            pnlResim = new Panel();
            btnResimSec = new Button();
            ((System.ComponentModel.ISupportInitialize)numGenislik).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numYukseklik).BeginInit();
            SuspendLayout();
            // 
            // btnIleriAl
            // 
            btnIleriAl.Location = new Point(584, 204);
            btnIleriAl.Name = "btnIleriAl";
            btnIleriAl.Size = new Size(50, 50);
            btnIleriAl.TabIndex = 30;
            btnIleriAl.Text = ">";
            btnIleriAl.UseVisualStyleBackColor = true;
            btnIleriAl.Click += BtnIleriAl_Click;
            // 
            // btnGeriAl
            // 
            btnGeriAl.Location = new Point(166, 204);
            btnGeriAl.Name = "btnGeriAl";
            btnGeriAl.Size = new Size(50, 50);
            btnGeriAl.TabIndex = 29;
            btnGeriAl.Text = "<";
            btnGeriAl.UseVisualStyleBackColor = true;
            btnGeriAl.Click += BtnGeriAl_Click;
            // 
            // numGenislik
            // 
            numGenislik.Location = new Point(257, 28);
            numGenislik.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numGenislik.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            numGenislik.Name = "numGenislik";
            numGenislik.Size = new Size(94, 27);
            numGenislik.TabIndex = 28;
            numGenislik.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // btnSirala
            // 
            btnSirala.Enabled = false;
            btnSirala.Location = new Point(463, 395);
            btnSirala.Name = "btnSirala";
            btnSirala.Size = new Size(94, 29);
            btnSirala.TabIndex = 27;
            btnSirala.Text = "Sırala";
            btnSirala.UseVisualStyleBackColor = true;
            btnSirala.Click += BtnSirala_Click;
            // 
            // btnCoz
            // 
            btnCoz.Location = new Point(360, 395);
            btnCoz.Name = "btnCoz";
            btnCoz.Size = new Size(94, 29);
            btnCoz.TabIndex = 26;
            btnCoz.Text = "Çöz";
            btnCoz.UseVisualStyleBackColor = true;
            btnCoz.Click += BtnCoz_Click;
            // 
            // checkNumaralandir
            // 
            checkNumaralandir.AutoSize = true;
            checkNumaralandir.Enabled = false;
            checkNumaralandir.Location = new Point(257, 365);
            checkNumaralandir.Name = "checkNumaralandir";
            checkNumaralandir.Size = new Size(122, 24);
            checkNumaralandir.TabIndex = 25;
            checkNumaralandir.Text = "Numaralandır";
            checkNumaralandir.UseVisualStyleBackColor = true;
            checkNumaralandir.Click += checkNumaralandir_CheckedChanged;
            // 
            // numYukseklik
            // 
            numYukseklik.Location = new Point(360, 28);
            numYukseklik.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numYukseklik.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            numYukseklik.Name = "numYukseklik";
            numYukseklik.Size = new Size(94, 27);
            numYukseklik.TabIndex = 24;
            numYukseklik.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // btnKaristir
            // 
            btnKaristir.Enabled = false;
            btnKaristir.Location = new Point(257, 395);
            btnKaristir.Name = "btnKaristir";
            btnKaristir.Size = new Size(94, 29);
            btnKaristir.TabIndex = 23;
            btnKaristir.Text = "Karıştır";
            btnKaristir.UseVisualStyleBackColor = true;
            btnKaristir.Click += BtnKaristir_Click;
            // 
            // pnlResim
            // 
            pnlResim.Location = new Point(257, 59);
            pnlResim.Name = "pnlResim";
            pnlResim.Size = new Size(300, 300);
            pnlResim.TabIndex = 22;
            // 
            // btnResimSec
            // 
            btnResimSec.Location = new Point(463, 26);
            btnResimSec.Name = "btnResimSec";
            btnResimSec.Size = new Size(94, 29);
            btnResimSec.TabIndex = 21;
            btnResimSec.Text = "Resim Seç";
            btnResimSec.UseVisualStyleBackColor = true;
            btnResimSec.Click += BtnResimSec_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnIleriAl);
            Controls.Add(btnGeriAl);
            Controls.Add(numGenislik);
            Controls.Add(btnSirala);
            Controls.Add(btnCoz);
            Controls.Add(checkNumaralandir);
            Controls.Add(numYukseklik);
            Controls.Add(btnKaristir);
            Controls.Add(pnlResim);
            Controls.Add(btnResimSec);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "Form1";
            Text = "8 Bulmacası";
            ((System.ComponentModel.ISupportInitialize)numGenislik).EndInit();
            ((System.ComponentModel.ISupportInitialize)numYukseklik).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnIleriAl;
        private Button btnGeriAl;
        private NumericUpDown numGenislik;
        private Button btnSirala;
        private Button btnCoz;
        private CheckBox checkNumaralandir;
        private NumericUpDown numYukseklik;
        private Button btnKaristir;
        private Panel pnlResim;
        private Button btnResimSec;
    }
}