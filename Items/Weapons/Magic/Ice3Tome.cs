﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace tsorcRevamp.Items.Weapons.Magic {
    class Ice3Tome : ModItem {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Ice 3 Tome");
            Tooltip.SetDefault("A lost tome fabled to deal great damage.\n" +
                                "Only mages will be able to realize this tome's full potential. \n" +
                                "Can be upgraded with 80,000 Dark Souls");

        }
        public override void SetDefaults() {
            item.autoReuse = true; //why was it the only one without autoreuse?
            item.damage = 32;
            item.height = 10;
            item.knockBack = 0f;
            item.maxStack = 1;
            item.rare = ItemRarityID.Green;
            item.scale = 1;
            item.channel = true;
            item.shootSpeed = 10;
            item.magic = true;
            item.noMelee = true;
            item.mana = 30;
            item.useAnimation = 10;
            item.UseSound = SoundID.Item21;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useTime = 10;
            item.value = 200000;
            item.width = 34;
            item.shoot = ModContent.ProjectileType<Projectiles.Ice3Ball>();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int count = 0;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].modProjectile != null && Main.projectile[i].modProjectile is Projectiles.Ice3Ball)
                {
                    Projectiles.Ice3Ball iceBall = (Projectiles.Ice3Ball)Main.projectile[i].modProjectile;
                    if (iceBall.isChanneled)
                    {
                        count++;
                    }
                }
            }
            if (count < 10) return true;
            else return false;
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.GetItem("Ice2Tome"), 1);
            recipe.AddIngredient(ItemID.SoulofLight, 15);
            recipe.AddIngredient(mod.GetItem("DarkSoul"), 25000);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
