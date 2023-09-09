using System.IO;
using Vintagestory.API.Common;

namespace TeleportationRunes.src.Dkosher.ClientPacket
{
    internal class RuneClientPacketService
    {
        /**
         * Since Dialog logic exists only on the client side - that was the only way how Rune name can be set on the Server side.
         */
        public static void SetName(ItemSlot rune, byte[] data)
        {
            string text = "";

            if (data != null)
            {
                using (MemoryStream ms = new MemoryStream(data))
                {
                    BinaryReader reader = new BinaryReader(ms);
                    text = reader.ReadString();
                    if (text == null) text = "";
                }
                if (text == null) text = "";
            }
            if (rune != null)
            {
                rune.Itemstack.Attributes.SetString("name", text);
            }
        }
    }
}
