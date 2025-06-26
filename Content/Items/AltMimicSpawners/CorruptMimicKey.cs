using Terraria;
using Terraria.ID;

namespace FaeQOL.Content.Items.AltMimicSpawners {
    public class CorruptMimicKey : LeftInChestActionItem {
        public override int NPCToSpawn(Player player) => NPCID.BigMimicCorruption;

        public override void SetDefaults() {
            Item.CloneDefaults(ItemID.NightKey);
            Item.width = 26;
            Item.height = 40;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.NightKey)
                .AddIngredient(ItemID.RottenChunk, 5)
                .Register();
        }

    }
}
