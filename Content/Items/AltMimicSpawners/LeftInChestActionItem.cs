using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeQOL.Content.Items.AltMimicSpawners {

    // I should improve this system later, but for now I just want to see it work!
    public abstract class LeftInChestActionItem : ModItem {

        public abstract int NPCToSpawn(Player player);

        public virtual bool ProduceSpawnSmoke => true;

        public virtual bool ActiveCondition(Player player, int tileX, int tileY) {
            return true;
            //return Main.hardMode;
        }

        public virtual bool CanBeUsedInsideStorageTile(int tileType, int tileStyle) {
            // The second set of conditions may seem strange, but all we are trying to do is eliminating barrels and trash cans from the equasion!
            return TileID.Sets.BasicChest[tileType] && (tileType != TileID.Containers || tileStyle < 5 || tileStyle > 6);
        }

        public virtual void OnLeftInChest() { 
            
        }

    }
}
