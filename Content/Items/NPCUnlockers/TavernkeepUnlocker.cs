using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace FaeQOL.Content.Items.NPCUnlockers {
    internal class TavernkeepUnlocker : AbstractNPCUnlocker {
        public override int NPCToUnlock => NPCID.DD2Bartender;
        public override bool IsNPCSaved { 
            get => NPC.savedBartender; 
            set => NPC.savedBartender = value; 
        }

        public override int Width => 28;

        public override int Height => 30;

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.Ale)
                .AddCondition(Condition.DownedEowOrBoc)
                .AddTile(DEFAULT_WORKBENCH)
                .Register();
        }
    }
}
