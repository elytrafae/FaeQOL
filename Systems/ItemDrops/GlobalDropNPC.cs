using FaeQOL.Content.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeQOL.Systems.ItemDrops {
    internal class GlobalDropNPC : GlobalNPC {

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) {
            if (npc.type == NPCID.SkeletronHead) {
                LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Keychain>()));
                npcLoot.Add(notExpertRule);
            } else if (npc.type == NPCID.DemonTaxCollector) {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GreedySoulFragment>()));
            }
            
        }

    }
}
