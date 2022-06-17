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
using System.Globalization;

namespace WindowsFormsApplication1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        Form1 F1 = new Form1();
        DataTable Tablo = new DataTable();
        
        private void Kullanici()
        {
            Tablo.Clear();
            try
            {
                F1.Baglan.Open();
                OleDbDataAdapter Yaz = new OleDbDataAdapter("SELECT Tc,Adi,Soyadi,Cinsiyeti,DogumYeri,DogumTarihi FROM Hesaplar", F1.Baglan);
                Yaz.Fill(Tablo);
                dataGridView1.DataSource = Tablo;
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }
        private void KullaniciSayi()
        {
            F1.Baglan.Open();
            OleDbCommand Komut = new OleDbCommand("SELECT * FROM Hesaplar", F1.Baglan);
            OleDbDataReader Oku = Komut.ExecuteReader();
            int Sayi = 0;
            while (Oku.Read())
            {
                Sayi++;
            }
            label1.Text = "Hastane sistemizde şuan " + Sayi + " adet üye kayıtlı üye bulunmaktadır.";
            F1.Baglan.Close();
        }
        private void Doktor()
        {
            Tablo.Clear();
            try
            {
                F1.Baglan.Open();
                OleDbDataAdapter Yaz = new OleDbDataAdapter("SELECT Tc,AdiSoyadi FROM Doktorlar", F1.Baglan);
                Yaz.Fill(Tablo);
                dataGridView1.DataSource = Tablo;
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }
        private void DoktorSayi()
        {
            F1.Baglan.Open();
            OleDbCommand Komut = new OleDbCommand("SELECT * FROM Doktorlar", F1.Baglan);
            OleDbDataReader Oku = Komut.ExecuteReader();
            int Sayi = 0;
            while (Oku.Read())
            {
                Sayi++;
            }
            label1.Text = "Hastane sistemizde şuan " + Sayi + " adet kayıtlı doktor bulunmaktadır.";
            F1.Baglan.Close();
        }
        private void Klinik()
        {
            Tablo.Clear();
            try
            {
                F1.Baglan.Open();
                OleDbDataAdapter Yaz = new OleDbDataAdapter("SELECT* FROM Klinikler", F1.Baglan);
                Yaz.Fill(Tablo);
                dataGridView1.DataSource = Tablo;
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                F1.Baglan.Close();
                MessageBox.Show(Hata.Message);
            }
        }
        private void KlinikSayi()
        {
            F1.Baglan.Open();
            OleDbCommand Komut = new OleDbCommand("SELECT * FROM Klinikler", F1.Baglan);
            OleDbDataReader Oku = Komut.ExecuteReader();
            int Sayi = 0;
            while (Oku.Read())
            {
                Sayi++;
            }
            label1.Text = "Hastane sistemizde şuan " + Sayi + " adet kayıtlı klinik bulunmaktadır.";
            F1.Baglan.Close();
        }
        private void Form4_Load(object sender, EventArgs e) { this.CenterToScreen(); timer1.Start(); toolStripStatusLabel3.Text = "Yönetici Olarak Giriş Yapıldı."; toolStripStatusLabel3.ForeColor = Color.Gold; }

        private void button1_Click(object sender, EventArgs e)
        {
            Kullanici();
            KullaniciSayi();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult D = MessageBox.Show(dataGridView1.CurrentRow.Cells[0].Value + " Kimlik Numaralı Adı " + dataGridView1.CurrentRow.Cells[1].Value + " Soyadı " + dataGridView1.CurrentRow.Cells[2].Value + " olan kullanıcı hesabını silmek istediğine emin misiniz?", "Hastane", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (D == DialogResult.Yes)
                {
                    F1.Baglan.Open();
                    OleDbCommand Komut = new OleDbCommand("DELETE * FROM Hesaplar WHERE Tc=@Tc", F1.Baglan);
                    Komut.Parameters.AddWithValue("@Tc", dataGridView1.CurrentRow.Cells[0].Value);
                    Komut.ExecuteNonQuery();
                    F1.Baglan.Close();
                    Tablo.Clear();
                    Kullanici();
                    KullaniciSayi();
                }
                else
                {
                    MessageBox.Show("Silme işlemi iptal edildi.", "Hastane", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToLongTimeString() + " / " + DateTime.Now.ToShortDateString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Doktor();
            DoktorSayi();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            try
            {
                F1.Baglan.Open();
                OleDbCommand Komut = new OleDbCommand("SELECT * FROM Klinikler", F1.Baglan);
                OleDbDataReader Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    comboBox1.Items.Add(Oku["Klinikid"].ToString());
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult D = MessageBox.Show(dataGridView1.CurrentRow.Cells[0].Value + " Kimlik Numaralı Adı Soyadı " + dataGridView1.CurrentRow.Cells[1].Value + " olan doktor hesabını silmek istediğine emin misiniz?", "Hastane", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (D == DialogResult.Yes)
                {
                    F1.Baglan.Open();
                    OleDbCommand Komut = new OleDbCommand("DELETE * FROM Doktorlar WHERE Tc=@Tc", F1.Baglan);
                    Komut.Parameters.AddWithValue("@Tc", dataGridView1.CurrentRow.Cells[0].Value);
                    Komut.ExecuteNonQuery();
                    F1.Baglan.Close();
                    Tablo.Clear();
                    Doktor();
                    DoktorSayi();
                }
                else
                {
                    MessageBox.Show("Silme işlemi iptal edildi.", "Hastane", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "" && textBox4.Text == "" && comboBox1.Text == "")
            {
                MessageBox.Show("Bilgiler eksik olduğu için kayıt işlemi iptal edildi.", "Hastane", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                try
                {
                    DialogResult D = MessageBox.Show("Sisteme Bulunan " + textBox3.Text +  " Kliniğine " + textBox1.Text + " Kimlik Numaralı Adı Soyadı "  + textBox2.Text + " Olan Doktor Kayıdı Ekleniyor Onaylıyor musunuz?" , "Hastane", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (D == DialogResult.Yes)
                    {
                        F1.Baglan.Open();
                        OleDbCommand Komut = new OleDbCommand("INSERT INTO Doktorlar(Tc,AdiSoyadi,Klinikid,Sifre) VALUES (@Tc,@AdiSoyadi,@Klinikid,@Sifre)", F1.Baglan);
                        Komut.Parameters.AddWithValue("@Tc", textBox1.Text);
                        Komut.Parameters.AddWithValue("@AdiSoyadi",CultureInfo.CurrentCulture.TextInfo.ToTitleCase(textBox2.Text));
                        Komut.Parameters.AddWithValue("@Klinikid", comboBox1.Text);
                        Komut.Parameters.AddWithValue("@Sifre", textBox4.Text);
                        Komut.ExecuteNonQuery();
                        F1.Baglan.Close();
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        comboBox1.Text = "";
                        panel1.Visible = false;
                        Tablo.Clear();
                        Doktor();
                        DoktorSayi();
                    }
                    else
                    {
                        MessageBox.Show("Kayıt işlemi iptal edildi.", "Hastane", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        panel1.Visible = false;
                    }
                }
                catch (Exception Hata)
                {
                    MessageBox.Show(Hata.Message);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                F1.Baglan.Open();
                OleDbCommand Komut = new OleDbCommand("SELECT * FROM Klinikler WHERE Klinikid=@Klinikid", F1.Baglan);
                Komut.Parameters.AddWithValue("@Klinikid", comboBox1.Text);
                OleDbDataReader Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    textBox3.Text = Oku["KlinikAdi"].ToString();
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Klinik();
            KlinikSayi();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult D = MessageBox.Show("Klinik silmek istediğine emin misiniz?", "Hastane", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (D == DialogResult.Yes)
                {
                    F1.Baglan.Open();
                    OleDbCommand Komut = new OleDbCommand("DELETE * FROM Klinikler WHERE Klinikid=@Id", F1.Baglan);
                    Komut.Parameters.AddWithValue("@Id", dataGridView1.CurrentRow.Cells[0].Value);
                    Komut.ExecuteNonQuery();
                    F1.Baglan.Close();
                    Tablo.Clear();
                    Klinik();
                    KlinikSayi();
                }
                else
                {
                    MessageBox.Show("Silme işlemi iptal edildi.", "Hastane", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult D = MessageBox.Show(textBox5.Text + " isimli klinik ekleniyor onaylıyor musunuz?", "Hastane", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (D == DialogResult.Yes)
                {
                    F1.Baglan.Open();
                    OleDbCommand Komut = new OleDbCommand("INSERT INTO Klinikler(KlinikAdi) VALUES (@KlinikAdi)", F1.Baglan);
                    Komut.Parameters.AddWithValue("@KlinikAdi", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(textBox5.Text));
                    Komut.ExecuteNonQuery();
                    F1.Baglan.Close();
                    Tablo.Clear();
                    Klinik();
                    KlinikSayi();
                    textBox5.Text = "";
                    panel2.Visible = false;
                }
                else
                {
                    textBox5.Text = "";
                    panel2.Visible = false;
                    MessageBox.Show("Kayıt işlemi iptal edildi.", "Hastane", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (Exception Hata)
            {
                F1.Baglan.Close();
                MessageBox.Show(Hata.Message);
            }
        }
    }
}
