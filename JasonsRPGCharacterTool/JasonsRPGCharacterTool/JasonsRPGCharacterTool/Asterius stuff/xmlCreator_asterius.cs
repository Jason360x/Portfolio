using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace JasonsRPGCharacterTool.Asterius_stuff
{
    class xmlCreator_asterius
    {
        //Erstellt ein Settingsobjekt, mit dem man die Einstellungen des XMLWriters verändern kann
        XmlWriterSettings xmlSettings = new XmlWriterSettings();

        //Nimmt den Pfad auf und speichert alle Charakterdaten dann am richtigen Platz in einer .xml Datei
        public void createAsteriusXML(string path)
        {
            xmlSettings.Indent = true;
            XmlWriter writer = XmlWriter.Create(path, xmlSettings);

            writer.WriteStartDocument();
            writer.WriteComment("DO NOT CHANGE ANYTHING IN HERE - THE RPGTOOL TAKES CARE OF THAT!");
            writer.WriteStartElement("Character");

            writer.WriteElementString("Name", character.name);
            writer.WriteElementString("Age", character.age);
            writer.WriteElementString("Height", character.height);
            writer.WriteElementString("Gender", character.gender);
            writer.WriteElementString("Style", character.style);

            writer.WriteElementString("HPMax", character.hpmax.ToString());
            writer.WriteElementString("HPCurrent", character.hpcurr.ToString());
            writer.WriteElementString("SleepRegen", character.sleepregen.ToString());
            writer.WriteElementString("RollRequirement", character.rollreq.ToString());
            writer.WriteElementString("Credits", character.credits.ToString());
            writer.WriteElementString("Intel", character.intel.ToString());

            writer.WriteElementString("Gun", character.gun);
            writer.WriteElementString("GunDmg", character.gundmg);
            writer.WriteElementString("GunBonus", character.gunbonus);
            writer.WriteElementString("Outfit", character.outfit);
            writer.WriteElementString("OutfitResist", character.outfitresist);
            writer.WriteElementString("OutfitBonus", character.outfitbonus);
            writer.WriteElementString("OutfitUpgrade", character.outfitupgrade);

            writer.WriteElementString("Perception", character.perception.ToString());
            writer.WriteElementString("Endurance", character.endurance.ToString());
            writer.WriteElementString("Dexterity", character.dexter.ToString());
            writer.WriteElementString("Intelligence", character.intelli.ToString());
            writer.WriteElementString("Charisma", character.chari.ToString());
            writer.WriteElementString("Luck", character.luck.ToString());
            writer.WriteElementString("Strength", character.strength.ToString());

            writer.WriteElementString("Notes", character.notes);

            writer.WriteElementString("Guns", character.guns.ToString());
            writer.WriteElementString("SpaceCombat", character.spacecombat.ToString());
            writer.WriteElementString("Unarmed", character.unarmed.ToString());
            writer.WriteElementString("Speech", character.speech.ToString());
            writer.WriteElementString("Leading", character.leading.ToString());
            writer.WriteElementString("Trading", character.trading.ToString());
            writer.WriteElementString("Lying", character.lying.ToString());
            writer.WriteElementString("Pickpocket", character.pickpocket.ToString());
            writer.WriteElementString("Lockpicking", character.lockpick.ToString());
            writer.WriteElementString("Medicine", character.medicine.ToString());
            writer.WriteElementString("Repairing", character.repairing.ToString());
            writer.WriteElementString("ShipControl", character.shipcontrol.ToString());
            writer.WriteElementString("Infantery", character.infantery.ToString());
            writer.WriteElementString("Searching", character.searching.ToString());

            writer.WriteElementString("KnowGuns", character.knowGuns.ToString());
            writer.WriteElementString("KnowUnarmed", character.knowUnarmed.ToString());
            writer.WriteElementString("KnowExplosives", character.knowExplosives.ToString());
            writer.WriteElementString("KnowPolitics", character.knowPol.ToString());
            writer.WriteElementString("KnowPlants", character.knowPlants.ToString());
            writer.WriteElementString("KnowSpaceships", character.knowSpaceShips.ToString());
            writer.WriteElementString("KnowTraps", character.knowTraps.ToString());
            writer.WriteElementString("KnowArchitecture", character.knowArchitecture.ToString());
            writer.WriteElementString("KnowMedicine", character.knowMedicine.ToString());
            writer.WriteElementString("KnowHistory", character.knowHistory.ToString());
            writer.WriteElementString("KnowTech", character.knowTech.ToString());
            writer.WriteElementString("KnowComputers", character.knowComputers.ToString());
            writer.WriteElementString("KnowBionic", character.knowBionic.ToString());
            writer.WriteElementString("KnowCriminality", character.knowCriminality.ToString());
            writer.WriteElementString("KnowAstronomy", character.knowAstronomy.ToString());
            writer.WriteElementString("KnowSciences", character.knowSciences.ToString());
            writer.WriteElementString("KnowMilitary", character.knowMilitary.ToString());

            writer.WriteElementString("LangUSL", character.langusl.ToString());
            writer.WriteElementString("LangFornsh", character.langfornsh.ToString());
            writer.WriteElementString("LangMekish", character.langmekish.ToString());
            writer.WriteElementString("LangLop", character.langlop.ToString());
            writer.WriteElementString("LangVulsh", character.langvulsh.ToString());

            writer.WriteElementString("charHitChance", character.hitchance.ToString());

            writer.WriteEndElement();

            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();

        }

        //Liest die eingegebene XML Datei und fügst dann alles in der App ein
        public void readAsteriusXML(string path)
        {
            if (File.Exists(path))
            {
                string temppath = Path.GetTempPath() + "JRPGTtempAsteriusChar.xml";
                if (File.Exists(temppath))
                {
                    File.Delete(temppath);
                }
                File.Copy(path, temppath);

                using (XmlReader reader = XmlReader.Create(temppath))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name)
                            {
                                case "Name":
                                    character.name = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "Age":
                                    character.age = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "Height":
                                    character.height = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "Gender":
                                    character.gender = reader.ReadElementContentAsString().Trim();
                                    break;

                                case "Gun":
                                    character.gun = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "GunDmg":
                                    character.gundmg = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "GunBonus":
                                    character.gunbonus = reader.ReadElementContentAsString().Trim();
                                    break;

                                case "Outfit":
                                    character.outfit = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "OutfitResist":
                                    character.outfitresist = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "OutfitBonus":
                                    character.outfitbonus = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "OutfitUpgrade":
                                    character.outfitupgrade = reader.ReadElementContentAsString().Trim();
                                    break;

                                case "Style":
                                    character.style = reader.ReadElementContentAsString().Trim();
                                    break;

                                case "HPMax":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int a);
                                    character.hpmax = a;
                                    break;
                                case "HPCurrent":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int b);
                                    character.hpcurr = b;
                                    break;
                                case "RollRequirement":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int h);
                                    character.rollreq = h;
                                    break;
                                case "SleepRegen":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int bd);
                                    character.sleepregen = bd;
                                    break;

                                case "Strength":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int n);
                                    character.strength = n;
                                    break;
                                case "Perception":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int o);
                                    character.perception = o;
                                    break;
                                case "Endurance":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int p);
                                    character.endurance = p;
                                    break;
                                case "Dexterity":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int q);
                                    character.dexter = q;
                                    break;
                                case "Intelligence":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int r);
                                    character.intelli = r;
                                    break;
                                case "Charisma":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int s);
                                    character.chari = s;
                                    break;
                                case "Luck":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int t);
                                    character.luck = t;
                                    break;
                                case "Notes":
                                    character.notes = reader.ReadElementContentAsString().Trim();
                                    break;

                                case "Intel":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int bb);
                                    character.intel = bb;
                                    break;
                                case "Credits":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int bc);
                                    character.credits = bc;
                                    break;

                                case "Guns":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ad);
                                    character.guns = ad;
                                    break;
                                case "SpaceCombat":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ae);
                                    character.spacecombat = ae;
                                    break;
                                case "Unarmed":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int af);
                                    character.unarmed = af;
                                    break;
                                case "Speech":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ag);
                                    character.speech = ag;
                                    break;
                                case "Leading":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ah);
                                    character.leading = ah;
                                    break;
                                case "Trading":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ai);
                                    character.trading = ai;
                                    break;
                                case "Lying":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int aj);
                                    character.lying = aj;
                                    break;
                                case "Pickpocket":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int al);
                                    character.pickpocket = al;
                                    break;
                                case "Lockpicking":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int an);
                                    character.lockpick = an;
                                    break;
                                case "Medicine":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ao);
                                    character.medicine = ao;
                                    break;
                                case "Repairing":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ap);
                                    character.repairing = ap;
                                    break;
                                case "ShipControl":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int aq);
                                    character.shipcontrol = aq;
                                    break;
                                case "Infantery":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ar);
                                    character.infantery = ar;
                                    break;
                                case "Searching":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int at);
                                    character.searching = at;
                                    break;

                                case "LangUSL":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int au);
                                    character.langusl = au;
                                    break;
                                case "LangFornsh":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int av);
                                    character.langfornsh = av;
                                    break;
                                case "LangMekish":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int aw);
                                    character.langmekish = aw;
                                    break;
                                case "LangLop":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ax);
                                    character.langlop = ax;
                                    break;
                                case "LangVulsh":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ay);
                                    character.langvulsh = ay;
                                    break;

                                case "charHitChance":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int hc);
                                    character.hitchance = hc;
                                    break;
                                case "KnowGuns":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int kg);
                                    character.knowGuns = kg;
                                    break;
                                case "KnowUnarmed":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ku);
                                    character.knowUnarmed = ku;
                                    break;
                                case "KnowExplosives":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ke);
                                    character.knowExplosives = ke;
                                    break;
                                case "KnowPolitics":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int kp);
                                    character.knowPol = kp;
                                    break;
                                case "KnowPlants":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int kpl);
                                    character.knowPlants = kpl;
                                    break;
                                case "KnowSpaceships":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ks);
                                    character.knowSpaceShips = ks;
                                    break;
                                case "KnowTraps":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int kt);
                                    character.knowTraps = kt;
                                    break;
                                case "KnowArchitecture":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ka);
                                    character.knowArchitecture = ka;
                                    break;
                                case "KnowMedicine":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int km);
                                    character.knowMedicine = km;
                                    break;
                                case "KnowHistory":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int kh);
                                    character.knowHistory = kh;
                                    break;
                                case "KnowTech":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int kte);
                                    character.knowTech = kte;
                                    break;
                                case "KnowComputers":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int kc);
                                    character.knowComputers = kc;
                                    break;
                                case "KnowBionic":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int kbi);
                                    character.knowBionic = kbi;
                                    break;
                                case "KnowCriminality":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int kcr);
                                    character.knowCriminality = kcr;
                                    break;
                                case "KnowAstronomy":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int kas);
                                    character.knowAstronomy = kas;
                                    break;
                                case "KnowSciences":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ksc);
                                    character.knowSciences = ksc;
                                    break;
                                case "KnowMilitary":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int kmi);
                                    character.knowMilitary = kmi;
                                    break;
                                default:
                                    break;
                            }

                        }
                    }
                    reader.Close();
                }
            }

        }

        //Ship stuff from here

        public void createAsteriusShipXML(string path)
        {
            //Gibt den Pfad fürs Debugging in die Konsole aus
            //Console.WriteLine("Speichere bei: " + path);

            xmlSettings.Indent = true;
            XmlWriter writer = XmlWriter.Create(path, xmlSettings);

            writer.WriteStartDocument();
            writer.WriteComment("DO NO CHANGE ANYTHING IN HERE - THE PROGRAM TAKES CARE OF THAT!");
            writer.WriteStartElement("Ship");

            writer.WriteElementString("Name", ship.shipname);

            writer.WriteElementString("recon", ship.recon);
            writer.WriteElementString("tactRecon", ship.tactRecon);
            writer.WriteElementString("fighterBomber", ship.fighterBomber);
            writer.WriteElementString("supportFighter", ship.supportFighter);
            writer.WriteElementString("tankfighter", ship.tankFighter);

            writer.WriteElementString("shipAP", ship.shipAP.ToString());
            writer.WriteElementString("shipAPTurn", ship.shipAPTurn.ToString());
            writer.WriteElementString("shipHPMax", ship.shipHPMax.ToString());
            writer.WriteElementString("shipHPCurr", ship.shipHPCurr.ToString());
            writer.WriteElementString("shipHitChance", ship.shipHitChance.ToString());
            writer.WriteElementString("shipSkills", ship.shipSkills);

            writer.WriteElementString("weaponName1", ship.weaponName1);
            writer.WriteElementString("weaponName2", ship.weaponName2);
            writer.WriteElementString("weaponName3", ship.weaponName3);
            writer.WriteElementString("weaponName4", ship.weaponName4);
            writer.WriteElementString("weaponName5", ship.weaponName5);
            writer.WriteElementString("weaponName6", ship.weaponName6);

            writer.WriteElementString("weaponAP1", ship.weaponAP1);
            writer.WriteElementString("weaponAP2", ship.weaponAP2);
            writer.WriteElementString("weaponAP3", ship.weaponAP3);
            writer.WriteElementString("weaponAP4", ship.weaponAP4);
            writer.WriteElementString("weaponAP5", ship.weaponAP5);
            writer.WriteElementString("weaponAP6", ship.weaponAP6);

            writer.WriteElementString("weaponDmg1", ship.weaponDmg1);
            writer.WriteElementString("weaponDmg2", ship.weaponDmg2);
            writer.WriteElementString("weaponDmg3", ship.weaponDmg3);
            writer.WriteElementString("weaponDmg4", ship.weaponDmg4);
            writer.WriteElementString("weaponDmg5", ship.weaponDmg5);
            writer.WriteElementString("weaponDmg6", ship.weaponDmg6);

            writer.WriteElementString("weaponAmmo1", ship.weaponAmmo1);
            writer.WriteElementString("weaponAmmo2", ship.weaponAmmo2);
            writer.WriteElementString("weaponAmmo3", ship.weaponAmmo3);
            writer.WriteElementString("weaponAmmo4", ship.weaponAmmo4);
            writer.WriteElementString("weaponAmmo5", ship.weaponAmmo5);
            writer.WriteElementString("weaponAmmo6", ship.weaponAmmo6);

            writer.WriteElementString("weaponRange1", ship.weaponRange1);
            writer.WriteElementString("weaponRange2", ship.weaponRange2);
            writer.WriteElementString("weaponRange3", ship.weaponRange3);
            writer.WriteElementString("weaponRange4", ship.weaponRange4);
            writer.WriteElementString("weaponRange5", ship.weaponRange5);
            writer.WriteElementString("weaponRange6", ship.weaponRange6);

            writer.WriteElementString("weaponLoss1", ship.weaponLoss1);
            writer.WriteElementString("weaponLoss2", ship.weaponLoss2);
            writer.WriteElementString("weaponLoss3", ship.weaponLoss3);
            writer.WriteElementString("weaponLoss4", ship.weaponLoss4);
            writer.WriteElementString("weaponLoss5", ship.weaponLoss5);
            writer.WriteElementString("weaponLoss6", ship.weaponLoss6);

            writer.WriteElementString("shield", ship.shield.ToString());
            writer.WriteElementString("armor", ship.armor.ToString());

            writer.WriteEndElement();

            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        //Liest die eingegebene XML Datei und fügst dann alles in der App ein
        public void readAsteriusShip(string path)
        {
            if (File.Exists(path))
            {
                string temppath = Path.GetTempPath() + "JRPGTtempAsteriusShip.xml";
                if (File.Exists(temppath))
                {
                    File.Delete(temppath);
                }
                File.Copy(path, temppath);

                using (XmlReader reader = XmlReader.Create(temppath))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name)
                            {
                                case "Name":
                                    ship.shipname = reader.ReadElementContentAsString().Trim();
                                    break;

                                case "recon":
                                    ship.recon = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "tactRecon":
                                    ship.tactRecon = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "fighterBomber":
                                    ship.fighterBomber = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "supportFighter":
                                    ship.supportFighter = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "tankfighter":
                                    ship.tankFighter = reader.ReadElementContentAsString().Trim();
                                    break;

                                case "shipAP":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int a);
                                    ship.shipAP = a;
                                    break;
                                case "shipAPTurn":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int b);
                                    ship.shipAPTurn = b;
                                    break;
                                case "shipHPMax":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int c);
                                    ship.shipHPMax = c;
                                    break;
                                case "shipHPCurr":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int d);
                                    ship.shipHPCurr = d;
                                    break;
                                case "shipHitChance":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int e);
                                    ship.shipHitChance = e;
                                    break;
                                case "shipSkills":
                                    ship.shipSkills = reader.ReadElementContentAsString().Trim();
                                    break;

                                case "weaponName1":
                                    ship.weaponName1 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponAP1":
                                    ship.weaponAP1 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponDmg1":
                                    ship.weaponDmg1 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponAmmo1":
                                    ship.weaponAmmo1 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponRange1":
                                    ship.weaponRange1 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponLoss1":
                                    ship.weaponLoss1 = reader.ReadElementContentAsString().Trim();
                                    break;

                                case "weaponName2":
                                    ship.weaponName2 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponAP2":
                                    ship.weaponAP2 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponDmg2":
                                    ship.weaponDmg2 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponAmmo2":
                                    ship.weaponAmmo2 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponRange2":
                                    ship.weaponRange2 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponLoss2":
                                    ship.weaponLoss2 = reader.ReadElementContentAsString().Trim();
                                    break;

                                case "weaponName3":
                                    ship.weaponName3 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponAP3":
                                    ship.weaponAP3 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponDmg3":
                                    ship.weaponDmg3 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponAmmo3":
                                    ship.weaponAmmo3 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponRange3":
                                    ship.weaponRange3 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponLoss3":
                                    ship.weaponLoss3 = reader.ReadElementContentAsString().Trim();
                                    break;

                                case "weaponName4":
                                    ship.weaponName4 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponAP4":
                                    ship.weaponAP4 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponDmg4":
                                    ship.weaponDmg4 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponAmmo4":
                                    ship.weaponAmmo4 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponRange4":
                                    ship.weaponRange4 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponLoss4":
                                    ship.weaponLoss4 = reader.ReadElementContentAsString().Trim();
                                    break;

                                case "weaponName5":
                                    ship.weaponName5 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponAP5":
                                    ship.weaponAP5 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponDmg5":
                                    ship.weaponDmg5 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponAmmo5":
                                    ship.weaponAmmo5 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponRange5":
                                    ship.weaponRange5 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponLoss5":
                                    ship.weaponLoss5 = reader.ReadElementContentAsString().Trim();
                                    break;

                                case "weaponName6":
                                    ship.weaponName6 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponAP6":
                                    ship.weaponAP6 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponDmg6":
                                    ship.weaponDmg6 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponAmmo6":
                                    ship.weaponAmmo6 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponRange6":
                                    ship.weaponRange6 = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "weaponLoss6":
                                    ship.weaponLoss6 = reader.ReadElementContentAsString().Trim();
                                    break;

                                case "shield":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int sh);
                                    ship.shield = sh;
                                    break;
                                case "armor":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ar);
                                    ship.armor = ar;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    reader.Close();
                }
            }

        }


        //Überprüft ob PlayerCharacter1.xml und PlayerShip1.xml da sind und erstellt einen Satz .xml Daten, wenn nicht.
        public void createXMLsAtStart()
        {
            string pathP1 = Application.StartupPath + "/Asterius/characters/players/PlayerCharacter1.xml";
            string pathP2 = Application.StartupPath + "/Asterius/characters/players/PlayerCharacter2.xml";
            string pathP3 = Application.StartupPath + "/Asterius/characters/players/PlayerCharacter3.xml";
            string pathP4 = Application.StartupPath + "/Asterius/characters/players/PlayerCharacter4.xml";

            string pathC1 = Application.StartupPath + "/Asterius/characters/companions/CompanionCharacter1.xml";
            string pathC2 = Application.StartupPath + "/Asterius/characters/companions/CompanionCharacter2.xml";
            string pathC3 = Application.StartupPath + "/Asterius/characters/companions/CompanionCharacter3.xml";

            string pathE1 = Application.StartupPath + "/Asterius/characters/enemies/EnemyCharacter1.xml";
            string pathE2 = Application.StartupPath + "/Asterius/characters/enemies/EnemyCharacter2.xml";
            string pathE3 = Application.StartupPath + "/Asterius/characters/enemies/EnemyCharacter3.xml";
            string pathE4 = Application.StartupPath + "/Asterius/characters/enemies/EnemyCharacter4.xml";
            string pathE5 = Application.StartupPath + "/Asterius/characters/enemies/EnemyCharacter5.xml";
            string pathE6 = Application.StartupPath + "/Asterius/characters/enemies/EnemyCharacter6.xml";
            string pathE7 = Application.StartupPath + "/Asterius/characters/enemies/EnemyCharacter7.xml";
            string pathE8 = Application.StartupPath + "/Asterius/characters/enemies/EnemyCharacter8.xml";

            string pathPS1 = Application.StartupPath + "/Asterius/ships/players/PlayerShip1.xml";
            string pathPS2 = Application.StartupPath + "/Asterius/ships/players/PlayerShip2.xml";
            string pathPS3 = Application.StartupPath + "/Asterius/ships/players/PlayerShip3.xml";
            string pathPS4 = Application.StartupPath + "/Asterius/ships/players/PlayerShip4.xml";

            string pathCS1 = Application.StartupPath + "/Asterius/ships/companions/CompanionShip1.xml";
            string pathCS2 = Application.StartupPath + "/Asterius/ships/companions/CompanionShip2.xml";
            string pathCS3 = Application.StartupPath + "/Asterius/ships/companions/CompanionShip3.xml";

            string pathES1 = Application.StartupPath + "/Asterius/ships/enemies/EnemyShip1.xml";
            string pathES2 = Application.StartupPath + "/Asterius/ships/enemies/EnemyShip2.xml";
            string pathES3 = Application.StartupPath + "/Asterius/ships/enemies/EnemyShip3.xml";
            string pathES4 = Application.StartupPath + "/Asterius/ships/enemies/EnemyShip4.xml";
            string pathES5 = Application.StartupPath + "/Asterius/ships/enemies/EnemyShip5.xml";
            string pathES6 = Application.StartupPath + "/Asterius/ships/enemies/EnemyShip6.xml";
            string pathES7 = Application.StartupPath + "/Asterius/ships/enemies/EnemyShip7.xml";
            string pathES8 = Application.StartupPath + "/Asterius/ships/enemies/EnemyShip8.xml";

            if (!File.Exists(Application.StartupPath + "/Asterius/characters/players/PlayerCharacter1.xml"))
            {
                createAsteriusXML(pathP1);
                createAsteriusXML(pathP2);
                createAsteriusXML(pathP3);
                createAsteriusXML(pathP4);

                createAsteriusXML(pathC1);
                createAsteriusXML(pathC2);
                createAsteriusXML(pathC3);

                createAsteriusXML(pathE1);
                createAsteriusXML(pathE2);
                createAsteriusXML(pathE3);
                createAsteriusXML(pathE4);
                createAsteriusXML(pathE5);
                createAsteriusXML(pathE6);
                createAsteriusXML(pathE7);
                createAsteriusXML(pathE8);
            }

            if (!File.Exists(Application.StartupPath + "/Asterius/ships/players/PlayerShip1.xml"))
            {
                createAsteriusShipXML(pathPS1);
                createAsteriusShipXML(pathPS2);
                createAsteriusShipXML(pathPS3);
                createAsteriusShipXML(pathPS4);

                createAsteriusShipXML(pathCS1);
                createAsteriusShipXML(pathCS2);
                createAsteriusShipXML(pathCS3);

                createAsteriusShipXML(pathES1);
                createAsteriusShipXML(pathES2);
                createAsteriusShipXML(pathES3);
                createAsteriusShipXML(pathES4);
                createAsteriusShipXML(pathES5);
                createAsteriusShipXML(pathES6);
                createAsteriusShipXML(pathES7);
                createAsteriusShipXML(pathES8);
            }
        }

    }


}
