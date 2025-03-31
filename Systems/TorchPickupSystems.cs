using FaeQOL.Content.BuilderToggles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FaeQOL.Systems {
    public class TorchPickupGlobalItem : GlobalItem {

        private static bool IsBiomeTorch(Item item) {
            return ItemSets.IsTorchGodTorch.Contains(item.type);
        }

        private static bool IsBiomeCampfire(Item item) {
            return ItemSets.IsTorchGodCampfire.Contains(item.type);
        }

        public override bool OnPickup(Item item, Player player) {
            if (ModContent.GetInstance<TorchPickupBuilderToggle>().CurrentState != 1) {
                return true;
            } 
            if (IsBiomeTorch(item)) {
                int stack = item.stack;
                item.ChangeItemType(ItemID.Torch);
                item.stack = stack;
                return true;
            }
            if (IsBiomeCampfire(item)) {
                int stack = item.stack;
                item.ChangeItemType(ItemID.Campfire);
                item.stack = stack;
                return true;
            }
            return true;
        }


    }
}
