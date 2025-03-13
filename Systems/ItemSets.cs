using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeQOL.Systems {
    internal class ItemSets : ModSystem {

        private static List<int> IsKey = null;
        public static List<int> IsTorchGodTorch = null;
        public static List<int> IsTorchGodCampfire = null;

        public override void Load() {
            IsKey = new([ItemID.GoldenKey, ItemID.ShadowKey, ItemID.TempleKey, ItemID.JungleKey, ItemID.FrozenKey, ItemID.HallowedKey, ItemID.CorruptionKey, ItemID.CrimsonKey, ItemID.DungeonDesertKey]);
            IsTorchGodTorch = new([ItemID.IceTorch, ItemID.DesertTorch, ItemID.JungleTorch, ItemID.HallowedTorch, ItemID.CorruptTorch, ItemID.CrimsonTorch, ItemID.CursedTorch, ItemID.IchorTorch, ItemID.CoralTorch, ItemID.MushroomTorch, ItemID.BoneTorch, ItemID.DemonTorch, ItemID.ShimmerTorch]);
            IsTorchGodCampfire = new([ItemID.FrozenCampfire, ItemID.DesertCampfire, ItemID.JungleCampfire, ItemID.HallowedCampfire, ItemID.CorruptCampfire, ItemID.CrimsonCampfire, ItemID.CursedCampfire, ItemID.IchorCampfire, ItemID.CoralCampfire, ItemID.MushroomCampfire, ItemID.BoneCampfire, ItemID.DemonCampfire, ItemID.ShimmerCampfire]);
        }

        public override void PostSetupContent() {
            foreach (ModBiome biome in ModContent.GetContent<ModBiome>()) {
                if (biome.BiomeTorchItemType > -1) {
                    IsTorchGodTorch.Add(biome.BiomeTorchItemType);
                }
                if (biome.BiomeCampfireItemType > -1) {
                    IsTorchGodCampfire.Add(biome.BiomeCampfireItemType);
                }
            }
        }

        public static bool IsItemKey(int type) { 
            return IsKey.Contains(type);
        }

        public static void RegisterKey(int type) {
            IsKey.Add(type);
        }

    }
}
