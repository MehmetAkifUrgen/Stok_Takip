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

namespace Stoks
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection sqlConnection = new SqlConnection("Data Source=DESKTOP-TMGKHKI\\SQLEXPRESS;Initial Catalog=myDatabase;Integrated Security=True");
        private void işlemlerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'myDatabaseDataSet1.MainTable' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.mainTableTableAdapter.Fill(this.myDatabaseDataSet1.MainTable);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.mainTableTableAdapter.Fill(this.myDatabaseDataSet1.MainTable);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("insert into MainTable (Ad,Cins,Fiyat,Adet,Saglayici,Aciklama) values (@p1,@p2,@p3,@p4,@p5,@p6)", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@p1", adText.Text);
                sqlCommand.Parameters.AddWithValue("@p2", cinsText.Text);
                sqlCommand.Parameters.AddWithValue("@p3", fiyatText.Text);
                sqlCommand.Parameters.AddWithValue("@p4", adetText.Text);
                sqlCommand.Parameters.AddWithValue("@p5", sagText.Text);
                sqlCommand.Parameters.AddWithValue("@p6", acikText.Text);

                if (adText.Text == "" ||
                    cinsText.Text == "" ||
                    fiyatText.Text == "" ||
                    adetText.Text == "" ||
                    sagText.Text == "" ||
                    acikText.Text == "")
                {
                    MessageBox.Show("Hiç bir satır boş olamaz!!");
                }
                else
                {
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Kayıt Eklendi.");
                }


                sqlConnection.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
            adText.Clear(); cinsText.Clear(); fiyatText.Clear(); adetText.Clear(); sagText.Clear(); acikText.Clear();

        }

        private void fiyatText_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("lütfen sayı girin.");
            }
        }

        private void adetText_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 8)
            {
                e.Handled = true;
                MessageBox.Show("lütfen sayı girin.");
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int chosen = dataGridView1.SelectedCells[0].RowIndex;
            adText.Text = dataGridView1.Rows[chosen].Cells[0].Value.ToString();
            cinsText.Text = dataGridView1.Rows[chosen].Cells[1].Value.ToString();
            fiyatText.Text = dataGridView1.Rows[chosen].Cells[2].Value.ToString();
            adetText.Text = dataGridView1.Rows[chosen].Cells[3].Value.ToString();
            sagText.Text = dataGridView1.Rows[chosen].Cells[4].Value.ToString();
            acikText.Text = dataGridView1.Rows[chosen].Cells[5].Value.ToString();
            updaText.Text = dataGridView1.Rows[chosen].Cells[0].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("Delete from MainTable where Ad=@k1", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@k1", adText.Text);
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show(adText.Text + " adlı kayıt Silindi.");
                sqlConnection.Close();

            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
            adText.Clear(); cinsText.Clear(); fiyatText.Clear(); adetText.Clear(); sagText.Clear(); acikText.Clear(); araText.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("Update MainTable  Set Ad=@a1,Cins=@a2,Fiyat=@a3,Adet=@a4,Saglayici=@a5,Aciklama=@a6 where Ad=@k1", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@a1", adText.Text);
                sqlCommand.Parameters.AddWithValue("@a2", cinsText.Text);
                sqlCommand.Parameters.AddWithValue("@a3", fiyatText.Text);
                sqlCommand.Parameters.AddWithValue("@a4", adetText.Text);
                sqlCommand.Parameters.AddWithValue("@a5", sagText.Text);
                sqlCommand.Parameters.AddWithValue("@a6", acikText.Text);
                sqlCommand.Parameters.AddWithValue("@k1", updaText.Text);
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Kayıt Güncellendi.");
                sqlConnection.Close();

            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
            adText.Clear(); cinsText.Clear(); fiyatText.Clear(); adetText.Clear(); sagText.Clear(); acikText.Clear(); araText.Clear();
        }

        private void araText_TextChanged(object sender, EventArgs e)
        {
            sqlConnection.Open();
            DataTable tbl = new DataTable();
            string vara, cumle;
            vara = araText.Text;
            cumle = "Select * from MainTable where Ad like '%" + araText.Text + "%'";
            SqlDataAdapter adptr = new SqlDataAdapter(cumle, sqlConnection);
            adptr.Fill(tbl);
            sqlConnection.Close();
            dataGridView1.DataSource = tbl;
        }
    }
}
