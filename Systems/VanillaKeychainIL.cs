using FaeQOL.Content.Items;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Map;
using FullSerializer.Internal;
using FaeQOL.Systems.Config;
using Terraria.UI;

namespace FaeQOL.Systems {
    internal class VanillaKeychainIL : ModSystem {

        const int DOOR_FLAG = 64;

        public override void Load() {
            if (ModContent.GetInstance<ServerConfig>().EnableKeychain) {
                IL_Player.TileInteractionsUse += IL_Player_TileInteractionsUse;
                On_Main.TryFreeingElderSlime += On_Main_TryFreeingElderSlime;
                IL_ItemSlot.TryOpenContainer += IL_ItemSlot_TryOpenContainer;
            }
        }

        private void IL_ItemSlot_TryOpenContainer(ILContext il) {
            try {
                var c = new ILCursor(il);
                var c2 = new ILCursor(il); // This is gonna be used for obtaining labels
                // Skip to the Golden Lock Box ID
                c.GotoNext(MoveType.After, i => i.MatchLdfld<Item>("type"), i => i.MatchLdcI4(ItemID.LockBox));
                c.Index++; // Go below the equals check

                // Get the label needed to jump to if we consumed the key
                ILLabel goldenLockLabel = null;
                c2.GotoNext(MoveType.After, i => i.MatchLdfld<Item>("type"), i => i.MatchLdcI4(ItemID.LockBox));
                c2.GotoNext(i => i.MatchBrtrue(out goldenLockLabel));
                if (goldenLockLabel != null) {
                    // Push the following onto the stack:
                    c.Emit(Mono.Cecil.Cil.OpCodes.Ldarg_1); // Player (arg1)
                    c.EmitDelegate((Player player) => {
                        Item key = Utilities.SearchForKeyInKeychains(player, ItemID.GoldenKey);
                        if (key == null) {
                            return false;
                        }
                        if (ItemLoader.ConsumeItem(key, player)) {
                            key.stack--;
                        }
                        if (key.stack <= 0) {
                            key.TurnToAir();
                        }
                        return true;
                    });
                    c.EmitBrtrue(goldenLockLabel);
                }


                // Skip to the Obsidian Lockbox ID
                c.GotoNext(MoveType.After, i => i.MatchLdfld<Item>("type"), i => i.MatchLdcI4(ItemID.ObsidianLockbox));
                c.Index++; // Go below the equals check

                // Get the label needed to jump to if we found the key
                ILLabel obsidianLockLabel = null;
                c2.GotoNext(MoveType.After, i => i.MatchLdfld<Item>("type"), i => i.MatchLdcI4(ItemID.ObsidianLockbox));
                c2.GotoNext(i => i.MatchBrtrue(out obsidianLockLabel));
                if (obsidianLockLabel != null) {
                    // Push the following onto the stack:
                    c.Emit(Mono.Cecil.Cil.OpCodes.Ldarg_1); // Player (arg1)
                    c.EmitDelegate((Player player) => {
                        return Utilities.SearchForKeyInKeychains(player, ItemID.ShadowKey) != null;
                    });
                    c.EmitBrtrue(obsidianLockLabel);
                }

            } catch (Exception e) {
                MonoModHooks.DumpIL(ModContent.GetInstance<FaeQOL>(), il);
            }
        }

        private bool On_Main_TryFreeingElderSlime(On_Main.orig_TryFreeingElderSlime orig, int npcIndex) {
            Player player = Main.player[Main.myPlayer];
            Item key = Utilities.SearchForKeyInKeychains(player, ItemID.GoldenKey);
            if (key != null) {
                key.stack--;
                if (key.stack <= 0) {
                    key.TurnToAir();
                }
                return true;
            }
            return orig(npcIndex);
        }

