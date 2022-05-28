using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace Loader_1._0
{
    public partial class RegisterPanel : Form
    {
        bool clickUsername = false;
        bool clickPassword = false;
        bool clickInviteCode = false;
        public RegisterPanel()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            string username = eUsername.Text;
            string password = ePassword.Text;
            string invite = eInvitationcode.Text;
            string HWID = System.Security.Principal.WindowsIdentity.GetCurrent().User.Value;
            int month = Int32.Parse(DateTime.Now.ToString("MM"));
            int Date = Int32.Parse(DateTime.Now.ToString("dd"));
            int year = Int32.Parse(DateTime.Now.ToString("yyyy"));
            int period = 0;
            MySqlConnection connection = new MySqlConnection("Server=188.212.101.56; Database=atriosxy_loader;User ID=atriosxy_loader;Password=tk#Ieyh}8eIq; Pooling=true;");
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
                MySqlDataAdapter inviteCode = new MySqlDataAdapter("select * from invitationcode where code = '" + invite + "' and used = 0", connection);
                DataTable dt = new DataTable();
                inviteCode.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {


                    period = int.Parse(string.Format("{0}", row["period"]));
                }
                if (month == 12)
                {
                    if (period == 30)
                    {
                        year = year + 1;
                        month = 1;
                    }
                    else
                    {
                        if (period == 90)
                        {
                            year = year + 1;
                            month = 3;
                        }
                        else
                        {
                            if (period == 360)
                            {
                                year = year + 1;
                            }
                            else
                            {
                                year = 90000;
                            }
                        }
                    }
                }
                else
                {
                    if (period == 30)
                    {

                        month++;
                    }
                    else
                    {
                        if (period == 90)
                        {

                            month = month + 3;
                        }
                        else
                        {
                            if (period == 360)
                            {
                                year = year + 1;
                            }
                            else
                            {
                                year = 90000;
                            }
                        }
                    }
                }


                string date = year.ToString() + "/" + month + "/" + Date;
                if (dt.Rows.Count <= 0)
                {
                    msgbox message = new msgbox("The code you use is invalid or use!", "error");
                    message.Show();
                    return;
                }
                else
                {
                    
                    MySqlDataAdapter userExist = new MySqlDataAdapter("select count(*) from users where user = '" + username + "'", connection);
                    DataTable userExistTable = new DataTable();
                    userExist.Fill(userExistTable);



                   
              
                 

                    if (userExistTable.Rows[0][0].ToString() == "0")
                    {
                        string insertQuery = "INSERT INTO users SET user = '" + username + "',pass = '" + password + "',hwid = '" + HWID + "',dateexpirate = '" + date + "'";
                        MySqlCommand insert = new MySqlCommand(insertQuery, connection);
                        if (insert.ExecuteNonQuery() == 1)
                        {
                            string updateQuery = "UPDATE invitationcode SET used = 1 WHERE code = '" + invite + "'";
                            MySqlCommand update = new MySqlCommand(updateQuery, connection);
                            if (update.ExecuteNonQuery() == 1)
                            {

                                msgbox message = new msgbox("Account created successfully!", "succes");
                                message.Show();
                            }

                        }

                    }
                    else
                    {
                        msgbox message = new msgbox("The user already exists! Use another!", "error");
                        message.Show();
                    }
                }
            }
            connection.Close();
            }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();

        }

        private void eUsername_MouseEnter(object sender, EventArgs e)
        {
            if (eUsername.Text == "Username")
            {
                eUsername.Text = "";
            }
        }

        private void eUsername_MouseLeave(object sender, EventArgs e)
        {
            if(eUsername.Text=="")
            {
                eUsername.Text = "Username";
            }
        }

        private void ePassword_MouseEnter(object sender, EventArgs e)
        {
            if(ePassword.Text == "Password")
            {
                ePassword.Text = "";
            }
        }

        private void ePassword_MouseLeave(object sender, EventArgs e)
        {
            if (ePassword.Text == "")
            {
                ePassword.Text = "Password";
            }
        }

        private void eInvitationcode_MouseEnter(object sender, EventArgs e)
        {
            if (eInvitationcode.Text == "Invitation Code")
            {
                eInvitationcode.Text = "";
            }
        }

        private void eInvitationcode_MouseLeave(object sender, EventArgs e)
        {
            if (eInvitationcode.Text == "")
            {
                eInvitationcode.Text = "Invitation Code";
            }
        }

        private void eUsername_Click(object sender, EventArgs e)
        {
            if(clickUsername == false)
            {
                usernamePanel.BackColor = Color.FromArgb(192, 0, 192);
                clickUsername = true;
                clickPassword = false;
                clickInviteCode = false;
                passwordpanel.BackColor = Color.Silver;
                invitecodePanel.BackColor = Color.Silver;
            }
                    
        }

        private void ePassword_Click(object sender, EventArgs e)
        {
            if (clickPassword == false)
            {
                usernamePanel.BackColor = Color.Silver;
                clickUsername = false;
                clickPassword = true;
                clickInviteCode = false;
                passwordpanel.BackColor = Color.FromArgb(192, 0, 192);
                invitecodePanel.BackColor = Color.Silver;
            }
        }

        private void eInvitationcode_Click(object sender, EventArgs e)
        {
            if (clickInviteCode == false)
            {
                usernamePanel.BackColor = Color.Silver;
                clickUsername = false;
                clickPassword = false;
                clickInviteCode = true;
                passwordpanel.BackColor = Color.Silver;
                invitecodePanel.BackColor = Color.FromArgb(192, 0, 192);
            }
        }
    }
}
