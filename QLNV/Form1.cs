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

namespace QLNV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string flag = " ";

        string sql = @"Data Source=TRINHTHITHANH;Initial Catalog=QLNV;Integrated Security=True";
        private void btnwatch_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = getPhanCong().Tables[0];
        }
        public DataSet getPhanCong()
        {
            DataSet data = new DataSet();
            
            using (SqlConnection connection=new SqlConnection(sql))
            {
                connection.Open();
                string query = " select * from PhanCong";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(data);
                connection.Close();
            }

            return data;
        }
        

        private void button1_Click(object sender, EventArgs e)

        {
            flag = "them";
            txtmada.Enabled = true;
            txtmanv.Enabled = true;
            txttg.Enabled = true;
            
            
        }
         public void add()

        {
           
            using (SqlConnection connection = new SqlConnection(sql))
            {
                connection.Open();
                
                string query = "insert into PhanCong values('" + txtmanv.Text + "','" + txtmada.Text + "'," +float.Parse( txttg.Text)+ ")";
                SqlCommand cmd = new SqlCommand(query,connection);
                cmd.ExecuteNonQuery();
             
                connection.Close();


            
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            flag = "xoa";
            txtmada.Enabled = false;
            txtmanv.Enabled = false;
            txttg.Enabled = false;
            if (MessageBox.Show("ban co that su muon xoa", " thong bao", MessageBoxButtons.YesNoCancel) == System.Windows.Forms.DialogResult.Yes)
            {
                xoa();
                dataGridView1.DataSource = getPhanCong().Tables[0];
            }
        }
       
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
           int  index = dataGridView1.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dataGridView1.DataSource;
            if (dt.Rows.Count > 0)
            {
                txtmanv.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
                txtmada.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
                txttg.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
            }
        }

        private void btnsua_Click(object sender, EventArgs e)
        {



            flag = "sua";
            txtmada.Enabled = false;
            txtmanv.Enabled = false;
            txttg.Enabled = true;
            



        }
        public void SUA()

        {

            using (SqlConnection connection = new SqlConnection(sql))
            {
                connection.Open();

                string query = "UPDATE  PhanCong set  sogio=" + float.Parse(txttg.Text) + "where MaNV='"+txtmanv.Text+"' and MaDA='"+txtmada.Text+"'" ;
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();

                connection.Close();



            }

        }
        public void xoa()
        {
            using (SqlConnection connection = new SqlConnection(sql))
            {
                connection.Open();

                string query = "delete phancong  where MaNV='" + txtmanv.Text + "' and MaDA='" + txtmada.Text + "'";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();

                connection.Close();



            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (flag == "them")
                add();
            if (flag == "sua")
                SUA();
            
            dataGridView1.DataSource = getPhanCong().Tables[0];
        }
    }
}
