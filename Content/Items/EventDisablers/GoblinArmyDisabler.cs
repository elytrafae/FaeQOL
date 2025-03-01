using FaeQOL.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaeQOL.Content.Items.EventDisablers {
    internal class GoblinArmyDisabler : AbstractEventDisabler {
        public override bool IsEventDisabled {
            get => EventDisableSystem.isGoblinArmyDisabled;
            set => EventDisableSystem.isGoblinArmyDisabled = value;
        }
    }
}
