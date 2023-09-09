using System;
using TeleporatationRunes;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace TeleportationRunes.src.Dkosher.Sound
{
    public class SoundService
    {
        public static SoundParams SoundParams(Vec3f position, ItemRune rune)
        {
            String fileName = rune.Attributes["tpsound"].AsString();
            return new SoundParams()
            {
                Location = new AssetLocation("tprunes", "sounds/rune/" + fileName),
                ShouldLoop = false,
                Position = position,
                DisposeOnFinish = true,
                Volume = 0.8f
            };
        }

        public static SoundParams FailedSoundParams(Vec3f position)
        {
            return new SoundParams()
            {
                Location = new AssetLocation("tprunes", "sounds/rune/fail.ogg"),
                ShouldLoop = false,
                Position = position,
                DisposeOnFinish = true,
                Volume = 1f
            };
        }
    }
}
