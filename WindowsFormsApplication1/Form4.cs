using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        Form1 F1 = new Form1();
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        OleDbCommand Komut;
        OleDbDataReader Oku;
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        bool ParolaKontrol = false;
        bool KayitKontrol = false;
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        private void UyeEkle()
        {
            try
            {
                DialogResult D = MessageBox.Show(textBox2.Text + " " + textBox3.Text + " isimli kaydınız oluşturulsun mu?", "Hastane", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (D == DialogResult.Yes)
                {
                    F1.Baglan.Open();
                    Komut = new OleDbCommand("INSERT INTO Hesaplar(Tc,Adi,Soyadi,Cinsiyeti,DogumYeri,DogumTarihi,Parola) VALUES (@Tc,@Adi,@Soyadi,@Cinsiyeti,@DogumYeri,@DogumTarihi,@Parola)", F1.Baglan);
                    Komut.Parameters.AddWithValue("@Tc", textBox1.Text.ToString());
                    Komut.Parameters.AddWithValue("@Adi", textBox2.Text);
                    Komut.Parameters.AddWithValue("@Soyadi", textBox3.Text);
                    Komut.Parameters.AddWithValue("@Cinsiyeti", comboBox1.SelectedItem.ToString());
                    Komut.Parameters.AddWithValue("@DogumYeri", textBox4.Text);
                    Komut.Parameters.AddWithValue("@DogumTarihi", textBox5.Text);
                    Komut.Parameters.AddWithValue("@Parola", textBox6.Text);
                    Oku = Komut.ExecuteReader();
                    MessageBox.Show(textBox2.Text + " " + textBox3.Text + " isimli kaydınız başarıyla oluşturulmuştur giriş ekranına dönüp giriş yapabilirsiniz.");
                    this.Close();
                    F1.Baglan.Close();
                }
                else
                {
                    MessageBox.Show("Kaydınız iptal edilmiştir girilen tüm bilgiler silinecektir!", "Hastane");
                    foreach (Control item in this.Controls)
                    {
                        if (item is TextBox)
                        {
                            TextBox Txt = (TextBox)item;
                            Txt.Clear();
                        }
                    }
                }

            }
            catch (Exception Hata)
            {
                F1.Baglan.Close();
                MessageBox.Show(Hata.ToString());
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button1_Click(object sender, EventArgs e)
        {
            ErrorProvider provider = new ErrorProvider();
            if (textBox6.Text == "")
            {
                provider.SetError(textBox6, "Gerekli Alan!");
                ParolaKontrol = true;
            }
            else
            {
                provider.Dispose();
                ParolaKontrol = false;
            }
            if (ParolaKontrol == false)
            {
                foreach (Control Kontrol in this.Controls)
                {
                    Kontrol.Enabled = false;
                }
                provider.Dispose();
                if (!KayitKontrol)
                {
                    DialogResult D = MessageBox.Show("Sistemde " + textBox1.Text + " kimlik numaralı kayıt mevcuttur kayıt işleminiz iptal edildi parola hatırlamak istiyorsanız evet butonuna tıklayınız.", "Hastane", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (D == DialogResult.Yes)
                    {
                        Form14 F14 = new Form14();
                        F14.Show();
                        this.Close();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else { UyeEkle(); }
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                F1.Baglan.Open();
                ErrorProvider provider = new ErrorProvider();
                Komut = new OleDbCommand("SELECT COUNT(Tc) FROM Hesaplar WHERE Tc=@Tc", F1.Baglan);
                Komut.Parameters.AddWithValue("@Tc", textBox1.Text);
                if (Convert.ToInt32(Komut.ExecuteScalar()) > 0)
                {
                    provider.SetError(textBox1, "Kimlik Numarası Kullanımda.");
                }
                else
                {
                    KayitKontrol = true;
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Hata: " + Hata.Message);
            }
        }
    }
}
