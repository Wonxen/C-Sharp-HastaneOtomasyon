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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        Form1 F1 = new Form1();
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        OleDbCommand Komut;
        OleDbDataReader Oku;
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        private void KullaniciListele()
        {
            listView1.Items.Clear();
            F1.Baglan.Open();
            try
            {
                Komut = new OleDbCommand("SELECT * FROM Hesaplar", F1.Baglan);
                Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    listView1.Items.Add(Oku["Tc"].ToString());
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add(Oku["Adi"].ToString());
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add(Oku["Soyadi"].ToString());
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add(Oku["Cinsiyeti"].ToString());
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Hata:" + Hata.Message);
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void Form8_Load(object sender, EventArgs e)
        {
            KullaniciListele();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button1_Click(object sender, EventArgs e)
        {
            string Kimlik = "";
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem itm = listView1.SelectedItems[0];
                Kimlik = itm.SubItems[0].Text;
            }
            try
            {
                F1.Baglan.Open();
                DialogResult D = MessageBox.Show("Seçtiğiniz " + Kimlik + " numaralı hasta kaydını silmek istediğinize eminmisiniz?", "Admin Paneli", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (D == DialogResult.Yes)
                {
                    Komut = new OleDbCommand("DELETE * FROM Hesaplar WHERE Tc='" + Kimlik + "'", F1.Baglan);
                    Komut.ExecuteNonQuery();
                    MessageBox.Show("Hasta kayıtı başarıyla silinmiştir!");
                    F1.Baglan.Close();
                    KullaniciListele();
                }
                else
                {
                    MessageBox.Show("Hasta kayıt silme işlemi iptal edildi!");
                    F1.Baglan.Close();
                }
            }
            catch (Exception Hata)
            {
                F1.Baglan.Close();
                MessageBox.Show("Hata:" + Hata.Message);
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            KullaniciListele();
        }
    }
}
