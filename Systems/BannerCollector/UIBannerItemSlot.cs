using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;

namespace FaeQOL.Systems.BannerCollector {

    // Copy-paste from FaeCustomUIItemSlot, except it works with a List<Item>!
    public class UIBannerItemSlot : UIElement {

        private List<Item> inventory;
        private readonly int slot;
        private readonly int context;
        private readonly float scale;
        public bool viewonly;

        // The context is for visuals only!
        public UIBannerItemSlot(List<Item> inventory, int slot, float scale, int context) {
            this.inventory = inventory;
            this.slot = slot;
            this.context = context;
            this.scale = scale;
            this.viewonly = false;

            Width.Set(TextureAssets.InventoryBack9.Width() * scale, 0f);
            Height.Set(TextureAssets.InventoryBack9.Height() * scale, 0f);
        }

        public override int CompareTo(object obj) {
            if (obj is UIBannerItemSlot other) {
                if (this.viewonly != other.viewonly) {
                    return this.viewonly ? 1 : -1;
                }
                if (inventory.IndexInRange(slot)) { return 1; }
                if (other.inventory.IndexInRange(other.slot)) { return -1; }
                Item thisItem = inventory[slot];
                Item otherItem = other.inventory[other.slot];
                if (thisItem.stack != otherItem.stack) {
                    return thisItem.stack - otherItem.stack;
                }
                return thisItem.type - otherItem.type;
            }
            return base.CompareTo(obj);
        }

        public virtual bool CanInsertItem(Item item) {
            return true;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch) {
            if (!inventory.IndexInRange(slot)) {
                // Silently return if the slot index no longer exists!
                return;
            }

            float oldScale = Main.inventoryScale;
            Main.inventoryScale = scale;
            Rectangle rectangle = GetDimensions().ToRectangle();

            // Ugly. UGLY hack to make the inventory slot actions still receive a ref, while Lists not providing any.
            Item item = inventory[slot];

            if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface) {
                Main.LocalPlayer.mouseInterface = true;
                if (viewonly) {
                    ItemSlot.OverrideHover(ref item, context);
                    ItemSlot.MouseHover(ref item, context);
                } else if (IsReadyToTakeItemsFromSlot(ref item) && CanInsertItem(Main.mouseItem)) {
                    // Handle handles all the click and hover actions based on the context.
                    ItemSlot.Handle(ref item, context);
                }
            }
            // Draw draws the slot itself and Item. Depending on context, the color will change, as will drawing other things like stack counts.
            ItemSlot.Draw(spriteBatch, ref item, context, rectangle.TopLeft());
            Main.inventoryScale = oldScale;
            inventory[slot] = item;
        }

        private bool IsReadyToTakeItemsFromSlot(ref Item item) { 
            return Main.mouseItem.IsAir || (item.type == Main.mouseItem.type && ItemLoader.CanStack(item, Main.mouseItem));
        }

    }
}
