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

namespace PracticDemoExam
{
    public partial class authForm : Form
    {
        public authForm()
        {
            InitializeComponent();
        }
       
           

        private void btnReg_Click(object sender, EventArgs e)
        {
           
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void butAuth_Click_1(object sender, EventArgs e)
        {
            string username = txtLogin.Text;
            string password = txtPass.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, введите имя пользователя и пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connection))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM Users WHERE login = @username AND password = @password;";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);
                        SqlDataReader reader = command.ExecuteReader();

                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Неверный логин или пароль");
                            reader.Close();
                        }
                        else
                        {
                            reader.Read();
                            int id = int.Parse(reader.GetValue(0).ToString());
                            switch (reader.GetString(5))
                            {
                                case "Admin":
                                    MessageBox.Show("Успешный вход!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Hide();
                                    Form adminForm = new AdminForm();
                                    adminForm.FormClosed += (object s, FormClosedEventArgs ev) => { this.Show(); };
                                    adminForm.Show();
                                    break;
                                case "Master":
                                    MessageBox.Show("Успешный вход!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Hide();
                                    WorkerForm userForm = new WorkerForm(id);
                                    userForm.FormClosed += (object s, FormClosedEventArgs ev) => { this.Show(); };
                                    userForm.Show();
                                    break;
                                case "Operator":
                                    MessageBox.Show("Успешный вход!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Hide();
                                    Form managerForm = new ManagerForm(id);
                                    managerForm.FormClosed += (object s, FormClosedEventArgs ev) => { this.Show(); };
                                    managerForm.Show();
                                    break;
                                case "User":
                                    MessageBox.Show("Успешный вход!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Hide();
                                    Form UserForm = new UserForm(id);
                                    UserForm.FormClosed += (object s, FormClosedEventArgs ev) => { this.Show(); };
                                    UserForm.Show();
                                    break;
                                default:
                                    MessageBox.Show("Что-то пошло не так");
                                    break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnReg_Click_1(object sender, EventArgs e)
        {
            string username = nameTxt.Text;
            string password = passwordRegTxt.Text;
            string phoneNumber = phoneNumberTxt.Text;
            string login = loginTxt.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Проверка формата номера телефона (только цифры)
            foreach (char c in phoneNumber)
            {
                if (!char.IsDigit(c))
                {
                    MessageBox.Show("Пожалуйста, введите только цифры.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connection))
            {
                try
                {
                    connection.Open();

                    string query = "INSERT INTO Users (name, password, phoneNumber, role, login) VALUES (@Username, @Password, @PhoneNumber, @Role, @Login)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        command.Parameters.AddWithValue("@Role", "User");
                        command.Parameters.AddWithValue("@Login", login);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Успешная регистрация!", "Успешно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    nameTxt.Clear();
                    passwordRegTxt.Clear();
                    phoneNumberTxt.Clear();
                    loginTxt.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
