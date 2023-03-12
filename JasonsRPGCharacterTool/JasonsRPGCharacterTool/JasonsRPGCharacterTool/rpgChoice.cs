using JasonsRPGCharacterTool.Global_stuff;
using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace JasonsRPGCharacterTool
{
    public partial class rpgChoice : Form
    {
        //Definiert die beiden Farben für den Beenden Knopf
        private readonly Color highlightRed = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(141)))), ((int)(((byte)(133)))));
        private readonly Color exitRed = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(78)))), ((int)(((byte)(61)))));

        darkMode darkTheme = new darkMode();

        public rpgChoice()
        {
            InitializeComponent();

            Global_stuff.FormManager.rpgChoice = this;

            //Ändert den Versionsstring zur installierten Version
            versionText.Text = "v" + Application.ProductVersion;

            //Überprüft ob ein Update verfügbar ist
            checkForUpdates();

            //Lokalisiert die entsprechenden Strings zu deutsch oder englisch
            rpgChoiceText.Text = Properties.strings.chooseRPG;
            exitText.Text = Properties.strings.exitText;
            shopToolDescript.Text = Properties.strings.shopToolDescript;

            changeTheme(darkTheme.checkMode());
        }

        //Erlaubt mit einem Click auf die Controlbar das Fenster zu bewegen.
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void RPGToolText_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void tesPic_Click(object sender, EventArgs e)
        {
            Global_stuff.FormManager.tesPnP.setTheme(darkTheme.checkMode());

            //Definiert ein neues Form, setzt die Startposition des neuen Forms auf Mitte des Bildschirms, öffnet es und schließt das alte Form.
            Global_stuff.FormManager.tesPnP.StartPosition = FormStartPosition.CenterScreen;
            Global_stuff.FormManager.tesPnP.Show();
            this.Hide();
        }

        private void asteriusPic_Click(object sender, EventArgs e)
        {
            Global_stuff.FormManager.asteriusPnP.setTheme(darkTheme.checkMode());
            Global_stuff.FormManager.asteriusPnP.knowledge.setTheme(darkTheme.checkMode());

            Global_stuff.FormManager.asteriusPnP.StartPosition = FormStartPosition.CenterScreen;
            Global_stuff.FormManager.asteriusPnP.Show();
            this.Hide();
        }


        //OnClick des Beenden Buttons wird die App beendet.
        private void exitText_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Ändert die Farbe beim Mouseover auf ein helleres Rot.
        private void exitText_MouseEnter(object sender, EventArgs e)
        {
            exitText.BackColor = highlightRed;
        }

        //Ändert die Farbe wieder nach MouseExit auf das Standardrot.
        private void exitText_MouseLeave(object sender, EventArgs e)
        {
            exitText.BackColor = exitRed;
        }

        //Minimiert das Fenster bei Rechtsklick auf Name
        private void RPGToolText_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //Minimiert das Fenster bei Klick auf Taskleiste
        const int WS_MINIMIZEBOX = 0x20000;
        const int CS_DBLCLKS = 0x8;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= WS_MINIMIZEBOX;
                cp.ClassStyle |= CS_DBLCLKS;
                return cp;
            }
        }

        private void shopPic_Click(object sender, EventArgs e)
        {
            Global_stuff.FormManager.shopTool.setTheme(darkTheme.checkMode());

            Global_stuff.FormManager.shopTool.StartPosition = FormStartPosition.CenterScreen;
            Global_stuff.FormManager.shopTool.Show();
            this.Hide();
        }

        //Vergleicht die aktuelle Version mit der Online verfügbaren Version und gibt bei Ungleichheit eine Meldung
        private void checkForUpdates()
        {
#if DEBUG
//Ändert den Text nicht, wenn es ein Debug Build ist
#else
            if (Program.hasInternet)
            {
                using (var client = new WebClient())
                {
                    string currentVersion = client.DownloadString("https://jason360x.github.io/RPGTool/currentRPGToolVersion.txt");

                    if (currentVersion.Trim() != versionText.Text.Trim())
                    {
                        versionText.Text = Properties.strings.newVersion + " " + versionText.Text;
                        versionText.ForeColor = SystemColors.Highlight;
                        versionText.Cursor = Cursors.Hand;
                        versionText.Click += new EventHandler(downloadNewVersion);
                    }
                }
            }
#endif
        }

        //Öffnet onClick den Browser um die aktuelle Version herunterzuladen
        private void downloadNewVersion(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://jason360x.github.io/RPGTool/InstallJasonsRPGTool.exe");
        }

        //Färbt alle Elemente entweder hell oder dunkel, je nach Einstellung in Windows
        public void changeTheme(string mode)
        {
            Color darkFormBackground = darkTheme.darkFormBackground;
            Color lightFormBackground = darkTheme.lightFormBackground;

            Color dark = darkTheme.darkBackground;
            Color light = darkTheme.lightBackground;

            Color darkText = darkTheme.darkText;
            Color lightText = darkTheme.lightText;

            Color darkTextLighter = darkTheme.darkTextLighter;
            Color lightTextDarker = darkTheme.lightTextDarker;

            if (mode == "Dark")
            {
                this.BackColor = darkFormBackground;
                foreach (Label text in this.Controls.OfType<Label>())
                {
                    if (text.Tag == null || text.Tag.ToString() != "KeepColor")
                    {
                        if (text.Tag != null && text.Tag.ToString() == "IsBold")
                        {
                            text.ForeColor = lightTextDarker;
                        }
                        else
                        {
                            text.ForeColor = lightText;
                        }
                    }
                }

                if (versionText.ForeColor != SystemColors.Highlight)
                {
                    versionText.ForeColor = lightText;
                }
            }
            else if (mode == "Light")
            {
                this.BackColor = lightFormBackground;

                foreach (Label text in this.Controls.OfType<Label>())
                {
                    if (text.Tag == null || text.Tag.ToString() != "KeepColor")
                    {
                        if (text.Tag != null && text.Tag.ToString() == "IsBold")
                        {
                            text.ForeColor = darkTextLighter;
                        }
                        else
                        {
                            text.ForeColor = darkText;
                        }
                    }
                }

                if (versionText.ForeColor != SystemColors.Highlight)
                {
                    versionText.ForeColor = darkText;
                }
            }
            else if (mode == "Black")
            {
                Color blackFormBack = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));

                Color blackText = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
                Color blackTextLighter = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));

                this.BackColor = blackFormBack;

                foreach (Label text in this.Controls.OfType<Label>())
                {
                    if (text.Tag == null || text.Tag.ToString() != "KeepColor")
                    {
                        if (text.Tag != null && text.Tag.ToString() == "IsBold")
                        {
                            text.ForeColor = blackTextLighter;
                        }
                        else
                        {
                            text.ForeColor = blackText;
                        }
                    }
                }

                if (versionText.ForeColor != SystemColors.Highlight)
                {
                    versionText.ForeColor = blackText;
                }
            }
        }
    }
}
