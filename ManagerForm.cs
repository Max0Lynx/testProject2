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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.Mail;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Runtime.Remoting.Messaging;
using System.Xml.Linq;

namespace PracticDemoExam
{
    public partial class ManagerForm : Form
    {
        int idWorker;
        int id;
        string nameManager, email;
        public ManagerForm(int id)
        {
            InitializeComponent();
            loadTable();
            comboBox1.Visible = false;
            textBox1.Visible = false;
            comboBox2.Visible = false;
            label1.Visible = false;
            label2.Visible = false; 
            label3.Visible = false;
            btnAdd.Visible = false;
            idWorker= id;
            getUser();
        }
        private void getUser() {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connection))
            {
                using (SqlCommand cmd = new SqlCommand("Select name FROM Users WHERE role=3", connection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    comboBox1.Items.Add(reader.GetValue(i).ToString());
                                }
                               
                            }
                        }
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
        private void loadTable()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connection))
            {
                using (SqlCommand cmd = new SqlCommand("Select Orders.*, Users.name FROM Orders, Users WHERE workerOrd=Users.Id", connection))
                {
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

        private void generateUsers()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connection))
            {
                using (SqlCommand cmd = new SqlCommand("Select * FROM Users WHERE Id=@id", connection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = idWorker;
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                nameManager = reader["name"].ToString();
                            }
                        }

                    }
                    catch
                    {
                        MessageBox.Show("Что-то пошло не так");
                    }
                    connection.Close();
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fillData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAdd.Visible = true;
            comboBox1.Visible = true;
            textBox1.Visible = true;
            comboBox2.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connection))
            {
                using (SqlCommand cmd = new SqlCommand("Select Orders.*, Users.name FROM Orders, Users WHERE IdOrders=@idOrd and Users.Id=@id", connection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = fillData.CurrentRow.Cells[9].Value;

                    cmd.Parameters.Add("@idOrd", SqlDbType.Int);
                    cmd.Parameters["@idOrd"].Value = fillData.CurrentRow.Cells[0].Value;
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                textBox1.Text = reader["dateFinish"].ToString();
                                comboBox1.Text = reader["name"].ToString();
                                comboBox2.Text = reader["status"].ToString();
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Что-то пошло не так");
                    }
                    connection.Close();
                }
            }
        }
        private void newNameWork(string name)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connection))
            {
                using (SqlCommand cmd = new SqlCommand("Select Id FROM Users WHERE name=@name;", connection))
                {
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar);
                    cmd.Parameters["@name"].Value = name;
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                id=(Int32)reader.GetValue(0);
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Что-то пошло не так");
                    }
                    connection.Close();
                }
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            email = fillData.CurrentRow.Cells[11].Value.ToString();
            generateUsers();
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connection))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Orders SET dateFinish=@dateFinish, status=@status, workerOrd=@workerOrd WHERE IdOrders=@idOrders", connection))
                { 
                    string status = fillData.CurrentRow.Cells[7].ToString();
                    cmd.Parameters.Add("@dateFinish", SqlDbType.NVarChar);
                    cmd.Parameters["@dateFinish"].Value = textBox1.Text.ToString();

                    cmd.Parameters.Add("@status", SqlDbType.NVarChar);
                    cmd.Parameters["@status"].Value = comboBox2.SelectedItem.ToString();

                    newNameWork(comboBox1.SelectedItem.ToString());
                    
                    cmd.Parameters.Add("@workerOrd", SqlDbType.Int);
                    cmd.Parameters["@workerOrd"].Value = id;

                    cmd.Parameters.Add("@idOrders", SqlDbType.Int);
                    cmd.Parameters["@idOrders"].Value = (Int32)fillData.CurrentRow.Cells[0].Value;
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (status != comboBox2.SelectedItem.ToString())
                        {
                            MailAddress from = new MailAddress("olga.rysaefa@gmail.com", nameManager);

                            MailAddress to = new MailAddress(email);

                            MailMessage m = new MailMessage(from, to);

                            m.Subject = "Статус вашей заявки изменен!";

                            m.Body = "<h2>Здравствуйте! Изменился статус вашей заявки на "+ comboBox2.SelectedItem.ToString() + "!</h2>";

                            m.IsBodyHtml = true;

                            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

                            smtp.Credentials = new NetworkCredential("olga.rysaefa@gmail.com", "yrgp kqtb ygjy izsm");
                            smtp.EnableSsl = true;
                            smtp.Send(m);
                            MessageBox.Show("Письмо успешно отправлено!");
                            status = "";
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Что-то пошло не так");
                    }
                    connection.Close();
                    loadTable();
                    comboBox1.Visible = false;
                    textBox1.Visible = false;
                    comboBox2.Visible = false;
                    label1.Visible = false;
                    label2.Visible = false;
                    label3.Visible = false;
                    btnAdd.Visible = false;
                    idWorker = id;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connection))
            {
                if (textBox2.Text.Length == 0)
                {
                    loadTable();
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT Orders.*, Users.name FROM Orders,Users WHERE IdOrders = @search and workerOrd=Users.Id;", connection))
                    {
                        cmd.Parameters.Add("@search", SqlDbType.Int);
                        cmd.Parameters["@search"].Value = Int32.Parse(textBox2.Text);
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

        private void button1_Click(object sender, EventArgs e)
        {
            Form qr = new QR();
            qr.ShowDialog();
        }
    }
}
