
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace tsorcRevamp.Items.Weapons.Runeterra.Ranged
{
    public class TSItem3 : ModItem
    {
        public float cooldown = 0f;
        public static float shroomCD = 0f;
        public static bool ToxicShotHeld = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Omega Squad Rifle");
            Tooltip.SetDefault("Converts seeds into Toxic Shots, these scale with magic damage too" +
                "\nAlso uses all darts as ammo" +
                "\nRight click on a cd to shoot a homing blind dart which inflicts confusion, also scales with magic damage" +
                "\nPress Q hotkey on a cd to drop a miniature nuclear bomb, scaling with magic damage too");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 8;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item64;//63
            Item.DamageType = DamageClass.Ranged; 
            Item.damage = 123;
            Item.knockBack = 1f;
            Item.noMelee = true;
            Item.shoot = ProjectileID.Seed;
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Dart;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.Seed & player.altFunctionUse == 1)
            {
                type = ModContent.ProjectileType<TSToxicShot>();
            }
            if (player.altFunctionUse == 2)
            {
                type = ModContent.ProjectileType<TSBlindDart>();
            }
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (Main.mouseRight & !Main.mouseLeft)
            {
                player.altFunctionUse = 2;
                cooldown = 10;
            }
            if (Main.mouseLeft)
            {
                player.altFunctionUse = 1;
            }
        }
        public override void HoldItem(Player player)
        {
            ToxicShotHeld = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2 || cooldown <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override void UpdateInventory(Player player)
        {
            if (Main.GameUpdateCount % 1 == 0)
            {
                cooldown -= 0.0167f;
                shroomCD -= 0.0167f;
            }
            if (Main.GameUpdateCount % 10 == 0)
            {
                ToxicShotHeld = false;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<TSItem2>());
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(ModContent.ItemType<DarkSoul>(), 70000);
            recipe.AddTile(TileID.DemonAltar);

            recipe.Register();
        }
    }
}