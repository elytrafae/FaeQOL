using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeQOL.Systems {
    internal class ItemSets : ModSystem {

        private static bool[] IsKey = null;
        public static bool[] IsTorchGodTorch = null;
        public static bool[] IsTorchGodCampfire = null;

        public override void Load() {
            IsKey = ItemID.Sets.Factory.CreateBoolSet(ItemID.GoldenKey, ItemID.ShadowKey, ItemID.TempleKey, ItemID.JungleKey, ItemID.FrozenKey, ItemID.HallowedKey, ItemID.CorruptionKey, ItemID.CrimsonKey, ItemID.DungeonDesertKey);
            IsTorchGodTorch = ItemID.Sets.Factory.CreateBoolSet(ItemID.IceTorch, ItemID.DesertTorch, ItemID.JungleTorch, ItemID.HallowedTorch, ItemID.CorruptTorch, ItemID.CrimsonTorch, ItemID.CursedTorch, ItemID.IchorTorch, ItemID.CoralTorch, ItemID.MushroomTorch, ItemID.BoneTorch, ItemID.DemonTorch, ItemID.ShimmerTorch);
            IsTorchGodCampfire = ItemID.Sets.Factory.CreateBoolSet(ItemID.FrozenCampfire, ItemID.DesertCampfire, ItemID.JungleCampfire, ItemID.HallowedCampfire, ItemID.CorruptCampfire, ItemID.CrimsonCampfire, ItemID.CursedCampfire, ItemID.IchorCampfire, ItemID.CoralCampfire, ItemID.MushroomCampfire, ItemID.BoneCampfire, ItemID.DemonCampfire, ItemID.ShimmerCampfire);
        }

        public override void PostSetupContent() {
            foreach (ModBiome biome in ModContent.GetContent<ModBiome>()) {
                IsTorchGodTorch[biome.BiomeTorchItemType] = true;
                IsTorchGodCampfire[biome.BiomeCampfireItemType] = true;
            }
        }

        public static bool IsItemKey(int type) { 
            return IsKey[type];
        }

        public static void RegisterKey(int type) {
            IsKey[type] = true;
        }

    }
}
