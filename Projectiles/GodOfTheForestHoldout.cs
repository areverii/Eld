using Eld.Items.Weapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Audio;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;
// using CalamityMod.Sounds;
// using CalamityMod.Particles;
using Eld.Projectiles.BaseProjectiles;
using Steamworks;
using System.Threading;

namespace Eld.Projectiles.Ranged
{
    public class GodOfTheForestHoldout : BaseIdleHoldoutProjectile//, ILocalizedModType
    {
        public new string LocalizationCategory => "Projectiles.Ranged";
        public bool OwnerCanShoot => Owner.HasAmmo(Owner.ActiveItem()) && !Owner.noItems && !Owner.CCed;
        public override int AssociatedItemID => ModContent.ItemType<GodOfTheForest>();
        public override int IntendedProjectileType => ModContent.ProjectileType<GodOfTheForestHoldout>();

        /* LASER SCOPE DEFS */
        public ref float Charge => ref Projectile.ai[0];
        // Stores the max charge before firing, then stores the target post-recoil rotation after firing
        //public ref float MaxChargeOrTargetRotation => ref Projectile.ai[1];
        
        public ref float inRecoil => ref Projectile.ai[1];
        public ref float targetRotation => ref Projectile.ai[2];
        public ref float currentLerp => ref Projectile.localAI[0];
        public const float BaseMaxCharge = 60f;
        public const float MinimumCharge = 18f;
        public float ChargePercent => MathHelper.Clamp(Charge / BaseMaxCharge, 0f, 1f);

        public Vector2 MousePosition => Owner.Eld().mouseWorld - Owner.MountedCenter;
        public const float WeaponLength = 47f;
        public const float MaxSightAngle = MathHelper.Pi * (2f / 3f);

        public const float bullet_speed = 12f;

        public Color ScopeColor => Color.White;

        public override string Texture => "Eld/Projectiles/InvisibleProj";
        public static readonly SoundStyle LargeWeaponFireSound = new("Eld/Sounds/Items/LargeWeaponFire");

        //private ref float inRecoil => ref Projectile.localAI[1];

        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.MaxUpdates = 2;
        }

