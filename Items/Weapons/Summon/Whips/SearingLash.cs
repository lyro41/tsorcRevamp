using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace tsorcRevamp.Items.Weapons.Summon.Whips
{
	public class SearingLash : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;  //journey mode lmao
			Tooltip.SetDefault("Enemies struck by this whip will burn horribly" +
                "\nand increase minion damage by 66% of this whips base damage in +%" +
                "\nThis stacks on top of other whip tag dmg" +
                "\nThis whip can be charged up for increased range and damage" +
                "\nYour minions will focus struck enemies");
		}

		public override void SetDefaults()
		{

			Item.height = 84;
			Item.width = 88;

			Item.DamageType = DamageClass.SummonMeleeSpeed;
			Item.damage = 30;
			Item.knockBack = 2;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.buyPrice(0, 8, 50, 0);

			Item.shoot = ModContent.ProjectileType<Projectiles.Summon.Whips.SearingLashProjectile>();
			Item.shootSpeed = 4;
			Item.channel = true;

			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 30; // for some reason a lower use speed gives it increased range....
			Item.useAnimation = 30;
			Item.noMelee = true;
			Item.noUseGraphic = true;

		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.HellstoneBar, 10);
			recipe.AddIngredient(ItemID.MeteoriteBar, 10);
			recipe.AddIngredient(ModContent.ItemType<DarkSoul>(), 5500);

			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}