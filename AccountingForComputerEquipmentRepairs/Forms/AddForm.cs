using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ScrollBar;

namespace AccountingForComputerEquipmentRepairs.Forms
{
    public partial class AddForm : Form
    {
        public AddForm()
        {
            InitializeComponent();
            NumUpDColumn.Minimum = 1;
            PriceBox.MaxLength = 5;
            ColumnBox.MaxLength = 3;
            NameBox.MaxLength = 25;
            NameBox.Enabled = AdminPanel.isEditOk;
            StateComboBox.Enabled = AdminPanel.isEditOk;
            if (AdminPanel.isEditOk == false)
            {
                StateComboBox.Enabled |= AdminPanel.isEditOk;
                NameBox.Enabled|= AdminPanel.isEditOk;
            }

        }
        public AddForm(Users get, bool isEdit) : this()
        {
            id = get.id;
            NameBox.Text = get.name;
            StateComboBox.Text = get.status;
            PriceBox.Text = get.price.ToString();
            ColumnBox.Text = get.column.ToString();
            if (isEdit)
            {
                BtnEdit.Visible = isEdit;
                this.Text = "Редактировать запись";
            }
            else
            {
                this.Text = "Заказать товар";
                PriceBox.Visible = false;
                ColumnBox.Visible = false;
                BtnBuy.Visible = true;
                TextChangeUse.Visible = true;
                TextChangeUse.Text = get.price.ToString();
                NumUpDColumn.Visible = true;
                price = get.price;
                NumUpDColumn.Maximum = get.column;
            }
        }
        public int users
        {
            set { log = value; }
        }
        public int price;
        public int id_user;
        public int id;
        public int Max = 0;
        public static int  log = 0;
        DB datebase = new DB();
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            datebase.openConnection();
            if (NameBox.Text != String.Empty && PriceBox.Text != String.Empty && ColumnBox.Text != String.Empty)
            {
                if (StateComboBox.Text == "")
                    StateComboBox.Text = "Неизвестен";
                var addQuery = $"INSERT INTO Availability(prod_name,prod_status,prod_price,prod_column)" +
                $" VALUES ('{NameBox.Text}','{StateComboBox.Text}','{PriceBox.Text}','{ColumnBox.Text}')";
                var command = new SqlCommand(addQuery, datebase.GetConnection());
                command.ExecuteNonQuery();
                MessageBox.Show("Новая запись успешно создана!");
                ModTables mod = new ModTables();
                Hide();
                mod.Show();
            }
            else
            {
                MessageBox.Show("Неверный ввод!");
            }
            datebase.closeConnection();
        }
        
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            datebase.openConnection();
            if (ModTables.isEdit == false)
            {
                var changequery = $"UPDATE Availability SET prod_name = '{NameBox.Text}', prod_status = '{StateComboBox.Text}', prod_price = {PriceBox.Text} , prod_column = {ColumnBox.Text} WHERE prod_id = {id}";
                var command = new SqlCommand(changequery, datebase.GetConnection());
                command.ExecuteNonQuery();
                if (int.Parse(ColumnBox.Text) > 0)
                {
                    datebase.openConnection();
                    changequery = $"UPDATE Availability set prod_status = 'В наличии' where prod_id = {id}";
                    command = new SqlCommand(changequery, datebase.GetConnection());
                    command.ExecuteNonQuery();
                    datebase.closeConnection();
                }
                ModTables modTables = new ModTables();
                datebase.closeConnection();
                Hide();
                modTables.Show();
                ModTables.isEdit = true;
            }else if(AdminPanel.isEditOk == false)
            {
                var change = $"UPDATE Orders SET id_prod = '{NameBox.Text}', users_id = '{StateComboBox.Text}', order_price = {PriceBox.Text} , order_count = {ColumnBox.Text} WHERE order_id = {id}";
                var com = new SqlCommand(change, datebase.GetConnection());
                com.ExecuteNonQuery();
                AdminPanel adm = new AdminPanel();
                datebase.closeConnection();
                Hide();
                adm.Show();
                AdminPanel.isEditOk = true;
            }
        }

        private void BtnBuy_Click(object sender, EventArgs e)
        {
            datebase.openConnection();
            var selecttable = $"INSERT INTO Orders(id_prod,users_id,order_count,order_price) VALUES('{id}','{Login.User_Id}','{NumUpDColumn.Value}','{TextChangeUse.Text}')";
            var vai = new SqlCommand(selecttable, datebase.GetConnection());
            vai.ExecuteNonQuery();

            var updt = $"UPDATE Availability SET prod_column = '{GetProdId() - NumUpDColumn.Value}' where prod_id = '{id}'";
            var await = new SqlCommand(updt, datebase.GetConnection());
            await.ExecuteNonQuery();

            if (GetProdId() <= 0)
            {
                var changequery = $"update availability set prod_status = 'Нет в наличии' where prod_id = {id}";
                var command = new SqlCommand(changequery, datebase.GetConnection());
                command.ExecuteNonQuery();
            }

            datebase.closeConnection();

            bool gav = false;
            ModTables modTables = new ModTables();
            modTables.user = gav;
            Hide();
            modTables.Show();
        }
        private void NumUpDColumn_ValueChanged(object sender, EventArgs e)
        {
            TextChangeUse.Text = (NumUpDColumn.Value * price).ToString();
        }
        public int GetProdId()
        {
            int prod_col = 0;
            string prod = $"SELECT prod_column FROM Availability WHERE prod_id = '{id}'";
            var comment = new SqlCommand(prod, datebase.GetConnection());
            SqlDataReader readln = comment.ExecuteReader();
            while (readln.Read())
            {
                prod_col = readln.GetInt32(0);
            }
            readln.Close();
            return prod_col;
        }

        private void ColumnBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || (!string.IsNullOrEmpty(ColumnBox.Text) && e.KeyChar == ','))
            {
                return;
            }
            e.Handled = true;
        }

        private void PriceBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || (!string.IsNullOrEmpty(PriceBox.Text) && e.KeyChar == ','))
            {
                return;
            }
            e.Handled = true;
        }
    }
}
