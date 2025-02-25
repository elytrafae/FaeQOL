using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace FaeQOL.Content.Items.NPCUnlockers {
    internal class GolferUnlocker : AbstractNPCUnlocker {
        public override int NPCToUnlock => NPCID.Golfer;
        public override bool IsNPCSaved { 
            get => NPC.savedGolfer; 
            set => NPC.savedGolfer = value; 
        }
        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.FossilOre, 5)
                .AddIngredient(ItemID.AntlionMandible, 5)
                .AddTile(DEFAULT_WORKBENCH)
                .Register();
        }
    }
}
