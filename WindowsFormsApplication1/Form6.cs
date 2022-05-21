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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        public Form6(string tc, string parola)
        {
            InitializeComponent();
            TcKimlik = tc;
            Parola = parola;
        }
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        string TcKimlik = "";
        string Parola = "";
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        Form1 F1 = new Form1(); 
        Form2 F2 = new Form2();
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        OleDbCommand Komut; 
        OleDbDataReader Oku;
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        bool ParolaKontrol = false;
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        private void BilgiGuncelle()
        {
            try
            {
                DialogResult D = MessageBox.Show(textBox1.Text + " kimlik numaralı kaydınızda bilgiler düzenlensin mi?", "Hastane", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (D == DialogResult.Yes)
                {
                    Komut = new OleDbCommand("UPDATE Hesaplar SET Adi=@Adi,Soyadi=@Soyadi,Cinsiyeti=@Cinsiyeti,DogumYeri=@DogumYeri,DogumTarihi=@DogumTarihi,Parola=@Parola WHERE tc='" + F1.textBox1.Text + "' AND parola='" + F1.textBox2.Text + "'", F1.Baglan);
                    Komut.Parameters.AddWithValue("@Adi", textBox2.Text);
                    Komut.Parameters.AddWithValue("@Soyadi", textBox3.Text);
                    Komut.Parameters.AddWithValue("@Cinsiyeti", comboBox1.SelectedItem.ToString());
                    Komut.Parameters.AddWithValue("@DogumYeri", textBox4.Text);
                    Komut.Parameters.AddWithValue("@DogumTarihi", textBox5.Text);
                    Komut.Parameters.AddWithValue("@Parola", textBox6.Text);
                    Komut.ExecuteNonQuery();
                    MessageBox.Show("İşlem başarıyla gerçekleşti veriler güncellendi!");
                    F1.Baglan.Close();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("İşlem başarıyla iptal edildi!");
                    F1.Baglan.Close();
                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Hata:" + Hata.Message);
                F1.Baglan.Close();
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void Bilgiler()
        {
            try
            {
                F1.Baglan.Open();
                Komut = new OleDbCommand("SELECT * FROM Hesaplar WHERE Tc='" + TcKimlik + "'", F1.Baglan);
                Komut.ExecuteNonQuery();
                Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    textBox1.Text = F1.textBox1.Text;
                    textBox2.Text = Oku["Adi"].ToString();
                    textBox3.Text = Oku["Soyadi"].ToString();
                    comboBox1.SelectedItem = Oku["Cinsiyeti"].ToString();
                    textBox4.Text = Oku["DogumYeri"].ToString();
                    textBox5.Text = Oku["DogumTarihi"].ToString();
                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.ToString());
            }
        } 
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void Form6_Load(object sender, EventArgs e)
        {
            Bilgiler();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button1_Click(object sender, EventArgs e)
        {
            ErrorProvider provider = new ErrorProvider();
            if (textBox6.Text == "")
            {
                provider.SetError(textBox6, "Boş geçilemez!");
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
                BilgiGuncelle();
            }
        }
    }
}


