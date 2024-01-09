//using CalamityMod.Cooldowns;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Eld.EldPl
{
    public partial class EldPlayer : ModPlayer
    {
        #region Standard Syncs
        internal const int GlobalSyncPacketTimer = 15;

        internal void StandardSync()
        {
            // SyncRage(false);
            // SyncAdrenaline(false);
            // SyncDefenseDamage(false);
        }

        private void EnterWorldSync()
        {
            StandardSync();
        }

        internal void MouseControlsSync()
        {
            SyncRightClick(false);
            SyncMousePosition(false);
        }
        #endregion

        

        public void SyncRightClick(bool server)
        {
            ModPacket packet = Mod.GetPacket(256);
            packet.Write((byte)EldMessageType.RightClickSync);
            packet.Write(Player.whoAmI);
            packet.Write(mouseRight);
            Player.SendPacket(packet, server);
        }
        public void SyncMousePosition(bool server)
        {
            ModPacket packet = Mod.GetPacket(256);
            packet.Write((byte)EldMessageType.MousePositionSync);
            packet.Write(Player.whoAmI);
            packet.WriteVector2(mouseWorld);
            Player.SendPacket(packet, server);
        }

        internal void HandleRightClick(BinaryReader reader)
        {
            mouseRight = reader.ReadBoolean();
            if (Main.netMode == NetmodeID.Server)
                SyncRightClick(true);
        }
        internal void HandleMousePosition(BinaryReader reader)
        {
            mouseWorld = reader.ReadVector2();
            if (Main.netMode == NetmodeID.Server)
                SyncMousePosition(true);
        }
    }
}
