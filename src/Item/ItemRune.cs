using System.Text;
using TeleportationRunes.src.Dkosher.Animation;
using TeleportationRunes.src.Dkosher.Dialog;
using TeleportationRunes.src.Dkosher.ItemDescription;
using TeleportationRunes.src.Dkosher.Particles;
using TeleportationRunes.src.Dkosher.Teleportation;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;

namespace TeleporatationRunes
{
    public class ItemRune : Item
    {
        private bool _teleported = true;
        private bool _validated = false;
        private bool _runAnimation = false;
        private bool _failPlayed;
        private BlockPos _initialPos;
        private ILoadedSound _sound;

        public bool Teleported { get => _teleported; set => _teleported = value; }
        public bool Validated { get => _validated; set => _validated = value; }
        public bool RunAnimation { get => _runAnimation; set => _runAnimation = value; }
        public BlockPos InitialPos { get => _initialPos; set => _initialPos = value; }
        public bool FailPlayed { get => _failPlayed; set => _failPlayed = value; }
        public ILoadedSound Sound { get => _sound; set => _sound = value; }

        /**
         * Get Description of the rune in hands.
         * If it's bound to the beacon - will show position name and coordinates.
         */
        public override void GetHeldItemInfo(ItemSlot inSlot, StringBuilder dsc, IWorldAccessor world, bool withDebugInfo)
        {
            base.GetHeldItemInfo(inSlot, dsc, world, withDebugInfo);
            ItemDescriptionService.GetRuneDescription(inSlot, dsc, this, world);
        }

        /**
         * Show help text:
         * - If not bound to the beacon - Right click to bind
         * - If bound - hold to teleport
         */
        public override WorldInteraction[] GetHeldInteractionHelp(ItemSlot inSlot)
        {
            return ItemDescriptionService.GetRuneHelpText(inSlot);
        }

        /**
         * Handler for action start:
         * - If user facing Beacon block - Bind dialog will be opened
         * - Otherwhise will attemt to start teleportation
         */
        public override void OnHeldInteractStart(ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, bool firstEvent, ref EnumHandHandling handling)
        {
            FailPlayed = false;
            slot.Itemstack.Attributes.RemoveAttribute("validationFailed");
            slot.MarkDirty();

            if (blockSel?.Position != null && byEntity.World.BlockAccessor.GetBlockEntity(blockSel.Position) is BEBeacon bec)
            {
                DialogService.BindDialog(bec, slot, blockSel, byEntity, this);
            }
            else
            {
                TeleportService.Start(byEntity, this);
            }
            handling = EnumHandHandling.PreventDefault;
        }

        /**
         * Process user's interation with rune, see body of the function for the additional details.
         */
        public override bool OnHeldInteractStep(float secondsUsed, ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel)
        {
            Vec3d pos = TeleportPositionService.GetPosition(slot);
            ICoreServerAPI sapi = byEntity.Api as ICoreServerAPI;
            bool serverSide = sapi != null;
            int teleportTime = GetTpTime();

            // Validate if beacon exists after one second of usage.
            if (secondsUsed > 1 && serverSide && !Validated)
            {
                TeleportPositionValidator.Validate(pos, slot, byEntity, this, sapi);
            }
            // Teleport if time elapsed, and validation passed.
            if (secondsUsed > teleportTime && !Teleported && Validated)
            {
                if (!byEntity.Teleporting && slot.Itemstack.Attributes.HasAttribute("x"))
                {
                    bool? teleported = TeleportService.Teleport(byEntity, this, pos, slot);

                    if (teleported.HasValue)
                    {
                        return (bool)teleported;
                    };
                }
            }
            // Spawn "in process" particles almost exactly till the teleport time.
            else if (secondsUsed + 0.4 < teleportTime && !Teleported && serverSide)
            {
                byEntity.World.SpawnParticles(ParticleFactory.Get(ParticleType.TELEPORTING, byEntity));
            }
            // Run "Teleporting" animation for the beacon.
            if (RunAnimation)
            {
                BeaconAnimationService.RunTeleport(byEntity, this, pos);
            }
            // At that moment of time rune durability might be depleted, and we need to check if rune exists.
            bool runeStillExist = slot != null && slot.Itemstack != null;
            // If validation vailed - spawn particles and play sound.
            if (runeStillExist && slot.Itemstack.Attributes.HasAttribute("validationFailed") && !serverSide)
            {
                TeleportService.Fail(byEntity, this);
            }
            // Animate rune - so it will raise in player's hands.
            if (byEntity.World is IClientWorldAccessor)
            {
                RuneAnimationService.LiftRune(byEntity, secondsUsed);
            }
            return !Teleported;
        }

        /**
         * Handler for the case when user stops interaction.
         */
        public override void OnHeldInteractStop(float secondsUsed, ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel)
        {
            Sound?.Stop();
            base.OnHeldInteractStop(secondsUsed, slot, byEntity, blockSel, entitySel);
        }

        // Attributes getters.
        public int GetTpTime()
        {
            return Attributes["tptime"].AsInt();
        }

        public void SetName(string name, ItemSlot slot)
        {
            if (name == null)
            {
                slot.Itemstack.Attributes.RemoveAttribute("name");
            }
            else
            {
                slot.Itemstack.Attributes.SetString("name", name);
            }

        }
    }
}
