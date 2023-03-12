using JasonsRPGCharacterTool.Asterius_stuff;
using JasonsRPGCharacterTool.Global_stuff;
using JasonsRPGCharacterTool.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace JasonsRPGCharacterTool
{
    public partial class asteriusPnP : Form
    {
        //Definiert einige Farben für die selbstgemachten Knöpfe
        private readonly Color highlightRed = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(141)))), ((int)(((byte)(133)))));
        private readonly Color exitRed = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(78)))), ((int)(((byte)(61)))));

        private readonly Color backNonHighlight1 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
        private readonly Color backHighlight1 = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(94)))), ((int)(((byte)(122)))));

        private readonly Color backNonHighlight2 = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(114)))), ((int)(((byte)(170)))));
        private readonly Color backHighlight2 = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));

        int currentCharacter = 1;
        int currentShip = 1;

        int label1 = 0;
        string tempLoadPath = "";
        string loadPath = "";

        //Wenn true werden alle NumericUpdowns und Buttons gesperrt, damit man sie nicht mehr versehentlich drücken kann
        public static bool isLocked = false;

        //Definiert das Form für das Wissen
        public asteriusKnowledge knowledge = new asteriusKnowledge();

        //Instanziiert den xmlCreator
        Asterius_stuff.xmlCreator_asterius createXML = new Asterius_stuff.xmlCreator_asterius();

        //Instanziiert die Klasse für das Lesen und Schreiben der ZIP Daten
        Global_stuff.compressFiles fileCompress = new Global_stuff.compressFiles();

        List<Label> textBoxes = new List<Label>();

        CultureInfo culture = Thread.CurrentThread.CurrentUICulture;

        //Instanziiert die Klasse um den Dark Mode zu erkennen
        Global_stuff.darkMode darkTheme = new Global_stuff.darkMode();

        public asteriusPnP()
        {
            InitializeComponent();

            //Zentriert das Fenster am Start
            this.StartPosition = FormStartPosition.CenterScreen;

            //Ändert den Versionsstring zur installierten Version
            versionText.Text = "v" + Application.ProductVersion;

            //Überprüft ob ein Update verfügbar ist
            checkForUpdates();

            //Initialisiert das Wissen und macht es unsichtbar
            knowledge.Show(this);
            knowledge.Visible = false;

            //Erstellt XMLs, falls sie nicht da sind.
            createXML.createXMLsAtStart();
            
            //Zeigt beim Start die Charaktere an.
            showCharacters();

            //Lokalisiert die entsprechenden Strings zu deutsch oder englisch.
            localizeStuff();

            //Fixt den Namensbug
            messyNamesBugFix();

            //Erstellt Reihen für die Tabelle im Shop
            messyFix();

            //Zeigt beim Start das 1. Schiff an.
            openShip();

            //Zeigt beim Start den 1. Charakter an.
            openChar();
            TalentsRework();

            //Ruft die pointsSpent Methode ab, damit die Zahlen direkt am Anfang angezeigt werden
            pointsSpent(null, null);

            //Ruft die calculateUpgrades Methode ab, damit die Upgradekosten am Anfang richtig angezeigt werden
            calculateUpgrades(null, null);

            //Ruft bei der weaponType ComboBox die Laser auf und lokalisiert die Namen
            changeComboBoxNames();
            weaponTypeComboBox.SelectedIndex = 0;

            //Fixt den zu großen Text im Englischen
            if (culture.Name.Contains("en-"))
            {
                companionChar1Button.Font = new System.Drawing.Font("Segoe UI", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                companionChar2Button.Font = new System.Drawing.Font("Segoe UI", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                companionChar3Button.Font = new System.Drawing.Font("Segoe UI", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                companionShip1NameText.Font = new System.Drawing.Font("Segoe UI", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                companionShip2NameText.Font = new System.Drawing.Font("Segoe UI", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                companionShip3NameText.Font = new System.Drawing.Font("Segoe UI", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                charGunDmgText.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                charGunBonusText.Font = new System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                charOutfitUpgradeText.Font = new System.Drawing.Font("Segoe UI", 10.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }

            //Setzt das Theme auf Dark bzw. Light, abhängig vom Windows Apps Theme
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
        private void localizeStuff()
        {
            saveButton.Text = Properties.strings.saveCharsToZip;
            loadButton.Text = Properties.strings.loadCharsFromZip;

            characterNameText.Text = Properties.strings.charNameText;
            charAgeText.Text = Properties.strings.charAgeText;
            charHeightText.Text = Properties.strings.charHeightText;
            charGenderText.Text = Properties.strings.charGenderText;
            charStyleText.Text = Properties.strings.charStyleText;
            idealPicSizeInfo.Text = Properties.strings.idealPiceSizeInfoText;

            charAttribText.Text = Properties.strings.charAttribText;

            charStrengthText.Text = Properties.strings.charStrengthAsterius;
            charPerceptText.Text = Properties.strings.charPerceptText;
            charEndurText.Text = Properties.strings.charEndurText;
            charDexterText.Text = Properties.strings.charDexterText;
            charIntelliText.Text = Properties.strings.charIntelliText;
            charChariText.Text = Properties.strings.charChariText;
            charLuckText.Text = Properties.strings.charLuckText;

            charHPMaxText.Text = Properties.strings.charHPMaxText;
            charHPCurText.Text = Properties.strings.charHPCurrText;
            charSleepHPRegenText.Text = Properties.strings.charSleepHPRegen;
            charRollReqText.Text = Properties.strings.charRollReqText;
            charCreditsText.Text = Properties.strings.charCreditsText;
            charIntelText.Text = Properties.strings.charIntelText;

            charGunText.Text = Properties.strings.charGun;
            charGunDmgText.Text = Properties.strings.charGunDmg;
            charGunBonusText.Text = Properties.strings.charGunBonus;
            charOutfitText.Text = Properties.strings.charOutfit;
            charOutfitResistText.Text = Properties.strings.charOutfitResist;
            charOutfitBonusText.Text = Properties.strings.charOutfitBonus;
            charOutfitUpgradeText.Text = Properties.strings.charOutfitUpgrade;

            charTalentsText.Text = Properties.strings.charTalentsText;
            charGunsText.Text = Properties.strings.charGuns;
            charSpacecombatText.Text = Properties.strings.charSpaceCombat;
            charUnarmedText.Text = Properties.strings.charUnarmedText;
            charSpeechText.Text = Properties.strings.charSpeechText;
            charLeadingText.Text = Properties.strings.charLeading;
            charTradingText.Text = Properties.strings.charTrading;
            charLyingText.Text = Properties.strings.charLying;
            charPickpocketText.Text = Properties.strings.charPickpocketText;
            charLockPickText.Text = Properties.strings.charHacking;
            charMedicineText.Text = Properties.strings.charMedicine;
            charRepairingText.Text = Properties.strings.charRepairing;
            charShipControlText.Text = Properties.strings.charShipControl;
            charInfanteryText.Text = Properties.strings.charInfantery;
            charSearchingText.Text = Properties.strings.charSearching;

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

            idealPicRatioInfoText.Text = Properties.strings.shipIdealPicRatioInfo;

            shipClassText.Text = Properties.strings.shipClass;
            reconBox.Text = Properties.strings.shipRecon;
            tactReconBox.Text = Properties.strings.shiptTactRecon;
            fighterBomberBox.Text = Properties.strings.shipFighterBomber;
            supportFighterBox.Text = Properties.strings.shipSupportFighter;
            tankfighterBox.Text = Properties.strings.shipTankfighter;

            shipNameText.Text = Properties.strings.shipName;
            shipAPText.Text = Properties.strings.shipAP;
            shipAPTurnText.Text = Properties.strings.shipAPTurn;
            shipHPMaxText.Text = Properties.strings.shipHPMax;
            shipHPCurrText.Text = Properties.strings.shipHPCurr;
            shipHitChanceText.Text = Properties.strings.shipHitChance;

            applyClassButton.Text = Properties.strings.applyClassButton;
            shipSkillsText.Text = Properties.strings.shipSkills;

            shipWeapon1Text.Text = Properties.strings.shipWeapon + " 1";
            shipWeapon2Text.Text = Properties.strings.shipWeapon + " 2";
            shipWeapon3Text.Text = Properties.strings.shipWeapon + " 3";
            shipWeapon4Text.Text = Properties.strings.shipWeapon + " 4";
            shipWeapon5Text.Text = Properties.strings.shipWeapon + " 5";
            shipWeapon6Text.Text = Properties.strings.shipWeapon + " 6";

            shipWeaponNameText1.Text = Properties.strings.shipWeaponName;
            shipWeaponNameText2.Text = Properties.strings.shipWeaponName;
            shipWeaponNameText3.Text = Properties.strings.shipWeaponName;
            shipWeaponNameText4.Text = Properties.strings.shipWeaponName;
            shipWeaponNameText5.Text = Properties.strings.shipWeaponName;
            shipWeaponNameText6.Text = Properties.strings.shipWeaponName;

            shipWeaponAPText1.Text = Properties.strings.shipWeaponAP;
            shipWeaponAPText2.Text = Properties.strings.shipWeaponAP;
            shipWeaponAPText3.Text = Properties.strings.shipWeaponAP;
            shipWeaponAPText4.Text = Properties.strings.shipWeaponAP;
            shipWeaponAPText5.Text = Properties.strings.shipWeaponAP;
            shipWeaponAPText6.Text = Properties.strings.shipWeaponAP;

            shipWeaponDamageText1.Text = Properties.strings.shipDmg;
            shipWeaponDamageText2.Text = Properties.strings.shipDmg;
            shipWeaponDamageText3.Text = Properties.strings.shipDmg;
            shipWeaponDamageText4.Text = Properties.strings.shipDmg;
            shipWeaponDamageText5.Text = Properties.strings.shipDmg;
            shipWeaponDamageText6.Text = Properties.strings.shipDmg;

            shipWeaponAmmoText1.Text = Properties.strings.shipAmmo;
            shipWeaponAmmoText2.Text = Properties.strings.shipAmmo;
            shipWeaponAmmoText3.Text = Properties.strings.shipAmmo;
            shipWeaponAmmoText4.Text = Properties.strings.shipAmmo;
            shipWeaponAmmoText5.Text = Properties.strings.shipAmmo;
            shipWeaponAmmoText6.Text = Properties.strings.shipAmmo;

            shipWeaponRangeText1.Text = Properties.strings.shipRange;
            shipWeaponRangeText2.Text = Properties.strings.shipRange;
            shipWeaponRangeText3.Text = Properties.strings.shipRange;
            shipWeaponRangeText4.Text = Properties.strings.shipRange;
            shipWeaponRangeText5.Text = Properties.strings.shipRange;
            shipWeaponRangeText6.Text = Properties.strings.shipRange;

            shipWeaponRangeLossText1.Text = Properties.strings.shipRangeLoss;
            shipWeaponRangeLossText2.Text = Properties.strings.shipRangeLoss;
            shipWeaponRangeLossText3.Text = Properties.strings.shipRangeLoss;
            shipWeaponRangeLossText4.Text = Properties.strings.shipRangeLoss;
            shipWeaponRangeLossText5.Text = Properties.strings.shipRangeLoss;
            shipWeaponRangeLossText6.Text = Properties.strings.shipRangeLoss;

            playerShip1NameText.Text = Properties.strings.playerChar1Name;
            playerShip2NameText.Text = Properties.strings.playerChar2Name;
            playerShip3NameText.Text = Properties.strings.playerChar3Name;
            playerShip4NameText.Text = Properties.strings.playerChar4Name;

            companionShip1NameText.Text = Properties.strings.companionChar1Name;
            companionShip2NameText.Text = Properties.strings.companionChar2Name;
            companionShip3NameText.Text = Properties.strings.companionChar3Name;

            enemyShip1Text.Text = Properties.strings.enemyChar1Name;
            enemyShip2Text.Text = Properties.strings.enemyChar2Name;
            enemyShip3Text.Text = Properties.strings.enemyChar3Name;
            enemyShip4Text.Text = Properties.strings.enemyChar4Name;
            enemyShip5Text.Text = Properties.strings.enemyChar5Name;
            enemyShip6Text.Text = Properties.strings.enemyChar6Name;
            enemyShip7Text.Text = Properties.strings.enemyChar7Name;
            enemyShip8Text.Text = Properties.strings.enemyChar8Name;

            shipShieldText.Text = Properties.strings.shipShield;
            shipArmorText.Text = Properties.strings.shipArmor;

            charHitChanceText.Text = Properties.strings.charHitChance;

            upgradeNameText.Text = Properties.strings.upgradeName;
            upgradeFormulaText.Text = Properties.strings.upgradeFormula;
            upgradeInputText.Text = Properties.strings.upgradeInput;
            upgradeResultText.Text = Properties.strings.upgradeResult;

            upgradeAPText.Text = Properties.strings.upgradeAP;
            upgradeHCText.Text = Properties.strings.upgradeHC;
            upgradeAPTurnText.Text = Properties.strings.upgradeAPTurn;
            upgradeShieldText.Text = Properties.strings.upgradeShield;
            upgradeArmorText.Text = Properties.strings.upgradeArmor;

            upgradeAPFormulaText.Text = Properties.strings.upgradeAPFormula;
            upgradeHCFormulaText.Text = Properties.strings.upgradeHCFormula;
            upgradeAPTurnFormulaText.Text = Properties.strings.upgradeAPTurnFormula;
            upgradeShieldFormulaText.Text = Properties.strings.upgradeShieldFormula;
            upgradeArmorFormulaText.Text = Properties.strings.upgradeArmorFormula;

            weaponCalcShieldText.Text = Properties.strings.shipShield;
            weaponCalcShieldResultText.Text = Properties.strings.shipShield;
            weaponCalcArmorText.Text = Properties.strings.shipArmor;
            weaponCalcArmorResultText.Text = Properties.strings.shipArmor;
            weaponCalcResultText.Text = Properties.strings.upgradeResult;
            weaponCalcDealtDamageText.Text = Properties.strings.dealtDamage;
            weaponCalcTypeText.Text = Properties.strings.weaponType;

            weaponCalcApplyStatsButton.Text = Properties.strings.applyStats;

            creditsUpperText.Text = Properties.strings.creditsText;
            exitText.Text = Properties.strings.exitText;

            characterButton.Text = Properties.strings.characterButtonText;
            rollButton.Text = Properties.strings.rollButtonText;
            shopButton.Text = Properties.strings.shopButton;
            shipButton.Text = Properties.strings.shipButton;
            upgradesButton.Text = Properties.strings.upgradeButton;
            creditsButton.Text = Properties.strings.creditsButtonText;

            rollHelpText.Text = Properties.strings.rollHelpText;
            customRollText.Text = Properties.strings.customRollText;
            customRollButton.Text = Properties.strings.customRollButtonText;
            minimumText.Text = Properties.strings.minimumText;
            maximumText.Text = Properties.strings.maximumText;

            shopNameText.Text = Properties.strings.shopName;
            shopAmountText.Text = Properties.strings.shopAmount;
            shopPriceText.Text = Properties.strings.shopPrice;

            calculateCostsOfUpgradesText.Text = Properties.strings.calculateCostsOfUpgrades;
            calculateDamageText.Text = Properties.strings.calculateDamage;

            charAttackTalentsText.Text = Properties.strings.charAttackTalents;
            charSocialTalentsText.Text = Properties.strings.charSocialTalents;
            charPracticalTalentsText.Text = Properties.strings.charPracticalTalents;
        }

        //Fügt der ComboBox Items hinzu, damit sie lokalisiert sind
        private void changeComboBoxNames()
        {
            weaponTypeComboBox.Items.Clear();
            weaponTypeComboBox.Items.Add(Properties.strings.Lasers);
            weaponTypeComboBox.Items.Add(Properties.strings.Missiles);
            weaponTypeComboBox.Items.Add(Properties.strings.Torpedos);
            weaponTypeComboBox.Items.Add(Properties.strings.Miniguns);
            weaponTypeComboBox.Items.Add(Properties.strings.Railgun);
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

            for (int i = 0; i < 15; i++)
            {
                currentShip++;
                openShip();
            }
            currentShip = 1;
        }

        //Blendet alle UI Elemente aus.
        private void hideAll()
        {
            rollPanel.Hide();
            creditsPanel.Hide();
            characterPanel.Hide();
            shopPanel.Hide();
            shipPanel.Hide();
            upgradePanel.Hide();
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

        //Blendet alle UI Elemente aus und die Shopansicht ein
        private void showShop()
        {
            hideAll();
            shopPanel.Show();
        }

        //Blendet alle UI Elemente aus und die Schiffe ein.
        private void showShips()
        {
            hideAll();
            shipPanel.Show();
        }

        //Blendet alle UI Elemente aus und die Schiffe ein.
        private void showUpgrades()
        {
            hideAll();
            upgradePanel.Show();
        }

        //Beschreibt was OnClick bei den Seitenknöpfen passiert.
        private void characterButton_Click(object sender, EventArgs e)
        {
            saveChar();
            saveShip();
            showCharacters();
            openChar();
        }

        private void rollButton_Click(object sender, EventArgs e)
        {
            knowledge.Visible = false;
            openKnowledgeButton.BackgroundImage = Properties.Resources.openKnowledgePassend;
            saveChar();
            saveShip();
            showRolls();
        }

        private void creditsButton_Click(object sender, EventArgs e)
        {
            knowledge.Visible = false;
            openKnowledgeButton.BackgroundImage = Properties.Resources.openKnowledgePassend;
            saveChar();
            saveShip();
            showCredits();
        }


        private void shopButton_Click(object sender, EventArgs e)
        {
            knowledge.Visible = false;
            openKnowledgeButton.BackgroundImage = Properties.Resources.openKnowledgePassend;
            saveChar();
            saveShip();
            showShop();
        }


        //OnClick des Beenden Buttons wird die App beendet und der aktuelle Charakter gespeichert.
        private void exitText_Click(object sender, EventArgs e)
        {
            saveChar();
            saveShip();
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
            rollButton.BackColor = backHighlight1;
        }

        private void rollButton_MouseLeave(object sender, EventArgs e)
        {
            rollButton.BackColor = backNonHighlight1;
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
            shopButton.BackColor = backHighlight2;
        }

        private void shopButton_MouseLeave(object sender, EventArgs e)
        {
            shopButton.BackColor = backNonHighlight2;
        }


        private void shipButton_MouseEnter(object sender, EventArgs e)
        {
            shipButton.BackColor = backHighlight2;
        }

        private void shipButton_MouseLeave(object sender, EventArgs e)
        {
            shipButton.BackColor = backNonHighlight2;
        }

        private void upgradesButton_MouseEnter(object sender, EventArgs e)
        {
            upgradesButton.BackColor = backHighlight1;
        }

        private void upgradesButton_MouseLeave(object sender, EventArgs e)
        {
            upgradesButton.BackColor = backNonHighlight1;
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

        //Speichert je nach Charakter den Charakter am richtigen Platz
        private void saveChar()
        {
            //Setzt bei jeder Speicherung das Theme, damit bei der Änderung des Windows Themes das Theme auch geändert wird
            setTheme(darkTheme.checkMode());

            copyChar();

            if (currentCharacter == 1)
            {
                createXML.createAsteriusXML(Application.StartupPath + "/Asterius/characters/players/PlayerCharacter1.xml");
            }
            else if (currentCharacter == 2)
            {
                createXML.createAsteriusXML(Application.StartupPath + "/Asterius/characters/players/PlayerCharacter2.xml");
            }
            else if (currentCharacter == 3)
            {
                createXML.createAsteriusXML(Application.StartupPath + "/Asterius/characters/players/PlayerCharacter3.xml");
            }
            else if (currentCharacter == 4)
            {
                createXML.createAsteriusXML(Application.StartupPath + "/Asterius/characters/players/PlayerCharacter4.xml");
            }
            else if (currentCharacter == 5)
            {
                createXML.createAsteriusXML(Application.StartupPath + "/Asterius/characters/companions/CompanionCharacter1.xml");
            }
            else if (currentCharacter == 6)
            {
                createXML.createAsteriusXML(Application.StartupPath + "/Asterius/characters/companions/CompanionCharacter2.xml");
            }
            else if (currentCharacter == 7)
            {
                createXML.createAsteriusXML(Application.StartupPath + "/Asterius/characters/companions/CompanionCharacter3.xml");
            }
            else if (currentCharacter == 8)
            {
                createXML.createAsteriusXML(Application.StartupPath + "/Asterius/characters/enemies/EnemyCharacter1.xml");
            }
            else if (currentCharacter == 9)
            {
                createXML.createAsteriusXML(Application.StartupPath + "/Asterius/characters/enemies/EnemyCharacter2.xml");
            }
            else if (currentCharacter == 10)
            {
                createXML.createAsteriusXML(Application.StartupPath + "/Asterius/characters/enemies/EnemyCharacter3.xml");
            }
            else if (currentCharacter == 11)
            {
                createXML.createAsteriusXML(Application.StartupPath + "/Asterius/characters/enemies/EnemyCharacter4.xml");
            }
            else if (currentCharacter == 12)
            {
                createXML.createAsteriusXML(Application.StartupPath + "/Asterius/characters/enemies/EnemyCharacter5.xml");
            }
            else if (currentCharacter == 13)
            {
                createXML.createAsteriusXML(Application.StartupPath + "/Asterius/characters/enemies/EnemyCharacter6.xml");
            }
            else if (currentCharacter == 14)
            {
                createXML.createAsteriusXML(Application.StartupPath + "/Asterius/characters/enemies/EnemyCharacter7.xml");
            }
            else if (currentCharacter == 15)
            {
                createXML.createAsteriusXML(Application.StartupPath + "/Asterius/characters/enemies/EnemyCharacter8.xml");

            }

            }
        private void copyChar()
        {
            Asterius_stuff.character.name = charNameTextBox.Text;
            Asterius_stuff.character.age = charAgeTextBox.Text;
            Asterius_stuff.character.height = charHeightTextBox.Text;
            Asterius_stuff.character.gender = charGenderTextBox.Text;
            Asterius_stuff.character.style = charStyleRichTextBox.Text;

            Asterius_stuff.character.gun = charGun.Text;
            Asterius_stuff.character.gundmg = charGunDmg.Text;
            Asterius_stuff.character.gunbonus = charGunBonus.Text;
            Asterius_stuff.character.outfit = charOutfit.Text;
            Asterius_stuff.character.outfitresist = charOutfitResist.Text;
            Asterius_stuff.character.outfitbonus = charOutfitBonus.Text;
            Asterius_stuff.character.outfitupgrade = charOutfitUpgrade.Text;

            Asterius_stuff.character.hpmax = Decimal.ToInt32(charHpMax.Value);
            Asterius_stuff.character.hpcurr = Decimal.ToInt32(charHpCurr.Value);
            Asterius_stuff.character.sleepregen = Decimal.ToInt32(charSleepRegen.Value);
            Asterius_stuff.character.rollreq = Decimal.ToInt32(charRollReq.Value);
            Asterius_stuff.character.credits = Decimal.ToInt32(charCredits.Value);
            Asterius_stuff.character.intel = Decimal.ToInt32(charIntel.Value);
            Asterius_stuff.character.hitchance = Decimal.ToInt32(charHitChance.Value);

            Asterius_stuff.character.strength = Decimal.ToInt32(charStrengthBase.Value);
            Asterius_stuff.character.perception = Decimal.ToInt32(charPerceptBase.Value);
            Asterius_stuff.character.endurance = Decimal.ToInt32(charEndurBase.Value);
            Asterius_stuff.character.dexter = Decimal.ToInt32(charDexterBase.Value);
            Asterius_stuff.character.intelli = Decimal.ToInt32(charIntelliBase.Value);
            Asterius_stuff.character.chari = Decimal.ToInt32(charChariTextBase.Value);
            Asterius_stuff.character.luck = Decimal.ToInt32(charLuckBase.Value);

            Asterius_stuff.character.notes = charNotesRichTextBox.Text;
        }
        private void insertChar()
        {
            charNameTextBox.Text = Asterius_stuff.character.name;
            charAgeTextBox.Text = Asterius_stuff.character.age;
            charHeightTextBox.Text = Asterius_stuff.character.height;
            charGenderTextBox.Text = Asterius_stuff.character.gender;
            charStyleRichTextBox.Text = Asterius_stuff.character.style;

            charGun.Text = Asterius_stuff.character.gun;
            charGunDmg.Text = Asterius_stuff.character.gundmg;
            charGunBonus.Text = Asterius_stuff.character.gunbonus;
            charOutfit.Text = Asterius_stuff.character.outfit;
            charOutfitResist.Text = Asterius_stuff.character.outfitresist;
            charOutfitBonus.Text = Asterius_stuff.character.outfitbonus;
            charOutfitUpgrade.Text = Asterius_stuff.character.outfitupgrade;

            if (Asterius_stuff.character.hpmax >= 1) { charHpMax.Value = Asterius_stuff.character.hpmax; }
            charHpCurr.Value = Asterius_stuff.character.hpcurr;
            if (Asterius_stuff.character.sleepregen >= 1) { charSleepRegen.Value = Asterius_stuff.character.sleepregen; }
            if (Asterius_stuff.character.rollreq >= 1) { charRollReq.Value = Asterius_stuff.character.rollreq; }
            charCredits.Value = Asterius_stuff.character.credits;
            charIntel.Value = Asterius_stuff.character.intel;
            charHitChance.Value = Asterius_stuff.character.hitchance;

            if (Asterius_stuff.character.strength >= 1) { charStrengthBase.Value = Asterius_stuff.character.strength; }
            if (Asterius_stuff.character.perception >= 1) { charPerceptBase.Value = Asterius_stuff.character.perception; }
            if (Asterius_stuff.character.endurance >= 1) { charEndurBase.Value = Asterius_stuff.character.endurance; }
            if (Asterius_stuff.character.dexter >= 1) { charDexterBase.Value = Asterius_stuff.character.dexter; }
            if (Asterius_stuff.character.intelli >= 1) { charIntelliBase.Value = Asterius_stuff.character.intelli; }
            if (Asterius_stuff.character.chari >= 1) { charChariTextBase.Value = Asterius_stuff.character.chari; }
            if (Asterius_stuff.character.luck >= 1) { charLuckBase.Value = Asterius_stuff.character.luck; }

            charNotesRichTextBox.Text = Asterius_stuff.character.notes;
        }

        private void unboldAllCharButtons()
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

        private void boldCharButton(Button boldBtn)
        {
            Font thisButton = boldBtn.Font;

            unboldAllCharButtons();

            boldBtn.Font = new Font(thisButton, FontStyle.Bold);
        }

        //Liest currentCharacter aus und liest dann die relevante .xml Datei, außerdem lädt es das passende Bild.
        private void openChar()
        {
            Random rand = new Random();
            string path = Application.StartupPath + "\\Asterius\\characters\\pictures\\characterTemp.png";

            if (currentCharacter == 1)
            {
                createXML.readAsteriusXML(Application.StartupPath + "/Asterius/characters/players/PlayerCharacter1.xml");
                boldCharButton(playerChar1SelectButton);

                path = Application.StartupPath + "\\Asterius\\characters\\pictures\\character1.png";
            }
            else if (currentCharacter == 2)
            {
                createXML.readAsteriusXML(Application.StartupPath + "/Asterius/characters/players/PlayerCharacter2.xml");
                boldCharButton(playerChar2SelectButton);

                path = Application.StartupPath + "\\Asterius\\characters\\pictures\\character2.png";
            }
            else if (currentCharacter == 3)
            {
                createXML.readAsteriusXML(Application.StartupPath + "/Asterius/characters/players/PlayerCharacter3.xml");
                boldCharButton(playerChar3SelectButton);

                path = Application.StartupPath + "\\Asterius\\characters\\pictures\\character3.png";
            }
            else if (currentCharacter == 4)
            {
                createXML.readAsteriusXML(Application.StartupPath + "/Asterius/characters/players/PlayerCharacter4.xml");
                boldCharButton(playerChar4SelectButton);

                path = Application.StartupPath + "\\Asterius\\characters\\pictures\\character4.png";
            }
            else if (currentCharacter == 5)
            {
                createXML.readAsteriusXML(Application.StartupPath + "/Asterius/characters/companions/CompanionCharacter1.xml");
                boldCharButton(companionChar1Button);

                path = Application.StartupPath + "\\Asterius\\characters\\pictures\\character5.png";
            }
            else if (currentCharacter == 6)
            {
                createXML.readAsteriusXML(Application.StartupPath + "/Asterius/characters/companions/CompanionCharacter2.xml");
                boldCharButton(companionChar2Button);

                path = Application.StartupPath + "\\Asterius\\characters\\pictures\\character6.png";
            }
            else if (currentCharacter == 7)
            {
                createXML.readAsteriusXML(Application.StartupPath + "/Asterius/characters/companions/CompanionCharacter3.xml");
                boldCharButton(companionChar3Button);

                path = Application.StartupPath + "\\Asterius\\characters\\pictures\\character7.png";
            }
            else if (currentCharacter == 8)
            {
                createXML.readAsteriusXML(Application.StartupPath + "/Asterius/characters/enemies/EnemyCharacter1.xml");
                boldCharButton(enemyChar1Button);

                path = Application.StartupPath + "\\Asterius\\characters\\pictures\\character8.png";
            }
            else if (currentCharacter == 9)
            {
                createXML.readAsteriusXML(Application.StartupPath + "/Asterius/characters/enemies/EnemyCharacter2.xml");
                boldCharButton(enemyChar2Button);

                path = Application.StartupPath + "\\Asterius\\characters\\pictures\\character9.png";
            }
            else if (currentCharacter == 10)
            {
                createXML.readAsteriusXML(Application.StartupPath + "/Asterius/characters/enemies/EnemyCharacter3.xml");
                boldCharButton(enemyChar3Button);

                path = Application.StartupPath + "\\Asterius\\characters\\pictures\\character10.png";
            }
            else if (currentCharacter == 11)
            {
                createXML.readAsteriusXML(Application.StartupPath + "/Asterius/characters/enemies/EnemyCharacter4.xml");
                boldCharButton(enemyChar4Button);

                path = Application.StartupPath + "\\Asterius\\characters\\pictures\\character11.png";
            }
            else if (currentCharacter == 12)
            {
                createXML.readAsteriusXML(Application.StartupPath + "/Asterius/characters/enemies/EnemyCharacter5.xml");
                boldCharButton(enemyChar5Button);

                path = Application.StartupPath + "\\Asterius\\characters\\pictures\\character12.png";
            }
            else if (currentCharacter == 13)
            {
                createXML.readAsteriusXML(Application.StartupPath + "/Asterius/characters/enemies/EnemyCharacter6.xml");
                boldCharButton(enemyChar6Button);

                path = Application.StartupPath + "\\Asterius\\characters\\pictures\\character13.png";
            }
            else if (currentCharacter == 14)
            {
                createXML.readAsteriusXML(Application.StartupPath + "/Asterius/characters/enemies/EnemyCharacter7.xml");
                boldCharButton(enemyChar7Button);

                path = Application.StartupPath + "\\Asterius\\characters\\pictures\\character14.png";
            }
            else if (currentCharacter == 15)
            {
                createXML.readAsteriusXML(Application.StartupPath + "/Asterius/characters/enemies/EnemyCharacter8.xml");
                boldCharButton(enemyChar8Button);

                path = Application.StartupPath + "\\Asterius\\characters\\pictures\\character15.png";
            }

            insertChar();
            TalentsRework();

            string temppath = Path.GetTempPath() + "JRPGTAsteriusCharTemp.png";
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
                charPic.Image = Properties.Resources.asteriusCharExamplePic3x4;
            }

            knowledge.localizeAndShowPoints();
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
            decimal value = 40 + (charEndurBase.Value * 2) + charStrengthBase.Value;
            value = Decimal.Ceiling(value);

            charHpMax.Value = value;
        }

        //Berechnet den Wurfanforderungswert und ersetzt die betreffende Zahl dann
        private void charRollReqCalc_Click(object sender, EventArgs e)
        {
            decimal value = (decimal.Divide(55, 40) * (charDexterBase.Value + charPerceptBase.Value)) + 5; //decimal.Divide wird verwendet, weil das sonst ein falsches Ergebnis ausgibt
            value = Decimal.Ceiling(value);

            charRollReq.Value = value;
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
            using (OpenFileDialog charFileDialog = new OpenFileDialog())
            {
                charFileDialog.Title = "Open Image";
                charFileDialog.Filter = "PNG Files (PNG)|*.PNG";

                if (charFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ext = Path.GetExtension(charFileDialog.FileName);
                    if (!File.Exists(Application.StartupPath + "\\Asterius\\characters\\pictures\\character" + currentCharacter.ToString() + ext))
                    {
                        File.Copy(charFileDialog.FileName, Application.StartupPath + "\\Asterius\\characters\\pictures\\character" + currentCharacter.ToString() + ext);
                    }
                    else
                    {
                        File.Delete(Application.StartupPath + "\\Asterius\\characters\\pictures\\character" + currentCharacter.ToString() + ext);
                        File.Copy(charFileDialog.FileName, Application.StartupPath + "\\Asterius\\characters\\pictures\\character" + currentCharacter.ToString() + ext);
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
            saveChar();
            saveShip();

            knowledge.Hide();
            openKnowledgeButton.BackgroundImage = Properties.Resources.openKnowledgePassend;

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
                openFileDialog.InitialDirectory = Application.StartupPath + "\\Asterius\\shop";
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

        //Öffnet OnClick die Website auf GitHub
        private void websiteLabel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://jason360x.github.io/");
        }

        //Berechnet die ausgegebenen Punkte und zeigt sie an
        private void pointsSpent(object sender, EventArgs e)
        {
            decimal x = charStrengthBase.Value + charPerceptBase.Value + charEndurBase.Value + charDexterBase.Value + charIntelliBase.Value + charChariTextBase.Value + charLuckBase.Value;

            if (culture.Name.Contains("de-"))
            {
                charPointsSpentInfoText.Text = "Punkte ausgegeben: " + x;
            }
            else
            {
                charPointsSpentInfoText.Text = "Points spent: " + x;
            }

            //Um die Talente zu updaten, sonst müsste ich ne neue Methode machen und beides kombinieren und so gehts auch.
            TalentsRework();
            Asterius_stuff.character.knowledgePointsRefresh(Decimal.ToInt32(charIntelliBase.Value));
            knowledge.localizeAndShowPoints();
        }
        

        //Berechnet die HP Regeneration beim Schlafen
        private void sleepRegenButton_Click(object sender, EventArgs e)
        {
            decimal value = Asterius_stuff.character.strength + Asterius_stuff.character.endurance;
            value = Decimal.Floor(value);

            Asterius_stuff.character.sleepregen = Decimal.ToInt32(value);
            charSleepRegen.Value = Asterius_stuff.character.sleepregen;
        }

        private void shipButton_Click(object sender, EventArgs e)
        {
            knowledge.Visible = false;
            openKnowledgeButton.BackgroundImage = Properties.Resources.openKnowledgePassend;
            saveChar();
            saveShip();
            showShips();
            openShip();
        }

        private void shipPic_Click(object sender, EventArgs e)
        {
            saveShipPic();
            saveShip();
            openShip();
        }

        //Speichert das eingegebene Bild am richtigen Ort
        private void saveShipPic()
        {
            using (OpenFileDialog charFileDialog = new OpenFileDialog())
            {
                charFileDialog.Title = "Open Image";
                charFileDialog.Filter = "PNG Files (PNG)|*.PNG";

                if (charFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string ext = Path.GetExtension(charFileDialog.FileName);
                    if (!File.Exists(Application.StartupPath + "\\Asterius\\ships\\pictures\\ship" + currentShip.ToString() + ext))
                    {
                        File.Copy(charFileDialog.FileName, Application.StartupPath + "\\Asterius\\ships\\pictures\\ship" + currentShip.ToString() + ext);
                    }
                    else
                    {
                        File.Delete(Application.StartupPath + "\\Asterius\\ships\\pictures\\ship" + currentShip.ToString() + ext);
                        File.Copy(charFileDialog.FileName, Application.StartupPath + "\\Asterius\\ships\\pictures\\ship" + currentShip.ToString() + ext);
                    }
                }
            }
        }

        private void unboldAllShipButtons()
        {
            Font unBoldFontPlayers = new Font(playerShip1NameText.Font, FontStyle.Regular);

            playerShip1NameText.Font = unBoldFontPlayers;
            playerShip2NameText.Font = unBoldFontPlayers;
            playerShip3NameText.Font = unBoldFontPlayers;
            playerShip4NameText.Font = unBoldFontPlayers;

            Font unBoldFontComp = new Font(companionShip1NameText.Font, FontStyle.Regular);

            companionShip1NameText.Font = unBoldFontComp;
            companionShip2NameText.Font = unBoldFontComp;
            companionShip3NameText.Font = unBoldFontComp;

            Font unBoldFontEnemy = new Font(enemyShip1Text.Font, FontStyle.Regular);

            enemyShip1Text.Font = unBoldFontEnemy;
            enemyShip2Text.Font = unBoldFontEnemy;
            enemyShip3Text.Font = unBoldFontEnemy;
            enemyShip4Text.Font = unBoldFontEnemy;
            enemyShip5Text.Font = unBoldFontEnemy;
            enemyShip6Text.Font = unBoldFontEnemy;
            enemyShip7Text.Font = unBoldFontEnemy;
            enemyShip8Text.Font = unBoldFontEnemy;
        }

        private void boldShipButton(Button boldBtn)
        {
            Font thisButton = boldBtn.Font;

            unboldAllShipButtons();

            boldBtn.Font = new Font(thisButton, FontStyle.Bold);
        }

        //Ruft je nach aktuellem Schiff die richtige .xml Datei auf
        private void openShip()
        {
            Random rand = new Random();
            string path = Application.StartupPath + "\\Asterius\\ships\\pictures\\shipTemp.png";

            if (currentShip == 1)
            {
                createXML.readAsteriusShip(Application.StartupPath + "/Asterius/ships/players/PlayerShip1.xml");
                boldShipButton(playerShip1NameText);

                path = Application.StartupPath + "\\Asterius\\ships\\pictures\\ship1.png";
            }
            else if (currentShip == 2)
            {
                createXML.readAsteriusShip(Application.StartupPath + "/Asterius/ships/players/PlayerShip2.xml");
                boldShipButton(playerShip2NameText);

                path = Application.StartupPath + "\\Asterius\\ships\\pictures\\ship2.png";
            }
            else if (currentShip == 3)
            {
                createXML.readAsteriusShip(Application.StartupPath + "/Asterius/ships/players/PlayerShip3.xml");
                boldShipButton(playerShip3NameText);

                path = Application.StartupPath + "\\Asterius\\ships\\pictures\\ship3.png";
            }
            else if (currentShip == 4)
            {
                createXML.readAsteriusShip(Application.StartupPath + "/Asterius/ships/players/PlayerShip4.xml");
                boldShipButton(playerShip4NameText);

                path = Application.StartupPath + "\\Asterius\\ships\\pictures\\ship4.png";
            }
            else if (currentShip == 5)
            {
                createXML.readAsteriusShip(Application.StartupPath + "/Asterius/ships/companions/CompanionShip1.xml");
                boldShipButton(companionShip1NameText);

                path = Application.StartupPath + "\\Asterius\\ships\\pictures\\ship5.png";
            }
            else if (currentShip == 6)
            {
                createXML.readAsteriusShip(Application.StartupPath + "/Asterius/ships/companions/CompanionShip2.xml");
                boldShipButton(companionShip2NameText);

                path = Application.StartupPath + "\\Asterius\\ships\\pictures\\ship6.png";
            }
            else if (currentShip == 7)
            {
                createXML.readAsteriusShip(Application.StartupPath + "/Asterius/ships/companions/CompanionShip3.xml");
                boldShipButton(companionShip3NameText);

                path = Application.StartupPath + "\\Asterius\\ships\\pictures\\ship7.png";
            }
            else if (currentShip == 8)
            {
                createXML.readAsteriusShip(Application.StartupPath + "/Asterius/ships/enemies/EnemyShip1.xml");
                boldShipButton(enemyShip1Text);

                path = Application.StartupPath + "\\Asterius\\ships\\pictures\\ship8.png";
            }
            else if (currentShip == 9)
            {
                createXML.readAsteriusShip(Application.StartupPath + "/Asterius/ships/enemies/EnemyShip2.xml");
                boldShipButton(enemyShip2Text);

                path = Application.StartupPath + "\\Asterius\\ships\\pictures\\ship9.png";
            }
            else if (currentShip == 10)
            {
                createXML.readAsteriusShip(Application.StartupPath + "/Asterius/ships/enemies/EnemyShip3.xml");
                boldShipButton(enemyShip3Text);

                path = Application.StartupPath + "\\Asterius\\ships\\pictures\\ship10.png";
            }
            else if (currentShip == 11)
            {
                createXML.readAsteriusShip(Application.StartupPath + "/Asterius/ships/enemies/EnemyShip4.xml");
                boldShipButton(enemyShip4Text);

                path = Application.StartupPath + "\\Asterius\\ships\\pictures\\ship11.png";
            }
            else if (currentShip == 12)
            {
                createXML.readAsteriusShip(Application.StartupPath + "/Asterius/ships/enemies/EnemyShip5.xml");
                boldShipButton(enemyShip5Text);

                path = Application.StartupPath + "\\Asterius\\ships\\pictures\\ship12.png";
            }
            else if (currentShip == 13)
            {
                createXML.readAsteriusShip(Application.StartupPath + "/Asterius/ships/enemies/EnemyShip6.xml");
                boldShipButton(enemyShip6Text);

                path = Application.StartupPath + "\\Asterius\\ships\\pictures\\ship13.png";
            }
            else if (currentShip == 14)
            {
                createXML.readAsteriusShip(Application.StartupPath + "/Asterius/ships/enemies/EnemyShip7.xml");
                boldShipButton(enemyShip7Text);

                path = Application.StartupPath + "\\Asterius\\ships\\pictures\\ship14.png";
            }
            else if (currentShip == 15)
            {
                createXML.readAsteriusShip(Application.StartupPath + "/Asterius/ships/enemies/EnemyShip8.xml");
                boldShipButton(enemyShip8Text);

                path = Application.StartupPath + "\\Asterius\\ships\\pictures\\ship15.png";
            }

            string temppath = Path.GetTempPath() + "JRPGTShipTemp.png";
            if (File.Exists(path))
            {
                if (File.Exists(temppath))
                {
                    File.Delete(temppath);
                }
                File.Copy(path, temppath);
                shipPic.ImageLocation = temppath;
            }
            else
            {
                shipPic.Image = Properties.Resources.asteriusShipExamplePic4x3;
            }

            insertShip();
        }

        void insertShip()
        {
            switch (currentShipType())
            {
                case "recon":
                    turnOffAllShipTypes();
                    reconBox.Checked = true;
                    break;
                case "tactRecon":
                    turnOffAllShipTypes();
                    tactReconBox.Checked = true;
                    break;
                case "fighterBomber":
                    turnOffAllShipTypes();
                    fighterBomberBox.Checked = true;
                    break;
                case "supportFighter":
                    turnOffAllShipTypes();
                    supportFighterBox.Checked = true;
                    break;
                case "tankFighter":
                    turnOffAllShipTypes();
                    tankfighterBox.Checked = true;
                    break;
                default:
                    turnOffAllShipTypes();
                    break;
            }

            shipName.Text = Asterius_stuff.ship.shipname;
            shipAP.Value = Asterius_stuff.ship.shipAP;
            shipAPTurn.Value = Asterius_stuff.ship.shipAPTurn;
            shipHPMax.Value = Asterius_stuff.ship.shipHPMax;
            shipHPCurr.Value = Asterius_stuff.ship.shipHPCurr;
            shipHitChance.Value = Asterius_stuff.ship.shipHitChance;
            shipShield.Value = Asterius_stuff.ship.shield;
            shipArmor.Value = Asterius_stuff.ship.armor;
            shipSkillsTextBox.Text = Asterius_stuff.ship.shipSkills;

            shipWeaponName1.Text = Asterius_stuff.ship.weaponName1;
            shipWeaponAP1.Text = Asterius_stuff.ship.weaponAP1;
            shipWeaponDamage1.Text = Asterius_stuff.ship.weaponDmg1;
            shipWeaponAmmo1.Text = Asterius_stuff.ship.weaponAmmo1;
            shipWeaponRange1.Text = Asterius_stuff.ship.weaponRange1;
            shipWeaponRangeLoss1.Text = Asterius_stuff.ship.weaponLoss1;

            shipWeaponName2.Text = Asterius_stuff.ship.weaponName2;
            shipWeaponAP2.Text = Asterius_stuff.ship.weaponAP2;
            shipWeaponDamage2.Text = Asterius_stuff.ship.weaponDmg2;
            shipWeaponAmmo2.Text = Asterius_stuff.ship.weaponAmmo2;
            shipWeaponRange2.Text = Asterius_stuff.ship.weaponRange2;
            shipWeaponRangeLoss2.Text = Asterius_stuff.ship.weaponLoss2;

            shipWeaponName3.Text = Asterius_stuff.ship.weaponName3;
            shipWeaponAP3.Text = Asterius_stuff.ship.weaponAP3;
            shipWeaponDamage3.Text = Asterius_stuff.ship.weaponDmg3;
            shipWeaponAmmo3.Text = Asterius_stuff.ship.weaponAmmo3;
            shipWeaponRange3.Text = Asterius_stuff.ship.weaponRange3;
            shipWeaponRangeLoss3.Text = Asterius_stuff.ship.weaponLoss3;

            shipWeaponName4.Text = Asterius_stuff.ship.weaponName4;
            shipWeaponAP4.Text = Asterius_stuff.ship.weaponAP4;
            shipWeaponDamage4.Text = Asterius_stuff.ship.weaponDmg4;
            shipWeaponAmmo4.Text = Asterius_stuff.ship.weaponAmmo4;
            shipWeaponRange4.Text = Asterius_stuff.ship.weaponRange4;
            shipWeaponRangeLoss4.Text = Asterius_stuff.ship.weaponLoss4;

            shipWeaponName5.Text = Asterius_stuff.ship.weaponName5;
            shipWeaponAP5.Text = Asterius_stuff.ship.weaponAP5;
            shipWeaponDamage5.Text = Asterius_stuff.ship.weaponDmg5;
            shipWeaponAmmo5.Text = Asterius_stuff.ship.weaponAmmo5;
            shipWeaponRange5.Text = Asterius_stuff.ship.weaponRange5;
            shipWeaponRangeLoss5.Text = Asterius_stuff.ship.weaponLoss5;

            shipWeaponName6.Text = Asterius_stuff.ship.weaponName6;
            shipWeaponAP6.Text = Asterius_stuff.ship.weaponAP6;
            shipWeaponDamage6.Text = Asterius_stuff.ship.weaponDmg6;
            shipWeaponAmmo6.Text = Asterius_stuff.ship.weaponAmmo6;
            shipWeaponRange6.Text = Asterius_stuff.ship.weaponRange6;
            shipWeaponRangeLoss6.Text = Asterius_stuff.ship.weaponLoss6;
        }

        void wipeShipType()
        {
            Asterius_stuff.ship.recon = "0";
            Asterius_stuff.ship.tactRecon = "0";
            Asterius_stuff.ship.fighterBomber = "0";
            Asterius_stuff.ship.supportFighter = "0";
            Asterius_stuff.ship.tankFighter = "0";
        }
        void copyShip()
        {
            if (reconBox.Checked)
            {
                wipeShipType();
                Asterius_stuff.ship.recon = "1";
            }
            else if (tactReconBox.Checked)
            {
                wipeShipType();
                Asterius_stuff.ship.tactRecon = "1";
            }
            else if (fighterBomberBox.Checked)
            {
                wipeShipType();
                Asterius_stuff.ship.fighterBomber = "1";
            }
            else if (supportFighterBox.Checked)
            {
                wipeShipType();
                Asterius_stuff.ship.supportFighter = "1";
            }
            else if (tankfighterBox.Checked)
            {
                wipeShipType();
                Asterius_stuff.ship.tankFighter = "1";
            }

            Asterius_stuff.ship.shipname = shipName.Text;
            Asterius_stuff.ship.shipAP = Decimal.ToInt32(shipAP.Value);
            Asterius_stuff.ship.shipAPTurn = Decimal.ToInt32(shipAPTurn.Value);
            Asterius_stuff.ship.shipHPMax = Decimal.ToInt32(shipHPMax.Value);
            Asterius_stuff.ship.shipHPCurr = Decimal.ToInt32(shipHPCurr.Value);
            Asterius_stuff.ship.shipHitChance = Decimal.ToInt32(shipHitChance.Value);
            Asterius_stuff.ship.shield = Decimal.ToInt32(shipShield.Value);
            Asterius_stuff.ship.armor = Decimal.ToInt32(shipArmor.Value);
            Asterius_stuff.ship.shipSkills = shipSkillsTextBox.Text;

            Asterius_stuff.ship.weaponName1 = shipWeaponName1.Text;
            Asterius_stuff.ship.weaponAP1 = shipWeaponAP1.Text;
            Asterius_stuff.ship.weaponDmg1 = shipWeaponDamage1.Text;
            Asterius_stuff.ship.weaponAmmo1 = shipWeaponAmmo1.Text;
            Asterius_stuff.ship.weaponRange1 = shipWeaponRange1.Text;
            Asterius_stuff.ship.weaponLoss1 = shipWeaponRangeLoss1.Text;

            Asterius_stuff.ship.weaponName2 = shipWeaponName2.Text;
            Asterius_stuff.ship.weaponAP2 = shipWeaponAP2.Text;
            Asterius_stuff.ship.weaponDmg2 = shipWeaponDamage2.Text;
            Asterius_stuff.ship.weaponAmmo2 = shipWeaponAmmo2.Text;
            Asterius_stuff.ship.weaponRange2 = shipWeaponRange2.Text;
            Asterius_stuff.ship.weaponLoss2 = shipWeaponRangeLoss2.Text;

            Asterius_stuff.ship.weaponName3 = shipWeaponName3.Text;
            Asterius_stuff.ship.weaponAP3 = shipWeaponAP3.Text;
            Asterius_stuff.ship.weaponDmg3 = shipWeaponDamage3.Text;
            Asterius_stuff.ship.weaponAmmo3 = shipWeaponAmmo3.Text;
            Asterius_stuff.ship.weaponRange3 = shipWeaponRange3.Text;
            Asterius_stuff.ship.weaponLoss3 = shipWeaponRangeLoss3.Text;

            Asterius_stuff.ship.weaponName4 = shipWeaponName4.Text;
            Asterius_stuff.ship.weaponAP4 = shipWeaponAP4.Text;
            Asterius_stuff.ship.weaponDmg4 = shipWeaponDamage4.Text;
            Asterius_stuff.ship.weaponAmmo4 = shipWeaponAmmo4.Text;
            Asterius_stuff.ship.weaponRange4 = shipWeaponRange4.Text;
            Asterius_stuff.ship.weaponLoss4 = shipWeaponRangeLoss4.Text;

            Asterius_stuff.ship.weaponName5 = shipWeaponName5.Text;
            Asterius_stuff.ship.weaponAP5 = shipWeaponAP5.Text;
            Asterius_stuff.ship.weaponDmg5 = shipWeaponDamage5.Text;
            Asterius_stuff.ship.weaponAmmo5 = shipWeaponAmmo5.Text;
            Asterius_stuff.ship.weaponRange5 = shipWeaponRange5.Text;
            Asterius_stuff.ship.weaponLoss5 = shipWeaponRangeLoss5.Text;

            Asterius_stuff.ship.weaponName6 = shipWeaponName6.Text;
            Asterius_stuff.ship.weaponAP6 = shipWeaponAP6.Text;
            Asterius_stuff.ship.weaponDmg6 = shipWeaponDamage6.Text;
            Asterius_stuff.ship.weaponAmmo6 = shipWeaponAmmo6.Text;
            Asterius_stuff.ship.weaponRange6 = shipWeaponRange6.Text;
            Asterius_stuff.ship.weaponLoss6 = shipWeaponRangeLoss6.Text;
        }

        void turnOffAllShipTypes()
        {
            reconBox.Checked = false;
            tactReconBox.Checked = false;
            fighterBomberBox.Checked = false;
            supportFighterBox.Checked = false;
            tankfighterBox.Checked = false;
        }
        string currentShipType()
        {
            if (Asterius_stuff.ship.recon == "1")
            {
                return "recon";
            }
            else if (Asterius_stuff.ship.tactRecon == "1")
            {
                return "tactRecon";
            }
            else if (Asterius_stuff.ship.fighterBomber == "1")
            {
                return "fighterBomber";
            }
            else if (Asterius_stuff.ship.supportFighter == "1")
            {
                return "supportFighter";
            }
            else if (Asterius_stuff.ship.tankFighter == "1")
            {
                return "tankFighter";
            }
            else { return "none"; }
        }
        //Speichert je nach Schiff das Schiff am richtigen Platz
        private void saveShip()
        {
            string recon, tactRecon, fighterBomber, supportFighter, tankFighter;

            if (reconBox.Checked == true)
            {
                recon = "1";
            }
            else
            {
                recon = "0";
            }
            if (tactReconBox.Checked == true)
            {
                tactRecon = "1";
            }
            else
            {
                tactRecon = "0";
            }
            if (fighterBomberBox.Checked == true)
            {
                fighterBomber = "1";
            }
            else
            {
                fighterBomber = "0";
            }
            if (supportFighterBox.Checked == true)
            {
                supportFighter = "1";
            }
            else
            {
                supportFighter = "0";
            }
            if (tankfighterBox.Checked == true)
            {
                tankFighter = "1";
            }
            else
            {
                tankFighter = "0";
            }

            Asterius_stuff.ship.recon = recon;
            Asterius_stuff.ship.tactRecon = tactRecon;
            Asterius_stuff.ship.fighterBomber = fighterBomber;
            Asterius_stuff.ship.supportFighter = supportFighter;
            Asterius_stuff.ship.tankFighter = tankFighter;

            copyShip();

            switch (currentShip)
            {
                case 1:
                    createXML.createAsteriusShipXML(Application.StartupPath + "/Asterius/ships/players/PlayerShip1.xml");
                    break;
                case 2:
                    createXML.createAsteriusShipXML(Application.StartupPath + "/Asterius/ships/players/PlayerShip2.xml");
                    break;
                case 3:
                    createXML.createAsteriusShipXML(Application.StartupPath + "/Asterius/ships/players/PlayerShip3.xml");
                    break;
                case 4:
                    createXML.createAsteriusShipXML(Application.StartupPath + "/Asterius/ships/players/PlayerShip4.xml");
                    break;
                case 5:
                    createXML.createAsteriusShipXML(Application.StartupPath + "/Asterius/ships/companions/CompanionShip1.xml");
                    break;
                case 6:
                    createXML.createAsteriusShipXML(Application.StartupPath + "/Asterius/ships/companions/CompanionShip2.xml");
                    break;
                case 7:
                    createXML.createAsteriusShipXML(Application.StartupPath + "/Asterius/ships/companions/CompanionShip3.xml");
                    break;
                case 8:
                    createXML.createAsteriusShipXML(Application.StartupPath + "/Asterius/ships/enemies/EnemyShip1.xml");
                    break;
                case 9:
                    createXML.createAsteriusShipXML(Application.StartupPath + "/Asterius/ships/enemies/EnemyShip2.xml");
                    break;
                case 10:
                    createXML.createAsteriusShipXML(Application.StartupPath + "/Asterius/ships/enemies/EnemyShip3.xml");
                    break;
                case 11:
                    createXML.createAsteriusShipXML(Application.StartupPath + "/Asterius/ships/enemies/EnemyShip4.xml");
                    break;
                case 12:
                    createXML.createAsteriusShipXML(Application.StartupPath + "/Asterius/ships/enemies/EnemyShip5.xml");
                    break;
                case 13:
                    createXML.createAsteriusShipXML(Application.StartupPath + "/Asterius/ships/enemies/EnemyShip6.xml");
                    break;
                case 14:
                    createXML.createAsteriusShipXML(Application.StartupPath + "/Asterius/ships/enemies/EnemyShip7.xml");
                    break;
                case 15:
                    createXML.createAsteriusShipXML(Application.StartupPath + "/Asterius/ships/enemies/EnemyShip8.xml");
                    break;
                default:
                    break;
            }
        }

        //Speichert das Schiff und öffnet das nächste
        private void playerShip1NameText_Click(object sender, EventArgs e)
        {
            saveShip();

            currentShip = 1;

            openShip();
        }

        private void playerShip2NameText_Click(object sender, EventArgs e)
        {
            saveShip();

            currentShip = 2;

            openShip();
        }

        private void playerShip3NameText_Click(object sender, EventArgs e)
        {
            saveShip();

            currentShip = 3;

            openShip();
        }

        private void playerShip4NameText_Click(object sender, EventArgs e)
        {
            saveShip();

            currentShip = 4;

            openShip();
        }

        private void companionShip1NameText_Click(object sender, EventArgs e)
        {
            saveShip();

            currentShip = 5;

            openShip();
        }

        private void companionShip2NameText_Click(object sender, EventArgs e)
        {
            saveShip();

            currentShip = 6;

            openShip();
        }

        private void companionShip3NameText_Click(object sender, EventArgs e)
        {
            saveShip();

            currentShip = 7;

            openShip();
        }

        private void enemyShip1Text_Click(object sender, EventArgs e)
        {
            saveShip();

            currentShip = 8;

            openShip();
        }

        private void enemyShip2Text_Click(object sender, EventArgs e)
        {
            saveShip();

            currentShip = 9;

            openShip();
        }

        private void enemyShip3Text_Click(object sender, EventArgs e)
        {
            saveShip();

            currentShip = 10;

            openShip();
        }

        private void enemyShip4Text_Click(object sender, EventArgs e)
        {
            saveShip();

            currentShip = 11;

            openShip();
        }

        private void enemyShip5Text_Click(object sender, EventArgs e)
        {
            saveShip();

            currentShip = 12;

            openShip();
        }

        private void enemyShip6Text_Click(object sender, EventArgs e)
        {
            saveShip();

            currentShip = 13;

            openShip();
        }

        private void enemyShip7Text_Click(object sender, EventArgs e)
        {
            saveShip();

            currentShip = 14;

            openShip();
        }

        private void enemyShip8Text_Click(object sender, EventArgs e)
        {
            saveShip();

            currentShip = 15;

            openShip();
        }

        //Sorgt dafür, dass immer nur ein Haken existiert
        private void reconBox_Click(object sender, EventArgs e)
        {
            tactReconBox.Checked = false;
            fighterBomberBox.Checked = false;
            supportFighterBox.Checked = false;
            tankfighterBox.Checked = false;
        }

        private void tactReconBox_Click(object sender, EventArgs e)
        {
            reconBox.Checked = false;
            fighterBomberBox.Checked = false;
            supportFighterBox.Checked = false;
            tankfighterBox.Checked = false;
        }

        private void fighterBomberBox_Click(object sender, EventArgs e)
        {
            reconBox.Checked = false;
            tactReconBox.Checked = false;
            supportFighterBox.Checked = false;
            tankfighterBox.Checked = false;
        }

        private void supportFighterBox_Click(object sender, EventArgs e)
        {
            reconBox.Checked = false;
            tactReconBox.Checked = false;
            fighterBomberBox.Checked = false;
            tankfighterBox.Checked = false;
        }

        private void tankfighterBox_Click(object sender, EventArgs e)
        {
            reconBox.Checked = false;
            tactReconBox.Checked = false;
            fighterBomberBox.Checked = false;
            supportFighterBox.Checked = false;
        }

        //Wendet onClick die Klassendaten an
        private void applyClassButton_Click(object sender, EventArgs e)
        {
            if (reconBox.Checked == true)
            {
                shipHPMax.Value = 100;
                shipAP.Value = 120;
                shipAPTurn.Value = 15;
                shipArmor.Value = 10;
                shipShield.Value = 0;
                shipHitChance.Value = -20;
            }

            else if (tactReconBox.Checked == true)
            {
                shipHPMax.Value = 100;
                shipAP.Value = 100;
                shipAPTurn.Value = 30;
                shipArmor.Value = 10;
                shipShield.Value = 5;
                shipHitChance.Value = -10;
            }

            else if (fighterBomberBox.Checked == true)
            {
                shipHPMax.Value = 120;
                shipAP.Value = 100;
                shipAPTurn.Value = 20;
                shipArmor.Value = 10;
                shipShield.Value = 10;
                shipHitChance.Value = 0;
            }

            else if (supportFighterBox.Checked == true)
            {
                shipHPMax.Value = 140;
                shipAP.Value = 80;
                shipAPTurn.Value = 20;
                shipArmor.Value = 20;
                shipShield.Value = 20;
                shipHitChance.Value = 10;
            }

            else if (tankfighterBox.Checked == true)
            {
                shipHPMax.Value = 160;
                shipAP.Value = 80;
                shipAPTurn.Value = 40;
                shipArmor.Value = 30;
                shipShield.Value = 30;
                shipHitChance.Value = 20;
            }
        }

        //Sorgt dafür, dass der Name oben aktualisiert wird
        private void shipName_TextChanged(object sender, EventArgs e)
        {
            switch (currentShip)
            {
                case 1:
                    if (shipName.Text.Trim() != "") { playerShip1NameText.Text = shipName.Text; }
                    else { playerShip1NameText.Text = Properties.strings.playerChar1Name; }
                    break;
                case 2:
                    if (shipName.Text.Trim() != "") { playerShip2NameText.Text = shipName.Text; }
                    else { playerShip2NameText.Text = Properties.strings.playerChar2Name; }
                    break;
                case 3:
                    if (shipName.Text.Trim() != "") { playerShip3NameText.Text = shipName.Text; }
                    else { playerShip3NameText.Text = Properties.strings.playerChar3Name; }
                    break;
                case 4:
                    if (shipName.Text.Trim() != "") { playerShip4NameText.Text = shipName.Text; }
                    else { playerShip4NameText.Text = Properties.strings.playerChar4Name; }
                    break;
                case 5:
                    if (shipName.Text.Trim() != "") { companionShip1NameText.Text = shipName.Text; }
                    else { companionShip1NameText.Text = Properties.strings.companionChar1Name; }
                    break;
                case 6:
                    if (shipName.Text.Trim() != "") { companionShip2NameText.Text = shipName.Text; }
                    else { companionShip2NameText.Text = Properties.strings.companionChar2Name; }
                    break;
                case 7:
                    if (shipName.Text.Trim() != "") { companionShip3NameText.Text = shipName.Text; }
                    else { companionShip3NameText.Text = Properties.strings.companionChar3Name; }
                    break;
                default:
                    break;
            }
        }

        //Öffnet das Upgrades Fenster
        private void upgradesButton_Click(object sender, EventArgs e)
        {
            knowledge.Visible = false;
            openKnowledgeButton.BackgroundImage = Properties.Resources.openKnowledgePassend;
            saveChar();
            saveShip();
            showUpgrades();
        }

        //Berechnet die Kosten der Upgrades
        private void calculateUpgrades(object sender, EventArgs e)
        {
            decimal ap = upgradeAPUpDown.Value * 10;
            upgradeAPResult.Text = ap.ToString();

            decimal hc = (-20 - upgradeHCUpDown.Value) * -150;
            upgradeHCResult.Text = hc.ToString();

            decimal apturn = (100 - upgradeAPTurnUpDown.Value) * 40;
            upgradeAPTurnResult.Text = apturn.ToString();

            decimal shield = upgradeShieldUpDown.Value * 50;
            upgradeShieldResult.Text = shield.ToString();

            decimal armor = upgradeArmorUpDown.Value * 50;
            upgradeArmorResult.Text = armor.ToString();
        }

        //Berechnet den verursachten Schaden aufgrund des Inputs
        private void calculateDamage(object sender, EventArgs e)
        {
            decimal dmg = weaponCalcDealtDamage.Value;
            decimal shield = weaponCalcShield.Value;
            decimal armor = weaponCalcArmor.Value;
            decimal HP = weaponCalcHP.Value;

            //Lasers
            if (weaponTypeComboBox.SelectedIndex == 0)
            {
                decimal dealtShield = dmg / 2;
                shield = shield - dealtShield;

                if (shield < 0)
                {
                    decimal dealtArmor = -shield * 4;
                    dealtArmor = Math.Ceiling(dealtArmor);
                    shield = 0;

                    armor = armor - dealtArmor;

                    if (armor < 0)
                    {
                        decimal dealtHP = -armor / 2;
                        dealtHP = Math.Ceiling(dealtHP);
                        armor = 0;

                        HP = HP - dealtHP;
                    }
                }
            }

            //Missiles
            else if (weaponTypeComboBox.SelectedIndex == 1)
            {
                decimal dealtShield = dmg;
                shield = shield - dealtShield;

                if (shield < 0)
                {
                    decimal dealtArmor = -shield / 2;
                    dealtArmor = Math.Ceiling(dealtArmor);
                    shield = 0;

                    armor = armor - dealtArmor;

                    if (armor < 0)
                    {
                        decimal dealtHP = -armor * 2;
                        dealtHP = Math.Ceiling(dealtHP);
                        armor = 0;

                        HP = HP - dealtHP;
                    }
                }
            }

            //Torpedos
            else if (weaponTypeComboBox.SelectedIndex == 2)
            {
                decimal dealtShield = dmg * 2;
                shield = shield - dealtShield;

                if (shield < 0)
                {
                    decimal dealtArmor = -shield / 2;
                    dealtArmor = Math.Ceiling(dealtArmor);
                    shield = 0;

                    armor = armor - dealtArmor;

                    if (armor < 0)
                    {
                        decimal dealtHP = -armor;
                        dealtHP = Math.Ceiling(dealtHP);
                        armor = 0;

                        HP = HP - dealtHP;
                    }
                }
            }

            //Miniguns
            else if (weaponTypeComboBox.SelectedIndex == 3)
            {
                decimal dealtShield = dmg * 4;
                shield = shield - dealtShield;

                if (shield < 0)
                {
                    decimal dealtArmor = -shield / 16;
                    dealtArmor = Math.Ceiling(dealtArmor);
                    shield = 0;

                    armor = armor - dealtArmor;

                    if (armor < 0)
                    {
                        decimal dealtHP = -armor * 4;
                        dealtHP = Math.Ceiling(dealtHP);
                        armor = 0;

                        HP = HP - dealtHP;
                    }
                }
            }

            //Railguns
            else if (weaponTypeComboBox.SelectedIndex == 4)
            {
                decimal damageToDealToShield = shield * 2;
                decimal damageToDealToArmor = armor / 2;
                damageToDealToArmor = Math.Floor(damageToDealToArmor);

                decimal dealtDamage = dmg - damageToDealToShield;

                if (dealtDamage > 0)
                {
                    shield = 0;

                    dealtDamage = dealtDamage - damageToDealToArmor;

                    if (dealtDamage > 0)
                    {
                        armor = 0;

                        HP = HP - dealtDamage;
                    }

                    else if (dealtDamage < 0)
                    {
                        armor = -dealtDamage * 2;
                    }

                    else if (dealtDamage == 0)
                    {
                        armor = 0;
                    }
                }

                else if (dealtDamage < 0)
                {
                    shield = -dealtDamage / 2;
                    shield = Math.Floor(shield);
                }

                else if (dealtDamage == 0)
                {
                    shield = 0;
                }

                /*
                decimal dealtShield = dmg / 2;
                shield = shield - dealtShield;

                if (shield < 0)
                {
                    decimal dealtArmor = -shield * 4;
                    dealtArmor = Math.Ceiling(dealtArmor);
                    shield = 0;

                    armor = armor - dealtArmor;

                    if (armor < 0)
                    {
                        decimal dealtHP = -armor;
                        dealtHP = Math.Ceiling(dealtHP);
                        armor = 0;

                        HP = HP - dealtHP;
                    }
                }
                */
            }

            weaponCalcShieldResult.Text = shield.ToString();
            weaponCalcArmorResult.Text = armor.ToString();
            weaponCalcHPResult.Text = HP.ToString();
        }

        //Übernimmt onClick die berechneten Stats zum Rechnen
        private void applyStats(object sender, EventArgs e)
        {
            Int32.TryParse(weaponCalcShieldResult.Text, out int a);
            weaponCalcShield.Value = a;

            Int32.TryParse(weaponCalcArmorResult.Text, out int b);
            weaponCalcArmor.Value = b;

            Int32.TryParse(weaponCalcHPResult.Text, out int c);
            weaponCalcHP.Value = c;
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

        //Erforderliche Änderungen durch das Rework der Talente
        private void TalentsRework()
        {
            charAttackTalentsText.Text = Properties.strings.charAttackTalents + " (" + (Asterius_stuff.character.guns + Asterius_stuff.character.unarmed + Asterius_stuff.character.spacecombat + Asterius_stuff.character.shipcontrol) + ")";
            charSocialTalentsText.Text = Properties.strings.charSocialTalents + " (" + (Asterius_stuff.character.speech + Asterius_stuff.character.lying + Asterius_stuff.character.leading + Asterius_stuff.character.trading) + ")";
            charPracticalTalentsText.Text = Properties.strings.charPracticalTalents + " (" + (Asterius_stuff.character.pickpocket + Asterius_stuff.character.lockpick + Asterius_stuff.character.medicine + Asterius_stuff.character.repairing + Asterius_stuff.character.infantery + Asterius_stuff.character.searching) + ")";

            int y = Asterius_stuff.character.guns + Asterius_stuff.character.unarmed + Asterius_stuff.character.spacecombat + Asterius_stuff.character.shipcontrol + Asterius_stuff.character.speech + Asterius_stuff.character.lying + Asterius_stuff.character.leading +
                    Asterius_stuff.character.trading + Asterius_stuff.character.pickpocket + Asterius_stuff.character.lockpick + Asterius_stuff.character.medicine + Asterius_stuff.character.repairing + Asterius_stuff.character.infantery + Asterius_stuff.character.searching;

            if (culture.Name.Contains("de-"))
            {
                charTalentsPointSpentInfo.Text = "Punkte ausgegeben: " + y;
            }
            else
            {
                charTalentsPointSpentInfo.Text = "Points spent: " + y;
            }

            //Debug
            //Console.WriteLine("Strength: " + charStrengthBase.Value + ", Dexter: " + charDexterBase.Value + ", Bonus: " + charTalGunsBonus);

            decimal charGunVal = 10 + (decimal)85/60 * (charStrengthBase.Value * 2 + charDexterBase.Value) + Asterius_stuff.character.guns;
            charGunVal = Math.Ceiling(charGunVal);
            charGunsText.Text = Properties.strings.charGuns + " " + charGunVal.ToString() + "%";
            if (Asterius_stuff.character.guns != 0)
            {
                if (Asterius_stuff.character.guns < 0)
                {
                    charGunsText.Text += " (" + Asterius_stuff.character.guns + ")";
                }
                if (Asterius_stuff.character.guns > 0)
                {
                    charGunsText.Text += " (+" + Asterius_stuff.character.guns + ")";
                }
            }

            decimal charUnarmedVal = 10 + (decimal)85/60 * (charStrengthBase.Value * 2 + charEndurBase.Value) + Asterius_stuff.character.unarmed;
            charUnarmedVal = Math.Ceiling(charUnarmedVal);
            charUnarmedText.Text = Properties.strings.charUnarmedText + " " + charUnarmedVal.ToString() + "%";
            if (Asterius_stuff.character.unarmed != 0)
            {
                if (Asterius_stuff.character.unarmed < 0)
                {
                    charUnarmedText.Text += " (" + Asterius_stuff.character.unarmed + ")";
                }
                if (Asterius_stuff.character.unarmed > 0)
                {
                    charUnarmedText.Text += " (+" + Asterius_stuff.character.unarmed + ")";
                }
            }

            decimal charSpacecombat = 10 + (decimal)85 / 60 * (charIntelliBase.Value * 2 + charDexterBase.Value) + Asterius_stuff.character.spacecombat;
            charSpacecombat = Math.Ceiling(charSpacecombat);
            charSpacecombatText.Text = Properties.strings.charSpaceCombat + " " + charSpacecombat.ToString() + "%";
            if (Asterius_stuff.character.spacecombat != 0)
            {
                if (Asterius_stuff.character.spacecombat < 0)
                {
                    charSpacecombatText.Text += " (" + Asterius_stuff.character.spacecombat + ")";
                }
                if (Asterius_stuff.character.spacecombat > 0)
                {
                    charSpacecombatText.Text += " (+" + Asterius_stuff.character.spacecombat + ")";
                }
            }

            decimal charShipControl = 10 + (decimal)85 / 60 * (charIntelliBase.Value * 2 + charPerceptBase.Value) + Asterius_stuff.character.shipcontrol;
            charShipControl = Math.Ceiling(charShipControl);
            charShipControlText.Text = Properties.strings.charShipControl + " " + charShipControl.ToString() + "%";
            if (Asterius_stuff.character.shipcontrol != 0)
            {
                if (Asterius_stuff.character.shipcontrol < 0)
                {
                    charShipControlText.Text += " (" + Asterius_stuff.character.shipcontrol + ")";
                }
                if (Asterius_stuff.character.shipcontrol > 0)
                {
                    charShipControlText.Text += " (+" + Asterius_stuff.character.shipcontrol + ")";
                }
            }

            decimal charSpeech = 5 + (decimal)90 / 60 * (charChariTextBase.Value * 2 + charPerceptBase.Value) + Asterius_stuff.character.speech;
            charSpeech = Math.Ceiling(charSpeech);
            charSpeechText.Text = Properties.strings.charSpeechText + " " + charSpeech.ToString() + "%";
            if (Asterius_stuff.character.speech != 0)
            {
                if (Asterius_stuff.character.speech < 0)
                {
                    charSpeechText.Text += " (" + Asterius_stuff.character.speech + ")";
                }
                if (Asterius_stuff.character.speech > 0)
                {
                    charSpeechText.Text += " (+" + Asterius_stuff.character.speech + ")";
                }
            }

            decimal charLying = 5 + (decimal)90 / 60 * (charChariTextBase.Value * 2 + charIntelliBase.Value) + Asterius_stuff.character.lying;
            charLying = Math.Ceiling(charLying);
            charLyingText.Text = Properties.strings.charLying + " " + charLying.ToString() + "%";
            if (Asterius_stuff.character.lying != 0)
            {
                if (Asterius_stuff.character.lying < 0)
                {
                    charLyingText.Text += " (" + Asterius_stuff.character.lying + ")";
                }
                if (Asterius_stuff.character.lying > 0)
                {
                    charLyingText.Text += " (+" + Asterius_stuff.character.lying + ")";
                }
            }

            decimal charLeading = 5 + (decimal)90 / 60 * (charChariTextBase.Value * 3) + Asterius_stuff.character.leading;
            charLeading = Math.Ceiling(charLeading);
            charLeadingText.Text = Properties.strings.charLeading + " " + charLeading.ToString() + "%";
            if (Asterius_stuff.character.leading != 0)
            {
                if (Asterius_stuff.character.leading < 0)
                {
                    charLeadingText.Text += " (" + Asterius_stuff.character.leading + ")";
                }
                if (Asterius_stuff.character.leading > 0)
                {
                    charLeadingText.Text += " (+" + Asterius_stuff.character.leading + ")";
                }
            }

            decimal charTrading = 5 + (decimal)90 / 60 * (charChariTextBase.Value * 2 + charLuckBase.Value) + Asterius_stuff.character.trading;
            charTrading = Math.Ceiling(charTrading);
            charTradingText.Text = Properties.strings.charTrading + " " + charTrading.ToString() + "%";
            if (Asterius_stuff.character.trading != 0)
            {
                if (Asterius_stuff.character.trading < 0)
                {
                    charTradingText.Text += " (" + Asterius_stuff.character.trading + ")";
                }
                if (Asterius_stuff.character.trading > 0)
                {
                    charTradingText.Text += " (+" + Asterius_stuff.character.trading + ")";
                }
            }

            decimal charPickpocket = (decimal)95 / 60 * (charDexterBase.Value + charPerceptBase.Value + charLuckBase.Value) + Asterius_stuff.character.pickpocket;
            charPickpocket = Math.Ceiling(charPickpocket);
            charPickpocketText.Text = Properties.strings.charPickpocketText + " " + charPickpocket.ToString() + "%";
            if (Asterius_stuff.character.pickpocket != 0)
            {
                if (Asterius_stuff.character.pickpocket < 0)
                {
                    charPickpocketText.Text += " (" + Asterius_stuff.character.pickpocket + ")";
                }
                if (Asterius_stuff.character.pickpocket > 0)
                {
                    charPickpocketText.Text += " (+" + Asterius_stuff.character.pickpocket + ")";
                }
            }

            decimal charLockpick = (decimal)95 / 60 * (charDexterBase.Value * 2 + charPerceptBase.Value) + Asterius_stuff.character.lockpick;
            charLockpick = Math.Ceiling(charLockpick);
            charLockPickText.Text = Properties.strings.charLockpickText + " " + charLockpick.ToString() + "%";
            if (Asterius_stuff.character.lockpick != 0)
            {
                if (Asterius_stuff.character.lockpick < 0)
                {
                    charLockPickText.Text += " (" + Asterius_stuff.character.lockpick + ")";
                }
                if (Asterius_stuff.character.lockpick > 0)
                {
                    charLockPickText.Text += " (+" + Asterius_stuff.character.lockpick + ")";
                }
            }

            decimal charMedicine = (decimal)95 / 60 * (charIntelliBase.Value * 2 + charDexterBase.Value) + Asterius_stuff.character.medicine;
            charMedicine = Math.Ceiling(charMedicine);
            charMedicineText.Text = Properties.strings.charMedicine + " " + charMedicine.ToString() + "%";
            if (Asterius_stuff.character.medicine != 0)
            {
                if (Asterius_stuff.character.medicine < 0)
                {
                    charMedicineText.Text += " (" + Asterius_stuff.character.medicine + ")";
                }
                if (Asterius_stuff.character.medicine > 0)
                {
                    charMedicineText.Text += " (+" + Asterius_stuff.character.medicine + ")";
                }
            }

            decimal charRepair = (decimal)95 / 60 * (charDexterBase.Value * 2 + charIntelliBase.Value) + Asterius_stuff.character.repairing;
            charRepair = Math.Ceiling(charRepair);
            charRepairingText.Text = Properties.strings.charRepairing + " " + charRepair.ToString() + "%";
            if (Asterius_stuff.character.repairing != 0)
            {
                if (Asterius_stuff.character.repairing < 0)
                {
                    charRepairingText.Text += " (" + Asterius_stuff.character.repairing + ")";
                }
                if (Asterius_stuff.character.repairing > 0)
                {
                    charRepairingText.Text += " (+" + Asterius_stuff.character.repairing + ")";
                }
            }

            decimal charInfantery = (decimal)95 / 60 * (charStrengthBase.Value + charEndurBase.Value + charPerceptBase.Value) + Asterius_stuff.character.infantery;
            charInfantery = Math.Ceiling(charInfantery);
            charInfanteryText.Text = Properties.strings.charInfantery + " " + charInfantery.ToString() + "%";
            if (Asterius_stuff.character.infantery != 0)
            {
                if (Asterius_stuff.character.infantery < 0)
                {
                    charInfanteryText.Text += " (" + Asterius_stuff.character.infantery + ")";
                }
                if (Asterius_stuff.character.infantery > 0)
                {
                    charInfanteryText.Text += " (+" + Asterius_stuff.character.infantery + ")";
                }
            }

            decimal charSearch = (decimal)95 / 60 * (charPerceptBase.Value * 2 + charIntelliBase.Value) + Asterius_stuff.character.searching;
            charSearch = Math.Ceiling(charSearch);
            charSearchingText.Text = Properties.strings.charSearching + " " + charSearch.ToString() + "%";
            if (Asterius_stuff.character.searching != 0)
            {
                if (Asterius_stuff.character.searching < 0)
                {
                    charSearchingText.Text += " (" + Asterius_stuff.character.searching + ")";
                }
                if (Asterius_stuff.character.searching > 0)
                {
                    charSearchingText.Text += " (+" + Asterius_stuff.character.searching + ")";
                }
            }
        }

        private void TalentsBonus(object sender, MouseEventArgs e)
        {
            //Speichert das sender label als ein neues Label
            Label lbl = sender as Label;

            //Wenn das Speichern funktioniert hat, fahre mit dem Code fort
            if (lbl != null && !isLocked)
            {
                switch (lbl.Name)
                {
                    case "charGunsText":
                        //Vergleicht die Klickarten
                        switch (e.Button)
                        {
                            //Bei Linksklick wird der Wert um 1 erhöht
                            case MouseButtons.Left:
                                Asterius_stuff.character.guns++;
                                break;
                            //Bei Rechtsklick wird der Wert um 1 verringert
                            case MouseButtons.Right:
                                Asterius_stuff.character.guns--;
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charUnarmedText":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                Asterius_stuff.character.unarmed++;
                                break;
                            case MouseButtons.Right:
                                Asterius_stuff.character.unarmed--;
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charSpacecombatText":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                Asterius_stuff.character.spacecombat++;
                                break;
                            case MouseButtons.Right:
                                Asterius_stuff.character.spacecombat--;
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charShipControlText":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                Asterius_stuff.character.shipcontrol++;
                                break;
                            case MouseButtons.Right:
                                Asterius_stuff.character.shipcontrol--;
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charSpeechText":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                Asterius_stuff.character.speech++;
                                break;
                            case MouseButtons.Right:
                                Asterius_stuff.character.speech--;
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charLyingText":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                Asterius_stuff.character.lying++;
                                break;
                            case MouseButtons.Right:
                                Asterius_stuff.character.lying--;
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charLeadingText":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                Asterius_stuff.character.leading++;
                                break;
                            case MouseButtons.Right:
                                Asterius_stuff.character.leading--;
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charTradingText":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                Asterius_stuff.character.trading++;
                                break;
                            case MouseButtons.Right:
                                Asterius_stuff.character.trading--;
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charPickpocketText":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                Asterius_stuff.character.pickpocket++;
                                break;
                            case MouseButtons.Right:
                                Asterius_stuff.character.pickpocket--;
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charLockPickText":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                Asterius_stuff.character.lockpick++;
                                break;
                            case MouseButtons.Right:
                                Asterius_stuff.character.lockpick--;
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charMedicineText":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                Asterius_stuff.character.medicine++;
                                break;
                            case MouseButtons.Right:
                                Asterius_stuff.character.medicine--;
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charRepairingText":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                Asterius_stuff.character.repairing++;
                                break;
                            case MouseButtons.Right:
                                Asterius_stuff.character.repairing--;
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charInfanteryText":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                Asterius_stuff.character.infantery++;
                                break;
                            case MouseButtons.Right:
                                Asterius_stuff.character.infantery--;
                                break;
                            default:
                                break;
                        }
                        break;

                    case "charSearchingText":
                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                Asterius_stuff.character.searching++;
                                break;
                            case MouseButtons.Right:
                                Asterius_stuff.character.searching--;
                                break;
                            default:
                                break;
                        }
                        break;

                    default:
                        break;
                }
                saveChar();
            }

            TalentsRework();
        }

        //Öffnet das Form für Wissen und Sprachen, außerdem passt es den Knopf an
        private void OpenKnowledgeButton_Click(object sender, EventArgs e)
        {
            Asterius_stuff.character.knowledgePointsRefresh(Decimal.ToInt32(charIntelliBase.Value));
            knowledge.localizeAndShowPoints();

            if (knowledge.Visible == false)
            {
                //knowledge.increaseOpacity();
                knowledge.Visible = true;
                openKnowledgeButton.BackgroundImage = Properties.Resources.openKnowledgePassend2;
            }
            else if (knowledge.Visible == true)
            {
                Thread.Sleep(1000);
                //knowledge.lowerOpacity();
                knowledge.Visible = false;
                openKnowledgeButton.BackgroundImage = Properties.Resources.openKnowledgePassend;
            }
        }

        //Öffnet beim Klick den Speicherdialog
        private void saveButton_Click(object sender, EventArgs e)
        {
            fileCompress.saveZip("asterius", Application.StartupPath + "/Asterius");
        }

        //Öffnet beim Klick den Ladedialog
        private void loadButton_Click(object sender, EventArgs e)
        {
            fileCompress.openZip("asterius");
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
                darkTheme.changeAllToDark(upgradePanel);

                darkTheme.changeAllToDark(shipPanel);
                darkTheme.changeAllToDark(shipWeapon1Panel);
                darkTheme.changeAllToDark(shipWeapon2Panel);
                darkTheme.changeAllToDark(shipWeapon3Panel);
                darkTheme.changeAllToDark(shipWeapon4Panel);
                darkTheme.changeAllToDark(shipWeapon5Panel);
                darkTheme.changeAllToDark(shipWeapon6Panel);

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
                darkTheme.changeAllToLight(upgradePanel);

                darkTheme.changeAllToLight(shipPanel);
                darkTheme.changeAllToLight(shipWeapon1Panel);
                darkTheme.changeAllToLight(shipWeapon2Panel);
                darkTheme.changeAllToLight(shipWeapon3Panel);
                darkTheme.changeAllToLight(shipWeapon4Panel);
                darkTheme.changeAllToLight(shipWeapon5Panel);
                darkTheme.changeAllToLight(shipWeapon6Panel);

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
                darkTheme.changeAllToColor(blackTextLighter, blackText, blackBack, upgradePanel);

                darkTheme.changeAllToColor(blackTextLighter, blackText, blackBack, shipPanel);
                darkTheme.changeAllToColor(blackTextLighter, blackText, blackBack, shipWeapon1Panel);
                darkTheme.changeAllToColor(blackTextLighter, blackText, blackBack, shipWeapon2Panel);
                darkTheme.changeAllToColor(blackTextLighter, blackText, blackBack, shipWeapon3Panel);
                darkTheme.changeAllToColor(blackTextLighter, blackText, blackBack, shipWeapon4Panel);
                darkTheme.changeAllToColor(blackTextLighter, blackText, blackBack, shipWeapon5Panel);
                darkTheme.changeAllToColor(blackTextLighter, blackText, blackBack, shipWeapon6Panel);

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

            lockControls.lockUnlockCont(isLocked, characterPanel);
            lockControls.lockUnlockCont(isLocked, shipPanel);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            string clearHeading = Properties.strings.clearButtonHeading;
            string clearButton = Properties.strings.clearButton;

            MessageBoxButtons yesNo = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(clearButton, clearHeading, yesNo);

            if (result == DialogResult.Yes)
            {
                if (characterPanel.Visible == true)
                {
                    Asterius_stuff.character.clearAll();

                    charPic.Image = Properties.Resources.asteriusCharExamplePic3x4;

                    string path = "";

                    path = Application.StartupPath + "\\Asterius\\characters\\pictures\\character" + currentCharacter.ToString() + ".png";

                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                    insertChar();
                    pointsSpent(null, null);
                }
                else if (shipPanel.Visible == true)
                {
                    Asterius_stuff.ship.clearAll();

                    shipPic.Image = Resources.asteriusShipExamplePic4x3;

                    string path = "";

                    path = Application.StartupPath + "\\Asterius\\ships\\pictures\\ship" + currentShip.ToString() + ".png";

                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                    insertShip();
                }
            }
        }
    }
}
