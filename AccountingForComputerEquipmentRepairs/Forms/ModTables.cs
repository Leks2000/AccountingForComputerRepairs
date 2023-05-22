using AccountingForComputerEquipmentRepairs.DB_CompAvailabilityDataSetTableAdapters;
using Microsoft.Office.Interop.Excel;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using Application = System.Windows.Forms.Application;
using DataTable = System.Data.DataTable;
using ToolTip = System.Windows.Forms.ToolTip;

namespace AccountingForComputerEquipmentRepairs.Forms
{
    public partial class ModTables : Form
    {
        public static bool isEdit = true;
        public int user_id;
        DB datebase = new DB();
        public ModTables()
        {
            InitializeComponent();
            if (Login.isAdmin == false)
            {
                loadgrid("SELECT * FROM Availability where prod_status='в наличии'");
                Status.Text = "Статус: Пользователь";
            }else
                loadgrid("SELECT * FROM Availability");
            dataGridView1.Columns[0].Visible = false;

        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public bool user
        {
            get { return BtnAdd.Visible = true; }
            set { BtnAdd.Visible = value; BtnChange.Visible = value; BtnBuy.Visible = true; }
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddForm add = new AddForm();
            Hide();
            add.Show();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            var ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
            //Книга.
            ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
            //Таблица.
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);
            ExcelWorkSheet.Columns.ColumnWidth = 27;
            ExcelApp.Cells[1, 2] = "Название продукта";
            ExcelApp.Cells[1, 3] = "Статус";
            ExcelApp.Cells[1, 4] = "Цена";
            ExcelApp.Cells[1, 5] = "Количество товара";
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    ExcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value;
                }
            }
            ExcelApp.Visible = true;
            ExcelApp.UserControl = true;
        }

        private void BtnChange_Click(object sender, EventArgs e)
        {
            isEdit = false;
            var btnchange = new Users()
            {
                id = int.Parse(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[dataGridView1.Columns["dataGridViewTextBoxColumn1"].Index].Value.ToString()),
                name = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[dataGridView1.Columns["dataGridViewTextBoxColumn2"].Index].Value.ToString(),
                status = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[dataGridView1.Columns["dataGridViewTextBoxColumn3"].Index].Value.ToString(),
                price = int.Parse(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[dataGridView1.Columns["dataGridViewTextBoxColumn4"].Index].Value.ToString()),
                column = int.Parse(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[dataGridView1.Columns["dataGridViewTextBoxColumn5"].Index].Value.ToString()),
            };
            AddForm tr = new AddForm(btnchange, true);
            Hide();
            tr.Show();
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

        private void BtnEscape_Click(object sender, EventArgs e)
        {
            if (Login.User_Id != 0)
            {
                Login login = new Login();
                Hide();
                login.Show();
            }
            else
            {
                MainMenu main = new MainMenu();
                Hide();
                main.Show();
            }
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
                    MessageBox.Show("Такого товара не существует", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBuy_Click(object sender, EventArgs e)
        {
            var user = new Users()
            {
                id = int.Parse(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[dataGridView1.Columns["dataGridViewTextBoxColumn1"].Index].Value.ToString()),
                name = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[dataGridView1.Columns["dataGridViewTextBoxColumn2"].Index].Value.ToString(),
                status = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[dataGridView1.Columns["dataGridViewTextBoxColumn3"].Index].Value.ToString(),
                price = int.Parse(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[dataGridView1.Columns["dataGridViewTextBoxColumn4"].Index].Value.ToString()),
                column = int.Parse(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[dataGridView1.Columns["dataGridViewTextBoxColumn5"].Index].Value.ToString()),
            };
            
            AddForm add = new AddForm(user, false);
            Hide();
            add.Show();
        }

        private void ModTables_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dB_CompAvailabilityDataSet.Availability". При необходимости она может быть перемещена или удалена.
            this.availabilityTableAdapter.Fill(this.dB_CompAvailabilityDataSet.Availability);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dB_CompAvailabilityDataSet.Availability". При необходимости она может быть перемещена или удалена.
            this.availabilityTableAdapter.Fill(this.dB_CompAvailabilityDataSet.Availability);
        }

        private void контактыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программу сделал студент группы ИП-20-3 Халле Александр", "Контакты",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, helpProvider1.HelpNamespace);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //if (dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells.Count > 1)
            //{
            //    BtnChange.Enabled = true;
            //    BtnBuy.Enabled = true;
            //}
        }

        private void FillterBtn_Click(object sender, EventArgs e)
        {
            if (TextSearch.Text == string.Empty)
            {
                MessageBox.Show("Введите значение для фильтра", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (Login.isAdmin == false)
                {
                    loadgrid("SELECT * FROM Availability where prod_status='в наличии'");
                    Status.Text = "Статус: Пользователь";
                }
                else
                    loadgrid($"SELECT * FROM Availability");
            }
            else
                loadgrid($"SELECT * FROM Availability where prod_name='{TextSearch.Text}'");
        }

        private void TextSearch_MouseHover(object sender, EventArgs e)
        {
            ToolSearch.SetToolTip(TextSearch, "Введите название товара");
        }
        public void loadgrid(string str)
        {
            datebase.openConnection();
            SqlDataAdapter da = new SqlDataAdapter($"{str}", datebase.GetConnection());
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            datebase.closeConnection();
        }

        private void dataGridView1_ColumnStateChanged(object sender, DataGridViewColumnStateChangedEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }
    }
}