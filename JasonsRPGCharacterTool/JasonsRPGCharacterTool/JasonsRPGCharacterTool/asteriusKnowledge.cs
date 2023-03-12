using JasonsRPGCharacterTool.Global_stuff;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace JasonsRPGCharacterTool
{
    public partial class asteriusKnowledge : Form
    {
        darkMode darkTheme = new darkMode();

        private System.Windows.Forms.Timer timer1;
        //private System.Windows.Forms.Timer timer2;

        //bool startFrom0 = false;
        //bool startFrom100 = false;

        //int timer = 0;

        public asteriusKnowledge()
        {
            InitializeComponent();

            //Übersetzt alle Strings in die richtige Sprache
            localizeAndShowPoints();

            //Startet den Timer um das Form immer an der richtigen Position zu haben.
            InitTimer();

            //Unbenutzt, war für langsame Transition (Also FadeIn/Out) geplant, hat aber nicht zufriedenstellend funktioniert.
            //fadeTimer();

            //Setzt auch hier das Theme für das Form
            setTheme(darkTheme.checkMode());
        }

        //Startet den Timer
        public void InitTimer()
        {
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 1;
            timer1.Start();
        }

        //Positioniert das Form an jedem Tick an die richtige Position
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Owner != null)
            {
                Point trueLocation = Owner.Location;

                //trueLocation = new Point(trueLocation.X + 1000, trueLocation.Y + 105);

                trueLocation = new Point(trueLocation.X + 1000, trueLocation.Y + 115);
                this.Location = trueLocation;
            }
        }

        //Geplantes Feature, jedoch erstmal auf Eis gelegt
        /*
        public void fadeTimer()
        {
            timer2 = new System.Windows.Forms.Timer();
            timer2.Tick += new EventHandler(timer2_Tick);
            timer2.Interval = 1;
            timer2.Start();
        }


        private void timer2_Tick(object sender, EventArgs e)
        {
            if (timer != 20)
            {
                Console.WriteLine("Start " + this.Opacity);
                if (startFrom0 == true)
                {
                    this.Opacity += 0.05;
                }

                else if (startFrom100 == true)
                {
                    this.Opacity -= 0.05;
                }
                timer++;
            }
            else
            {
                timer = 0;
                timer2.Stop();
            }
        }


        public void lowerOpacity()
        {
            timer2.Stop();
            timer = 0;

            startFrom0 = false;
            startFrom100 = true;
            fadeTimer();

            this.Visible = false;
        }

        public void increaseOpacity()
        {
            timer2.Stop();
            timer = 0;

            this.Visible = true;

            startFrom0 = true;
            startFrom100 = false;
            fadeTimer();
        }

    */

        //Übersetzt die Strings in die richtige Sprache und zeigt die vergebenen Punkte
        public void localizeAndShowPoints()
        {
            //Setzt das Theme, damit es auch beim Wechsel übernommen wird
            setTheme(darkTheme.checkMode());

            knowledgeLanguageText.Text = Properties.strings.standaloneKnowledge + " && " + Properties.strings.standaloneLanguages + " (" + (Asterius_stuff.character.knowGuns + Asterius_stuff.character.knowUnarmed + Asterius_stuff.character.knowExplosives + Asterius_stuff.character.knowPol + Asterius_stuff.character.knowPlants + Asterius_stuff.character.knowSpaceShips + Asterius_stuff.character.knowTraps + Asterius_stuff.character.knowArchitecture + Asterius_stuff.character.knowMedicine + Asterius_stuff.character.knowHistory + Asterius_stuff.character.knowTech + Asterius_stuff.character.knowComputers + Asterius_stuff.character.knowBionic + Asterius_stuff.character.knowCriminality + Asterius_stuff.character.knowAstronomy + Asterius_stuff.character.knowSciences + Asterius_stuff.character.knowMilitary + Asterius_stuff.character.langusl + Asterius_stuff.character.langfornsh + Asterius_stuff.character.langmekish + Asterius_stuff.character.langlop + Asterius_stuff.character.langvulsh) + "/" + (6 + (Asterius_stuff.character.knowledgePoints)) + ")";

            charKnowledge.Text = Properties.strings.knowledge;

            charKnowledgeGuns.Text = Properties.strings.knowledgeGuns + ": " + Asterius_stuff.character.knowGuns;
            charKnowledgeUnarmed.Text = Properties.strings.knowledgeUnarmed + ": " + Asterius_stuff.character.knowUnarmed;
            charKnowledgeExplosives.Text = Properties.strings.knowledgeExplosives + ": " + Asterius_stuff.character.knowExplosives;
            charKnowledgePolitics.Text = Properties.strings.knowledgePolitics + ": " + Asterius_stuff.character.knowPol;
            charKnowledgePlants.Text = Properties.strings.knowledgePlants + ": " + Asterius_stuff.character.knowPlants;
            charKnowledgeSpaceships.Text = Properties.strings.knowledgeSpaceships + ": " + Asterius_stuff.character.knowSpaceShips;
            charKnowledgeTraps.Text = Properties.strings.knowledgeTraps + ": " + Asterius_stuff.character.knowTraps;
            charKnowledgeArchitecture.Text = Properties.strings.knowledgeArchitecture + ": " + Asterius_stuff.character.knowArchitecture;
            charKnowledgeMedicine.Text = Properties.strings.knowledgeMedicine + ": " + Asterius_stuff.character.knowMedicine;
            charKnowledgeHistory.Text = Properties.strings.knowledgeHistory + ": " + Asterius_stuff.character.knowHistory;
            charKnowledgeTech.Text = Properties.strings.knowledgeTech + ": " + Asterius_stuff.character.knowTech;
            charKnowledgeComputers.Text = Properties.strings.knowledgeComputers + ": " + Asterius_stuff.character.knowComputers;
            charKnowledgeBionic.Text = Properties.strings.knowledgeBionic + ": " + Asterius_stuff.character.knowBionic;
            charKnowledgeCriminality.Text = Properties.strings.knowledgeCriminality + ": " + Asterius_stuff.character.knowCriminality;
            charKnowledgeAstronomy.Text = Properties.strings.knowledgeAstronomy + ": " + Asterius_stuff.character.knowAstronomy;
            charKnowledgeSciences.Text = Properties.strings.knowledgeSciences + ": " + Asterius_stuff.character.knowSciences;
            charKnowledgeMilitary.Text = Properties.strings.knowledgeMilitary + ": " + Asterius_stuff.character.knowMilitary;

            languages.Text = Properties.strings.languages;

            charLanguagesUSL.Text = Properties.strings.charLangUSL + " " + Asterius_stuff.character.langusl;
            charLanguagesFornsh.Text = Properties.strings.charLangFornsh + " " + Asterius_stuff.character.langfornsh;
            charLanguagesMekish.Text = Properties.strings.charLangMekish + " " + Asterius_stuff.character.langmekish;
            charLanguagesLop.Text = Properties.strings.charLangLop + " " + Asterius_stuff.character.langlop;
            charLanguagesVulsh.Text = Properties.strings.charLangVulsh + " " + Asterius_stuff.character.langvulsh;
        }

        private void KnowledgeBonus(object sender, MouseEventArgs e)
        {
            //Speichert das sender label als ein neues Label
            Label lbl = sender as Label;

            //Wenn das Speichern funktioniert hat, fahre mit dem Code fort
            if (lbl != null && !asteriusPnP.isLocked)
            {
                switch (lbl.Name)
                {
                    case "charKnowledgeGuns":
                        //Vergleicht die Klickarten
                        switch (e.Button)
                        {
                            //Bei Linksklick wird der Wert um 1 erhöht
                            case MouseButtons.Left:
                                if (Asterius_stuff.character.knowGuns < 5)
                                {
                                    Asterius_stuff.character.knowGuns++;
                                }
                                break;
                            //Bei Rechtsklick wird der Wert um 1 verringert
                            case MouseButtons.Right:
                                if (Asterius_stuff.character.knowGuns > 0)
                                {
                                    Asterius_stuff.character.knowGuns--;
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charKnowledgeUnarmed":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                if (Asterius_stuff.character.knowUnarmed < 5)
                                {
                                    Asterius_stuff.character.knowUnarmed++;
                                }
                                break;
                            case MouseButtons.Right:
                                if (Asterius_stuff.character.knowUnarmed > 0)
                                {
                                    Asterius_stuff.character.knowUnarmed--;
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charKnowledgeExplosives":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                if (Asterius_stuff.character.knowExplosives < 5)
                                {
                                    Asterius_stuff.character.knowExplosives++;
                                }
                                break;
                            case MouseButtons.Right:
                                if (Asterius_stuff.character.knowExplosives > 0)
                                {
                                    Asterius_stuff.character.knowExplosives--;
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charKnowledgePolitics":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                if (Asterius_stuff.character.knowPol < 5)
                                {
                                    Asterius_stuff.character.knowPol++;
                                }
                                break;
                            case MouseButtons.Right:
                                if (Asterius_stuff.character.knowPol > 0)
                                {
                                    Asterius_stuff.character.knowPol--;
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charKnowledgePlants":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                if (Asterius_stuff.character.knowPlants < 5)
                                {
                                    Asterius_stuff.character.knowPlants++;
                                }
                                break;
                            case MouseButtons.Right:
                                if (Asterius_stuff.character.knowPlants > 0)
                                {
                                    Asterius_stuff.character.knowPlants--;
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charKnowledgeSpaceships":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                if (Asterius_stuff.character.knowSpaceShips < 5)
                                {
                                    Asterius_stuff.character.knowSpaceShips++;
                                }
                                break;
                            case MouseButtons.Right:
                                if (Asterius_stuff.character.knowSpaceShips > 0)
                                {
                                    Asterius_stuff.character.knowSpaceShips--;
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charKnowledgeTraps":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                if (Asterius_stuff.character.knowTraps < 5)
                                {
                                    Asterius_stuff.character.knowTraps++;
                                }
                                break;
                            case MouseButtons.Right:
                                if (Asterius_stuff.character.knowTraps > 0)
                                {
                                    Asterius_stuff.character.knowTraps--;
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charKnowledgeArchitecture":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                if (Asterius_stuff.character.knowArchitecture < 5)
                                {
                                    Asterius_stuff.character.knowArchitecture++;
                                }
                                break;
                            case MouseButtons.Right:
                                if (Asterius_stuff.character.knowArchitecture > 0)
                                {
                                    Asterius_stuff.character.knowArchitecture--;
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charKnowledgeMedicine":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                if (Asterius_stuff.character.knowMedicine < 5)
                                {
                                    Asterius_stuff.character.knowMedicine++;
                                }
                                break;
                            case MouseButtons.Right:
                                if (Asterius_stuff.character.knowMedicine > 0)
                                {
                                    Asterius_stuff.character.knowMedicine--;
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charKnowledgeHistory":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                if (Asterius_stuff.character.knowHistory < 5)
                                {
                                    Asterius_stuff.character.knowHistory++;
                                }
                                break;
                            case MouseButtons.Right:
                                if (Asterius_stuff.character.knowHistory > 0)
                                {
                                    Asterius_stuff.character.knowHistory--;
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charKnowledgeTech":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                if (Asterius_stuff.character.knowTech < 5)
                                {
                                    Asterius_stuff.character.knowTech++;
                                }
                                break;
                            case MouseButtons.Right:
                                if (Asterius_stuff.character.knowTech > 0)
                                {
                                    Asterius_stuff.character.knowTech--;
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charKnowledgeComputers":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                if (Asterius_stuff.character.knowComputers < 5)
                                {
                                    Asterius_stuff.character.knowComputers++;
                                }
                                break;
                            case MouseButtons.Right:
                                if (Asterius_stuff.character.knowComputers > 0)
                                {
                                    Asterius_stuff.character.knowComputers--;
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charKnowledgeBionic":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                if (Asterius_stuff.character.knowBionic < 5)
                                {
                                    Asterius_stuff.character.knowBionic++;
                                }
                                break;
                            case MouseButtons.Right:
                                if (Asterius_stuff.character.knowBionic > 0)
                                {
                                    Asterius_stuff.character.knowBionic--;
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charKnowledgeCriminality":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                if (Asterius_stuff.character.knowCriminality < 5)
                                {
                                    Asterius_stuff.character.knowCriminality++;
                                }
                                break;
                            case MouseButtons.Right:
                                if (Asterius_stuff.character.knowCriminality > 0)
                                {
                                    Asterius_stuff.character.knowCriminality--;
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charKnowledgeAstronomy":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                if (Asterius_stuff.character.knowAstronomy < 5)
                                {
                                    Asterius_stuff.character.knowAstronomy++;
                                }
                                break;
                            case MouseButtons.Right:
                                if (Asterius_stuff.character.knowAstronomy > 0)
                                {
                                    Asterius_stuff.character.knowAstronomy--;
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charKnowledgeSciences":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                if (Asterius_stuff.character.knowSciences < 5)
                                {
                                    Asterius_stuff.character.knowSciences++;
                                }
                                break;
                            case MouseButtons.Right:
                                if (Asterius_stuff.character.knowSciences > 0)
                                {
                                    Asterius_stuff.character.knowSciences--;
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charKnowledgeMilitary":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                if (Asterius_stuff.character.knowMilitary < 5)
                                {
                                    Asterius_stuff.character.knowMilitary++;
                                }
                                break;
                            case MouseButtons.Right:
                                if (Asterius_stuff.character.knowMilitary > 0)
                                {
                                    Asterius_stuff.character.knowMilitary--;
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charLanguagesUSL":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                if (Asterius_stuff.character.langusl < 5)
                                {
                                    Asterius_stuff.character.langusl++;
                                }
                                break;
                            case MouseButtons.Right:
                                if (Asterius_stuff.character.langusl > 0)
                                {
                                    Asterius_stuff.character.langusl--;
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charLanguagesFornsh":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                if (Asterius_stuff.character.langfornsh < 5)
                                {
                                    Asterius_stuff.character.langfornsh++;
                                }
                                break;
                            case MouseButtons.Right:
                                if (Asterius_stuff.character.langfornsh > 0)
                                {
                                    Asterius_stuff.character.langfornsh--;
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charLanguagesMekish":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                if (Asterius_stuff.character.langmekish < 5)
                                {
                                    Asterius_stuff.character.langmekish++;
                                }
                                break;
                            case MouseButtons.Right:
                                if (Asterius_stuff.character.langmekish > 0)
                                {
                                    Asterius_stuff.character.langmekish--;
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charLanguagesLop":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                if (Asterius_stuff.character.langlop < 5)
                                {
                                    Asterius_stuff.character.langlop++;
                                }
                                break;
                            case MouseButtons.Right:
                                if (Asterius_stuff.character.langlop > 0)
                                {
                                    Asterius_stuff.character.langlop--;
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charLanguagesVulsh":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                if (Asterius_stuff.character.langvulsh < 5)
                                {
                                    Asterius_stuff.character.langvulsh++;
                                }
                                break;
                            case MouseButtons.Right:
                                if (Asterius_stuff.character.langvulsh > 0)
                                {
                                    Asterius_stuff.character.langvulsh--;
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    default:
                        break;
                }
            }
            localizeAndShowPoints();
        }

        public void setTheme(string mode)
        {
            Color darkFormBackground = darkTheme.darkFormBackground;
            Color lightFormBackground = darkTheme.lightFormBackground;

            Color darkBackground = darkTheme.darkBackground;
            Color lightBackground = darkTheme.lightBackground;

            Color darkText = darkTheme.darkText;
            Color darkTextLighter = darkTheme.darkTextLighter;

            Color lightText = darkTheme.lightText;
            Color lightTextDarker = darkTheme.lightTextDarker;

            if (mode == "Dark")
            {
                this.BackColor = darkFormBackground;
                knowledgeFlowPanel.BackColor = darkFormBackground;

                foreach (Label text in knowledgeFlowPanel.Controls.OfType<Label>())
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
            }

            else if (mode == "Light")
            {
                this.BackColor = lightFormBackground;
                knowledgeFlowPanel.BackColor = lightFormBackground;

                foreach (Label text in knowledgeFlowPanel.Controls.OfType<Label>())
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
            }

            else if (mode == "Black")
            {
                Color blackFormBack = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));

                Color blackText = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
                Color blackTextLighter = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));

                this.BackColor = blackFormBack;
                knowledgeFlowPanel.BackColor = blackFormBack;

                foreach (Label text in knowledgeFlowPanel.Controls.OfType<Label>())
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
            }
        }

    }
}
