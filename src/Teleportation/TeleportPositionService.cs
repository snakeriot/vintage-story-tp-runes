using System;
using System.Collections.Generic;
using TeleporatationRunes;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace TeleportationRunes.src.Dkosher.Teleportation
{
    public class TeleportPositionService
    {
        public static Vec3d GetRandomizePosition(Vec3d pos, EntityAgent byEntity)
        {
            List<Vec3d> randomPositions = new() { new Vec3d(-1, 0, 0), new Vec3d(1, 0, 0), new Vec3d(0, 0, 1), new Vec3d(0, 0, -1) };
            return GetRandomizedTeleportPosition(pos, byEntity, randomPositions);
        }

        public static Vec3d GetPosition(ItemSlot slot)
        {
            return new Vec3d(slot.Itemstack.Attributes.GetInt("x"),
                             slot.Itemstack.Attributes.GetInt("y"),
                             slot.Itemstack.Attributes.GetInt("z"));
        }

        public static void SavePosition(BEBeacon bec, ItemSlot slot, BlockSelection blockSel)
        {
            if (bec == null)
            {
                slot.Itemstack.Attributes.RemoveAttribute("x");
                slot.Itemstack.Attributes.RemoveAttribute("y");
                slot.Itemstack.Attributes.RemoveAttribute("z");
            }
            else
            {
                slot.Itemstack.Attributes.SetInt("x", blockSel.Position.X);
                slot.Itemstack.Attributes.SetInt("y", blockSel.Position.Y);
                slot.Itemstack.Attributes.SetInt("z", blockSel.Position.Z);
            }
            slot.MarkDirty();
        }

        private static Vec3d GetRandomizedTeleportPosition(Vec3d pos, EntityAgent byEntity, List<Vec3d> vectors)
        {
            if (vectors.Count == 0)
            {
                return null;
            }
            Vec3d newPosition = pos.Clone();
            Random random = new Random();
            int index = random.Next(vectors.Count);
            newPosition.Add(vectors[index]);
            Block block = byEntity.World.BlockAccessor.GetBlock(new BlockPos((int)newPosition.X, (int)newPosition.Y, (int)newPosition.Z));
            Block blockAbove = byEntity.World.BlockAccessor.GetBlock(new BlockPos((int)newPosition.X, (int)newPosition.Y + 1, (int)newPosition.Z));

            if (!MaterialAllowsTeleport(block) || !MaterialAllowsTeleport(blockAbove))
            {
                vectors.RemoveAt(index);
                return GetRandomizedTeleportPosition(pos, byEntity, vectors);
            }
            return newPosition;
        }

        private static bool MaterialAllowsTeleport(Block block)
        {
            return block.BlockMaterial == EnumBlockMaterial.Air || block.BlockMaterial == EnumBlockMaterial.Plant;
        }
    }
}
