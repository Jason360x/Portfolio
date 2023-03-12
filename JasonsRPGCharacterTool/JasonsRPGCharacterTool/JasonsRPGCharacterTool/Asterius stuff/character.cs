using System;
using System.Runtime.CompilerServices;

namespace JasonsRPGCharacterTool.Asterius_stuff
{
    class character
    {
        public static string 
            name = "", age = "", height = "", gender = "", style = "", notes = "", gun = "", gundmg = "",
            outfit = "", outfitresist = "", outfitbonus = "", outfitupgrade = "", gunbonus = "";

        public static int 
            strength = 1, perception = 1, dexter = 1, intelli = 1, chari = 1, luck = 1, endurance = 1,

            guns = 0, spacecombat = 0, unarmed = 0, speech = 0, leading = 0, trading = 0, lying = 0, pickpocket = 0, lockpick = 0, medicine = 0,
                    repairing = 0, shipcontrol = 0, infantery = 0, searching = 0,

            langusl = 0, langfornsh = 0, langmekish = 0, langlop = 0, langvulsh = 0,

            knowGuns = 0, knowUnarmed = 0, knowExplosives = 0, knowPol = 0, knowPlants = 0, knowSpaceShips = 0, knowTraps = 0,
                    knowArchitecture = 0, knowMedicine = 0, knowHistory = 0, knowTech = 0, knowComputers = 0, knowBionic = 0, 
                    knowCriminality = 0, knowAstronomy = 0, knowSciences = 0, knowMilitary = 0,

            hitchance = 0, credits = 0, intel = 0, hpmax = 1, hpcurr = 0, sleepregen = 1, rollreq = 1;


        public static int knowledgePoints = 0;

        public static void knowledgePointsRefresh(int i)
        {
            knowledgePoints = i * 3;
        }

        public static void clearAll()
        {
            name = ""; age = ""; height = ""; gender = ""; style = ""; notes = ""; gun = ""; gundmg = "";
            outfit = ""; outfitresist = ""; outfitbonus = ""; outfitupgrade = ""; gunbonus = "";

            strength = 1; perception = 1; dexter = 1; intelli = 1; chari = 1; luck = 1; endurance = 1;

            guns = 0; spacecombat = 0; unarmed = 0; speech = 0; leading = 0; trading = 0; lying = 0; pickpocket = 0; lockpick = 0; medicine = 0;
                    repairing = 0; shipcontrol = 0; infantery = 0; searching = 0;

            langusl = 0; langfornsh = 0; langmekish = 0; langlop = 0; langvulsh = 0;

            knowGuns = 0; knowUnarmed = 0; knowExplosives = 0; knowPol = 0; knowPlants = 0; knowSpaceShips = 0; knowTraps = 0;
                    knowArchitecture = 0; knowMedicine = 0; knowHistory = 0; knowTech = 0; knowComputers = 0; knowBionic = 0; 
                    knowCriminality = 0; knowAstronomy = 0; knowSciences = 0; knowMilitary = 0;

            hitchance = 0; credits = 0; intel = 0; hpmax = 1; hpcurr = 0; sleepregen = 1; rollreq = 1;
        }
    }
}
