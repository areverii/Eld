// using Eld.Projectiles;
// using Microsoft.Xna.Framework;
// using Terraria;
// using Terraria.Audio;
// using Terraria.ID;
// using Terraria.ModLoader;
// using System.IO;
// using Terraria.DataStructures;

// namespace Eld.Items.Weapons
// {
// 	public class OldForestGod : ModItem
// 	{

// 		public int mode = 0;
// 		public int hunter_mark_stack = 0;
// 		public static float hunter_mark_bonus = 100f;
// 		public static int max_magazine = 5;
// 		public int magazine = max_magazine;

// 		public static Color OverrideColor = new(122, 173, 255);

// 		//public override string Texture => "Terraria/Images/Item_" + ItemID.LastPrism;

// 		public override void SetDefaults() {
// 			// Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

// 			Item.CloneDefaults(ItemID.LastPrism);
// 			Item.mana = 0;
// 			Item.shoot = ModContent.ProjectileType<EidolonHoldout>();
// 			Item.shootSpeed = 30f;

// 			// Change the item's draw color so that it is visually distinct from the vanilla Last Prism.
// 			Item.color = OverrideColor;



// 			// Common Properties
// 			Item.width = 49; // Hitbox width of the item.
// 			Item.height = 25; // Hitbox height of the item.
// 			Item.scale = 1.1f;
// 			Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.

// 			// Use Properties
// 			Item.useTime = 20; // The item's use time in ticks (60 ticks == 1 second.)
// 			Item.useAnimation = 20; // The length of the item's use animation in ticks (60 ticks == 1 second.)
// 			Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
// 			Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

// 			// The sound that this item plays when used.
// 			// Item.UseSound = new SoundStyle($"{nameof(Eld)}/Assets/Sounds/Items/Guns/ExampleGun") {
// 			// 	Volume = 0.9f,
// 			// 	PitchVariance = 0.2f,
// 			// 	MaxInstances = 3,
// 			// };
// 			Item.UseSound = SoundID.Item40;

// 			// Weapon Properties
// 			Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
// 			Item.damage = 3000; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
// 			Item.knockBack = 12f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
// 			Item.noMelee = true; // So the item's animation doesn't do damage.

// 			// Gun Properties
// 			Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
// 			Item.shootSpeed = 50f; // The speed of the projectile (measured in pixels per frame.)
// 			//Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.
// 		}


// 		// public override void NetSend(BinaryWriter writer) {
// 		// 	writer.Write((byte)mode);
// 		// 	writer.Write((byte)hunter_mark_stack);
// 		// 	writer.Write((byte)magazine);
// 		// }

// 		// public override void NetReceive(BinaryReader reader) {
// 		// 	mode = reader.ReadByte();
// 		// 	hunter_mark_stack = reader.ReadByte();
// 		// 	magazine = reader.ReadByte();
// 		// }

// 		// public override bool AltFunctionUse(Player player) {
// 		// 	return true;
// 		// }

//         public override bool CanUseItem(Player player)
//         {
//             return player.ownedProjectileCounts[ModContent.ProjectileType<EidolonHoldout>()] <= 0;
//         }


//         // public override bool CanUseItem(Player player)
//         // {
//         // 	if (player.ownedProjectileCounts[ModContent.ProjectileType<EidolonHoldout>()] > 0) {
//         // 		return false;
//         // 	}

//         // 	if (player.altFunctionUse == 2) {
//         // 			Main.NewText($"Reload");
//         // 			magazine = max_magazine;
//         // 			hunter_mark_stack = 0;
//         // 			mode = 0;
//         // 			return false;
//         // 	}

//         //     if (mode == 2) {
//         // 		Main.NewText($"Need to reload!");
//         // 		return false;
//         // 	}

//         // 	return true;
//         // }

//         // public override bool? UseItem(Player player)
//         // {
//         //     if (player.whoAmI != Main.myPlayer) {
//         // 		return true;
//         // 	}

//         // 	if (player.altFunctionUse == 2) {

