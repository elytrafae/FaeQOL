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

        public static LocalizedText TorchMergeTutorial { get; private set; }
        public static LocalizedText CampfireMergeTutorial { get; private set; }

        public override void SetStaticDefaults() {
            TorchMergeTutorial = Mod.GetLocalization(nameof(TorchMergeTutorial));
            CampfireMergeTutorial = Mod.GetLocalization(nameof(CampfireMergeTutorial));
        }

        private static bool IsBiomeTorch(Item item) {
            return item.type != ItemID.Torch && ItemID.Sets.Torches[item.type];
        }

        private static bool IsBiomeCampfire(Item item) {
            return item.type != ItemID.Campfire && item.createTile > -1 && TileID.Sets.Campfire[item.createTile];
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

        public override bool CanRightClick(Item item) {
            return item.type == ItemID.Torch || item.type == ItemID.Campfire;
        }

        public override bool ConsumeItem(Item item, Player player) {
            return !CanRightClick(item); // This should only call the hook for THIS ModItem!
        }

        public override void RightClick(Item item, Player player) {
            if (Main.myPlayer != player.whoAmI) {
                return;
            }
            if ((item.type == ItemID.Torch && IsBiomeTorch(Main.mouseItem)) || (item.type == ItemID.Campfire && IsBiomeCampfire(item))) {
                int countToTransfer = Math.Min(Main.mouseItem.stack, item.maxStack - item.stack);
                item.stack += countToTransfer;
                Main.mouseItem.stack -= countToTransfer;
                if (Main.mouseItem.stack <= 0) {
                    Main.mouseItem.TurnToAir();
                }
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
            if (IsBiomeTorch(item)) {
                tooltips.Add(new TooltipLine(Mod, "TorchMergeTutorial", TorchMergeTutorial.Value));
            }
            if (IsBiomeCampfire(item)) {
                tooltips.Add(new TooltipLine(Mod, "CampfireMergeTutorial", CampfireMergeTutorial.Value));
            }
        }

    }
}
