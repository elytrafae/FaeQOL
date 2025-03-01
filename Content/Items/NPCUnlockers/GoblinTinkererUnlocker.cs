using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace FaeQOL.Content.Items.NPCUnlockers {
    internal class GoblinTinkererUnlocker : AbstractNPCUnlocker {
        public override int NPCToUnlock => NPCID.GoblinTinkerer;
        public override bool IsNPCSaved { 
            get => NPC.savedGoblin; 
            set => NPC.savedGoblin = true; 
        }

        public override int Width => 40;

        public override int Height => 40;

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.SpikyBall, 25)
                .AddTile(DEFAULT_WORKBENCH)
                .Register();
        }
    }
}
