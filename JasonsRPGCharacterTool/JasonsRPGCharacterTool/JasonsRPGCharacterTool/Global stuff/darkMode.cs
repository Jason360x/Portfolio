using Microsoft.Win32;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace JasonsRPGCharacterTool.Global_stuff
{
    class darkMode
    {
        //Die Farbe des Forms
        public Color darkFormBackground = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30))))); //Dark Mode
        public Color lightFormBackground = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))); //Light Mode

        //Die Hintergründe z.B. für Textboxen
        public Color darkBackground = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45))))); //Dark Mode
        public Color lightBackground = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))); //Light Mode

        //Textfarben
        public Color darkText = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45))))); //Light Mode
        public Color darkTextLighter = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))); //Light Mode, Überschriften only

        //Textfarben²
        public Color lightText = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200))))); //Dark Mode
        public Color lightTextDarker = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230))))); //Dark Mode, Überschriften only

        public string checkMode()
        {
            //"Dark", "Light" oder "Black" forcieren das Programm auf eines der Themes

            //-Black ist buggy und clasht mit dem Lockmode, daher wird es nicht automatisch gesetzt (z.B. in der späten Nacht, etc)-
            if (Program.startArgs.Length > 0)
            {
                string[] exeArgs = Program.startArgs;

                for (int i = 0; i < exeArgs.Length; i++)
                {
                    switch (exeArgs[i].ToUpper())
                    {
                        case "-DARK":
                        case "DARK":
                        case "-DUNKEL":
                        case "DUNKEL":
                            return "Dark";

                        case "-LIGHT":
                        case "LIGHT":
                        case "-HELL":
                        case "HELL":
                            return "Light";

                        case "-BLACK":
                        case "BLACK":
                        case "-SCHWARZ":
                        case "SCHWARZ":
                            return "Black";

                        default:
                            break;
                    }
                }
            }

            if (Environment.OSVersion.Version.Major >= 10)
            {

                bool keyExists = false;
                RegistryKey theme;

                try
                {
                    //Der Key beinhaltet den Value "AppsUseLightTheme"
                    theme = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");

                    keyExists = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Couldn't find dark mode registry key! Higher than Win 10?! Exception: " + e);
                    throw;
                }

                if (keyExists)
                {
                    //AppsUseLightTheme == 0: Dark Mode 
                    //AppsUseLightTheme == 1: Light Mode

                    string mode = theme.GetValue("AppsUseLightTheme").ToString();

                    if (mode == "0")
                    {
                        return "Dark";
                    }
                    else
                    {
                        return "Light";
                    }
                }
            }


            return "Light";
        }

        public void changeColorForEach(Color primaryTextCol, Color secondaryTextCol, Color backCol, string type, Panel parentPanel)
        {
            switch (type)
            {
                case "Label":
                    foreach (Label text in parentPanel.Controls.OfType<Label>())
                    {
                        if (text.Tag == null || text.Tag.ToString() != "KeepColor")
                        {
                            if (text.Tag != null && text.Tag.ToString() == "IsBold")
                            {
                                text.ForeColor = primaryTextCol;
                            }
                            else
                            {
                                text.ForeColor = secondaryTextCol;
                            }
                        }
                    }
                    break;

                case "TextBox":
                    foreach (TextBox txtBox in parentPanel.Controls.OfType<TextBox>())
                    {
                        if (txtBox.Tag == null || txtBox.Tag.ToString() != "KeepColor")
                        {
                            txtBox.BackColor = backCol;
                            txtBox.ForeColor = secondaryTextCol;
                        }
                    }
                    break;

                case "UpDown":
                    foreach (NumericUpDown upDwn in parentPanel.Controls.OfType<NumericUpDown>())
                    {
                        if (upDwn.Tag == null || upDwn.Tag.ToString() != "KeepColor")
                        {
                            upDwn.BackColor = backCol;
                            upDwn.ForeColor = secondaryTextCol;
                        }
                    }
                    break;

                case "RichText":
                    foreach (RichTextBox richText in parentPanel.Controls.OfType<RichTextBox>())
                    {
                        if (richText.Tag == null || richText.Tag.ToString() != "KeepColor")
                        {
                            richText.BackColor = backCol;
                            richText.ForeColor = secondaryTextCol;
                        }
                    }
                    break;

                case "Button":
                    foreach (Button btn in parentPanel.Controls.OfType<Button>())
                    {
                        if (btn.Tag == null || btn.Tag.ToString() != "KeepColor")
                        {
                            btn.BackColor = backCol;
                            btn.ForeColor = secondaryTextCol;
                        }
                    }
                    break;

                case "CheckBox":
                    foreach (CheckBox checkBox in parentPanel.Controls.OfType<CheckBox>())
                    {
                        if (checkBox.Tag == null || checkBox.Tag.ToString() != "KeepColor")
                        {
                            checkBox.ForeColor = secondaryTextCol;
                        }
                    }
                    break;

                case "ComboBox":
                    foreach (ComboBox combBox in parentPanel.Controls.OfType<ComboBox>())
                    {
                        if (combBox.Tag == null || combBox.Tag.ToString() != "KeepColor")
                        {
                            combBox.BackColor = backCol;
                            combBox.ForeColor = secondaryTextCol;
                        }
                    }
                    break;

                default:
                    break;
            }

        }

        public void changeAllToDark(Panel parentPanel)
        {
            changeColorForEach(lightTextDarker, lightText, darkBackground, "Label", parentPanel);
            changeColorForEach(lightTextDarker, lightText, darkBackground, "TextBox", parentPanel);
            changeColorForEach(lightTextDarker, lightText, darkBackground, "UpDown", parentPanel);
            changeColorForEach(lightTextDarker, lightText, darkBackground, "RichText", parentPanel);
            changeColorForEach(lightTextDarker, lightText, darkBackground, "Button", parentPanel);
            changeColorForEach(lightTextDarker, lightText, darkBackground, "CheckBox", parentPanel);
            changeColorForEach(lightTextDarker, lightText, darkBackground, "ComboBox", parentPanel);
        }

        public void changeAllToLight(Panel parentPanel)
        {
            changeColorForEach(darkTextLighter, darkText, lightBackground, "Label", parentPanel);
            changeColorForEach(darkTextLighter, darkText, lightBackground, "TextBox", parentPanel);
            changeColorForEach(darkTextLighter, darkText, lightBackground, "UpDown", parentPanel);
            changeColorForEach(darkTextLighter, darkText, lightBackground, "RichText", parentPanel);
            changeColorForEach(darkTextLighter, darkText, lightBackground, "Button", parentPanel);
            changeColorForEach(darkTextLighter, darkText, lightBackground, "CheckBox", parentPanel);
            changeColorForEach(darkTextLighter, darkText, lightBackground, "ComboBox", parentPanel);
        }

        public void changeAllToColor(Color primaryTextCol, Color secondaryTextCol, Color backCol, Panel parentPanel)
        {
            changeColorForEach(primaryTextCol, secondaryTextCol, backCol, "Label", parentPanel);
            changeColorForEach(primaryTextCol, secondaryTextCol, backCol, "TextBox", parentPanel);
            changeColorForEach(primaryTextCol, secondaryTextCol, backCol, "UpDown", parentPanel);
            changeColorForEach(primaryTextCol, secondaryTextCol, backCol, "RichText", parentPanel);
            changeColorForEach(primaryTextCol, secondaryTextCol, backCol, "Button", parentPanel);
            changeColorForEach(primaryTextCol, secondaryTextCol, backCol, "CheckBox", parentPanel);
            changeColorForEach(primaryTextCol, secondaryTextCol, backCol, "ComboBox", parentPanel);
        }
    }
}
