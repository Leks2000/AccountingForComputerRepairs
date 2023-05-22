using AccountingForComputerEquipmentRepairs.DB_CompAvailabilityDataSetTableAdapters;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using Application = System.Windows.Forms.Application;
using DataTable = System.Data.DataTable;

namespace AccountingForComputerEquipmentRepairs.Forms
{
    public partial class AdminPanel : Form
    {
        DB datebase = new DB();
        public static bool isEditOk;
        public AdminPanel()
        {
            InitializeComponent();
            loadgrid("SELECT * FROM Orders");
            dataGridView1.Columns[0].Visible = false;
        }


        private void BtnEscape_Click(object sender, EventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            Hide();
            mainMenu.Show();
        }

        private void BtnSort_Click(object sender, EventArgs e)
        {
            var sort = ListSortDirection.Ascending;
            if (radioButton4.Checked)
            {
                sort = ListSortDirection.Ascending;
            }
            if (radioButton5.Checked)
            {
                sort = ListSortDirection.Descending;
            }
            if (radioButton1.Checked)
            {
                dataGridView1.Sort(dataGridView1.Columns[4], sort);
            }
            if (radioButton2.Checked)
            {
                dataGridView1.Sort(dataGridView1.Columns[3], sort);
            }
            if (radioButton3.Checked)
            {
                dataGridView1.Sort(dataGridView1.Columns[2], sort);
            }
        }

        private void BtnChange_Click(object sender, EventArgs e)
        {
            isEditOk = false;
            var btnchange = new Users()
            {
                id = int.Parse(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[dataGridView1.Columns["dataGridViewTextBoxColumn26"].Index].Value.ToString()),
                name = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[dataGridView1.Columns["dataGridViewTextBoxColumn27"].Index].Value.ToString(),
                status = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[dataGridView1.Columns["dataGridViewTextBoxColumn28"].Index].Value.ToString(),
                price = int.Parse(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[dataGridView1.Columns["dataGridViewTextBoxColumn30"].Index].Value.ToString()),
                column = int.Parse(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[dataGridView1.Columns["dataGridViewTextBoxColumn29"].Index].Value.ToString()),
            };
            AddForm tr = new AddForm(btnchange, true);
            Hide();
            tr.Show();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var count = 0;
            if (TextSearch.Text == String.Empty)
            {
                MessageBox.Show("Введите в поле поиск то что хотите найти", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    for (int j = 0; j < dataGridView1.RowCount; j++)
                    {
                        dataGridView1[i, j].Style.BackColor = Color.FromArgb(255, 255, 255, 217);
                        dataGridView1[i, j].Style.ForeColor = Color.Black;

                    }
                }
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(TextSearch.Text.ToLower()))
                        {
                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.AliceBlue;
                            dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Blue;
                            count++;
                        }
                    }
                }
                if (count == 0)
                    MessageBox.Show("Такого товара не существует");
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы хотите удалить данный заказ?", "Сообщение", MessageBoxButtons.YesNo, 
                MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes)
            {
                datebase.openConnection();
                var id = int.Parse(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[dataGridView1.Columns["dataGridViewTextBoxColumn26"].Index].Value.ToString());
                var deleteQuery = $"DELETE FROM Orders WHERE order_id = '{id}'";
                var command = new SqlCommand(deleteQuery, datebase.GetConnection());
                command.ExecuteNonQuery();
                datebase.closeConnection();
            }
            AdminPanel adminPanel = new AdminPanel();
            Hide();
            adminPanel.Show();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //if (dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells.Count != 0)
            //{
            //    BtnChange.Enabled = true;
            //}
        }
        public void loadgrid(string str)
        {
            datebase.openConnection();
            SqlDataAdapter da = new SqlDataAdapter($"{str}", datebase.GetConnection());
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            //$"SELECT user_name FROM Users WHERE id_user = '{listId}'"
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            datebase.closeConnection();
        }

        private void TextSearch_MouseHover(object sender, EventArgs e)
        {
            ToolSearch.SetToolTip(TextSearch,"Введите название товара");
        }

        private void dataGridView1_ColumnStateChanged(object sender, DataGridViewColumnStateChangedEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        public static List<string> column = new List<string>();
        private void BtnExport_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                column.Add(dataGridView1[3, i].Value.ToString());
                column.Add(dataGridView1[4, i].Value.ToString());
            }
            ListInfo listInfo = new ListInfo();
            Hide();
            listInfo.Show();
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dB_CompAvailabilityDataSet.Orders". При необходимости она может быть перемещена или удалена.
            this.ordersTableAdapter.Fill(this.dB_CompAvailabilityDataSet.Orders);

        }
    }
}
