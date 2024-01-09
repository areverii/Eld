using Eld.EldPl;
// using CalamityMod.Items;
// using CalamityMod.NPCs;
// using CalamityMod.Projectiles;
using Terraria;

namespace Eld
{
    public static partial class EldUtils
    {
        public static EldPlayer Eld(this Player player) => player.GetModPlayer<EldPlayer>();
        // public static CalamityGlobalNPC Calamity(this NPC npc) => npc.GetGlobalNPC<CalamityGlobalNPC>();
        // public static CalamityFallDamageNPC FallingNPC(this NPC npc) => npc.GetGlobalNPC<CalamityFallDamageNPC>();
        // public static CalamityPolarityNPC PolarityNPC(this NPC npc) => npc.GetGlobalNPC<CalamityPolarityNPC>();
        // public static CalamityGlobalItem Calamity(this Item item) => item.GetGlobalItem<CalamityGlobalItem>();
        // public static CalamityGlobalProjectile Calamity(this Projectile proj) => proj.GetGlobalProjectile<CalamityGlobalProjectile>();
        public static Item ActiveItem(this Player player) => Main.mouseItem.IsAir ? player.HeldItem : Main.mouseItem;
    }
}
