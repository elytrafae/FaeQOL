using FaeQOL.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace FaeQOL.Content.Items.EventDisablers {
    internal class GoblinArmyDisabler : AbstractEventDisabler {
        public override EventDisableSystem.EventDisabledData EventDisableData => EventDisableSystem.goblinArmy;

        public override int ItemToGetThisFrom => ItemID.GoblinBattleStandard;

        public override int Width => 30;

        public override int Height => 32;
    }
}
