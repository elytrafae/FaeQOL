using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaeQOL.Systems.Config;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeQOL.Content.Items.ClassOaths {
    public class EmptyOath : ModItem {

        public override bool IsLoadingEnabled(Mod mod) {
            return ModContent.GetInstance<ServerConfig>().EnableOaths;
        }

        public override string Texture => (GetType().Namespace + ".OathBase").Replace('.', '/');
        public override string LocalizationCategory => base.LocalizationCategory + ".ClassOaths";

        public override void SetDefaults() {
            Item.width = 34;
            Item.height = 36;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.Wood, 5)
                .Register();
        }



    }
}
