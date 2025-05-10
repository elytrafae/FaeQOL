using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace FaeQOL.Systems.PreventStupidUIClicksDuringCombat {

    internal class HotbarItemIL : ModSystem {

        // Feature disabled because it hurts my brain
        public override bool IsLoadingEnabled(Mod mod) {
            return false;
        }

        public override void Load() {
            On_ItemSlot.OverrideLeftClick += On_ItemSlot_OverrideLeftClick;
            On_ItemSlot.RightClick_ItemArray_int_int += On_ItemSlot_RightClick_ItemArray_int_int;
            On_ItemSlot.RightClick_refItem_int += On_ItemSlot_RightClick_refItem_int;
        }

        private void On_ItemSlot_RightClick_refItem_int(On_ItemSlot.orig_RightClick_refItem_int orig, ref Terraria.Item inv, int context) {
            if (context == ItemSlot.Context.HotbarItem && PreventUIClicksPlayer.IsUIDisabled()) {
                return;
            }
            orig(ref inv, context);
        }

        private void On_ItemSlot_RightClick_ItemArray_int_int(On_ItemSlot.orig_RightClick_ItemArray_int_int orig, Terraria.Item[] inv, int context, int slot) {
            if (context == ItemSlot.Context.HotbarItem && PreventUIClicksPlayer.IsUIDisabled()) {
                return;
            }
            orig(inv, context, slot);
        }

        private bool On_ItemSlot_OverrideLeftClick(On_ItemSlot.orig_OverrideLeftClick orig, Terraria.Item[] inv, int context, int slot) {
            return orig(inv, context, slot);
            if (context == ItemSlot.Context.HotbarItem && PreventUIClicksPlayer.IsUIDisabled()) {
                return false;
            }
            return orig(inv, context, slot);
        }
    }
}
