using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeQOL.Content.Items {
    // It's abstract so that I can disable it for release
    internal abstract class TestItem : ModItem {

        public override void SetDefaults() {
            Item.DefaultToPlaceableTile(TileID.ClosedDoor, 11);
        }

    }
}
