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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        Form1 F1 = new Form1();
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        OleDbCommand Komut;
        OleDbDataReader Oku;
        OleDbDataAdapter Yaz;
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        private void Klinik()
        {
            Yaz = new OleDbDataAdapter("SELECT * FROM Klinik", F1.Baglan);
            DataTable tablo = new DataTable();
            Yaz.Fill(tablo);
            dataGridView1.DataSource = tablo;
            F1.Baglan.Close();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult D = MessageBox.Show("Bilgileri verilen " + textBox2.Text + " isimli doktorun kaydı gerçekleştirilsinmi?", "Admin Panel", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (D == DialogResult.Yes)
                {
                    F1.Baglan.Open();
                    Komut = new OleDbCommand("INSERT INTO Doktorlar(Tc,AdiSoyadi,Klinikid,Parola) VALUES (@Tc,@AdiSoyadi,@Klinikid,@Parola)", F1.Baglan);
                    Komut.Parameters.AddWithValue("@TC",textBox1.Text.ToString());
                    Komut.Parameters.AddWithValue("@AdiSoyadi", textBox2.Text);
                    Komut.Parameters.AddWithValue("@Klinikid", textBox3.Text);
                    Komut.Parameters.AddWithValue("@Parola", textBox5.Text);
                    Oku = Komut.ExecuteReader();
                    F1.Baglan.Close();
                    //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox5.Text = "";
                }
                else
                {
                    MessageBox.Show("Kaydınız iptal edilmiştir girilen tüm bilgiler silinecektir!", "Admin Panel");
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
                MessageBox.Show("Hata:" + Hata.Message);
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void Form10_Load(object sender, EventArgs e)
        {
            Klinik();
        }
    }
}