//         // 	}
//         // 	else {
//         // 		switch (mode) {
//         // 			case 0:
//         // 				magazine--;
//         // 				if (magazine == 1) {
//         // 					mode = 1;
//         // 				}
//         // 			break;

//         // 			case 1:
//         // 				Main.NewText($"Eidolon shot");
//         // 				Main.NewText($"Hunter stacks: #{hunter_mark_stack}");
//         // 				magazine--;
//         // 				mode = 2;
//         // 			break;
//         // 		}
//         // 	}
//         // 	Item.NetStateChanged();
//         // 	return true;
//         // }

//         // public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
//         // {
// 		// 	int projectile = Projectile.NewProjectile(source, position, velocity, type, damage, knockback);
// 		// 	if (Main.projectile[projectile].ModProjectile is EidolonTracer tracer) {
// 		// 		tracer.source = this;
// 		// 	}
//         //     return false;
//         // }




//         // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
//         public override void AddRecipes() {
// 			CreateRecipe()
// 				.AddIngredient(ItemID.DirtBlock, 10)
// 				.AddTile(TileID.WorkBenches)
// 				.Register();
// 		}

// 		// This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
// 		public override Vector2? HoldoutOffset() {
// 			return new Vector2(6f, -2f);
// 		}

// 		//TODO: Move this to a more specifically named example. Say, a paint gun?
// 		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {

// 			Vector2 muzzleOffset = Vector2.Normalize(velocity) * 60f;

// 			/* move upwards slightly */
// 			muzzleOffset += new Vector2(0, -10f);

// 			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0)) {
// 				position += muzzleOffset;
// 			}

// 			// /* TODO: Change this to eidolon tracer */
// 			// if (type == ProjectileID.Bullet) {
// 			// 	type = ModContent.ProjectileType<EidolonTracer>();
// 			// 	//Item.shoot = ModContent.ProjectileType<EidolonTracer>();
// 			// }
// 		}

// 		/*
// 		* Feel free to uncomment any of the examples below to see what they do
// 		*/

// 		// What if I wanted it to work like Uzi, replacing regular bullets with High Velocity Bullets?
// 		// Uzi/Molten Fury style: Replace normal Bullets with High Velocity
// 		/*public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
// 			if (type == ProjectileID.Bullet) { // or ProjectileID.WoodenArrowFriendly
// 				type = ProjectileID.BulletHighVelocity; // or ProjectileID.FireArrow;
// 			}
// 		}*/

// 		// What if I wanted multiple projectiles in a even spread? (Vampire Knives)
// 		// Even Arc style: Multiple Projectile, Even Spread
// 		/*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
// 			float numberProjectiles = 3 + Main.rand.Next(3); // 3, 4, or 5 shots
// 			float rotation = MathHelper.ToRadians(45);

// 			position += Vector2.Normalize(velocity) * 45f;

// 			for (int i = 0; i < numberProjectiles; i++) {
// 				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
// 				Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
// 			}

// 			return false; // return false to stop vanilla from calling Projectile.NewProjectile.
// 		}*/

// 		// How can I make the shots appear out of the muzzle exactly?
// 		// Also, when I do this, how do I prevent shooting through tiles?
// 		/*public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
// 			Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;

// 			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0)) {
// 				position += muzzleOffset;
// 			}
// 		}*/

// 		// How can I get a "Clockwork Assault Rifle" effect?
// 		// 3 round burst, only consume 1 ammo for burst. Delay between bursts, use reuseDelay
// 		// Make the following changes to SetDefaults():
// 		/*
// 			item.useAnimation = 12;
// 			item.useTime = 4; // one third of useAnimation
// 			item.reuseDelay = 14;
// 			item.consumeAmmoOnLastShotOnly = true;
// 		*/

// 		// How can I shoot 2 different projectiles at the same time?
// 		/*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
// 			// Here we manually spawn the 2nd projectile, manually specifying the projectile type that we wish to shoot.
// 			Projectile.NewProjectile(source, position, velocity, ProjectileID.GrenadeI, damage, knockback, player.whoAmI);

// 			// By returning true, the vanilla behavior will take place, which will shoot the 1st projectile, the one determined by the ammo.
// 			return true;
// 		}*/

