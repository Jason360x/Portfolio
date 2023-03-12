using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasonsRPGCharacterTool.TES_stuff
{
    class character
    {
       public static string name = "", age = "", height = "", gender = "", race = "", style = "", monster = "", notes = "";

       public static int             
            hpmax = 1, hpcurr = 0, magickamax = 1, magickacurr = 0, physarmor = 0, magarmor = 0, parry = 0, rollreq = 1, init = 1,
            fireresist = 0, frostresist = 0, electroresist = 0, poisonresist = 0,

            perception = 1, endurance = 1, dexter = 1, intelli = 1, chari = 1, concent = 1, strength = 1, agility = 1, 
            perceptionBonus = 0, enduranceBonus = 0, dexterBonus = 0, intelliBonus = 0, chariBonus = 0, concentBonus = 0, strengthBonus = 0, agilityBonus = 0, 

            heavyweap = 0, lightweap = 0, archery = 0, throwing = 0, unarmed = 0, speech = 0, pickpocket = 0, sneak = 0, lockpick = 0, singing = 0, drinking = 0,
            destruction = 0, illusion = 0, alteration = 0, restoration = 0, enchanting = 0, conjuration = 0, smithing = 0, alchemy = 0, traps = 0, crafting = 0;

        public static void clearAll()
        {
            name = ""; age = ""; height = ""; gender = ""; race = ""; style = ""; monster = ""; notes = "";

            hpmax = 1; hpcurr = 0; magickamax = 1; magickacurr = 0; physarmor = 0; magarmor = 0; parry = 0; rollreq = 1; init = 1;
            fireresist = 0; frostresist = 0; electroresist = 0; poisonresist = 0;

            perception = 1; endurance = 1; dexter = 1; intelli = 1; chari = 1; concent = 1; strength = 1; agility = 1; 
            perceptionBonus = 0; enduranceBonus = 0; dexterBonus = 0; intelliBonus = 0; chariBonus = 0; concentBonus = 0; strengthBonus = 0; agilityBonus = 0; 

            heavyweap = 0; lightweap = 0; archery = 0; throwing = 0; unarmed = 0; speech = 0; pickpocket = 0; sneak = 0; lockpick = 0; singing = 0; drinking = 0;
            destruction = 0; illusion = 0; alteration = 0; restoration = 0; enchanting = 0; conjuration = 0; smithing = 0; alchemy = 0; traps = 0; crafting = 0;
        }
    }
}
