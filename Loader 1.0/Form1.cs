using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using Loader_1._0.Properties;

namespace Loader_1._0
{
    public partial class Form1 : Form
    {
        int mov;
        int movX;
        int movY;
        bool clickUsername = false;
        bool clickPassword = false;
 

        public Form1()
        {
            
            InitializeComponent();

          
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            string username = eUsername.Text;
            string password = ePassword.Text;
            string HWID = System.Security.Principal.WindowsIdentity.GetCurrent().User.Value;
            int expire = 0 ;
            MySqlConnection connection = new MySqlConnection("Server=188.212.101.56; Database=atriosxy_loader;User ID=atriosxy_loader;Password=tk#Ieyh}8eIq; Pooling=true;");
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                MySqlDataAdapter sda = new MySqlDataAdapter("select *  from users where user = '" + username + "' and pass = '" + password + "' and hwid = '" + HWID +"'", connection);
                DataTable table = new DataTable();
                sda.Fill(table);


                foreach (DataRow row in table.Rows)
                {
             

                    expire = int.Parse(string.Format("{0}", row["expirated"]));
                }


                if (expire == 1 )
                {
                    Globals.username = username;
                    msgbox message = new msgbox("Cont expirat", "error");
                    message.Show();
                    activateAccount panel = new activateAccount();
                    panel.Show();
                    return;
                }


                //   sda.Fill(dt);               
                if (table.Rows.Count <= 0)
                {
                    ///Restul de cod
              
                   
                    msgbox message = new msgbox("The user name or password is not valid or HWID invalide!","error");
                    message.Show();
                    // MessageBox.Show("The user name or password is not valid or HWID invalide!");


                }
                else
                {
                    Properties.Settings.Default.utilizator = eUsername.Text;
                    Properties.Settings.Default.parola = ePassword.Text;
                    Properties.Settings.Default.Save();
                    this.Hide();
                    Globals.username = username;
                    LoggedMenu menu = new LoggedMenu();
                    menu.Show();
            
                    
                  
                }
                connection.Close();
            }
            else
            {
               // MessageBox.Show();
                msgbox message = new msgbox("Don't Exitsa Conexiunne to the database!", "error");
                message.Show();
                connection.Close();
            }
            connection.Close();
        }

        private void eUsername_MouseEnter(object sender, EventArgs e)
        {
         
            if (eUsername.Text == "Username")
            {
                eUsername.Clear();
            }
        }

        private void eUsername_MouseLeave(object sender, EventArgs e)
        {
            if (eUsername.Text == " ")
            {
                eUsername.Text = "Username";
            }
        }

        private void ePassword_MouseEnter(object sender, EventArgs e)
        {
            if (ePassword.Text == "Password")
            {
                ePassword.Clear();
            }
        }

        private void ePassword_MouseLeave(object sender, EventArgs e)
        {
            if (ePassword.Text == " ")
            {
                ePassword.Text = "Password";
            }
        }

        private void eUsername_Click(object sender, EventArgs e)
        {
            if (clickUsername==false)
            {
                usernamePanel.BackColor = Color.FromArgb(192, 0, 192);
                clickUsername = true;
                clickPassword = false;
                passwordpanel.BackColor = Color.Silver;

            }
        }

        private void ePassword_TextChanged(object sender, EventArgs e)
        {
            if (clickPassword == false)
            {
                usernamePanel.BackColor = Color.Silver;
                clickUsername = false;
                clickPassword = true;
                ePassword.UseSystemPasswordChar = true;
                passwordpanel.BackColor = Color.FromArgb(192, 0, 192);

            }
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.ForeColor = Color.FromArgb(192, 0, 192);
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.CornflowerBlue;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // this.Hide();
            this.Hide();
            RegisterPanel register = new RegisterPanel();
            register.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if ((Properties.Settings.Default.utilizator != "Username") && (Properties.Settings.Default.parola != "Password"))
            {
                eUsername.Text = Properties.Settings.Default.utilizator;
                ePassword.Text = Properties.Settings.Default.parola;
            }
        }
    }
}
