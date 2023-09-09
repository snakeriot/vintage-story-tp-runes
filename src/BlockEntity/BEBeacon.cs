using System.Text;
using TeleportationRunes.src.Dkosher.Animation;
using TeleportationRunes.src.Dkosher.Beacon;
using TeleportationRunes.src.Dkosher.ClientPacket;
using TeleportationRunes.src.Dkosher.ItemDescription;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace TeleporatationRunes
{
    public class BEBeacon : BlockEntity
    {
        private ItemSlot _rune;

        public override void Initialize(ICoreAPI api)
        {
            base.Initialize(api);
            BeaconAnimationService.RunIdle(this, api);
        }

        /**
         * Get Description for the Beacon.
         * 
         * @param forPlayer - Player for which description is generated.
         * @param dsc - String Builder for the description.
         */
        public override void GetBlockInfo(IPlayer forPlayer, StringBuilder dsc)
        {
            base.GetBlockInfo(forPlayer, dsc);
            ItemDescriptionService.GetBeaconDescription(dsc, this);
        }

        public override void OnReceivedClientPacket(IPlayer player, int packetid, byte[] data)
        {
            if (packetid == (int)EnumSignPacketId.SaveText)
            {
                RuneClientPacketService.SetName(_rune, data);
            }
        }

        public void RunAnimation(BeaconState state)
        {
            BEBehaviorAnimatable animUtil = GetBehavior<BEBehaviorAnimatable>();
            animUtil?.animUtil.StartAnimation(new AnimationMetaData() { Animation = state.Name, Code = state.Code, Weight = 1 });
            MarkDirty();
        }

        public void SetRune(ItemSlot rune)
        {
            _rune = rune;
        }
    }
}
