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

namespace PracticDemoExam
{
    public partial class addForm : Form
    {
        int workerID;
        public addForm(int id)
        {
            InitializeComponent();
            workerID = id;
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button_enable()
        {
            if(textBox1.Text!="" && textBox2.Text!="" && textBox3.Text!="" && textBox4.Text!="" && textBox5.Text!="" && textBox6.Text!="" && textBox7.Text != "")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime thisDay = DateTime.Today;
            DateTime day2 = thisDay.AddDays(3);
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connection))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Orders (nameEquip, equipBroke, dateAdd, dateFinish, commentWorker, status, problemType, clientName, workerOrd, clientSurname, email) VALUES (@nameEquip, @equipBroke, @dateAdd, @dateFinish, @commentWorker, @status, @problemType, @clientName, @workerOrd, @clientSurname, @email)", connection))
                {
                    cmd.Parameters.Add("@nameEquip", SqlDbType.NVarChar);
                    cmd.Parameters["@nameEquip"].Value = textBox1.Text.ToString();

                    cmd.Parameters.Add("@equipBroke", SqlDbType.NVarChar);
                    cmd.Parameters["@equipBroke"].Value = textBox2.Text.ToString();

                    cmd.Parameters.Add("@dateAdd", SqlDbType.NVarChar);
                    cmd.Parameters["@dateAdd"].Value = thisDay.ToString("D");

                    cmd.Parameters.Add("@dateFinish", SqlDbType.NVarChar);
                    cmd.Parameters["@dateFinish"].Value = day2.ToString("D");

                    cmd.Parameters.Add("@problemType", SqlDbType.NVarChar);
                    cmd.Parameters["@problemType"].Value = textBox3.Text.ToString();

                    cmd.Parameters.Add("@clientName", SqlDbType.NVarChar);
                    cmd.Parameters["@clientName"].Value = textBox4.Text.ToString();

                    cmd.Parameters.Add("@commentWorker", SqlDbType.NVarChar);
                    cmd.Parameters["@commentWorker"].Value = textBox5.Text.ToString();

                    cmd.Parameters.Add("@status", SqlDbType.NVarChar);
                    cmd.Parameters["@status"].Value = "В ожидании";

                    cmd.Parameters.Add("@workerOrd", SqlDbType.NVarChar);
                    cmd.Parameters["@workerOrd"].Value = workerID;

                    cmd.Parameters.Add("@clientSurname", SqlDbType.NVarChar);
                    cmd.Parameters["@clientSurname"].Value = textBox6.Text.ToString();

                    cmd.Parameters.Add("@email", SqlDbType.NVarChar);
                    cmd.Parameters["@email"].Value = textBox7.Text.ToString();

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

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            button_enable();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button_enable();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            button_enable();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            button_enable();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            button_enable();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            button_enable();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            button_enable();
        }
    }
}
