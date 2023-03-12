using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace JasonsRPGCharacterTool
{
    public partial class shopTool : Form
    {
        Global_stuff.darkMode darkTheme = new Global_stuff.darkMode();

        string saveOrNah;
        string save;
        string yes;
        string no;

        public shopTool()
        {
            InitializeComponent();

            //Ändert den Versionsstring zur installierten Version
            versionText.Text = "v" + Application.ProductVersion;

            //Überprüft ob ein Update verfügbar ist
            checkForUpdates();

            //Erstellt alle Reihen
            messyFix();

            //Übersetzt alles in die richtige Sprache
            localize();

            //Setzt das Theme entsprechend den Windows Einstellungen
            setTheme(darkTheme.checkMode());
        }

        //Erstellt eine Reihe in der Tabelle
        private void addRow()
        {
            //get a reference to the previous existent 
            RowStyle temp = shopTable.RowStyles[shopTable.RowCount - 1];
            //increase panel rows count by one
            shopTable.RowCount++;
            //add a new RowStyle as a copy of the previous one
            shopTable.RowStyles.Add(new RowStyle(temp.SizeType, temp.Height));

            shopTable.Controls.Add(new TextBox() { BackColor = SystemColors.Control, Width = shopTable.Width / 100 * 50 }, 0, shopTable.RowCount - 1);
            shopTable.Controls.Add(new TextBox() { BackColor = SystemColors.Control, Width = shopTable.Width / 100 * 25 }, 1, shopTable.RowCount - 1);
            shopTable.Controls.Add(new TextBox() { BackColor = SystemColors.Control, Width = shopTable.Width / 100 * 25 }, 2, shopTable.RowCount - 1);
        }


        //Erstellt die ersten Textboxen
        private void messyFix()
        {
            for (int i = 0; i < 14; i++)
            {
                shopTable.Controls.Add(new TextBox() { BackColor = SystemColors.Control, Width = shopTable.Width / 100 * 50 }, 0, i);
                shopTable.Controls.Add(new TextBox() { BackColor = SystemColors.Control, Width = shopTable.Width / 100 * 25 }, 1, i);
                shopTable.Controls.Add(new TextBox() { BackColor = SystemColors.Control, Width = shopTable.Width / 100 * 25 }, 2, i);
            }

            for (int i = 0; i < 85; i++)
            {
                addRow();
            }
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

        //Fragt noch einmal nach ob man Speichern will und schließt dann das Programm.
        private void exitButton_Click(object sender, EventArgs e)
        {
            askIfWantToSave();

            Application.Exit();
        }

        //Geht OnClick zur Mode Choice zurück
        private void backButton_Click(object sender, EventArgs e)
        {
            askIfWantToSave();

            Global_stuff.FormManager.rpgChoice.changeTheme(darkTheme.checkMode());

            Global_stuff.FormManager.rpgChoice.StartPosition = FormStartPosition.CenterScreen;
            Global_stuff.FormManager.rpgChoice.Show();
            this.Hide();
        }

        //Übersetzt einige Strings in die richtige Sprache
        private void localize()
        {
            shopNameText.Text = Properties.strings.shopName;
            shopAmountText.Text = Properties.strings.shopAmount;
            shopPriceText.Text = Properties.strings.shopPrice;

            shopTESSaveButton.Text = Properties.strings.shopTESSave;
            shopAsteriusSaveButton.Text = Properties.strings.shopAsteriusSave;

            exitButton.Text = Properties.strings.exitText;

            saveOrNah = Properties.strings.saveOrNah;
            yes = Properties.strings.yes;
            no = Properties.strings.no;
            save = Properties.strings.save;

            shopLoadSaveButton.Text = Properties.strings.shopLoadSave;
        }

        //Speichert für jede TextBox den Namen, die Reihe und die Spalte in einer TXT Datei
        private void saveTESToXml(string path)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
            {
                foreach (TextBox textBox in shopTable.Controls)
                {
                    file.WriteLine(textBox.Text);
                }
            }
        }

        //Öffnet beim Klick auf den Speicherknopf eine Dialogbox zum Speichern
        private void shopTESSaveButton_Click(object sender, EventArgs e)
        {
            setTheme(darkTheme.checkMode());

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "TXT File|*.txt";
            saveFile.Title = "Save the shop TXT file.";
            saveFile.InitialDirectory = Application.StartupPath + "\\TES\\shop";
            saveFile.RestoreDirectory = true;
            saveFile.ShowDialog();

            if (saveFile.FileName != "")
            {
                saveTESToXml(saveFile.FileName);
            }
        }


        private void shopAsteriusSaveButton_Click(object sender, EventArgs e)
        {
            setTheme(darkTheme.checkMode());

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "TXT File|*.txt";
            saveFile.Title = "Save the shop TXT file.";
            saveFile.InitialDirectory = Application.StartupPath + "\\Asterius\\shop";
            saveFile.RestoreDirectory = true;
            saveFile.ShowDialog();

            if (saveFile.FileName != "")
            {
                saveTESToXml(saveFile.FileName);
            }
        }

        //Fragt ob man speichern will
        private void askIfWantToSave()
        {
            setTheme(darkTheme.checkMode());

            MessageBoxButtons yesNo = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(saveOrNah, save, yesNo);

            if (result == DialogResult.Yes)
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "TXT File|*.txt";
                saveFile.Title = "Save the shop TXT file.";
                saveFile.InitialDirectory = Application.StartupPath;
                saveFile.RestoreDirectory = true;
                saveFile.ShowDialog();

                if (saveFile.FileName != "")
                {
                    saveTESToXml(saveFile.FileName);
                }
            }
        }

        //Lädt die richtige TXT bei Klick
        private void shopLoadSaveButton_Click(object sender, EventArgs e)
        {
            setTheme(darkTheme.checkMode());

            loadXML();
        }

        //Macht ein Fenster zum Laden der TXT Datei auf und lädt sie dann in die relevanten Boxen.
        private void loadXML()
        {
            string tempPath;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Load the shop TXT file.";
                openFileDialog.Filter = "TXT File|*.txt";
                openFileDialog.InitialDirectory = Application.StartupPath;
                openFileDialog.RestoreDirectory = true;

                Random rand = new Random();
                tempPath = Path.GetTempPath() + "JRPGTShopTemp.txt";

                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(openFileDialog.FileName, tempPath);

                    string[] lines = File.ReadAllLines(tempPath);

                    List < TextBox > textBoxes = new List<TextBox>();

                    var numLines = File.ReadLines(tempPath).Count();

                    foreach (TextBox textBox in shopTable.Controls)
                    {
                        textBoxes.Add(textBox);
                    }

                    for (int i = 0; i < numLines;)
                    {
                        TextBox text1 = textBoxes[i];
                        text1.Text = lines[i];
                        i++;
                        TextBox text2 = textBoxes[i];
                        text2.Text = lines[i];
                        i++;
                        TextBox text3 = textBoxes[i];
                        text3.Text = lines[i];
                        i++;
                    }


                    /*  Code hier war dafür gedacht, die Nummer der Reihen an die benötigte Menge anzupassen, hat aber nicht richtig funktioniert.
                       
                                           
                    var currentRows = shopTable.RowCount;
                    var newRows = (numLines - 1) / 3;

                    if (newRows != shopTable.RowCount)
                    {
                        var rowDifference = newRows - currentRows;

                        if (rowDifference < 0)
                        {
                            for (int i = 0; i < rowDifference - rowDifference - rowDifference; i++)
                            {
                                TextBox text1 = textBoxes[currentRows];
                                TextBox text2 = textBoxes[currentRows - 1];
                                TextBox text3 = textBoxes[currentRows - 2];

                                text1.Dispose();
                                text2.Dispose();
                                text3.Dispose();

                                shopTable.RowCount--;

                                Console.WriteLine("Removed: " + currentRows + " and " + (currentRows - 1) + " and " + (currentRows - 2));
                            }
                        }
                        else if (rowDifference > 0)
                        {
                            for (int i = 0; i < rowDifference; i++)
                            {
                                shopTable.RowCount++;

                                shopTable.Controls.Add(new TextBox() { BackColor = SystemColors.Control, Width = shopTable.Width / 100 * 50 }, 0, i);
                                shopTable.Controls.Add(new TextBox() { BackColor = SystemColors.Control, Width = shopTable.Width / 100 * 25 }, 1, i);
                                shopTable.Controls.Add(new TextBox() { BackColor = SystemColors.Control, Width = shopTable.Width / 100 * 25 }, 2, i);
                            }

                        }
                        
                    }
                    */
                }
            }
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
                        versionText.ForeColor = SystemColors.HotTrack;
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

        public void setTheme(string mode)
        {
            Color darkFormBackground = darkTheme.darkFormBackground;
            Color lightFormBackground = darkTheme.lightFormBackground;

            Color darkBackground = darkTheme.darkBackground;
            Color lightBackground = darkTheme.lightBackground;

            Color darkText = darkTheme.darkText;
            Color lightText = darkTheme.lightText;

            if (mode == "Dark")
            {
                this.BackColor = darkFormBackground;

                darkTheme.changeAllToDark(shopPanel);

                foreach (TextBox textBox in shopTable.Controls.OfType<TextBox>())
                {
                    textBox.BackColor = darkFormBackground;
                    textBox.ForeColor = lightText;
                }
            }

            else if (mode == "Light")
            {
                this.BackColor = lightFormBackground;

                darkTheme.changeAllToLight(shopPanel);

                foreach (TextBox textBox in shopTable.Controls.OfType<TextBox>())
                {
                    textBox.BackColor = lightFormBackground;
                    textBox.ForeColor = darkText;
                }
            }

            else if (mode == "Black")
            {
                Color blackFormBack = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));

                Color blackBack = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
                Color blackText = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
                Color blackTextLighter = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));

                this.BackColor = blackFormBack;

                darkTheme.changeAllToColor(blackTextLighter, blackText, blackBack, shopPanel);

                foreach (TextBox textBox in shopTable.Controls.OfType<TextBox>())
                {
                    textBox.BackColor = blackFormBack;
                    textBox.ForeColor = blackText;
                }
            }

            //Fixt das Problem, dass der Updatetext nicht blau hinterlegt ist
            if (versionText.Text.Length > 20)
            {
                versionText.ForeColor = SystemColors.Highlight;
            }
        }

    }
}