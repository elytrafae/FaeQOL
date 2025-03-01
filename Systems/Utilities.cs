using FaeQOL.Content.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeQOL.Systems {
    internal class Utilities {

        public static Item SearchForKeyInKeychains(Player player, int keyType) {
            MyModPlayer myPlayer = MyModPlayer.Get(player);
            for (int i = 0; i < myPlayer.keychainsInInventory.Count; i++) {
                Keychain keychain = myPlayer.keychainsInInventory[i];
                Item key = keychain.GetKeyOfTypeFromKeychain(keyType);
                if (key == null) {
                    continue;
                }
                return key;
            }
            return null;
        }

        public static Item SearchForKeyInKeychains(Player player, Item keyInstance) {
            MyModPlayer myPlayer = MyModPlayer.Get(player);
            for (int i = 0; i < myPlayer.keychainsInInventory.Count; i++) {
                Keychain keychain = myPlayer.keychainsInInventory[i];
                Item key = keychain.GetKeyOfTypeFromKeychain(keyInstance);
                if (key == null) {
                    continue;
                }
                return key;
            }
            return null;
        }

        public static bool IsBossAlive() {
            foreach (NPC npc in Main.ActiveNPCs) {
                if (npc.boss) {
                    return true;
                }
            }
            return false;
        }

    }
}
