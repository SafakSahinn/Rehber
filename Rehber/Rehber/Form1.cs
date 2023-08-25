using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Rehber
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-S866UD2;Initial Catalog=Rehber;Integrated Security=True");

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM KISILER",baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        
        void temizle()
        {
            TxtId.Text = "";
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            TxtMail.Text = "";
            MskTelefon.Text = "";
            TxtAd.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO KISILER (AD,SOYAD,TELEFON,MAIL) VALUES (@P1,@P2,@P3,@P4)",baglanti);
            komut.Parameters.AddWithValue("@P1",TxtAd.Text);
            komut.Parameters.AddWithValue("@P2",TxtSoyad.Text);
            komut.Parameters.AddWithValue("@P3",MskTelefon.Text);
            komut.Parameters.AddWithValue("@P4",TxtMail.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            temizle();
            listele();
            MessageBox.Show("Kişi Eklendi!");

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("DELETE FROM KISILER WHERE ID=" + TxtId.Text,baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            temizle();
            listele();
            MessageBox.Show("Kişi Silindi!");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            TxtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            MskTelefon.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtMail.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("UPDATE KISILER SET AD=@P1,SOYAD=@P2,TELEFON=@P3,MAIL=@P4 WHERE ID=@P5",baglanti);
            komut.Parameters.AddWithValue("@P1",TxtAd.Text);
            komut.Parameters.AddWithValue("@P2",TxtSoyad.Text);
            komut.Parameters.AddWithValue("@P3",MskTelefon.Text);
            komut.Parameters.AddWithValue("@P4",TxtMail.Text);
            komut.Parameters.AddWithValue("@P5",TxtId.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            temizle();
            listele();
            MessageBox.Show("Kişi Bilgileri Güncellendi!");
        }
    }
}
