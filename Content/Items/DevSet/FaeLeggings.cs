using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria;

namespace FaeQOL.Content.Items.DevSet {
    [AutoloadEquip(EquipType.Legs)]
    internal class FaeLeggings : ModItem {

        public override void SetDefaults() {
            Item.SetShopValues(ItemRarityColor.Cyan9, Terraria.Item.sellPrice(gold: 5));
            Item.vanity = true;
            Item.width = 20;
            Item.height = 28;
        }

    }
}
