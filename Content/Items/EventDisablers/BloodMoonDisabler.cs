using FaeQOL.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace FaeQOL.Content.Items.EventDisablers {
    internal class BloodMoonDisabler : AbstractEventDisabler {
        public override bool IsEventDisabled {
            get => EventDisableSystem.isBloodMoonDisabled; 
            set => EventDisableSystem.isBloodMoonDisabled = value; 
        }
    }
}
