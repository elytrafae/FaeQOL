using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeQOL.Content.Items.NPCUnlockers {
    internal class TaxCollectorUnlocker : AbstractNPCUnlocker {
        public override int NPCToUnlock => NPCID.TaxCollector;

        public override bool IsNPCSaved { 
            get => NPC.savedTaxCollector; 
            set => NPC.savedTaxCollector = value; 
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient<GreedySoulFragment>(10)
                .AddTile(DEFAULT_WORKBENCH)
                .Register();
        }
    }
}
