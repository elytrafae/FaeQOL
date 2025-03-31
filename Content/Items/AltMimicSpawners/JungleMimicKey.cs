using Terraria;
using Terraria.ID;

namespace FaeQOL.Content.Items.AltMimicSpawners {
    public class JungleMimicKey : LeftInChestActionItem {
        public override int NPCToSpawn(Player player) => NPCID.BigMimicJungle;

        public override void SetDefaults() {
            Item.CloneDefaults(ItemID.NightKey);
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.LightKey)
                .AddIngredient(ItemID.JungleSpores, 5)
                .Register();
        }

    }
}
