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

        private void On_SceneMetrics_ScanAndExportToMain(On_SceneMetrics.orig_ScanAndExportToMain orig, SceneMetrics self, SceneMetricsScanSettings settings) {
            orig(self, settings);
            foreach (Item item in Main.LocalPlayer.inventory) {
                int bannerID = NPCLoader.BannerItemToNPC(item.type);
                if (bannerID == -1) {
                    bannerID = GetVanillaBannerIDFromItem(item);
                }
                if (bannerID != -1 && ItemID.Sets.BannerStrength.IndexInRange(item.type) && ItemID.Sets.BannerStrength[item.type].Enabled) {
                    Main.SceneMetrics.NPCBannerBuff[bannerID] = true;
                    Main.SceneMetrics.hasBanner = true;
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
