using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

// TODO: Continue this (below code is from examplemod)
namespace FaeQOL.Content.BuilderToggles {

    public class TorchPickupBuilderToggle : BuilderToggle {
		public static LocalizedText OnText { get; private set; }
		public static LocalizedText OffText { get; private set; }

		public override void SetStaticDefaults() {
			OnText = this.GetLocalization(nameof(OnText));
			OffText = this.GetLocalization(nameof(OffText));
		}

		public override bool Active() => Main.LocalPlayer.unlockedBiomeTorches;

		public override int NumberOfStates => 2;

		public override string DisplayValue() {
			return CurrentState == 1 ? OnText : OffValue;
		}

		public override bool Draw(SpriteBatch spriteBatch, ref BuilderToggleDrawParams drawParams) {
			// TODO: Add rendering
			// CurrentState
			return true;
		}

	}

}