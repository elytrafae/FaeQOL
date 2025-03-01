using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeQOL.Content.Items.TestItems
{
    internal class LockedLihzardDoor : ModItem {

        public override bool IsLoadingEnabled(Mod mod) {
            return FaeQOL.TEST_MODE;
        }

        public override void SetDefaults() {
            Item.DefaultToPlaceableTile(TileID.ClosedDoor, 11);
        }

    }
}
