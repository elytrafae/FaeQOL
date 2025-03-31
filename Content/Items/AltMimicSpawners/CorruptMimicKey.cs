using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeQOL.Content.Items.AltMimicSpawners {
    public abstract class CorruptMimicKey : ModItem {

        public override void SetDefaults() {
            Item.CloneDefaults(ItemID.NightKey);
        }

    }
}
