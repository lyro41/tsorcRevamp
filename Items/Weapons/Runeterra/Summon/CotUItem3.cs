
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace tsorcRevamp.Items.Weapons.Runeterra.Summon
{
	public class CotUItem3 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Center of the Universe");
			Tooltip.SetDefault("Summons stars to rotate around you and damage enemies in their way" +
				"\nIncrease their radius by right-clicking" +
                "\nReset their range by clearing the buff" +
                "\nStars will gain a movement speed and damage boost on crit for 3 seconds" +
                "\nEvery third crit spawns an Arcanum projectile");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
		}
        public override void SetDefaults()
		{
			Item.damage = 124;
			Item.knockBack = 3f;
			Item.mana = 10;
			Item.width = 1;
			Item.height = 1;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.holdStyle = ItemHoldStyleID.HoldFront;
			Item.noUseGraphic = true;
			Item.useTurn = false;
			Item.value = Item.buyPrice(1, 0, 0, 0);
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item117;

			Item.noMelee = true;
			Item.DamageType = DamageClass.Summon;
			Item.buffType = ModContent.BuffType<CotUBuff3>();
			Item.shoot = ModContent.ProjectileType<CotUStar3>();
		}
		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (Main.mouseRight & !Main.mouseLeft)
			{
				player.altFunctionUse = 2;
				Item.useStyle = ItemUseStyleID.HoldUp;
				CotUStar3.circleRad3 += 1f;
			}
			if (Main.mouseLeft)
			{
				player.altFunctionUse = 1;
				Item.useStyle = ItemUseStyleID.Shoot;
			}
		}
		public override void UpdateInventory(Player player)
		{
			if (Main.GameUpdateCount % 1 == 0)
			{
				CotUIAnim3.holditemtimer3 -= 0.3f;
			}
		}
		public override void HoldItem(Player player)
		{
			bool CotUItemAnim3Exists = false;
			CotUIAnim3.holditemtimer3 = 0.2f;
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				if (Main.projectile[i].active && Main.projectile[i].type == ModContent.ProjectileType<CotUIAnim3>() && Main.projectile[i].owner == player.whoAmI)
				{
					CotUItemAnim3Exists = true;
					break;
				}
			}
			if (!CotUItemAnim3Exists)
			{
				Projectile.NewProjectile(Projectile.GetSource_None(), player.Center, Vector2.Zero, ModContent.ProjectileType<CotUIAnim3>(), 0, 0, Main.myPlayer);
			}
		}
		public override bool CanShoot(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				return false;
			}
			return true;
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			// Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
			position = player.Bottom;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			// This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
			player.AddBuff(Item.buffType, 2);

			// Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
			var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
			projectile.originalDamage = Item.damage;

			// Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
			return false;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();

			recipe.AddIngredient(ModContent.ItemType<CotUItem2>());
			recipe.AddIngredient(ItemID.LunarBar, 12);
			recipe.AddIngredient(ModContent.ItemType<DarkSoul>(), 70000);

			recipe.AddTile(TileID.DemonAltar);

			recipe.Register();
		}
	}
}