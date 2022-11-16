using Vintagestory.API.Common;
using Vintagestory.GameContent;
using Vintagestory.API.Config;
using Vintagestory.API.Client;
using Vintagestory.API.Server;
using Vintagestory.API.MathTools;

using System.Text;
using System.IO;
using System;
using System.Collections.Generic;

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

    public override void OnHeldInteractStart(ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, bool firstEvent, ref EnumHandHandling handling)
    {
      _failPlayed = false;
      slot.Itemstack.Attributes.RemoveAttribute("validationFailed");
      slot.MarkDirty();

      if (blockSel?.Position != null && byEntity.World.BlockAccessor.GetBlockEntity(blockSel.Position) is BEBeacon bec)
      {
        BindToBeacon(bec, slot, blockSel, byEntity);
      }
      else
      {
        _teleported = false;
        _validated = false;
        _runAnimation = true;
        _initialPos = byEntity.Pos.AsBlockPos;

        if (byEntity.World is IClientWorldAccessor world)
        {
          _sound = world.LoadSound(GetSoundParams(byEntity.Pos.XYZ.ToVec3f()));
          _sound?.Start();
        }
      }

      handling = EnumHandHandling.PreventDefault;
    }

    public override void GetHeldItemInfo(ItemSlot inSlot, StringBuilder dsc, IWorldAccessor world, bool withDebugInfo)
    {
      base.GetHeldItemInfo(inSlot, dsc, world, withDebugInfo);
      string name = inSlot.Itemstack.Attributes.GetString("name");
      dsc.AppendLine(Lang.Get("tprunes:tptime") + " " + GetTpTime() + " " + Lang.Get("tprunes:seconds"));

      if (name != null)
      {
        dsc.AppendLine(Lang.Get("tprunes:rune_bound_to") + " " + name);
      }
    }

    public override WorldInteraction[] GetHeldInteractionHelp(ItemSlot inSlot)
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

    public override bool OnHeldInteractStep(float secondsUsed, ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel)
    {
      Vec3d pos = getTeleportPosition(slot);
      ICoreServerAPI sapi = byEntity.Api as ICoreServerAPI;
      bool serverSide = sapi != null;

      if (secondsUsed > 1 && serverSide && !_validated)
      {
        int chunkX = (int)pos.X / byEntity.Api.World.BlockAccessor.ChunkSize;
        int chunkZ = (int)pos.Z / byEntity.Api.World.BlockAccessor.ChunkSize;

        sapi.WorldManager.LoadChunkColumnPriority(chunkX, chunkZ, new ChunkLoadOptions()
        {
          OnLoaded = () => ValidateBeaconExistance(pos, slot, byEntity)
        });
      }
      if (secondsUsed > GetTpTime() && !_teleported && _validated)
      {
        if (!byEntity.Teleporting && slot.Itemstack.Attributes.HasAttribute("x"))
        {
          Vec3d tpPosition = GetRandomizedTeleportPosition(pos, byEntity, GetRandomVectorsList());

          if (tpPosition == null)
          {
            byEntity.World.SpawnParticles(ParticleFactory.Get(ParticleType.BLOCKED, byEntity));
            return !_teleported;
          }
          byEntity.TeleportToDouble(tpPosition.X, tpPosition.Y, tpPosition.Z, () => _teleported = true);
          BlockPos teleportTo = new BlockPos((int)pos.X, (int)pos.Y, (int)pos.Z);
          slot.Itemstack.Collectible.DamageItem(byEntity.World, byEntity, slot, (int)_initialPos.DistanceTo(teleportTo) / 10);
          slot.MarkDirty();

          byEntity.World.SpawnParticles(ParticleFactory.Get(ParticleType.TELEPORTED, byEntity));
        }
      }
      else if (secondsUsed + 0.4 < GetTpTime() && !_teleported && serverSide)
      {
        byEntity.World.SpawnParticles(ParticleFactory.Get(ParticleType.TELEPORTING, byEntity));
      }
      if (_runAnimation)
      {
        // Learn how to run animation on server side (for other players)
        BEBeacon beacon = byEntity.World.BlockAccessor.GetBlockEntity(new BlockPos((int)pos.X, (int)pos.Y, (int)pos.Z)) as BEBeacon;

        if (beacon != null)
        {
          beacon.RunAnimation(BEBeacon.State.TELEPORT);
          _runAnimation = false;
        }
      }
      if (slot.Itemstack.Attributes.HasAttribute("validationFailed") && !serverSide)
      {
        _sound?.Stop();

        if (byEntity.World is IClientWorldAccessor world && !_failPlayed)
        {
          ILoadedSound failSound = world.LoadSound(GetFailSoundParams(byEntity.Pos.XYZ.ToVec3f()));
          failSound?.Start();
          _failPlayed = true;
        }
      }
      if (byEntity.World is IClientWorldAccessor)
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
      return !_teleported;
    }

    public override void OnHeldInteractStop(float secondsUsed, ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel)
    {
      _sound?.Stop();
      base.OnHeldInteractStop(secondsUsed, slot, byEntity, blockSel, entitySel);
    }

    private List<Vec3d> GetRandomVectorsList()
    {
      return new List<Vec3d> { new Vec3d(-1, 0, 0), new Vec3d(1, 0, 0), new Vec3d(0, 0, 1), new Vec3d(0, 0, -1) };
    }

    private Vec3d GetRandomizedTeleportPosition(Vec3d pos, EntityAgent byEntity, List<Vec3d> vectors)
    {
      if (vectors.Count == 0)
      {
        return null;
      }
      Vec3d newPosition = pos.Clone();
      var random = new Random();
      int index = random.Next(vectors.Count);
      newPosition.Add(vectors[index]);
      Block block = byEntity.World.BlockAccessor.GetBlock(new BlockPos((int)newPosition.X, (int)newPosition.Y, (int)newPosition.Z));
      Block blockAbove = byEntity.World.BlockAccessor.GetBlock(new BlockPos((int)newPosition.X, (int)newPosition.Y + 1, (int)newPosition.Z));

      if (block.BlockMaterial != EnumBlockMaterial.Air || blockAbove.BlockMaterial != EnumBlockMaterial.Air)
      {
        vectors.RemoveAt(index);
        return GetRandomizedTeleportPosition(pos, byEntity, vectors);
      }
      return newPosition;
    }

    private void ValidateBeaconExistance(Vec3d pos, ItemSlot slot, EntityAgent byEntity)
    {
      BEBeacon beacon = byEntity.World.BlockAccessor.GetBlockEntity(new BlockPos((int)pos.X, (int)pos.Y, (int)pos.Z)) as BEBeacon;

      if (beacon == null)
      {
        // Beacon destroyed
        // TODO: Play failure sound.
        byEntity.World.SpawnParticles(ParticleFactory.Get(ParticleType.DETACHED, byEntity));
        setName(null, slot);
        saveBeaconPosition(null, slot, null);
        _teleported = true;
        slot.Itemstack.Attributes.SetBool("validationFailed", true);
        slot.MarkDirty();
      }
      _validated = true;
    }

    private void BindToBeacon(BEBeacon bec, ItemSlot slot, BlockSelection blockSel, EntityAgent byEntity)
    {
      if (byEntity.World is IClientWorldAccessor)
      {
        GuiDialogBlockEntityTextInput dlg = new GuiDialogBlockEntityTextInput(Lang.Get("tprunes:beacon_location_name"),
                                        bec.Pos, "", bec.Api as ICoreClientAPI, 500);
        dlg.OnTextChanged = (text) =>
        {
          bec.RunAnimation(BEBeacon.State.BIND_RUNE);
          int packetId = (int)EnumSignPacketId.SaveText;

          using (MemoryStream ms = new MemoryStream())
          {
            BinaryWriter writer = new BinaryWriter(ms);
            writer.Write(text);
            byte[] data = ms.ToArray();
            (bec.Api as ICoreClientAPI).Network.SendBlockEntityPacket(bec.Pos.X, bec.Pos.Y, bec.Pos.Z, packetId, data);
          }
          setName(text, slot);
        };
        dlg.OnCloseCancel = () =>
        {
          int packetId = (int)EnumSignPacketId.SaveText;
          (bec.Api as ICoreClientAPI).Network.SendBlockEntityPacket(bec.Pos.X, bec.Pos.Y, bec.Pos.Z, packetId, null);
          setName("", slot);
        };
        dlg.TryOpen();
      }
      else
      {
        bec.setRune(slot);
        saveBeaconPosition(bec, slot, blockSel);
      }
    }

    private void setName(string name, ItemSlot slot)
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

    private Vec3d getTeleportPosition(ItemSlot slot)
    {
      return new Vec3d(slot.Itemstack.Attributes.GetInt("x"),
                       slot.Itemstack.Attributes.GetInt("y"),
                       slot.Itemstack.Attributes.GetInt("z"));
    }

    private void saveBeaconPosition(BEBeacon bec, ItemSlot slot, BlockSelection blockSel)
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

    private int GetTpTime()
    {
      return this.Attributes["tptime"].AsInt();
    }

    private SoundParams GetSoundParams(Vec3f position)
    {
      String fileName = this.Attributes["tpsound"].AsString();
      return new SoundParams()
      {
        Location = new AssetLocation("tprunes", "sounds/rune/" + fileName),
        ShouldLoop = false,
        Position = position,
        DisposeOnFinish = true,
        Volume = 0.8f
      };
    }

    private SoundParams GetFailSoundParams(Vec3f position)
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
