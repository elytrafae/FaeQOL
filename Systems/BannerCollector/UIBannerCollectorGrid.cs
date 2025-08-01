using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;

namespace FaeQOL.Systems.BannerCollector {
    internal class UIBannerCollectorGrid : UIGrid {

        public override void LeftClick(UIMouseEvent evt) {
            base.LeftClick(evt);
            AnyClick(evt);
        }

        public override void RightClick(UIMouseEvent evt) {
            base.RightClick(evt);
            AnyClick(evt);
        }

        private void AnyClick(UIMouseEvent evt) {
            if (BannerCollectorActivatorSystem.IsBanner(Main.mouseItem.type) && IsClickedObjectAChild(this, evt.Target)) {
                Main.LocalPlayer.GetModPlayer<BannerCollectorModPlayer>().StackBannerIntoBannerInventory(Main.LocalPlayer.HeldItem);
                Main.mouseItem = Main.LocalPlayer.HeldItem;
            }
        }

        private bool IsClickedObjectAChild(UIElement parent, UIElement target) {
            if (parent.Equals(target)) {
                return true;
            }
            foreach (UIElement child in Children) {
                if (target.Equals(child)) {
                    return true;
                }
            }
            return false;
        }

    }
}
