using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Net;

namespace JasonsRPGCharacterTool
{
    public partial class tesPnp : Form
    {
        //Definiert einige Farben für die selbstgemachten Knöpfe
        private readonly Color highlightRed = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(141)))), ((int)(((byte)(133)))));
        private readonly Color exitRed = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(78)))), ((int)(((byte)(61)))));

        private readonly Color backNonHighlight1 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
        private readonly Color backHighlight1 = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(94)))), ((int)(((byte)(122)))));

        private readonly Color backNonHighlight2 = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(114)))), ((int)(((byte)(170)))));
        private readonly Color backHighlight2 = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));

        int currentCharacter = 1;

        int label1 = 0;
        string tempLoadPath = "";
        string loadPath = "";

        List<Label> textBoxes = new List<Label>();

        public static bool isLocked = false;

        TES_stuff.xmlCreator_TES xmlCreator = new TES_stuff.xmlCreator_TES();

        Global_stuff.darkMode darkTheme = new Global_stuff.darkMode();

        //Instanziiert die Klasse für das Lesen und Schreiben der ZIP Daten
        Global_stuff.compressFiles fileCompress = new Global_stuff.compressFiles();

        public tesPnp()
        {
            InitializeComponent();

            //Ändert den Versionsstring zur installierten Version
            versionText.Text = "v" + Application.ProductVersion;

            //Überprüft ob ein Update verfügbar ist
            checkForUpdates();

            //Erstellt XMLs, falls sie nicht da sind.
            xmlCreator.createXMLsAtStart();

            //Zeigt beim Start die Charaktere an.
            showCharacters();

            //Lokalisiert die entsprechenden Strings zu deutsch oder englisch.
            localizeCharStuff();
            creditsUpperText.Text = Properties.strings.creditsText;
            exitText.Text = Properties.strings.exitText;
            characterButton.Text = Properties.strings.characterButtonText;
            rollButton.Text = Properties.strings.rollButtonText;
            shopButton.Text = Properties.strings.shopButton;
            creditsButton.Text = Properties.strings.creditsButtonText;
            rollHelpText.Text = Properties.strings.rollHelpText;
            customRollText.Text = Properties.strings.customRollText;
            customRollButton.Text = Properties.strings.customRollButtonText;
            minimumText.Text = Properties.strings.minimumText;
            maximumText.Text = Properties.strings.maximumText;
            shopNameText.Text = Properties.strings.shopName;
            shopAmountText.Text = Properties.strings.shopAmount;
            shopPriceText.Text = Properties.strings.shopPrice;

            //Fixt den Namensbug
            messyNamesBugFix();

            //Erstellt Reihen für die Tabelle im Shop
            messyFix();

            //Zeigt beim Start den 1. Charakter an.
            openChar();

            //Fixt den zu großen Text im Englischen
            CultureInfo culture = Thread.CurrentThread.CurrentUICulture;
            if (culture.Name.Contains("en-"))
            {
                companionChar1Button.Font = new System.Drawing.Font("Segoe UI", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                companionChar2Button.Font = new System.Drawing.Font("Segoe UI", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                companionChar3Button.Font = new System.Drawing.Font("Segoe UI", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }

            setTheme(darkTheme.checkMode());
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


        //Übersetzt alles im Characterpanel auf deutsch bzw. englisch
        private void localizeCharStuff()
        {
            saveButton.Text = Properties.strings.saveCharsToZip;
            loadButton.Text = Properties.strings.loadCharsFromZip;

            characterNameText.Text = Properties.strings.charNameText;
            charAgeText.Text = Properties.strings.charAgeText;
            charHeightText.Text = Properties.strings.charHeightText;
            charGenderText.Text = Properties.strings.charGenderText;
            charRaceText.Text = Properties.strings.charRaceText;
            charStyleText.Text = Properties.strings.charStyleText;
            idealPicSizeInfo.Text = Properties.strings.idealPiceSizeInfoText;

            charAttribText.Text = Properties.strings.charAttribText;
            charAttribBaseText.Text = Properties.strings.charAttribBaseText;
            charAttribBaseText2.Text = Properties.strings.charAttribBaseText;
            charAttribBonusText.Text = Properties.strings.charAttribBonusText;
            charAttribBonusText2.Text = Properties.strings.charAttribBonusText;

            charPerceptText.Text = Properties.strings.charPerceptText;
            charEndurText.Text = Properties.strings.charEnduraText;
            charDexterText.Text = Properties.strings.charDexterText;
            charIntelliText.Text = Properties.strings.charIntelliText;
            charChariText.Text = Properties.strings.charChariText;
            charConcentText.Text = Properties.strings.charConcentText;
            charStrengthText.Text = Properties.strings.charStrengthText;
            charAgilityText.Text = Properties.strings.charAgilityText;

            charHPMaxText.Text = Properties.strings.charHPMaxText;
            charHPCurText.Text = Properties.strings.charHPCurrText;
            charMagMaxText.Text = Properties.strings.charMagMaxText;
            charMagCurrText.Text = Properties.strings.charMagCurrText;
            charPhysArmorText.Text = Properties.strings.charPhysArmText;
            charMagicArmorText.Text = Properties.strings.charMagArmText;
            charParryText.Text = Properties.strings.charParryText;

            charRollReqText.Text = Properties.strings.charRollReqText;
            charInitText.Text = Properties.strings.charInitText;
            charFireResText.Text = Properties.strings.charFireResText;
            charFrostResText.Text = Properties.strings.charFrostResText;
            charElectResText.Text = Properties.strings.charElectResText;
            charPoisResText.Text = Properties.strings.charPoisResText;
            charMonsterText.Text = Properties.strings.charMonsterText;

            charTalentsText.Text = Properties.strings.charTalentsText;
            charHeavWeapText.Text = Properties.strings.charHeavWeapText;
            charLightWeapText.Text = Properties.strings.charLightWeapText;
            charArcheryText.Text = Properties.strings.charArcheryText;
            charThrowText.Text = Properties.strings.charThrowText;
            charUnarmedText.Text = Properties.strings.charUnarmedText;
            charSpeechText.Text = Properties.strings.charSpeechText;
            charPickpocketText.Text = Properties.strings.charPickpocketText;
            charSneakText.Text = Properties.strings.charSneakText;
            charLockpickText.Text = Properties.strings.charLockpickText;
            charSingingText.Text = Properties.strings.charSingingText;
            charDrinkingText.Text = Properties.strings.charDrinkingText;

            charDestructionText.Text = Properties.strings.charDestructionText;
            charIllusionText.Text = Properties.strings.charIllusionText;
            charAlterationText.Text = Properties.strings.charAlterationText;
            charRestorationText.Text = Properties.strings.charRestorationText;
            charEnchantingText.Text = Properties.strings.charEnchantingText;
            charConjurationText.Text = Properties.strings.charConjurationText;
            charSmithingText.Text = Properties.strings.charSmithingText;
            charAlchemyText.Text = Properties.strings.charAlchemyText;
            charTrapsText.Text = Properties.strings.charTrapsText;
            charCraftingText.Text = Properties.strings.charCraftingText;

            charNotesText.Text = Properties.strings.charNotesText;

            playerChar1SelectButton.Text = Properties.strings.playerChar1Name;
            playerChar2SelectButton.Text = Properties.strings.playerChar2Name;
            playerChar3SelectButton.Text = Properties.strings.playerChar3Name;
            playerChar4SelectButton.Text = Properties.strings.playerChar4Name;

            companionChar1Button.Text = Properties.strings.companionChar1Name;
            companionChar2Button.Text = Properties.strings.companionChar2Name;
            companionChar3Button.Text = Properties.strings.companionChar3Name;

            enemyChar1Button.Text = Properties.strings.enemyChar1Name;
            enemyChar2Button.Text = Properties.strings.enemyChar2Name;
            enemyChar3Button.Text = Properties.strings.enemyChar3Name;
            enemyChar4Button.Text = Properties.strings.enemyChar4Name;
            enemyChar5Button.Text = Properties.strings.enemyChar5Name;
            enemyChar6Button.Text = Properties.strings.enemyChar6Name;
            enemyChar7Button.Text = Properties.strings.enemyChar7Name;
            enemyChar8Button.Text = Properties.strings.enemyChar8Name;
        }

        //Fixt das Problem, dass beim Start der App die Namen oben nicht richtig angezeigt werden.
        private void messyNamesBugFix()
        {
            for (int i = 0; i < 15; i++)
            {
                currentCharacter++;
                openChar();
            }
            currentCharacter = 1;
        }

        //Blendet alle UI Elemente aus.
        private void hideAll()
        {
            rollPanel.Hide();
            creditsPanel.Hide();
            characterPanel.Hide();
            shopPanel.Hide();
        }

        //Blendet alle UI Elemente aus und die fürs Würfeln ein.
        private void showRolls()
        {
            hideAll();
            rollPanel.Show();
        }

        //Blendet alle UI Elemente aus und die für die Credits ein.
        private void showCredits()
        {
            hideAll();
            creditsPanel.Show();
        }

        //Blendet alle UI Elemente aus und die für die Charaktere ein.
        private void showCharacters()
        {
            hideAll();
            characterPanel.Show();
        }

        private void showShop()
        {
            hideAll();
            shopPanel.Show();
        }

        //Beschreibt was OnClick bei den Seitenknöpfen passiert.
        private void characterButton_Click(object sender, EventArgs e)
        {
            saveChar();
            showCharacters();
        }

        private void rollButton_Click(object sender, EventArgs e)
        {
            saveChar();
            showRolls();
        }

        private void creditsButton_Click(object sender, EventArgs e)
        {
            saveChar();
            showCredits();
        }


        private void shopButton_Click(object sender, EventArgs e)
        {
            saveChar();
            showShop();
        }


        //OnClick des Beenden Buttons wird die App beendet und der aktuelle Charakter gespeichert.
        private void exitText_Click(object sender, EventArgs e)
        {
            saveChar();
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

        //Hier das gleiche für alle anderen Buttons
        private void characterButton_MouseEnter(object sender, EventArgs e)
        {
            characterButton.BackColor = backHighlight1;
        }

        private void characterButton_MouseLeave(object sender, EventArgs e)
        {
            characterButton.BackColor = backNonHighlight1;
        }

        private void rollButton_MouseEnter(object sender, EventArgs e)
        {
            rollButton.BackColor = backHighlight2;
        }

        private void rollButton_MouseLeave(object sender, EventArgs e)
        {
            rollButton.BackColor = backNonHighlight2;
        }

        private void creditsButton_MouseEnter(object sender, EventArgs e)
        {
            creditsButton.BackColor = backHighlight2;
        }

        private void creditsButton_MouseLeave(object sender, EventArgs e)
        {
            creditsButton.BackColor = backNonHighlight2;
        }

        private void shopButton_MouseEnter(object sender, EventArgs e)
        {
            shopButton.BackColor = backHighlight1;
        }

        private void shopButton_MouseLeave(object sender, EventArgs e)
        {
            shopButton.BackColor = backNonHighlight1;
        }

        //Generiert eine zufällige Zahl zwischen dem gegebenen Minimum und Maximum (Zahl = inkl. Minimum, exkl. Maximum)
        private string generateRandom(int min, int max)
        {
            Random random = new Random();
            int randNum = random.Next(min, max);

            string number = randNum.ToString();

            return number;
        }

        //Ändert OnClick vom jeweiligen Würfel die Nummer
        private void d4Pic_Click(object sender, EventArgs e)
        {
            string number = generateRandom(1, 5);
            d4Text.Text = number;
        }

        private void d6Pic_Click(object sender, EventArgs e)
        {
            string number = generateRandom(1, 7);
            d6Text.Text = number;
        }

        private void d10Pic_Click(object sender, EventArgs e)
        {
            string number = generateRandom(1, 11);
            d10Text.Text = number;
        }

        private void d12Pic_Click(object sender, EventArgs e)
        {
            string number = generateRandom(1, 13);
            d12Text.Text = number;
        }

        private void d20Pic_Click(object sender, EventArgs e)
        {
            string number = generateRandom(1, 21);
            d20Text.Text = number;
        }

        private void d100Pic_Click(object sender, EventArgs e)
        {
            string number = generateRandom(1, 101);
            d100Text.Text = number;
        }

        private void customRollButton_Click(object sender, EventArgs e)
        {
            int min = Decimal.ToInt32(minimumNumber.Value);
            int max = Decimal.ToInt32(maximumNumber.Value);
            max = max + 1;

            string number = generateRandom(min, max);

            customRollButton.Text = number;
        }

        void copyChar()
        {
            TES_stuff.character.name = charNameTextBox.Text;
            TES_stuff.character.age = charAgeTextBox.Text;
            TES_stuff.character.height = charHeightTextBox.Text;
            TES_stuff.character.gender = charGenderTextBox.Text;
            TES_stuff.character.race = charRaceTextBox.Text;
            TES_stuff.character.style = charStyleRichTextBox.Text;

            TES_stuff.character.hpmax = Decimal.ToInt32(charHpMax.Value);
            TES_stuff.character.hpcurr = Decimal.ToInt32(charHpCurr.Value);
            TES_stuff.character.magickamax = Decimal.ToInt32(charMagMax.Value);
            TES_stuff.character.magickacurr = Decimal.ToInt32(charMagCurr.Value);
            TES_stuff.character.physarmor = Decimal.ToInt32(charPhysArmor.Value);
            TES_stuff.character.magarmor = Decimal.ToInt32(charMagArmor.Value);
            TES_stuff.character.parry = Decimal.ToInt32(charParry.Value);

            TES_stuff.character.rollreq = Decimal.ToInt32(charRollReq.Value);
            TES_stuff.character.init = Decimal.ToInt32(charInit.Value);
            TES_stuff.character.fireresist = Decimal.ToInt32(charFireRes.Value);
            TES_stuff.character.frostresist = Decimal.ToInt32(charFrostRes.Value);
            TES_stuff.character.electroresist = Decimal.ToInt32(charElectRes.Value);
            TES_stuff.character.poisonresist = Decimal.ToInt32(charPoisRes.Value);
            TES_stuff.character.monster = charMonsterTextBox.Text;

            TES_stuff.character.perception = Decimal.ToInt32(charPerceptBase.Value);
            TES_stuff.character.perceptionBonus = Decimal.ToInt32(charPerceptBonus.Value);

            TES_stuff.character.endurance = Decimal.ToInt32(charEndurBase.Value);
            TES_stuff.character.enduranceBonus = Decimal.ToInt32(charEndurBonus.Value);

            TES_stuff.character.dexter = Decimal.ToInt32(charDexterBase.Value);
            TES_stuff.character.dexterBonus = Decimal.ToInt32(charDexterBonus.Value);

            TES_stuff.character.intelli = Decimal.ToInt32(charIntelliBase.Value);
            TES_stuff.character.intelliBonus = Decimal.ToInt32(charIntelliBonus.Value);

            TES_stuff.character.chari = Decimal.ToInt32(charChariBase.Value);
            TES_stuff.character.chariBonus = Decimal.ToInt32(charChariBonus.Value);

            TES_stuff.character.concent = Decimal.ToInt32(charConcentBase.Value);
            TES_stuff.character.concentBonus = Decimal.ToInt32(charConcentBonus.Value);

            TES_stuff.character.strength = Decimal.ToInt32(charStrengthBase.Value);
            TES_stuff.character.strengthBonus = Decimal.ToInt32(charStrengthBonus.Value);

            TES_stuff.character.agility = Decimal.ToInt32(charAgilityBase.Value);
            TES_stuff.character.agilityBonus = Decimal.ToInt32(charAgilityBonus.Value);

            TES_stuff.character.notes = charNotesRichTextBox.Text;

            TES_stuff.character.heavyweap = Decimal.ToInt32(charHeavWeap.Value);
            TES_stuff.character.lightweap = Decimal.ToInt32(charLightWeap.Value);
            TES_stuff.character.archery = Decimal.ToInt32(charArchery.Value);
            TES_stuff.character.throwing = Decimal.ToInt32(charThrow.Value);
            TES_stuff.character.unarmed = Decimal.ToInt32(charUnarmed.Value);
            TES_stuff.character.speech = Decimal.ToInt32(charSpeech.Value);
            TES_stuff.character.pickpocket = Decimal.ToInt32(charPickpocket.Value);
            TES_stuff.character.sneak = Decimal.ToInt32(charSneak.Value);
            TES_stuff.character.lockpick = Decimal.ToInt32(charLockpick.Value);
            TES_stuff.character.singing = Decimal.ToInt32(charSinging.Value);
            TES_stuff.character.drinking = Decimal.ToInt32(charDrinking.Value);
            TES_stuff.character.destruction = Decimal.ToInt32(charDestruction.Value);
            TES_stuff.character.illusion = Decimal.ToInt32(charIllusion.Value);
            TES_stuff.character.alteration = Decimal.ToInt32(charAlteration.Value);
            TES_stuff.character.restoration = Decimal.ToInt32(charRestoration.Value);
            TES_stuff.character.enchanting = Decimal.ToInt32(charEnchanting.Value);
            TES_stuff.character.conjuration = Decimal.ToInt32(charConjuration.Value);
            TES_stuff.character.smithing = Decimal.ToInt32(charSmithing.Value);
            TES_stuff.character.alchemy = Decimal.ToInt32(charAlchemy.Value);
            TES_stuff.character.traps = Decimal.ToInt32(charTraps.Value);
            TES_stuff.character.crafting = Decimal.ToInt32(charCrafting.Value);
        }

        //Speichert je nach Charakter den Charakter am richtigen Platz
        private void saveChar()
        {
            setTheme(darkTheme.checkMode());
            copyChar();

            if (currentCharacter == 1)
            {
                xmlCreator.createTESXML(Application.StartupPath + "/TES/characters/players/PlayerCharacter1.xml");
            }
            else if (currentCharacter == 2)
            {
                xmlCreator.createTESXML(Application.StartupPath + "/TES/characters/players/PlayerCharacter2.xml");
            }
            else if (currentCharacter == 3)
            {
                xmlCreator.createTESXML(Application.StartupPath + "/TES/characters/players/PlayerCharacter3.xml");
            }
            else if (currentCharacter == 4)
            {
                xmlCreator.createTESXML(Application.StartupPath + "/TES/characters/players/PlayerCharacter4.xml");
            }
            else if (currentCharacter == 5)
            {
                xmlCreator.createTESXML(Application.StartupPath + "/TES/characters/companions/CompanionCharacter1.xml");
            }
            else if (currentCharacter == 6)
            {
                xmlCreator.createTESXML(Application.StartupPath + "/TES/characters/companions/CompanionCharacter2.xml");
            }
            else if (currentCharacter == 7)
            {
                xmlCreator.createTESXML(Application.StartupPath + "/TES/characters/companions/CompanionCharacter3.xml");
            }
            else if (currentCharacter == 8)
            {
                xmlCreator.createTESXML(Application.StartupPath + "/TES/characters/enemies/EnemyCharacter1.xml");
            }
            else if (currentCharacter == 9)
            {
                xmlCreator.createTESXML(Application.StartupPath + "/TES/characters/enemies/EnemyCharacter2.xml");
            }
            else if (currentCharacter == 10)
            {
                xmlCreator.createTESXML(Application.StartupPath + "/TES/characters/enemies/EnemyCharacter3.xml");
            }
            else if (currentCharacter == 11)
            {
                xmlCreator.createTESXML(Application.StartupPath + "/TES/characters/enemies/EnemyCharacter4.xml");
            }
            else if (currentCharacter == 12)
            {
                xmlCreator.createTESXML(Application.StartupPath + "/TES/characters/enemies/EnemyCharacter5.xml");
            }
            else if (currentCharacter == 13)
            {
                xmlCreator.createTESXML(Application.StartupPath + "/TES/characters/enemies/EnemyCharacter6.xml");
            }
            else if (currentCharacter == 14)
            {
                xmlCreator.createTESXML(Application.StartupPath + "/TES/characters/enemies/EnemyCharacter7.xml");
            }
            else if (currentCharacter == 15)
            {
                xmlCreator.createTESXML(Application.StartupPath + "/TES/characters/enemies/EnemyCharacter8.xml");
            }
        }

        void insertChar()
        {
            charNameTextBox.Text = TES_stuff.character.name;
            charAgeTextBox.Text = TES_stuff.character.age;
            charHeightTextBox.Text = TES_stuff.character.height;
            charGenderTextBox.Text = TES_stuff.character.gender;
            charRaceTextBox.Text = TES_stuff.character.race;
            charStyleRichTextBox.Text = TES_stuff.character.style;

            charHpMax.Value = TES_stuff.character.hpmax;
            charHpCurr.Value = TES_stuff.character.hpcurr;
            charMagMax.Value = TES_stuff.character.magickamax;
            charMagCurr.Value = TES_stuff.character.magickacurr;
            charPhysArmor.Value = TES_stuff.character.physarmor;
            charMagArmor.Value = TES_stuff.character.magarmor;
            charParry.Value = TES_stuff.character.parry;

            charRollReq.Value = TES_stuff.character.rollreq;
            charInit.Value = TES_stuff.character.init;
            charFireRes.Value = TES_stuff.character.fireresist;
            charFrostRes.Value = TES_stuff.character.frostresist;
            charElectRes.Value = TES_stuff.character.electroresist;
            charPoisRes.Value = TES_stuff.character.poisonresist;
            charMonsterTextBox.Text = TES_stuff.character.monster;

            charPerceptBase.Value = TES_stuff.character.perception;
            charPerceptBonus.Value = TES_stuff.character.perceptionBonus;

            charEndurBase.Value = TES_stuff.character.endurance;
            charEndurBonus.Value = TES_stuff.character.enduranceBonus;

            charDexterBase.Value = TES_stuff.character.dexter;
            charDexterBonus.Value = TES_stuff.character.dexterBonus;

            charIntelliBase.Value = TES_stuff.character.intelli;
            charIntelliBonus.Value = TES_stuff.character.intelliBonus;

            charChariBase.Value = TES_stuff.character.chari;
            charChariBonus.Value = TES_stuff.character.chariBonus;

            charConcentBase.Value = TES_stuff.character.concent;
            charConcentBonus.Value = TES_stuff.character.concentBonus;

            charStrengthBase.Value = TES_stuff.character.strength;
            charStrengthBonus.Value = TES_stuff.character.strengthBonus;

            charAgilityBase.Value = TES_stuff.character.agility;
            charAgilityBonus.Value = TES_stuff.character.agilityBonus;

            charNotesRichTextBox.Text = TES_stuff.character.notes;

            charHeavWeap.Value = TES_stuff.character.heavyweap;
            charLightWeap.Value = TES_stuff.character.lightweap;
            charArchery.Value = TES_stuff.character.archery;
            charThrow.Value = TES_stuff.character.throwing;
            charUnarmed.Value = TES_stuff.character.unarmed;
            charSpeech.Value = TES_stuff.character.speech;
            charPickpocket.Value = TES_stuff.character.pickpocket;
            charSneak.Value = TES_stuff.character.sneak;
            charLockpick.Value = TES_stuff.character.lockpick;
            charSinging.Value = TES_stuff.character.singing;
            charDrinking.Value = TES_stuff.character.drinking;
            charDestruction.Value = TES_stuff.character.destruction;
            charIllusion.Value = TES_stuff.character.illusion;
            charAlteration.Value = TES_stuff.character.alteration;
            charRestoration.Value = TES_stuff.character.restoration;
            charEnchanting.Value = TES_stuff.character.enchanting;
            charConjuration.Value = TES_stuff.character.conjuration;
            charSmithing.Value = TES_stuff.character.smithing;
            charAlchemy.Value = TES_stuff.character.alchemy;
            charTraps.Value = TES_stuff.character.traps;
            charCrafting.Value = TES_stuff.character.crafting;
        }

        private void unboldAllButtons()
        {
            Font unBoldFontChars = new Font(playerChar1SelectButton.Font, FontStyle.Regular);

            playerChar1SelectButton.Font = unBoldFontChars;
            playerChar2SelectButton.Font = unBoldFontChars;
            playerChar3SelectButton.Font = unBoldFontChars;
            playerChar4SelectButton.Font = unBoldFontChars;

            Font unBoldFontComp = new Font(companionChar1Button.Font, FontStyle.Regular);

            companionChar1Button.Font = unBoldFontComp;
            companionChar2Button.Font = unBoldFontComp;
            companionChar3Button.Font = unBoldFontComp;

            Font unBoldFontEnemy = new Font(enemyChar1Button.Font, FontStyle.Regular);

            enemyChar1Button.Font = unBoldFontEnemy;
            enemyChar2Button.Font = unBoldFontEnemy;
            enemyChar3Button.Font = unBoldFontEnemy;
            enemyChar4Button.Font = unBoldFontEnemy;
            enemyChar5Button.Font = unBoldFontEnemy;
            enemyChar6Button.Font = unBoldFontEnemy;
            enemyChar7Button.Font = unBoldFontEnemy;
            enemyChar8Button.Font = unBoldFontEnemy;
        }

        private void boldButton(Button boldBtn)
        {
            Font thisButton = boldBtn.Font;

            unboldAllButtons();

            boldBtn.Font = new Font(thisButton, FontStyle.Bold);
        }

        //Liest currentCharacter aus und liest dann die relevante .xml Datei, außerdem lädt es das passende Bild.
        private void openChar()
        {
            Random rand = new Random();
            string path = Application.StartupPath + "\\TES\\characters\\pictures\\characterTemp.png";

            if (currentCharacter == 1)
            {
                xmlCreator.readTESXML(Application.StartupPath + "/TES/characters/players/PlayerCharacter1.xml");
                boldButton(playerChar1SelectButton);

                path = Application.StartupPath + "\\TES\\characters\\pictures\\character1.png";
            }
            else if (currentCharacter == 2)
            {
                xmlCreator.readTESXML(Application.StartupPath + "/TES/characters/players/PlayerCharacter2.xml");
                boldButton(playerChar2SelectButton);

                path = Application.StartupPath + "\\TES\\characters\\pictures\\character2.png";
            }
            else if (currentCharacter == 3)
            {
                xmlCreator.readTESXML(Application.StartupPath + "/TES/characters/players/PlayerCharacter3.xml");
                boldButton(playerChar3SelectButton);

                path = Application.StartupPath + "\\TES\\characters\\pictures\\character3.png";
            }
            else if (currentCharacter == 4)
            {
                xmlCreator.readTESXML(Application.StartupPath + "/TES/characters/players/PlayerCharacter4.xml");
                boldButton(playerChar4SelectButton);

                path = Application.StartupPath + "\\TES\\characters\\pictures\\character4.png";
            }
            else if (currentCharacter == 5)
            {
                xmlCreator.readTESXML(Application.StartupPath + "/TES/characters/companions/CompanionCharacter1.xml");
                boldButton(companionChar1Button);

                path = Application.StartupPath + "\\TES\\characters\\pictures\\character5.png";
            }
            else if (currentCharacter == 6)
            {
                xmlCreator.readTESXML(Application.StartupPath + "/TES/characters/companions/CompanionCharacter2.xml");
                boldButton(companionChar2Button);

                path = Application.StartupPath + "\\TES\\characters\\pictures\\character6.png";
            }
            else if (currentCharacter == 7)
            {
                xmlCreator.readTESXML(Application.StartupPath + "/TES/characters/companions/CompanionCharacter3.xml");
                boldButton(companionChar3Button);

                path = Application.StartupPath + "\\TES\\characters\\pictures\\character7.png";
            }
            else if (currentCharacter == 8)
            {
                xmlCreator.readTESXML(Application.StartupPath + "/TES/characters/enemies/EnemyCharacter1.xml");
                boldButton(enemyChar1Button);

                path = Application.StartupPath + "\\TES\\characters\\pictures\\character8.png";
            }
            else if (currentCharacter == 9)
            {
                xmlCreator.readTESXML(Application.StartupPath + "/TES/characters/enemies/EnemyCharacter2.xml");
                boldButton(enemyChar2Button);

                path = Application.StartupPath + "\\TES\\characters\\pictures\\character9.png";
            }
            else if (currentCharacter == 10)
            {
                xmlCreator.readTESXML(Application.StartupPath + "/TES/characters/enemies/EnemyCharacter3.xml");
                boldButton(enemyChar3Button);

                path = Application.StartupPath + "\\TES\\characters\\pictures\\character10.png";
            }
            else if (currentCharacter == 11)
            {
                xmlCreator.readTESXML(Application.StartupPath + "/TES/characters/enemies/EnemyCharacter4.xml");
                boldButton(enemyChar4Button);

                path = Application.StartupPath + "\\TES\\characters\\pictures\\character11.png";
            }
            else if (currentCharacter == 12)
            {
                xmlCreator.readTESXML(Application.StartupPath + "/TES/characters/enemies/EnemyCharacter5.xml");
                boldButton(enemyChar5Button);

                path = Application.StartupPath + "\\TES\\characters\\pictures\\character12.png";
            }
            else if (currentCharacter == 13)
            {
                xmlCreator.readTESXML(Application.StartupPath + "/TES/characters/enemies/EnemyCharacter6.xml");
                boldButton(enemyChar6Button);

                path = Application.StartupPath + "\\TES\\characters\\pictures\\character13.png";
            }
            else if (currentCharacter == 14)
            {
                xmlCreator.readTESXML(Application.StartupPath + "/TES/characters/enemies/EnemyCharacter7.xml");
                boldButton(enemyChar7Button);

                path = Application.StartupPath + "\\TES\\characters\\pictures\\character14.png";
            }
            else if (currentCharacter == 15)
            {
                xmlCreator.readTESXML(Application.StartupPath + "/TES/characters/enemies/EnemyCharacter8.xml");
                boldButton(enemyChar8Button);

                path = Application.StartupPath + "\\TES\\characters\\pictures\\character15.png";
            }
            string temppath = Path.GetTempPath() + "JRPGTTESCharTemp.png";
            if (File.Exists(path))
            {
                if (File.Exists(temppath))
                {
                    File.Delete(temppath);
                }
                File.Copy(path, temppath);
                charPic.ImageLocation = temppath;
            }
            else
            {
                charPic.Image = Properties.Resources.tesCharExamplePic3x4;
            }

            insertChar();
        }


        //Beim Klick wird der aktuelle Charakter gespeichert, der Charakter intern gewechselt und der Neue aufgerufen
        private void playerChar1SelectButton_Click(object sender, EventArgs e)
        {
            saveChar();

            currentCharacter = 1;

            openChar();
        }

        private void playerChar2SelectButton_Click(object sender, EventArgs e)
        {
            saveChar();

            currentCharacter = 2;

            openChar();
        }

        private void playerChar3SelectButton_Click(object sender, EventArgs e)
        {
            saveChar();

            currentCharacter = 3;

            openChar();
        }

        private void playerChar4SelectButton_Click(object sender, EventArgs e)
        {
            saveChar();

            currentCharacter = 4;

            openChar();
        }

        private void companionChar1Button_Click(object sender, EventArgs e)
        {
            saveChar();

            currentCharacter = 5;

            openChar();
        }

        private void companionChar2Button_Click(object sender, EventArgs e)
        {
            saveChar();

            currentCharacter = 6;

            openChar();
        }

        private void companionChar3Button_Click(object sender, EventArgs e)
        {
            saveChar();

            currentCharacter = 7;

            openChar();
        }

        private void enemyChar1Button_Click(object sender, EventArgs e)
        {
            saveChar();

            currentCharacter = 8;

            openChar();
        }

        private void enemyChar2Button_Click(object sender, EventArgs e)
        {
            saveChar();

            currentCharacter = 9;

            openChar();
        }

        private void enemyChar3Button_Click(object sender, EventArgs e)
        {
            saveChar();

            currentCharacter = 10;

            openChar();
        }

        private void enemyChar4Button_Click(object sender, EventArgs e)
        {
            saveChar();

            currentCharacter = 11;

            openChar();
        }

        private void enemyChar5Button_Click(object sender, EventArgs e)
        {
            saveChar();

            currentCharacter = 12;

            openChar();
        }

        private void enemyChar6Button_Click(object sender, EventArgs e)
        {
            saveChar();

            currentCharacter = 13;

            openChar();
        }

        private void enemyChar7Button_Click(object sender, EventArgs e)
        {
            saveChar();

            currentCharacter = 14;

            openChar();
        }

        private void enemyChar8Button_Click(object sender, EventArgs e)
        {
            saveChar();

            currentCharacter = 15;

            openChar();
        }


        //Berechnet den HP Max Wert und ersetzt die betreffende Zahl dann
        private void charHpMaxCalc_Click(object sender, EventArgs e)
        {
            decimal value = 60 + (charStrengthBase.Value + charEndurBase.Value) * 2;
            value = Decimal.Ceiling(value);

            charHpMax.Value = value;
        }

        //Berechnet den Magicka Max Wert und ersetzt die betreffende Zahl dann
        private void charMagCalc_Click(object sender, EventArgs e)
        {
            decimal value = (charIntelliBase.Value + charConcentBase.Value) * 5;

            charMagMax.Value = value;
        }

        //Berechnet den Parieren Wert und ersetzt die betreffende Zahl dann
        private void charParrCalc_Click(object sender, EventArgs e)
        {
            decimal value = (charAgilityBase.Value + charDexterBase.Value) / 4;
            if (value > 7)
            {
                value = 7;
            }
            value = Decimal.Round(value);

            charParry.Value = value;
        }

        //Berechnet den Wurfanforderungswert und ersetzt die betreffende Zahl dann
        private void charRollReqCalc_Click(object sender, EventArgs e)
        {
            decimal value = 20 - ((charPerceptBase.Value + charAgilityBase.Value) / 2);
            value = Decimal.Floor(value);

            charRollReq.Value = value;
        }

        //Berechnet den Initiative Wert und ersetzt die betreffende Zahl dann
        private void charInitCalc_Click(object sender, EventArgs e)
        {
            decimal value = (charPerceptBase.Value + charConcentBase.Value) / 2;

            value = Decimal.Round(value);

            charInit.Value = value;
        }

        //Ändert den oben angezeigten Namen, wenn der Text geändert wird
        private void charNameTextBox_TextChanged(object sender, EventArgs e)
        {
            switch (currentCharacter)
            {
                case 1:
                    if (charNameTextBox.Text.Trim() != "") { playerChar1SelectButton.Text = charNameTextBox.Text; }
                    else { playerChar1SelectButton.Text = Properties.strings.playerChar1Name; }
                    break;
                case 2:
                    if (charNameTextBox.Text.Trim() != "") { playerChar2SelectButton.Text = charNameTextBox.Text; }
                    else { playerChar2SelectButton.Text = Properties.strings.playerChar2Name; }
                    break;
                case 3:
                    if (charNameTextBox.Text.Trim() != "") { playerChar3SelectButton.Text = charNameTextBox.Text; }
                    else { playerChar3SelectButton.Text = Properties.strings.playerChar3Name; }
                    break;
                case 4:
                    if (charNameTextBox.Text.Trim() != "") { playerChar4SelectButton.Text = charNameTextBox.Text; }
                    else { playerChar4SelectButton.Text = Properties.strings.playerChar4Name; }
                    break;
                case 5:
                    if (charNameTextBox.Text.Trim() != "") { companionChar1Button.Text = charNameTextBox.Text; }
                    else { companionChar1Button.Text = Properties.strings.companionChar1Name; }
                    break;
                case 6:
                    if (charNameTextBox.Text.Trim() != "") { companionChar2Button.Text = charNameTextBox.Text; }
                    else { companionChar2Button.Text = Properties.strings.companionChar2Name; }
                    break;
                case 7:
                    if (charNameTextBox.Text.Trim() != "") { companionChar3Button.Text = charNameTextBox.Text; }
                    else { companionChar3Button.Text = Properties.strings.companionChar3Name; }
                    break;
                default:
                    break;
            }
        }

        //Öffnet den Bilddialog und speichert und öffnet den Charakter, dass die Änderung geladen wird.
        private void charPic_Click(object sender, EventArgs e)
        {
            saveCharPic();
            saveChar();
            openChar();
        }

        //Speichert das ausgewählte Bild, als das des aktuellen Charakters
        private void saveCharPic()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Open Image";
                openFileDialog.Filter = "PNG Files (PNG)|*.PNG";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ext = Path.GetExtension(openFileDialog.FileName);
                    if (!File.Exists(Application.StartupPath + "\\TES\\characters\\pictures\\character" + currentCharacter.ToString() + ext))
                    {
                        File.Copy(openFileDialog.FileName, Application.StartupPath + "\\TES\\characters\\pictures\\character" + currentCharacter.ToString() + ext);
                    }
                    else
                    {
                        File.Delete(Application.StartupPath + "\\TES\\characters\\pictures\\character" + currentCharacter.ToString() + ext);
                        File.Copy(openFileDialog.FileName, Application.StartupPath + "\\TES\\characters\\pictures\\character" + currentCharacter.ToString() + ext);
                    }

                }
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

        //Erlaubt es, zurück zur Mode Choice zu gehen.
        private void backButton_Click(object sender, EventArgs e)
        {
            Global_stuff.FormManager.rpgChoice.changeTheme(darkTheme.checkMode());

            Global_stuff.FormManager.rpgChoice.StartPosition = FormStartPosition.CenterScreen;
            Global_stuff.FormManager.rpgChoice.Show();
            this.Hide();
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

            shopTable.Controls.Add(new Label() { BackColor = SystemColors.Control, Width = shopTable.Width / 100 * 50, TextAlign = ContentAlignment.MiddleLeft, Font = new System.Drawing.Font("Segoe UI", 10.00F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))) }, 0, shopTable.RowCount - 1);

            //shopTable.Controls.Add(new Label() { BackColor = SystemColors.Control, Width = shopTable.Width / 100 * 25, TextAlign = ContentAlignment.MiddleLeft, Font = new System.Drawing.Font("Segoe UI", 10.00F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))) }, 1, shopTable.RowCount - 1);
            Label LB = new Label();
            LB.BackColor = SystemColors.Control;
            LB.Width = shopTable.Width / 100 * 25;
            LB.TextAlign = ContentAlignment.MiddleLeft;
            LB.Font = new System.Drawing.Font("Segoe UI", 10.00F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            LB.MouseClick += new MouseEventHandler(label2_Click);
            shopTable.Controls.Add(LB, 1, shopTable.RowCount - 1);

            shopTable.Controls.Add(new Label() { BackColor = SystemColors.Control, Width = shopTable.Width / 100 * 25, TextAlign = ContentAlignment.MiddleLeft, Font = new System.Drawing.Font("Segoe UI", 10.00F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))) }, 2, shopTable.RowCount - 1);
        }


        //Erstellt die ersten Textboxen
        private void messyFix()
        {
            for (int i = 0; i < 14; i++)
            {
                shopTable.Controls.Add(new Label() { BackColor = SystemColors.Control, Width = shopTable.Width / 100 * 50, TextAlign = ContentAlignment.MiddleLeft, Font = new System.Drawing.Font("Segoe UI", 10.00F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))) }, 0, i);

                Label LB = new Label();
                LB.BackColor = SystemColors.Control;
                LB.Width = shopTable.Width / 100 * 25;
                LB.TextAlign = ContentAlignment.MiddleLeft;
                LB.Font = new System.Drawing.Font("Segoe UI", 10.00F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                LB.MouseClick += new MouseEventHandler(label2_Click);
                shopTable.Controls.Add(LB, 1, i);

                shopTable.Controls.Add(new Label() { BackColor = SystemColors.Control, Width = shopTable.Width / 100 * 25, TextAlign = ContentAlignment.MiddleLeft, Font = new System.Drawing.Font("Segoe UI", 10.00F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))) }, 2, i);
            }

            
            for (int i = 0; i < 85; i++)
            {
                addRow();
            }
            
        }

        //Lädt die ausgewählte TXT Datei und fügt sie in den Shoptab ein.
        private void loadXML()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Load the shop TXT file.";
                openFileDialog.Filter = "TXT File|*.txt";
                openFileDialog.InitialDirectory = Application.StartupPath + "\\TES\\shop";
                openFileDialog.RestoreDirectory = true;

                Random rand = new Random();
                tempLoadPath = Path.GetTempPath() + "JRPGTShop.txt";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(tempLoadPath))
                    {
                        File.Delete(tempLoadPath);
                    }
                    loadPath = openFileDialog.FileName.ToString();
                    File.Copy(openFileDialog.FileName, tempLoadPath);

                    string[] lines = File.ReadAllLines(tempLoadPath);

                    var numLines = File.ReadLines(tempLoadPath).Count();

                    foreach (Label labello in shopTable.Controls)
                    {
                        textBoxes.Add(labello);
                    }

                    for (int i = 0; i < numLines;)
                    {
                        Label text1 = textBoxes[i];
                        text1.Text = lines[i];
                        i++;
                        Label text2 = textBoxes[i];
                        text2.Text = lines[i];
                        i++;
                        Label text3 = textBoxes[i];
                        text3.Text = lines[i];
                        i++;
                    }
                }
            }
        }

        //Lädt die XML in den Shop ein bei Klick
        private void loadShopButton_Click(object sender, EventArgs e)
        {
            loadXML();
        }

        //Wird bei einem Links- oder Rechtsklick auf das Mengenlabel aufgerufen und passt dann die Menge um +1 oder -1 an und speichert sie
        private void label2_Click(object sender, MouseEventArgs e)
        {
            //Speichert das sender label als ein neues Label
            Label lbl = sender as Label;

            //Wenn das Speichern funktioniert hat, fahre mit dem Code fort
            if (lbl != null && !isLocked)
            {
                //Wenn der Ladepfad und der Temppfad deklariert wurden, fahre fort
                if (loadPath != "" && tempLoadPath != "")
                {
                    //Konvertiert den String zu einem int, um das Rechnen zu erleichtern
                    Int32.TryParse(lbl.Text, out label1);

                    //Vergleicht die Klickarten
                    switch (e.Button)
                    {
                        //Bei Linksklick wird der Wert um 1 erhöht
                        case MouseButtons.Left:
                            label1++;
                            break;
                        //Bei Rechtsklick wird der Wert um 1 verringert
                        case MouseButtons.Right:
                            label1--;
                            break;
                        default:
                            break;
                    }
                    //Der Text des Labels wird zum int gemacht, nachdem der Int zu einem String konvertiert wurde
                    lbl.Text = label1.ToString();

                    //Speichert die Reihe des senderobjekts
                    int i = shopTable.GetCellPosition(lbl).Row;

                    //Gibt die Position in der Konsole fürs Debugging aus
                    //Console.WriteLine(shopTable.GetCellPosition(lbl));

                    //Gibt die beiden Pfade fürs Debugging aus
                    //Console.WriteLine(loadPath);
                    //Console.WriteLine(tempLoadPath);

                    //Erstellt ein neues Stringarray, bei dem jede Zeile des Dokuments ein neuer String ist
                    string[] arrLine = File.ReadAllLines(tempLoadPath);

                    //Sucht den richtigen String im Array und ersetzt ihn durch den neuen, angepassten String
                    arrLine[i * 3 + 1] = lbl.Text;

                    //Schreibt alles aus dem Array in die Textdatei beim Temppfad
                    File.WriteAllLines(tempLoadPath, arrLine);

                    //Kopiert die TXT Datei vom Temppfad zum Ladepfad
                    File.Delete(loadPath);
                    File.Copy(tempLoadPath, loadPath);
                }
            }
        }

        private void websiteLabel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://jason360x.github.io/");
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

        private void saveButton_Click(object sender, EventArgs e)
        {
            fileCompress.saveZip("tes", Application.StartupPath + " /TES");
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            fileCompress.openZip("tes");
            openChar();
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

                darkTheme.changeAllToDark(characterPanel);
                darkTheme.changeAllToDark(creditsPanel);
                darkTheme.changeAllToDark(rollPanel);

                darkTheme.changeAllToDark(shopPanel);

                foreach (Label text in shopTable.Controls.OfType<Label>())
                {
                    text.BackColor = darkFormBackground;
                    text.ForeColor = lightText;
                }
            }

            else if (mode == "Light")
            {
                this.BackColor = lightFormBackground;

                darkTheme.changeAllToLight(characterPanel);
                darkTheme.changeAllToLight(creditsPanel);
                darkTheme.changeAllToLight(rollPanel);

                darkTheme.changeAllToLight(shopPanel);

                foreach (Label text in shopTable.Controls.OfType<Label>())
                {
                    text.BackColor = lightFormBackground;
                    text.ForeColor = darkText;
                }
            }

            else if (mode == "Black")
            {
                Color blackFormBack = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));

                Color blackBack = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
                Color blackText = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
                Color blackTextLighter = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));

                this.BackColor = blackFormBack;

                darkTheme.changeAllToColor(blackTextLighter, blackText, blackBack, characterPanel);
                darkTheme.changeAllToColor(blackTextLighter, blackText, blackBack, creditsPanel);
                darkTheme.changeAllToColor(blackTextLighter, blackText, blackBack, rollPanel);

                darkTheme.changeAllToColor(blackTextLighter, blackText, blackBack, shopPanel);

                foreach (Label text in shopTable.Controls.OfType<Label>())
                {
                    text.BackColor = blackFormBack;
                    text.ForeColor = blackText;
                }
            }
        }

        private void lockButton_Click(object sender, EventArgs e)
        {
            isLocked = !isLocked;

            if (isLocked)
            {
                lockButton.Text = "🔓"; //Open Lock
            }
            else
            {
                lockButton.Text = "🔒"; //Closed Lock
            }

            Global_stuff.lockControls.lockUnlockCont(isLocked, characterPanel);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            string clearHeading = Properties.strings.clearButtonHeading;
            string clearButton = Properties.strings.clearButton;

            MessageBoxButtons yesNo = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(clearButton, clearHeading, yesNo);

            if (result == DialogResult.Yes)
            {
                TES_stuff.character.clearAll();

                charPic.Image = Properties.Resources.tesCharExamplePic3x4;

                string path = "";

                path = Application.StartupPath + "\\TES\\characters\\pictures\\character" + currentCharacter.ToString() + ".png";

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                insertChar();
            }
        }
    }
}
