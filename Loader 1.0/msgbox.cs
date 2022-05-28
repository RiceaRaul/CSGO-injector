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
    public partial class msgbox : Form
    {
        
        public msgbox( string message , string type)
        {
 
            InitializeComponent();
            textInfo.Text = message;
            timerToShow.Start();
            if (type == "succes")
            {
              
                this.BackColor = Color.FromArgb(0, 192, 0);
                pictureBox1.Image= Loader_1._0.Properties.Resources.ok;
            }
            else
            {

                this.BackColor = Color.FromArgb(192, 0, 0);
                pictureBox1.Image = Loader_1._0.Properties.Resources.error;
            }
              
                   
            
 
    
        }
    

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            timerToClose.Start();
        }

        private void msgbox_Load(object sender, EventArgs e)
        {
            this.Top = -1 * (this.Height);
            this.Left = Screen.PrimaryScreen.Bounds.Width - this.Width - 20;
            timerToShow.Start();
        }

        int interval = 0;

        private void timerToShow_Tick(object sender, EventArgs e)
        { 
            if (this.Top < 0)
            {
                this.Top = (this.Top + interval);
                interval += 2;
            }
            else
            {
                timerToShow.Stop();
              
            }

        }

        private void timerToClose_Tick(object sender, EventArgs e)
        {
            if(this.Opacity > 0)
            {
                this.Opacity-=0.05;
            }
            else
            {
                this.Close();
            }
        }

        private void CloseNotification_Tick(object sender, EventArgs e)
        {
            timerToClose.Start();
        }
    }
}
