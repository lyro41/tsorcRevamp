using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace tsorcRevamp.Items.Weapons.Runeterra.Summon
{
    public class CotUIAnim2 : ModProjectile
    {
        public static float holditemtimer2 = 0f;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 9;
        }
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.aiStyle = 1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 999999999;
        }
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            if (owner.direction == 1)
            {
                Projectile.position = Main.player[Projectile.owner].Center + new Vector2(7, -34);//7
            } else
            {
                Projectile.position = Main.player[Projectile.owner].Center + new Vector2(-34, -34);
            }
            if (holditemtimer2 <= 0)
            {
                Projectile.Kill();
            }

            Visuals();
        }
        private void Visuals()
        {
            int frameSpeed = 5;

            Projectile.frameCounter++;

            if (Projectile.frameCounter >= frameSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }

            // Some visuals here
            Lighting.AddLight(Projectile.Center, Color.LightSteelBlue.ToVector3() * 0.78f);
            Dust.NewDust(Projectile.Center, 2, 2, DustID.MagicMirror, 0, 0, 150, default, 0.5f);
        }
    }
}