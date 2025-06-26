using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaeQOL.Systems;
using Terraria.ID;

namespace FaeQOL.Content.Items.EventDisablers {
    internal class PirateInvasionDisabler : AbstractEventDisabler {
        public override EventDisableSystem.EventDisabledData EventDisableData => EventDisableSystem.pirateInvasion;
        public override int ItemToGetThisFrom => ItemID.PirateMap;
        public override int Width => 28;
        public override int Height => 28;
    }
}
