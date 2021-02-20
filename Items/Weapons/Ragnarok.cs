using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace tsorcRevamp.Items.Weapons
{
    public class Ragnarok : ModItem
    {
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Ragnarok");
            Tooltip.SetDefault("");

	}

        public override void SetDefaults()
        {


            
            item.stack = 1;
            //item.prefixType=368;
            item.rare = ItemRarityID.Pink;
            item.useTurn = true;
            item.autoReuse = true;
            item.damage = 170;
            item.knockBack = (float)10;
            item.maxStack = 1;
            item.melee = true;
            item.scale = (float)1.3;
            item.useAnimation = 15;
            item.UseSound = SoundID.Item1;
            item.useStyle =ItemUseStyleID.SwingThrow;
            item.useTime = 21;
            item.value = 820000;
        }


    }
}
