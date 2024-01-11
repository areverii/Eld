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
			
			Item.damage = 3000;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.channel = true;
            Item.knockBack = 5f;
            Item.UseSound = SoundID.Item77 with { Volume = SoundID.Item77.Volume * 0.7f };
            Item.autoReuse = false;
            //Item.shoot = ModContent.ProjectileType<GodOfTheForestScope>();
            //Item.shootSpeed = 16f;
			Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 7f;
            Item.useAmmo = AmmoID.Bullet;
		}

		public override bool CanShoot(Player player) => false;
        public override bool CanUseItem(Player player) => false;


        //public override Vector2? HoldoutOffset() => new Vector2(-1.5f, 3f);

        // public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        // {
        // 	// Vector2 laserOffset = new Vector2(-2f, 2f);
        // 	// if (player.direction == -1)
        // 	// {
        // 	// 	laserOffset.X = -laserOffset.X;
        // 	// }
        // 	// float rotation = player.itemRotation;
        // 	// Vector2 rotatedOffset = laserOffset.RotatedBy(rotation);

        // 	// // Apply the offset to the position
        // 	// position += rotatedOffset;

        //     Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, ai1: GodOfTheForestScope.BaseMaxCharge * player.GetWeaponAttackSpeed(player.HeldItem));
        //     return false;
        // }

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



        /* Draw unlit sprite pixels for dropped item */
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI) {
			Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("Eld/Items/Weapons/GodOfTheForestGlow").Value);
		}


		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.DirtBlock, 10)
				.AddTile(TileID.WorkBenches)
				.Register();
		}



	}
}