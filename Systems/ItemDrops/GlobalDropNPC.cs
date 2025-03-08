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

        private static readonly int[] MIMIC_TYPES = [NPCID.Mimic, NPCID.IceMimic, NPCID.PresentMimic, NPCID.BigMimicCorruption, NPCID.BigMimicCrimson, NPCID.BigMimicHallow, NPCID.BigMimicJungle];

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) {
            if (npc.type == NPCID.SkeletronHead) {
                LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Keychain>()));
                npcLoot.Add(notExpertRule);
            } else if (npc.type == NPCID.DemonTaxCollector) {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulOfSleight>(), 1, 2, 3));
            } else if (NPCID.Sets.BelongsToInvasionPirate[npc.type]) {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulOfSleight>(), 10, 1, 2));
            } else if (MIMIC_TYPES.Contains(npc.type)) { 
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulOfSleight>(), 3, 1, 2));
            }
        }

    }
}
