using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace tsorcRevamp.Items.VanillaItems
{
    class RangerNerfs : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.ChlorophyteBullet)
            {
                //chlorophyte bullets are fucking stupid, dont @ me
                item.damage = 6; //from 10
            }

            //Lunar items
            if (item.type == ItemID.Phantasm)
            {
                item.damage = 35;
            }
        }
    }
}
