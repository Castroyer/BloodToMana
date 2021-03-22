using Terraria;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ID;

namespace BloodToMana.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class FoxHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fox Hat");
            Tooltip.SetDefault("Vanity Item");
        }
        public override void SetDefaults()
        {
            item.Size = new Vector2(18);
            item.value = Item.sellPrice(silver: 1);
            item.rare = ItemRarityID.Orange;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FoxMask, 1);
            recipe.AddIngredient(ItemID.WizardHat, 1);
            recipe.AddIngredient(ItemID.BlackInk, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
