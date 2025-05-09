using FaeQOL.Systems;
using FaeQOL.Systems.Config;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace FaeQOL.Content.Items {
    internal class Keychain : ModItem {

        public override bool IsLoadingEnabled(Mod mod) {
            return ModContent.GetInstance<ServerConfig>().EnableKeychain;
        }

        public Item[] keys = null;

        public static LocalizedText ContentsText { get; private set; }
        public static Asset<Texture2D> KeysTexture { get; private set; }
        public static readonly List<int> KEYS_IN_RENDER_ORDER = [ItemID.DungeonDesertKey, ItemID.CorruptionKey, ItemID.CrimsonKey, ItemID.TempleKey, ItemID.ShadowKey, ItemID.GoldenKey, ItemID.HallowedKey, ItemID.FrozenKey, ItemID.JungleKey];
        const int FRAME_WIDTH = 86;
        const int FRAME_HEIGHT = 80;

        public override void SetStaticDefaults() {
            ContentsText = this.GetLocalization(nameof(ContentsText));
            KeysTexture = ModContent.Request<Texture2D>(Texture + "_Keys");
        }

        public override void SetDefaults() {
            Item.width = FRAME_WIDTH;
            Item.height = FRAME_HEIGHT;
            Item.maxStack = 1;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 0, 0, 30);
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.Chain, 7)
                .AddIngredient(ItemID.Rope, 2)
                .AddTile(TileID.HeavyWorkBench)
                .Register();
        }

        public override ModItem Clone(Item item) {
            Keychain clone = (Keychain)base.Clone(item);
            clone.keys = (Item[])keys?.Clone(); // note the ? here is important, keys may be null if spawned from other mods which don't call OnCreate
            return clone;
        }

        public override void OnCreated(ItemCreationContext context) {
            TryInitialize();
        }

        private void TryInitialize() {
            if (keys != null) {
                return;
            }
            keys = new Item[20];
            for (int i = 0; i < keys.Length; i++) {
                keys[i] = new Item(ItemID.None);
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips) {
            if (keys == null) // keys may be null if spawned from other mods which don't call OnCreate
                return;

            List<TooltipLine> cache = new List<TooltipLine>();

            for (int i = 0; i < keys.Length; i++) {
                if (keys[i] == null || keys[i].IsAir) {
                    continue;
                }
                Item key = keys[i];
                string suffix = "";
                if (key.stack > 1) {
                //if (key.maxStack > 1 || key.stack > 1) {
                    suffix += " x" + key.stack;
                }
                TooltipLine tooltipLine = new TooltipLine(Mod, "Keys" + i, "[i:" + key.type + "] " + key.AffixName() + suffix) { OverrideColor = ItemRarity.GetColor(key.rare) };
                cache.Add(tooltipLine);
            }

            if (cache.Count > 0) {
                tooltips.Add(new TooltipLine(Mod, "KeysHeader", ContentsText.Value));
                foreach (TooltipLine line in cache) {
                    tooltips.Add(line);
                }
            }
        }

        // NOTE: The tag instance provided here is always empty by default.
        // Read https://github.com/tModLoader/tModLoader/wiki/Saving-and-loading-using-TagCompound to better understand Saving and Loading data.
        public override void SaveData(TagCompound tag) {
            TryInitialize();
            tag.Set("Keys", keys, true);
        }

        public override void LoadData(TagCompound tag) {
            if (tag.TryGet("Keys", out Item[] val)) {
                keys = val;
            } else {
                TryInitialize();
            }
        }

        public override bool CanUseItem(Player player) {
            CleanUpKeysArray();
            return !keys[0].IsAir;
        }

        public override bool? UseItem(Player player) {
            if (player == Main.LocalPlayer) {
                CleanUpKeysArray(); // Just for safety's sake
                int i = 0;
                while (i < keys.Length && keys[i] != null && !keys[i].IsAir) {
                    player.QuickSpawnItem(player.GetSource_ItemUse(Item), keys[i], keys[i].stack);
                    keys[i].TurnToAir();
                    i++;
                }
            }
            return null;
        }

        public override bool CanRightClick() {
            return (!Main.mouseItem.IsAir) && CustomSetsSystem.IsItemKey(Main.mouseItem.type);
        }

        public override void RightClick(Player player) {
            if (Main.mouseItem.favorited) {
                SoundEngine.PlaySound(SoundID.Shimmer2);
                return;
            }
            if (!AddItemIntoKeychain(Main.mouseItem)) {
                SoundEngine.PlaySound(SoundID.MenuClose);
            }
        }

        public override bool ConsumeItem(Player player) {
            return false;
        }

        public override void UpdateInventory(Player player) {
            MyModPlayer.Get(player).keychainsInInventory.Add(this);
        }

        public override void NetSend(BinaryWriter writer) {
            TryInitialize();
            CleanUpKeysArray();
            int k = 0;
            while (k < keys.Length && !keys[k].IsAir) {
                k++;
            }
            writer.Write(k);
            for (int i = 0; i < k; i++) {
                ItemIO.Send(keys[i], writer, true);
            }
        }

        public override void NetReceive(BinaryReader reader) {
            keys = null;
            TryInitialize();
            int k = reader.ReadInt32();
            for (int i = 0; i < k; i++) { 
                keys[i] = ItemIO.Receive(reader, true);
            }
        }

        private void CleanUpKeysArray() {
            int indexesToMoveBack = 0;
            for (int i = 0; i < keys.Length; i++) {
                if (keys[i] == null || keys[i].IsAir) {
                    indexesToMoveBack++;
                } else {
                    if (indexesToMoveBack > 0) {
                        keys[i - indexesToMoveBack] = keys[i];
                        keys[i] = new Item(ItemID.None); // "Why not call TurnToAir?" because then it would affect the instance we JUST moved over!
                    }
                }
            }
        }

        

        // PUBLICLY AVAILABLE STUFF //

        public bool AddItemIntoKeychain(Item item) {
            TryInitialize();
            CleanUpKeysArray();
            // Try to stack the key

            int i = 0;
            while (i < keys.Length) {
                // The key was not part of the keychain before. Add it!
                if (keys[i] == null || keys[i].IsAir) {
                    keys[i] = item.Clone();
                    item.TurnToAir(true);
                    return true;
                }
                // If the item is part of the keychain, attempt to stack it
                if (item.type == keys[i].type && ItemLoader.TryStackItems(keys[i], item, out int numTransferred, false)) {
                    if (numTransferred > 0) {
                        return true;
                    } else {
                        return false; // If the stack is already full, we return false!
                    }
                }
                i++;
            }

            // No suitable spot was found!
            return false;
        }

        public Item GetKeyOfTypeFromKeychain(int itemID) {
            return GetKeyOfTypeFromKeychain(ContentSamples.ItemsByType[itemID]);
        }

        // This exists so that keys differentiated by data within the same ID can be worked with in the future
        public Item GetKeyOfTypeFromKeychain(Item item) {
            TryInitialize();
            CleanUpKeysArray();

            int i = 0;
            while (i < keys.Length) {
                // The key was not part of the keychain.
                if (keys[i] == null || keys[i].IsAir) {
                    return null;
                }
                // If the item is part of the keychain, return the stack
                if (item.type == keys[i].type && ItemLoader.CanStack(keys[i], item)) {
                    return keys[i];
                }
                i++;
            }

            // End of array, not found!
            return null;
        }


        // RENDERING STUFF //
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) {
            BitArray flags = GetKeychainRenderFlags();
            for (int i = 0; i < KEYS_IN_RENDER_ORDER.Count; i++) {
                if (flags[i]) {
                    spriteBatch.Draw(KeysTexture.Value, position, new Rectangle(FRAME_WIDTH * i, 0, FRAME_WIDTH, FRAME_HEIGHT), Color.White, 0, origin, scale, SpriteEffects.None, 0);
                }
            }
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI) {
            BitArray flags = GetKeychainRenderFlags();
            Vector2 drawPos = Item.position - Main.screenPosition;
            for (int i = 0; i < KEYS_IN_RENDER_ORDER.Count; i++) {
                if (flags[i]) {
                    spriteBatch.Draw(KeysTexture.Value, drawPos, new Rectangle(FRAME_WIDTH * i, 0, FRAME_WIDTH, FRAME_HEIGHT), lightColor, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                }
            }
        }

        public BitArray GetKeychainRenderFlags() {
            TryInitialize();
            CleanUpKeysArray();
            BitArray flags = new(KEYS_IN_RENDER_ORDER.Count);
            int k = 0;
            while (k < keys.Length && !keys[k].IsAir) {
                int index = KEYS_IN_RENDER_ORDER.IndexOf(keys[k].type);
                if (index > -1) {
                    flags[index] = true;
                }
                k++;
            }
            return flags;
        }


    }
}
