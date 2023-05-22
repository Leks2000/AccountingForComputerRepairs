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

namespace AccountingForComputerEquipmentRepairs.Forms
{
    public partial class Registration : Form
    {
        DB datebase = new DB();
        public Registration()
        {
            InitializeComponent();
        }

        private void EnterBtn_Click(object sender, EventArgs e)
        {
            datebase.openConnection();
            if (LoginTxtBox.Text != String.Empty || PasswdTxtBox.Text != String.Empty)
            {
                var user = "";
                var users = $"SELECT user_name FROM Users WHERE user_name ='{LoginTxtBox.Text}'";
                var comm = new SqlCommand(users, datebase.GetConnection());
                datebase.openConnection();
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    user = reader.GetString(0);
                }
                reader.Close();
                if (LoginTxtBox.Text != user)
                {
                    var addQuery = $"INSERT INTO users(user_name,user_password)" +
                    $" VALUES ('{LoginTxtBox.Text}','{PasswdTxtBox.Text}')";
                    var command = new SqlCommand(addQuery, datebase.GetConnection());
                    command.ExecuteNonQuery();
                    MessageBox.Show("Новый профиль успешно создан!");
                    Login mod = new Login();
                    Hide();
                    mod.Show();
                }
                else
                {
                    MessageBox.Show("Такой пользователь уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Неверный ввод!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            datebase.closeConnection();
        }

        private void BtnEscape_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            Hide();
            login.Show();
        }
    }
}
