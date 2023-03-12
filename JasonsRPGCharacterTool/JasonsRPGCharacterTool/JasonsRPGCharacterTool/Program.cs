using System;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace JasonsRPGCharacterTool
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        /// 
        public static string[] startArgs;

        public static bool hasInternet = false;
        public static bool forceNoUpdate = false;

        [STAThread]
        static void Main(string[] args)
        {
            //Erstellt alle benötigten Verzeichnisse, falls sie fehlen
            Global_stuff.directoryCreator.createDirectories();

            startArgs = args;

            //Mit DE oder EN kann man die jeweilige Sprache mit den Startargumenten forcieren
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToUpper())
                {
                    case "DE":
                    case "-DE":
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("de-DE");
                        break;

                    case "EN":
                    case "-EN":
                    case "US":
                    case "-US":
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-EN");
                        break;

                    case "NOUPDATE":
                    case "-NOUPDATE":
                        forceNoUpdate = true;
                        break;
                    default:
                        break;
                }
            }

            //Prüft ob eine Verbindung zu Github hergestellt werden kann
            hasInternet = checkInternetConnection();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new rpgChoice());
        }

        static bool checkInternetConnection()
        {
            if (!forceNoUpdate)
            {
                try
                {
                    using (var client = new WebClient())
                    {
                        string currentVersion = client.DownloadString("https://jason360x.github.io/RPGTool/currentRPGToolVersion.txt");
                    }
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Can't connect to Github! No internet connection? Exception: " + e);

                    return false;
                }
            }
            return false;
        }
    }
}
