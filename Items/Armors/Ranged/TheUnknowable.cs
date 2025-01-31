﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace tsorcRevamp.Items.Armors.Ranged
{
    [AutoloadEquip(EquipType.Head)]
    public class TheUnknowable : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("25% chance to not consume ammo\nInfinite breath, waterwalk, no knockback\nSet Bonus: +25% ranged damage, crit and +21% movement speed + Archery Skill + No Fall DMG");
            ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 15;
            Item.rare = ItemRarityID.Yellow;
            Item.value = PriceByRarity.fromItem(Item);
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<TheUnforseeable>() && legs.type == ModContent.ItemType<TheUntouchable>();
        }

        public override void UpdateEquip(Player player)
        {
            player.ammoCost75 = true;
            player.breath = 999999;
            player.waterWalk = true;
            player.noKnockback = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.25f;
            player.GetCritChance(DamageClass.Ranged) += 25;
            player.moveSpeed += 0.21f;
            player.archery = true;
            player.noFallDmg = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HallowedHelmet, 1);
            recipe.AddIngredient(ModContent.ItemType<DarkSoul>(), 10000);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
