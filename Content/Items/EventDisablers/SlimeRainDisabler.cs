using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaeQOL.Systems;
using Terraria.ModLoader;

namespace FaeQOL.Content.Items.EventDisablers {
    internal class SlimeRainDisabler : AbstractEventDisabler {
        public override EventDisableSystem.EventDisabledData EventDisableData => EventDisableSystem.slimeRain;

        public override int ItemToGetThisFrom => ModContent.ItemType<SlimeRainStarter>();

        public override int Width => throw new NotImplementedException();

        public override int Height => throw new NotImplementedException();
    }
}
