using FaeQOL.Content.BuilderToggles;
using FaeQOL.Systems.Config;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;

namespace FaeQOL.Systems.BannerCollector {
    internal class BannerCollectorUI : UIState {

        private UIElement area;
        private UIPanel panel;
        private UIBannerCollectorGrid inventoryGrid;
        private UIScrollbar inventoryScrollbar;
        private List<UIBannerItemSlot> slots = new();
        private List<Item> viewonlyItems = new();
        private List<UIBannerItemSlot> viewonlySlots = new();

        //private const string SpritePrefix = nameof(FaeQOL) + "/Assets/MiscSprites/";

        public override void OnInitialize() {
            area = new UIElement();
            area.Width.Set(-400, 0.6f);
            area.Height.Set(0, 0.8f);
            area.Top.Set(0, 0.1f);
            area.Left.Set(250, 0.2f);

            panel = new UIPanel();
            panel.Width.Set(0, 1f);
            panel.Height.Set(0, 1f);
            panel.Top.Set(0, 0f);
            panel.Left.Set(0, 0f);
            area.Append(panel);

            inventoryGrid = new UIBannerCollectorGrid();
            inventoryGrid.Width.Set(0, 1f);
            inventoryGrid.Height.Set(0, 1f);
            inventoryGrid.MaxHeight.Set(0, 1f);
            inventoryGrid.ListPadding = 2.5f;
            inventoryGrid.SetScrollbar(inventoryScrollbar);
            panel.Append(inventoryGrid);

            inventoryScrollbar = new UIScrollbar();
            inventoryScrollbar.Height.Set(-10f, 1f);
            inventoryScrollbar.Left.Set(-inventoryScrollbar.Width.Pixels + 5f, 1f);
            inventoryScrollbar.Top.Set(5f, 0f);
            panel.Append(inventoryScrollbar);

            inventoryGrid.Width.Set(-inventoryScrollbar.Width.Pixels, 1f);
            inventoryGrid.SetScrollbar(inventoryScrollbar);

            Append(area);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            // Add draw condition here, if ever relevant
            if (!BannerCollectorToggle.Get().IsOn) {
                return;
            }
            UpdateSlots();
            base.Draw(spriteBatch);
        }

        // Here we draw our UI
        protected override void DrawSelf(SpriteBatch spriteBatch) {
            base.DrawSelf(spriteBatch);
        }

        public override void Update(GameTime gameTime) {
            Main.LocalPlayer.GetModPlayer<BannerCollectorModPlayer>().RemoveEmptyStacksFromBannerInventory();
            if (!BannerCollectorToggle.Get().IsOn) {
                return;
            }
            UpdateSlots();
            base.Update(gameTime);
        }

        public void UpdateGridOrder() { 
            inventoryGrid.UpdateOrder();
        }

        private void UpdateSlots() {

            BannerCollectorModPlayer modPlayer = Main.LocalPlayer.GetModPlayer<BannerCollectorModPlayer>();
            modPlayer.RemoveEmptyStacksFromBannerInventory();
            List<Item> BannerInventory = modPlayer.BannerInventory;

            if (BannerInventory.Count == slots.Count) {
                return;
            }

            viewonlyItems.Clear();
            HashSet<int> viewOnlyIDs = new HashSet<int>();
            foreach (var pair in BannerCollectorActivatorSystem.itemToBannerIDs) {
                viewOnlyIDs.Add(pair.Key);
            }
            foreach (Item banner in BannerInventory) {
                viewOnlyIDs.Remove(banner.type);
            }

            foreach (var id in viewOnlyIDs) {
                viewonlyItems.Add(new Item(id));
            }

            if (BannerInventory.Count > slots.Count) {
                for (int i = slots.Count; i < BannerInventory.Count; i++) {
                    UIBannerItemSlot slot = new UIBannerItemSlot(modPlayer.BannerInventory, i, 1f, false);
                    slots.Add(slot);
                    inventoryGrid.Add(slot);
                }
            } else {
                for (int i = slots.Count - 1; i >= BannerInventory.Count; i--) { 
                    inventoryGrid.Remove(slots[i]);
                    slots.RemoveAt(i);
                }
            }

            if (viewonlyItems.Count > viewonlySlots.Count) {
                for (int i = viewonlySlots.Count; i < viewonlyItems.Count; i++) {
                    UIBannerItemSlot slot = new UIBannerItemSlot(viewonlyItems, i, 1f, true);
                    viewonlySlots.Add(slot);
                    inventoryGrid.Add(slot);
                }
            } else {
                for (int i = viewonlySlots.Count - 1; i >= viewonlyItems.Count; i--) {
                    inventoryGrid.Remove(viewonlySlots[i]);
                    viewonlySlots.RemoveAt(i);
                }
            }


            UpdateGridOrder();

        }

    }

    // This class will only be autoloaded/registered if we're not loading on a server
    [Autoload(Side = ModSide.Client)]
    internal class BannerCollectorUISystem : ModSystem {
        private UserInterface BannerCollectorUserInterface;

        internal static BannerCollectorUI BannerCollectorUI;

        public override void Load() {
            BannerCollectorUI = new();
            BannerCollectorUserInterface = new();
            BannerCollectorUserInterface.SetState(BannerCollectorUI);
        }

        public override void UpdateUI(GameTime gameTime) {
            BannerCollectorUserInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (resourceBarIndex != -1) {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    nameof(FaeQOL) + ": BannerCollector",
                    delegate {
                        BannerCollectorUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
