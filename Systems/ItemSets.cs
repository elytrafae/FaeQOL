using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeQOL.Systems {
    internal class ItemSets : ModSystem {

        private static bool[] IsKey = null;// ItemID.Sets.Factory.CreateBoolSet(ItemID.GoldenKey, ItemID.ShadowKey, ItemID.TempleKey, ItemID.JungleKey, ItemID.FrozenKey, ItemID.HallowedKey, ItemID.CorruptionKey, ItemID.CrimsonKey, ItemID.DungeonDesertKey); 

        public override void Load() {
            IsKey = ItemID.Sets.Factory.CreateBoolSet(ItemID.GoldenKey, ItemID.ShadowKey, ItemID.TempleKey, ItemID.JungleKey, ItemID.FrozenKey, ItemID.HallowedKey, ItemID.CorruptionKey, ItemID.CrimsonKey, ItemID.DungeonDesertKey);
        }

        public static bool IsItemKey(int type) { 
            return IsKey[type];
        }

        public static void RegisterKey(int type) {
            IsKey[type] = true;
        }

    }
}
