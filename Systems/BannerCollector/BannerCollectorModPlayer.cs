using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace FaeQOL.Systems.BannerCollector {
#if DEBUG
    public class BannerCollectorModPlayer : ModPlayer {

        public const string BANNER_INVENTORY_KEY = "BannerInventory";
        public List<Item> BannerInventory = new();

        public override void SaveData(TagCompound tag) {
            List<TagCompound> list = new List<TagCompound>();
            BannerInventory.ForEach(item => { list.Add(ItemIO.Save(item)); });
            tag.Add(BANNER_INVENTORY_KEY, list);
        }

        public override void LoadData(TagCompound tag) {
            if (tag.TryGet(BANNER_INVENTORY_KEY, out List<TagCompound> list)) {
                BannerInventory.Clear();
                list.ForEach(item => { BannerInventory.Add(ItemIO.Load(item)); });
            }
        }

        public void RemoveEmptyStacksFromBannerInventory() {
            for (int i = 0; i < BannerInventory.Count; i++) {
                Item invItem = BannerInventory[i];
                if (invItem.IsAir) {
                    BannerInventory.Remove(invItem);
                    i--;
                }
            }
        }

        public void StackBannerIntoBannerInventory(Item banner) {

            for (int i=0; i < BannerInventory.Count; i++) {
                Item invItem = BannerInventory[i];
                if (invItem.IsAir) {
                    BannerInventory.RemoveAt(i);
                    i--;
                }else if (banner.type == invItem.type && ItemLoader.TryStackItems(invItem, banner, out int numTransferred, false)) {
                    BannerInventory[i] = invItem; // Do this to make the array realize we actually DID SOMETHING!!!
                    return;
                }
            }
            BannerInventory.Add(banner.Clone());
            banner.TurnToAir();
        }

    }
#endif
}
