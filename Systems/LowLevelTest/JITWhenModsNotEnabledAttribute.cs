using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace FaeQOL.Systems.LowLevelTest {
    public sealed class JITWhenModsNotEnabledAttribute : MemberJitAttribute {
        public readonly string[] Names;

        public JITWhenModsNotEnabledAttribute(params string[] names) {
            Names = names ?? throw new ArgumentNullException(nameof(names));
        }

        public override bool ShouldJIT(MemberInfo member) => !Names.All(ModLoader.HasMod);
    }
}
