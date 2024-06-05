using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticDemoExam
{
    public partial class WorkerForm : Form
    {
        int userId;
        public WorkerForm(int id)
        {
            userId = id;
            InitializeComponent();
            loadTable();
        }
         
        private void loadTable()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connection))
            {
                using (SqlCommand cmd = new SqlCommand("Select Orders.*, Users.name FROM Orders, Users WHERE workerOrd=@id and Users.id=@id", connection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = userId;
                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                        DataTable ds = new DataTable();
                        adapter.Fill(ds);
                        fillData.DataSource = ds;
                    }
                    catch
                    {
                        MessageBox.Show("Что-то пошло не так");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form addForm = new addForm(userId);
            addForm.FormClosed += (object s, FormClosedEventArgs ev) => { this.Show(); loadTable(); };
            addForm.Show();
        }

        private void fillData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connection))
            {
                if (textBox1.Text.Length == 0)
                {
                    loadTable();
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT Orders.*, Users.name FROM Orders,Users WHERE Users.Id=@id and IdOrders = @search;", connection))
                    {
                        cmd.Parameters.Add("@search", SqlDbType.Int);
                        cmd.Parameters["@search"].Value = Int32.Parse(textBox1.Text);

                        cmd.Parameters.Add("@id", SqlDbType.Int);
                        cmd.Parameters["@id"].Value = userId;
                        try
                        {
                            connection.Open();
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                            DataTable ds = new DataTable();
                            adapter.Fill(ds);
                            fillData.DataSource = ds;
                        }
                        catch
                        {
                            MessageBox.Show("Что-то пошло не так");
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }
    }
}
