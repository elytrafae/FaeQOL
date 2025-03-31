using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Enums;
using Terraria.ModLoader;

namespace FaeQOL.Content.Items.DevSet {

    [AutoloadEquip(EquipType.Body)]
    internal class FaeDress : ModItem {

        public override void SetDefaults() {
            Item.SetShopValues(ItemRarityColor.Cyan9, Terraria.Item.sellPrice(gold: 5));
            Item.vanity = true;
        }

    }
}