// 		// How can I choose between several projectiles randomly?
// 		/*public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
// 			// Here we randomly set type to either the original (as defined by the ammo), a vanilla projectile, or a mod projectile.
// 			type = Main.rand.Next(new int[] { type, ProjectileID.GoldenBullet, ModContent.ProjectileType<Projectiles.ExampleBullet>() });
// 		}*/
// 	}
// }



using Eld.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Eld.Rarities;


using System.Collections.Generic;

namespace Eld.Items.Weapons
{
	public class GodOfTheForest : ModItem
	{
		// You can use a vanilla texture for your item by using the format: "Terraria/Item_<Item ID>".
		//public override string Texture => "Terraria/Images/Item_" + ItemID.LastPrism;
		public static Color OverrideColor = new(18, 210, 152);

		public override void SetDefaults() {

			Item.width = 46; // Hitbox width of the item.
			Item.height = 22; // Hitbox height of the item.
			Item.scale = 1.0f;
			Item.rare = ModContent.RarityType<SeaGreen>();

			// // Use Properties
			// Item.useTime = 20; // The item's use time in ticks (60 ticks == 1 second.)
			// Item.useAnimation = 20; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			// Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
			// Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

			// // The sound that this item plays when used.
			// // Item.UseSound = new SoundStyle($"{nameof(Eld)}/Assets/Sounds/Items/Guns/OldForestGod/OldForestGod.ogg") {
			// // 	Volume = 0.9f,
			// // 	PitchVariance = 0.2f,
			// // 	MaxInstances = 3,
			// // };
			// Item.UseSound = SoundID.Item40;

			// // Weapon Properties
			// Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
			// Item.damage = 3000; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
			// Item.knockBack = 12f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
			// Item.noMelee = true; // So the item's animation doesn't do damage.

			// // Gun Properties
			// Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
			// Item.shootSpeed = 50f; // The speed of the projectile (measured in pixels per frame.)
			// Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.

			Item.damage = 3000;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.channel = true;
            Item.knockBack = 5f;
            Item.UseSound = SoundID.Item77 with { Volume = SoundID.Item77.Volume * 0.7f };
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<GodOfTheForestScope>();
            Item.shootSpeed = 16f;
		}

		public override Vector2? HoldoutOffset() => new Vector2(-1.5f, 3f);

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			// Vector2 laserOffset = new Vector2(-2f, 2f);
			// if (player.direction == -1)
			// {
			// 	laserOffset.X = -laserOffset.X;
			// }
			// float rotation = player.itemRotation;
			// Vector2 rotatedOffset = laserOffset.RotatedBy(rotation);

			// // Apply the offset to the position
			// position += rotatedOffset;

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, ai1: GodOfTheForestScope.BaseMaxCharge * player.GetWeaponAttackSpeed(player.HeldItem));
            return false;
        }

        // public override void UseItemFrame(Player player)
        // {
        //     // Thank you Mr. IbanPlay (CoralSprout.cs)
        //     // Calculate the dirction in which the players arms should be pointing at.
        //     float armPointingDirection = player.itemRotation;
        //     if (player.direction < 0)
        //         armPointingDirection += MathHelper.Pi;

        //     player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, armPointingDirection - MathHelper.PiOver2);
        //     player.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, armPointingDirection - MathHelper.PiOver2);
        // }

		/* Draw unlit sprite pixels when drawing dropped item */
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI) {
			Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("Eld/Items/Weapons/GodOfTheForestGlow").Value);
		}


		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.DirtBlock, 10)
				.AddTile(TileID.WorkBenches)
				.Register();
		}

		public override void HoldItem(Player player)
        {
            // Check if the holdout projectile does not exist
            // if (player.ownedProjectileCounts[ModContent.ProjectileType<OldForestGodHoldout>()] <= 0)
            // {
			// 	Main.NewText("creating");
            //     // Spawn the holdout projectile
            //     Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center, Vector2.Zero, ModContent.ProjectileType<OldForestGodHoldout>(), 0, 0, player.whoAmI);
            // }
        }


	}
}