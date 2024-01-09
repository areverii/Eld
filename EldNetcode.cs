// using CalamityMod.Events;
// using CalamityMod.Items;
// using CalamityMod.NPCs;
// using CalamityMod.NPCs.NormalNPCs;
// using CalamityMod.NPCs.Providence;
// using CalamityMod.NPCs.TownNPCs;
// using CalamityMod.TileEntities;
// using CalamityMod.World;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eld
{
    public class EldNetcode
    {
        public static void HandlePacket(Mod mod, BinaryReader reader, int whoAmI)
        {
            try
            {
                EldMessageType msgType = (EldMessageType)reader.ReadByte();
                switch (msgType)
                {

                    //
                    // Mouse control syncs
                    //

                    case EldMessageType.RightClickSync:
                        Main.player[reader.ReadInt32()].Eld().HandleRightClick(reader);
                        break;
                    case EldMessageType.MousePositionSync:
                        Main.player[reader.ReadInt32()].Eld().HandleMousePosition(reader);
                        break;


                    //
                    // Default case: with no idea how long the packet is, we can't safely read data.
                    // Throw an exception now instead of allowing the network stream to corrupt.
                    //
                    default:
                        //CalamityMod.Instance.Logger.Error($"Failed to parse Calamity packet: No Calamity packet exists with ID {msgType}.");
                        throw new Exception("Failed to parse Calamity packet: Invalid Calamity packet ID.");
                }
            }
            catch (Exception e)
            {
                // if (e is EndOfStreamException eose)
                //     CalamityMod.Instance.Logger.Error("Failed to parse Calamity packet: Packet was too short, missing data, or otherwise corrupt.", eose);
                // else if (e is ObjectDisposedException ode)
                //     CalamityMod.Instance.Logger.Error("Failed to parse Calamity packet: Packet reader disposed or destroyed.", ode);
                // else if (e is IOException ioe)
                //     CalamityMod.Instance.Logger.Error("Failed to parse Calamity packet: An unknown I/O error occurred.", ioe);
                // else
                    //throw; // this either will crash the game or be caught by TML's packet policing
            }
        }

        public static void SyncWorld()
        {
            if (Main.netMode == NetmodeID.Server)
                NetMessage.SendData(MessageID.WorldData);
        }

        

        // public static void NewNPC_ClientSide(Vector2 spawnPosition, int npcType, Player player)
        // {
        //     if (Main.netMode == NetmodeID.SinglePlayer)
        //     {
        //         NPC.NewNPC(new EntitySource_WorldEvent(), (int)spawnPosition.X, (int)spawnPosition.Y, npcType, Target: player.whoAmI);
        //         return;
        //     }

        //     var netMessage = CalamityMod.Instance.GetPacket();
        //     netMessage.Write((byte)CalamityModMessageType.SpawnNPCOnPlayer);
        //     netMessage.Write((int)spawnPosition.X);
        //     netMessage.Write((int)spawnPosition.Y);
        //     netMessage.Write(npcType);
        //     netMessage.Write(player.whoAmI);
        //     netMessage.Send();
        // }
    }

    public enum EldMessageType : byte
    {
        // Mouse Controls syncs
        RightClickSync,
        MousePositionSync,

        // World state sync
        SyncDifficulties
    }
}
