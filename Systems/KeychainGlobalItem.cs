using FaeQOL.Content.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FaeQOL.Systems {
    public class KeychainGlobalItem : GlobalItem {

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
            if (ItemSets.IsItemKey(item.type)) {
                tooltips.Add(new TooltipLine(this.Mod, "item_can_be_put_in_keychain", Language.GetTextValue("Mods.FaeQOL.CanBePutOnKeychain")));
            }
        }

        public override bool OnPickup(Item item, Player player) {
            if (!ItemSets.IsItemKey(item.type)) {
                return true; // This is not a key. Continue as normal.
            }
            Item itemCopy = item.Clone();
            bool atLeastOneItemWentIntoKeychain = false;
            foreach (Keychain keychain in MyModPlayer.Get(player).keychainsInInventory) {
                Item stackInKeychain = keychain.GetKeyOfTypeFromKeychain(item);
                if (stackInKeychain != null) {
                    if (ItemLoader.TryStackItems(stackInKeychain, item, out int numTransferred, infiniteSource: false)) {
                        if (numTransferred > 0) {
                            PopupText.NewText(PopupTextContext.ItemPickupToVoidContainer, itemCopy, numTransferred, noStack: false, longText: false);
                            atLeastOneItemWentIntoKeychain = true;
                        }
                        if (item.IsAir) {
                            // All of the keys we picked up went into a keychains.
                            // We signal that the pickup should not continue any further!
                            SoundEngine.PlaySound(SoundID.Grab, player.Center);
                            return false; 
                        }
                    }
                }
            }
            if (atLeastOneItemWentIntoKeychain) {
                SoundEngine.PlaySound(SoundID.Grab, player.Center);
            }
            return true; // Not all keys went into keychains. The rest should be picked up into the inventory.
        }

        public override bool ItemSpace(Item item, Player player) {
            if (!ItemSets.IsItemKey(item.type)) {
                return false; // This is not a key. Continue as normal.
            }

            foreach (Keychain keychain in MyModPlayer.Get(player).keychainsInInventory) {
                Item stackInKeychain = keychain.GetKeyOfTypeFromKeychain(item);
                if (stackInKeychain != null) {
                    if (stackInKeychain.stack < stackInKeychain.maxStack) {
                        return true; // One of the keychains has available space! Commence with the pickup!
                    }
                }
            }

            return false; // None of the Keychains have any available space
        }

    }
}