        public override void SafeAI()
        {
            //Main.NewText("Charge percent" + ChargePercent);
            // Main.NewText("Charge: " + Charge);
            // Main.NewText("MaxChargeOr: " + MaxChargeOrTargetRotation);
            /* New */
            Vector2 armPosition = Owner.RotatedRelativePoint(Owner.MountedCenter, true);
            bool activatingShoot = Main.mouseLeft && !Main.mapFullscreen && !Owner.mouseInterface && Charge >= BaseMaxCharge;
            
            if (Main.myPlayer == Projectile.owner && OwnerCanShoot && activatingShoot)
            {
                //Projectile.localAI[1] = 1;

                //SoundEngine.PlaySound(SoundID.Item82 with { Volume = 1.6f }, Projectile.Center);
                SoundEngine.PlaySound(LargeWeaponFireSound with { Volume = 0.7f }, Projectile.Center);

                Vector2 direction = MousePosition.SafeNormalize(Vector2.UnitX);

                // Apply some knockback to the player
                Owner.velocity += direction * -1.5f;
                Owner.Eld().GeneralScreenShakePower = 6f;
                Projectile.NewProjectile(new EntitySource_ItemUse_WithAmmo(Owner, Owner.HeldItem, -1), Owner.MountedCenter + direction * (WeaponLength + 70f), direction * bullet_speed, ModContent.ProjectileType<EidolonTracer>(), Projectile.damage, Projectile.knockBack, Projectile.owner);

                float recoil = direction.RotatedBy((MathHelper.PiOver4 - MathHelper.Pi) * Owner.direction).ToRotation();
                float initialRecoil = Projectile.rotation.ToRotationVector2().RotatedBy(MathHelper.PiOver4 * -Owner.direction).ToRotation();

                Projectile.rotation = initialRecoil;
                Charge = -1;
                Projectile.ai[2] = recoil;
                //MaxChargeOrTargetRotation = recoil;
                Projectile.ai[1] = 1;
                Projectile.netUpdate = true;
                //return;
            }

            //if (Charge >= -1 && Charge < BaseMaxCharge) {
                //Main.NewText("in recoil");
                //float recoilRotation = UpdateAimPostShotRecoil(MaxChargeOrTargetRotation.ToRotationVector2());
                //Projectile.rotation = newRotation;



                // if (newRotation == MaxChargeOrTargetRotation * Owner.direction) {
                //     Main.NewText("hit target rot");
                //     //Projectile.localAI[1] = 0;
                //     //inRecoil = false;
                //     //activatingShoot = false;
                // }
            //} else {
                
                /* Update regular sprite drawing values */
                //Main.NewText("Not in recoil");
            UpdateProjectileHeldVariables(armPosition);
            //}
            
            ManipulatePlayerVariables();
            
            Charge++;
            if (Charge > BaseMaxCharge)
                Charge = BaseMaxCharge;
            

            // Owner.Eld().mouseWorldListener = true;
            // Projectile.rotation = MousePosition.ToRotation();
            // Vector2 laser_offset = new Vector2(-11, 6);
            // Projectile.Center = Projectile.rotation.ToRotationVector2() * WeaponLength + (Owner.MountedCenter + laser_offset);


            /* End new*/






            

            // // Fire arrows.
            // if (ShootDelay > 0f && Projectile.FinalExtraUpdate())
            // {
            //     float shootCompletionRatio = 1f - ShootDelay / (Owner.ActiveItem().useAnimation - 1f);
            //     float bowAngularOffset = (float)Math.Sin(MathHelper.TwoPi * shootCompletionRatio) * 0.4f;
            //     float damageFactor = Utils.Remap(ChargeTimer, 0f, HeavenlyGale.MaxChargeTime, 1f, HeavenlyGale.MaxChargeDamageBoost);

            //     // Fire arrows.
            //     if (ShootDelay % HeavenlyGale.ArrowShootRate == 0)
            //     {
            //         Vector2 arrowDirection = Projectile.velocity.RotatedBy(bowAngularOffset);

            //         // Release a streak of energy.
            //         Color energyBoltColor = CalamityUtils.MulticolorLerp(shootCompletionRatio, CalamityUtils.ExoPalette);
            //         energyBoltColor = Color.Lerp(energyBoltColor, Color.White, 0.35f);
            //         SquishyLightParticle exoEnergyBolt = new(tipPosition + arrowDirection * 16f, arrowDirection * 4.5f, 0.85f, energyBoltColor, 40, 1f, 5.4f, 4f, 0.08f);
            //         GeneralParticleHandler.SpawnParticle(exoEnergyBolt);

            //         // Update the tip position for one frame.
            //         tipPosition = armPosition + arrowDirection * Projectile.width * 0.45f;

            //         if (Main.myPlayer == Projectile.owner && Owner.HasAmmo(Owner.ActiveItem()))
            //         {
            //             Item heldItem = Owner.ActiveItem();
            //             Owner.PickAmmo(heldItem, out int projectileType, out float shootSpeed, out int damage, out float knockback, out _);
            //             damage = (int)(damage * damageFactor);
            //             projectileType = ModContent.ProjectileType<ExoCrystalArrow>();

            //             bool createLightning = ChargeTimer / HeavenlyGale.MaxChargeTime >= HeavenlyGale.ChargeLightningCreationThreshold;
            //             Vector2 arrowVelocity = arrowDirection * shootSpeed;
            //             Projectile.NewProjectile(Projectile.GetSource_FromThis(), tipPosition, arrowVelocity, projectileType, damage, knockback, Projectile.owner, createLightning.ToInt());
            //         }
            //     }

            //     ShootDelay--;
            //     if (ShootDelay <= 0f)
            //         ChargeTimer = 0f;
            // }

            // // Create orange exo energy at the tip of the bow.
            // Color energyColor = Color.Orange;
            // Vector2 verticalOffset = Vector2.UnitY.RotatedBy(Projectile.rotation) * 8f;
            // if (Math.Cos(Projectile.rotation) < 0f)
            //     verticalOffset *= -1f;

            // if (Main.rand.NextBool(4))
            // {
            //     SquishyLightParticle exoEnergy = new(tipPosition + verticalOffset, -Vector2.UnitY.RotatedByRandom(0.39f) * Main.rand.NextFloat(0.4f, 1.6f), 0.28f, energyColor, 25);
            //     GeneralParticleHandler.SpawnParticle(exoEnergy);
            // }

            // // Create light at the tip of the bow.
            // DelegateMethods.v3_1 = energyColor.ToVector3();
            // Utils.PlotTileLine(tipPosition - verticalOffset, tipPosition + verticalOffset, 10f, DelegateMethods.CastLightOpen);
            // Lighting.AddLight(tipPosition, energyColor.ToVector3());

            // // Create a puff of energy in a star shape and play a sound to indicate that the bow is at max charge.
            // if (ShootDelay <= 0)
            //     ChargeTimer++;
            // if (ChargeTimer == HeavenlyGale.MaxChargeTime)
            // {
            //     SoundEngine.PlaySound(SoundID.Item158 with { Volume = 1.6f }, Projectile.Center);
            //     for (int i = 0; i < 75; i++)
            //     {
            //         float offsetAngle = MathHelper.TwoPi * i / 75f;

            //         // Parametric equations for an asteroid.
            //         float unitOffsetX = (float)Math.Pow(Math.Cos(offsetAngle), 3D);
            //         float unitOffsetY = (float)Math.Pow(Math.Sin(offsetAngle), 3D);

            //         Vector2 puffDustVelocity = new Vector2(unitOffsetX, unitOffsetY) * 5f;
            //         Dust magic = Dust.NewDustPerfect(tipPosition, 267, puffDustVelocity);
            //         magic.scale = 1.8f;
            //         magic.fadeIn = 0.5f;
            //         magic.color = CalamityUtils.MulticolorLerp(i / 75f, CalamityUtils.ExoPalette);
            //         magic.noGravity = true;
            //     }
            //     ChargeTimer++;
            // }

            /* LASER AI */
            //Owner.Eld().mouseWorldListener = true;






















            /*
            if (Charge != -1)
            {
                // Only let the owner do this
                if (Projectile.owner != Main.myPlayer)
                    return;

                // Increment the charge and set the projectile's properties
                Charge++;
                Projectile.rotation = MousePosition.ToRotation();
                Vector2 laser_offset = new Vector2(-11, 6);
                Projectile.Center = Projectile.rotation.ToRotationVector2() * WeaponLength + (Owner.MountedCenter + laser_offset);

                // Set the player's properties
                //Owner.heldProj = Projectile.whoAmI;
                //Owner.ChangeDir(MousePosition.X >= 0 ? 1 : -1);
                //Owner.itemRotation = (MousePosition * Owner.direction).ToRotation();

                // Re-increment use time so that the post-shot delay is consistent with speed increases/decreases
                Owner.itemTime++;
                Owner.itemAnimation++;
                Projectile.timeLeft = Owner.itemAnimation;

                // Play a sound to let the player know they're at max charge
                if (Charge == MaxChargeOrTargetRotation)
                    SoundEngine.PlaySound(SoundID.Item82 with { Volume = SoundID.Item82.Volume * 0.7f }, Owner.MountedCenter);
                
                // Idly emit particles every other frame while at max charge
                // if (ChargePercent == 1f && Charge % 2 == 0)
                // {
                //     Vector2 direction = MousePosition.SafeNormalize(Vector2.UnitX);
                //     Vector2 sparkVelocity = direction.RotatedBy(Main.rand.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4)) * 6f;
                //     CritSpark spark = new CritSpark(Owner.MountedCenter + direction * WeaponLength, sparkVelocity + Owner.velocity, Color.White, Color.LightBlue, 1f, 16);
                //     GeneralParticleHandler.SpawnParticle(spark);
                // }

                // Sync the projectile in MP
                Projectile.netUpdate = true;
            }
            else
            {
            // On the first frame of firing
            if (Projectile.owner == Main.myPlayer && Main.mouseLeft)
            {
                Main.NewText("shot");
                // Don't fire and immediately reset cooldowns if minimum charge isn't met
                if (Charge < MinimumCharge)
                {
                    Projectile.netUpdate = true;
                    Projectile.Kill();
                    Owner.itemTime = 1;
                    Owner.itemAnimation = 1;
                    return;
                }

                // Calculate the direction of the shot
                Vector2 direction = MousePosition.SafeNormalize(Vector2.UnitX);

                // Apply some recoil to the player
                Owner.velocity += direction * (ChargePercent * -3f);
                //Owner.Eld().GeneralScreenShakePower = 4f * ChargePercent;

                // Spawn the bullet
                //int shotDamage = (int)(Projectile.damage * ChargePercent);
                Projectile.NewProjectile(new EntitySource_ItemUse_WithAmmo(Owner, Owner.HeldItem, -1), Owner.MountedCenter + direction * (WeaponLength + 70f), direction * bullet_speed, ModContent.ProjectileType<EidolonTracer>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                


                // Calculate the target end recoil and initial recoil based off of charge level
                // Initial recoil is manually set to give a smoother recoil animation
                float recoil = direction.RotatedBy((MathHelper.PiOver4 - MathHelper.Pi) * ChargePercent * Owner.direction).ToRotation();
                //float initialRecoil = Owner.itemRotation.ToRotationVector2().RotatedBy(MathHelper.PiOver4 * -Owner.direction * ChargePercent).ToRotation();
                float initialRecoil = Projectile.rotation.ToRotationVector2().RotatedBy(MathHelper.PiOver4 * -Owner.direction * ChargePercent).ToRotation();

                // Spawn a ring particle based off charge percent to emphasize the impact
                float originalScale = 0.2f * ChargePercent;
                float maxScale = 1f * ChargePercent;
                //Particle pulse = new DirectionalPulseRing(Owner.MountedCenter + direction * WeaponLength, Vector2.Zero, Color.White, new Vector2(0.5f, 1f), direction.ToRotation(), originalScale, maxScale, 30);
                //GeneralParticleHandler.SpawnParticle(pulse);

                // Play a sound with volume scaling with charge percent
                SoundEngine.PlaySound(SoundID.Item62 with { Volume = SoundID.Item62.Volume * ChargePercent }, Owner.MountedCenter);

                // Summon Luxor's gift manually, as channeling tends to be.... unbalanced, to say the least, if you don't
                // if (Owner.Calamity().luxorsGift)
                // {
                //     double rangedDamage = shotDamage * 0.15;
                //     if (rangedDamage >= 1D)
                //     {
                //         float speed = 24f * ChargePercent;
                //         int projectile = Projectile.NewProjectile(new EntitySource_ItemUse_WithAmmo(Owner, Owner.HeldItem, -1), Projectile.Center, direction * speed, ModContent.ProjectileType<LuxorsGiftRanged>(), (int)rangedDamage, 0f, Projectile.owner);
                //         if (projectile.WithinBounds(Main.maxProjectiles))
                //             Main.projectile[projectile].DamageType = DamageClass.Generic;
                //     }
                // }

                // Set the initial recoil, mark the projectile as fired, and set the target recoil
                Projectile.rotation = initialRecoil;
                Charge = -1;
                MaxChargeOrTargetRotation = recoil;
                Projectile.netUpdate = true;
                return;

            } else {
                Main.NewText("otherwise");
                Vector2 armPosition = Owner.RotatedRelativePoint(Owner.MountedCenter, true);
                // Vector2 tipPosition = armPosition + Projectile.velocity * Projectile.width * 0.45f;

                // // Activate shot behavior if the owner stops channeling or otherwise cannot use the weapon.
                // bool activatingShoot = ShootDelay <= 0 && Main.mouseLeft && !Main.mapFullscreen && !Owner.mouseInterface;
                // if (Main.myPlayer == Projectile.owner && OwnerCanShoot && activatingShoot)
                // {
                //     SoundEngine.PlaySound(HeavenlyGale.FireSound, Projectile.Center);
                //     ShootDelay = Owner.ActiveItem().useAnimation;
                //     Projectile.netUpdate = true;
                // }

                // // Update damage based on current ranged damage stat, since this projectile exists regardless of if it's being fired.
                Projectile.damage = Owner.ActiveItem() is null ? 0 : Owner.GetWeaponDamage(Owner.ActiveItem());

                UpdateProjectileHeldVariables(armPosition);
                ManipulatePlayerVariables();
            }

            // Step the player's target rotation towards the max recoil
            float newRotation = UpdateAimPostShotRecoil(MaxChargeOrTargetRotation.ToRotationVector2());
            //Owner.heldProj = Projectile.whoAmI;
            Projectile.rotation = newRotation;
            } */
        }
        private float NormalizeAngle(float angle)
        {
            angle %= MathHelper.TwoPi; // Modulo 2π
            if (angle > Math.PI)
                angle -= MathHelper.TwoPi;
            else if (angle <= -Math.PI)
                angle += MathHelper.TwoPi;
            return angle;
        }

