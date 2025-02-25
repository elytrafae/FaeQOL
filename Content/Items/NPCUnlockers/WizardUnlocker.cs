using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace FaeQOL.Content.Items.NPCUnlockers {
    internal class WizardUnlocker : AbstractNPCUnlocker {
        public override int NPCToUnlock => NPCID.Wizard;
        public override bool IsNPCSaved { 
            get => NPC.savedWizard; 
            set => NPC.savedWizard = value; 
        }
        public override SoundStyle UseSound => SoundID.MaxMana;
        
    }
}
