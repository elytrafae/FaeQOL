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
            panel.Append(inventoryGrid);

            //UpdateSlots();

            Append(area);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            // Add draw condition here, if ever relevant
            UpdateSlots();
            base.Draw(spriteBatch);
        }

        // Here we draw our UI
        protected override void DrawSelf(SpriteBatch spriteBatch) {
            base.DrawSelf(spriteBatch);
        }

        public override void Update(GameTime gameTime) {
            Main.LocalPlayer.GetModPlayer<BannerCollectorModPlayer>().RemoveEmptyStacksFromBannerInventory();
            UpdateSlots();
            base.Update(gameTime);
        }

        private void UpdateSlots() {

            // Testing for now
            if (viewonlySlots.Count <= 0) {
                foreach (var pair in BannerCollectorActivatorSystem.itemToBannerIDs) {
                    viewonlyItems.Add(new Item(pair.Key));
                    UIBannerItemSlot slot = new UIBannerItemSlot(viewonlyItems, viewonlyItems.Count-1, 1f, ItemSlot.Context.BankItem);
                    slot.viewonly = true;
                    viewonlySlots.Add(slot);
                    inventoryGrid.Add(slot);
                }
            }

            BannerCollectorModPlayer modPlayer = Main.LocalPlayer.GetModPlayer<BannerCollectorModPlayer>();
            List<Item> BannerInventory = modPlayer.BannerInventory;
            if (BannerInventory.Count == slots.Count) {
                return;
            }

            if (BannerInventory.Count > slots.Count) {
                for (int i = slots.Count; i < BannerInventory.Count; i++) {
                    UIBannerItemSlot slot = new UIBannerItemSlot(modPlayer.BannerInventory, i, 1f, ItemSlot.Context.GuideItem);
                    slots.Add(slot);
                    inventoryGrid.Add(slot);
                }
            } else {
                for (int i = slots.Count - 1; i >= BannerInventory.Count; i--) { 
                    inventoryGrid.Remove(slots[i]);
                    slots.RemoveAt(i);
                }
            }
                
        }

    }

    // This class will only be autoloaded/registered if we're not loading on a server
    [Autoload(Side = ModSide.Client)]
    internal class BannerCollectorUISystem : ModSystem {
        private UserInterface BannerCollectorUserInterface;

        internal BannerCollectorUI BannerCollectorUI;

        public override void Load() {
            BannerCollectorUI = new();
            BannerCollectorUserInterface = new();
            BannerCollectorUserInterface.SetState(BannerCollectorUI);
        }

        public override void UpdateUI(GameTime gameTime) {
            BannerCollectorUserInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
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
