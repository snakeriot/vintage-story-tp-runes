using System.Text;
using TeleporatationRunes;
using TeleportationRunes.src.Dkosher.Teleportation;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;

namespace TeleportationRunes.src.Dkosher.ItemDescription
{
    public class ItemDescriptionService
    {

        /**
         * Get Description for the Beacon.
         * 
         * @param dsc - String Builder for the description.
         * @param beacon - Beacon for which description is generated.
         */
        public static void GetBeaconDescription(StringBuilder description, BEBeacon beacon)
        {
            description.AppendLine(Lang.Get("game:blockmaterial-Stone") + ": "
              + Lang.Get("game:block-rock-" + beacon.Block.Variant["stone"]));
            description.AppendLine(Lang.Get("game:blockmaterial-Metal") + ": "
                + Lang.Get("game:material-" + beacon.Block.Variant["metal"]));
        }

        /**
         * Get Description for the Rune stone.
         * Will show name of the Beacon and its coordinates if bound.
         * 
         * @param inSlot - ItemSlot
         * @param dsc - String Builder for the description.
         * @param rune - Rune for which description is generated.
         * @param world - IWorldAccessor is used to generate human-readable coordinates.
        */
        public static void GetRuneDescription(ItemSlot inSlot, StringBuilder dsc, ItemRune rune, IWorldAccessor world)
        {
            int teleportTime = rune.GetTpTime();
            dsc.AppendLine(Lang.Get("tprunes:tptime") + " " + teleportTime + " " + Lang.Get("tprunes:seconds"));
            string name = inSlot.Itemstack.Attributes.GetString("name");

            if (name != null)
            {
                dsc.AppendLine(Lang.Get("tprunes:rune_bound_to") + " " + name);
            }
            Vec3d teleportPosition = TeleportPositionService.GetPosition(inSlot);

            if (teleportPosition != null && world != null && world.BlockAccessor != null)
            {
                IBlockAccessor blockAccessor = world.BlockAccessor;
                double xPosition = teleportPosition.X - blockAccessor.MapSizeX / 2;
                double zPosition = teleportPosition.Z - blockAccessor.MapSizeZ / 2;
                dsc.AppendLine("X:" + xPosition + " Y:" + teleportPosition.Y + " Z:" + zPosition);
            }
        }

        public static WorldInteraction[] GetRuneHelpText(ItemSlot inSlot)
        {
            string name = inSlot.Itemstack.Attributes.GetString("name");
            string langCode = name == null ? Lang.Get("tprunes:bind_to_beacon") : (Lang.Get("tprunes:hold_to_teleport") + " " + name);

            WorldInteraction[] customInteractions = new WorldInteraction[] {
                new WorldInteraction()
                {
                    ActionLangCode = langCode,
                    MouseButton = EnumMouseButton.Right
                }
            };
            return customInteractions;
        }
    }
}
