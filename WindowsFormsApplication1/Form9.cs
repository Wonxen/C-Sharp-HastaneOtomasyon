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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        Form1 F1 = new Form1();
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        OleDbCommand Komut;
        OleDbDataReader Oku;
        //--------------------------------------------------//--------------------------------------------------//--------------------------------------------------
        private void DoktorListele()
        {
            listView1.Items.Clear();
            try
            {
                Komut = new OleDbCommand("SELECT * FROM Doktorlar", F1.Baglan);
                Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    listView1.Items.Add(Oku["Tc"].ToString());
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add(Oku["AdiSoyadi"].ToString());
                    listView1.Items[listView1.Items.Count - 1].SubItems.Add(Oku["Klinikid"].ToString());
                }
                F1.Baglan.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Hata:" + Hata.Message);
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void Form9_Load(object sender, EventArgs e)
        {
            F1.Baglan.Open();
            DoktorListele();
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button1_Click(object sender, EventArgs e)
        {
            F1.Baglan.Open();
            string Kimlik = "";
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem itm = listView1.SelectedItems[0];
                Kimlik = itm.SubItems[0].Text;
            }
            try
            {
                listView1.SelectedItems.ToString();
                Komut = new OleDbCommand("DELETE * FROM Doktorlar WHERE Tc='" + Kimlik + "'", F1.Baglan);
                Komut.ExecuteNonQuery();
                DoktorListele();
            }
            catch (Exception Hata)
            {
                MessageBox.Show("Hata:" + Hata.Message);
            }
        }
        //——————————————————————————————————————————————————|——————————————————————————————————————————————————\\
        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            DoktorListele();
        }
    }
}
