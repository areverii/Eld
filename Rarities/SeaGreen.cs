using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Eld.Rarities
{
	public class SeaGreen : ModRarity
	{
		// Sea Green is Rarity 16 ??
		public override Color RarityColor => new Color(18, 210, 113);

		public override int GetPrefixedRarity(int offset, float valueMult) => offset switch
		{
			// -2 => ModContent.RarityType<DarkBlue>(),
			// -1 => ModContent.RarityType<Violet>(),
			// 1 => ModContent.RarityType<CalamityRed>(),
			// 2 => ModContent.RarityType<CalamityRed>(),
			_ => Type,
		};
	}
}
