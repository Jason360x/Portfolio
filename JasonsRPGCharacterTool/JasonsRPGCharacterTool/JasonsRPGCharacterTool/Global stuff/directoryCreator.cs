using System.IO;
using System.Windows.Forms;

namespace JasonsRPGCharacterTool.Global_stuff
{
    class directoryCreator
    {
        //Überprüft ob Ordner fehlen und erstellt sie ggf.
        public static void createDirectories()
        {
            if (!Directory.Exists(Application.StartupPath + "/Asterius"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/Asterius");
            }

            if (!Directory.Exists(Application.StartupPath + "/Asterius/characters"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/Asterius/characters");
            }
            if (!Directory.Exists(Application.StartupPath + "/Asterius/characters/companions"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/Asterius/characters/companions");
            }
            if (!Directory.Exists(Application.StartupPath + "/Asterius/characters/players"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/Asterius/characters/players");
            }
            if (!Directory.Exists(Application.StartupPath + "/Asterius/characters/enemies"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/Asterius/characters/enemies");
            }
            if (!Directory.Exists(Application.StartupPath + "/Asterius/characters/pictures"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/Asterius/characters/pictures");
            }

            if (!Directory.Exists(Application.StartupPath + "/TES"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/TES");
            }
            if (!Directory.Exists(Application.StartupPath + "/TES/characters"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/TES/characters");
            }
            if (!Directory.Exists(Application.StartupPath + "/TES/characters/companions"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/TES/characters/companions");
            }
            if (!Directory.Exists(Application.StartupPath + "/TES/characters/players"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/TES/characters/players");
            }
            if (!Directory.Exists(Application.StartupPath + "/TES/characters/enemies"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/TES/characters/enemies");
            }
            if (!Directory.Exists(Application.StartupPath + "/TES/characters/pictures"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/TES/characters/pictures");
            }

            if (!Directory.Exists(Application.StartupPath + "/Asterius/shop"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/Asterius/shop");
            }
            if (!Directory.Exists(Application.StartupPath + "/TES/shop"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/TES/shop");
            }

            if (!Directory.Exists(Application.StartupPath + "/Asterius/ships"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/Asterius/ships");
            }
            if (!Directory.Exists(Application.StartupPath + "/Asterius/ships/companions"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/Asterius/ships/companions");
            }
            if (!Directory.Exists(Application.StartupPath + "/Asterius/ships/players"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/Asterius/ships/players");
            }
            if (!Directory.Exists(Application.StartupPath + "/Asterius/ships/enemies"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/Asterius/ships/enemies");
            }
            if (!Directory.Exists(Application.StartupPath + "/Asterius/ships/pictures"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/Asterius/ships/pictures");
            }
        }
    }
}
