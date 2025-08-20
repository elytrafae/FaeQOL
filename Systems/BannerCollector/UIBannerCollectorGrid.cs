using Microsoft.Xna.Framework.Graphics;
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
#if DEBUG
    internal class UIBannerCollectorGrid : UIGrid {

        public override void LeftClick(UIMouseEvent evt) {
            base.LeftClick(evt);
            TryPutMouseStackIntoBannerInventory();
        }

        public override void RightClick(UIMouseEvent evt) {
            base.RightClick(evt);
            
        }

        public static void TryPutMouseStackIntoBannerInventory() {
            if (BannerCollectorActivatorSystem.IsBanner(Main.mouseItem.type)) {
                Main.LocalPlayer.GetModPlayer<BannerCollectorModPlayer>().StackBannerIntoBannerInventory(Main.LocalPlayer.HeldItem);
                Main.mouseItem = Main.LocalPlayer.HeldItem;
            }
        }

        private static bool IsClickedObjectAChild(UIElement parent, UIElement target) {
            if (parent.Equals(target)) {
                return true;
            }
            foreach (UIElement child in parent.Children) {
                /*
                if (target.Equals(child)) {
                    return true;
                }*/
                if (IsClickedObjectAChild(child, target)) {
                    return true;
                }
            }
            return false;
        }

    }
#endif
}
