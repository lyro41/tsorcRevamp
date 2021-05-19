﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace tsorcRevamp.Projectiles.Enemy {
    class EnemySpellIce3Ball : ModProjectile {
		public override string Texture => "tsorcRevamp/Projectiles/Ice1Ball";
        public override void SetDefaults() {
            projectile.aiStyle = 4;
            projectile.hostile = true;
            projectile.height = 16;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.tileCollide = false;
            projectile.width = 16;
        }

		public override void Kill(int timeLeft) {
			if (!projectile.active) {
				return;
			}
			projectile.timeLeft = 0;
			{
				Main.PlaySound(SoundID.Item, (int)projectile.position.X, (int)projectile.position.Y, 10);
				for (int num40 = 0; num40 < 20; num40++) {
					var Icicle = ModContent.ProjectileType<EnemySpellIce3Icicle>();
					Projectile.NewProjectile(projectile.position.X + (float)(projectile.width), projectile.position.Y + (float)(projectile.height), 0, 5, Icicle, 75, 3f, projectile.owner);
					Projectile.NewProjectile(projectile.position.X + (float)(projectile.width * 4), projectile.position.Y + (float)(projectile.height * 2), 0, 5, Icicle, 75, 3f, projectile.owner);
					Projectile.NewProjectile(projectile.position.X + (float)(projectile.width * -2), projectile.position.Y + (float)(projectile.height * 2), 0, 5, Icicle, 75, 3f, projectile.owner);
					Vector2 projectilePos = new Vector2(projectile.position.X - projectile.velocity.X, projectile.position.Y - projectile.velocity.Y);
					int num41 = Dust.NewDust(projectilePos, projectile.width, projectile.height, 15, 0f, 0f, 100, default, 2f);
					Main.dust[num41].noGravity = true;
					Main.dust[num41].velocity *= 2f;
					Dust.NewDust(projectilePos, projectile.width, projectile.height, 15, 0f, 0f, 100, default, 1f);
				}
			}
			if (projectile.owner == Main.myPlayer) {
				if (Main.netMode != NetmodeID.SinglePlayer) {
					NetMessage.SendData(MessageID.KillProjectile, -1, -1, null, projectile.identity, (float)projectile.owner, 0f, 0f, 0);
				}
			}
			projectile.active = false;
		}
	}
}
