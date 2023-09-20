using System.IO;
using TeleporatationRunes;
using TeleportationRunes.src.Dkosher.Beacon;
using TeleportationRunes.src.Dkosher.Teleportation;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.GameContent;

namespace TeleportationRunes.src.Dkosher.Dialog
{
    public class DialogService
    {
        public static void BindDialog(BEBeacon bec, ItemSlot slot, BlockSelection blockSel, EntityAgent byEntity, ItemRune rune)
        {
            if (byEntity.World is IClientWorldAccessor)
            {
                GuiDialogBlockEntityTextInput dlg = new GuiDialogBlockEntityTextInput(Lang.Get("tprunes:beacon_location_name"),
                                                bec.Pos, "", bec.Api as ICoreClientAPI,
                                                new TextAreaConfig() { MaxWidth = 500, MaxHeight = 160 });
                dlg.OnTextChanged = (text) =>
                {
                    bec.RunAnimation(BeaconState.BIND_RUNE);
                    int packetId = (int)EnumSignPacketId.SaveText;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        BinaryWriter writer = new BinaryWriter(ms);
                        writer.Write(text);
                        byte[] data = ms.ToArray();
                        (bec.Api as ICoreClientAPI).Network.SendBlockEntityPacket(bec.Pos.X, bec.Pos.Y, bec.Pos.Z, packetId, data);
                    }
                    rune.SetName(text, slot);
                };
                dlg.OnCloseCancel = () =>
                {
                    int packetId = (int)EnumSignPacketId.SaveText;
                    (bec.Api as ICoreClientAPI).Network.SendBlockEntityPacket(bec.Pos.X, bec.Pos.Y, bec.Pos.Z, packetId, null);
                    rune.SetName("", slot);
                };
                dlg.TryOpen();
            }
            else
            {
                bec.SetRune(slot);
                TeleportPositionService.SavePosition(bec, slot, blockSel);
            }
        }
    }
}
