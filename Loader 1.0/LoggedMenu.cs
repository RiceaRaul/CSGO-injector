using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using ManualMapInjection.Injection;
using System.IO;


namespace Loader_1._0
{
    public partial class LoggedMenu : Form
    {

        int mov;
        int movX;
        int movY;
        public LoggedMenu()
        {
            InitializeComponent();
            label3.Text = "Welcome " + Globals.username;
            if (Globals.AdminName.Contains(Globals.username))
            {
                adminButton.Visible = true;
            }
            else
            {
                adminButton.Visible = false;
            }
        }



        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void LoggedMenu_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            movX = e.X;
            movY = e.Y;
        }

        private void LoggedMenu_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov == 1)
            {
                this.SetDesktopLocation(MousePosition.X - movX, MousePosition.Y - movY);
            }
        }

        private void LoggedMenu_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            var name = "csgo";
            var target = Process.GetProcessesByName(name).FirstOrDefault();
            if (target == null)
            {
                msgbox message = new msgbox("Process not found", "error");
                message.Show();
                return;
            }
            string Temppath = Path.GetTempPath();
            var path = Temppath + "634182313956.dll";
            client.DownloadFile("https://www.atrios.xyz/.kakamitzu/634182313956.dll", path);

            var file = File.ReadAllBytes(path);

            if (!File.Exists(path))
            {
                msgbox message = new msgbox("File not found! press OK to restart client...", "error");
                message.Show();
                Application.Restart();
            }


            var injector = new ManualMapInjector(target) { AsyncInjection = true };

            //  label2.Text = $"hmodule = 0x{injector.Inject(file).ToInt64():x8}";
            if ($"hmodule = 0x{ injector.Inject(file).ToInt64():x8}" != "hmodule = 0x00000")
            {
                msgbox message = new msgbox("Injected", "succes");
                message.Show();
            }
            else
            {
                msgbox message = new msgbox("Hack could not be injected", "error");
                message.Show();
            }
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            var name = "csgo";
            var target = Process.GetProcessesByName(name).FirstOrDefault();
            if (target == null)
            {
               // MessageBox.Show("Process not found");
                msgbox message = new msgbox("Process not found", "error");
                message.Show();
                return;
            }
            string Temppath = Path.GetTempPath();
            var path = Temppath + "214683593q32.dll";
            client.DownloadFile("https://www.atrios.xyz/.kakamitzu/214683593q32.dll", path);

            var file = File.ReadAllBytes(path);

            if (!File.Exists(path))
            {
               // MessageBox.Show("unexpected error. File not found! press OK to restart client...");
                msgbox message = new msgbox("File not found! press OK to restart client...", "error");
                message.Show();
                Application.Restart();
            }


            var injector = new ManualMapInjector(target) { AsyncInjection = true };

            //label2.Text = $"hmodule = 0x{injector.Inject(file).ToInt64():x8}";

            if ($"hmodule = 0x{ injector.Inject(file).ToInt64():x8}" != "hmodule = 0x00000")
            {
                msgbox message = new msgbox("Injected", "succes");
                message.Show();
            }
            else
            {
                msgbox message = new msgbox("Hack could not be injected", "error");
                message.Show();
            }
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void adminButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            adminPanel panel = new adminPanel();
            panel.Show();
        }
    }
}
