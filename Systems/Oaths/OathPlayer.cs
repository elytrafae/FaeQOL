using SteelSeries.GameSense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace FaeQOL.Systems.Oaths
{
    internal class OathPlayer : ModPlayer {


        public readonly List<DamageClass> oathsActive = [];
        public bool DidCraftOathAlready = false;
        private const string OATH_CRAFT_KEY = "OathCrafted";

        public override void LoadData(TagCompound tag) {
            if (tag.ContainsKey(OATH_CRAFT_KEY)) {
                DidCraftOathAlready = tag.GetBool(OATH_CRAFT_KEY);
            }
        }

        public override void SaveData(TagCompound tag) {
            tag.Add(OATH_CRAFT_KEY, DidCraftOathAlready);
        }

        public override void ResetEffects() {
            oathsActive.Clear();
        }

        public override bool? CanHitNPCWithItem(Item item, NPC target) {
            if (DoOathsBlockDamage(item.DamageType)) {
                return false;
            }
            return null;
        }

        public override bool? CanHitNPCWithProj(Projectile proj, NPC target) {
            if (DoOathsBlockDamage(proj.DamageType)) {
                return false;
            }
            return null;
        }

        public override bool CanHitPvp(Item item, Player target) {
            return !DoOathsBlockDamage(item.DamageType);
        }

        public override bool CanHitPvpWithProj(Projectile proj, Player target) {
            return !DoOathsBlockDamage(proj.DamageType);
        }

        public bool DoOathsBlockDamage(DamageClass damageClass) {
            if (oathsActive.Count <= 0) {
                return false; // If no oaths are active, no type of damage is blocked
            }
            foreach (DamageClass oath in oathsActive) {
                if (oath == null) { continue; } // For some reason, this can happen?
                if (oath.Equals(damageClass) || damageClass.CountsAsClass(oath)) {
                    return false; // If any of the player's oaths match the damage's class, then we allow it
                }
            }
            return true; // None of the oaths matched, block damage!
        }

    }
}
