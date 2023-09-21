using TeleporatationRunes;
using TeleportationRunes.src.Dkosher.Particles;
using TeleportationRunes.src.Dkosher.Sound;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace TeleportationRunes.src.Dkosher.Teleportation
{
    public class TeleportService
    {
        public const int DURABILITY_PER_BLOCK = 10;

        public static void Start(EntityAgent byEntity, ItemRune rune)
        {

            rune.Teleported = false;
            rune.Validated = false;
            rune.RunAnimation = true;
            rune.InitialPos = byEntity.Pos.AsBlockPos;

            if (byEntity.World is IClientWorldAccessor world)
            {
                SoundParams soundParams = SoundService.SoundParams(byEntity.Pos.XYZ.ToVec3f(), rune);
                rune.Sound = world.LoadSound(soundParams);
                rune.Sound?.Start();
            }
        }

        public static bool? Teleport(EntityAgent byEntity, ItemRune rune, Vec3d pos, ItemSlot slot)
        {
            Vec3d tpPosition = TeleportPositionService.GetRandomizePosition(pos, byEntity); ;

            if (tpPosition == null)
            {
                byEntity.World.SpawnParticles(ParticleFactory.Get(ParticleType.BLOCKED, byEntity));
                return !rune.Teleported;
            }
            byEntity.TeleportToDouble(tpPosition.X, tpPosition.Y, tpPosition.Z, () => rune.Teleported = true);
            BlockPos teleportTo = new BlockPos((int)pos.X, (int)pos.Y, (int)pos.Z);
            slot.Itemstack.Collectible.DamageItem(byEntity.World, byEntity, slot, (int)rune.InitialPos.DistanceTo(teleportTo) / DURABILITY_PER_BLOCK);
            slot.MarkDirty();

            byEntity.World.SpawnParticles(ParticleFactory.Get(ParticleType.TELEPORTED, byEntity));
            return null;
        }

        public static void Fail(EntityAgent byEntity, ItemRune rune)
        {
            rune.Sound?.Stop();

            if (byEntity.World is IClientWorldAccessor world && !rune.FailPlayed)
            {
                ILoadedSound failSound = world.LoadSound(SoundService.FailedSoundParams(byEntity.Pos.XYZ.ToVec3f()));
                failSound?.Start();
                rune.FailPlayed = true;
            }
        }
    }
}
