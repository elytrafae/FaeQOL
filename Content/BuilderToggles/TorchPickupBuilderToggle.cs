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
		public static LocalizedText TorchPickup { get; private set; }
		public static LocalizedText[] ColorText { get; private set; }

		public override void SetStaticDefaults() {
			CurrentColorText = this.GetLocalization("CurrentColor");
			ColorText = Enumerable.Range(0, 4).Select(i => this.GetLocalization($"Color_{i}")).ToArray();
		}

		public override bool Active() => Main.LocalPlayer.HeldItem.IsAir;

		public override int NumberOfStates => 4;

		public override string DisplayValue() {
			return CurrentColorText.Format(ColorText[CurrentState].Value);
		}

		public override bool Draw(SpriteBatch spriteBatch, ref BuilderToggleDrawParams drawParams) {
			Color[] colors = [Color.Red, Color.Blue, Color.Green, Color.Yellow];
			drawParams.Color = colors[CurrentState];
			return true;
		}


		// Right click to cycle through states backwards.
		public override void OnRightClick() {
			CurrentState -= 1;
			if (CurrentState < 0) {
				CurrentState = NumberOfStates - 1;
			}

			SoundEngine.PlaySound(SoundID.Coins);
		}
	}

}