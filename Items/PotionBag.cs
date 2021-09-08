﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using tsorcRevamp.UI;

namespace tsorcRevamp.Items {
    class PotionBag : ModItem {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Can store up to 30 potions" + 
                               "\n\"Favorite\" potions in the pouch to allow them to work with Quick Buff!.");
        }

        public override void SetDefaults() {
            item.width = 16;
            item.height = 16;
            item.rare = ItemRarityID.Quest;
            item.value = 0;
            item.useAnimation = 15;
            item.useTime = 15;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.UseSound = SoundID.Item4;
            item.maxStack = 1;
        }

        public override bool CanUseItem(Player player) {
			return true;
        }

        public override bool UseItem(Player player) {
            if(player.whoAmI == Main.myPlayer)
            {
                /** if (!PotionBagUIState.Visible)
                 {
                     PotionBagUIState.Visible = true;
                     Main.PlaySound(SoundID.MenuOpen);

                 }
                 else
                 {
                     PotionBagUIState.Visible = false;
                     Main.PlaySound(SoundID.MenuClose);
                 }**/
            }
            return true;
        }
    }
}