        // Function to check if the rotation is close enough to the target
        private bool IsRotationClose(float currentRotation, float targetRotation, float threshold = 0.01f)
        {
            currentRotation = NormalizeAngle(currentRotation);
            targetRotation = NormalizeAngle(targetRotation);

            // Calculate the absolute difference
            float difference = Math.Abs(currentRotation - targetRotation);

            // Check if the difference is within the threshold
            return difference <= threshold;
        }

        // Gently adjusts the aim vector of the cannon to point towards the mouse.
        private float UpdateAimPostShotRecoil(Vector2 target) => Vector2.Lerp(target * Owner.direction, Projectile.rotation.ToRotationVector2(), 0.825f).ToRotation();

        public void UpdateProjectileHeldVariables(Vector2 armPosition)
        {
            // If the projectile has already fired, don't update the held variables?

            if (Main.myPlayer == Projectile.owner)
            {
                float aimInterpolant = Utils.GetLerpValue(10f, 40f, Projectile.Distance(Main.MouseWorld), true);
                Vector2 oldVelocity = Projectile.velocity;
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Projectile.SafeDirectionTo(Main.MouseWorld), aimInterpolant);
                if (Projectile.velocity != oldVelocity)
                {
                    Projectile.netSpam = 0;
                    Projectile.netUpdate = true;
                }
            }

