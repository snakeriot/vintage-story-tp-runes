using TeleporatationRunes;
using TeleportationRunes.src.Dkosher.Particles;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace TeleportationRunes.src.Dkosher.Teleportation
{
    public class TeleportPositionValidator
    {

        public static void Validate(Vec3d pos, ItemSlot slot, EntityAgent byEntity, ItemRune rune, ICoreServerAPI sapi)
        {
            int chunkX = (int)pos.X / byEntity.Api.World.BlockAccessor.ChunkSize;
            int chunkZ = (int)pos.Z / byEntity.Api.World.BlockAccessor.ChunkSize;

            sapi.WorldManager.LoadChunkColumnPriority(chunkX, chunkZ, new ChunkLoadOptions()
            {
                OnLoaded = () => Validate(pos, slot, byEntity, rune)
            });
        }

        private static void Validate(Vec3d pos, ItemSlot slot, EntityAgent byEntity, ItemRune rune)
        {
            BEBeacon beacon = byEntity.World.BlockAccessor.GetBlockEntity(new BlockPos((int)pos.X, (int)pos.Y, (int)pos.Z)) as BEBeacon;

            if (beacon == null)
            {
                byEntity.World.SpawnParticles(ParticleFactory.Get(ParticleType.DETACHED, byEntity));
                rune.SetName(null, slot);
                TeleportPositionService.SavePosition(null, slot, null);
                rune.Teleported = true;
                slot.Itemstack.Attributes.SetBool("validationFailed", true);
                slot.MarkDirty();
            }
            rune.Validated = true;
        }
    }
}
