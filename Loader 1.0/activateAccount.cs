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
    public partial class activateAccount : Form
    {
        public activateAccount()
        {
            InitializeComponent();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            string invite = eCode.Text;
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
                    string updateQuery = "UPDATE users SET expirated = 0 ,dateexpirate = '"+ date +"' WHERE user = '" + Globals.username + "'";
                    MySqlCommand update = new MySqlCommand(updateQuery, connection);
                    if (update.ExecuteNonQuery() == 1)
                    {
                        string updateQueryCode = "UPDATE invitationcode SET used = 1 WHERE code = '" + invite + "'";
                        MySqlCommand updateCode = new MySqlCommand(updateQueryCode, connection);
                        if (updateCode.ExecuteNonQuery() == 1)
                        {

                            msgbox message = new msgbox("The account has been successfully activated!", "succes");
                            message.Show();
                            this.Close();
                        }
                       
                    }
                }
            }
        }
    }
}
