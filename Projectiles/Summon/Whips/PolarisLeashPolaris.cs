using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace tsorcRevamp.Projectiles.Summon.Whips
{

	public class PolarisLeashPolaris : ModProjectile
	{

		public override void SetDefaults()
		{

			Projectile.CloneDefaults(ProjectileID.CoolWhipProj);

			AIType = ProjectileID.CoolWhipProj;
			Projectile.width = 22;
			Projectile.height = 18;
			Projectile.tileCollide = false;
			Projectile.DamageType = DamageClass.SummonMeleeSpeed;
			Projectile.friendly = true;
		}

		public override void AI()
		{
			Projectile.position = Main.MouseWorld;
			Player player = Main.player[Projectile.owner];
			if (player.dead || !player.active)
			{
				player.ClearBuff(ModContent.BuffType<Buffs.Summon.PolarisLeashBuff>());
			}
			if (player.HasBuff(ModContent.BuffType<Buffs.Summon.PolarisLeashBuff>()))
			{
				Projectile.timeLeft = 2;
			}
			Dust.NewDust(Projectile.Center, 10, 10, DustID.IceRod, 0f, 0f, 150, default(Color), 1f);
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<Buffs.Summon.WhipDebuffs.PolarisLeashDebuff>(), 240);
			Main.player[Projectile.owner].MinionAttackTargetNPC = target.whoAmI;
		}
	}
}
