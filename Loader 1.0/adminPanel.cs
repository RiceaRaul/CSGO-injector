using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loader_1._0
{
    public partial class adminPanel : Form
    {
        public adminPanel()
        {
            InitializeComponent();
            generateCode1.BringToFront();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoggedMenu menu = new LoggedMenu();
            menu.Show();
        }
    }
}
