﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace tsorcRevamp.Items.Armors.Ranged
{
    [AutoloadEquip(EquipType.Body)]
    public class TheUnforseeable : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+2 life regen / +11 life regen when health is below 100");
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 10;
            Item.rare = ItemRarityID.Yellow;
            Item.value = PriceByRarity.fromItem(Item);
        }

        public override void UpdateEquip(Player player)
        {
            player.ammoCost75 = true;
            if (player.statLife <= 100)
            {
                player.lifeRegen += 11;
            }
            else
            {
                player.lifeRegen += 2;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HallowedPlateMail, 1);
            recipe.AddIngredient(ModContent.ItemType<DarkSoul>(), 20000);
            recipe.AddTile(TileID.DemonAltar);
            
            recipe.Register();
        }
    }
}
