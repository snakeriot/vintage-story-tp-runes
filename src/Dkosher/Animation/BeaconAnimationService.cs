using TeleporatationRunes;
using TeleportationRunes.src.Dkosher.Beacon;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace TeleportationRunes.src.Dkosher.Animation
{
    public class BeaconAnimationService
    {
        public static void RunIdle(BEBeacon beacon, ICoreAPI api)
        {
            BEBehaviorAnimatable animUtil = beacon.GetBehavior<BEBehaviorAnimatable>();

            if (api.World.Side == EnumAppSide.Client && animUtil != null)
            {
                animUtil.animUtil.InitializeAnimator("tprunes:beacon");
                animUtil.animUtil.StartAnimation(new AnimationMetaData() { Animation = "Idle", Code = "idle", Weight = 10 });
            }
        }

        public static void RunTeleport(EntityAgent byEntity, ItemRune rune, Vec3d tpPosition)
        {
            BlockPos teleportBlock = new BlockPos((int)tpPosition.X, (int)tpPosition.Y, (int)tpPosition.Z);
            BEBeacon beacon = byEntity.World.BlockAccessor.GetBlockEntity(teleportBlock) as BEBeacon;

            if (beacon != null)
            {
                beacon.RunAnimation(BeaconState.TELEPORT);
                beacon.MarkDirty();
                rune.RunAnimation = false;
            }
        }
    }
}
