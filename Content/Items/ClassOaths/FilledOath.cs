using FaeQOL.Systems.Oaths;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeQOL.Content.Items.ClassOaths {

    [Autoload(false)]
    public class FilledOath : ModItem {

        public static Asset<Texture2D> WritingTexture;

        public class OathLoader : ILoadable {
            public void Load(Mod mod) {
                FilledOath.WritingTexture = mod.Assets.Request<Texture2D>("Content/Items/ClassOaths/OathWriting");
                mod.AddContent(new FilledOath(DamageClass.Default, "Classless", Color.White, []));
                mod.AddContent(new FilledOath(DamageClass.Melee, "Melee", new Color(235, 25, 25), []));
                mod.AddContent(new FilledOath(DamageClass.Ranged, "Ranged", new Color(0, 180, 60), [new(ItemID.WoodenBow, 1), new(ItemID.WoodenArrow, 100)]));
                mod.AddContent(new FilledOath(DamageClass.Magic, "Magic", new Color(15, 135, 255), [new(ItemID.WandofSparking, 1), new(ItemID.ManaCrystal, 1)]));
                mod.AddContent(new FilledOath(DamageClass.Summon, "Summoner", new Color(255, 115, 195), [new(ItemID.BabyBirdStaff, 1), new(ItemID.BlandWhip, 1)]));
                mod.AddContent(new FilledOath(DamageClass.Throwing, "Thrower", new Color(206, 132, 227), [])); // This also applies to Calamity Rogue. Used its color, too
            }

            public void Unload() {
            }
        }

        public override string LocalizationCategory => base.LocalizationCategory + ".ClassOaths";

        protected override bool CloneNewInstances => true;

        readonly DamageClass damageClass;
        readonly string name;
        readonly Color color;
        readonly Tuple<int, int>[] itemsToGiveOnCraft;

        public FilledOath(DamageClass damageClass, string name, Color color, Tuple<int, int>[] itemsToGiveOnCraft) {
            this.damageClass = damageClass;
            this.name = name + "Oath";
            this.color = color;
            this.itemsToGiveOnCraft = itemsToGiveOnCraft;
        }

        //private FilledOath() { }

        public override string Name => name;
        public override string Texture => (GetType().Namespace + ".OathBase").Replace('.', '/');

        public override void SetDefaults() {
            Item.rare = ItemRarityID.White;
            Item.value = 1;
            Item.width = 32;
            Item.height = 32;
        }

        public override void UpdateInventory(Player player) {
            player.GetModPlayer<OathPlayer>().oathsActive.Add(damageClass);
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<EmptyOath>())
                .AddOnCraftCallback(CraftCallback)
                .Register();
        }

        public void CraftCallback(Recipe recipe, Item item, List<Item> consumedItems, Item destinationStack) {
            OathPlayer op = Main.LocalPlayer.GetModPlayer<OathPlayer>();
            if (op.DidCraftOathAlready) { return; }

            foreach (Tuple<int, int> tuple in itemsToGiveOnCraft) {
                Main.LocalPlayer.QuickSpawnItem(destinationStack.GetSource_FromThis(), tuple.Item1, tuple.Item2);
            }
            if (itemsToGiveOnCraft.Length > 0) {
                op.DidCraftOathAlready = true;
            }
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) {
            spriteBatch.Draw(WritingTexture.Value, position, null, this.color, 0, origin, scale, SpriteEffects.None, 0);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI) {
            // Whoever made DrawItem_GetBasics private, I hope they are proud of themselves
            //Type mainType = typeof(Main);
            //MethodInfo drawItemGetBasics = mainType.GetMethod("DrawItem_GetBasics", BindingFlags.NonPublic | BindingFlags.Instance);
            //drawItemGetBasics.Invoke(Main.instance, Item, Item.whoAmI, out Texture2D texture, out Rectangle frame, out Rectangle glowmaskFrame);
            //Main.DrawItem_GetBasics(Item, Item.whoAmI, out var texture, out var frame, out var glowmaskFrame);

            // I made the item's size as big as the frame should be.
            // I can't be asked to fix the commented out code above.
            // This is all stupider than it needs to be.
            Rectangle frame = Item.getRect();
            Vector2 vector = frame.Size() / 2f;
            Vector2 vector2 = new((float)(Item.width / 2) - vector.X, (float)(Item.height - frame.Height));
            Vector2 drawPos = Item.position - Main.screenPosition + vector + vector2;
            //Vector2 drawPos = Item.position - Main.screenPosition + new Vector2(0, 32);
            Color newColor = lightColor.MultiplyRGBA(color);
            //spriteBatch.Draw(WritingTexture.Value, drawPos, null, newColor, rotation, new Vector2(0, 32), scale, SpriteEffects.None, 0);
            spriteBatch.Draw(WritingTexture.Value, drawPos, null, newColor, rotation, vector, scale, SpriteEffects.None, 0f);
        }
    }
}
