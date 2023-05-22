using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountingForComputerEquipmentRepairs.Forms
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

      
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void ordersbtn_Click(object sender, EventArgs e)
        {
            AdminPanel adminPanel = new AdminPanel();
            Hide();
            adminPanel.Show();
        }
        private void productsbtn_Click(object sender, EventArgs e)
        {
            ModTables modTables = new ModTables();
            Hide();
            modTables.Show();
        }
    }
}
