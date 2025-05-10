using FaeQOL.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace FaeQOL.Content.Items.EventDisablers {
    internal class SolarEclipseDisabler : AbstractEventDisabler {
        public override EventDisableSystem.EventDisabledData EventDisableData => EventDisableSystem.solarEclipse;

        public override int ItemToGetThisFrom => ItemID.SolarTablet;

        public override int Width => 24;

        public override int Height => 50;
    }
}
