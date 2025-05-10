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
            Item.width = Width;
            Item.height = Height;
        }

        public override bool ConsumeItem(Player player) {
            return !Main.slimeRain;
        }

        public override void OnConsumeItem(Player player) {
            Main.StopRain();
            Main.StartSlimeRain();
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.SlimeCrown)
                .AddIngredient(ItemID.Gel, 20)
                .Register();
        }

    }
}
