using FaeQOL.Content.Items;
using FaeQOL.Systems.Config;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeQOL.Systems.BannerCollector {
    internal class BannerCollectorGlobalItem : GlobalItem {

        public override bool AppliesToEntity(Item entity, bool lateInstantiation) {
            return BannerCollectorActivatorSystem.IsBanner(entity.type);
        }

        public override bool OnPickup(Item item, Player player) {
            if (!ModContent.GetInstance<ServerConfig>().EnableBannerCollector) {
                return true;
            }
            Item itemCopy = item.Clone();
            
            player.GetModPlayer<BannerCollectorModPlayer>().StackBannerIntoBannerInventory(item);
            int numTransferred = itemCopy.stack;
            if (!item.IsAir) {
                numTransferred -= item.stack;
            }
            if (numTransferred > 0) {
                SoundEngine.PlaySound(SoundID.Grab, player.Center);
                PopupText.NewText(PopupTextContext.ItemPickupToVoidContainer, itemCopy, numTransferred, noStack: false, longText: false);
            }
            return !item.IsAir;
        }

        public override bool ItemSpace(Item item, Player player) {
            if (!ModContent.GetInstance<ServerConfig>().EnableBannerCollector) {
                return false;
            }
            return player.GetModPlayer<BannerCollectorModPlayer>().CanStackBannerIntoInventory(item);
        }

    }
}
