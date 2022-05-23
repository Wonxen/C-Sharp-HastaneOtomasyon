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
    public partial class Form14 : Form
    {
        public Form14()
        {
            InitializeComponent();
        }
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        Form1 F1 = new Form1();
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        bool Gonderildimi = true;
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        OleDbCommand Komut;
        OleDbDataReader Oku;
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        private void ParolaHatırlatma()
        {
            try
            {
                F1.Baglan.Open();
                Komut = new OleDbCommand("SELECT * FROM Hesaplar WHERE Tc='" + textBox1.Text + "'", F1.Baglan);
                Oku = Komut.ExecuteReader();
                Gonderildimi = false;
                if (Oku.Read())
                {
                    try
                    {
                        DialogResult D = MessageBox.Show(textBox1.Text + " Kimlik numaralı biligleri metin belgenize gönderilicek onaylıyor musunuz?", "Hastane", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (D == DialogResult.Yes)
                        {
                            using (System.IO.StreamWriter Dosya = new System.IO.StreamWriter("ŞifremiUnuttum.txt"))
                            {
                                String Metin = ("Merhaba " + Oku["Adi"].ToString() + " " + Oku["Soyadi"].ToString() + "\n\nŞifrenizi gönderdiniz ve şifrenizi göndermek için bir metin belgesi gönderildi.\nŞifreniz ve Kimlik numaranız aşağıda belirtilmektedir." + "\n\nKimlik Numaranız= " + Oku["Tc"].ToString() + "\nŞifreniz = " + Oku["Parola"].ToString() + "\n\nGüvenliğiniz için giriş yaptıktan sonra lütfen hemen şifrenizi değiştirin." + "\n\nHastane ekibiniz");
                                Dosya.WriteLine(Metin);
                            }
                            MessageBox.Show("Başarıyla metin belgesine bilgileriniz gönderilmiştir.\nGüvenliğiniz için giriş yaptıktan sonra lütfen şifrenizi değiştirin.","Hastane");
                            this.Close();
                        }
                        else
                        {
                            textBox1.Clear();
                        }
                    }
                    catch (Exception Hata)
                    {
                        MessageBox.Show("Hata: " + Hata.Message);
                    }  
                }
                else
                {
                    MessageBox.Show("Kimlik bilgisi bulunamadı veya eksik girildi.","Hastane");
                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Hata: " + Hata.Message);
            }
            F1.Baglan.Close();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button1_Click(object sender, EventArgs e)
        {
            ParolaHatırlatma();
        }
    }
}
