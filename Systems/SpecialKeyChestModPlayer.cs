using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using FaeQOL.Content.Items.AltMimicSpawners;

namespace FaeQOL.Systems {
    internal class SpecialKeyChestModPlayer : ModPlayer {

        // Based on this outdated example: https://github.com/tModLoader/tModLoader/blob/stable/ExampleMod/Old/NPCs/ExampleChestSummon.cs

        public int LastChest;

        public override void PreUpdateBuffs() {
            if (Main.netMode != NetmodeID.MultiplayerClient) {
                if (Player.chest == -1 && LastChest >= 0 && Main.chest[LastChest] != null) {
                    int x2 = Main.chest[LastChest].x;
                    int y2 = Main.chest[LastChest].y;
                    ChestItemSummonCheck(x2, y2, Player);
                }
                LastChest = Player.chest;
            }
        }

        // Allows mimic spawning in single player with autopause on
        public override void UpdateAutopause() {
            LastChest = Player.chest;
        }

        public static bool ChestItemSummonCheck(int x, int y, Player player) {
            if (Main.netMode == NetmodeID.MultiplayerClient) { // Moved hardmode condition to the action item instead
                return false;
            }
            int chestID = Chest.FindChest(x, y);
            if (chestID < 0) {
                return false;
            }

            // The example was a bit more flexible, but I wanted to make it less so for performance
            LeftInChestActionItem actionItem = null;

            ushort tileType = Main.tile[Main.chest[chestID].x, Main.chest[chestID].y].TileType;
            int tileStyle = (int)(Main.tile[Main.chest[chestID].x, Main.chest[chestID].y].TileFrameX / 36);
            for (int i = 0; i < Chest.maxItems; i++) {
                Item item = Main.chest[chestID].item[i];
                if (item != null && !item.IsAir) {
                    // If this is a LeftInChestActionItem, and this is the first one, then set that we found it!
                    if (item.ModItem != null && item.ModItem is LeftInChestActionItem licai && actionItem == null) {
                        if (licai.ActiveCondition(player, x, y) && licai.CanBeUsedInsideStorageTile(tileType, tileStyle)) {
                            actionItem = licai;
                        } else {
                            return false; // The action item cannot be activated, and shall act as any other item, blocking the process!
                        }
                    } else { // Otherwise, end the check, since this is invalid!
                        return false;
                    }
                }
            }

            // Check if we found actonItem
            if (actionItem == null) {
                return false;
            }

            // No idea what this all does . . .
            if (TileID.Sets.BasicChest[Main.tile[x, y].TileType]) {
                if (Main.tile[x, y].TileFrameX % 36 != 0) {
                    x--;
                }
                if (Main.tile[x, y].TileFrameY % 36 != 0) {
                    y--;
                }
                int number = Chest.FindChest(x, y);
                for (int j = x; j <= x + 1; j++) {
                    for (int k = y; k <= y + 1; k++) {
                        if (TileID.Sets.BasicChest[Main.tile[j, k].TileType]) {
                            Tile t = Main.tile[j, k];
                            t.HasTile = false;
                        }
                    }
                }
                for (int l = 0; l < Chest.maxItems; l++) {
                    Main.chest[chestID].item[l].TurnToAir();
                }
                Chest.DestroyChest(x, y);
                NetMessage.SendData(MessageID.ChestUpdates, -1, -1, null, 1, (float)x, (float)y, 0f, number, 0, 0);
                NetMessage.SendTileSquare(-1, x, y, 3);
            }

            int npcToSpawn = actionItem.NPCToSpawn(player);
            int npcIndex = NPC.NewNPC(player.GetSource_TileInteraction(x, y), x * 16 + 16, y * 16 + 32, npcToSpawn, 0, 0f, 0f, 0f, 0f, 255);
            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npcIndex, 0f, 0f, 0f, 0, 0, 0);
            if (actionItem.ProduceSpawnSmoke) {
                Main.npc[npcIndex].BigMimicSpawnSmoke();
            }
            return true;
        }


    }
}
