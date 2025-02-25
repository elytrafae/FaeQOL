using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeQOL.Content.Items {
    public class GreedySoulFragment : ModItem {

        public override void SetDefaults() {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = Terraria.Item.CommonMaxStack;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Terraria.Item.buyPrice(0, 0, 0, 5);
        }

    }
}
