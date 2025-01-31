﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace tsorcRevamp.Items.Armors.Summon
{
    [AutoloadEquip(EquipType.Body)]
    public class WitchkingTop : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+25% minion damage\nIncreases your max number of minions by 2\nGrants immunity to 'On Fire'" +
                "\nSet Bonus: +1 Turret Slot\n+20% whip range, +25% whip speed\n+30% movement speed\nKnockback and fall damage immunity");
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.defense = 22;
            Item.rare = ItemRarityID.Purple;
            Item.value = PriceByRarity.fromItem(Item);
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return head.type == ModContent.ItemType<WitchkingHelmet>() && legs.type == ModContent.ItemType<WitchkingBottoms>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.maxTurrets += 1;
            player.GetAttackSpeed(DamageClass.Summon) += 0.25f;
            player.whipRangeMultiplier += 0.2f;
            player.moveSpeed += 0.3f;
            player.noKnockback = true;
            player.noFallDmg = true;

            int i2 = (int)(player.position.X + (float)(player.width / 2) + (float)(8 * player.direction)) / 16;
            int j2 = (int)(player.position.Y + 2f) / 16;
            Lighting.AddLight(i2, j2, 0.92f, 0.8f, 0.65f);
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.25f;
            player.maxMinions += 2;
            player.onFire = false;
        }
    }
}
