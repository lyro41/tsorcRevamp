﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace tsorcRevamp.Items.Accessories.Expert
{
    public class DragonStone : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Imbues swords with fire, raises damage dealt by 5% and provides immunity to" +
                                "\nmost flying creatures, lava, catching on fire, knockback, and fire blocks.");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.accessory = true;
            Item.value = PriceByRarity.LightRed_4;
            Item.expert = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.05f;
            player.noKnockback = true;
            player.fireWalk = true;
            player.lavaImmune = true;
            player.buffImmune[BuffID.OnFire] = true;
            player.AddBuff(BuffID.WeaponImbueFire, 60, false);
            Main.LocalPlayer.GetModPlayer<tsorcRevampPlayer>().DragonStone = true;
        }


    }
}
