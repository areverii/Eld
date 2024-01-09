using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Eld.Rarities;

namespace Eld.Items.Armor.Twilight
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Body value here will result in TML expecting X_Arms.png, X_Body.png and X_FemaleBody.png sprite-sheet files to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Head)]
	public class TwilightHelm : ModItem, IExtendedHat
	{

		public new string LocalizationCategory => "Items.Armor.PostMoonLord";

		public override void SetDefaults() {
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
			Item.rare = ModContent.RarityType<SeaGreen>(); // The rarity of the item
			Item.defense = 30; // The amount of defense the item will give when equipped
		}

		public override void UpdateEquip(Player player) {
			//player.buffImmune[BuffID.OnFire] = true; // Make the player immune to Fire
			//player.statManaMax2 += MaxManaIncrease; // Increase how many mana points the player can have by 20
			//player.maxMinions += MaxMinionIncrease; // Increase how many minions the player can have by one
		}

		public string ExtensionTexture => "Eld/Items/Armor/Twilight/TwilightHelm_Extension";
        public Vector2 ExtensionSpriteOffset(PlayerDrawSet drawInfo) => new Vector2(0f, -6f);

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		// public override void AddRecipes() {
		// 	CreateRecipe().AddIngredient<ExampleItem>()
		// 		.AddTile<Tiles.Furniture.ExampleWorkbench>()
		// 		.Register();
		// }
	}
}