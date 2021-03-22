using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BloodToMana.Buffs
{
    public class ArcaneInfusion : ModBuff
    {

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Bloody Equivalence");
            Description.SetDefault("An uncontrollable stream of magic flows through you, but you feel anemic.");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            canBeCleared = false;
        }

        //This is the beginning of a timer. This timer regulates the mana regeneration rate of the Arcane Infusion Buff.
        public int Timer;
        public override void Update(Player player, ref int buffIndex)
        {
            Timer++;

            if (Timer % 2 == 0) // tick 1 > 1 = false, tick 2 > 0 = true, tick 3 > 1 = false, tick 4 > 0 = true ...
            {
                if (player.HasBuff(mod.BuffType("ArcaneInfusion")))
                    player.statMana += 1;
            }

            //If the player is below a certain life threshold, they will gain the below boosts
            if (player.statLife < 100)
            {
                if (player.HasBuff(mod.BuffType("ArcaneInfusion")))
                    player.statMana += 1;
                player.magicDamage += 1.25f;
                player.magicCrit += 20;
            }
            player.buffTime[buffIndex] = 18000;
            //This will stall your mana regen so that "player.statMana" can completely take over.
            player.manaRegenDelay = 180;
            //This will drain your life
            player.lifeRegen -= 25;
            //This will buff your magic damage
            player.magicDamage += 1.5f;
            player.magicCrit += 15;
            //This will Increase your max mana
            player.statManaMax2 += 100;
        }
    }
}