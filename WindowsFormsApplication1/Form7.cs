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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        public Form7(string tc)
        {
            InitializeComponent();
            TcKimlik = tc;
        }
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        string TcKimlik = "";
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        Form1 F1 = new Form1();
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        OleDbCommand Komut;
        OleDbDataReader Oku;
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        private void AlinmisRandevuListele()
        {
            listView1.Items.Clear();
            try
            {
                F1.Baglan.Open();
                Komut = new OleDbCommand("SELECT * FROM Randevu WHERE Tc='" + TcKimlik + "'",F1.Baglan);
                Komut.ExecuteNonQuery();
                Oku = Komut.ExecuteReader();
                int i = 0;
                while (Oku.Read())
                {
                    i++;
                    listView1.Items.Add(i.ToString());
                    listView1.Items[i - 1].SubItems.Add(Oku["KlinikAdi"].ToString());
                    listView1.Items[i - 1].SubItems.Add(Oku["DoktorAdi"].ToString());
                    listView1.Items[i - 1].SubItems.Add(Oku["Tarih"].ToString());
                    listView1.Items[i - 1].SubItems.Add(Oku["Saat"].ToString());
                    listView1.Items[i - 1].SubItems.Add(Oku["Randevuid"].ToString());
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Hata: " + Hata.Message);
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void Form7_Load(object sender, EventArgs e)
        {
            AlinmisRandevuListele();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button1_Click(object sender, EventArgs e)
        {
            string[] idler = new string[listView1.CheckedItems.Count];

            for (int i = 0; i < listView1.CheckedItems.Count; i++)
            {
                idler[i] = listView1.CheckedItems[i].Text;
                try
                {
                    F1.Baglan.Open();
                    Komut = new OleDbCommand("DELETE * FROM Randevu WHERE Randevuid=@id",F1.Baglan);
                    Komut.Parameters.AddWithValue("@id", listView1.CheckedItems[i].SubItems[1].Text);
                    Komut.ExecuteNonQuery();
                    F1.Baglan.Close();
                }
                catch (Exception Hata)
                {
                    MessageBox.Show("Hata: " + Hata.Message);
                }

            }
            AlinmisRandevuListele();
        }
    }
}
