using Terraria;
using Terraria.ID;

namespace FaeQOL.Content.Items.AltMimicSpawners {
    public class RegularMimicKey : LeftInChestActionItem {
        public override int NPCToSpawn(Player player) => player.ZoneSnow ? NPCID.IceMimic : NPCID.Mimic;

        public override void SetDefaults() {
            Item.CloneDefaults(ItemID.NightKey);
            Item.width = 18;
            Item.height = 40;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.GoldenKey)
                .AddIngredient(ItemID.SoulofNight, 5)
                .AddIngredient(ItemID.SoulofLight, 5)
                .Register();
        }

    }
}
