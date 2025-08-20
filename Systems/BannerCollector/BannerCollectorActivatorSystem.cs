using FaeQOL.Content.BuilderToggles;
using FaeQOL.Systems.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace FaeQOL.Systems.BannerCollector {

    internal class BannerCollectorActivatorSystem : ModSystem {

        public override void Load() {
            On_SceneMetrics.ScanAndExportToMain += On_SceneMetrics_ScanAndExportToMain;
        }

        public static Dictionary<int, int> itemToBannerIDs = new Dictionary<int, int>();

        private void On_SceneMetrics_ScanAndExportToMain(On_SceneMetrics.orig_ScanAndExportToMain orig, SceneMetrics self, SceneMetricsScanSettings settings) {
            orig(self, settings);
            ServerConfig config = ModContent.GetInstance<ServerConfig>();
            if (config.EnableBannerCollector && config.BannerCollectorEffects) {
                foreach (Item item in Main.LocalPlayer.GetModPlayer<BannerCollectorModPlayer>().BannerInventory) {
                    if (itemToBannerIDs.TryGetValue(item.type, out int bannerID)) {
                        Main.SceneMetrics.NPCBannerBuff[bannerID] = true;
                        Main.SceneMetrics.hasBanner = true;
                    }
                }
            }
        }

        public override void PostUpdatePlayers() {
            if (!Main.playerInventory) {
                BannerCollectorToggle.Get().IsOn = false;
            }
        }

        public static bool IsBanner(int itemID) { 
            return itemToBannerIDs.ContainsKey(itemID);
        }

        public override void PostSetupContent() {
            // Get a list of every banner item, and their corresponding banner items
            foreach (var pair in ContentSamples.ItemsByType) {
                int bannerID = NPCLoader.BannerItemToNPC(pair.Key);
                if (bannerID == -1) {
                    bannerID = GetVanillaBannerIDFromItem(pair.Value);
                }
                if (bannerID != -1 && ItemID.Sets.BannerStrength.IndexInRange(pair.Key) && ItemID.Sets.BannerStrength[pair.Key].Enabled) { 
                    itemToBannerIDs.Add(pair.Key, bannerID);
                }
            }
        }

        private int GetVanillaBannerIDFromItem(Item item) {
            if (item.createTile != TileID.Banners) {
                return -1;
            }

            if (!TryGetTileFrame(item.createTile, item.placeStyle, out int frameX, out int frameY)) {
                return -1;
            }
            if (!(frameX >= 396 || frameY >= 54)) {
                return -1;
            }
            // Code copied from vanilla source code
            int num4 = frameX / 18 - 21;
            for (int num5 = frameY; num5 >= 54; num5 -= 54) {
                num4 += 90;
                num4 += 21;
            }


            return num4;
        }

        private bool TryGetTileFrame(int tileID, int tileStyle, out int frameX, out int frameY) {
            TileObjectData tileData = TileObjectData.GetTileData(tileID, tileStyle, 0);
            if (tileData == null) {
                frameX = 0;
                frameY = 0;
                return false;
            }
            // Code mostly from vanilla
            int num2 = 0;
            int num3 = 0;
            int num4 = tileData.CalculatePlacementStyle(tileStyle, 0, 0);
            int num5 = 0;
            if (tileData.StyleWrapLimit > 0) {
                num5 = num4 / tileData.StyleWrapLimit * tileData.StyleLineSkip;
                num4 %= tileData.StyleWrapLimit;
            }
            if (tileData.StyleHorizontal) {
                num2 = tileData.CoordinateFullWidth * num4;
                num3 = tileData.CoordinateFullHeight * num5;
            } else {
                num2 = tileData.CoordinateFullWidth * num5;
                num3 = tileData.CoordinateFullHeight * num4;
            }

            frameX = num2 + 0 * (tileData.CoordinateWidth + tileData.CoordinatePadding);
            frameY = num3;
            return true;
        }
    }

}
