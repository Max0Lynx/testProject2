using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticDemoExam
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }
        int gID=0;
        private void AdminForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dBdemoExamDataSet.Users". При необходимости она может быть перемещена или удалена.
            this.usersTableAdapter.Fill(this.dBdemoExamDataSet.Users);

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form addForm = new addUser(gID);
            addForm.FormClosed += (object s, FormClosedEventArgs ev) => { this.Show(); };
            addForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connection))
            {

                using (SqlCommand cmd = new SqlCommand("SELECT FROM Users WHERE id=@id", connection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = fillData.CurrentRow.Cells[0].Value;

                    try
                    {
                        Form editForm = new addUser(int.Parse(fillData.CurrentRow.Cells[0].Value.ToString()));
                        editForm.FormClosed += (object s, FormClosedEventArgs ev) => { this.Show(); };
                        editForm.ShowDialog();
                    }
                    catch
                    {
                        MessageBox.Show("Что-то пошло не так");
                    }
                    connection.Close();

                }
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connection))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE id=@id", connection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = fillData.CurrentRow.Cells[0].Value;
                    try
                    {
                        connection.Open();

                        cmd.ExecuteNonQuery();
                        AdminForm_Load(sender, e);
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

        private void btn_update_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connection))
            {
                using (SqlCommand cmd = new SqlCommand("Select * FROM Users", connection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = gID;
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
