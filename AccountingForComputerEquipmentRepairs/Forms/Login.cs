using AccountingForComputerEquipmentRepairs.Forms;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AccountingForComputerEquipmentRepairs
{
    public partial class Login : Form
    {
        public static bool isAdmin;
        DB dateb = new DB();
        public Login()
        {
            InitializeComponent();
            PasswdTxtBox.UseSystemPasswordChar = true;
        }
        public static int User_Id;
        private void EnterBtn_Click(object sender, EventArgs e)
        {
            var log = "";
            var user_log = "";
            var passwd = "";
            dateb.openConnection();
            string adm = $"SELECT adm_admin FROM admins WHERE adm_admin ='{LoginTxtBox.Text}'";
            var command = new SqlCommand(adm, dateb.GetConnection());
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                log = reader.GetString(0);
            }
            reader.Close();
            string user = $"SELECT user_name FROM Users WHERE user_name ='{LoginTxtBox.Text}'";
            var com = new SqlCommand(user, dateb.GetConnection());
            SqlDataReader usecom = com.ExecuteReader();
            while (usecom.Read())
            {
                user_log = usecom.GetString(0);
            }
            usecom.Close();
            if (LoginTxtBox.Text == string.Empty)
            {
                MessageBox.Show("Введите логин", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }else if (PasswdTxtBox.Text == string.Empty)
            {
                MessageBox.Show("Введите пароль", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            { 
                if (LoginTxtBox.Text == log)
                {
                    var pass = $"SELECT adm_password FROM admins WHERE adm_admin ='{log}'";
                    command = new SqlCommand(pass, dateb.GetConnection());
                    SqlDataReader reader1 = command.ExecuteReader();
                    while (reader1.Read())
                    {
                        passwd = reader1.GetString(0);
                    }
                    reader1.Close();
                    if (PasswdTxtBox.Text == passwd)
                    {
                        isAdmin = true;
                        User_Id = 0;
                        Forms.MainMenu mainMenu = new Forms.MainMenu();
                        Hide();
                        mainMenu.Show();
                    }
                    else
                    {
                        MessageBox.Show("Пароль или логин неправильный", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                if (LoginTxtBox.Text == user_log)
                {
                    var pass = $"SELECT user_password FROM Users WHERE user_name ='{user_log}'";
                    var coms = new SqlCommand(pass, dateb.GetConnection());
                    SqlDataReader reader2 = coms.ExecuteReader();
                    while (reader2.Read())
                    {
                        passwd = reader2.GetString(0);
                    }
                    reader2.Close();
                    if (PasswdTxtBox.Text == passwd)
                    {
                        isAdmin = false;
                        bool gav = false;
                        ModTables guideForm = new ModTables();
                        guideForm.user = gav;
                        guideForm.Text = "Доступные товары";
                        dateb.openConnection();
                        int id = 0;
                        var User_id = $"SELECT id_user FROM Users WHERE user_name='{user_log}'";
                        var comes = new SqlCommand(User_id, dateb.GetConnection());
                        SqlDataReader read = comes.ExecuteReader();
                        while (read.Read())
                        {
                            id = read.GetInt32(0);
                        }
                        read.Close();
                        User_Id = id;
                        Hide();
                        guideForm.Show();
                    }
                    else
                        MessageBox.Show("Пароль или логин неправильный", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Пароль или логин неправильный", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            Hide();
            registration.Show();
        }

        private void ShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowPass.Checked)
            {
                PasswdTxtBox.UseSystemPasswordChar = false;
            }
            else
            {
                PasswdTxtBox.UseSystemPasswordChar = true;
            }
        }
    }
}
