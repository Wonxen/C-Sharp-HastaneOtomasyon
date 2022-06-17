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
        public Form8(string tc)
        {
            InitializeComponent();
            Tc = tc;
        }
        string Tc = "";
        Form1 F1 = new Form1();
        private void Randevular()
        {
            try
            {
                F1.Baglan.Open();
                OleDbCommand Komut = new OleDbCommand("SELECT * FROM Randevular WHERE Tc=@Tc ORDER BY Randevuid DESC", F1.Baglan);
                Komut.Parameters.AddWithValue("@Tc", Tc);
                Komut.ExecuteNonQuery();
                OleDbDataReader Oku = Komut.ExecuteReader();
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
                    label1.Text = "Sistemde toplam " + i.ToString() + " randevu bilgisi mevcut.";
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.ToString());
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string[] idler = new string[listView1.CheckedItems.Count];
            for (int i = 0; i < listView1.CheckedItems.Count; i++)
            {
                idler[i] = listView1.CheckedItems[i].Text;
                try
                {
                    F1.Baglan.Open();
                    OleDbCommand Komut = new OleDbCommand("DELETE * FROM Randevular WHERE Randevuid=@id", F1.Baglan);
                    Komut.Parameters.AddWithValue("@id", listView1.CheckedItems[i].SubItems[1].Text);
                    Komut.ExecuteNonQuery();
                    F1.Baglan.Close();
                    listView1.Items.Clear();
                    Randevular();
                    this.Close();
                }
                catch (ArgumentOutOfRangeException)
                {
                    F1.Baglan.Close();
                    MessageBox.Show("İptal edilecek bir randevu belirtiniz.", "Hastane", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            Randevular();
            this.CenterToScreen();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            F1.Baglan.Close();
            this.Close();
        }
    }
}
