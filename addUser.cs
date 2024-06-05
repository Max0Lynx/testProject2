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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PracticDemoExam
{
    public partial class addUser : Form
    {
        int globID;
        int role;
        string parseRole;
        public addUser(int id)
        {
            InitializeComponent();
            globID = id;
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connection))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE id=@id", connection))
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
                                    textBox1.Text = reader.GetValue(1).ToString();
                                    textBox2.Text = reader.GetValue(2).ToString();
                                    textBox3.Text = reader.GetValue(3).ToString();
                                    textBox4.Text = reader.GetValue(4).ToString();
                                switch (reader.GetValue(5).ToString())
                                {
                                    case "2":
                                        parseRole = "Менеджер";
                                        break;
                                    case "3":
                                        parseRole = "Работник";
                                        break;
                                }
                                comboBox2.Text = parseRole;
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (globID != 0)
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connection))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE Users SET login=@login, password=@password, name=@name, role=@role WHERE id = @id", connection))
                    {
                        cmd.Parameters.Add("@id", SqlDbType.Int);
                        cmd.Parameters["@id"].Value = globID;

                        cmd.Parameters.Add("@login", SqlDbType.NVarChar);
                        cmd.Parameters["@login"].Value = textBox1.Text.ToString();

                        cmd.Parameters.Add("@password", SqlDbType.NVarChar);
                        cmd.Parameters["@password"].Value = textBox2.Text.ToString();

                        cmd.Parameters.Add("@name", SqlDbType.NVarChar);
                        cmd.Parameters["@name"].Value = textBox3.Text.ToString();

                        cmd.Parameters.Add("@surname", SqlDbType.NVarChar);
                        cmd.Parameters["@surname"].Value = textBox3.Text.ToString();

                        switch (comboBox2.SelectedItem.ToString())
                        {
                            case "Работник":
                                role = 3;
                                break;
                            case "Менеджер":
                                role = 2;
                                break;
                        }
                        cmd.Parameters.Add("@role", SqlDbType.VarChar);
                        cmd.Parameters["@role"].Value = role;

                        try
                        {
                            connection.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Успешно!");
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
            else
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connection))
                {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Users (login, password, name, surname, role) VALUES (@login, @password, @name, @surname, @role)", connection))
                        {
                            cmd.Parameters.Add("@login", SqlDbType.NVarChar);
                            cmd.Parameters["@login"].Value = textBox1.Text.ToString();

                            cmd.Parameters.Add("@password", SqlDbType.NVarChar);
                            cmd.Parameters["@password"].Value = textBox2.Text.ToString();

                            cmd.Parameters.Add("@name", SqlDbType.NVarChar);
                            cmd.Parameters["@name"].Value = textBox3.Text.ToString();

                            cmd.Parameters.Add("@surname", SqlDbType.NVarChar);
                            cmd.Parameters["@surname"].Value = textBox3.Text.ToString();

                        switch (comboBox2.SelectedItem.ToString())
                        {
                            case "Работник":
                                role = 2;
                                break;
                            case "Менеджер":
                                role = 3;
                                break;
                        }
                            cmd.Parameters.Add("@role", SqlDbType.Int);
                            cmd.Parameters["@role"].Value = role;

                            try
                            {
                                connection.Open();
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Успешно!");
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

        private void addUser_Load(object sender, EventArgs e)
        {

        }
    }
    }
