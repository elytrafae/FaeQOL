using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace FaeQOL.Content.Items.DevSet {

    internal abstract class AbstractFaeGoggle : ModItem {

        public override void SetDefaults() {
            Item.SetShopValues(ItemRarityColor.Cyan9, Terraria.Item.sellPrice(gold: 5));
            Item.vanity = true;
            Item.accessory = true;
            Item.width = 32;
            Item.height = 20;
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player) {
            return !(equippedItem.ModItem is AbstractFaeGoggle && incomingItem.ModItem is AbstractFaeGoggle);
        }

    }
}
