using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeQOL.Systems {

    [ReinitializeDuringResizeArrays]
    public class CustomItemSets {
        public static bool[] IsKey = ItemID.Sets.Factory.CreateNamedSet("KeychainKey")
        .Description("Items in this set can be added to the mod's Keychain item. Usually, items that can open things should be added here. Note that keys within Keychains can't open things out of the box, and additional code is required for that. Check the Mod.Call section of this mod's documentation for methods.")
        .RegisterBoolSet(false, ItemID.GoldenKey, ItemID.ShadowKey, ItemID.TempleKey, ItemID.JungleKey, ItemID.FrozenKey, ItemID.HallowedKey, ItemID.CorruptionKey, ItemID.CrimsonKey, ItemID.DungeonDesertKey);

        public static bool[] IsTorchGodTorch = ItemID.Sets.Factory.CreateNamedSet("TorchGodTorch")
        .Description("Items in this set are denoted to be torches that the Torch God's Favor can turn regular torches into. Note that torches set to be a specific biome's torch in ModBiomes are registered automatically. No additional code is required for compatibility. This set is used for a feature to automatically turn Torch God Torches back to regular torches upon pickup.")
        .RegisterBoolSet(false, ItemID.IceTorch, ItemID.DesertTorch, ItemID.JungleTorch, ItemID.HallowedTorch, ItemID.CorruptTorch, ItemID.CrimsonTorch, ItemID.CursedTorch, ItemID.IchorTorch, ItemID.CoralTorch, ItemID.MushroomTorch, ItemID.BoneTorch, ItemID.DemonTorch, ItemID.ShimmerTorch);

        public static bool[] IsTorchGodCampfire = ItemID.Sets.Factory.CreateNamedSet("TorchGodCampfire")
        .Description("Items in this set are denoted to be campfires that the Torch God's Favor can turn regular campfires into. Note that campfires set to be a specific biome's campfire in ModBiomes are registered automatically. No additional code is required for compatibility. This set is used for a feature to automatically turn Torch God Campfires back to regular campfires upon pickup.")
        .RegisterBoolSet(false, ItemID.FrozenCampfire, ItemID.DesertCampfire, ItemID.JungleCampfire, ItemID.HallowedCampfire, ItemID.CorruptCampfire, ItemID.CrimsonCampfire, ItemID.CursedCampfire, ItemID.IchorCampfire, ItemID.CoralCampfire, ItemID.MushroomCampfire, ItemID.BoneCampfire, ItemID.DemonCampfire, ItemID.ShimmerCampfire);
    }
    

    public class CustomSetsSystem : ModSystem {

        public override void PostSetupContent() {
            foreach (ModBiome biome in ModContent.GetContent<ModBiome>()) {
                if (biome.BiomeTorchItemType > -1) {
                    CustomItemSets.IsTorchGodTorch[biome.BiomeTorchItemType] = true;
                }
                if (biome.BiomeCampfireItemType > -1) {
                    CustomItemSets.IsTorchGodCampfire[biome.BiomeCampfireItemType] = true;
                }
            }
        }

        public static bool IsItemKey(int type) { 
            return CustomItemSets.IsKey[type];
        }

        public static void RegisterKey(int type) {
            CustomItemSets.IsKey[type] = true;
        }

    }
}
