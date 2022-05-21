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
    public partial class Form13 : Form
    {
        public Form13()
        {
            InitializeComponent();
        }
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        Form1 F1 = new Form1();
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        OleDbCommand Komut;
        OleDbDataReader Oku;
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        private void KlinikListeleme()
        {
            try
            {
                F1.Baglan.Open();
                Komut = new OleDbCommand("SELECT * FROM Klinik", F1.Baglan);
                Komut.ExecuteNonQuery();
                Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    listBox1.Items.Add(Oku["KlinikAdi"].ToString());
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Hata: " + Hata.Message);
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void KlinikSilme()
        {
            try
            {
                listBox1.SelectedItem.ToString();
                DialogResult D = MessageBox.Show(listBox1.SelectedItem.ToString() + " isimli klinik bilgisini silmek istediğinize emin misiniz?", "ADMIN PANEL", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (D == DialogResult.Yes)
                {
                    F1.Baglan.Open();
                    Komut = new OleDbCommand("DELETE * FROM Klinik WHERE KlinikAdi=@KlinikAdi", F1.Baglan);
                    Komut.Parameters.AddWithValue("@KlinikAdi", listBox1.SelectedItem.ToString());
                    Komut.ExecuteNonQuery();
                    F1.Baglan.Close();
                    listBox1.Items.Clear();
                    KlinikListeleme();
                }
                else
                {
                    MessageBox.Show("Silme işlemi iptal edildi!","ADMIN PANEL");
                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Hata: " + Hata.Message);
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void Form13_Load(object sender, EventArgs e)
        {
            KlinikListeleme();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button1_Click(object sender, EventArgs e)
        {
            KlinikSilme();
        }
    }
}
