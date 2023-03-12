using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace JasonsRPGCharacterTool.TES_stuff
{
    class xmlCreator_TES
    {
        //Erstellt ein Settingsobjekt, mit dem man die Einstellungen des XMLWriters verändern kann
        XmlWriterSettings xmlSettings = new XmlWriterSettings();

        //Nimmt strings auf und speichert sie dann am richtigen Platz in einer .xml Datei
        public void createTESXML(string path)
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
            writer.WriteElementString("Race", character.race);
            writer.WriteElementString("Style", character.style);

            writer.WriteElementString("HPMax", character.hpmax.ToString());
            writer.WriteElementString("HPCurrent", character.hpcurr.ToString());
            writer.WriteElementString("MagickaMax", character.magickamax.ToString());
            writer.WriteElementString("MagickaCurrent", character.magickacurr.ToString());
            writer.WriteElementString("PhysArmor", character.physarmor.ToString());
            writer.WriteElementString("MagArmor", character.magarmor.ToString());
            writer.WriteElementString("Parry", character.parry.ToString());

            writer.WriteElementString("RollRequirement", character.rollreq.ToString());
            writer.WriteElementString("Initative", character.init.ToString());
            writer.WriteElementString("FireResist", character.fireresist.ToString());
            writer.WriteElementString("FrostResist", character.frostresist.ToString());
            writer.WriteElementString("ElectroResist", character.electroresist.ToString());
            writer.WriteElementString("PoisonResist", character.poisonresist.ToString());
            writer.WriteElementString("Monster", character.monster);

            writer.WriteElementString("Perception", character.perception.ToString());
            writer.WriteElementString("PerceptionBonus", character.perceptionBonus.ToString());

            writer.WriteElementString("Endurance", character.endurance.ToString());
            writer.WriteElementString("EnduranceBonus", character.enduranceBonus.ToString());

            writer.WriteElementString("Dexterity", character.dexter.ToString());
            writer.WriteElementString("DexterityBonus", character.dexterBonus.ToString());

            writer.WriteElementString("Intelligence", character.intelli.ToString());
            writer.WriteElementString("IntelligenceBonus", character.intelliBonus.ToString());

            writer.WriteElementString("Charisma", character.chari.ToString());
            writer.WriteElementString("CharismaBonus", character.chariBonus.ToString());

            writer.WriteElementString("Concentration", character.concent.ToString());
            writer.WriteElementString("ConcentrationBonus", character.concentBonus.ToString());

            writer.WriteElementString("Strength", character.strength.ToString());
            writer.WriteElementString("StrengthBonus", character.strengthBonus.ToString());

            writer.WriteElementString("Agility", character.agility.ToString());
            writer.WriteElementString("AgilityBonus", character.agilityBonus.ToString());

            writer.WriteElementString("Notes", character.notes);

            writer.WriteElementString("HeavyWeapons", character.heavyweap.ToString());
            writer.WriteElementString("LightWeapons", character.lightweap.ToString());
            writer.WriteElementString("Archery", character.archery.ToString());
            writer.WriteElementString("Throwing", character.throwing.ToString());
            writer.WriteElementString("Unarmed", character.unarmed.ToString());
            writer.WriteElementString("Speech", character.speech.ToString());
            writer.WriteElementString("Pickpocket", character.pickpocket.ToString());
            writer.WriteElementString("Sneak", character.sneak.ToString());
            writer.WriteElementString("Lockpick", character.lockpick.ToString());
            writer.WriteElementString("Singing", character.singing.ToString());
            writer.WriteElementString("Drinking", character.drinking.ToString());
            writer.WriteElementString("Destruction", character.destruction.ToString());
            writer.WriteElementString("Illusion", character.illusion.ToString());
            writer.WriteElementString("Alteration", character.alteration.ToString());
            writer.WriteElementString("Restoration", character.restoration.ToString());
            writer.WriteElementString("Enchanting", character.enchanting.ToString());
            writer.WriteElementString("Conjuration", character.conjuration.ToString());
            writer.WriteElementString("Smithing", character.smithing.ToString());
            writer.WriteElementString("Alchemy", character.alchemy.ToString());
            writer.WriteElementString("Traps", character.traps.ToString());
            writer.WriteElementString("Crafting", character.crafting.ToString());

            writer.WriteEndElement();

            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        //Liest die eingegebene XML Datei und fügst dann alles in der App ein
        public void readTESXML(string path)
        {
            if (File.Exists(path))
            {
                string temppath = Path.GetTempPath() + "JRPGTtempTESChar.xml";
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
                                case "Race":
                                    character.race = reader.ReadElementContentAsString().Trim();
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
                                case "MagickaMax":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int c);
                                    character.magickamax = c;
                                    break;
                                case "MagickaCurrent":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int d);
                                    character.magickacurr = d;
                                    break;
                                case "PhysArmor":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int e);
                                    character.physarmor = e;
                                    break;
                                case "MagArmor":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int f);
                                    character.magarmor = f;
                                    break;
                                case "Parry":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int g);
                                    character.parry = g;
                                    break;
                                case "RollRequirement":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int h);
                                    character.rollreq = h;
                                    break;
                                case "Initative":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int i);
                                    character.init = i;
                                    break;
                                case "FireResist":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int j);
                                    character.fireresist = j;
                                    break;
                                case "FrostResist":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int k);
                                    character.frostresist = k;
                                    break;
                                case "ElectroResist":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int l);
                                    character.electroresist = l;
                                    break;
                                case "PoisonResist":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int m);
                                    character.poisonresist = m;
                                    break;
                                case "Monster":
                                    character.monster = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "Perception":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int n);
                                    character.perception = n;
                                    break;
                                case "Endurance":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int o);
                                    character.endurance = o;
                                    break;
                                case "Dexterity":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int p);
                                    character.dexter = p;
                                    break;
                                case "Intelligence":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int q);
                                    character.intelli = q;
                                    break;
                                case "Charisma":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int r);
                                    character.chari = r;
                                    break;
                                case "Concentration":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int s);
                                    character.concent = s;
                                    break;
                                case "Strength":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int t);
                                    character.strength = t;
                                    break;
                                case "Agility":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int u);
                                    character.agility = u;
                                    break;
                                case "Notes":
                                    character.notes = reader.ReadElementContentAsString().Trim();
                                    break;
                                case "PerceptionBonus":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int v);
                                    character.perceptionBonus = v;
                                    break;
                                case "EnduranceBonus":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int w);
                                    character.enduranceBonus = w;
                                    break;
                                case "DexterityBonus":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int x);
                                    character.dexterBonus = x;
                                    break;
                                case "IntelligenceBonus":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int y);
                                    character.intelliBonus = y;
                                    break;
                                case "CharismaBonus":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int z);
                                    character.chariBonus = z;
                                    break;
                                case "ConcentrationBonus":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int aa);
                                    character.concentBonus = aa;
                                    break;
                                case "StrengthBonus":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ab);
                                    character.strengthBonus = ab;
                                    break;
                                case "AgilityBonus":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ac);
                                    character.agilityBonus = ac;
                                    break;
                                case "HeavyWeapons":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ad);
                                    character.heavyweap = ad;
                                    break;
                                case "LightWeapons":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ae);
                                    character.lightweap = ae;
                                    break;
                                case "Archery":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int af);
                                    character.archery = af;
                                    break;
                                case "Throwing":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ag);
                                    character.throwing = ag;
                                    break;
                                case "Unarmed":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ah);
                                    character.unarmed = ah;
                                    break;
                                case "Speech":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ai);
                                    character.speech = ai;
                                    break;
                                case "Pickpocket":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int aj);
                                    character.pickpocket = aj;
                                    break;
                                case "Sneak":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ak);
                                    character.sneak = ak;
                                    break;
                                case "Lockpick":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int al);
                                    character.lockpick = al;
                                    break;
                                case "Singing":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int an);
                                    character.singing = an;
                                    break;
                                case "Drinking":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ao);
                                    character.drinking = ao;
                                    break;
                                case "Destruction":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ap);
                                    character.destruction = ap;
                                    break;
                                case "Illusion":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int aq);
                                    character.illusion = aq;
                                    break;
                                case "Alteration":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ar);
                                    character.alteration = ar;
                                    break;
                                case "Restoration":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int at);
                                    character.restoration = at;
                                    break;
                                case "Enchanting":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int au);
                                    character.enchanting = au;
                                    break;
                                case "Conjuration":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int av);
                                    character.conjuration = av;
                                    break;
                                case "Smithing":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int aw);
                                    character.smithing = aw;
                                    break;
                                case "Alchemy":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ax);
                                    character.alchemy = ax;
                                    break;
                                case "Traps":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int ay);
                                    character.traps = ay;
                                    break;
                                case "Crafting":
                                    Int32.TryParse(reader.ReadElementContentAsString().Trim(), out int az);
                                    character.crafting = az;
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

        //Überprüft ob PlayerCharacter1.xml da ist und erstellt einen Satz .xml Daten, wenn nicht.
        public void createXMLsAtStart()
        {
            string pathP1 = Application.StartupPath + "/TES/characters/players/PlayerCharacter1.xml";
            string pathP2 = Application.StartupPath + "/TES/characters/players/PlayerCharacter2.xml";
            string pathP3 = Application.StartupPath + "/TES/characters/players/PlayerCharacter3.xml";
            string pathP4 = Application.StartupPath + "/TES/characters/players/PlayerCharacter4.xml";

            string pathC1 = Application.StartupPath + "/TES/characters/companions/CompanionCharacter1.xml";
            string pathC2 = Application.StartupPath + "/TES/characters/companions/CompanionCharacter2.xml";
            string pathC3 = Application.StartupPath + "/TES/characters/companions/CompanionCharacter3.xml";

            string pathE1 = Application.StartupPath + "/TES/characters/enemies/EnemyCharacter1.xml";
            string pathE2 = Application.StartupPath + "/TES/characters/enemies/EnemyCharacter2.xml";
            string pathE3 = Application.StartupPath + "/TES/characters/enemies/EnemyCharacter3.xml";
            string pathE4 = Application.StartupPath + "/TES/characters/enemies/EnemyCharacter4.xml";
            string pathE5 = Application.StartupPath + "/TES/characters/enemies/EnemyCharacter5.xml";
            string pathE6 = Application.StartupPath + "/TES/characters/enemies/EnemyCharacter6.xml";
            string pathE7 = Application.StartupPath + "/TES/characters/enemies/EnemyCharacter7.xml";
            string pathE8 = Application.StartupPath + "/TES/characters/enemies/EnemyCharacter8.xml";

            if (!File.Exists(Application.StartupPath + "/TES/characters/players/PlayerCharacter1.xml"))
            {
                createTESXML(pathP1);
                createTESXML(pathP2);
                createTESXML(pathP3);
                createTESXML(pathP4);
                createTESXML(pathC1);
                createTESXML(pathC2);
                createTESXML(pathC3);
                createTESXML(pathE1);
                createTESXML(pathE2);
                createTESXML(pathE3);
                createTESXML(pathE4);
                createTESXML(pathE5);
                createTESXML(pathE6);
                createTESXML(pathE7);
                createTESXML(pathE8);
            }
        }

    }
}
