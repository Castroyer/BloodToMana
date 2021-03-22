using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework.Audio;

namespace BloodToMana
{
    public class BloodToMana : Mod
    {
        public override void PreSaveAndQuit()
        {
            Main.LocalPlayer.GetModPlayer<BloodToManaPlayer>().SparkleSparkInstance?.Stop(true);
        }
        public static ModHotKey TickHotKey;

        public override void Load()
        {
            TickHotKey = RegisterHotKey("Celestial Tick Effect", "P");
        }
        public override void Unload()
        {
            TickHotKey = null;
        }
    }
}