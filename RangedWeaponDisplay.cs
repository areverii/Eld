// using System.Collections.Generic;
// using Terraria;
// using Terraria.ID;
// using Terraria.ModLoader;
// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;
// using Eld.Items.Weapons;
// using ReLogic.Content;
// using Terraria.GameContent;
// using Terraria.DataStructures;

// namespace Eld {
//     public class RangedWeaponDisplay : ModPlayer
//     {
//         // public override void PostUpdate()
//         // {
//         //     Item heldItem = Player.inventory[Player.selectedItem];

//         //     // Check if the held item is a ranged weapon
//         //     // if (heldItem != null && heldItem.ranged)
//         //     // {
//         //     //     DisplayRangedWeapon(heldItem);
//         //     // }
//         //     // FOR NOW, just display OldForestGod
//         //     if (Player.HeldItem.type == ModContent.ItemType<OldForestGod>()) {
//         //         DisplayRangedWeapon(heldItem);
//         //     }
//         // }

//         public override void ModifyDrawLayers(List<PlayerLayer> layers)
//         {
//             // Find the index of the layer where Terraria draws held items
//             int heldItemLayerIndex = layers.FindIndex(layer => layer.Name.Equals("HeldItem") && layer.mod.Equals("Terraria"));

//             if (heldItemLayerIndex != -1)
//             {
//                 // Insert a new layer to draw your custom weapon right after the standard HeldItem layer
//                 layers.Insert(heldItemLayerIndex + 1, new PlayerLayer("YourModName", "CustomRangedWeapon", player =>
//                 {
//                     // Only draw if the player is holding a ranged weapon
//                     if (Player.HeldItem.type == ModContent.ItemType<OldForestGod>())
//                     {
//                         DrawCustomRangedWeapon(player);
//                     }
//                 }));
//             }
//         }

//         private void DisplayRangedWeapon(Item weapon)
//         {
//             // Here you can adjust the player's animation or pose based on the weapon
//             // For simplicity, this part is omitted in this example

//             // This method would handle drawing the weapon
//             // It's called in an appropriate place, like ModifyDrawLayers or a similar hook
//             DrawWeapon(weapon);
//         }

//         private void DrawWeapon(Item weapon)
//         {
//             Texture2D weaponTexture = TextureAssets.Item[weapon.type].Value;
//             if (weaponTexture == null || weaponTexture.IsDisposed) return;

//             // Calculate the position to draw the weapon
//             Vector2 weaponPosition = Player.Center - Main.screenPosition;
//             weaponPosition.X += (Player.direction == 1) ? 20 : -20; // Adjust position based on player direction

//             // Draw the weapon
//             Main.EntitySpriteDraw(weaponTexture, weaponPosition, null, Color.White, 0f, weaponTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0);
//         }
//     }
// }