        private void IL_Player_TileInteractionsUse(MonoMod.Cil.ILContext il) {
            try {
                var c = new ILCursor(il);
                // Go right below the following line, before vanilla's loop of looking for the door key item!
                // bool flag9 = false;
                // NOTE: Will 100% break with a terraria update!
                c.GotoNext(i => (i.MatchStloc(out int localVariableIndex) && localVariableIndex == DOOR_FLAG));
                c.Index++;

                // Push the following onto the stack:
                c.Emit(Mono.Cecil.Cil.OpCodes.Ldarg_0); // Player (arg0)
                c.Emit(Mono.Cecil.Cil.OpCodes.Ldloc, 63); // keyType (num63)
                c.Emit(Mono.Cecil.Cil.OpCodes.Ldarg_1); // myX (arg1)
                c.Emit(Mono.Cecil.Cil.OpCodes.Ldarg_2); // myY (arg2)
                c.EmitDelegate(SearchForDoorKeyInKeychains);
                c.Emit(Mono.Cecil.Cil.OpCodes.Stloc, DOOR_FLAG); // Store to flag9

                // Code that makes sure a potential second jungle temple key from the inventory won't be consumed
                // We do this by implementing a similar branch to the void bag one

                // Step 1: Get the ILLabel that the void bag skip uses!
                // We do this by finding the first brtrue instruction after DOOR_FLAG is initialized
                ILLabel skipLabel = null;
                var c2 = new ILCursor(il);
                c2.GotoNext(i => (i.MatchStloc(out int localVariableIndex) && localVariableIndex == DOOR_FLAG));
                c2.GotoNext(i => { 
                    bool isBrTrue = i.MatchBrtrue(out ILLabel label);
                    skipLabel = label;
                    return isBrTrue;
                });
                if (skipLabel != null) {
                    c.Emit(Mono.Cecil.Cil.OpCodes.Ldloc, DOOR_FLAG);
                    c.Emit(Mono.Cecil.Cil.OpCodes.Brtrue, skipLabel);
                }


                // Go right below the following line, before vanilla's loop of looking for the chest key item!
                // bool flag17 = num73 != 329;
                // NOTE: Will 100% break with a terraria update!
                c.GotoNext(i => (i.MatchStloc(out int localVariableIndex) && localVariableIndex == 104) );
                c.Index++;

                // Push the following onto the stack:
                c.Emit(Mono.Cecil.Cil.OpCodes.Ldarg_0); // Player (arg0)
                c.Emit(Mono.Cecil.Cil.OpCodes.Ldloc, 102); // keyType (num73)
                c.Emit(Mono.Cecil.Cil.OpCodes.Ldloc, 104); // consumeKey (flag17)
                c.Emit(Mono.Cecil.Cil.OpCodes.Ldloc, 96); // chestX (num69)
                c.Emit(Mono.Cecil.Cil.OpCodes.Ldloc, 97); // chestY (num70)
                c.EmitDelegate(SearchForChestKeyInKeychains);
                c.Emit(Mono.Cecil.Cil.OpCodes.Stloc, 103); // Store to flag15

            } catch (Exception e) {
                MonoModHooks.DumpIL(ModContent.GetInstance<FaeQOL>(), il);
            }
        }

        // Returns the value of the flag that controls wether or not to search in void bags, too.
        private bool SearchForDoorKeyInKeychains(Player player, int keyType, int doorX, int doorY) {
            Item key = Utilities.SearchForKeyInKeychains(player, keyType);
            if (key == null) {
                return false;
            }
            if (ItemLoader.ConsumeItem(key, player)) {
                key.stack--;
            }
            if (key.stack <= 0) {
                key.TurnToAir();
            }
            WorldGen.UnlockDoor(doorX, doorY);
            if (Main.netMode == NetmodeID.MultiplayerClient) {
                NetMessage.SendData(MessageID.LockAndUnlock, -1, -1, null, player.whoAmI, 2f, doorX, doorY);
            }
            return true;
        }

        // Returns the value of the flag that controls wether or not to search in void bags, too.
        private bool SearchForChestKeyInKeychains(Player player, int keyType, bool consumeKey, int chestX, int chestY) {
            Item key = Utilities.SearchForKeyInKeychains(player, keyType);
            if (key == null) {
                return false;
            }
            if (!Chest.Unlock(chestX, chestY)) {
                return false;
            }
            if (consumeKey) {
                if (ItemLoader.ConsumeItem(key, player)) {
                    key.stack--;
                }
                if (key.stack <= 0) {
                    key.TurnToAir();
                }
            }
            if (Main.netMode == NetmodeID.MultiplayerClient) {
                NetMessage.SendData(MessageID.LockAndUnlock, -1, -1, null, player.whoAmI, 1f, chestX, chestY);
            }
            return true;
        }
    }
}
