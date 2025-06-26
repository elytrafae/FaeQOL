using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeQOL.Content.Items {
    internal class SlimeRainStarter : ModItem {

        public override void SetDefaults() {
            Item.noMelee = true;
            Item.value = 0;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.UseSound = SoundID.Item155;
            Item.width = 40;
            Item.height = 42;
        }

        public override bool CanUseItem(Player player) {
            return !Main.slimeRain && Main.dayTime;
        }

        public override bool? UseItem(Player player) {
            if (Main.netMode != NetmodeID.MultiplayerClient) {
                Main.StopRain();
                Main.StartSlimeRain(true);
            }
            return true;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.SlimeCrown)
                .AddIngredient(ItemID.Gel, 20)
                .AddRecipeGroup(RecipeGroupID.IronBar, 5)
                .Register();
        }

    }
}
