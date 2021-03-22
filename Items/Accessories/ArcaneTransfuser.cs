using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BloodToMana.Items.Accessories
{
	public class ArcaneTransfuser : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Celestial Tick");
			Tooltip.SetDefault("Trade life to unleash a wild burst of magical energy!" +
	"\nHas the effect of the Hercules Beetle!");
		}
		public override void SetDefaults()
		{
			//This determines the width hitbox of the item in pixels
			item.width = 34;
			//This determines the height hitbox of the item in pixels
			item.height = 30;
			item.accessory = true;
			item.value = Item.sellPrice(gold: 10);
			item.rare = ItemRarityID.Yellow;
			
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			//This line, when the accessory is worn, overrides the "Reset Effects" in BloodToManaPlayer.cs so that it registers as true. Allowing the if statement in Arcane Transfuser.cs to execute the code
			player.GetModPlayer<BloodToManaPlayer>().wearingAccessory = true;
			player.minionKB += 2f;
			player.minionKB += 1.15f;
			if (!player.HasBuff(mod.BuffType("ArcaneInfusion")))
				player.manaRegenDelay -= 10;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 5);
			recipe.AddIngredient(ItemID.HerculesBeetle, 1);
			recipe.AddIngredient(ItemID.ManaCrystal, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}