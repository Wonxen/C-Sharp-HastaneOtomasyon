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
    public partial class Form12 : Form
    {
        public Form12()
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
        private void Form12_Load(object sender, EventArgs e)
        {
            KlinikListeleme();
        }
    }
}
