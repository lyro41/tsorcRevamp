﻿using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace tsorcRevamp.Items.Weapons {
    class WandOfDarkness2 : ModItem {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Wand of Darkness II");
            Tooltip.SetDefault("Greater damage and higher knockback");
        }
        public override void SetDefaults() {
            item.autoReuse = true;
            item.width = 12;
            item.height = 17;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 25;
            item.useTime = 25;
            item.damage = 20;
            item.knockBack = 6;
            item.mana = 4;
            item.UseSound = SoundID.Item8;
            item.shootSpeed = 7;
            item.noMelee = true;
            item.value = 53000;
            item.magic = true;
            item.shoot = ModContent.ProjectileType<Projectiles.ShadowBall>();
        }
        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.GetItem("WandOfDarkness"), 1);
            recipe.AddIngredient(mod.GetItem("DarkSoul"), 2700);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}