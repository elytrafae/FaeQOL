using FaeQOL.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace FaeQOL.Content.Items.EventDisablers {
    internal class BloodMoonDisabler : AbstractEventDisabler {
        public override EventDisableSystem.EventDisabledData EventDisableData => EventDisableSystem.bloodMoon;

        public override int ItemToGetThisFrom => ItemID.BloodMoonStarter;

        public override int Width => 26;

        public override int Height => 30;

        
    }
}
