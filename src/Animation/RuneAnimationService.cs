using System;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace TeleportationRunes.src.Dkosher.Animation
{
    public class RuneAnimationService
    {
        public static void LiftRune(EntityAgent byEntity, float secondsUsed)
        {
            ModelTransform tf = new ModelTransform();
            tf.EnsureDefaultValues();

            float offset = GameMath.Clamp(secondsUsed * 4f, 0, 1f);

            if (secondsUsed < 0.35f)
            {
                tf.Translation.Set(-offset / 5, offset / 5, offset / 5);
            }
            else
            {
                var rand = new Random();

                tf.Translation.Set(
                  -offset / 5 + ((float)rand.NextDouble() * 0.02f),
                  offset / 5 + ((float)rand.NextDouble() * 0.02f),
                  offset / 5 + ((float)rand.NextDouble() * 0.02f)
                );
            }
            byEntity.Controls.UsingHeldItemTransformBefore = tf;
        }
    }
}
