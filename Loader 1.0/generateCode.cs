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
    public partial class generateCode : UserControl
    {
        public generateCode()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

            int period = 0;
            if (comboBox1.SelectedIndex == -1)
            {
                msgbox message = new msgbox("Select time!", "error");
                message.Show();
                return;
            }
            else
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    period = 30;
                }
                else
                {
                    if (comboBox1.SelectedIndex == 1)
                    {
                        period = 90;
                    }
                    else
                    {
                        if (comboBox1.SelectedIndex == 2)
                        {
                            period = 360;
                        }
                        else
                        {
                            if (comboBox1.SelectedIndex == 3)
                            {
                                period = 99999;
                            }
                        }
                    }
                }
            }




            MySqlConnection connection = new MySqlConnection("Server=188.212.101.56; Database=atriosxy_loader;User ID=atriosxy_loader;Password=tk#Ieyh}8eIq; Pooling=true;");
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {

               generate: Random rnd = new Random();
                int unu = rnd.Next(1000, 9999);
                int doi = rnd.Next(1000, 9999);
                int trei = rnd.Next(1000, 9999);
                int patru = rnd.Next(1000, 9999);
                string code = unu.ToString() + doi.ToString() + trei.ToString() + patru.ToString();
                MySqlDataAdapter inviteCode = new MySqlDataAdapter("select * from invitationcode where code = '" + code + "'", connection);
                DataTable dt = new DataTable();
                inviteCode.Fill(dt);
                if (dt.Rows.Count <= 0)
                {
                    ///Restul de cod
                }
                else
                {
                    goto generate;
                }

                string insertQuery = "INSERT INTO invitationcode SET code = '" + code + "',period = '"+period+"'";
                MySqlCommand insert = new MySqlCommand(insertQuery, connection);
                if (insert.ExecuteNonQuery() == 1)
                    {
                        msgbox message = new msgbox("Code generated successfully!", "succes");
                        message.Show();
                        eCode.Text = code;
                    }
                    else
                    {
                        msgbox message = new msgbox("A MySQL error has occurred!//Ricea", "error");
                        message.Show();
                    }
               
            }
            connection.Close();
        }
      
    }
 
}
