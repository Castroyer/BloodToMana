using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BloodToMana.Items
{
	public class TestingStick : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Testing Stick");
			Tooltip.SetDefault("Made to test whatever I need it to");
		}

		public override void SetDefaults()
		{
			item.width = 8;
			item.height = 32;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useTurn = true;
			item.consumable = false;
			item.UseSound = SoundID.Item2;
		}
		public override bool CanUseItem(Player player)
		{
			int buff = mod.BuffType("ArcaneInfusion");
			return !player.HasBuff(buff);
		}

		public override bool UseItem(Player player)
		{
			player.AddBuff(mod.BuffType("ArcaneInfusion"), 18000);

			return base.UseItem(player);
		}
	}
}