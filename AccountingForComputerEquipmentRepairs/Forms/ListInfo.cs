using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AccountingForComputerEquipmentRepairs.Forms
{
    public partial class ListInfo : Form
    {
        String form = "{0, -20}{1, -20}";
        public ListInfo()
        {
            InitializeComponent();
            listBox1.Font = new Font(FontFamily.GenericMonospace.Name, 20);
            listBox1.Items.Add(String.Format(form, "Количество", "Цена"));
            var count = 0;
            String column = "", price = "";
            foreach (string item in AdminPanel.column)
            {
                if (count == 0)
                {
                    column = item;
                    count++;
                }
                else
                {
                    price = item;
                    count = 0;
                    listBox1.Items.Add(String.Format(form, column ,price));
                }
            }
        }

        private void BtnEscape_Click(object sender, EventArgs e)
        {
            AdminPanel.column.Clear();
            AdminPanel admin = new AdminPanel();
            Hide();
            admin.Show();
        }
    }
}
;