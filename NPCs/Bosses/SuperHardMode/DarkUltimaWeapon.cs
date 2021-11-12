using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace tsorcRevamp.NPCs.Bosses.SuperHardMode
{
    class DarkUltimaWeapon : ModNPC
    {
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.width = 12;
            npc.height = 12;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.dontTakeDamage = true;
            npc.lifeMax = 500000;
            npc.scale = 1.2f;
            npc.damage = DarkCloud.swordDamage;
            npc.behindTiles = false;
            AttackModeCounter = 3;            
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Ultima Weapon");
            NPCID.Sets.TrailCacheLength[npc.type] = 6;
            NPCID.Sets.TrailingMode[npc.type] = 1;
            NPCID.Sets.NeedsExpertScaling[npc.type] = false;
        }
        public NPC HolderDarkCloud
        {
            get => Main.npc[(int)npc.ai[0]];
            set => Main.npc[(int)npc.ai[0]] = value;
        }
        public float AttackModeCounter
        {
            get => npc.ai[1];
            set => npc.ai[1] = value;
        }
        public Player Target
        {
            get => Main.player[HolderDarkCloud.target];
        }


        //Yet again this is timer based instead of state based.
        //Huge mess, but for now it works.
        float[] trailRotations = new float[6] { 0, 0, 0, 0, 0, 0 };
        bool spawnedSubProjectiles = false;
        public override void AI()
        {
            if(HolderDarkCloud.active == false)
            {
                npc.active = false;
            }

            if (npc.ai[2] == 0)
            {
                npc.rotation = MathHelper.ToRadians(-20);

                if (spawnedSubProjectiles == false)
                {
                    //These projectiles track the sword in a line formation and do the actual damage, because fuck getting both the sword hitbox *and* visuals both right at the same time
                    //Also, this makes the hitbox fit the sprite *way* better than an enormous square
                    //One projectile sits on the hilt, and the other sits at the end of the sword
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            Projectile.NewProjectileDirect(npc.position, Vector2.Zero, ModContent.ProjectileType<Projectiles.Enemy.DarkCloud.DarkUltimaWeaponDummyProjectile>(), DarkCloud.swordDamage, 0.5f, Main.myPlayer, npc.whoAmI, i);
                        }
                    }

                    spawnedSubProjectiles = true;
                }

                if (HolderDarkCloud == null || HolderDarkCloud.active == false)
                {
                    npc.active = false;
                }

                //Always stay in Dark Cloud's hand except for during the throw attack
                if (AttackModeCounter < 120 || AttackModeCounter > 240)
                {
                    InHand();
                }

                //Swing
                if (AttackModeCounter > 60 && AttackModeCounter < 80)
                {
                    Swing(AttackModeCounter - 60, 20);
                }
                if (AttackModeCounter >= 80 && AttackModeCounter <= 120)
                {
                    DownAngle();
                }

                //Launch toward the player
                if (AttackModeCounter == 120)
                {
                    npc.velocity = UsefulFunctions.GenerateTargetingVector(npc.Center + new Vector2(0, -62), Target.Center, 25);
                }

                //Rotate as it flies
                if (AttackModeCounter > 120 && AttackModeCounter < 180)
                {
                    npc.rotation += 0.05f * AttackModeCounter;
                }

                //Launch back toward Dark Cloud
                if (AttackModeCounter > 180 && AttackModeCounter < 240)
                {
                    //If not close to the Dark Cloud, accelerate toward it until close enough to teleport into its hands.
                    if (Vector2.Distance(npc.Center, HolderDarkCloud.Center) > 100)
                    {
                        npc.velocity = UsefulFunctions.GenerateTargetingVector(npc.Center, HolderDarkCloud.Center, 25);
                    }
                    else
                    {
                        InHand();
                    }
                }

                //Swing as dark cloud falls
                if (AttackModeCounter >= 240 && AttackModeCounter < 300)
                {
                    int rotProgress = (int)AttackModeCounter;
                    if (rotProgress > 280)
                    {
                        rotProgress = 280;
                    }
                    Swing(rotProgress - 240, 40);
                }


                float projSpeed = 9;
                //Swing as dark cloud fires off a projectile
                if (AttackModeCounter >= 300 && AttackModeCounter < 530)
                {
                    npc.rotation = UsefulFunctions.GenerateTargetingVector(npc.Center, Target.Center, 1).ToRotation() + MathHelper.ToRadians(45);
                }
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (AttackModeCounter == 302)
                    {
                        Projectile.NewProjectileDirect(npc.Center + new Vector2(0, -62), UsefulFunctions.GenerateTargetingVector(npc.Center, Target.Center + new Vector2(0, -62), projSpeed).RotatedBy(MathHelper.ToRadians(45)), ModContent.ProjectileType<Projectiles.Enemy.DarkCloud.DarkWave>(), DarkCloud.darkSlashDamage, 0.5f, Main.myPlayer);
                    }
                    if (AttackModeCounter == 305)
                    {
                        Projectile.NewProjectileDirect(npc.Center + new Vector2(0, -62), UsefulFunctions.GenerateTargetingVector(npc.Center, Target.Center + new Vector2(0, -62), projSpeed), ModContent.ProjectileType<Projectiles.Enemy.DarkCloud.DarkWave>(), DarkCloud.darkSlashDamage, 0.5f, Main.myPlayer);
                    }
                    if (AttackModeCounter == 308)
                    {
                        Projectile.NewProjectileDirect(npc.Center + new Vector2(0, -62), UsefulFunctions.GenerateTargetingVector(npc.Center, Target.Center + new Vector2(0, -62), projSpeed).RotatedBy(MathHelper.ToRadians(-45)), ModContent.ProjectileType<Projectiles.Enemy.DarkCloud.DarkWave>(), DarkCloud.darkSlashDamage, 0.5f, Main.myPlayer);
                    }

                    if (AttackModeCounter == 422)
                    {
                        Projectile.NewProjectileDirect(npc.Center + new Vector2(0, -62), UsefulFunctions.GenerateTargetingVector(npc.Center, Target.Center + new Vector2(0, -62), projSpeed).RotatedBy(MathHelper.ToRadians(90)), ModContent.ProjectileType<Projectiles.Enemy.DarkCloud.DarkWave>(), DarkCloud.darkSlashDamage, 0.5f, Main.myPlayer);
                    }
                    if (AttackModeCounter == 425)
                    {
                        Projectile.NewProjectileDirect(npc.Center + new Vector2(0, -62), UsefulFunctions.GenerateTargetingVector(npc.Center, Target.Center + new Vector2(0, -62), projSpeed).RotatedBy(MathHelper.ToRadians(45)), ModContent.ProjectileType<Projectiles.Enemy.DarkCloud.DarkWave>(), DarkCloud.darkSlashDamage, 0.5f, Main.myPlayer);
                    }
                    if (AttackModeCounter == 428)
                    {
                        Projectile.NewProjectileDirect(npc.Center + new Vector2(0, -62), UsefulFunctions.GenerateTargetingVector(npc.Center, Target.Center + new Vector2(0, -62), projSpeed), ModContent.ProjectileType<Projectiles.Enemy.DarkCloud.DarkWave>(), DarkCloud.darkSlashDamage, 0.5f, Main.myPlayer);
                    }

                    if (AttackModeCounter == 522)
                    {
                        Projectile.NewProjectileDirect(npc.Center + new Vector2(0, -62), UsefulFunctions.GenerateTargetingVector(npc.Center, Target.Center + new Vector2(0, -62), projSpeed).RotatedBy(MathHelper.ToRadians(45)), ModContent.ProjectileType<Projectiles.Enemy.DarkCloud.DarkWave>(), DarkCloud.darkSlashDamage, 0.5f, Main.myPlayer);
                    }
                    if (AttackModeCounter == 525)
                    {
                        Projectile.NewProjectileDirect(npc.Center + new Vector2(0, -62), UsefulFunctions.GenerateTargetingVector(npc.Center, Target.Center + new Vector2(0, -62), projSpeed), ModContent.ProjectileType<Projectiles.Enemy.DarkCloud.DarkWave>(), DarkCloud.darkSlashDamage, 0.5f, Main.myPlayer);
                    }
                    if (AttackModeCounter == 528)
                    {
                        Projectile.NewProjectileDirect(npc.Center + new Vector2(0, -62), UsefulFunctions.GenerateTargetingVector(npc.Center, Target.Center + new Vector2(0, -62), projSpeed).RotatedBy(MathHelper.ToRadians(-45)), ModContent.ProjectileType<Projectiles.Enemy.DarkCloud.DarkWave>(), DarkCloud.darkSlashDamage, 0.5f, Main.myPlayer);
                    }
                }

                //Point straight down for the final falling attack
                if (AttackModeCounter >= 600)
                {
                    npc.rotation = MathHelper.ToRadians(135);
                }

                //End the attack phase
                if (AttackModeCounter == 750)
                {
                    npc.active = false;
                    //Maybe add dust?
                }

                
                AttackModeCounter++;
            }


            if (npc.ai[2] == DarkCloud.DarkCloudAttackID.TeleportingSlashes)
            {
                if (spawnedSubProjectiles == false)
                {
                    //These projectiles track the sword in a line formation and do the actual damage, because fuck getting both the sword hitbox *and* visuals both right at the same time
                    //Also, this makes the hitbox fit the sprite *way* better than an enormous square
                    //One projectile sits on the hilt, and the other sits at the end of the sword
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            Projectile.NewProjectileDirect(npc.position, Vector2.Zero, ModContent.ProjectileType<Projectiles.Enemy.DarkCloud.DarkUltimaWeaponDummyProjectile>(), DarkCloud.swordDamage, 0.5f, Main.myPlayer, npc.whoAmI, i);
                        }
                    }

                    spawnedSubProjectiles = true;
                }
                AttackModeCounter++;
                if(HolderDarkCloud.Center.X < Target.Center.X)
                {
                    npc.rotation = MathHelper.ToRadians(-20);
                    npc.Center = HolderDarkCloud.Center + new Vector2(0, 55);
                }
                else
                {
                    npc.rotation = MathHelper.ToRadians(-70);

                    npc.Center = HolderDarkCloud.Center + new Vector2(10, 55);
                }
                
                if (AttackModeCounter == 640)
                {                    
                    npc.active = false;
                }
            }



            for (int i = 5; i > 0; i--)
            {
                trailRotations[i] = trailRotations[i - 1];
            }
            trailRotations[0] = npc.rotation;
        }
        void InHand()
        {
            npc.direction = HolderDarkCloud.direction;
            Vector2 offset = new Vector2(0, 45);          

            if (AttackModeCounter < 120)
            {
                offset.Y = 55;
            }
            if (AttackModeCounter > 120)
            {
                offset.Y = 75;
            }
            
            npc.Center = HolderDarkCloud.Center + offset;
        }

        void DownAngle()
        {
            npc.rotation = MathHelper.ToRadians(110);
            if (npc.direction == -1)
            {
                npc.rotation = MathHelper.ToRadians(170);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = TransparentTextureHandler.TransparentTextures[TransparentTextureHandler.TransparentTextureType.DarkUltimaWeapon];
            Texture2D glowTexture = TransparentTextureHandler.TransparentTextures[TransparentTextureHandler.TransparentTextureType.DarkUltimaWeaponGlowmask];
            Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height / Main.npcFrameCount[npc.type]);
            Vector2 origin = sourceRectangle.Size() / 2f;
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (npc.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }

            Vector2 offset = new Vector2(0, 45);

            //Draw shadow trails
            for (float i = NPCID.Sets.TrailCacheLength[npc.type] - 1; i >= 0; i--)
            {
                Main.spriteBatch.Draw(texture, npc.oldPos[(int)i] - Main.screenPosition - offset, sourceRectangle, drawColor * ((6 - i) / 6), trailRotations[(int)i], origin, npc.scale, spriteEffects, 0f);
                Main.spriteBatch.Draw(glowTexture, npc.oldPos[(int)i] - Main.screenPosition - offset, sourceRectangle, Color.White * ((6 - i) / 6), trailRotations[(int)i], origin, npc.scale, spriteEffects, 0f);
            }

            //Draw actual npc
            Main.spriteBatch.Draw(texture, npc.position - Main.screenPosition - offset, sourceRectangle, drawColor, npc.rotation, origin, npc.scale, spriteEffects, 0f);
            Main.spriteBatch.Draw(glowTexture, npc.position - Main.screenPosition - offset, sourceRectangle, Color.White, npc.rotation, origin, npc.scale, spriteEffects, 0f);

            return false;
        }

        //Make the projectile swing.
        void Swing(float progress, float maxProgress)
        {
            npc.rotation = MathHelper.ToRadians(-20) + (MathHelper.ToRadians(130) * (progress / maxProgress));
            if (npc.direction == -1)
            {
                npc.rotation = MathHelper.ToRadians(180) + MathHelper.ToRadians(-20) + (MathHelper.ToRadians(130) * ((maxProgress - progress) / maxProgress));
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}