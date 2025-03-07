using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeQOL.Systems.ItemDrops {
    internal class GlobalRecipesSystem : ModSystem {

        public override void AddRecipes() {
            Recipe.Create(ItemID.BloodMoonStarter, 1)
                .AddIngredient(ItemID.RottenChunk, 10)
                .AddTile(TileID.DemonAltar)
                .Register();

            Recipe.Create(ItemID.BloodMoonStarter, 1)
                .AddIngredient(ItemID.Vertebrae, 10)
                .AddTile(TileID.DemonAltar)
                .Register();
        }

    }
}
