using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //--------------------------------------------------//--------------------------------------------------\\--------------------------------------------------\\
        public OleDbConnection Baglan = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Hastane.accdb");
        //--------------------------------------------------//--------------------------------------------------\\--------------------------------------------------\\
        bool Admins = false, Doktor = false, Kullanici = false;
        //--------------------------------------------------//--------------------------------------------------\\--------------------------------------------------\\
        private void Form1_Load(object sender, EventArgs e) { this.CenterToScreen(); }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void Admin()
        {
            // if (textBox1.Text == "Administrator" && textBox2.Text == "1234")
            if (textBox1.Text == "123456789" && textBox2.Text == "1234")
            {
                Form4 F4 = new Form4();
                F4.Show();
                this.Hide();
                Admins = true;
                Kullanici = true;
            }
        }
        private void User()
        {
            try
            {
                Baglan.Open();
                OleDbCommand Komut = new OleDbCommand("SELECT * FROM Hesaplar WHERE Tc=@Tc AND Sifre=@Sifre", Baglan);
                Komut.Parameters.AddWithValue("@Tc", textBox1.Text);
                Komut.Parameters.AddWithValue("@Sifre", textBox2.Text);
                Komut.ExecuteNonQuery();
                OleDbDataReader Oku = Komut.ExecuteReader();
                if (Oku.Read())
                {
                    Form2 F2 = new Form2(textBox1.Text, textBox2.Text);
                    F2.Show();
                    this.Hide();
                    Kullanici = true;
                }
                else
                {
                    Baglan.Close();
                    MessageBox.Show("Kimlik numaranız veya şifreniz yanlış lütfen tekrar deneyin.", "Hastane", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Baglan.Close();
            }
            catch (Exception Hata)
            {
                Baglan.Close();
                MessageBox.Show(Hata.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6 F6 = new Form6();
            F6.ShowDialog();
        }
        private void Doctor()
        {
            Doktor = false;
            Admins = false;
            try
            {
                Baglan.Open();
                OleDbCommand Komut = new OleDbCommand("SELECT * FROM Doktorlar WHERE Tc=@Tc AND Sifre=@Sifre", Baglan);
                Komut.Parameters.AddWithValue("@Tc", textBox1.Text);
                Komut.Parameters.AddWithValue("@Sifre", textBox2.Text);
                Komut.ExecuteNonQuery();
                OleDbDataReader Oku = Komut.ExecuteReader();
                if (Oku.Read())
                {
                    Form3 F3 = new Form3(textBox1.Text, textBox2.Text);
                    F3.Show();
                    this.Hide();
                    Doktor = true;
                    Kullanici = true;
                }
                Baglan.Close();
            }
            catch (Exception Hata)
            {
                Baglan.Close();
                MessageBox.Show(Hata.Message);
            }
        }
        private void Dur()
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
        }
        private void Devam()
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button1_Click(object sender, EventArgs e)
        {
            Admin();
            if (Admins == false)
            {
                Doctor();
            }
            if (Admins == false && Doktor == false)
            {
                User();
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e) { if (checkBox1.Checked) { textBox2.PasswordChar = '\0'; } else { textBox2.PasswordChar = '*'; } }

        private void button3_Click(object sender, EventArgs e)
        {
                        try
            {
                Baglan.Open();
                OleDbCommand Komut = new OleDbCommand("SELECT * FROM Hesaplar WHERE Tc=@Tc", Baglan);
                Komut.Parameters.AddWithValue("@Tc", textBox3.Text);
                OleDbDataReader Oku = Komut.ExecuteReader();
                if (Oku.Read())
                {
                    try
                    {
                        MailMessage Message = new MailMessage();
                        SmtpClient Client = new SmtpClient();

                        Client.Credentials = new System.Net.NetworkCredential("hastanesistem@outlook.com", "hastane1");
                        Client.Port = 587;
                        Client.Host = "smtp.outlook.com";
                        Client.EnableSsl = true;
                        SmtpClient server = new SmtpClient("outlook.com");

                        Message.Body = "Sayın " + Oku["Adi"].ToString() + " " + Oku["Soyadi"].ToString() + "\n\nParola hatırlatma talebiniz başarıyla oluşturuldu ve mail gönderildi.\nŞifreniz ve kimlik numaranız aşağıda belirtilmektedir." + "\n\nKimlik Numaranız: " + Oku["Tc"].ToString() + "\nŞifreniz: " + Oku["Sifre"].ToString() + "\n\nGüvenliğiniz için şifre değişikliğini 24 saat içinde yapmanızı rica ederiz." + "\nProgramımızı tercih ettiğiniz için teşekkür ederiz,\nSaygılarımızla";
                        Message.Subject = "Parola Hatırlama Talebiniz";
                        Message.From = new MailAddress("hastanesistem@outlook.com", "Hastane Otomasyon");
                        Message.To.Add(Oku["ePosta"].ToString());

                        Client.Send(Message);

                        MessageBox.Show("Başarıyla e-posta adresinize bilgileriniz gönderilmiştir.\nGüvenliğiniz için giriş yaptıktan sonra lütfen şifrenizi değiştirin.", "Hastane", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        panel1.Visible = false;
                        textBox3.Clear();
                        Devam();
                    }
                    catch (Exception Hata)
                    {
                        MessageBox.Show("Mail gönderilmedi : " + Hata.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Kimlik bilgisi bulunamadı veya eksik girildi.", "Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            Baglan.Close();
            Application.Exit();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            checkBox1.Visible = true;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            Dur();
        }
    }
}
