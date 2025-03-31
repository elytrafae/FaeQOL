using Terraria;
using Terraria.ID;

namespace FaeQOL.Content.Items.AltMimicSpawners {
    public class CrimsonMimicKey : LeftInChestActionItem {
        public override int NPCToSpawn(Player player) => NPCID.BigMimicCrimson;

        public override void SetDefaults() {
            Item.CloneDefaults(ItemID.NightKey);
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.NightKey)
                .AddIngredient(ItemID.Vertebrae, 5)
                .Register();
        }

    }
}
