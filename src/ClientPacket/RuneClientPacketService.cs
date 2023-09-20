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
                text = System.Text.Encoding.UTF8.GetString(data, 0, data.Length);
                text ??= "";
            }
            rune?.Itemstack.Attributes.SetString("name", text);
        }
    }
}