            Projectile.position = armPosition - Projectile.Size * 0.5f + Projectile.velocity.SafeNormalize(Vector2.UnitY) * 44f;
            
            // Add recoil to rotation if in recoil
            // if (Charge >= -1 && Charge < BaseMaxCharge)
            // {
            //     float recoilRotation = UpdateAimPostShotRecoil(MaxChargeOrTargetRotation.ToRotationVector2());
            //     Projectile.rotation += recoilRotation;
            // }
            // else
            // {
            //     Projectile.rotation = Projectile.velocity.ToRotation();
            // }
            // if (Charge >= -1 && Charge < BaseMaxCharge)
            // {
            //     float recoilRotation = UpdateAimPostShotRecoil(MaxChargeOrTargetRotation.ToRotationVector2());
            //     Projectile.rotation += recoilRotation;
            // }
            // else
            // {
            //     Projectile.rotation = Projectile.velocity.ToRotation();
            // }
            if (Projectile.ai[1] == 1) {
                //Main.NewText("UPDATING ROT FOR RECOIL");
                //float recoilRotation = UpdateAimPostShotRecoil(targetRotation.ToRotationVector2());  
                //Projectile.rotation = Projectile.velocity.ToRotation() + recoilRotation;
                Projectile.rotation = Projectile.velocity.ToRotation();

                //Projectile.localAI[0] = UpdateAimPostShotRecoil(Projectile.ai[2].ToRotationVector2());
                //Projectile.rotation = currentLerp;
                Projectile.rotation = UpdateAimPostShotRecoil(Projectile.ai[2].ToRotationVector2());

                //if (Projectile.rotation == targetRotation * Owner.direction) {
                if (IsRotationClose(Projectile.rotation, targetRotation)) {
                    Main.NewText("hit target rot");
                    //Projectile.localAI[0] = UpdateAimPostShotRecoil(Projectile.velocity.ToRotation().ToRotationVector2());
                    //Projectile.rotation = currentLerp;
                    Projectile.rotation = UpdateAimPostShotRecoil(Projectile.velocity.ToRotation().ToRotationVector2());
                    //inRecoil = 0;
                }
                //if (Projectile.rotation == Projectile.velocity.ToRotation() * Owner.direction) {
                if (IsRotationClose(Projectile.rotation, Projectile.velocity.ToRotation())) {
                    Main.NewText("finished");
                    Projectile.ai[1] = 0;
                }


            } else {
                Projectile.rotation = Projectile.velocity.ToRotation();
            }

            
            Projectile.spriteDirection = Projectile.direction;
            Projectile.timeLeft = 2;
        }

        public void ManipulatePlayerVariables()
        {
            Owner.ChangeDir(Projectile.direction);
            //Owner.heldProj = Projectile.whoAmI;

            // // Make the player lower their front arm a bit to indicate the pulling of the string.
            // // This is precisely calculated by representing the top half of the string as a right triangle and using SOH-CAH-TOA to
            // // calculate the respective angle from the appropriate widths and heights.
            //float frontArmRotation = (float)Math.Atan(StringHalfHeight / MathHelper.Max(StringReelbackDistance, 0.001f) * 0.5f);
            float frontArmRotation = -300f;
            if (Owner.direction == -1)
                frontArmRotation += MathHelper.PiOver4;
            else
                frontArmRotation = MathHelper.PiOver2 - frontArmRotation;
            frontArmRotation += Projectile.rotation + MathHelper.Pi + Owner.direction * MathHelper.PiOver2 + 0.12f;
            Owner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, frontArmRotation);
            // Owner.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, Projectile.velocity.ToRotation() - MathHelper.PiOver2);
            // General arm rotation for holding the weapon
            
        }

        /* PREDRAW is responsible for simply drawing the sprite when it is pulled out*/
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D this_texture = ModContent.Request<Texture2D>("Eld/Projectiles/GodOfTheForestHoldout").Value;
            Texture2D textureGlow = ModContent.Request<Texture2D>("Eld/Projectiles/GodOfTheForestHoldoutGlow").Value;
            Vector2 origin = this_texture.Size() * 0.5f;
            Vector2 drawPosition = Projectile.Center - Main.screenPosition;

            // // float chargeOffset = ChargeupInterpolant * Projectile.scale * 3f;
            // // Color chargeColor = Color.Lerp(Color.Lime, Color.Cyan, (float)Math.Cos(Main.GlobalTimeWrappedHourly * 7.1f) * 0.5f + 0.5f) * ChargeupInterpolant * 0.6f;
            // // chargeColor.A = 0;

            float rotation = Projectile.rotation;
            SpriteEffects direction = SpriteEffects.None;
            if (Math.Cos(rotation) < 0f)
            {
                direction = SpriteEffects.FlipHorizontally;
                rotation += MathHelper.Pi;
            }

            // // // Draw a backglow effect as an indicator of charge.
            // // for (int i = 0; i < 8; i++)
            // // {
            // //     Vector2 drawOffset = (MathHelper.TwoPi * i / 8f).ToRotationVector2() * chargeOffset;
            // //     Main.spriteBatch.Draw(texture, drawPosition + drawOffset, null, chargeColor, rotation, origin, Projectile.scale, direction, 0f);
            // // }
            Main.spriteBatch.Draw(this_texture, drawPosition, null, Projectile.GetAlpha(lightColor), rotation, origin, Projectile.scale, direction, 0f);
            Main.spriteBatch.Draw(textureGlow, drawPosition, null, Color.White, rotation, origin, Projectile.scale, direction, 0f);

            // /* LASER SIGHT PREDRAW */

            // // If the projectile has already fired, don't draw sights
            // if (Charge >= -1 && Charge < BaseMaxCharge)
            //     return false;

            float sightsSize = 1800;
            float sightsResolution = MathHelper.Lerp(0.04f, 0.2f, Math.Min(ChargePercent * 1.5f, 1));

            // // Converge the sights
            // // float spread = (1f - ChargePercent) * MaxSightAngle;
            // // float halfAngle = spread / 2f;
            Texture2D texture = ModContent.Request<Texture2D>("Eld/Projectiles/InvisibleProj").Value;

            Color sightsColor = Color.Lerp(Color.LightBlue, Color.SeaGreen, ChargePercent);

            // // //Setup the spread gradient effect.
            // // Effect spreadEffect = Filters.Scene["CalamityMod:SpreadTelegraph"].GetShader().Shader;
            // // spreadEffect.Parameters["centerOpacity"].SetValue(0.9f);
            // // spreadEffect.Parameters["mainOpacity"].SetValue(ChargePercent);
            // // spreadEffect.Parameters["halfSpreadAngle"].SetValue(halfAngle);
            // // spreadEffect.Parameters["edgeColor"].SetValue(sightsColor.ToVector3());
            // // spreadEffect.Parameters["centerColor"].SetValue(sightsColor.ToVector3());
            // // spreadEffect.Parameters["edgeBlendLength"].SetValue(0.07f);
            // // spreadEffect.Parameters["edgeBlendStrength"].SetValue(8f);

            // // Main.spriteBatch.End();
            // //Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, spreadEffect, Main.GameViewMatrix.TransformationMatrix);
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, new Vector2(texture.Width / 2f, texture.Height / 2f), sightsSize, 0, 0);

            // //Setup the laser sights effect.
            Effect laserScopeEffect = Filters.Scene["Eld:PixelatedSightLine"].GetShader().Shader;
            laserScopeEffect.Parameters["sampleTexture2"].SetValue(ModContent.Request<Texture2D>("Eld/ExtraTextures/GrayscaleGradients/CertifiedCrustyNoise").Value);
            laserScopeEffect.Parameters["noiseOffset"].SetValue(Main.GameUpdateCount * -0.003f); 
            laserScopeEffect.Parameters["mainOpacity"].SetValue(ChargePercent); //Opacity increases as the gun charges
            laserScopeEffect.Parameters["Resolution"].SetValue(new Vector2(sightsResolution * sightsSize));
            laserScopeEffect.Parameters["laserAngle"].SetValue(-Projectile.rotation);
            laserScopeEffect.Parameters["laserWidth"].SetValue(0.001f);
            //laserScopeEffect.Parameters["laserWidth"].SetValue(0.0001f + (float)Math.Pow(ChargePercent, 5) * ((float)Math.Sin(Main.GlobalTimeWrappedHourly * 3f) * 0.002f + 0.002f));
            laserScopeEffect.Parameters["laserLightStrenght"].SetValue(7f);

            laserScopeEffect.Parameters["color"].SetValue(sightsColor.ToVector3());
            laserScopeEffect.Parameters["darkerColor"].SetValue(Color.Black.ToVector3());
            laserScopeEffect.Parameters["bloomSize"].SetValue(0.06f);
            laserScopeEffect.Parameters["bloomMaxOpacity"].SetValue(0.4f);
            laserScopeEffect.Parameters["bloomFadeStrenght"].SetValue(7f);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, laserScopeEffect, Main.GameViewMatrix.TransformationMatrix);

            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, null, Color.White, 0, new Vector2(texture.Width / 2f, texture.Height / 2f), sightsSize, 0, 0);

            laserScopeEffect.Parameters["laserAngle"].SetValue(-Projectile.rotation);
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, null, Color.White, 0, new Vector2(texture.Width / 2f, texture.Height / 2f), sightsSize, 0, 0);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);


            return false;
        }

        // The gun itself should not do contact damage.
        public override bool? CanDamage() => false;
    }
}
